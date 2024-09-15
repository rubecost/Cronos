using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
    public class UltimasInspecoesCompostaModel
    {
        public UltimaInspecaoModel? UltimaInspecao { get; set; }
        public MaiorTaxaCorrosaoAtualModel? MaiorTaxaCorrosaoAtual { get; set;  }
        public EstimativaVidaResidualModel? EstimativaVidaResidual { get; set; }
	}
}
