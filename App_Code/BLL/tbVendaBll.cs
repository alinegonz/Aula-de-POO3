using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.OleDb;

public class tbVendaBll : AcessoDAL
{
    private OleDbConnection conexao;
    private OleDbCommand command;
    private OleDbDataReader drOleDb;
    private OleDbDataAdapter daOleDb;
    private DataSet dsOleDb;

    private String erro;

    //Aqui é o The JAMPE OFI KETI
    private tbVendedorBLL vendedorBLL = new tbVendedorBLL();
    private tbProdutoBLL produtoBLL = new tbProdutoBLL();


	public tbVendaBll()
	{
	}

    private String MontaQuery(tbVendaDTO venda)
    {
        String sSQL = "SELECT idVenda, idProduto, idVendedor, qtQuantidade FROM tbVenda WHERE 1=1 ";

        if (venda.idVenda > 0)
        {
            sSQL += " AND idVenda = " + venda.idVenda;
        }
        if (venda.Produto.idProduto > 0)
        {
            sSQL += " AND idProduto = " + venda.Produto.idProduto;
        }

        if (venda.Vendedor.idVendedor > 0)
        {
            sSQL += " AND idVendedor = " + venda.Vendedor.idVendedor;
        }
        if (venda.qtQuantidade > 0)
        {
            sSQL += " AND qtQuantidade = " + venda.qtQuantidade;
        }
        return sSQL;
    }

    private int MaxId()
    {
        String sSQL = "SELECT MAX(idVenda)+1  FROM tbVenda";
        int idRetorno = 1;
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
    public tbVendaDTO BuscaPorId(tbVendaDTO venda)
    {
        String sSQL = this.MontaQuery(venda);
        tbVendaDTO retorno = new tbVendaDTO();
        try
        {
            conexao = (OleDbConnection)CriaConexaoOleDb();
            drOleDb = cria_DataReader_OleDb(sSQL, conexao);
            while (drOleDb.Read())
            {
                retorno.idVenda = (int)drOleDb["idVenda"];

                tbProdutoDTO prodAux = new tbProdutoDTO();
                if (!DBNull.Value.Equals(drOleDb["idProduto"]))
                {
                    prodAux.idProduto = (int)drOleDb["idProduto"];
                    retorno.Produto = produtoBLL.BuscaPorId(prodAux);
                }
                tbVendedorDTO vendAux = new tbVendedorDTO();
                if (!DBNull.Value.Equals(drOleDb["idVendedor"]))
                {
                    vendAux.idVendedor = (int)drOleDb["idVendedor"];
                    retorno.Vendedor = vendedorBLL.BuscaPorId(vendAux);
                }
                if (!DBNull.Value.Equals(drOleDb["qtQuantidade"]))
                {
                    retorno.qtQuantidade = (int)drOleDb["qtQuantidade"];
                }
            }
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
        return retorno;
    }

    public ListatbVendaDTO ListaTodos(tbVendaDTO venda)
    {
        ListatbVendaDTO listaRetorno = new ListatbVendaDTO();
        String sSQL = this.MontaQuery(venda);
        try
        {
            conexao = (OleDbConnection)CriaConexaoOleDb();
            drOleDb = cria_DataReader_OleDb(sSQL, conexao);
            while (drOleDb.Read())
            {

                tbVendaDTO retorno = new tbVendaDTO();

                retorno.idVenda = (int)drOleDb["idVenda"];

                tbProdutoDTO prodAux = new tbProdutoDTO();
                if (!DBNull.Value.Equals(drOleDb["idProduto"]))
                {
                    prodAux.idProduto = (int)drOleDb["idProduto"];
                    retorno.Produto = produtoBLL.BuscaPorId(prodAux);
                }
                tbVendedorDTO vendAux = new tbVendedorDTO();
                if (!DBNull.Value.Equals(drOleDb["idVendedor"]))
                {
                    vendAux.idVendedor = (int)drOleDb["idVendedor"];
                    retorno.Vendedor = vendedorBLL.BuscaPorId(vendAux);
                }
                if (!DBNull.Value.Equals(drOleDb["qtQuantidade"]))
                {
                    retorno.qtQuantidade = (int)drOleDb["qtQuantidade"];
                }

                listaRetorno.Add(retorno);
            }
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }

        return listaRetorno;
    }

    public int SalvarDados(tbVendaDTO venda)
    {
        int idVenda = this.MaxId();
        String sSQL = "";

        sSQL += "INSERT INTO tbVenda (idVenda, idProduto, idVendedor, qtQuantidade) VALUES ";
        sSQL += "(@idVenda, @idProduto, @idVendedor, @qtQuantidade) ";

        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@idVenda", OleDbType.Integer);
        parametro.Value = idVenda;

        parametro = command.Parameters.Add("@idProduto", OleDbType.Integer);
        parametro.Value = venda.Produto.idProduto;

        parametro = command.Parameters.Add("@idVendedor", OleDbType.Integer);
        parametro.Value = venda.Vendedor.idVendedor;

        parametro = command.Parameters.Add("@qtQuantidade", OleDbType.Integer);
        parametro.Value = venda.qtQuantidade;

        try
        {
            drOleDb = command.ExecuteReader();
            drOleDb.Close();
            conexao.Close();
            TriggerEstoque(venda);
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
        return idVenda;
    }

    //private void EventoDaVIew()
    //{
    //    // No Controller

    //    tbVendaDTO minhaVenda = new tbVendaDTO();
    //    minhaVenda.Produto.idProduto = 1; //Pega de uma combo
    //    minhaVenda.Vendedor.idVendedor = 76; // Pega de uma Combo
    //    minhaVenda.qtQuantidade = 3;// Pega da caixa de texto

    //    tbVendaBll vendaAux = new tbVendaBll();
    //    vendaAux.SalvarDados(minhaVenda);
    //    tbProdutoDTO prodAux = new tbProdutoDTO();
    //    tbProdutoBLL prodBLL = new tbProdutoBLL();
    //    prodAux = prodBLL.BuscaPorId(minhaVenda.Produto);
    //    prodAux.qtEstoque = prodAux.qtEstoque - minhaVenda.Produto.qtEstoque;
    //    prodBLL.AlterarDados(prodAux);
    //}

    private void TriggerEstoque(tbVendaDTO venda)
    {
        String sSQL = "";

        sSQL += "UPDATE tbVenda SET qtQuantidade = qtQuantidade - @qtQuantidade WHERE idVenda = @idVenda ";


        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@qtQuantidade", OleDbType.Integer);
        parametro.Value = venda.qtQuantidade;

        parametro = command.Parameters.Add("@idVenda", OleDbType.Integer);
        parametro.Value = venda.idVenda;

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

    public void AlterarDados(tbVendaDTO venda)
    {
        String sSQL = "";

        sSQL += "UPDATE tbVenda SET idProduto = @idProduto, idVendedor = @idVendedor, qtQuantidade = @qtQuantidade WHERE idVenda = @idVenda ";


        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@idProduto", OleDbType.Integer);
        parametro.Value = venda.Produto.idProduto;

        parametro = command.Parameters.Add("@idVendedor", OleDbType.Integer);
        parametro.Value = venda.Vendedor.idVendedor;

        parametro = command.Parameters.Add("@qtQuantidade", OleDbType.Integer);
        parametro.Value = venda.qtQuantidade;

        parametro = command.Parameters.Add("@idVenda", OleDbType.Integer);
        parametro.Value = venda.idVenda;

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

    public void ApagarDados(tbVendaDTO venda)
    {
        String sSQL = "";

        sSQL += "DELETE FROM tbVenda WHERE idVenda = @idVenda";

        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@idVenda", OleDbType.Integer);
        parametro.Value = venda.idVenda;

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
}