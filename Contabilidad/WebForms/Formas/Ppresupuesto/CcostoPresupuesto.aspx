<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CCostoPresupuesto.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Ppresupuesto.CCostoPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>

    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/js/chosen.jquery.js" type="text/javascript"></script>
</head>

<body style="text-align: center">
    <div class="loading">
        <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
    </div>
    <div class="container">
        <form id="form1" runat="server">
            <table style="width: 100%" cellspacing="0">
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
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
            <hr />
            <table width="100%">
                <tr>
                    <td></td>
                    <td width="200px" class="auto-style1">
                        <asp:Label ID="Label2" runat="server" Text="Código" Visible="False"></asp:Label></td>
                    <td style="text-align: left" width="300px">
                        <asp:TextBox ID="txtCodigo" runat="server" Visible="False" Width="200px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" width="150px">
                        <asp:Label ID="Label4" runat="server" Text="Nombre " Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombre" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" width="150px">
                        <asp:Label ID="Label3" runat="server" Text="Padre" Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlRaiz" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" width="150px">
                        <asp:Label ID="Label6" runat="server" Text="Tipo Ccosto" Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipo" runat="server" Visible="False" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" CssClass="chzn-select">
                            <asp:ListItem Value="A">Auxiliar</asp:ListItem>
                            <asp:ListItem Value="M">Mayor</asp:ListItem>
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" width="150px">
                        <asp:Label ID="Label5" runat="server" Text="Nivel" Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNivel" runat="server" Visible="False" Width="50px" CssClass="input">0</asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left"></td>
                    <td style="text-align: left">
                        <asp:CheckBox
                            ID="chkActivo" runat="server" Text="Ccosto. Activo" Visible="False" Checked="True" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left"></td>
                    <td style="text-align: left">
                        <asp:CheckBox
                            ID="chkInforme" runat="server" Text="Informe" Visible="False" Checked="True" />
                        <asp:CheckBox
                            ID="chkEjecutado" runat="server" Text="Ejecutado" Visible="False" Checked="True" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
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
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="if(!confirm('Desea eliminar el registro ?')){return false;};" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="True">
                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="padre" HeaderText="Padre" ReadOnly="True">
                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nivel" HeaderText="Nivel">
                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo">
                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="mInforme" HeaderText="Informe"></asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mEjecutado" HeaderText="Ejecutado"></asp:CheckBoxField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activa"></asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
    </div>
    <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/moment/moment.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/js/chosen.jquery.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/iCheck/icheck.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/sweetalert2/dist/sweetalert2.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/js/core.js" type="text/javascript"></script>
</body>
</html>
