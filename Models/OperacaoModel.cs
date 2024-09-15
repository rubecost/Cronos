using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class OperacaoModel
	{
		public int ID_Operacao { get; set; }
		public string? Fluido { get; set; }
		public string? Pressao_Maxima { get; set; }
	}
}
