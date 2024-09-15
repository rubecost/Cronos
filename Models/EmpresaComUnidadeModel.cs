namespace Cronos.Models
{
    public class EmpresaComUnidadeModel
    {
        public int ID_Empresa { get; set; }
        public int ID_Unidade { get; set; }
        public string? NomeEmpresa { get; set; }
        public string? CNPJ { get; set; }
        public string? URL_Imagem_Logo_Superior { get; set; }
        public string? URL_Imagem_Logo_Esquerdo { get; set; }
        public string? NomeUnidade { get; set; }
        public string? Responsavel { get; set; }
        public string? Email { get; set; }
        public string? Telefones { get; set; }
        public string? TelefoneFixo { get; set; }
        public string? TelefoneCelular { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Cep { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Complemento { get; set; }        
    }
}
