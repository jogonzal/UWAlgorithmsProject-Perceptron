using System.Collections.Generic;

using PerceptronVariations.Interfaces;
using PerceptronVariations.Perceptrons;
using PerceptronVariations.Problems;
using PerceptronVariations.TransferFunction;

namespace PerceptronVariations
{
	static class Program
	{
		static void Main(string[] args)
		{
			var perceptronProblemPairs = new List<PerceptronProblemPair>()
			{
				new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0)), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(), new XorLogicalGate()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(), new RandomNumberCategories()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(), new XorLogicalGate()),
			};

			// NOTE: Can be parallelized easily via .AsParallel()
			List<PerceptronResult> perceptronResults = new List<PerceptronResult>();
			foreach (PerceptronProblemPair perceptronProblemPair in perceptronProblemPairs)
			{
				PerceptronResult result = perceptronProblemPair.Perceptron.SolveProblem(perceptronProblemPair.Problem);
				perceptronResults.Add(result);
			}

			// NOTE: Can be parallelized easily via .AsParallel()
			foreach (PerceptronResult perceptronResult in perceptronResults)
			{
				perceptronResult.SaveResults();
			}
		}
	}
}
