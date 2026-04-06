using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuildSPED.Services;
using BuildSPED.Models;
using BuildSPED.Infraestrutura;

namespace BuildSPED
{
    public partial class principal : Form
    {
        public principal()
        {
            InitializeComponent();
        }

        public class Parametros
        {
            public string Action { get; set; }
            public string[] Dados { get; set; }
        }
        public async void WebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string webMessageAsJson = e.WebMessageAsJson;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Parametros mensagem = JsonSerializer.Deserialize<Parametros>(webMessageAsJson, options);
            if (mensagem != null)
            {
                switch (mensagem.Action)
                {
                    case "salvar_config":
                        BancoTXT banco = new BancoTXT();
                        banco.Dados(mensagem.Dados[0], mensagem.Dados[1], mensagem.Dados[2]);
                        banco.Salvar();
                        break;
                    case "salvar_empresa":
                        Empresa empresa = new Empresa(mensagem.Dados[0], mensagem.Dados[1], mensagem.Dados[2], mensagem.Dados[3]);
                        bool teste = empresa.PreencherDados(mensagem.Dados[4], mensagem.Dados[5], mensagem.Dados[6]);
                        if(teste == true)
                        {
                            if(EmpresaRepository.InserirEmpresa(empresa) == true)
                            {
                                EmpresaRepository.ExibirEmpresas(exibir);
                            }
                        }
                        break;
                    case "carregar_empresas":
                        EmpresaRepository.ExibirEmpresas(exibir);
                        break;
                }
            }
        }
        private async void principal_Load(object sender, EventArgs e)
        {
            try
            {
                
                string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BuildSPED");
                CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
                await exibir.EnsureCoreWebView2Async(environment);
                exibir.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
                exibir.CoreWebView2.WebMessageReceived += WebView_WebMessageReceived;
                exibir.CoreWebView2.WebResourceRequested += delegate (object? s, CoreWebView2WebResourceRequestedEventArgs args)
                {
                    string text = new Uri(args.Request.Uri).AbsolutePath.TrimStart('/');
                    string name = "BuildSPED." + text.Replace("/", ".");
                    Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
                    if (manifestResourceStream != null)
                    {
                        string text2 = (text.EndsWith(".html") ? "text/html" : (text.EndsWith(".css") ? "text/css" : (text.EndsWith(".js") ? "application/javascript" : "text/plain")));
                        args.Response = exibir.CoreWebView2.Environment.CreateWebResourceResponse(manifestResourceStream, 200, "OK", "Content-Type: " + text2);
                    }
                };
                exibir.CoreWebView2.Navigate("https://app/index.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na inicialização do WebView2: " + ex.Message + "\n\nVerifique se o WebView2 Runtime está instalado.", "Erro Crítico");
            }
        }
    }
}
