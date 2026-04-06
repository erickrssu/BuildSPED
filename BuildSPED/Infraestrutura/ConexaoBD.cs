using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using BuildSPED.Services;

namespace BuildSPED.Infraestrutura
{
    public class ConexaoBD
    {
        public OleDbConnection Conectar()
        {
            BancoTXT banco = new BancoTXT();
            string[] dados = banco.Acesso();
            string caminhoConexao = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dados[0]}\{dados[1]};Jet OLEDB:Database Password={dados[2]};";

            return new OleDbConnection(caminhoConexao);
        }
    }
}
