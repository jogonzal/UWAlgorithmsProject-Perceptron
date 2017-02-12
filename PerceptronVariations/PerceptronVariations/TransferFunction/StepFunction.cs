using PerceptronVariations.Interfaces;

namespace PerceptronVariations.TransferFunction
{
	public class StepFunction : ITransferFunction
	{
		private readonly double _t;

		public StepFunction(double t)
		{
			_t = t;
		}

		public double Evaluate(double input)
		{
			return input > _t ? 1 : 0;
		}
	}
}