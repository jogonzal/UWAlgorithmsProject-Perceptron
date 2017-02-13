using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using HandlebarsDotNet;

using Newtonsoft.Json;

namespace PerceptronVariations.Plotting
{
	public class Series
	{
		public IList<double> Coordinates { get; set; }
		public string Name { get; set; }

		public Series(IList<double> coordinates, string name)
		{
			Coordinates = coordinates;
			Name = name;
		}
		public string CoordinatesAsString { get; set; }
	}

	public class ScatterInfo
	{
		public ScatterInfo(string title, string yAxisName, string xAxisName, IList<Series> series)
		{
			Title = title;
			YAxisName = yAxisName;
			XAxisName = xAxisName;
			Series = series;
		}

		public IList<Series> Series { get; set; }
		public string Title { get; set; }
		public string YAxisName { get; set; }
		public string XAxisName { get; set; }

	}

	public static class ScatterBuilder
	{
		public static void BuildAndDumpScatters(IList<ScatterInfo> scatterPlotInfos)
		{
			// We'll simply use http://jsfiddle.net/3r4s8455/

			// We'll need to make a few string replacements, then we'll be able to build the HTML file from the template
			string pathToHeatMapTemplate = Path.Combine(Directory.GetCurrentDirectory(), @"Plotting\ScatterTemplate.html");
			string heatMapContent = File.ReadAllText(pathToHeatMapTemplate);
			var template = Handlebars.Compile(heatMapContent);

			foreach (var scatterPlotInfo in scatterPlotInfos)
			{
				// Calculate coordinates for each series
				foreach (var serie in scatterPlotInfo.Series)
				{
					List<double[]> coordinates = new List<double[]>();
					for (int i = 0; i < serie.Coordinates.Count; i++)
					{
						coordinates.Add(new double[] {i, serie.Coordinates[i]});
					}
					serie.CoordinatesAsString = JsonConvert.SerializeObject(coordinates);
				}

				string templatizedFile = template(scatterPlotInfo);
				string calculatedPath = Path.Combine(Directory.GetCurrentDirectory(), @"GeneratedScatter" + scatterPlotInfo.Title + ".html");
				File.WriteAllText(calculatedPath, templatizedFile);

				// And open it (optionally)
				// Process.Start(calculatedPath);
			}
		}
	}
}
