using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Task4U.Models
{
    public enum NivelUsuario
    {
        [Display(Name = "Administrador")]
        Admin = 1,
        [Display(Name = "Colaborador")]
        Colaborador = 2,
        [Display(Name = "Gerente")]
        Gerente = 3
    }

    public class Usuario
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

        [Column("nome")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.") ]
        [StringLength(60, ErrorMessage = "O nome não deve passar de 60 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Column("email")]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [StringLength(60, ErrorMessage = "O e-mail não deve passar de 60 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Column("senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "A senha deve ter no máximo 50 caracteres")]
        public string? Senha { get; set; }

        [Column("ultimo_acesso_data")]
        [Display(Name = "Data Último Acesso ?")]
        public DateTime UltimoAcessoData { get; set;  } = DateTime.Now;

        [Column("ultimo_acesso_ip")]
        [Display(Name = "IP Último Acesso ?")]
        [StringLength(15)]
        public String UltimoAcessoIP { get; set; } = string.Empty;

        [Column("nivel_acesso")]
        [Display(Name = "Nível de Acesso")]
        [Required(ErrorMessage = "O nível de acesso é obrigatório")]
        public NivelUsuario Nivel { get; set; } = NivelUsuario.Colaborador;

        [Column("usuario_ativo")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

    }
}
