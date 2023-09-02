﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoIncapacidad.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.TipoIncapacidad" %>

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
                        <asp:Label ID="Label2" runat="server" Text="Código" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtCodigo" runat="server" Visible="False" Width="100px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input"></asp:TextBox></td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label3" runat="server" Text="Descripción" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="100%" CssClass="input" MaxLength="550"></asp:TextBox></td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="Label4" runat="server" Text="Porcentaje (%)" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <table >
                            <tr>
                                <td style="text-align: left; width: 120px">
                                    <asp:TextBox ID="txvPorcentaje" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label5" runat="server" Text="Afecta Novedad SS" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlAfectaNovedad" runat="server" CssClass="chzn-select-deselect" Visible="False">
                                        <asp:ListItem>NA</asp:ListItem>
                                        <asp:ListItem>IGE</asp:ListItem>
                                        <asp:ListItem>LMA</asp:ListItem>
                                        <asp:ListItem>IRP</asp:ListItem>
                                        <asp:ListItem>SLN</asp:ListItem>
                                        <asp:ListItem Value="VAC">VAC-L</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td colspan="2" style="text-align: left">
                        <table >
                            <tr>
                                <td style="text-align: left">
                                   <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                   <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAdicionar" runat="server" Text="Adicionar porcentaje (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkAdicionar_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="2">
                                   <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCalculaIBC" runat="server" Text="Calcula con IBC del mes anterior " Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                   <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAfectaSeguridadSocialARL" runat="server" Text="Afectación seguridad social (ARL)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkAdicionar_CheckedChanged" />
                                </td>
                                <td style="text-align: left">
                                   <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAfectaSeguridadSocial" runat="server" Text="Afectación seguridad social (SLN)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkAdicionar_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="width:130px; text-align:left;">
                        <asp:Label ID="lblDespues" runat="server" Text="Días despues" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <table >
                            <tr>
                                <td style="text-align: left; width: 90px">
                                    <asp:TextBox ID="txvDiasDespues" runat="server" Visible="False" Width="80px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblPorcentajeNuevo" runat="server" Text="Porcentaje nuevo (%)" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txvPorcentajeNuevo" runat="server" Visible="False" Width="80px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
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
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Código" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="porcentaje" HeaderText="%" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="afectaSeguridadSocial" HeaderText="AfecSS">
                            <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="adicionarPorcentaje" HeaderText="AdicPor">
                            <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="despues" HeaderText="DiasDesp" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="porcentajeNuevo" HeaderText="Nuevo%" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="afectaARL" HeaderText="ARL">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="afectaNovedadSS" HeaderText="ANSS" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="5px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="calculaIBC" HeaderText="CalIBC">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
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
