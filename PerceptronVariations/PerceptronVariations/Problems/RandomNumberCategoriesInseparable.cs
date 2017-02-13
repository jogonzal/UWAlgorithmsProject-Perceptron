using System;
using System.Collections.Generic;

using PerceptronVariations.Interfaces;

namespace PerceptronVariations.Problems
{
	public class RandomNumberCategoriesInseparable : RandomNumberCategoriesSeparable
	{
		public override Point BuildPoint(int expectedOutputSingle, Random r)
		{
			double[] expectedOutput = new double[] { expectedOutputSingle };
			double[] input = new double[3];
			switch (expectedOutputSingle)
			{
				case 0:
					input[0] = GetRandomNumber(1, 12, r);
					input[1] = GetRandomNumber(1, 3, r);
					input[2] = GetRandomNumber(0, 4, r);
					break;
				case 1:
					input[0] = GetRandomNumber(1, 10, r);
					input[1] = GetRandomNumber(-2, 3, r);
					input[2] = GetRandomNumber(-1, 5, r);
					break;
				default:
					throw new ArgumentException();
			}

			return new Point(input, expectedOutput);
		}

		public override string Name => "Inseparable";
	}
}
