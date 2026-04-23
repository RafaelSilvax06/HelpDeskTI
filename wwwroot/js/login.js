const form = document.getElementById("login-form");
const msgLogin = document.getElementById("login-message");

form.addEventListener("submit", async function (event) {
    event.preventDefault();

    msgLogin.textContent = "";

    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;

    const dados = {
        email: email,
        senha: senha
    };

    try {
        const response = await fetch("/api/usuarios/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dados)
        });

        const resultado = await response.json();

        if (response.ok) {
            msgLogin.textContent = "Login realizado com sucesso!";
            msgLogin.style.color = "green";

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
        msgLogin.textContent = "Erro ao conectar com o servidor.";
        msgLogin.style.color = "red";
    }
});