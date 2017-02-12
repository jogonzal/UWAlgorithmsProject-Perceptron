using PerceptronVariations.Interfaces;

namespace PerceptronVariations
{
	public class PerceptronProblemPair
	{
		public IPerceptron Perceptron { get; }
		public IPerceptronProblem Problem { get; }

		public PerceptronProblemPair(IPerceptron perceptron, IPerceptronProblem problem)
		{
			Perceptron = perceptron;
			Problem = problem;
		}
	}
}
