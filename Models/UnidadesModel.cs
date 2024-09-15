using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
    public class UnidadesModel
    {
		public int ID_Unidade { get; set; }
		public int ID_Empresa { get; set; }
		public int ID_Usuario { get; set; }
		public int ID_Setor { get; set; }
		public string? NomeEmpresa { get; set; }
		public string? NomeUnidade { get; set; }
		public string? Responsavel { get; set; }
		public string? Email { get; set; }
		public string? Telefone { get; set; }
		public string? Celular { get; set; }
		public string? Numero { get; set; }
		public string? CEP { get; set; }
		public string? Bairro { get; set; }
		public string? Cidade { get; set; }
		public string? Estado { get; set; }
		public string? Complemento { get; set; }
		public string? Logradouro { get; set; }
		public string? NomeSetor { get; set; }
	}
}
