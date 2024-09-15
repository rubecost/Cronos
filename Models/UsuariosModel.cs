using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cronos.Models
{
	public class UsuariosModel
	{
		public int ID_Usuario {get; set;}
		[Required(ErrorMessage = "O campo nome é obrigatório.")]
		public string? Nome { get; set; }
		[Required(ErrorMessage = "O campo e-mail é obrigatório.")]
		public string? Email_Principal { get; set; }
		public string? Cargo { get; set; }
		public string? URL_Image_Avatar { get; set; }
		[Required(ErrorMessage = "O campo senha é obrigatório.")]
		public string? Senha { get; set; }
		public string? Chave_API { get; set; }
		public DateTime Data_Criacao { get; set; }
	}
}
