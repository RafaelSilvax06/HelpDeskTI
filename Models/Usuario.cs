using System.ComponentModel.DataAnnotations;

namespace HelpDeskTI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public bool Ativo { get; set; } = true;
    }
}