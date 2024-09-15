using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
    public class ImagensEquipamentoComDescricaoModel
    {
        public string? Tag { get; set; }
        public int Page { get; set; }
        public string? URL_Imagem { get; set; }
        public DateTime Data_Criacao { get; set; }
        public string? Descricao { get; set; }
    }
}
