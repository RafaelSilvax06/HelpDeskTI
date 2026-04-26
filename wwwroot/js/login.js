const form = document.getElementById("login-form");

form.addEventListener("submit", async function (event) {
    event.preventDefault();

    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;

    const dados = {
        email: email,
        senha: senha
    };

    const msgLogin = document.getElementById("login-message");

    try {
        const response = await fetch("/api/usuarios/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dados)
        });

        const resultado = await response.json();

        console.log("Resposta da API:", resultado);
        console.log("Status HTTP:", response.status);

        if (response.ok) {
            if (resultado.perfil === 1) {
                window.location.href = "/Dashboard/Analista";
            } else {
                window.location.href = "/Dashboard/Cliente";
            }
        } else {
            msgLogin.textContent = "Erro ao realizar login. Verifique suas credenciais.";
            msgLogin.style.color = "red";
        }

    } catch (error) {
        console.error("Erro ao chamar a API:", error);
    }
});