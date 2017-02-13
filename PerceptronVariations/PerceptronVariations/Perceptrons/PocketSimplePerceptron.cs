using System;
using System.Collections.Generic;

using PerceptronVariations.Interfaces;
using PerceptronVariations.Plotting;

namespace PerceptronVariations.Perceptrons
{
	public class PocketSimplePerceptron : SimplePerceptron
	{

		private double[] _optimalWeights;

		private double _minError = double.MaxValue;

		public PocketSimplePerceptron(ITransferFunction transferFunction, int maxEpochs, double learningRate) : base(transferFunction, maxEpochs, learningRate)
		{
		}

		public override IList<ScatterInfo> Test(double[] weights, IList<Point> problemTestPoints, List<double> trainingErrors,
			List<double> testErrors)
		{
			return base.Test(_optimalWeights, problemTestPoints, trainingErrors, testErrors);
		}

		public override void PostEpochOperation(double currentEpochError, double[] weights)
		{
			if (currentEpochError < _minError)
			{
				_minError = currentEpochError;
				_optimalWeights = weights;
			}
		}

		protected override double CalculateTotalErrorTest(IList<Point> problemTrainingPoints, double[] weights)
		{
			return base.CalculateTotalErrorTest(problemTrainingPoints, _optimalWeights);
		}

		protected override string PerceptronName => "Pocket";

		protected override double[] InitializeWeights(IPerceptronProblem problem, Random r)
		{
			var originalweights = base.InitializeWeights(problem, r);
			_optimalWeights = originalweights;
			return originalweights;
		}
	}
}
