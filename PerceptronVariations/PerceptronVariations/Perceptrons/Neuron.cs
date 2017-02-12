using System;

namespace PerceptronVariations.Perceptrons
{
	public class Neuron
	{
		public double[] weights;
		public double[] nextWeights;

		public Neuron(double[] weights)
		{
			this.weights = weights;
			this.nextWeights = new double[weights.Length];
			Edit();
		}

		public void Edit()
		{
			for (int i = 0; i < weights.Length; i++)
			{
				nextWeights[i] = weights[i];
			}
		}

		public void Commit()
		{
			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] = nextWeights[i];
			}
		}

		public double CalculateOutput(double[] input)
		{
			double total = 0;
			for (int i = 0; i < input.Length; i++)
			{
				total += input[i]*weights[i];
			}
			// Bias
			total += 1*weights[weights.Length - 1];
			return Sigmoid(total);
		}

		private double Sigmoid(double x)
		{
			return 1 / (1 + Math.Exp(- x));
		}
	}
}
