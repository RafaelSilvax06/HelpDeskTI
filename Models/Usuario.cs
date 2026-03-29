using System.ComponentModel.DataAnnotations;

namespace HelpDeskTI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Perfil { get; set; } // Admin, Tecnico, Usuario

        public bool Ativo { get; set; } = true;
    }
}