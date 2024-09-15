using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class EstimativaVidaResidualModel
	{
		public int ID_Estimativa_Vida_Residual { get; set; }
		public string? Taxa_Corrosao_Atual { get; set; }
		public string? Taxa_Corrosao_Historica { get; set; }
	}
}