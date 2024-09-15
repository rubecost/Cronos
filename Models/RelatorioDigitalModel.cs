using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class RelatorioDigitalModel
	{
		public int ID_Relatorio_Digital { get; set; }
		[Required(ErrorMessage = "O campo anexos é obrigatório.")]
		public string? Anexos { get; set; }
		[Required(ErrorMessage = "O campo nome é obrigatório.")]
		public string? Nome { get; set; }
		public string? Descricao { get; set; }
		public DateTime Data_Emissao { get; set; }
	}
}
