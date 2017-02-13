using System.Collections.Generic;

namespace PerceptronVariations.Interfaces
{
	public interface IPerceptronProblem
	{
		void Initialize();

		int Dimensions { get; }
		int OutputDimensions { get; }

		IList<Point> TrainingPoints { get; }
		IList<Point> TestPoints { get; }

		string Name { get; }
	}
}
