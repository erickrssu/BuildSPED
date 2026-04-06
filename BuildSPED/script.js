const botoes = document.querySelectorAll('.menu button');
const secoes = document.querySelectorAll('.view-section');
botoes.forEach(botao => {
    botao.addEventListener('click', () => {
        botoes.forEach(b => b.classList.remove('active'));
        secoes.forEach(s => s.classList.remove('active'));
        botao.classList.add('active');
        const idSecao = botao.id.replace('btn-', 'view-');
        const secaoAlvo = document.getElementById(idSecao);
        if (secaoAlvo) {
            secaoAlvo.classList.add('active');
        } else {
            console.warn(`A seção com ID ${idSecao} ainda não foi criada no HTML.`);
        }
    });
});
document.addEventListener('DOMContentLoaded', () => {
    const salvar = document.getElementById('salvar_config');
    salvar.addEventListener('click', function () {
        const caminho = document.getElementById('caminho_banco').value;
        const nome = document.getElementById('nome_banco').value;
        const senha = document.getElementById('senha_banco').value;
        if (salvar) {
            window.chrome.webview.postMessage({
                action: "salvar_config",
                dados: [caminho, nome, senha]
            });
        }
    })
});
document.addEventListener('DOMContentLoaded', () => {
    const salvar = document.getElementById('salvar_empresa');
    salvar.addEventListener('click', function () {
        const codigo = document.getElementById('codigo_empresa').value;
        const nome = document.getElementById('nome_empresa').value;
        const cnpj = document.getElementById('cnpj_empresa').value;
        const ie = document.getElementById('ie_empresa').value;
        const regime = document.getElementById('regime_empresa').value;
        const uf = document.getElementById('uf_empresa').value;
        const municipio = document.getElementById('mun_empresa').value;
        if (salvar) {
            const tabela = document.querySelector('#tabela_empresas tbody');
            tabela.innerHTML = '';
            window.chrome.webview.postMessage({
                action: "salvar_empresa",
                dados: [codigo, nome, cnpj, ie, regime, uf, municipio]
            });
        }
    });
});
document.addEventListener('DOMContentLoaded', () => {
    const empresas = document.getElementById('btn-cadastrar-empresa');
    empresas.addEventListener('click', function(){
        const tabela = document.querySelector('#tabela_empresas tbody');
        tabela.innerHTML = '';
        window.chrome.webview.postMessage({
            action: "carregar_empresas"
        });
    });
});
function ReceberEmpresa(codigo, nome, cnpj, ie, regime, uf, municipio) {
    const tabela = document.querySelector('#tabela_empresas tbody');
    const linha = tabela.insertRow();
    if(regime === '0') {
        regime = 'Simples Nacional';
    }else if(regime === '1') {
        regime = 'Lucro Presumido';
    }else if(regime === '2') {
        regime = 'Lucro Real';
    }

    linha.insertCell(0).textContent = codigo;
    linha.insertCell(1).textContent = nome;
    linha.insertCell(2).textContent = cnpj;
    linha.insertCell(3).textContent = ie;
    linha.insertCell(4).textContent = regime;
    linha.insertCell(5).textContent = uf;
    linha.insertCell(6).textContent = municipio;
}