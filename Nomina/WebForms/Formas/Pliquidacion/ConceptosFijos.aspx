<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConceptosFijos.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.ConceptosFijos" %>

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
                    <td style="width: 140px; text-align: left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCentrocosto" runat="server" Text="Ccosto multiple" AutoPostBack="True" OnCheckedChanged="chkCentrocosto_CheckedChanged" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 470px">
                        <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="100%" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlCentroCosto_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">

                        <select runat="server" id="selCentroCosto" class="multiselect" multiple="true" name="countries[]0" visible="False">
                        </select></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 470px">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 120px">
                                    <asp:DropDownList ID="ddlAño" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Año..." OnSelectedIndexChanged="ddlAño_SelectedIndexChanged" Width="100px" Visible="False">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 60px">
                                    <asp:Label ID="Label11" runat="server" Text="Mes" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Mes..." OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" Width="150px" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="Label10" runat="server" Text="Periodo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 470px">
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="chzn-select" data-placeholder="Seleccione un periodo..." Width="100%" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="Label8" runat="server" Text="Forma de pago" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 470px">
                        <asp:DropDownList ID="ddlFomaPago" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="200px" Visible="False">
                            <asp:ListItem Value="0">Fijo</asp:ListItem>
                            <asp:ListItem Value="1">Destajo</asp:ListItem>
                            <asp:ListItem Value="2">Fijo a destajo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">
                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" placeholder="Observaciones, notas o comentarios..." Height="70px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <table style="width: 750px">
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary"  ID="chkLnovedades" runat="server" Text="Liquida Novedades +" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary"  ID="chkLPrimas" runat="server" Text="Liquida Primas" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary"  ID="chkLDomingos" runat="server" Text="Liquida Domingos" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary"  ID="chkLprestamos" runat="server" Text="Liquida Prestamos" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary"  ID="chkLausentismo" runat="server" Text="Liquida Ausentismo" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary"  ID="chkNovedadesCredito" runat="server" Text="Liquida Novedades -" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLFondavi" runat="server" Text="Liquida Fondavi" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLFestivos" runat="server" Text="Liquida Festivos" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkMuestraDomingo" runat="server" Text="Muestra Domingo" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLembargos" runat="server" Text="Liquida Embargos" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLhoras" runat="server" Text="Liquida Horas Extras" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLiquidaSindicato" runat="server" Text="Liquida Sindicato" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkFestivoPromedio" runat="server" Text="Promedia Festivo" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDomingoPromedio" runat="server" Text="Promedia Domingo" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLvacaciones" runat="server" Text="Liquida Vacaciones" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLDomingoCero" runat="server" Text="Domingos y Festivos Cero(0)" Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLotros" runat="server" Text="Otros" Visible="False" />
                                </td>
                                <td style="text-align: left"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: left"></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="2">
                            <select runat="server" id="selConceptos" class="multiselect" multiple="true" name="countries[]" visible="False">
                            </select></td>
                    <td>&nbsp;</td>
                </tr>
                </table>
           <%-- <div class="row">
                <div class="col-4"></div>
                            &nbsp;</div>--%>
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
                        <asp:BoundField DataField="centrocosto" HeaderText="IdCCosto" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="NombreCCosto" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noPeriodo" HeaderText="Perd" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="formaPago" HeaderText="fPago" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Observacion" HeaderText="Observación" ReadOnly="True" HtmlEncode="False" HtmlEncodeFormatString="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="liquidada" HeaderText="Liq">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="acumulada" HeaderText="Acum">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lNovedades" HeaderText="lNove">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lPresamo" HeaderText="lPres">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lHoras" HeaderText="lHoras">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lVacaciones" HeaderText="lVaca">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lPrimas" HeaderText="lPri">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lAusentismo" HeaderText="lAuse">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lEmbargo" HeaderText="lEmba">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lOtros" HeaderText="lOtr">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lNovedadesCredito" HeaderText="LNo">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lFondavi" HeaderText="LFon">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lDomingo" HeaderText="LDom">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lFestivo" HeaderText="LFes">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lDomingoCero" HeaderText="LDC">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mDomingo" HeaderText="MD">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lSindicato" HeaderText="lSin">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lDomingoPromedio" HeaderText="LDP">
                            <ItemStyle />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="lFestivoPromedio" HeaderText="LFP">
                            <ItemStyle />
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
