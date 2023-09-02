<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransaccionTipos.aspx.cs" Inherits="Administracion.WebForms.Formas.Padministracion.TransaccionTipos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%" cellspacing="0">
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
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left" style="width: 300px">
                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txtConcepto_TextChanged" Visible="False" Width="150px"></asp:TextBox>
                    </td>
                    <td style="width: 150px" class="text-left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                    </td>
                    <td class="text-left" style="width: 300px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkNumeracion" runat="server" Text="Numeración Automática" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left" colspan="3">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input" onkeyup="this.value=this.value.toUpperCase()" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label4" runat="server" Text="Prefijo" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left" style="width: 300px">
                        <asp:TextBox ID="txtPrefijo" runat="server" CssClass="input" Visible="False" onkeyup="this.value=this.value.toUpperCase()" Width="150px"></asp:TextBox>
                    </td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label3" runat="server" Text="Nro. Actual" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left" style="width: 300px">
                        <asp:TextBox ID="txtActual" runat="server" CssClass="input" Visible="False" Width="130px" onkeyup="formato_numero(this)">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label6" runat="server" Text="Naturaleza" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px;" class="text-left">
                        <asp:DropDownList ID="ddlNaturaleza" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="250px">
                            <asp:ListItem Value="0">No Aplica</asp:ListItem>
                            <asp:ListItem Value="2">Resta</asp:ListItem>
                            <asp:ListItem Value="1">Suma</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label5" runat="server" Text="Longitud" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px;" class="text-left">
                        <asp:TextBox ID="txvLongitud" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" Width="130px">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label7" runat="server" Text="Modulo" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px;" class="text-left">
                        <asp:DropDownList ID="ddlModulo" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px" class="text-left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkReferencia" runat="server" AutoPostBack="True" OnCheckedChanged="chkReferencia_CheckedChanged" Text="Referencia" Visible="False" />
                    </td>
                    <td style="width: 300px;" class="text-left">
                        <asp:TextBox ID="txtDataSet" runat="server" CssClass="input" Visible="False" Width="300px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label8" runat="server" Text="Met. Anulación" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px;" class="text-left">
                        <asp:DropDownList ID="ddlModoAnulacion" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="250px">
                            <asp:ListItem Value="A">Anular</asp:ListItem>
                            <asp:ListItem Value="E">Eliminar</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label9" runat="server" Text="Formato de impresión" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px;" class="text-left">
                        <asp:TextBox ID="txtFormatoImpresion" runat="server" CssClass="input" Visible="False" Width="300px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left"></td>
                    <td style="width: 300px;" class="text-left"></td>
                    <td style="width: 150px" class="text-left"></td>
                    <td style="width: 300px;" class="text-left"></td>
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
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Código">
                            <HeaderStyle Width="60px" />
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="numeracion" HeaderText="Num">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="actual" HeaderText="NoAct">
                            <ItemStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="prefijo" HeaderText="Prefijo">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="longitud" HeaderText="Log">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="naturaleza" HeaderText="Signo">
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="modulo" HeaderText="Modulo">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="modoAnulacion" HeaderText="Modo">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="referencia" HeaderText="Ref">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="vistaDs" HeaderText="Ds">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="formato" HeaderText="FormatoImpr">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
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
