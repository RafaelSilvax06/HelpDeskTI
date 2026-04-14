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

        public Setor Setor { get; set; }

        public required Usuario Solicitante { get; set; }

        public required Usuario Analista { get; set; }

        public required Categoria Categoria { get; set; }

        public required string Comentarios { get; set; }

        public  DateTime DataAbertura { get; set; }

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
