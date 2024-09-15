using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class ProjetoModel
	{
		public int ID_Projeto { get; set; }
		public string? Volume { get; set; }
		public string? PMTA { get; set; }
		public string? Pressao_Teste_Hidrosatico { get; set; }
	}
}