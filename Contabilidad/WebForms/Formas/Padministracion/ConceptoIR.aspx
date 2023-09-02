<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConceptoIR.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.ConceptoIR" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="formContainer" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" TabIndex="23" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label></td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" MaxLength="5" OnTextChanged="txtConcepto_TextChanged" Width="80px" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label3" runat="server" Text="Clase Imp/Rete" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlIR" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="100%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="100%" CssClass="input" Visible="False"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label4" runat="server" Text="Cálculo" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlCalculo" runat="server" CssClass="chzn-select-deselect" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlCalculo_SelectedIndexChanged" Visible="False">
                            <asp:ListItem Value="T">Tasa</asp:ListItem>
                            <asp:ListItem Value="V">Valor</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label6" runat="server" Text="Base gravable(%)" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtBaseGravable" onkeyup="formato_numero(this)" runat="server" Width="100px" CssClass="input" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label7" runat="server" Text="Tasa(%)" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtTasa" onkeyup="formato_numero(this)" runat="server" Width="100px" CssClass="input" MIN="1" Max="100" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label5" runat="server" Text="Base minima" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtBaseMinima" onkeyup="formato_numero(this)" runat="server" Width="200px" CssClass="input" Visible="False"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left;" colspan="2">
                          <asp:CheckBox ID="CheckBox1" runat="server" Text="Compra" Visible="False"  CssClass="checkbox checkbox-primary" />
                        <asp:CheckBox ID="chkCompra" runat="server" Text="Compra" Visible="False" CssClass="checkbox checkbox-primary"  />
                        <asp:CheckBox ID="chkVenta" runat="server" CssClass="checkbox checkbox-primary" Text=" Venta" Visible="False" />
                        <asp:CheckBox ID="chkActivo" runat="server" CssClass="checkbox checkbox-primary" Text=" Activo" Visible="False" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de elimnar el registro?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Código">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="clase" HeaderText="ClaseImpRet">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="calculo" HeaderText="Cálculo">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="baseGravable" HeaderText="BG(%)">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tasa" HeaderText="Tasa(%)">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BaseMinima" HeaderText="BaseMinima">
                            <ItemStyle Width="30px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="compra" HeaderText="Compra">
                            <ItemStyle Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="venta" HeaderText="Venta">
                            <ItemStyle Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <ItemStyle Width="5px" />
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
