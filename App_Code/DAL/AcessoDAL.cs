using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// 
/// </summary>
public class AcessoDAL
{
	public AcessoDAL()
	{
	}
    protected static IDbConnection CriaConexaoOleDb()
    {
        String sConecta = (String)System.Configuration.ConfigurationManager.AppSettings["Conexao"];
        OleDbConnection conexaoOLEDB;
        conexaoOLEDB = new OleDbConnection(sConecta);
        conexaoOLEDB.Open();
        return conexaoOLEDB;
    }

    /// <summary>
    /// kjhkdfhgkjh
    /// </summary>
    /// <param name="sSQL"></param>
    /// <param name="conecOledb"></param>
    /// <returns> Ele retorna algo de bom</returns>
    protected static OleDbDataReader cria_DataReader_OleDb(String sSQL, OleDbConnection conecOledb)
    {
        OleDbCommand comando = new OleDbCommand(sSQL, conecOledb);
        OleDbDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
        comando.Dispose();
        return dr;
    }

    protected static OleDbDataReader cria_DataReader_OleDb(String sSQL)
    {
        String sConecta = (String)System.Configuration.ConfigurationManager.AppSettings["Conexao"];
        OleDbConnection conexaoOLEDB;
        conexaoOLEDB = new OleDbConnection(sConecta);
        conexaoOLEDB.Open();
        OleDbCommand comando = new OleDbCommand(sSQL, conexaoOLEDB);
        OleDbDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
        comando.Dispose();
        return dr;
    }

    protected static OleDbDataAdapter cria_DataAdapter_OleDb(String sSQL, OleDbConnection conecOledb)
    {
        OleDbDataAdapter da = new OleDbDataAdapter(sSQL, conecOledb);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return da;
    }

    protected static DataSet cria_DataSet_OleDb(String sSQL, OleDbConnection conecOledb)
    {
        OleDbDataAdapter da = new OleDbDataAdapter(sSQL, conecOledb);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    protected static DataSet cria_DataSet_OleDb(OleDbDataAdapter da)
    {
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    protected static OleDbCommand cria_Command_OleDb(String sSQL, OleDbConnection conecOledb)
    {
        OleDbCommand comando = conecOledb.CreateCommand();
        comando.CommandText = sSQL;
        comando.CommandType = CommandType.Text;
        return comando;
    }

    /// <summary>
    /// Isso faz coisas lindas e boas para nossa vida.
    /// </summary>
    /// <param name="sSQL"></param>
    /// <param name="conecOledb"></param>
    /// <param name="TipoComando"></param>
    /// <returns></returns>
    protected static OleDbCommand cria_Command_OleDb(String sSQL, OleDbConnection conecOledb, CommandType TipoComando)
    {
        OleDbCommand comando = conecOledb.CreateCommand();
        comando.CommandText = sSQL;
        comando.CommandType = TipoComando;
        return comando;
    }
}