﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fondos.aspx.cs" Inherits="Nomina.WebForms.Formas.Pentidades.Fondos" %>

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
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtCodigo" runat="server" Visible="False" Width="200px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input"></asp:TextBox>
                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label6" runat="server" Text="Tercero" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTercero" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" AutoPostBack="True" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label7" runat="server" Text="Proveedor" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlProveedor" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label8" runat="server" Text="Código Nacional" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlCodigoNacional" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label9" runat="server" Text="Cuenta" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlCuenta" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label10" runat="server" Text="Tipo Fondo" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoFondo" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                            <asp:ListItem Value="1">Fondo de empleado</asp:ListItem>
                            <asp:ListItem Value="2">Sindicato</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align:left;" colspan="2">
                        <asp:TextBox ID="txtObservacion" placeholder="Observaciones y/o notas..."  runat="server" Visible="False" Width="100%" CssClass="input" Height="60px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td ></td>
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
                                <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True"
                                SortExpression="descripcion">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="tercero" HeaderText="Tercero" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="proveedor" HeaderText="Proveedor" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigoNacional" HeaderText="CodNac" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Observacion" HeaderText="Observación" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="activo" HeaderText="Act">
                                <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="cuenta" HeaderText="Cuenta" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="30px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="tipoFondo" HeaderText="Tipo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="10px" />
                            </asp:BoundField>
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
