namespace PerceptronVariations.Interfaces
{
	public interface IPerceptron
	{
		PerceptronResult SolveProblem(IPerceptronProblem problem);
	}
}
