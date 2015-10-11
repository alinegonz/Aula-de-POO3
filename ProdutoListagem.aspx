<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProdutoListagem.aspx.cs" Inherits="ProdutoListagem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Grade">
        <asp:Label ID="lblid" runat="server" ></asp:Label>
        <div class="divTable">
             
            <div class="divRow">
                <div class="divCell">
                    <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label>
                </div>
                <div class="divCell">
                    <asp:TextBox ID="txtDescricao" runat="server" Height="16px" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="divRow">
                <div class="divCell">
                    <asp:Label ID="lblFabricante" runat="server" Text="Fabricante"></asp:Label>
                </div>
                <div class="divCell">
                    <asp:TextBox ID="txtFabricante" runat="server" Height="16px" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="divRow">
                <div class="divCell">
                    <asp:Label ID="lblEstoque" runat="server" Text="Qtd. Estoque"></asp:Label>
                </div>
                <div class="divCell">
                    <asp:TextBox ID="txtqtEstoque" runat="server" Height="16px" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="divRow">
                <div class="divCell">
                    <asp:Label ID="lblPreco" runat="server" Text="Preço"></asp:Label>
                </div>
                <div class="divCell">
                    <asp:TextBox ID="txtPreco" runat="server" Height="16px" Width="100%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div align="center">
            <asp:Button ID="btnFiltrar" runat="server" Text="Filtar" Width="49%" 
                onclick="btnFiltrar_Click"/>
            <asp:Button ID="btnInserir" runat="server" Text="Inserir novo Produto" Width="50%" />
        </div>
        <asp:GridView ID="grdProduto" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" AutoGenerateSelectButton="True" 
            DataKeyNames="idProduto" Height="156px" PageSize="5" Width="100%"
            EmptyDataText="Sem registros....." 
            onpageindexchanging="grdProduto_PageIndexChanging" 
            onrowdatabound="grdProduto_RowDataBound" 
            onselectedindexchanged="grdProduto_SelectedIndexChanged" >
            <Columns>
                <asp:BoundField DataField="idProduto" HeaderText="CÓDIGO" InsertVisible="False" ReadOnly="True" SortExpression="idProduto" />
                <asp:BoundField DataField="dsDescricao" HeaderText="DESCRIÇÃO" SortExpression="dsDescricao" />
                <asp:BoundField DataField="dsFornecedor" HeaderText="FORNECEDOR" SortExpression="dsFornecedor" />
                <asp:BoundField DataField="vlValor" HeaderText="VALOR" SortExpression="vlValor" />
                <asp:BoundField DataField="qtEstoque" HeaderText="ESTOQUE" SortExpression="QuantityPerUnit" />
            </Columns>
            <selectedrowstyle backcolor="LightCyan" forecolor="DarkBlue" font-bold="true"/>  
        </asp:GridView>
        <br />
        <div align="center">
            <asp:Button ID="btnVisualizar" runat="server" Text="Visualizar" Width="25%" />
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" width="25%" />
            <asp:Button ID="btnDeletar" runat="server" Text="Deletar" width="24%" 
                onclick="btnDeletar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" width="24%" 
                onclick="btnVoltar_Click" />
        </div>
    </div>
</asp:Content>

