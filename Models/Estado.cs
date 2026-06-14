using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Task4U.Models
{
    public class Estado
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

        [Column("codigo_ibge")]
        [Display(Name = "IBGE")]
        [Required(ErrorMessage = "O IBGE é obrigatório.") ]
        [StringLength(2, ErrorMessage = "O IBGE não deve ultrapassar 2 caracteres")]
        public string Ibge { get; set; } = string.Empty;

        [Column("sigla")]
        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "A sigla é obrigatória.") ]
        [StringLength(2, ErrorMessage = "A sigla não deve ultrapassar 2 caracteres")]
        public string Sigla { get; set; } = string.Empty;

        [Column("nome")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.") ]
        [StringLength(60, ErrorMessage = "O nome não deve ultrapassar 60 caracteres")]
        public string Nome { get; set; } = string.Empty;

      
    }
}
