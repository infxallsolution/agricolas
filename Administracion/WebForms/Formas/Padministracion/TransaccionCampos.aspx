<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransaccionCampos.aspx.cs" Inherits="Administracion.WebForms.Formas.Padministracion.TransaccionCampos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>
   <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>

<body style="text-align: center">

    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%" >
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False"  />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
            <hr />
            <table id="tdCampos"  width="100%">
                <tr>
                    <td></td>
                    <td width="200px" style="text-align: left; width: 130px">
                        <asp:Label ID="Label2" runat="server" Text="Tipo Transacción" Visible="False" ></asp:Label>
                    </td>
                    <td width="400px" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoTransaccion" runat="server"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label3" runat="server" Text="Entidad" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlEntidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label4" runat="server" Text="Campo" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlCampo" runat="server"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label5" runat="server" Text="Tipo Campo" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoCampo" runat="server"  Width="200px" CssClass="chzn-select-deselect" Visible="False">
                            <asp:ListItem Value="chk">CheckBox</asp:ListItem>
                            <asp:ListItem Value="ddl">ComboBox</asp:ListItem>
                            <asp:ListItem Value="txv">TextBox Número</asp:ListItem>
                            <asp:ListItem Value="txt">TextBox</asp:ListItem>
                            <asp:ListItem Value="rbl">RadioButtonList</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style2" colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td class="text-left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkTercero" runat="server" Text="Es Tercero" Visible="False"  />
                                </td>
                                <td class="text-left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAplicaTercero" runat="server" Text="Aplica Tercero" Visible="False"  />
                                </td>
                                <td class="text-left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAplicaCliente" runat="server" Text="Aplica Cliente" Visible="False"  />
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAplicaProveedor" runat="server" Text="Aplica Proveedor" Visible="False"  />
                                </td>
                                <td class="text-left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkTerceroDefecto" runat="server" Text="Usuario por Defecto" Visible="False"  />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="40" AllowPaging="True">
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
                        <asp:BoundField DataField="tipoTransaccion" HeaderText="Tipo">
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="nombreTransaccion" HeaderText="Tipo">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="entidad" HeaderText="Entidad">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="campo" HeaderText="Campo">
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoCampo" HeaderText="TipoCampo">
                            <ItemStyle Width="10px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="tercero" HeaderText="Tercero">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="aplicaCliente" HeaderText="Cliente">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="aplicaProveedor" HeaderText="Proveedor">
                            <ItemStyle HorizontalAlign="Left" Width="10px" Wrap="True" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="aplicaTercero" HeaderText="Tercero">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="terceroDefecto" HeaderText="Defecto">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
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
