using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class EquipamentoCompostoModel
	{
		public EquipamentosModel? Equipamento { get; set; }
		public UltimaInspecaoModel? UltimaInspecao { get; set; }
		public ProximasInspecoesModel? ProximasInspecoes { get; set; }
		public ClassificacaoNR13Model? ClassificacaoNR13 { get; set; }
		public ProjetoModel? Projeto { get; set; }
		public OperacaoModel? Operacao { get; set; }
	}
}
