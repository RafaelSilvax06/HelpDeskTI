using System.ComponentModel.DataAnnotations;

namespace HelpDeskTI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Senha { get; set; }

        public required Perfil Perfil { get; set; }

    }

    public enum Perfil
    {
        Cliente,
        Analista
    }
}