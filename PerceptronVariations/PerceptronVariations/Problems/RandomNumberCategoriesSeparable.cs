using System;
using System.Collections.Generic;

using PerceptronVariations.Interfaces;

namespace PerceptronVariations.Problems
{
	public class RandomNumberCategoriesSeparable : IPerceptronProblem
	{
		private Point[] _trainingData;
		private Point[] _testData;

		const int totalTrainingPoints = 50;
		const int totalTestPoints = 1000;
		const int dimensions = 3;

		public void Initialize()
		{
			Random r = new Random();

			_trainingData = new Point[totalTrainingPoints];
			for(int i = 0; i < totalTrainingPoints; i++)
			{
				if (GetRandomNumber(0, 1, r) < 0.5)
				{
					_trainingData[i] = BuildPoint(0, r);
				}
				else
				{
					_trainingData[i] = BuildPoint(1, r);
				}
			}

			_testData = new Point[totalTestPoints];
			for (int i = 0; i < totalTestPoints; i++)
			{
				if (GetRandomNumber(0, 1, r) < 0.5)
				{
					_testData[i] = BuildPoint(0, r);
				}
				else
				{
					_testData[i] = BuildPoint(1, r);
				}
			}

		}

		public int Dimensions => dimensions;
		public int OutputDimensions => 1;
		public IList<Point> TrainingPoints => _trainingData;
		public IList<Point> TestPoints => _testData;
		public virtual string Name => "Separable";

		public static double GetRandomNumber(double minimum, double maximum, Random r)
		{
			return r.NextDouble() * (maximum - minimum) + minimum;
		}

		public virtual Point BuildPoint(int expectedOutputSingle, Random r)
		{
			double[] expectedOutput = new double[] { expectedOutputSingle };
			double[] input = new double[3];
			switch (expectedOutputSingle)
			{
				case 0:
					input[0] = GetRandomNumber(7, 12, r);
					input[1] = GetRandomNumber(5, 25, r);
					input[2] = GetRandomNumber(0, 4, r);
					break;
				case 1:
					input[0] = GetRandomNumber(1, 7, r);
					input[1] = GetRandomNumber(-2, 3, r);
					input[2] = GetRandomNumber(5, 12, r);
					break;
				default:
					throw new ArgumentException();
			}

			return new Point(input, expectedOutput);
		}
	}
}
