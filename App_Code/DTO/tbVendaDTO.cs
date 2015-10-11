using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for tbVendaDTO
/// </summary>
public class tbVendaDTO
{
	public tbVendaDTO()
	{
	}

    public tbVendaDTO(int idvenda, tbProdutoDTO produto, tbVendedorDTO vendedor, int qtquantidade)
    {
        this.idVenda = idvenda;
        this.Produto = produto;
        this.Vendedor = vendedor;
        this.qtQuantidade = qtquantidade;
    }

    public int idVenda { get; set; }
    public tbProdutoDTO Produto { get; set; }
    public tbVendedorDTO Vendedor { get; set; }
    public int qtQuantidade { get; set; }

}