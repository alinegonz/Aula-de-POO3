using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProdutoListagem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected tbProdutoDTO FiltroTela()
    {
        tbProdutoDTO prodRet = new tbProdutoDTO();
        prodRet.dsDescricao = txtDescricao.Text;
        prodRet.dsFornecedor = txtFabricante.Text;
        prodRet.qtEstoque = int.Parse(txtqtEstoque.Text);
        prodRet.vlValor = float.Parse(txtPreco.Text);
        return prodRet;
    }

    protected void carregarGrid(tbProdutoDTO produtoTela)
    {
        tbProdutoBLL prodBll = new tbProdutoBLL();
        grdProduto.DataSource = prodBll.ListaTodos(produtoTela);
        grdProduto.DataBind();
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        carregarGrid(FiltroTela());
    }
    protected void grdProduto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProduto.PageIndex = e.NewPageIndex;
        carregarGrid(FiltroTela());
    }
    protected void grdProduto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmuoseover"] = "this.style.backgroundColor='aquamarine';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
            e.Row.ToolTip = "Clique em select para selecionar a linha";
        }
    }
    protected void grdProduto_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = grdProduto.SelectedIndex;
        lblid.Text = grdProduto.DataKeys[index].Value.ToString();
    }
    protected void btnDeletar_Click(object sender, EventArgs e)
    {
        tbProdutoDTO prodRet = new tbProdutoDTO();
        prodRet.idProduto = int.Parse(lblid.Text);
        tbProdutoBLL prodBll = new tbProdutoBLL();
        prodBll.ApagarDados(prodRet);
        carregarGrid(FiltroTela());
    }
}