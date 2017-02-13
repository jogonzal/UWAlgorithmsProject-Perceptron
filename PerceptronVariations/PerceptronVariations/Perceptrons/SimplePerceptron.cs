using System;
using System.Collections.Generic;
using System.Linq;
using PerceptronVariations.Interfaces;
using PerceptronVariations.Plotting;
using PerceptronVariations.Problems;

namespace PerceptronVariations.Perceptrons
{
	public class SimplePerceptron : BasePerceptron
	{
		private readonly int _maxEpochs;
		private readonly double _learningRate;
		private IPerceptronProblem _problem;

		public SimplePerceptron(ITransferFunction transferFunction, int maxEpochs, double learningRate)
			: base(transferFunction)
		{
			_maxEpochs = maxEpochs;
			_learningRate = learningRate;
		}

		public override IList<ScatterInfo> SolveProblem(IPerceptronProblem problem)
		{
			_problem = problem;
			problem.Initialize();

			Random r = new Random();

			double[] weights = InitializeWeights(problem, r);

			List<double> trainingErrors;
			List<double> testErrors;
			Train(weights, problem.TrainingPoints, problem.TestPoints, out trainingErrors, out testErrors);

			return Test(weights, problem.TestPoints, trainingErrors, testErrors);
		}

		protected virtual double[] InitializeWeights(IPerceptronProblem problem, Random r)
		{
			double[] weights = new double[problem.Dimensions + 1];
			for (int index = 0; index < weights.Length; index++)
			{
				weights[index] = r.NextDouble();
			}
			return weights;
		}

		public virtual IList<ScatterInfo> Test(double[] weights, IList<Point> problemTestPoints, List<double> trainingErrors,
			List<double> testErrors)
		{
			IList<int> estimates = new List<int>();

			for (int p = 0; p < problemTestPoints.Count; p++)
			{
				Point problemTestPoint = problemTestPoints[p];
				double res = ObtainEstimate(weights, problemTestPoint);
				int i = Convert.ToInt32(Math.Round(res));
				estimates.Add(i);
			}

			return new List<ScatterInfo>()
			{
				new ScatterInfo($"{PerceptronName}Perceptron {_problem.Name} LearningRate({_learningRate}) Epochs({_maxEpochs}) ", "",
					"", new List<Series>()
					{
						new Series(trainingErrors, "Training"),
						new Series(testErrors, "Test"),
					})
			};
		}

		protected virtual string PerceptronName
		{
			get { return "Normal"; }
		}

		private double ObtainEstimate(double[] weights, Point currentPoint)
		{
			double estimate = ObtainSum(weights, currentPoint);
			double transferFunctionEstimate = TransferFunction.Evaluate(estimate);
			return transferFunctionEstimate;
		}

		public double ObtainSum(double[] weights, Point currentPoint)
		{
			double sum = 0;
			for (int weightIndex = 0; weightIndex < weights.Length - 1; weightIndex++)
			{
				sum += weights[weightIndex] * currentPoint.Input[weightIndex];
			}
			// Bias
			sum += weights[weights.Length - 1] * 1;

			return sum;
		}

		private void Train(double[] weights, IList<Point> problemTrainingPoints, IList<Point> testDataPoints, out List<double> trainingErrors, out List<double> testErrors)
		{
			double globalError;
			int epochs = 0;
			trainingErrors = new List<double>();
			testErrors = new List<double>();
			do
			{
				globalError = 0;

				double testError = CalculateTotalErrorTest(testDataPoints, weights);
				double trainingError = CalculateTotalErrorTest(problemTrainingPoints, weights);
				testErrors.Add(testError);
				trainingErrors.Add(trainingError);

				for (int pointIndex = 0; pointIndex < problemTrainingPoints.Count; pointIndex++)
				{
					Point currentPoint = problemTrainingPoints[pointIndex];
					double transferFunctionEstimate = ObtainEstimate(weights, currentPoint);
					double localError = currentPoint.ExpectedOutput[0] - transferFunctionEstimate;

					for (int weightIndex = 0; weightIndex < weights.Length - 1; weightIndex++)
					{
						weights[weightIndex] += _learningRate * localError * currentPoint.Input[weightIndex] * Normalize(weights, currentPoint);
					}
					// Bias
					weights[weights.Length - 1] += _learningRate * localError * 1;

					globalError += localError * localError / 2;
				}

				PostEpochOperation(globalError, weights);

				epochs++;
			} while (globalError > 0 && epochs < _maxEpochs);
		}

		public override double Normalize(double[] weights, Point currentPoint)
		{
			return 1.0;
		}

		public override void PostEpochOperation(double currentEpochError, double[] weights)
		{
			// No-Op
		}

		private double CalculateTotalError(IList<Point> problemTrainingPoints, double[] weights)
		{
			double totalError = 0;
			foreach (var problemTrainingPoint in problemTrainingPoints)
			{
				double estimate = ObtainEstimate(weights, problemTrainingPoint);
				double localError = (problemTrainingPoint.ExpectedOutput[0] - 1.0 *estimate);
				totalError += localError*localError/2;
			}
			return totalError;
		}

		protected virtual double CalculateTotalErrorTest(IList<Point> problemTrainingPoints, double[] weights)
		{
			double totalError = 0;
			foreach (var problemTrainingPoint in problemTrainingPoints)
			{
				double estimate = ObtainEstimate(weights, problemTrainingPoint);
				double localError = (problemTrainingPoint.ExpectedOutput[0] - 1.0 * estimate);
				totalError += localError * localError / 2;
			}
			return totalError;
		}
	}
}
