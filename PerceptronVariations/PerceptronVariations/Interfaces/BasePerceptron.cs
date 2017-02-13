using PerceptronVariations.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerceptronVariations.Plotting;

namespace PerceptronVariations.Interfaces
{
	public abstract class BasePerceptron : IPerceptron
	{
		public ITransferFunction TransferFunction { get; }

		public BasePerceptron(ITransferFunction transferFunction)
		{
			TransferFunction = transferFunction;
		}

		public abstract IList<ScatterInfo> SolveProblem(IPerceptronProblem problem);

		public abstract void PostEpochOperation(double currentEpochError, double[] weights);

		public abstract double Normalize(double[] weights, Point currentPoint);
	}
}
