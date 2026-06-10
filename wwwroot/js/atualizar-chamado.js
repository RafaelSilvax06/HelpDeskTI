const botaoAtualizar = document.getElementById("btn-atualizar-chamado");
const statusSelect = document.getElementById("status");
const prioridadeSelect = document.getElementById("prioridade");
const mensagemAtualizar = document.getElementById("mensagem-atualizar");

function mostrarMensagemAtualizar(texto, tipo = "erro") {
    if (!mensagemAtualizar) return;

    mensagemAtualizar.textContent = texto;
    mensagemAtualizar.style.color = tipo === "erro" ? "#e74c3c" : "#27ae60";
}

if (botaoAtualizar) {
    botaoAtualizar.addEventListener("click", async function () {
        const id = Number(botaoAtualizar.dataset.id);

        if (!id) {
            mostrarMensagemAtualizar("ID do chamado não encontrado.");
            return;
        }

        const chamado = {
            id: id,
            status: Number(statusSelect.value),
            prioridade: Number(prioridadeSelect.value)
        };

        botaoAtualizar.disabled = true;
        botaoAtualizar.textContent = "Atualizando...";

        try {
            const response = await fetch("/api/chamado/atualizarChamado", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(chamado)
            });

            if (response.ok) {
                mostrarMensagemAtualizar("Chamado atualizado com sucesso!", "sucesso");

                setTimeout(() => {
                    window.location.href = `/Dashboard/DetalhesAnalista/${id}`;
                }, 1000);
            } else {
                const erro = await response.json().catch(() => null);
                mostrarMensagemAtualizar(erro?.message || "Erro ao atualizar chamado.");
            }
        } catch (error) {
            console.error("Erro ao atualizar chamado:", error);
            mostrarMensagemAtualizar("Erro ao conectar ao servidor.");
        } finally {
            botaoAtualizar.disabled = false;
            botaoAtualizar.textContent = "Atualizar";
        }
    });
}
