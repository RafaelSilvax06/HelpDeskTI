const botaoExcluir = document.getElementById("btn-excluir-chamado");
const mensagemExcluir = document.getElementById("mensagem-excluir");

function mostrarMensagemExcluir(texto, tipo = "erro") {
    if (!mensagemExcluir) return;

    mensagemExcluir.textContent = texto;
    mensagemExcluir.style.color = tipo === "erro" ? "#e74c3c" : "#27ae60";
}

if (botaoExcluir) {
    botaoExcluir.addEventListener("click", async function () {
        const id = Number(botaoExcluir.dataset.id);

        if (!id) {
            mostrarMensagemExcluir("ID do chamado não encontrado.");
            return;
        }

        const confirmarExclusao = confirm("Deseja realmente excluir este chamado?");

        if (!confirmarExclusao) {
            return;
        }

        botaoExcluir.disabled = true;
        botaoExcluir.textContent = "Excluindo...";

        try {
            const response = await fetch(`/api/chamado/excluirChamado/${id}`, {
                method: "DELETE"
            });

            if (response.ok) {
                mostrarMensagemExcluir("Chamado excluído com sucesso.", "sucesso");

                setTimeout(() => {
                    window.location.href = "/Dashboard/Cliente";
                }, 1000);
            } else {
                const erro = await response.json().catch(() => null);
                mostrarMensagemExcluir(erro?.message || "Erro ao excluir chamado.");

                botaoExcluir.disabled = false;
                botaoExcluir.textContent = "Excluir";
            }
        } catch (error) {
            console.error("Erro ao excluir chamado:", error);
            mostrarMensagemExcluir("Erro ao conectar ao servidor.");

            botaoExcluir.disabled = false;
            botaoExcluir.textContent = "Excluir";
        }
    });
}
