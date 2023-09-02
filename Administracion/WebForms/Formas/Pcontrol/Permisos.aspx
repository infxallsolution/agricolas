<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="Administracion.WebForms.Formas.Pcontrol.Permisos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>

</head>
<body style="text-align: center">

    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <div style="vertical-align: top; width: 100%;">
                <div style="width: 100%; text-align: left;">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10px"></td>
                            <td style="width: 90px">
                                <asp:Label ID="Label1" runat="server" Text="Empresa"></asp:Label>
                            </td>
                            <td style="width: 300px">
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Width="320px" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                </asp:DropDownList>

                            </td>

                            <td style="width: 300px">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" />
                                <asp:Button ID="btnRefrescar" runat="server" CssClass="botones" Text="Refrescar permisos" Visible="true" OnClick="btnRefrescar_Click" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left"></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; height: 10px;"></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <fieldset>
                        <legend>Perfiles</legend>
                        <div data-spy="scroll" style="width: 100%; height: 300px; overflow-y: scroll;">
                            <div class="panel panel-default">
                                <div>
                                    <asp:GridView ID="gvPerfiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sel">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPerfiles" runat="server" OnCheckedChanged="chkPerfiles_CheckedChanged" AutoPostBack="True" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigo" HeaderText="Código">
                                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="thead" />
                                        <PagerStyle CssClass="footer" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <hr />
                    <fieldset>
                        <legend>Usuarios</legend>
                        <div data-spy="scroll" style="width: 100%; height: 310px; overflow-y: scroll;">
                            <div class="panel panel-default">
                                <div>
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkMarcarUsuario" runat="server" Text="Marcar Todos" Width="160px" AutoPostBack="True" OnCheckedChanged="chkMarcarUsuario_CheckedChanged" />
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDesmarcarUsuario" runat="server" Text="Desmarcar Todos" Width="160px" AutoPostBack="True" OnCheckedChanged="chkDesmarcarUsuario_CheckedChanged" />
                                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sel">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkUsuarios" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="usuario" HeaderText="Código">
                                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="thead" />
                                        <PagerStyle CssClass="footer" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="col-md-6">
                    <fieldset>
                        <legend>Métodos</legend>
                        <div data-spy="scroll" style="width: 100%; height: 687px; overflow-y: scroll;">
                            <div class="panel panel-default">
                                <div>
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkMarcarTPermisos" runat="server" Text="Marcar Todos" Width="160px" AutoPostBack="True" OnCheckedChanged="chkMarcarTPermisos_CheckedChanged" />
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDesmarcarTPermisos" runat="server" Text="Desmarcar Todos" Width="160px" AutoPostBack="True" OnCheckedChanged="chkDesmarcarTPermisos_CheckedChanged" />
                                    <asp:TreeView ID="tvPermisos" class="treeview" ForeColor="Blue"
                                        ExpandDepth="2" OnTreeNodeCheckChanged="tvPermisos_TreeNodeCheckChanged"
                                        runat="server">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
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
    <script type="text/javascript">
        function postBackByObject(e) {
            var evt = e || window.event;
            var o = evt.target || evt.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }
    </script>


</body>
</html>
