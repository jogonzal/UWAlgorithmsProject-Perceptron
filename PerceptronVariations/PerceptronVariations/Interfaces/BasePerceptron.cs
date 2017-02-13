using PerceptronVariations.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronVariations.Interfaces
{
	public abstract class BasePerceptron : IPerceptron
	{
		public ITransferFunction TransferFunction { get; }

		public BasePerceptron(ITransferFunction transferFunction)
		{
			TransferFunction = transferFunction;
		}

		public abstract PerceptronResult SolveProblem(IPerceptronProblem problem);

		public abstract void PostEpochOperation(double currentEpochError, double[] weights);

		public abstract double Normalize(double[] weights, RandomNumberCategories.Point currentPoint);
	}
}
