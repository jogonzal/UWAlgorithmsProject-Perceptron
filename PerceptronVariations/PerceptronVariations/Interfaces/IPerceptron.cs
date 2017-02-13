using System.Collections.Generic;
using PerceptronVariations.Plotting;

namespace PerceptronVariations.Interfaces
{
	public interface IPerceptron
	{
		IList<ScatterInfo> SolveProblem(IPerceptronProblem problem);
	}
}
