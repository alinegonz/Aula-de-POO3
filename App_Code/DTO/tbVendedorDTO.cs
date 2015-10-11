using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for tbVendedorDTO
/// </summary>
public class tbVendedorDTO
{
    public tbVendedorDTO()
	{
	}

    public tbVendedorDTO(int idvendedor, String dsnome, DateTime dtnascimento)
    {
        this.idVendedor = idvendedor;
        this.dsNome = dsnome;
        this.dtNascimento = dtnascimento;
    }

    public int idVendedor  { get; set; }
    public String dsNome  { get; set; }
    public DateTime dtNascimento { get; set; }
}