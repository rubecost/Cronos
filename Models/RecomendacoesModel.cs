using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public enum StatusRecomendacao
	{
		Atendida,
		NaoAtendida,
		ParcialmenteAtendida
	}
	public class RecomendacoesModel
	{
		public int ID_Recomendacoes { get; set; }
		public DateTime Data_Criacao { get; set; }
		public string? Texto { get; set; }
		public string? Detalhes_Parcial { get; set; }
		public StatusRecomendacao Status_Recomendacao { get; set; } = StatusRecomendacao.NaoAtendida;
		public DateTime Data_Atualizacao_Status { get; set; }
	}
}