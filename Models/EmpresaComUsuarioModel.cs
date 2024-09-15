using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Models
{
    public class EmpresaComUsuarioModel : EmpresasModel
    {
        public int ID_Usuario { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        [MaxLength(255)]
        public string? URL_Image_Avatar { get; set; }
    }
}
