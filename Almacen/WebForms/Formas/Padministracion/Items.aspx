<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="Almacen.WebForms.Formas.Padministracion.Items" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />

            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label2" runat="server" Text="Código" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 380px">
                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" Visible="False" Width="45%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 120px">
                         <asp:Label ID="Label15" runat="server" Text="Cod. Equivalencia" Visible="False"></asp:Label>
                        </td>
                    <td style="text-align: left; width: 400px">
                         <asp:TextBox ID="txtEquivalencia" runat="server" CssClass="input" MaxLength="50" Visible="False" Width="50%"></asp:TextBox>
                        </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label3" runat="server" Text="Descripción" Visible="False" Width="100%"></asp:Label>
                    </td>
                    <td style="text-align: left; " colspan="3">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input mayuscula" MaxLength="550" Visible="False" Width="97%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label5" runat="server" Text="Descrip. corta" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 380px">
                        <asp:TextBox ID="txtDesCorta" runat="server" CssClass="input mayuscula" MaxLength="50" Visible="False" Width="95%"></asp:TextBox>

                    </td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label7" runat="server" Text="Referencia" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtReferencia" runat="server" CssClass="input" MaxLength="550" Visible="False" Width="97%"></asp:TextBox>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label9" runat="server" Text="Unidad de medida" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 380px">
                        <asp:DropDownList ID="ddlUmedidaCompra" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label6" runat="server" Text="Tipo de item" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoItem" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                            <asp:ListItem Selected="True" Value="I">Inventario</asp:ListItem>
                            <asp:ListItem Value="S">Servicio</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label11" runat="server" Text="Stock min - max" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 380px">
                        <asp:TextBox ID="txvMinimo" runat="server" CssClass="input" MaxLength="2" placeholder="Mínimo" onkeyup="formato_numero(this)" Visible="False" Width="40%"></asp:TextBox>
                        <asp:TextBox ID="txvMaximo" runat="server" CssClass="input" MaxLength="2" placeholder="Máximo" onkeyup="formato_numero(this)" Visible="False" Width="40%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="lblTipoInventario" runat="server" Text="Tipo de inventario" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                         <asp:DropDownList ID="ddlTipoInventario" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label10" runat="server" Text="Reposición(Días)" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 380px">
                        <asp:TextBox ID="txvTiempoReposicion" runat="server" CssClass="input" MaxLength="2" onkeyup="formato_numero(this)" Visible="False" Width="50%"></asp:TextBox>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Checked="True" Text="Activo" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 120px">
                       
                        &nbsp;</td>
                    <td style="text-align: left; width: 400px">
                       
                        &nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 120px">
                        &nbsp;</td>
                    <td style="text-align: left; " colspan="3">
                        &nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; " colspan="4">
                        <asp:TextBox ID="txtNotas" runat="server" Height="50px" TextMode="MultiLine" Width="100%" placeholder="Observacione y/o notas..." CssClass="input" Visible="False"></asp:TextBox>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; " colspan="4">
                        <asp:GridView ID="gvPlanes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged1" PageSize="20" Width="100%">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:BoundField DataField="codigo" HeaderText="Cod. Grupo">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Nombre de grupo ">
                                    <ItemStyle Width="40%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Sub grupo asociado">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="100%">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </td>
                    <td></td>
                </tr>
            </table>

            <hr />

            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged1" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Item" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcionAbreviada" HeaderText="DesCorta" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="referencia" HeaderText="Referencia" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoItem" HeaderText="Tipo" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="5px" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
    </div>

    <script src="http://app.infos.com/recursosinfos/lib/chosen-js/chosen.jquery.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/promise-polyfill/promise.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/chosen-js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/lou-multi-select/js/jquery.multi-select.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/moment/moment.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/js/Core/core.js"></script>
</body>
</html>
