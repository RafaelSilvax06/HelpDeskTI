using System.ComponentModel.DataAnnotations;

namespace HelpDeskTI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string senha { get; set; }

        public required Perfil perfil { get; set; }

    }

    public enum Perfil
    {
        Cliente,
        Analista
    }
}