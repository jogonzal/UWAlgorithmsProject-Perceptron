using System.Collections.Generic;
using PerceptronVariations.Interfaces;

namespace PerceptronVariations.Problems
{
	public class XorLogicalGate : IPerceptronProblem
	{
		public void Initialize()
		{
		}

		public int Dimensions => 2;
		public int OutputDimensions => 1;

		public IList<Point> TrainingPoints
		{
			get
			{
				return new List<Point>()
				{
					new Point(new double[] {0, 0}, new double[] {0}),
					new Point(new double[] {0, 1}, new double[] {1}),
					new Point(new double[] {1, 0}, new double[] {1}),
					new Point(new double[] {1, 1}, new double[] {0}),
				};
			}
		}

		// In this case, same as test points
		public IList<Point> TestPoints => TrainingPoints;
		public string Name => "XOR";
	}
}
