<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoNovedadSS.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.TipoNovedadSS" %>

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
                    <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label5" runat="server" Text="Novedad SS" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:DropDownList ID="ddlTipoNovedad" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="100px">
                            <asp:ListItem>NA</asp:ListItem>
                            <asp:ListItem>IGE</asp:ListItem>
                            <asp:ListItem>LMA</asp:ListItem>
                            <asp:ListItem>IRP</asp:ListItem>
                            <asp:ListItem>SLN</asp:ListItem>
                            <asp:ListItem Value="VAC">VAC-L</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 120px">
                       <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSalud" runat="server" Text="Salud (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkSalud_CheckedChanged" />
                        </td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txvPorcentajeSalud" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                        </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 120px">
                       <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPension" runat="server" Text="Pensión (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkPension_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txvPorcentajePension" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 120px">
                       <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkARP" runat="server" Text="ARP (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkARP_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txvPorcentajeARP" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 120px">
                       <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkFondoSolidaridad" runat="server" Text="Fond Solidar. (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkFondoSolidaridad_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txvPorcentajeFondoSolidaridad" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 120px">
                       <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSena" runat="server" Text="Sena (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkSena_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txvPorcentajeSena" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 120px">
                       <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCaja" runat="server" Text="Caja (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkCaja_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txvPorcentajeCaja" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 120px">
                       <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkICBF" runat="server" Text="I.C.B.F. (%)" Visible="False" AutoPostBack="True" OnCheckedChanged="chkICBF_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txvPorcentajeICBF" runat="server" Visible="False" Width="100px" onkeyup="formato_numero(this)" CssClass="input"></asp:TextBox>
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
                        <asp:BoundField DataField="tipoNovedad" HeaderText="Novedad" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="5px" />
                        </asp:BoundField>
                         <asp:CheckBoxField DataField="salud" HeaderText="Salud">
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                            </asp:CheckBoxField>
                         <asp:BoundField DataField="pSalud" HeaderText="%Salud" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                            <asp:CheckBoxField DataField="pension" HeaderText="Pensión">
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                            </asp:CheckBoxField>
                         <asp:BoundField DataField="pPension" HeaderText="%Pension" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                            <asp:CheckBoxField DataField="solidaridad" HeaderText="FondoSol">
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                            </asp:CheckBoxField>
                         <asp:BoundField DataField="pSolidaridad" HeaderText="%Fondo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                            <asp:CheckBoxField DataField="arp" HeaderText="ARP">
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                            </asp:CheckBoxField>
                         <asp:BoundField DataField="pARP" HeaderText="%ARP" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                            <asp:CheckBoxField DataField="caja" HeaderText="Caja">
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                            </asp:CheckBoxField>
                         <asp:BoundField DataField="pCaja" HeaderText="%Caja" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                            <asp:CheckBoxField DataField="sena" HeaderText="Sena">
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                            </asp:CheckBoxField>
                           <asp:BoundField DataField="pSena" HeaderText="%Sena" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
                        </asp:BoundField>
                            <asp:CheckBoxField DataField="icbf" HeaderText="ICBF">
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                            </asp:CheckBoxField>
                           <asp:BoundField DataField="pICBF" HeaderText="%ICBF" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left"  Width="70px" />
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
