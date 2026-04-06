using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BuildSPED.Services
{

    public class BancoTXT
    {
        public string Caminho { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }

        byte[] key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
        byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");
        public void Dados(string caminho, string nome, string senha)
        {
            Caminho = Criptografia.CriptografarTexto(caminho, key, iv);
            Nome = Criptografia.CriptografarTexto(nome, key, iv);
            Senha = Criptografia.CriptografarTexto(senha, key, iv);
        }
        public void Salvar()
        {
            try
            {
                string pasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BuildSPED");
                Directory.CreateDirectory(pasta);
                string caminho = Path.Combine(pasta, "bd.txt");
                
                File.WriteAllText(caminho,$"{Caminho}\n{Nome}\n{Senha}");
                string arquivos = Path.Combine(Criptografia.DescriptografarTexto(Caminho, key, iv), "SPED");
                Directory.CreateDirectory(arquivos);
            }
            catch
            {
                MessageBox.Show("Erro ao salvar configurações");
            }
        }
        public string[] Acesso()
        {
            try
            {
                string caminho = null;
                string nome = null;
                string senha = null;

                string pasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BuildSPED");
                string dados = Path.Combine(pasta, "bd.txt");
                using (StreamReader sr = new StreamReader(dados))
                {
                    caminho = Criptografia.DescriptografarTexto(sr.ReadLine(), key, iv);
                    nome = Criptografia.DescriptografarTexto(sr.ReadLine(), key, iv);
                    senha = Criptografia.DescriptografarTexto(sr.ReadLine(), key, iv);
                }
                return [caminho, nome, senha];
            }
            catch
            {
                MessageBox.Show("Erro ao tentar acessar");
            }
            return [null];
        }
        public void CriarPasta(string codigo, string cnpj)
        {
            string[] dados = Acesso();
            string pasta = Path.Combine(dados[0], "SPED");
            pasta = Path.Combine(pasta, $"{codigo} - {cnpj}");
            Directory.CreateDirectory(pasta);
        }
    }
}
