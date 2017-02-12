using System;
using System.Collections.Generic;
using System.Linq;
using PerceptronVariations.Interfaces;
using PerceptronVariations.Problems;

namespace PerceptronVariations.Perceptrons
{
	public class SimplePerceptron : BasePerceptron
	{
		private const double learningRate = 0.04;
		private const int maximumEpochs = 10;

		public SimplePerceptron(ITransferFunction transferFunction) : base(transferFunction)
		{
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

			Train(weights, problem.TrainingPoints);

			return Test(weights, problem.TestPoints);
		}

		private PerceptronResult Test(double[] weights, IList<RandomNumberCategories.Point> problemTestPoints)
		{
			IList<int> estimates = new List<int>();
			for (int p = 0; p < problemTestPoints.Count; p++)
			{
				RandomNumberCategories.Point problemTestPoint = problemTestPoints[p];
				double res = ObtainEstimate(weights, problemTestPoint);
				int i = Convert.ToInt32(Math.Round(res));
				estimates.Add(i);
			}
			return new PerceptronResult(estimates, problemTestPoints.Select(s => s.Category).ToList());
		}

		private double ObtainEstimate(double[] weights, RandomNumberCategories.Point currentPoint)
		{
			double estimate = 0;
			for (int weightIndex = 0; weightIndex < weights.Length - 1; weightIndex++)
			{
				estimate += weights[weightIndex] * currentPoint.values[weightIndex];
			}
			// Bias
			estimate += weights[weights.Length - 1] * 1;
			double transferFunctionEstimate = TransferFunction.Evaluate(estimate);
			return transferFunctionEstimate;
		}

		private void Train(double[] weights, IList<RandomNumberCategories.Point> problemTrainingPoints)
		{
			double globalError;
			int epochs = 0;
			do
			{
				globalError = 0;
				for (int pointIndex = 0; pointIndex < problemTrainingPoints.Count; pointIndex++)
				{
					RandomNumberCategories.Point currentPoint = problemTrainingPoints[pointIndex];
					double transferFunctionEstimate = ObtainEstimate(weights, currentPoint);
					double localError = currentPoint.Category - transferFunctionEstimate;

					for (int weightIndex = 0; weightIndex < weights.Length - 1; weightIndex++)
					{
						weights[weightIndex] += learningRate * localError * currentPoint.values[weightIndex];
					}
					// Bias
					weights[weights.Length - 1] += learningRate * localError * 1;

					globalError += localError * localError;
				}
				epochs++;
			} while (globalError > 0 && epochs < maximumEpochs);
		}
	}
}
