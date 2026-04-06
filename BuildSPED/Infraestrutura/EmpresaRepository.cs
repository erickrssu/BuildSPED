using BuildSPED.Infraestrutura;
using BuildSPED.Models;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSPED.Services;

namespace BuildSPED.Infraestrutura
{
    public static class EmpresaRepository
    {
        public static bool InserirEmpresa(Empresa empresa)
        {
            try
            {
                ConexaoBD conexao = new ConexaoBD();

                using (var conn = conexao.Conectar())
                {
                    conn.Open();
                    string queryVerificar = @"SELECT COUNT(*) FROM empresas WHERE codigo = ? OR razao_social = ? OR cnpj = ? OR ie = ?";

                    using (var cmd = new OleDbCommand(queryVerificar, conn))
                    {
                        cmd.Parameters.AddWithValue("@Codigo", empresa.Codigo);
                        cmd.Parameters.AddWithValue("@Razao", empresa.Nome);
                        cmd.Parameters.AddWithValue("@CNPJ", empresa.CNPJ);
                        cmd.Parameters.AddWithValue("@IE", empresa.IE);

                        int existe = (int)cmd.ExecuteScalar();

                        if (existe == 0)
                        {
                            string queryInsert = @"INSERT INTO empresas (codigo, razao_social, cnpj, ie, regime_tributario, uf, cod_mun)VALUES (?, ?, ?, ?, ?, ?, ?)";

                            using (var insertCmd = new OleDbCommand(queryInsert, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@Codigo", empresa.Codigo);
                                insertCmd.Parameters.AddWithValue("@Razao", empresa.Nome);
                                insertCmd.Parameters.AddWithValue("@CNPJ", empresa.CNPJ);
                                insertCmd.Parameters.AddWithValue("@IE", empresa.IE);
                                insertCmd.Parameters.AddWithValue("@regime", empresa.RegimeTributação);
                                insertCmd.Parameters.AddWithValue("@UF", empresa.UF);
                                insertCmd.Parameters.AddWithValue("@Mun", empresa.CodMun);

                                insertCmd.ExecuteNonQuery();
                            }
                            BancoTXT banco = new BancoTXT();
                            banco.CriarPasta(empresa.Codigo, empresa.CNPJ);
                            
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Empresa já cadastrada com algum desses dados");
                            return false;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao se conectar no banco de dados");
                return false;
            }
            
        }
        public static async Task ExibirEmpresas(WebView2 exibir)
        {
            ConexaoBD conexao = new ConexaoBD();

            using (var conn = conexao.Conectar())
            {
                conn.Open();
                string sql = "SELECT * FROM empresas";

                using (OleDbDataAdapter da = new OleDbDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        await exibir.CoreWebView2.ExecuteScriptAsync(
                            $"ReceberEmpresa('{row[0]}','{row[1]}','{row[2]}','{row[3]}','{row[4]}','{row[5]}','{row[6]}')"
                        );
                    }
                }
            }
        }
    }
}
