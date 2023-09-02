<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContratistaSS.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.ContratistaSS" %>

<%@ OutputCache Location="None" %>
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
                        <asp:Button ID="nibtnBuscar" runat="server" CssClass="botones" OnClick="nibtnBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nibtnNuevo" runat="server" CssClass="botones" OnClick="nibtnNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="botones" OnClick="nibtnCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="nibtnGuardar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" Style="height: 26px" />
                    </td>
                </tr>
            </table>
           
            <hr />
            <table style="width: 100%;" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 100px">
                        <asp:Label ID="Label7" runat="server" Text="Proveedores" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="100%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 100px">
                        <asp:Label ID="Label9" runat="server" Text="Fecha inicial" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <table class="auto-style1">
                            <tr>
                                <td style="text-align: left; width: 110px">
                                    <asp:TextBox ID="txtFechaInicial" runat="server" class="input" placeholder="DD/MM/AAAA" ToolTip="Formato fecha (dd/mm/yyyy)" Visible="False" Width="100px" CssClass="input fecha"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 100px">
                                    <asp:Label ID="Label10" runat="server" Text="Fecha final" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaFinal" runat="server" class="input" placeholder="DD/MM/AAAA" ToolTip="Formato fecha (dd/mm/yyyy)" Visible="False" Width="100px" CssClass="input fecha"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; vertical-align: top;" colspan="2">
                        <asp:TextBox ID="txtObservacion" runat="server" class="input" Font-Bold="True" ReadOnly="True" Visible="False" Width="100%" Height="100px" placeholder="Observaciones y/o notas..." OnTextChanged="txtFechaFinal_TextChanged" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
             <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
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
                        <asp:BoundField DataField="tercero" HeaderText="C&#243;digo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="NombreProveedor" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaInicial" HeaderText="FechaInicial" ReadOnly="True" DataFormatString="{0:dd/MM/yyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaFinal" HeaderText="FechaFinal" ReadOnly="True" DataFormatString="{0:dd/MM/yyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="observacion" HeaderText="Observación" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
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
        </form>
    </div>
    

</body>
</html>
