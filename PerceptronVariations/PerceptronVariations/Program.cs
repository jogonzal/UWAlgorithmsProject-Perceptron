using System;
using System.Collections.Generic;

using PerceptronVariations.Perceptrons;
using PerceptronVariations.Plotting;
using PerceptronVariations.Problems;
using PerceptronVariations.TransferFunction;

namespace PerceptronVariations
{
	static class Program
	{
		static void Main(string[] args)
		{
			// TestPlotting();

			var perceptronProblemPairs = new List<PerceptronProblemPair>()
			{
				// TODO: Running them independently is not appropiate
				//new PerceptronProblemPair(new PocketSimplePerceptron(new StepFunction(0), 1000, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 20, 0.02), new RandomNumberCategoriesSeparable()),
				//new PerceptronProblemPair(new PocketSimplePerceptron(new StepFunction(0), 20, 0.02), new RandomNumberCategoriesSeparable()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 20, 0.02), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new PocketSimplePerceptron(new StepFunction(0), 20, 0.02), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 5, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 10, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1, 0.01), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 5, 0.01), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 10, 0.01), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(), new XorLogicalGate()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(), new RandomNumberCategories()),
				new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1200, 1), new XorLogicalGate()),
				new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1200, 2), new XorLogicalGate()),
				new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1200, 4), new XorLogicalGate()),
				new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1200, 8), new XorLogicalGate()),
				new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1200, 16), new XorLogicalGate()),
				
			};

			// NOTE: Can be parallelized easily via .AsParallel()
			List<ScatterInfo> perceptronResults = new List<ScatterInfo>();
			foreach (PerceptronProblemPair perceptronProblemPair in perceptronProblemPairs)
			{
				IList<ScatterInfo> result = perceptronProblemPair.Perceptron.SolveProblem(perceptronProblemPair.Problem);
				perceptronResults.AddRange(result);
			}

			ScatterBuilder.BuildAndDumpScatters(perceptronResults);
		}

		private static void TestPlotting()
		{
			ScatterBuilder.BuildAndDumpScatters(new List<ScatterInfo>()
			{
				new ScatterInfo("Jorge title", "Y AXIS NAME", "X AXIS NAME", new List<Series>()
				{
					new Series(new List<double>() {1, 2, 3, 4, 5, 6, 7, 8}, "jorge series 1"),
					new Series(new List<double>() {8, 7, 6, 5, 4, 3, 2, 1}, "jorge series 2"),
				})
			});
		}
	}
}
