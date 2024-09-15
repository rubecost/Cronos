using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace Cronos.Models
{
    public class EquipamentosModel
    {
		public int ID_Equipamentos { get; set; }    
	    public string? Nome { get; set; }
		[Required(ErrorMessage = "O campo Tag do Equipamento é obrigatório.")]
		public string? Tag { get; set; }
		public string? Localizacao { get; set; }
		public string? Servico { get; set; }
		public string? Numero_Fabricacao { get; set; }
		public string? Img_Planta_Localizacao { get; set; }
		public string? Status_Equipamento { get; set; }
	}
}
