using System.Collections.Generic;

namespace PerceptronVariations.Interfaces
{
	public class PerceptronResult
	{
		public double TestErrorRate { get; }

		public PerceptronResult(IList<int> guess, IList<int> real)
		{
			int total = 0;
			int totalErrors = 0;

			for (int i = 0; i < guess.Count; i++)
			{
				if (guess[i] != real[i])
				{
					totalErrors++;
				}
				total++;
			}

			TestErrorRate = 1.0 * totalErrors / total;
		}

		public void SaveResults()
		{
			// TODO
		}
	}
}
