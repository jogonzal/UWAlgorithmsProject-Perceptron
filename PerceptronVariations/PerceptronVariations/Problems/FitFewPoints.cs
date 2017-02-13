using System.Collections.Generic;

using PerceptronVariations.Interfaces;

namespace PerceptronVariations.Problems
{
	public class FitFewPoints : IPerceptronProblem
	{
		public void Initialize()
		{
		}

		public int Dimensions => 2;
		public int OutputDimensions => 2;

		public IList<Point> TrainingPoints
		{
			get
			{
				return new List<Point>()
				{
					new Point(new double[] {0.05, 0.1}, new double[] {0.3, 0.4}),
					new Point(new double[] {0.8, 0.8}, new double[] {0.9, 0.7}),
				};
			}
		}

		// In this case, same as test points
		public IList<Point> TestPoints => TrainingPoints;
		public string Name => "FitFewPoints";
	}
}
