using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronVariations.Interfaces
{
	public interface ITransferFunction
	{
		double Evaluate(double input);
	}
}
