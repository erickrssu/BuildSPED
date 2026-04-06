using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildSPED.Models
{
    public class Empresa
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public string IE { get; private set; }
        public int RegimeTributação { get; private set; }
        public string UF { get; private set; }
        public string CodMun { get; private set; }
        public Empresa(string codigo, string nome, string cnpj, string ie)
        {
            if (!codigo.All(char.IsDigit))
            {
                MessageBox.Show("Código deve conter apenas números");
                return;
            }
            if (!cnpj.All(char.IsDigit) || cnpj.Length != 14)
            {
                MessageBox.Show("CNPJ inválido");
                return;
            }
            if (!ie.All(char.IsDigit))
            {
                MessageBox.Show("IE inválida");
                return;
            }
            if (!Regex.IsMatch(nome, @"^[A-Za-zÀ-ÿ0-9\s\.\-&]+$"))
            {
                MessageBox.Show("Nome contém caracteres inválidos");
                return;
            }

            Codigo = codigo;
            Nome = nome;
            CNPJ = cnpj;
            IE = ie;
        }
        public bool PreencherDados(string regimeTributação, string uf, string codMun)
        {
            switch (regimeTributação)
            {
                case "Simples Nacional": RegimeTributação = 0;
                    break;
                case "Lucro Presumido": RegimeTributação = 1;
                    break;
                case "Lucro Real": RegimeTributação = 2;
                    break;
                default: return false;
            }
            if (uf == "AC" || uf == "AL" || uf == "AP" || uf == "AM" || uf == "BA" || uf == "CE" || uf == "DF" || uf == "ES" || uf == "GO" || uf == "MA" || uf == "MT" || uf == "MS" || uf == "MG" || uf == "PA"
             || uf == "PB" || uf == "PR" || uf == "PE" || uf == "PI" || uf == "RJ" || uf == "RN" || uf == "RS" || uf == "RO" || uf == "RR" || uf == "SC" || uf == "SP" || uf == "SE" || uf == "TO")
            {
                UF = uf;
            }
            else
            {
                return false;
            }
            if (codMun.All(char.IsDigit))
            {
                CodMun = codMun;
            }
            else
            {
                MessageBox.Show("Codigo de Municipio deve conter apenas números");
                return false;
            }
            return true;
        }
    }
}
