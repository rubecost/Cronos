using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class ClassificacaoNR13Model
	{
		public int ID_Classificacao_NR13 { get; set; }
		public string? Classe_Fluido { get; set; }
		public string? Grupo_Potencial_Risco { get; set; }
		public string? Categoria { get; set; }
	}
}