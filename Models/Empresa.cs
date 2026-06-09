using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Task4U.Models
{
    public class Empresa
    {
        [Key]
        [Column("id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Column("inc_data")]
        [ScaffoldColumn(false)]
        public DateTime IncData { get; set; } = DateTime.Now.ToUniversalTime();

        [Column("inc_usuario_id")]
        [ScaffoldColumn(false)]
        [Required]
        public int IncUsuarioId { get; set; }

        [Column("alt_data")]
        [ScaffoldColumn(false)]
        public DateTime AltData { get; set; } 

        [Column("alt_usuario_id")]
        [ScaffoldColumn(false)]
        [Required]
        public int AltUsuarioId { get; set; }

        [Column("razao_social")]
        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "A razão social é obrigatória.") ]
        [StringLength(60, ErrorMessage = "A razão social não deve passar de 60 caracteres")]
        public string RazaoSocial { get; set; } = string.Empty;

        [Column("nome_fantasia")]
        [Display(Name = "Nome Fantasia")]
        [Required(ErrorMessage = "O nome fantasia é obrigatório.") ]
        [StringLength(60, ErrorMessage = "O nome fantasia não deve passar de 60 caracteres")]
        public string NomeFantasia { get; set; } = string.Empty;

        [Column("email")]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [StringLength(60, ErrorMessage = "O e-mail não deve passar de 60 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Column("empresa_ativa")]
        [Display(Name = "Empresa Ativa ?")]
        public bool Ativa { get; set; } = true;

    }
}
