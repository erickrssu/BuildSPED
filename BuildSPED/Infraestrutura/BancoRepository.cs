using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSPED.Infraestrutura
{
    public class BancoRepository
    {
        public OleDbConnection Conectar()
        {
            string caminhoConexao = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SeuCaminho\BancoDados.accdb";

            return new OleDbConnection(caminhoConexao);
        }
    }
}
