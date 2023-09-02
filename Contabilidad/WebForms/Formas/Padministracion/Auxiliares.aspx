<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Auxiliares.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.Auxiliares" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Agronómico</title>
    <link href="../../css/Formularios.css" rel="stylesheet" />
</head>


<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="vertical-align: top; width: 1000px; height: 600px; text-align: center; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 12px; color: #003366;">
        <table cellspacing="0" style="width: 1000px">
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos" >
                    Entidad</td>
                <td class="Campos" >
                    <asp:DropDownList ID="niddlEntidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged" CssClass="input"
                        Width="300px">
                    </asp:DropDownList></td>
                <td class="bordes">
                    </td>
            </tr>
            <tr>
                <td class="bordes">
                </td>
                <td class="nombreCampos" >
                    Busqueda</td>
                <td class="Campos" >
                    <asp:TextBox ID="nitxtBusqueda" runat="server" Width="300px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                <td class="bordes">
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="4">
                    <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click" 
                        onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'" 
                        onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'"
                         />
                    <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click"
                        onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'" 
                        onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'"   />
                     <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="lbCancelar_Click"
                        onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'" 
                        onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" Visible="False"                         />
                   
                    <asp:ImageButton ID="lbRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" ToolTip="Habilita el formulario para un nuevo registro" 
                        onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'" 
                        onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" OnClick="nilbGuardar_Click" Visible="False"   />
                </td>
            </tr>
        </table>
        <table cellspacing="0" style="width: 1000px; border-top: silver thin solid; border-bottom: silver thin solid;" id="TABLE1">
            <tr>
                <td style="height: 15px;  text-align: center;" colspan="4">
                    <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                   
                    </td>
            </tr>
            <tr>
                <td class="bordes" >
                </td>
                <td class="nombreCampos" >
                </td>
                <td class="Campos" >
                </td>
                <td class="bordes" >
                </td>
            </tr>
            <tr>
                <td >
                </td>
                <td class="nombreCampos" >
                    <asp:Label ID="Label1" runat="server" Text="Código" Visible="False" CssClass="label"></asp:Label></td>
                <td class="Campos" >
                    <asp:TextBox ID="txtCodigo" runat="server" Visible="False" Width="200px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input" ></asp:TextBox></td>
                <td >
                </td>
            </tr>
            <tr>
                <td >
                </td>
                <td class="nombreCampos" >
                    <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False" CssClass="label"></asp:Label></td>
                <td class="Campos" >
                    <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="350px" CssClass="input"></asp:TextBox></td>
                <td >
                </td>
            </tr>
            <tr>
                <td >
                </td>
                <td >
                </td>
                <td >
                </td>
                <td >
                </td>
            </tr>
            <tr>
                <td >
                </td>
                <td >
                </td>
                <td >
                    <asp:Label ID="nilblMensaje" runat="server"></asp:Label></td>
                <td >
                    </td>
            </tr>
        </table>

        <div class="tablaGrilla">

                    <asp:GridView ID="gvLista" runat="server" Width="800px" GridLines="None" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False">
                        <HeaderStyle BackColor="#77C3E3" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <AlternatingRowStyle BackColor="#E0E0E0" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" Text="Editar" CommandName="Select" ImageUrl="~/Imagenes/botones/Edit.png" >
                                <ControlStyle Height="20px" Width="20px" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" />
                                <ItemStyle Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                            </asp:ButtonField>
                            <asp:TemplateField>
                        <ItemTemplate>
                         <asp:ImageButton ID="imbEliminar" runat="server" CommandName="Delete" ImageUrl="~/Imagenes/botones/anular.png" 
                         ToolTip="Elimina el registro seleccionado" Width="20px" Height="20px" ImageAlign="Middle"
                         onclientclick="if(!confirm('Desea eliminar el registro ?')){return false;};" />
                        </ItemTemplate>                            
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True" SortExpression="concepto" >
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True"
                                SortExpression="descripcion" >
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <FooterStyle BackColor="LightYellow" />
                    </asp:GridView>

        </div>
          
    </div>
    </form>
</body>
</html>
