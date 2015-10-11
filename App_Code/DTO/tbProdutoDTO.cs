using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for tbProdutoDTO
/// </summary>
public class tbProdutoDTO
{
	public tbProdutoDTO()
	{
	}

    public tbProdutoDTO(int idproduto, String dsdescricao, String dsfornecedor, float vlvalor, int qtestoque)
    {
        this.idProduto = idproduto;
        this.dsDescricao = dsdescricao;
        this.dsFornecedor = dsfornecedor;
        this.vlValor = vlvalor;
        this.qtEstoque = qtestoque;
    }

    public int idProduto { get; set; }
    public String dsDescricao { get; set; }
    public String dsFornecedor { get; set; }
    public float vlValor { get; set; }
    public int qtEstoque { get; set; }

}