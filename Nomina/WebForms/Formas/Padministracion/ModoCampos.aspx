<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModoCampos.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.ModoCampos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agronómico</title>
    <link href="../../css/Formularios.css" rel="stylesheet" />
    <link href="../../css/chosen.css" rel="stylesheet" />
</head>


<body style="text-align: center">
    <form id="form1" runat="server">
        <div style="vertical-align: top; width: 1000px; height: 600px; text-align: center; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 12px; color: #003366;">
            <table  style="width: 1000px">
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">Busqueda</td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="nitxtBusqueda" runat="server" Width="300px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td ></td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="4">
                        <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" />
                        <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" ToolTip="Cancela la operación" OnClick="lbCancelar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" Visible="False" />

                        <asp:ImageButton ID="lbRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" ToolTip="Guarda el nuevo registro en la base de datos"
                            onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" OnClick="lbRegistrar_Click" Visible="False" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                    </td>
                </tr>
            </table>
            <table id="TABLE1"  style="BORDER-TOP: silver thin solid; WIDTH: 1000px; BORDER-BOTTOM: silver thin solid">
                <tr>
                    <td colspan="4">
                        <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label2" runat="server" Cssstyle="text-align: left; width: 400px" Text="Modo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoTransaccion" runat="server" Visible="False" Width="300px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label1" runat="server" Cssstyle="text-align: left; width: 400px" Text="Entidad" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlEntidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged" Visible="False" Width="300px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label3" runat="server" Cssstyle="text-align: left; width: 400px" Text="Campo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlCampo" runat="server" Visible="False" Width="300px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label4" runat="server" Cssstyle="text-align: left; width: 400px" Text="Tipo Campo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoCampo" runat="server" Visible="False" Width="200px" CssClass="input">
                            <asp:ListItem>Seleccione una opción</asp:ListItem>
                            <asp:ListItem Value="chk">CheckBox</asp:ListItem>
                            <asp:ListItem Value="ddl">ComboBox</asp:ListItem>
                            <asp:ListItem Value="txv">TextBox Número</asp:ListItem>
                            <asp:ListItem Value="txt">TextBox</asp:ListItem>
                            <asp:ListItem Value="rbl">RadioButtonList</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

            <div style="margin: 10px">
                <div style="display: inline-block">
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" Width="960px" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="Select" HeaderText="Edit" ImageUrl="~/Imagen/TabsIcon/pencil.png" Text="Edit">
                                <HeaderStyle Width="20px" />
                                <ItemStyle CssClass="Items" HorizontalAlign="Center" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Elim">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbEliminar" runat="server" CommandName="Delete" ImageUrl="~/Imagen/TabsIcon/cancel.png" OnClientClick="if(!confirm('Desea eliminar el registro ?')){return false;};" ToolTip="Elimina el registro seleccionado" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle CssClass="Items" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="modo" HeaderText="Modo" ReadOnly="True">
                                <HeaderStyle  HorizontalAlign="Left" />
                                <ItemStyle  HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="entidad" HeaderText="Entidad" ReadOnly="True">
                                <HeaderStyle  HorizontalAlign="Left" />
                                <ItemStyle  HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="campo" HeaderText="Campo">
                                <HeaderStyle  HorizontalAlign="Left" />
                                <ItemStyle  HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipoCampo" HeaderText="Tipo Campo">
                                <HeaderStyle  HorizontalAlign="Left" />
                                <ItemStyle  HorizontalAlign="Left" />
                            </asp:BoundField>
                           
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <RowStyle CssClass="rw" />
                    </asp:GridView>
                </div>
            </div>

        </div>
        <script src="../../js/jquery.min.js" type="text/javascript"></script>
        <script src="../../js/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>


    </form>
</body>
</html>
