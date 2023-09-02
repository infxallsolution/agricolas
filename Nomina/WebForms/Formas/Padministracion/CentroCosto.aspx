<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CentroCosto.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.CentroCosto" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label4" runat="server" Text="Nivel" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlNivel" runat="server" Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged"
                            Visible="False" Width="200px" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label5" runat="server" Text="Nivel Mayor" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlNivelMayor" runat="server" Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlNivelPadre_SelectedIndexChanged" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label11" runat="server" Text="Mayor" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlMayor" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label9" runat="server" Text="Responsable"
                            Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtResponsable" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label8" runat="server" Text="Grupo CCosto"
                            Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlGrupo" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2" style="text-align: left">
                        <div class="row w-100">
                            <div class="col-4">
                                <asp:CheckBox ID="chkAuxiliar" runat="server" Text="Auxiliar" Visible="False" CssClass="checkbox checkbox-primary" />
                            </div>
                            <div class="col-4">
                                <asp:CheckBox ID="chkManejaHE" runat="server" Text="Maneja horas extras" Visible="False" CssClass="checkbox checkbox-primary" />
                            </div>
                            <div class="col-4">
                                <asp:CheckBox ID="chkManejaLC" runat="server" Text="Maneja labores de campo" Visible="False" CssClass="checkbox checkbox-primary" />
                            </div>
                        </div>
                        <div class="row w-100">
                            <div class="col-4">
                                <asp:CheckBox ID="chkProvisiona" runat="server" Text="No Provisiona" Visible="False" CssClass="checkbox checkbox-primary" />
                            </div>
                            <div class="col-4">
                                <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" Visible="False" CssClass="checkbox checkbox-primary" />

                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
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
                                <asp:HiddenField ID="hfDescripcion" runat="server" Value='<%#Eval("descripcion")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nivel" HeaderText="Nivel">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nivelMayor" HeaderText="NivelMayor">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mayor" HeaderText="Mayor">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcionConcadenada" HeaderText="Descripci&#243;n" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="grupo" HeaderText="Grupo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="responsable" HeaderText="Responsable">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="auxiliar" HeaderText="Aux">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaHE" HeaderText="HE">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaLC" HeaderText="LC">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="noProvisiona" HeaderText="NoProvisiona">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
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
