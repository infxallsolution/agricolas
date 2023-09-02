<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Destinos.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.Destinos" %>

<%@ OutputCache Location="None" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Seguridad</title>
    <link href="../../css/Formularios.css" rel="stylesheet" />
     <script type="text/javascript" >
         javascript: window.history.forward(1);
     </script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
   <div style="vertical-align: top; width: 1000px; height: 600px; text-align: center; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 12px; color: #003366;">
       <table cellspacing="0" style="width: 1000px">
            <tr>
                <td class="bordes">
                    </td>
                <td class="nombreCampos">
                    Busqueda</td>
                <td class="Campos">
                    <asp:TextBox ID="nitxtBusqueda"  runat="server" Width="350px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                <td>
                    </td>
            </tr>
            <tr>
                <td colspan="4">
                        <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" />
                        <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" ToolTip="Cancela la operación" OnClick="lbCancelar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" Visible="False" />

                        <asp:ImageButton ID="nilbGuardar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" ToolTip="Guarda el nuevo registro en la base de datos"
                            onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" OnClick="lbRegistrar_Click" Visible="False" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                    </td>
            </tr>
        </table>
        <table cellspacing="0" style="width: 1000px; border-top: silver thin solid; border-bottom: silver thin solid;" id="TABLE1" >
            <tr>
                <td class="bordes">
                    &nbsp;</td>
                <td class="nombreCampos">
                    </td>
                <td class="Campos">
                    <asp:Label ID="nilblInformacion" runat="server" ForeColor="#404040"></asp:Label></td>
                <td class="bordes">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label4" runat="server" Text="Nivel" Visible="False"></asp:Label></td>
                <td class="Campos">
                    <asp:DropDownList ID="ddlNivel" runat="server" Visible="False" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged" CssClass="input">
                    </asp:DropDownList></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label></td>
                <td class="Campos">
                    <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged"
                        Visible="False" Width="200px" CssClass="input"></asp:TextBox></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label5" runat="server" Text="Nivel Padre" Visible="False"></asp:Label></td>
                <td class="Campos">
                    <asp:DropDownList ID="ddlNivelPadre" runat="server" Visible="False" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivelPadre_SelectedIndexChanged" CssClass="input">
                    </asp:DropDownList></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label11" runat="server" Text="Padre" Visible="False"></asp:Label></td>
                <td class="Campos"><asp:DropDownList ID="ddlPadre" runat="server" Visible="False" Width="300px" CssClass="input">
                </asp:DropDownList></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                <td class="Campos">
                    <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="350px" CssClass="input"></asp:TextBox></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label8" runat="server" Text="Cta. Inversión"
                        Visible="False"></asp:Label></td>
                <td class="Campos">
                    <asp:DropDownList ID="ddlCtaInversion" runat="server" Visible="False" Width="300px" CssClass="input">
                    </asp:DropDownList></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label9" runat="server" Text="Cta. Gasto"
                        Visible="False"></asp:Label></td>
                <td class="Campos">
                    <asp:DropDownList ID="ddlCtaGasto" runat="server" Visible="False" Width="300px" CssClass="input">
                    </asp:DropDownList></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                </td>
                <td class="Campos">
                    <asp:CheckBox ID="chkActivo" runat="server" Text="Destino Activo" Visible="False" /></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                </td>
                <td class="Campos">
                    <asp:Label ID="nilblMensaje" runat="server"></asp:Label></td>
                <td class="bordes">
                    </td>
            </tr>
        </table>
          <div class="tablaGrilla">
                    <asp:GridView ID="gvLista" runat="server"  PageSize="20" AllowPaging="True" Width="100%" CssClass="Grid" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False">
                        <AlternatingRowStyle CssClass="alt" />
                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="Select" HeaderText="Edit" ImageUrl="~/Imagen/TabsIcon/pencil.png" Text="Edit">
                            <HeaderStyle BackColor="White" />
                            <ItemStyle Width="20px" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Elim">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imElimina" runat="server" CommandName="Delete" ImageUrl="~/Imagen/TabsIcon/cancel.png" OnClientClick="if(!confirm('Desea eliminar el registro ?')){return false;};" ToolTip="Elimina el registro seleccionado" />
                                </ItemTemplate>                           
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True" >
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nivel" HeaderText="Nivel">
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nivelPadre" HeaderText="Nivel Padre">
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="padre" HeaderText="Padre">
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True"
                                SortExpression="descripcion" >
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ctaInversion" HeaderText="Cta. Inversi&#243;n">
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ctaGasto" HeaderText="Cta. Gasto">
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                            </asp:CheckBoxField>
                        </Columns>
                          <PagerStyle CssClass="pgr" />
                        <RowStyle CssClass="rw" />
                    </asp:GridView>
             </div>

    </div>
    </form>
</body>
</html>
