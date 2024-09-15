using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class CriarSetorModel
	{
		public int ID_Empresa { get; set; }
		public int ID_Unidade { get; set; }
		public string? Nome_Setor { get; set; }
		public int ID_Usuario { get; set; }
	}
}
