<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Formatos.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxPagar.Formatos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body>

    <div class="container">
            <div class="loading">
        <i class="fas fa-spinner fa-spin fa-5x"></i>
    </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
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
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación"  />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos"  />
                    </td>
                </tr>
            </table>
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <br />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label7" runat="server" Text="Banco" ></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:DropDownList ID="ddlBanco" runat="server"  Width="350px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Nombre Campo" ></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:DropDownList ID="ddlCampos" runat="server"  Width="350px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label2" runat="server" Text="Tipo campo" ></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:DropDownList ID="ddlTipoDatos" runat="server"  Width="150px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                            <asp:ListItem Value="1">Númerico</asp:ListItem>
                            <asp:ListItem Value="2">Texto</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Inicio" ></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:TextBox ID="txvInicio" runat="server"  Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                        <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label5" runat="server" Text="Longitud" ></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:TextBox ID="txvLongitud" runat="server"  Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:CheckBox ID="chkValorFijo" runat="server" Text="Valor fijo"  AutoPostBack="True" OnCheckedChanged="chkValorFijo_CheckedChanged" />
                    </td>
                    <td style="width: 400px; text-align: left">
                        <asp:TextBox ID="txtValorFijo" runat="server" CssClass="input" Width="350px" ></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left"></td>
                    <td style="width: 400px; text-align: left">
                        <asp:Button ID="btnRegistrar" runat="server" OnClick="btnRegistrar_Click" Text="Agegar"
                             CssClass="botones" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div class="tablaGrilla">
                            <asp:GridView ID="gvListaDetalle" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
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

                                    <asp:BoundField DataField="registro" HeaderText="No">
                                        <HeaderStyle />
                                        <ItemStyle Width="10px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="campo" HeaderText="Campo">
                                        <HeaderStyle />
                                        <ItemStyle Width="220px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                        <HeaderStyle />
                                        <ItemStyle Width="10px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="inicio" DataFormatString="{0:N0}" HeaderText="Ini">
                                        <HeaderStyle />
                                        <ItemStyle Width="20px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="longitud" DataFormatString="{0:N0}" HeaderText="Long">
                                        <HeaderStyle />
                                        <ItemStyle Width="20px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="mValor" HeaderText="MV">
                                        <ItemStyle Width="10px" />
                                    </asp:CheckBoxField>
                                    <asp:BoundField DataField="valor" HeaderText="ValorFijo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="thead" />
                                <PagerStyle CssClass="footer" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                </table>
            <div class="tablaGrilla">
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
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="banco" HeaderText="Banco" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreBanco" HeaderText="NombreBanco" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
    </div>
   <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/jquery-ui/jquery-ui.min.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/moment/moment.js"></script>
    <script src="http://app.infos.com/RecursosInfos/js/daterangepicker.js" type="text/javascript"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/iCheck/icheck.min.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/chosen187/chosen.jquery.js"></script>
    <script src="http://app.infos.com/RecursosInfos/js/core.js"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/plugins/localisation/jquery.localisation-min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/ui.multiselect.js"></script>

</body>
</html>
