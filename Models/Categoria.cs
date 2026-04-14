using System.ComponentModel.DataAnnotations;
namespace HelpDeskTI.Models
{
    public class Categoria
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
    }

}
