namespace Cronos.Models
{
    public class ResumoEquipamentosModel
    {
        public int Total_Equipamentos { get; set; }
        public int Total_Equipamentos_Vencidos { get; set; }
        public int Total_Vence_Em_Menos_De_2_Meses { get; set; }
        public int Total_Dentro_Do_Prazo { get; set; }
        public int Total_NR13_Nao_Obrigatorio { get; set; }
        public DateTime Data_Ultima_Recomendacao { get; set; }
        public string? Tag_Ultima_Recomendacao { get; set; }
        public string? Descricao_Ultima_Recomendacao { get; set; }
        public int Total_Valvulas_Seguranca { get; set; }
        public int Total_Valvulas_Vencidas { get; set; }
        public int Total_Valvulas_No_Prazo { get; set; }
        public int Total_Manometros { get; set; }
        public int Total_Manometros_Vencidos { get; set; }
        public int Total_Manometros_No_Prazo { get; set; }
    }
}