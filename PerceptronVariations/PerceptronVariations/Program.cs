using System;
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
				// TODO: Running them independently is not appropiate
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 5, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 10, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1, 0.01), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 5, 0.01), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 10, 0.01), new RandomNumberCategories()),
				////new PerceptronProblemPair(new SimplePerceptron(), new XorLogicalGate()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(), new RandomNumberCategories()),
				new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 10000), new XorLogicalGate()),
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
				Console.WriteLine(perceptronResult.ToString());
				perceptronResult.SaveResults();
			}

			Console.ReadKey();
		}
	}
}
