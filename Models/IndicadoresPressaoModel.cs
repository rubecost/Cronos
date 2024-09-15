using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class IndicadoresPressaoModel
	{
		public int ID_Indicadores_Pressao { get; set; }
		public string? Identificacao_Tag { get; set; }
		public DateTime Ultima_Calibracao { get; set; }
		public string? Numero_Certificado { get; set; }
	}
}
