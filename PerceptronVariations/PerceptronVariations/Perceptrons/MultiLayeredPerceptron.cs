﻿using System;
using System.Collections.Generic;
using PerceptronVariations.Interfaces;
using PerceptronVariations.Plotting;

namespace PerceptronVariations.Perceptrons
{
	public class MultiLayeredPerceptron : IPerceptron
	{
		private readonly double _learningRate;
		private readonly int _maxEpochs;
		private readonly int _middleNeuronCount;

		public MultiLayeredPerceptron(double learningRate, int maxEpochs, int middleNeuronCount)
		{
			_learningRate = learningRate;
			_maxEpochs = maxEpochs;
			_middleNeuronCount = middleNeuronCount;
		}

		public IList<ScatterInfo> SolveProblem(IPerceptronProblem problem)
		{
			problem.Initialize();

			Random r = new Random();

			List<Neuron> middleNeurons = new List<Neuron>();
			// Neurons and initial weight
			for (int i = 0; i < _middleNeuronCount; i++)
			{
				double[] randomWeight = GetRandomWeight(problem.Dimensions + 1, r);
				middleNeurons.Add(new Neuron(randomWeight));
			}

			List<Neuron> outputNeurons = new List<Neuron>();
			for (int i = 0; i < problem.OutputDimensions; i++)
			{
				double[] randomWeight = GetRandomWeight(_middleNeuronCount + 1, r);
				outputNeurons.Add(new Neuron(randomWeight));
			}

			List<double> trainingErrors = new List<double>();
			List<double> testErrors = new List<double>();
			for (int epoch = 0; epoch < _maxEpochs; epoch++)
			{
				// Calculate training and test error
				var trainingError = CalculateTotalError(problem.TrainingPoints, middleNeurons, outputNeurons);
				var testError = CalculateTotalError(problem.TestPoints, middleNeurons, outputNeurons);
				trainingErrors.Add(trainingError);
				testErrors.Add(testError);

				for (int inputIndex = 0; inputIndex < problem.TrainingPoints.Count; inputIndex++)
				{
					var middleOutput = new double[middleNeurons.Count];
					for (int i = 0; i < middleNeurons.Count; i++)
					{
						middleOutput[i] = middleNeurons[i].CalculateOutput(problem.TrainingPoints[inputIndex].Input);
					}

					var output = new double[outputNeurons.Count];
					for (int i = 0; i < outputNeurons.Count; i++)
					{
						output[i] = outputNeurons[i].CalculateOutput(middleOutput);
					}

					// Backward pass
					// First the output neurons
					double[] decOutErrorTotals = new double[outputNeurons.Count];
					double[] decOutNets = new double[outputNeurons.Count];
					for (int neuronIndex = 0; neuronIndex < outputNeurons.Count; neuronIndex++)
					{
						outputNeurons[neuronIndex].Edit();
						decOutErrorTotals[neuronIndex] = -(problem.TrainingPoints[inputIndex].ExpectedOutput[neuronIndex] - output[neuronIndex]);
						decOutNets[neuronIndex] = output[neuronIndex] * (1 - output[neuronIndex]);
						for (int neuronInputIndex = 0; neuronInputIndex < middleNeurons.Count; neuronInputIndex++)
						{
							var diffw5 = middleOutput[neuronInputIndex];
							var diffw5Total = decOutErrorTotals[neuronIndex] * decOutNets[neuronIndex] * diffw5;
							outputNeurons[neuronIndex].nextWeights[neuronInputIndex] -= diffw5Total * _learningRate;
						}
					}

					// Now middle neurons
					// Calculate total error diff
					double[] totalErrorForNeurons = new double[middleNeurons.Count];
					for (int neuronIndex = 0; neuronIndex < middleNeurons.Count; neuronIndex++)
					{
						double totalErrorForNeuron = 0;
						for (int neuronInputIndex = 0; neuronInputIndex < outputNeurons.Count; neuronInputIndex++)
						{
							var weight = outputNeurons[neuronInputIndex].weights[neuronIndex];
							var derErrorOutIntermedio = decOutErrorTotals[neuronInputIndex] * decOutNets[neuronInputIndex] * weight;
							totalErrorForNeuron += derErrorOutIntermedio;
						}
						totalErrorForNeurons[neuronIndex] = totalErrorForNeuron;
					}
					// Apply the weight fix
					for (int neuronIndex = 0; neuronIndex < middleNeurons.Count; neuronIndex++)
					{
						middleNeurons[neuronIndex].Edit();
						for (int neuronInputIndex = 0; neuronInputIndex < problem.Dimensions; neuronInputIndex++)
						{
							double diffMidOutNet = middleOutput[neuronIndex] * (1 - middleOutput[neuronIndex]);
							double intermediateResult = diffMidOutNet * totalErrorForNeurons[neuronIndex] * problem.TrainingPoints[inputIndex].Input[neuronInputIndex];
							middleNeurons[neuronIndex].nextWeights[neuronInputIndex] -= _learningRate * intermediateResult;
						}
					}

					// Commit
					for (int neuronIndex = 0; neuronIndex < outputNeurons.Count; neuronIndex++)
					{
						outputNeurons[neuronIndex].Commit();
					}
					for (int neuronIndex = 0; neuronIndex < middleNeurons.Count; neuronIndex++)
					{
						middleNeurons[neuronIndex].Commit();
					}

					//if (inputIndex % 5 == 0)
					//{
					//	Console.WriteLine(totalError);
					//}
				}
			}

			return new List<ScatterInfo>()
			{
				new ScatterInfo($"MultilayerPerceptron {problem.Name} LearningRate {_learningRate} Epochs {_maxEpochs} IntermediateNeuronCount {_middleNeuronCount}",
				"TotalError", "Iteration", new List<Series>()
				{
					new Series(trainingErrors, "Training"),
					new Series(testErrors, "Test")
				})
			};
		}

		private double[] GetRandomWeight(int dimensions, Random r)
		{
			var rand = new double[dimensions];
			for (int i = 0; i < dimensions; i++)
			{
				rand[i] = r.NextDouble();
			}
			return rand;
		}

		private double CalculateTotalError(IList<Point> trainingPoints, List<Neuron> middleNeurons, List<Neuron> outputNeurons)
		{
			double[] middleOutput = new double[middleNeurons.Count];
			double[] outputs = new double[outputNeurons.Count];
			double totalError = 0;
			for (int inputIndex = 0; inputIndex < trainingPoints.Count; inputIndex++)
			{
				var trainingPoint = trainingPoints[inputIndex];
				for (int i = 0; i < middleNeurons.Count; i++)
				{
					middleOutput[i] = middleNeurons[i].CalculateOutput(trainingPoint.Input);
				}
				for (int i = 0; i < outputNeurons.Count; i++)
				{
					outputs[i] = outputNeurons[i].CalculateOutput(middleOutput);
					totalError += CalculateTotalError(trainingPoint.ExpectedOutput[i], outputs[i]);
				}
			}
			return totalError;
		}

		private double CalculateTotalError(double expected1, double output1)
		{
			double diff = expected1 - output1;
			return diff * diff / 2;
		}
	}
}
