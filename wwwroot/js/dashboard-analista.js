document.addEventListener('DOMContentLoaded', () => {
    const btnAbertos = document.getElementById('btn-abertos');
    const btnAtendimentos = document.getElementById('btn-atendimentos');
    const chamadosList = document.getElementById('chamados-list');

    // Função para renderizar os chamados
    function renderChamados(chamados) {
        if (!chamados || chamados.length === 0) {
            chamadosList.innerHTML = `
                <div class="mensagem-vazia">
                    <p>Nenhum chamado disponível no momento.</p>
                </div>
            `;
            return;
        }

        let html = '';
        chamados.forEach(chamado => {
            html += `
                <article class="chamado-card">
                    <div>
                        <h2>${chamado.titulo}</h2>
                        <p>${chamado.descricao}</p>
                    </div>

                    <div class="chamado-tags">
                        <span class="status">${chamado.status}</span>
                        <span class="prioridade">${chamado.prioridade}</span>
                        <button
                            type="button"
                            class="btn-Detalhes"
                            onclick="window.location.href='/Dashboard/DetalhesAnalista/${chamado.id}'">
                            Detalhes
                        </button>
                    </div>

                    <div class="chamado-info">
                        <span>${chamado.solicitante?.nome ?? 'Sem solicitante'}</span>
                        <span>${new Date(chamado.dataAbertura).toLocaleDateString('pt-BR')}</span>
                        <span>${chamado.categoria}</span>
                    </div>
                </article>
            `;
        });

        chamadosList.innerHTML = html;
    }

    // Função para atualizar botões ativos
    function atualizarBotoes(ativo) {
        btnAbertos.classList.remove('active');
        btnAtendimentos.classList.remove('active');
        ativo.classList.add('active');
    }

    // Evento para "Chamados Abertos"
    btnAbertos.addEventListener('click', async () => {
        try {
            atualizarBotoes(btnAbertos);
            const response = await fetch('/api/chamados/abertos');
            const chamados = await response.json();
            renderChamados(chamados);
        } catch (error) {
            console.error('Erro ao carregar chamados abertos:', error);
            chamadosList.innerHTML = `<div class="mensagem-vazia"><p>Erro ao carregar chamados.</p></div>`;
        }
    });

    // Evento para "Meus Atendimentos"
    btnAtendimentos.addEventListener('click', async () => {
        try {
            atualizarBotoes(btnAtendimentos);
            const response = await fetch('/api/chamados/meus-atendimentos');
            const chamados = await response.json();
            renderChamados(chamados);
        } catch (error) {
            console.error('Erro ao carregar meus atendimentos:', error);
            chamadosList.innerHTML = `<div class="mensagem-vazia"><p>Erro ao carregar atendimentos.</p></div>`;
        }
    });
});
