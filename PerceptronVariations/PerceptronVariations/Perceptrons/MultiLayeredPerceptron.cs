using System;
using System.Collections.Generic;
using PerceptronVariations.Interfaces;

namespace PerceptronVariations.Perceptrons
{
	public class MultiLayeredPerceptron : IPerceptron
	{
		private readonly double _learningRate;
		private readonly int _maxEpochs;

		public MultiLayeredPerceptron(double learningRate, int maxEpochs)
		{
			_learningRate = learningRate;
			_maxEpochs = maxEpochs;
		}

		List<Neuron> middleNeurons = new List<Neuron>();
		List<Neuron> outputNeurons = new List<Neuron>();

		public PerceptronResult SolveProblem(IPerceptronProblem problem)
		{
			const int dimensions = 2;
			double[][] inputs = new double[][]
			{
				new double[] {0.05, 0.1},
			};

			double[][] expected = new double[][]
			{
				new double[] {0.01, 0.99},
			};

			// Neurons and initial weight
			middleNeurons.Add(new Neuron(new double[] { 0.15, 0.2, 0.35 }));
			middleNeurons.Add(new Neuron(new double[] { 0.25, 0.3, 0.35 }));

			outputNeurons.Add(new Neuron(new double[] { 0.40, 0.45, 0.60 }));
			outputNeurons.Add(new Neuron(new double[] { 0.50, 0.55, 0.60 }));

			for (int epoch = 0; epoch < _maxEpochs; epoch++)
			{
				for (int inputIndex = 0; inputIndex < inputs.Length; inputIndex++)
				{
					var middleOutput = new double[middleNeurons.Count];
					for (int i = 0; i < middleNeurons.Count; i++)
					{
						middleOutput[i] = middleNeurons[i].CalculateOutput(inputs[inputIndex]);
					}

					var output = new double[outputNeurons.Count];
					for (int i = 0; i < outputNeurons.Count; i++)
					{
						output[i] = outputNeurons[i].CalculateOutput(middleOutput);
					}

					// Backward pass
					// First the output neurons
					double[] decOutErrorTotals = new double[dimensions];
					double[] decOutNets = new double[dimensions];
					for (int neuronIndex = 0; neuronIndex < outputNeurons.Count; neuronIndex++)
					{
						outputNeurons[neuronIndex].Edit();
						decOutErrorTotals[neuronIndex] = -(expected[inputIndex][neuronIndex] - output[neuronIndex]);
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
					double[] totalErrorForNeurons = new double[dimensions];
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
						for (int neuronInputIndex = 0; neuronInputIndex < dimensions; neuronInputIndex++)
						{
							double diffMidOutNet = middleOutput[neuronIndex] * (1 - middleOutput[neuronIndex]);
							double intermediateResult = diffMidOutNet * totalErrorForNeurons[neuronIndex] * inputs[inputIndex][neuronInputIndex];
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

					// Calculate again
					for (int i = 0; i < middleNeurons.Count; i++)
					{
						middleOutput[i] = middleNeurons[i].CalculateOutput(inputs[inputIndex]);
					}
					double totalError = 0;
					for (int i = 0; i < outputNeurons.Count; i++)
					{
						output[i] = outputNeurons[i].CalculateOutput(middleOutput);
						totalError += CalculateTotalError(expected[inputIndex][i], output[i]);
					}

					Console.WriteLine(totalError);
				}
			}

			throw new NotImplementedException();
		}

		private double CalculateTotalError(double expected1, double output1)
		{
			double diff = expected1 - output1;
			return diff*diff/2;
		}
	}
}
