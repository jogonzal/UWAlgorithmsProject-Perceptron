using System.Collections.Generic;
using PerceptronVariations.Interfaces;

namespace PerceptronVariations.Problems
{
	public class XorLogicalGate : IPerceptronProblem
	{
		public void Initialize()
		{
			throw new System.NotImplementedException();
		}

		public int Dimensions { get; }
		public IList<RandomNumberCategories.Point> TrainingPoints { get; }
		public IList<RandomNumberCategories.Point> TestPoints { get; }
	}
}
