const botaoAssumir = document.getElementById("btn-assumir-chamado");
const mensagemAssumir = document.getElementById("mensagem-assumir");

if (botaoAssumir) {
    botaoAssumir.addEventListener("click", async function () {
        const id = botaoAssumir.dataset.id;

        if (!id) {
            mensagemAssumir.textContent = "ID do chamado não encontrado.";
            mensagemAssumir.style.color = "red";
            return;
        }

        botaoAssumir.disabled = true;
        botaoAssumir.textContent = "Assumindo...";

        try {
            const response = await fetch(`/api/chamado/atenderChamado/${id}`, {
                method: "POST"
            });

            if (response.ok) {
                mensagemAssumir.textContent = "Chamado assumido com sucesso!";
                mensagemAssumir.style.color = "green";

                setTimeout(() => {
                    window.location.href = "/Dashboard/Analista";
                }, 1000);
            } else {
                const erro = await response.text();
                console.error("Erro:", erro);

                mensagemAssumir.textContent = "Erro ao assumir chamado.";
                mensagemAssumir.style.color = "red";

                botaoAssumir.disabled = false;
                botaoAssumir.textContent = "Assumir chamado";
            }
        } catch (error) {
            console.error("Erro ao conectar:", error);

            mensagemAssumir.textContent = "Erro ao conectar ao servidor.";
            mensagemAssumir.style.color = "red";

            botaoAssumir.disabled = false;
            botaoAssumir.textContent = "Assumir chamado";
        }
    });
}