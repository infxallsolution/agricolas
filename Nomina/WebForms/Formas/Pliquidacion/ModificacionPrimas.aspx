<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificacionPrimas.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.ModificacionPrimas" %>

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
            <h4>Modificación de liquidación de primas</h4>
            <hr />
            <table class="w-100" id="Table2">
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 400px; text-align: left;"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtTipo" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblNumero" runat="server" Text="Numero"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año primas desde"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtAñoDesde" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Desde perido"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txtPeriodoDesde" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblaño1" runat="server" Text="Año primas hasta"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtAñoHasta" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblPeriodo1" runat="server" Text="Hasta perido"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txtPeriodoHasta" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblañoPago" runat="server" Text="Año pago primas"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtAñoPago" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblPeriodoPago" runat="server" Text="Periodo pago primas"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txtPeriodoPago" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left;" colspan="4">
                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" placeholder="Observaciones, notas o comentarios..." TextMode="MultiLine" Enabled="false" Width="100%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <h6>Registrar tercero</h6>
            <hr />
            <table style="width: 100%;">
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblTercero" runat="server" Text="Tercero"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlTercero" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%"></asp:DropDownList>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblFechaIngreso" runat="server" OnClick="lblFechaIngreso_Click">Fecha Ingreso</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaIngreso" runat="server" Font-Bold="True"
                            CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFechaIngreso_TextChanged"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblFechaInicio" runat="server">Fecha Inicio</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaInicio" runat="server" Font-Bold="True"
                            CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFechaInicio_TextChanged"></asp:TextBox>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblFechaFin" runat="server">Fecha Fin</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaFin" runat="server" Font-Bold="True"
                            CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFechaFin_TextChanged"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: center;" colspan="4">
                        <asp:Button ID="btnCargar" runat="server" CssClass="botones" OnClick="btnCargar_Click" Text="Cargar" ToolTip="Cargar conceptos" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" Text="Cancelar" ToolTip="Cancela la operación" OnClick="lbCancelar_Click" />
                        <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="btnGuardar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guardar cambios" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla" id="detalleLiqidacion" runat="server">
                <asp:GridView ID="gvDetalleLiquidacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" RowHeaderColumn="cuenta" Width="100%" OnRowDeleting="gvDetalleLiquidacion_RowDeleting">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CodigoTercero" HeaderText="Cod.">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdentificacionTercero" HeaderText="Indent.">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreTercero" HeaderText="Nombre">
                            <HeaderStyle />
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaIngreso" HeaderText="FechaIngreso">
                            <HeaderStyle />
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaInicial" HeaderText="FechaInicial">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaFinal" HeaderText="FechaFinal">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Basico" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvBasico" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("Basico") %>' Width="80%">0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transporte" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvTransporte" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("Transporte") %>' Width="80%">0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Promedio" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvValorPromedio" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("ValorPromedio") %>' Width="80%">0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dias Promedio" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvDiasPromedio" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("DiasPromedio") %>' Width="80%">0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Base" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvBase" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("Base") %>' Enabled="false" Width="80%">0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dias Prima" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvDiasPrima" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("DiasPrimas") %>' Width="80%">0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Prima" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvValorPrima" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Enabled="false" Text='<%#Eval("ValorPrima") %>' Width="80%">0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="90px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="contrato" HeaderText="Cont">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
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
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/js/Liquidación/ModificacionPrima.js?v=20170710"></script>
</body>
</html>
