using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class NotificacoesGeraisModel
	{
		public int ID_NotificacoesGerais { get; set;}
		[Required(ErrorMessage = "O campo Tipo da mensagem é obrigatório.")]
		public string? Tipo { get; set; }
		[Required(ErrorMessage = "O campo mensagem é obrigatório.")]
		public string? Mensagem { get; set; }
		public DateTime Data_Criacao { get; set; }
	}
}
