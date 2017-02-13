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

		public IList<RandomNumberCategories.Point> TrainingPoints
		{
			get
			{
				return new List<RandomNumberCategories.Point>()
				{
					new RandomNumberCategories.Point(new double[] {0, 0}, 0),
					new RandomNumberCategories.Point(new double[] {0, 1}, 1),
					new RandomNumberCategories.Point(new double[] {1, 0}, 1),
					new RandomNumberCategories.Point(new double[] {1, 1}, 0),
				};
			}
		}

		// In this case, same as test points
		public IList<RandomNumberCategories.Point> TestPoints => TrainingPoints;
	}
}
