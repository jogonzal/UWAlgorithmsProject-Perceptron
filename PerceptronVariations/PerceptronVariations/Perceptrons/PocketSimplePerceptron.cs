using System;
using System.Collections.Generic;
using System.Linq;
using PerceptronVariations.Interfaces;
using PerceptronVariations.Problems;

namespace PerceptronVariations.Perceptrons
{
	public class PocketSimplePerceptron : SimplePerceptron
	{

		private double[] _optimalWeights;

		private double _minError = double.MaxValue;

		public PocketSimplePerceptron(ITransferFunction transferFunction, int maxEpochs, double learningRate) : base(transferFunction, maxEpochs, learningRate)
		{
		}

		public override PerceptronResult Test(double[] weights, IList<RandomNumberCategories.Point> problemTestPoints)
		{
			return base.Test(_optimalWeights, problemTestPoints);
		}

		public override void PostEpochOperation(double currentEpochError, double[] weights)
		{
			if (currentEpochError < _minError)
			{
				_minError = currentEpochError;
				_optimalWeights = weights;
			}
		}
	}
}
