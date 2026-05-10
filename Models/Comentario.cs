using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HelpDeskTI.Models
{
    public class Comentario
    {
        public long Id { get; set; }

        [JsonIgnore]
        public Chamado? Chamado { get; set; }


        public Usuario? autor { get; set; }

        public string mensagem { get; set; } = string.Empty;

        public DateTime data_criacao { get; set; } = DateTime.Now;
    }
}
