using System;
using System.Collections.Generic;

using PerceptronVariations.Interfaces;

namespace PerceptronVariations.Problems
{
	public class RandomNumberCategories : IPerceptronProblem
	{
		public class Point
		{
			public double[] values = new double[dimensions];
			public int Category { get; }

			public static double GetRandomNumber(double minimum, double maximum, Random r)
			{
				return r.NextDouble() * (maximum - minimum) + minimum;
			}

			public Point(double[] valuesp, int category)
			{
				values = valuesp;
				Category = category;
			}

			public Point(int category, Random r)
			{
				Category = category;

				switch (category)
				{
					case 0:
						values[0] = GetRandomNumber(1, 10, r);
						values[1] = GetRandomNumber(1, 5, r);
						values[2] = GetRandomNumber(0, 4, r);
						break;
					case 1:
						values[0] = GetRandomNumber(1, 7, r);
						values[1] = GetRandomNumber(-2, 3, r);
						values[2] = GetRandomNumber(3, 12, r);
						break;
					default:
						throw new ArgumentException();
				}
			}
		}

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
				if (Point.GetRandomNumber(0, 1, r) < 0.5)
				{
					_trainingData[i] = new Point(0, r);
				}
				else
				{
					_trainingData[i] = new Point(1, r);
				}
			}

			_testData = new Point[totalTestPoints];
			for (int i = 0; i < totalTestPoints; i++)
			{
				if (Point.GetRandomNumber(0, 1, r) < 0.5)
				{
					_testData[i] = new Point(0, r);
				}
				else
				{
					_testData[i] = new Point(1, r);
				}
			}

		}

		public int Dimensions => dimensions;
		public IList<Point> TrainingPoints => _trainingData;
		public IList<Point> TestPoints => _testData;
	}
}
