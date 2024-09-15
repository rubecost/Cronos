using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class UltimaInspecaoModel
	{
		public int ID_Ultima_Inspecao { get; set; }
		public DateTime Data_Ultima_Interna { get; set; }
		public DateTime Data_Ultima_Externa { get; set; }
		public DateTime Data_Medicao_Espessura { get; set; }
		public DateTime Data_Testes_e_Ensaios { get; set; }
		public string? Numero_Documento_Inspecao { get; set; }
		public string? Observacao { get; set; }
	}
}
