using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class ProximasInspecoesModel
	{
		public int ID_Proximas_Inspecoes { get; set; }
		public DateTime Data_Proxima_Interna { get; set; }
		public DateTime Data_Proxima_Externa { get; set; }
		public DateTime Data_Medicao_Espessura { get; set; }
		public DateTime Data_Testes_e_Ensaios { get; set; }
		public string? Observacao { get; set; }
	}
}
