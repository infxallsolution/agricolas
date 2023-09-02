<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaPrecios.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxCobrar.ListaPrecios" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <form id="form1" runat="server">
        <div class="container-fluid">
            <asp:Panel ID="formContainer" ClientIDMode="Static" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td class="bordesBusqueda"></td>
                        <td style="width: 150px; text-align:left">Busqueda</td>
                        <td style="width: 400px; text-align:left">
                            <asp:TextBox ID="nitxtBusqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                        <td class="bordesBusqueda"></td>
                    </tr>

                </table>
                <hr />
                <div class="text-center">
                    <asp:Button ID="niimbBuscar" CssClass="botones" runat="server" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click" />
                    <asp:Button ID="nilbNuevo" CssClass="botones" runat="server" Text="Nuevo" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click" />
                    <asp:Button ID="lbCancelar" CssClass="botones" runat="server" Text="Cancelar" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="lbCancelar_Click"  />
                    <asp:Button ID="lbRegistrar" CssClass="botones" runat="server" Text="Guardar" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbRegistrar_Click"  OnClientClick="return confirmSwal(this,'Guardado','Desea insertar el registro ?')" />
                </div>

                <table style="width: 100%;">
                    <tr>
                        <td></td>
                        <td style="width: 150px; text-align:left">
                            <asp:Label ID="Label11" runat="server" Text="Año" ></asp:Label>

                        </td>
                        <td style="width: 400PX; text-align: left">
                            <table class="w-100">
                                <tr>
                                    <td style="text-align: left; width: 120px">
                                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select-deselect" Width="100px"  OnSelectedIndexChanged="ddlAño_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>

                                    </td>
                                    <td style="text-align: left; width: 50px">
                                        <asp:Label ID="Label13" runat="server" Text="Mes" ></asp:Label>

                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="chzn-select-deselect" Width="100%"  >
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 150px; text-align:left">
                            <asp:Label ID="Label12" runat="server" Text="Calse cliente" ></asp:Label>

                        </td>
                        <td style="width: 400PX; text-align: left">
                            <asp:DropDownList ID="ddlClaseCliente" runat="server" CssClass="chzn-select-deselect" Width="100%"  OnSelectedIndexChanged="ddlClaseCliente_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>

                        </td>
                        <td></td>
                    </tr>

                </table>

                <div class="tablaGrilla">
                    <asp:Label ID="nilblInformacion" runat="server"></asp:Label>

                    <asp:GridView ID="gvNovedades" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" >
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:BoundField DataField="item" HeaderText="Item" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombreItem" HeaderText="NombreItem">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="($)ValorUnitario">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPrecioTerceros" runat="server" CssClass="input" onkeypress="formato_numero(this)" Text='<%# Eval("PrecioDestajo") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="200px" HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="footer" />
                    </asp:GridView>
                </div>

            </asp:Panel>
            <hr />
            <asp:GridView ID="gvLista" runat="server" PageSize="20" AllowPaging="True" Width="100%" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False">
                <AlternatingRowStyle CssClass="alt" />
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="imEdit" CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt" CommandName="Select" ToolTip="Edita el registro seleccionado"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" CssClass="action-item" />
                        <HeaderStyle CssClass="action-item" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash" CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                        <HeaderStyle CssClass="action-item" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="año" HeaderText="Año">
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="mes" HeaderText="Mes">
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="clase" HeaderText="Clase">
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombreClase" HeaderText="NombreClase" />
                </Columns>
                <HeaderStyle CssClass="thead" />
                <PagerStyle CssClass="footer" />
            </asp:GridView>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="CustomStyles">
    <style type="text/css">
        .auto-style1 {
            border: 1px solid #DFDFDF;
            border-radius: 3px;
            padding: 3px;
            color: #666666;
            box-shadow: 0px 2px 2px #C7C7C7;
            width: 130px;
            margin: 3px;
            font-size: 12px;
            text-decoration: none;
        }
    </style>
</asp:Content>
