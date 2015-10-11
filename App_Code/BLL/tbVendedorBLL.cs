using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using System.Data.OleDb;

public class tbVendedorBLL : AcessoDAL
{
    private OleDbConnection conexao;
    private OleDbCommand command;
    private OleDbDataReader drOleDb;
    private OleDbDataAdapter daOleDb;
    private DataSet dsOleDb;

    private String erro;

	public tbVendedorBLL()
	{
	}
    private String MontaQuery(tbVendedorDTO vendedor)
    {
        String sSQL = "SET LANGUAGE PORTUGUESE; SET DATEFORMAT dmy; ";
        sSQL += "SELECT idVendedor, dsNome, dtNascimento FROM tbVendedor WHERE 1 = 1 ";

        if (vendedor.idVendedor > 0)
        {
            sSQL += " AND idVendedor = " + vendedor.idVendedor;
        }
        if (vendedor.dsNome.Trim() != "")
        {
            sSQL += " AND dsNome LIKE '%" + vendedor.dsNome.Trim() + "%'";
        }
        if (vendedor.dtNascimento != null)
        {
            sSQL += " AND dtNascimento = '" + vendedor.dtNascimento + "'";
        }
        return sSQL;
    }

    private int MaxId()
    {
        String sSQL = "SELECT MAX(idVendedor)+1  FROM tbVendedor";
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

    public tbVendedorDTO BuscaPorId(tbVendedorDTO vendedor)
    {
        String sSQL = this.MontaQuery(vendedor);
        tbVendedorDTO retorno = new tbVendedorDTO();
        try
        {
            conexao = (OleDbConnection)CriaConexaoOleDb();
            drOleDb = cria_DataReader_OleDb(sSQL, conexao);
            while (drOleDb.Read())
            {
                retorno.idVendedor = (int)drOleDb["idVendedor"];
                if (!DBNull.Value.Equals(drOleDb["dsNome"]))
                {
                    retorno.dsNome = (String)drOleDb["dsNome"];
                }
                if (!DBNull.Value.Equals(drOleDb["dtNascimento"]))
                {
                    retorno.dtNascimento = (DateTime)drOleDb["dtNascimento"];
                }
            }
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }
        return retorno;
    }

    public ListatbVendedorDTO ListaTodos(tbVendedorDTO vendedor)
    {
        ListatbVendedorDTO listaRetorno = new ListatbVendedorDTO();
        String sSQL = this.MontaQuery(vendedor);
        try
        {
            conexao = (OleDbConnection)CriaConexaoOleDb();
            drOleDb = cria_DataReader_OleDb(sSQL, conexao);
            while (drOleDb.Read())
            {

                tbVendedorDTO Retorno = new tbVendedorDTO();

                Retorno.idVendedor = (int)drOleDb["idVendedor"];
                if (!DBNull.Value.Equals(drOleDb["dsNome"]))
                {
                    Retorno.dsNome = (String)drOleDb["dsNome"];
                }
                if (!DBNull.Value.Equals(drOleDb["dtNascimento"]))
                {
                    Retorno.dtNascimento = (DateTime)drOleDb["dtNascimento"];
                }
                
                listaRetorno.Add(Retorno);
            }
        }
        catch (SystemException e)
        {
            erro = e.Message;
        }

        return listaRetorno;
    }

    public int SalvarDados(tbVendedorDTO vendedor)
    {
        int idVendedor = this.MaxId();
        String sSQL = "";

        sSQL += "INSERT INTO tbVendedor (idVendedor, dsNome, dtNascimento) VALUES ";
        sSQL += "(@idVendedor, @dsNome, @dtNascimento) ";

        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@idVendedor", OleDbType.Integer);
        parametro.Value = idVendedor;

        parametro = command.Parameters.Add("@dsNome", OleDbType.VarChar);
        parametro.Value = vendedor.dsNome;

        parametro = command.Parameters.Add("@dtNascimento", OleDbType.Date);
        parametro.Value = vendedor.dtNascimento;

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
        return idVendedor;
    }

    public void AlterarDados(tbVendedorDTO vendedor)
    {
        String sSQL = "";

        sSQL += "UPDATE tbVendedor SET dsNome = @dsNome, dtNascimento = @dtNascimento WHERE idVendedor = @idVendedor ";


        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@dsNome", OleDbType.VarChar);
        parametro.Value = vendedor.dsNome;

        parametro = command.Parameters.Add("@dtNascimento", OleDbType.Date);
        parametro.Value = vendedor.dtNascimento;

        parametro = command.Parameters.Add("@idVendedor", OleDbType.Integer);
        parametro.Value = vendedor.idVendedor;

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

    public void ApagarDados(tbVendedorDTO vendedor)
    {
        String sSQL = "";

        sSQL += "DETELE FROM tbVendedor WHERE idVendedor = @idVendedor ";

        conexao = (OleDbConnection)CriaConexaoOleDb();
        command = new OleDbCommand(sSQL, conexao);
        command.CommandType = CommandType.Text;

        OleDbParameter parametro = command.Parameters.Add("@idVendedor", OleDbType.Integer);
        parametro.Value = vendedor.idVendedor;

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