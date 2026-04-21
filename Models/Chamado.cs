using System.ComponentModel.DataAnnotations;

namespace HelpDeskTI.Models
{
    public class Chamado
    {
        public long Id { get; set; }

        public required string Titulo { get; set; }

        public required string Descricao { get; set; }

        public StatusChamado Status { get; set; }

        public Prioridade Prioridade { get; set; }

        public Usuario? Solicitante { get; set; }

        public Usuario? Analista { get; set; }

        public required Setor Categoria { get; set; }

        public string? Comentarios { get; set; }

        public DateTime DataAbertura { get; set; }

        public DateTime DataAtualizacao { get; set; }      

        public DateTime DataFechamento { get; set; }

    }


    public enum StatusChamado
    {
        Aberto,
        EmAndamento,
        Fechado
    }

    public enum Prioridade
    {
        Baixa,
        Media,
        Alta
    }

    public enum Setor
    {
        Suporte,
        Desenvolvimento,
        Infraestrutura,
        Redes,
        Bug
    }

}
