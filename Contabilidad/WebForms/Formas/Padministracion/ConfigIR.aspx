﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigIR.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.ConfigIR" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>

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
        <form  runat="server">
            <table style="width: 100%">
                <tr>
                   <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                   <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" TabIndex="23" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%" id="tdCampos">
                    <tr>
                        <td ></td>
                        <td style="width:150px; text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="Código" Visible="False" ></asp:Label></td>
                        <td style="width:400px; text-align: left;" >
                            <asp:TextBox ID="txtCodigo" runat="server"  Width="200px" CssClass="input" Visible="False"></asp:TextBox></td>
                        <td ></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left; width: 150px">
                            <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False" ></asp:Label></td>
                        <td style="text-align: left; width: 400px">
                            <asp:TextBox ID="txtDescripcion" runat="server"  Width="350px" CssClass="input" Visible="False"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left; width: 150px">
                            <asp:Label ID="Label3" runat="server" Text="Tipo" Visible="False" ></asp:Label></td>
                        <td style="text-align: left; width: 400px;">
                            <asp:DropDownList ID="rbCaracteristica" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" >
                                <asp:ListItem Selected="True" Value="AU">Autorretenedor</asp:ListItem>
                                <asp:ListItem Value="LI">Libre de impuesto</asp:ListItem>
                                <asp:ListItem Value="RS">Régimen  simplificado</asp:ListItem>
                                <asp:ListItem Value="RI">Responsable impuesto</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left; " colspan="2">
                            <asp:CheckBox ID="chkImpuesto" runat="server" Text="Impuesto" Visible="False" CssClass="checkbox checkbox-primary"  />
                            <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkRetencion" runat="server" Text="Retención" Visible="False"  />
                            <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCargaLLave" runat="server" Text="ManejaLlave" Visible="False"  />
                            <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False"  />
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
                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de elimnar el registro?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                <HeaderStyle CssClass="action-item" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="Código">
                                <ItemStyle CssClass="Items" HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="impuesto" HeaderText="Impuestos">
                            <ItemStyle Width="5px" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="retencion" HeaderText="Retención">
                            <ItemStyle Width="5px" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="auretenedor" HeaderText="Autore">
                            <ItemStyle Width="5px" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="libre" HeaderText="LibImp">
                            <ItemStyle Width="5px" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="regimensimplificado" HeaderText="Rsim">
                            <ItemStyle Width="5px" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="responsable" HeaderText="ResImp">
                            <ItemStyle Width="5px" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="mLLave" HeaderText="mLLave">
                            <ItemStyle Width="5px" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <ItemStyle Width="5px" />
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
