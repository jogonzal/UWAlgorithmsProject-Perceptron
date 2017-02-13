using System;
using System.Collections.Generic;
using System.Linq;
using PerceptronVariations.Interfaces;
using PerceptronVariations.Problems;

namespace PerceptronVariations.Perceptrons
{
	public class SimplePerceptron : BasePerceptron
	{
		private readonly int _maxEpochs;
		private readonly double _learningRate;

		public SimplePerceptron(ITransferFunction transferFunction, int maxEpochs, double learningRate) : base(transferFunction)
		{
			_maxEpochs = maxEpochs;
			_learningRate = learningRate;
		}

		public override PerceptronResult SolveProblem(IPerceptronProblem problem)
		{
			problem.Initialize();
			
			Random r = new Random();

			double[] weights = new double[problem.Dimensions + 1];
			for (int index = 0; index < weights.Length; index++)
			{
				weights[index] = r.NextDouble();
			}

			Train(weights, problem.TrainingPoints, problem.TestPoints);

			return Test(weights, problem.TestPoints);
		}

		public virtual PerceptronResult Test(double[] weights, IList<RandomNumberCategories.Point> problemTestPoints)
		{
			IList<int> estimates = new List<int>();

			for (int p = 0; p < problemTestPoints.Count; p++)
			{
				RandomNumberCategories.Point problemTestPoint = problemTestPoints[p];
				double res = ObtainEstimate(weights, problemTestPoint);
				int i = Convert.ToInt32(Math.Round(res));
				estimates.Add(i);
			}

			return new PerceptronResult(estimates, problemTestPoints.Select(s => s.Category).ToList(),
				$"SimplePerceptron with LearningRate '{_learningRate}' and training over '{_maxEpochs}' epochs");
		}

		private double ObtainEstimate(double[] weights, RandomNumberCategories.Point currentPoint)
		{
			double estimate = ObtainSum(weights, currentPoint);
			double transferFunctionEstimate = TransferFunction.Evaluate(estimate);
			return transferFunctionEstimate;
		}

		public double ObtainSum(double[] weights, RandomNumberCategories.Point currentPoint)
		{
			double sum = 0;
			for (int weightIndex = 0; weightIndex < weights.Length - 1; weightIndex++)
			{
				sum += weights[weightIndex] * currentPoint.values[weightIndex];
			}
			// Bias
			sum += weights[weights.Length - 1] * 1;

			return sum;
		}

		private void Train(double[] weights, IList<RandomNumberCategories.Point> problemTrainingPoints, IList<RandomNumberCategories.Point> testDataPoints)
		{
			double globalError;
			int epochs = 0;
			do
			{
				globalError = 0;
				double currentEpochError = 0;
				for (int pointIndex = 0; pointIndex < problemTrainingPoints.Count; pointIndex++)
				{
					double totalError = CalculateTotalError(testDataPoints, weights);
					Console.WriteLine(totalError);

					RandomNumberCategories.Point currentPoint = problemTrainingPoints[pointIndex];
					double transferFunctionEstimate = ObtainEstimate(weights, currentPoint);
					double localError = currentPoint.Category - transferFunctionEstimate;

					for (int weightIndex = 0; weightIndex < weights.Length - 1; weightIndex++)
					{
						weights[weightIndex] += _learningRate * localError * currentPoint.values[weightIndex] * Normalize(weights, currentPoint);
					}
					// Bias
					weights[weights.Length - 1] += _learningRate * localError * 1;

					currentEpochError += localError * localError;
				}
				globalError += currentEpochError;

				PostEpochOperation(currentEpochError, weights);

				epochs++;
			} while (globalError > 0 && epochs < _maxEpochs);
		}

		public override double Normalize(double[] weights, RandomNumberCategories.Point currentPoint)
		{
			return 1.0;
		}

		public override void PostEpochOperation(double currentEpochError, double[] weights)
		{
			// No-Op
		}

		private double CalculateTotalError(IList<RandomNumberCategories.Point> problemTrainingPoints, double[] weights)
		{
			double totalError = 0;
			foreach (var problemTrainingPoint in problemTrainingPoints)
			{
				double estimate = ObtainEstimate(weights, problemTrainingPoint);
				double localError = (1.0*estimate - problemTrainingPoint.Category);
				totalError += localError*localError/2;
			}
			return totalError;
		}
	}
}
