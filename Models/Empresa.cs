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
        [StringLength(60, ErrorMessage = "A razão social não deve ultrapassar de 60 caracteres")]
        public string RazaoSocial { get; set; } = string.Empty;

        [Column("nome_fantasia")]
        [Display(Name = "Nome Fantasia")]
        [Required(ErrorMessage = "O nome fantasia é obrigatório.") ]
        [StringLength(60, ErrorMessage = "O nome fantasia não deve ultrapassar de 60 caracteres")]
        public string NomeFantasia { get; set; } = string.Empty;

        [Column("inscricao_municipal")]
        [Display(Name = "Inscrição Municipal")]
        [Required(ErrorMessage = "A Inscrição Municipal é obrigatória.") ]
        [StringLength(20, ErrorMessage = "A Inscrição Municipal não deve ultrapassar 20 caracteres")]
        public string InscricaoMunicipal { get; set; } = string.Empty;
        
        [Column("inscricao_estadual")]
        [Display(Name = "Inscrição Estadual")]
        [Required(ErrorMessage = "A Inscrição Estadual é obrigatória.") ]
        [StringLength(20, ErrorMessage = "A Inscrição Estadual não deve ultrapassar 20 caracteres")]
        public string InscricaoEstadual { get; set; } = string.Empty;

        [Column("cnpj")]
        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O CNPJ é obrigatório.") ]
        [StringLength(14, ErrorMessage = "O CNPJ não deve ultrapassar 14 caracteres")]
        public string CNPJ { get; set; } = string.Empty;

        [Column("logradouro")]
        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "O logradouro é obrigatório.") ]
        [StringLength(60, ErrorMessage = "O logradouro não deve ultrapassar 60 caracteres")]
        public string Logradouro { get; set; } = string.Empty;

        [Column("numero")]
        [Display(Name = "Número")]
        [Required(ErrorMessage = "O número é obrigatório.") ]
        [StringLength(10, ErrorMessage = "O número não deve ultrapassar 10 caracteres")]
        public string Número { get; set; } = string.Empty;

        [Column("complemento")]
        [Display(Name = "Complemento")]
        [StringLength(20, ErrorMessage = "O complemento não deve ultrapassar 20 caracteres")]
        public string Complemento { get; set; } = string.Empty;

        [Column("bairro")]
        [Display(Name = "Bairro")]
        [StringLength(40, ErrorMessage = "O bairro não deve ultrapassar 40 caracteres")]
        public string Bairro { get; set; } = string.Empty;

        [Column("cep")]
        [Display(Name = "CEP")]
        [StringLength(9, ErrorMessage = "O CEP não deve ultrapassar 9 caracteres")]
        public string Cep { get; set; } = string.Empty;

        [Column("telefone")]
        [Display(Name = "Telefone")]
        [StringLength(10, ErrorMessage = "O telefone não deve ultrapassar 10 caracteres")]
        public string Telefone { get; set; } = string.Empty;
        
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
