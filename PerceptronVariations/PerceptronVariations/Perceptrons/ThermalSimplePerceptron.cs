
using System;
using System.Collections.Generic;

using PerceptronVariations.Interfaces;
using PerceptronVariations.Plotting;

namespace PerceptronVariations.Perceptrons
{
	public class ThermalSimplePerceptron : SimplePerceptron
	{
		public double Temperature { get; }
		public ThermalSimplePerceptron(ITransferFunction transferFunction, int maxEpochs, double learningRate, double temperature) : base(transferFunction, maxEpochs, learningRate)
		{
			Temperature = temperature;
		}

		public override double Normalize(double[] weights, Point currentPoint)
		{
			double sum = ObtainSum(weights, currentPoint);

			return Math.Pow(Math.E, -(Math.Abs(sum) / Temperature));
		}
	}
}
