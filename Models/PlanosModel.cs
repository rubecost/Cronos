using System.ComponentModel.DataAnnotations;

namespace Cronos.Models
{
	public class PlanosModel
	{
		public int ID_Planos { get; set; }
		[Required(ErrorMessage = "O nome do plano é obrigatório.")]
		public string? Nome { get; set; }
		public string? Descricao { get; set; }
	}
	
}
