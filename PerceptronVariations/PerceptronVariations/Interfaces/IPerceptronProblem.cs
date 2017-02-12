using System.Collections.Generic;
using PerceptronVariations.Problems;

namespace PerceptronVariations.Interfaces
{
	public interface IPerceptronProblem
	{
		void Initialize();

		int Dimensions { get; }

		IList<RandomNumberCategories.Point> TrainingPoints { get; }
		IList<RandomNumberCategories.Point> TestPoints { get; }
	}
}
