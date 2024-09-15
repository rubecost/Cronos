using System.ComponentModel.DataAnnotations;

namespace Cronos.Models
{
    public class UsuarioComTelefonesModel
    {
        public int ID_Usuario { get; set; }
        public string? Nome { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Setor { get; set; }
        public string? Cargo { get; set; }
        public string? URL_Image_Avatar { get; set; }
        public string? Senha { get; set; }
        public string? Salt { get; set; }
        public string? Telefone_Fixo { get; set; }
        public string? Telefone_Celular { get; set; }
    }
}
