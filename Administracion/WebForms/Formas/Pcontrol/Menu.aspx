<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Administracion.WebForms.Formas.Pcontrol.Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading"><i class="fas fa-spinner fa-spin fa-5x"></i></div>
        <form id="form2" runat="server">

            <table style="width: 100%; text-align: center" cellspacing="0">
                <tr>
                    <td style="text-align: center">
                        <asp:DropDownList ID="niddlSitio" runat="server" OnSelectedIndexChanged="niddlSitio_SelectedIndexChanged"
                            Width="40%" AutoPostBack="True" data-placeholder="Seleccione una modulo..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table id="tdCampos" width="100%">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:HiddenField ID="hfrowid" runat="server" />
                        <asp:CheckBox ID="chkPadre" runat="server" AutoPostBack="true" Text="Padre" OnCheckedChanged="chkPadre_CheckedChanged" CssClass="checkbox checkbox-primary" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlPadre" runat="server" OnSelectedIndexChanged="niddlSitio_SelectedIndexChanged"
                            Width="100%" AutoPostBack="True" data-placeholder="Seleccione una modulo..." CssClass="chzn-select-deselect" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label6" runat="server" Text="Descripción"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input" Width="100%" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:CheckBox ID="chkWebSite" runat="server" AutoPostBack="true" Text="Web site - url" OnCheckedChanged="chkWebSite_CheckedChanged" CssClass="checkbox checkbox-primary" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtPagina" runat="server" Width="100%" CssClass="input" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" CssClass="checkbox checkbox-primary" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 400px"></td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="text-align: left;" colspan="2">
                        <select runat="server" id="selOperacion" class="multiselect" multiple="true" name="countries[]" style="width: 700px; height: 200px; display: inline-block;" visible="False">
                        </select></td>
                    <td></td>
                </tr>

            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <div class="tablaGrilla">

                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" CssClass="table table-bordered table-sm  table-hover table-striped grillas text-left" Width="100%" AutoGenerateColumns="False" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="100" OnPageIndexChanging="gvLista_PageIndexChanging">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" HeaderText="Edit" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Elim">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="idMenu" HeaderText="Id">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="idPadre" HeaderText="idPadre"></asp:BoundField>
                        <asp:BoundField DataField="nombrePadre" HeaderText="Padre"></asp:BoundField>
                        <asp:BoundField DataField="pagina" HeaderText="Página"></asp:BoundField>
                        <asp:CheckBoxField DataField="mweb" HeaderText="Web" />
                        <asp:TemplateField HeaderText="Act">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%#Eval("activo")%>' Enabled="false"></asp:CheckBox>
                                <asp:HiddenField ID="hfModulo" Value='<%#Eval("modulo")%>' runat="server" />
                                <asp:HiddenField ID="hfrowid" Value='<%#Eval("rowid")%>' runat="server" />
                                <asp:HiddenField ID="hfPadre" Value='<%#Eval("padre")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="2px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
    </div>
    <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
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
