const formNovoChamado = document.getElementById("novo-chamado-form");

formNovoChamado.addEventListener("submit", async function (event) {
    event.preventDefault();

    const titulo = document.getElementById("titulo").value;
    const descricao = document.getElementById("descricao").value;
    const categoria = Number(document.getElementById("categoria").value);
    const prioridade = Number(document.getElementById("prioridade").value);

    const chamado = {
        titulo: titulo,
        descricao: descricao,
        categoria: categoria,
        prioridade: prioridade
    };

    console.log("Enviando chamado:", chamado);

    try {
        const response = await fetch("/api/chamado/novoChamado", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(chamado)
        });

        console.log("Status HTTP:", response.status);

        if (response.ok) {
            alert("Chamado criado com sucesso!");
            window.location.href = "/Dashboard/Cliente";
        } else {
            const erro = await response.text();
            console.error("Erro da API:", erro);
            alert("Erro ao criar chamado.");
        }
    } catch (error) {
        console.error("Erro ao conectar com a API:", error);
        alert("Erro ao conectar com o servidor.");
    }
});