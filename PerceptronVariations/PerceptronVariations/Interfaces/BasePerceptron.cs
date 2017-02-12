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
	}
}
