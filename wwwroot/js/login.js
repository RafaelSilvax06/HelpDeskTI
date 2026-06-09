// Elementos do formulário
const form = document.getElementById("login-form");
const emailInput = document.getElementById("email");
const senhaInput = document.getElementById("senha");
const botaoEntrar = form.querySelector("button[type='submit']");
const msgLogin = document.getElementById("login-message");

// Função para validar email
function validarEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}

// Função para limpar mensagens
function limparMensagem() {
    msgLogin.textContent = "";
    msgLogin.style.color = "";
    msgLogin.style.display = "none";
}

// Função para exibir mensagem
function exibirMensagem(texto, tipo = "erro") {
    msgLogin.textContent = texto;
    msgLogin.style.color = tipo === "erro" ? "#e74c3c" : "#27ae60";
    msgLogin.style.display = "block";
}

// Event listener do formulário
form.addEventListener("submit", async function (event) {
    event.preventDefault();

    // Limpar mensagem anterior
    limparMensagem();

    // Obter valores
    const email = emailInput.value.trim();
    const senha = senhaInput.value;

    // Validações no front-end
    if (!email) {
        exibirMensagem("Por favor, insira seu email.");
        return;
    }

    if (!validarEmail(email)) {
        exibirMensagem("Por favor, insira um email válido.");
        return;
    }

    if (!senha) {
        exibirMensagem("Por favor, insira sua senha.");
        return;
    }

    if (senha.length < 8) {
        exibirMensagem("A senha deve ter no mínimo 8 caracteres.");
        return;
    }

    // Preparar dados
    const dados = {
        email: email,
        senha: senha
    };

    // Desabilitar botão durante requisição
    botaoEntrar.disabled = true;
    botaoEntrar.textContent = "Entrando...";

    try {
        // Fazer requisição à API
        const response = await fetch("/api/usuarios/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dados)
        });

        // Parsear resposta
        const resultado = await response.json();

        console.log("Resposta da API:", resultado);
        console.log("Status HTTP:", response.status);

        if (response.ok && resultado) {
            // Login bem-sucedido
            exibirMensagem("Login realizado com sucesso! Redirecionando...", "sucesso");

            // Verificar perfil e redirecionar
            if (resultado.perfil === 1 || resultado.perfil === "Analista") {
                // Esperar um pouco antes de redirecionar
                setTimeout(() => {
                    window.location.href = "/Dashboard/Analista";
                }, 1000);
            } else if (resultado.perfil === 0 || resultado.perfil === "Cliente") {
                setTimeout(() => {
                    window.location.href = "/Dashboard/Cliente";
                }, 1000);
            } else {
                // Se perfil não for reconhecido
                setTimeout(() => {
                    window.location.href = "/Dashboard/Cliente";
                }, 1000);
            }
        } else {
            // Erro na requisição
            const mensagemErro = resultado?.message || "Email ou senha inválidos.";
            exibirMensagem(mensagemErro);
        }

    } catch (error) {
        console.error("Erro ao chamar a API:", error);
        exibirMensagem("Erro ao conectar ao servidor. Tente novamente mais tarde.");
    } finally {
        // Reabilitar botão
        botaoEntrar.disabled = false;
        botaoEntrar.textContent = "Entrar";
    }
});