using BuildSPED.Infraestrutura;
using BuildSPED.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSPED.Models
{
    public class Sped
    {
        private static string RazaoSocial, Cnpj, Ie, Periodo, DataImportacao, Status, Responsavel, Caminho, CodMun, Uf, Nome = null;
        public static async Task OpenSped()
        {
            string arquivo = null;
            var t = new Thread(() =>
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "TXT (*.txt)|*.txt";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    arquivo = dlg.FileName;
                }
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            while (t.IsAlive)
            {
                await Task.Delay(100);
            }

            if (!string.IsNullOrEmpty(arquivo))
            {
                LerSped(arquivo);
            }
        }
        public static async Task LerSped(string arquivo)
        {
            string[] linhas = File.ReadAllLines(arquivo);
            foreach(string linha in linhas)
            {
                string[] campos = linha.Split("|");
                if (campos[1] == "0000")
                {
                    if (await PreencherDadosContribuinte(campos) == true)
                    {
                        if(await EmpresaRepository.VerificarExiste(Nome, Cnpj, Ie, Uf, CodMun) == true)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Empresa não identificada");
                        }
                    }
                        return;
                }
            }
        }
        public static async Task<bool> PreencherDadosContribuinte(string[] campos)
        {
            BancoTXT banco = new BancoTXT();
            RazaoSocial = campos[6].Trim();
            Cnpj = campos[7].Trim();
            Ie = campos[10].Trim();
            if (campos[4].Trim().Substring(2, 4) != campos[5].Trim().Substring(2, 4))
            {
                MessageBox.Show("Período inicial difere do final");
                return false ;
            }
            Periodo = campos[5].Trim().Substring(2, 4);
            DataImportacao = DateTime.Now.ToString("MM-yyyy");
            CodMun = campos[11].Trim();
            string arquivo = Path.Combine(banco.Acesso()[0], "SPED");
            Caminho = Path.Combine(arquivo, Cnpj);
            Uf = campos[9];
            if (RazaoSocial == "" || Cnpj == "" || Ie == "" || Periodo == "" || DataImportacao == "" || CodMun == "" || Uf == "" || Caminho == "")
            {
                return false;
            }
            return true;
        }
    }
}
