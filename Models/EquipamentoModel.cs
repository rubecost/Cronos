using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
	public class EquipamentoModel
	{
		public int ID_Empresa { get; set; }
		public int ID_Unidade { get; set; }
		public int ID_Setor { get; set; }
		[MaxLength(255)]
		public string? Tag { get; set; }
		[MaxLength(100)]
		public string? Localizacao { get; set; }
		[MaxLength(100)]
		public string? Servico { get; set; }
		[MaxLength(50)]
		public string? Numero_Fabricacao { get; set; }
		[MaxLength(25)]
		public string? Status_Equipamento { get; set; }
		[MaxLength(50)]
		public string? Volume { get; set; }
		[MaxLength(50)]
		public string? PMTA { get; set; }
		[MaxLength(50)]
		public string? Pressao_Teste_Hidrosatico { get; set; }
		[MaxLength(50)]
		public string? Fluido { get; set; }
		[MaxLength(50)]
		public string? Pressao_Maxima { get; set; }
		[MaxLength(50)]
		public string? Identificacao_Tag_Pressao { get; set; }
		public DateTime? Ultima_Calibracao_Pressao { get; set; }
		[MaxLength(50)]
		public string? Numero_Certificado_Pressao { get; set; }
		[MaxLength(50)]
		public string? Identificacao_Tag_Valvula { get; set; }
		[MaxLength(50)]
		public string? Pressao_Abertura { get; set; }
		[MaxLength(50)]
		public string? Bitola { get; set; }
		[MaxLength(50)]
		public string? DCBI { get; set; }
		public DateTime? Ultima_Calibracao_Valvula { get; set; }
        public DateTime? Proxima_Calibracao_Valvula { get; set; }
        [MaxLength(50)]
		public string? Numero_Certificado_Valvula { get; set; }
        public string? Identificacao_Tag_Manometro { get; set; }
        
        public string? Faixa_Pressao_Maxima_Manometro { get; set; }
        public string? Faixa_Pressao_Minima_Manometro { get; set; }
        public string? Unidade_Pressao_Manometro { get; set; }
        public DateTime? Ultima_Calibracao_Manometro { get; set; }
        public DateTime? Proxima_Calibracao_Manometro { get; set; }
        public string? Numero_Certificado_Manometro { get; set; }
        [MaxLength(50)]
		public string? Classe_Fluido { get; set; }
		[MaxLength(50)]
		public string? Grupo_Potencial_Risco { get; set; }
		[MaxLength(50)]
		public string? Categoria { get; set; }
		[MaxLength(50)]
		public string? Numero_Documento_Inspecao { get; set; }
		public DateTime? Data_Ultima_Interna { get; set; }
		public DateTime? Data_Ultima_Externa { get; set; }
		public DateTime? Data_Ultima_Medicao_Espessura { get; set; }
		public DateTime? Data_Ultimo_Teste_e_Ensaio { get; set; }
		[MaxLength(1000)]
		public string? Observacao_Inspecao { get; set; }
		public DateTime? Data_Proxima_Interna { get; set; }
		public DateTime? Data_Proxima_Externa { get; set; }
		public DateTime? Data_Proxima_Medicao_Espessura { get; set; }
		public DateTime? Data_Proximo_Teste_e_Ensaio { get; set; }
		[MaxLength(1000)]
		public string? Observacao_Proximas_Inspecoes { get; set; }
		[MaxLength(255)]
		public string? Taxa_Corrosao_Atual { get; set; }
		[MaxLength(50)]
		public string? Taxa_Corrosao_Historica { get; set; }
		public DateTime? Data_Maior_Taxa { get; set; }
		[MaxLength(1000)]
		public string? Texto_Recomendacao { get; set; }
		public int Status { get; set; }
		[MaxLength(100)]
		public string? NomeEmpresa { get; set; }
		[MaxLength(100)]
		public string? NomeUnidade { get; set; }
		[MaxLength(100)]
		public string? NomeSetor { get; set; }


	}
}
