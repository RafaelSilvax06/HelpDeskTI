// Elementos do formulário
const form = document.getElementById("form-cadastro");
const nomeInput = document.getElementById("nome");
const cpfInput = document.getElementById("cpf");
const emailInput = document.getElementById("email");
const senhaInput = document.getElementById("senha");
const perfilSelect = document.getElementById("perfil");
const botaoCadastro = form.querySelector("button[type='submit']");
const msgCadastro = document.getElementById("cadastro-message");

// Elementos para exibir erros
const erroNome = document.getElementById("erro-nome");
const erroCpf = document.getElementById("erro-cpf");
const erroEmail = document.getElementById("erro-email");
const erroSenha = document.getElementById("erro-senha");
const erroPerfil = document.getElementById("erro-perfil");

// ============================================
// FUNÇÕES DE VALIDAÇÃO
// ============================================

// Validar nome
function validarNome(nome) {
    nome = nome.trim();
    if (!nome) return { valido: false, msg: "Por favor, insira seu nome." };
    if (nome.length < 3) return { valido: false, msg: "Nome deve ter no mínimo 3 caracteres." };
    if (!/^[a-záàâãéèêíïóôõöúçñ\s]+$/i.test(nome)) return { valido: false, msg: "Nome deve conter apenas letras." };
    return { valido: true, msg: "" };
}

// Validar CPF
function validarCPF(cpf) {
    cpf = cpf.replace(/\D/g, "");
    if (!cpf) return { valido: false, msg: "Por favor, insira seu CPF." };
    if (cpf.length !== 11) return { valido: false, msg: "CPF deve conter 11 dígitos." };
    if (/^(\d)\1{10}$/.test(cpf)) return { valido: false, msg: "CPF inválido." };

    let soma = 0;
    for (let i = 0; i < 9; i++) soma += (cpf[i] - '0') * (10 - i);
    let dv1 = (soma * 10) % 11;
    if (dv1 === 10) dv1 = 0;
    if (dv1 !== (cpf[9] - '0')) return { valido: false, msg: "CPF inválido." };

    soma = 0;
    for (let i = 0; i < 10; i++) soma += (cpf[i] - '0') * (11 - i);
    let dv2 = (soma * 10) % 11;
    if (dv2 === 10) dv2 = 0;
    if (dv2 !== (cpf[10] - '0')) return { valido: false, msg: "CPF inválido." };

    return { valido: true, msg: "" };
}

// Formatar CPF enquanto digita
function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, "").slice(0, 11);
    if (cpf.length > 9) return cpf.slice(0, 3) + "." + cpf.slice(3, 6) + "." + cpf.slice(6, 9) + "-" + cpf.slice(9);
    if (cpf.length > 6) return cpf.slice(0, 3) + "." + cpf.slice(3, 6) + "." + cpf.slice(6);
    if (cpf.length > 3) return cpf.slice(0, 3) + "." + cpf.slice(3);
    return cpf;
}

// Validar email
function validarEmail(email) {
    email = email.trim().toLowerCase();
    if (!email) return { valido: false, msg: "Por favor, insira seu email." };
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) return { valido: false, msg: "Email inválido." };
    return { valido: true, msg: "" };
}

// Validar senha
function validarSenha(senha) {
    if (!senha) return { valido: false, msg: "Por favor, insira sua senha." };
    if (senha.length < 8) return { valido: false, msg: "Senha deve ter no mínimo 8 caracteres." };
    return { valido: true, msg: "" };
}

// Validar perfil
function validarPerfil(perfil) {
    if (!perfil) return { valido: false, msg: "Por favor, selecione um perfil." };
    if (perfil !== "0" && perfil !== "1") return { valido: false, msg: "Perfil inválido." };
    return { valido: true, msg: "" };
}

// ============================================
// FUNÇÕES DE EXIBIÇÃO
// ============================================

function exibirErro(elemento, msg) {
    elemento.textContent = msg;
    elemento.style.display = "block";
}

function limparErro(elemento) {
    elemento.textContent = "";
    elemento.style.display = "none";
}

function limparTodosErros() {
    limparErro(erroNome);
    limparErro(erroCpf);
    limparErro(erroEmail);
    limparErro(erroSenha);
    limparErro(erroPerfil);
}

function exibirMensagem(texto, tipo = "erro") {
    msgCadastro.textContent = texto;
    msgCadastro.style.color = tipo === "erro" ? "#e74c3c" : "#27ae60";
    msgCadastro.style.display = "block";
}

function limparMensagem() {
    msgCadastro.textContent = "";
    msgCadastro.style.display = "none";
}

// ============================================
// EVENT LISTENERS PARA VALIDAÇÃO EM TEMPO REAL
// ============================================

nomeInput.addEventListener("blur", function () {
    const validacao = validarNome(this.value);
    if (!validacao.valido) exibirErro(erroNome, validacao.msg);
    else limparErro(erroNome);
});

cpfInput.addEventListener("input", function () {
    this.value = formatarCPF(this.value);
});

cpfInput.addEventListener("blur", function () {
    const validacao = validarCPF(this.value);
    if (!validacao.valido) exibirErro(erroCpf, validacao.msg);
    else limparErro(erroCpf);
});

emailInput.addEventListener("blur", function () {
    const validacao = validarEmail(this.value);
    if (!validacao.valido) exibirErro(erroEmail, validacao.msg);
    else limparErro(erroEmail);
});

senhaInput.addEventListener("blur", function () {
    const validacao = validarSenha(this.value);
    if (!validacao.valido) exibirErro(erroSenha, validacao.msg);
    else limparErro(erroSenha);
});

perfilSelect.addEventListener("change", function () {
    const validacao = validarPerfil(this.value);
    if (!validacao.valido) exibirErro(erroPerfil, validacao.msg);
    else limparErro(erroPerfil);
});

// ============================================
// ENVIO DO FORMULÁRIO
// ============================================

form.addEventListener("submit", async function (event) {
    event.preventDefault();

    limparMensagem();
    limparTodosErros();

    const nome = nomeInput.value.trim();
    const cpf = cpfInput.value;
    const email = emailInput.value.trim().toLowerCase();
    const senha = senhaInput.value;
    const perfil = perfilSelect.value;

    let temErro = false;

    const validacaoNome = validarNome(nome);
    if (!validacaoNome.valido) { exibirErro(erroNome, validacaoNome.msg); temErro = true; }

    const validacaoCpf = validarCPF(cpf);
    if (!validacaoCpf.valido) { exibirErro(erroCpf, validacaoCpf.msg); temErro = true; }

    const validacaoEmail = validarEmail(email);
    if (!validacaoEmail.valido) { exibirErro(erroEmail, validacaoEmail.msg); temErro = true; }

    const validacaoSenha = validarSenha(senha);
    if (!validacaoSenha.valido) { exibirErro(erroSenha, validacaoSenha.msg); temErro = true; }

    const validacaoPerfil = validarPerfil(perfil);
    if (!validacaoPerfil.valido) { exibirErro(erroPerfil, validacaoPerfil.msg); temErro = true; }

    if (temErro) {
        exibirMensagem("Por favor, corrija os erros acima.", "erro");
        return;
    }

    const dados = {
        nome: nome,
        cpf: cpf.replace(/\D/g, ""),
        email: email,
        senha: senha,
        perfil: parseInt(perfil)
    };

    botaoCadastro.disabled = true;
    botaoCadastro.textContent = "Cadastrando...";

    try {
        const response = await fetch("/api/usuarios/cadastro", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(dados)
        });

        const resultado = await response.json();

        if (response.ok) {
            exibirMensagem("Cadastro realizado com sucesso! Redirecionando...", "sucesso");
            form.reset();
            limparTodosErros();
            setTimeout(() => { window.location.href = "/Login/Index"; }, 2000);
        } else {
            exibirMensagem(resultado?.message || "Erro ao cadastrar.", "erro");
        }
    } catch (error) {
        console.error("Erro:", error);
        exibirMensagem("Erro ao conectar ao servidor.", "erro");
    } finally {
        botaoCadastro.disabled = false;
        botaoCadastro.textContent = "Cadastrar";
    }
});
