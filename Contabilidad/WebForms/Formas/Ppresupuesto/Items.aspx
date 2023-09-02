<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Ppresupuesto.Items" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <div class="container">
        <div class="loading">
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="bordesBusqueda">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td class="bordesBusqueda"></td>
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
                    <td style="width: 100px"></td>
                    <td>
                        <asp:UpdatePanel ID="upCabeza" runat="server" UpdateMode="Conditional" Visible="False">
                            <ContentTemplate>
                                <div style="padding: 3px 10px 3px 10px">
                                    <div style="border: 1px solid silver;">
                                        <div style="padding: 5px">
                                            <table style="width: 100%" id="tdCampos">
                                                <tr>
                                                    <td style="text-align: left; width: 150px">
                                                        <asp:Label ID="Label2" runat="server" Text="Item" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" Visible="False" Width="150px"></asp:TextBox>
                                                        <asp:CheckBox ID="chkActivo" runat="server" Checked="True" CssClass="checkbox checkbox-primary" Text="Activo" Visible="False" />
                                                    </td>
                                                    <td style="text-align: left; width: 150px">
                                                        <asp:Label ID="Label7" runat="server" Text="Referencia" Visible="False"></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: left">
                                                        <asp:TextBox ID="txtReferencia" runat="server" CssClass="input" MaxLength="550" Visible="False" Width="350px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 150px">
                                                        <asp:Label ID="Label3" runat="server" Text="Descripción" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input" MaxLength="550" Visible="False" Width="400px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; width: 150px">
                                                        <asp:Label ID="Label5" runat="server" Text="Desc. corta" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="width: 155px; text-align: left">
                                                        <asp:TextBox ID="txtDesCorta" runat="server" CssClass="input" MaxLength="50" Visible="False" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; ">
                                                        <asp:CheckBox ID="chkCalculo" runat="server" Checked="false" CssClass="checkbox checkbox-primary" Text="Maneja calculo temporal" Visible="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 150px">
                                                        <asp:Label ID="Label9" runat="server" Text="Unidad medida" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlUmedidaCompra" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="400px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: left; width: 150px;">
                                                        <asp:Label ID="Label14" runat="server" Text="Orden (#)" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="width: 155px; text-align: left">
                                                        <asp:TextBox ID="txvOrden" runat="server" CssClass="input" MaxLength="50" onkeyup="formato_numero(this)" Visible="False" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; ">
                                                        <asp:CheckBox ID="chkSello" runat="server" Checked="false" CssClass="checkbox checkbox-primary" Text="Maneja sellos" Visible="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 150px">
                                                        <asp:Label ID="Label6" runat="server" Text="Tipo de item" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlTipoItem" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="400px">
                                                            <asp:ListItem Value="IP">Item presupuesto</asp:ListItem>
                                                            <asp:ListItem Value="MP">Movimiento  presupuesto</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: left; width: 150px">
                                                        <asp:Label ID="Label15" runat="server" Text="Equivalencia" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="width: 155px; text-align: left">
                                                        <asp:TextBox ID="txtEquivalencia" runat="server" CssClass="input" MaxLength="50" Visible="False" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; ">
                                                        <asp:CheckBox ID="chkManejaAnalisis" runat="server" Checked="false" CssClass="checkbox checkbox-primary" Text="No maneja analisis" Visible="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align: left;">
                                                        <div style="padding: 5px; float: left; height: 100%; width: 100%;">
                                                            <div style="border: 1px solid silver; padding: 5px; float: left; width: 100%">
                                                                <div>
                                                                    <asp:Label ID="Label1" runat="server" Text="Notas" Visible="False"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtNotas" runat="server" CssClass="input" Height="80px" TextMode="MultiLine" Visible="False" Width="98%"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>

                    </td>
                    <td style="width: 100px"></td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>

            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged1" PageSize="40" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Código" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcionAbreviada" HeaderText="DesCorta" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="referencia" HeaderText="Referencia" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="uMedida" HeaderText="Umedia" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="notas" HeaderText="Notas" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="orden" HeaderText="Ord" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="equivalencia" HeaderText="Equi" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="Sello" HeaderText="mSello">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mCalculo" HeaderText="mCal">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mAnalisis" HeaderText="mAnal">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>

                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
         <script src="http://infos.aceitesa.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
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
        </form>
    </div>
  </body>
</html>
