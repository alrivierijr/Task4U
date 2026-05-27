using System.ComponentModel.DataAnnotations;

namespace Task4U.Models
{
    public enum NivelUsuario
    {
        Admin = 1,
        Colaborador = 2,
        Gerente = 3
    }

    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório.") ]
        [StringLength(60, ErrorMessage = "O nome não deve passar de 60 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [StringLength(60, ErrorMessage = "O e-mail não deve passar de 60 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 e no máximo 50 caracteres")]
        public string Senha { get; set; } = string.Empty;

        public DateTime UltimoAcessoData { get; set;  } = DateTime.Now;

        [StringLength(15)]
        public String UltimoAcessoIP { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nível de acesso é obrigatório")]
        [Display(Name = "Nível de Acesso")]
        public NivelUsuario Nivel { get; set; } = NivelUsuario.Colaborador;

        [Display(Name = "Usuário Ativo ?")]
        public bool Ativo { get; set; } = true;

    }
}
