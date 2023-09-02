<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cuadrillas.aspx.cs" Inherits="Nomina.WebForms.Formas.Pprogramacion.Cuadrillas" %>

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
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <table style="width: 100%;" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label7" runat="server" Text="Departamento" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlDepartamento" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="niddlDepartamento_SelectedIndexChanged" Width="100%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px;">
                        <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px;">
                         <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txtCodigo_TextChanged" Visible="False" Width="190px"></asp:TextBox>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label6" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">
                        <select runat="server" id="selFuncionarios" class="multiselect" multiple="true" name="countries[]" visible="False" style="width: 700px; height: 200px">
                        </select></td>
                    <td></td>
                </tr>

            </table>
            <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
            <hr />
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
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" BorderWidth="1px" />
                            <ItemStyle HorizontalAlign="Left" BorderWidth="1px" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" BorderWidth="1px" />
                            <ItemStyle HorizontalAlign="Left" BorderWidth="1px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="departamento" HeaderText="Departamento">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Left" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreDepartamento" HeaderText="NombreDepartamento">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Left" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                        </asp:CheckBoxField>
                           
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
