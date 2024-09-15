using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cronos.Models
{
	public class ImagensEquipamentoModel
	{
		public int ID_Imagens_Equipamento { get; set; }
		[Required(ErrorMessage = "O campo Url da imagem é obrigatório.")]
		public ObservableCollection<string>? URLs_Imagens { get; set; }
		[Required(ErrorMessage = "O campo Data da criação é obrigatório.")]
		public DateTime Data_Criacao { get; set; }

		public ImagensEquipamentoModel() 
		{
			URLs_Imagens = new ObservableCollection<string>();
		}
	}
}
