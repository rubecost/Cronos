using System.ComponentModel.DataAnnotations;

namespace Cronos.Models
{
    public class EmailsNotificacaoComUsuarioModel
    {
        public int ID_Usuarios { get; set; }
        public string? Nome_Responsavel_1 { get; set; }
        public string? Email_1 { get; set; }
        public string? Nome_Responsavel_2 { get; set; }
        public string? Email_2 { get; set; }
        public string? Nome_Responsavel_3 { get; set; }
        public string? Email_3 { get; set; }
    }
}
