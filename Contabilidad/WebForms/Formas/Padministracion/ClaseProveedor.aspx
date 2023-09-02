<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaseProveedor.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.ClaseProveedor" %>

<%@ OutputCache Location="None" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Contabilidad</title>
   
    <link href="../../css/Formularios.css" rel="stylesheet" /> 
     <script type="text/javascript" >
         javascript: window.history.forward(1);
     </script>
     
</head>
<body >
    <form id="form1" runat="server">
   <div style="vertical-align: top; width: 1000px; height: 600px; text-align: center; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 12px; color: #003366;">
        <table cellspacing="0" style="width: 1000px">
            <tr>
                <td class="bordes" style="width: 250px">
                        <asp:ImageButton ID="nilbAtras" runat="server" ToolTip="Regresar" ImageUrl="~/Imagen/TabsIcon/back.png" OnClick="ImageButton1_Click" />
                    </td>
                <td class="nombreCampos">
                    Busqueda</td>
                <td class="Campos">
                    <asp:TextBox ID="nitxtBusqueda"  runat="server" Width="350px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                <td class="bordes">                        <asp:ImageButton ID="nilbTerceros" runat="server" OnClick="nilbTerceros_Click" ToolTip="Proveedores" ImageUrl="~/Imagen/Bonotes/pProveedores.png" />
                        </td>
            </tr>
            <tr>
                <td colspan="4">
                        <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" ToolTip="Cancela la operación" OnClick="lbCancelar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" Visible="False" />

                        <asp:ImageButton ID="lbRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" ToolTip="Guarda el nuevo registro en la base de datos"
                            onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" OnClick="nilbGuardar_Click" Visible="False" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                    </td>
            </tr>
        </table>
        <table cellspacing="0" style="border-top: silver thin solid; border-bottom: silver thin solid; width: 1000px; ">
            <tr>
                <td class="bordes" colspan="4">
                    <asp:Label ID="nilblInformacion" runat="server" ForeColor="#404040"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos">
                    <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label></td>
                <td class="Campos">
                    <asp:TextBox ID="txtCodigo" runat="server" Visible="False" Width="200px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input"></asp:TextBox></td>
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
                <td colspan="4">
                    <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
    <div class="tablaGrilla">
            <div style="display:inline-block"> 
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" Width="400px" OnPageIndexChanging="gvLista_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Código" >
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <RowStyle CssClass="rw" />
                    </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
