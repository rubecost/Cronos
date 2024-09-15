using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class RegistroHistoricoModel
	{
        public int Id { get; set; }
        public string? Email_Usuario { get; set; }
        public string? Setor { get; set; }
        public string? Tipo_Atualizacao { get; set; }
        public string? Item_Atualizado { get; set; }
        public DateTime? Data_Hora { get; set; }
        public int Page { get; set; }
    }
}
