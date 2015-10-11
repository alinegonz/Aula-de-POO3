using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for tbProdutoBLL
/// </summary>
public class tbProdutoBLL : AcessoDAL
{
    private OleDbConnection conexao;
    private OleDbCommand command;
    private OleDbDataReader drOleDb;
    private OleDbDataAdapter daOleDb;
    private DataSet dsOleDb;

    private String erro;

	public tbProdutoBLL()
	{
	}

    private String MontaQuery(tbProdutoDTO produto)
    {
        String sSQl = "SELECT idProduto, dsDescricao, dsFornecedor, " +
                      "vlValor, qtEstoque FROM tbProduto WHERE 1 = 1 ";

        if (produto.idProduto > 0)
        {
            sSQl += " AND idProduto = " + produto.idProduto;
        }
        if (produto.dsDescricao.Trim() != "")
        {
            sSQl += " AND dsDescricao LIKE '%" + produto.dsDescricao + "%'";
        }
        if (produto.dsFornecedor.Trim() != "")
        {
            sSQl += " AND dsFornecedor LIKE '%" + produto.dsFornecedor + "%'";
        }
        if (produto.vlValor > 0.0)
        {
            sSQl += " AND vlValor = " + produto.vlValor;
        }
        if (produto.qtEstoque > 0)
        {
            sSQl += " AND qtEstoque = " + produto.qtEstoque;
        }
        return sSQl;
    }
    private int MaxId()
    {
        String sSQL = "SELECT MAX(idProduto)+1  FROM tbProduto";
        int idRetorno=1;
        try
        {
            conexao = (OleDbConnection)CriaConexaoOleDb();
            drOleDb = cria_DataReader_OleDb(sSQL, conexao);
            
            while (drOleDb.Read())
            {
                if (!DBNull.Value.Equals(drOleDb[0]))
                {
                    idRetorno = (int)drOleDb[0];
                }
                else
                {
                    idRetorno = 1;
                }
                
            }
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
        return idRetorno;
    }
    public tbProdutoDTO BuscaPorId(tbProdutoDTO produto)
    {
        String sSQL = this.MontaQuery(produto);
        tbProdutoDTO prodRetorno = new tbProdutoDTO();
        try
        {
            conexao = (OleDbConnection)CriaConexaoOleDb();
            drOleDb = cria_DataReader_OleDb(sSQL,conexao);
            while (drOleDb.Read())
            {
                prodRetorno.idProduto = (int)drOleDb["idProduto"];
                if (!DBNull.Value.Equals(drOleDb["dsDescricao"]))
                {
                    prodRetorno.dsDescricao = (String)drOleDb["dsDescricao"];
                }
                if (!DBNull.Value.Equals(drOleDb["dsFornecedor"]))
                {
                    prodRetorno.dsFornecedor = (String)drOleDb["dsFornecedor"];
                }
                if (!DBNull.Value.Equals(drOleDb["qtEstoque"]))
                {
                    prodRetorno.qtEstoque = (int)drOleDb["qtEstoque"];
                }
                if (!DBNull.Value.Equals(drOleDb["vlValor"]))
                {
                    prodRetorno.vlValor = (float)drOleDb["vlValor"];
                }
            }
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
        return prodRetorno;
    }
    public ListatbProdutoDTO ListaTodos(tbProdutoDTO produto)
    {
        ListatbProdutoDTO listaRetorno = new ListatbProdutoDTO();
        String sSQL = this.MontaQuery(produto);
        try
        {
            conexao = (OleDbConnection)CriaConexaoOleDb();
            drOleDb = cria_DataReader_OleDb(sSQL, conexao);
            while (drOleDb.Read())
            {

                tbProdutoDTO prodRetorno = new tbProdutoDTO();

                prodRetorno.idProduto = (int)drOleDb["idProduto"];
                if (!DBNull.Value.Equals(drOleDb["dsDescricao"]))
                {
                    prodRetorno.dsDescricao = (String)drOleDb["dsDescricao"];
                }
                if (!DBNull.Value.Equals(drOleDb["dsFornecedor"]))
                {
                    prodRetorno.dsFornecedor = (String)drOleDb["dsFornecedor"];
                }
                if (!DBNull.Value.Equals(drOleDb["qtEstoque"]))
                {
                    prodRetorno.qtEstoque = (int)drOleDb["qtEstoque"];
                }
                if (!DBNull.Value.Equals(drOleDb["vlValor"]))
                {
                    prodRetorno.vlValor = (float)(double)drOleDb["vlValor"];
                }
                listaRetorno.Add(prodRetorno);
            }
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }

        return listaRetorno; 
    }

    public int SalvarDados(tbProdutoDTO produto)
    {
        int idProduto = this.MaxId();
        String sSQL = "";

        sSQL += "INSERT INTO tbProduto (idProduto, dsDescricao, dsFornecedor, ";
        sSQL += "vlValor, qtEstoque) VALUES ";
        sSQL += "(@idProduto, @dsDescricao, @dsFornecedor, ";
        sSQL += "@vlValor, @qtEstoque) ";

        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@idProduto", OleDbType.Integer);
        parametro.Value = idProduto;

        parametro = command.Parameters.Add("@dsDescricao", OleDbType.VarChar);
        parametro.Value = produto.dsDescricao;

        parametro = command.Parameters.Add("@dsFornecedor", OleDbType.VarChar);
        parametro.Value = produto.dsFornecedor;

        parametro = command.Parameters.Add("@vlValor", OleDbType.Double);
        parametro.Value = produto.vlValor;

        parametro = command.Parameters.Add("@qtEstoque", OleDbType.Integer);
        parametro.Value = produto.qtEstoque;

        try
        {
            drOleDb = command.ExecuteReader();
            drOleDb.Close();
            conexao.Close();
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
        return idProduto;
    }

    public void AlterarDados(tbProdutoDTO produto)
    {
        String sSQL = "";

        sSQL += "UPDATE tbProduto SET dsDescricao = @dsDescricao, dsFornecedor = @dsFornecedor, ";
        sSQL += "vlValor = @vlValor, qtEstoque = @qtEstoque WHERE idProduto = @idProduto ";
        

        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@dsDescricao", OleDbType.VarChar);
        parametro.Value = produto.dsDescricao;

        parametro = command.Parameters.Add("@dsFornecedor", OleDbType.VarChar);
        parametro.Value = produto.dsFornecedor;

        parametro = command.Parameters.Add("@vlValor", OleDbType.Double);
        parametro.Value = produto.vlValor;

        parametro = command.Parameters.Add("@qtEstoque", OleDbType.Integer);
        parametro.Value = produto.qtEstoque;

        parametro = command.Parameters.Add("@idProduto", OleDbType.Integer);
        parametro.Value = produto.idProduto;

        try
        {
            drOleDb = command.ExecuteReader();
            drOleDb.Close();
            conexao.Close();
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
    }

    public void ApagarDados(tbProdutoDTO produto)
    {
        String sSQL = "";

        sSQL += "DELETE FROM tbProduto WHERE idProduto = @idProduto ";

        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@idProduto", OleDbType.Integer);
        parametro.Value = produto.idProduto;

        try
        {
            drOleDb = command.ExecuteReader();
            drOleDb.Close();
            conexao.Close(); //Must declare the scalar variable \"@idProduto\"."
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
    }
}