using System.ComponentModel.DataAnnotations;

namespace HelpDeskTI.Models
{
    public class Chamado
    {
        public long Id { get; set; }

        public string titulo { get; set; }

        public string descricao { get; set; }

        public StatusChamado status { get; set; }

        public Prioridade prioridade { get; set; }

        public Setor setor { get; set; }

        public Usuario solicitante { get; set; }

        public Usuario analista { get; set; }

        public Categoria categoria { get; set; }

        public string comentarios { get; set; }

        public DateTime dataAbertura { get; set; }

        public DateTime dataAtualizacao { get; set; }

        public DateTime dataFechamento { get; set; }

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
