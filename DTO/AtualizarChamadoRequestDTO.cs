using HelpDeskTI.Models;

namespace HelpDeskTI.DTO
{
    public class AtualizarChamadoRequestDTO
    {
        public long Id { get; set; }

        public StatusChamado Status { get; set; }

        public Prioridade Prioridade { get; set; }
    }
}
