﻿using System;
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
				
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 100, 0.02), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new PocketSimplePerceptron(new StepFunction(0), 100, 0.02), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 5, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 10, 0.02), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1, 0.01), new RandomNumberCategories()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 5, 0.01), new RandomNumberCategories()),

				// Thermal and simple - Inseparable, thermal better
				//new PerceptronProblemPair(new ThermalSimplePerceptron(new StepFunction(0), 100, 0.0001, 10), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 100, 0.0001), new RandomNumberCategoriesInseparable()),

				// PocketPerceptron is better than simpleperceptron at XOR, but none can converge on it
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1000, 0.05), new XorLogicalGate()),
				//new PerceptronProblemPair(new PocketSimplePerceptron(new StepFunction(0), 1000, 0.05), new XorLogicalGate()),

				// SimplePerceptron, Pocket, non-separable. Overfitting on SimplePerceptron, but not on Pocket
				//new PerceptronProblemPair(new PocketSimplePerceptron(new StepFunction(0), 1000, 0.0001), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 1000, 0.0001), new RandomNumberCategoriesInseparable()),

				// Pocket is better than no pocket
				//new PerceptronProblemPair(new PocketSimplePerceptron(new StepFunction(0), 100, 0.0001), new RandomNumberCategoriesSeparable()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 100, 0.0001), new RandomNumberCategoriesSeparable()),

				// SimplePerceptron, different learning rates
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 100, 0.01), new RandomNumberCategoriesSeparable()),
				//new PerceptronProblemPair(new SimplePerceptron(new StepFunction(0), 100, 0.0001), new RandomNumberCategoriesSeparable()),

				// XOR gate and how it behaves against Multilayer
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 50000, 1), new XorLogicalGate()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 5000, 2), new XorLogicalGate()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 4), new XorLogicalGate()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 8), new XorLogicalGate()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 16), new XorLogicalGate()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 20000, 32), new XorLogicalGate()),

				// Multilayer fitting a few points
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1000, 2), new FitFewPoints()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1000, 4), new FitFewPoints()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1000, 8), new FitFewPoints()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1000, 16), new FitFewPoints()),
				// new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 1000, 32), new FitFewPoints()),

				// Multilayer fitting a few points. Works very nice.
				//new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 2), new FitFewPoints()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 4), new FitFewPoints()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 8), new FitFewPoints()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 16), new FitFewPoints()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(0.5, 2000, 32), new FitFewPoints()),

				// Overfitting when trying to make all neurons work in inseparable data
				//new PerceptronProblemPair(new MultiLayeredPerceptron(1.5, 2000, 1), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(1.5, 2000, 2), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(1.5, 2000, 4), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(1.5, 2000, 8), new RandomNumberCategoriesInseparable()),
				//new PerceptronProblemPair(new MultiLayeredPerceptron(1.5, 2000, 16), new RandomNumberCategoriesInseparable())
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
