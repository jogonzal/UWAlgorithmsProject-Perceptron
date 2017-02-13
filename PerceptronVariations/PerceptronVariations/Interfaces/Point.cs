namespace PerceptronVariations.Interfaces
{
	public class Point
	{
		public double[] Input { get; }
		public double[] ExpectedOutput { get; }

		public Point(double[] input, double[] expectedOutput)
		{
			Input = input;
			ExpectedOutput = expectedOutput;
		}
	}
}
