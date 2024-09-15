using Cronos.Models;

namespace Cronos.Models
{
    public class EquipamentosCompostoModel
    {
        public int ID_Setor { get; set; }
        public string? Tag { get; set; }
        public int Page { get; set; }
        public string? Localizacao { get; set; }
        public string? Status_Equipamento { get; set; }
        public string? Status_Inspecao_Nr13 { get; set; }
        public string? Valvula_Status_Operacional { get; set; }
        public string? Manometro_Status_Operacional { get; set; }
        public string? Status_Recomendacao { get; set; }
        public string? Classe_Fluido { get; set; }
        public string? Grupo_Potencial_Risco { get; set; }
        public string? Categoria { get; set; }
        public string? Volume { get; set; }
        public string? PMTA { get; set; }
        public string? Pressao_Teste_Hidrosatico { get; set; }
        public string? Fluido { get; set; }
        public string? Pressao_Maxima { get; set; }
    }
}
