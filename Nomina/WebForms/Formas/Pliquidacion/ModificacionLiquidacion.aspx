<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificacionLiquidacion.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.ModificacionLiquidacion" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function Visualizacion(informe) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function VisualizacionLiquidacion(informe, ano, periodo, numero) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&ano=" + ano + "&periodo=" + periodo + "&numero=" + numero;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function VisualizacionContrato(informe, numero) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&numero=" + numero;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
    </script>
    <link href="../../css/chosen.css" rel="stylesheet" />
    <script charset="utf-8" type="text/javascript">
        var contador = 0;
    </script>



</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <h4>Modificación de documento de nómina</h4>
            <hr />
            <table class="w-100" id="Table2">
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px;">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 150px;">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo Nomina"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px;">
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lblEmpleado1" runat="server" Text="Tipo documento"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px;">
                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 150px;">
                        <asp:Label ID="lblEmpleado0" runat="server" Text="Número documento"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px;">
                        <asp:DropDownList ID="ddlNumeroDocumento" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" AutoPostBack="true" OnSelectedIndexChanged="ddlNumeroDocumento_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px;">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="lblEmpleado2" runat="server" Text="Contrato"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlContratos" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" AutoPostBack="true" OnSelectedIndexChanged="ddlContratos_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lblConcepto" runat="server" Text="Concepto"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px;">
                        <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%"></asp:DropDownList>
                    </td>
                    <td colspan="2" style="text-align: left;"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="detailLoadedPanel">
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
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvDetalleLiquidacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" RowHeaderColumn="cuenta" OnRowDeleting="gvDetalleLiquidacion_RowDeleting" Width="100%">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="registroDetalleNomina" HeaderText="Reg.">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codConcepto" HeaderText="Concepto">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcionConcepto" HeaderText="NombreConcepto">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvCantidad" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Enabled='<%#!(bool)Eval("ValidaPorcentaje")%>' Text='<%#Eval("cantidad") %>' Width="80%">0</asp:TextBox>
                                <asp:HiddenField ID="cantidad" runat="server" ClientIDMode="Static" Value='<%#Eval("cantidad")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ValorUnitario" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvValorUnitario" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Enabled='<%#!(bool)Eval("ValidaPorcentaje") && !(bool)Eval("HabilitaValorTotal")%>' Text='<%#Eval("valorUnitario") %>' Width="80%">0</asp:TextBox>
                                <asp:HiddenField ID="valorUnitario" runat="server" ClientIDMode="Static" Value='<%#Eval("valorUnitario")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ValorTotal" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvValorTotal" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Enabled='<%#!(bool)Eval("ValidaPorcentaje") && (bool)Eval("HabilitaValorTotal")%>' Text='<%#Eval("valorTotal") %>' Width="80%">0</asp:TextBox>
                                <asp:HiddenField ID="valorTotal" runat="server" ClientIDMode="Static" Value='<%#Eval("valorTotal")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BaseSS" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBaseSeguridadSocial" runat="server" ClientIDMode="Static" Enabled="false" Checked='<%#Eval("BaseSeguridadSocial") %>' />
                                <asp:HiddenField ID="BaseSeguridadSocial" runat="server" ClientIDMode="Static" Value='<%#Eval("BaseSeguridadSocial")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Porc." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkValidaPorcentaje" runat="server" ClientIDMode="Static" Enabled="false" Checked='<%#Eval("ValidaPorcentaje") %>' />
                                <asp:HiddenField ID="ValidaPorcentaje" runat="server" ClientIDMode="Static" Value='<%#Eval("ValidaPorcentaje")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Porcentaje" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvPorcentaje" runat="server" ClientIDMode="Static" CssClass="input" Enabled="false" Text='<%#((bool)Eval("ValidaPorcentaje"))?Eval("Porcentaje"): ""%>' Width="40px">0</asp:TextBox>%
                                                <asp:HiddenField ID="Porcentaje" runat="server" ClientIDMode="Static" Value='<%#Eval("Porcentaje")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deducc." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDeduccion" runat="server" ClientIDMode="Static" Enabled="false" Checked='<%#Eval("Deduccion") %>' />
                                <asp:HiddenField ID="Deduccion" runat="server" ClientIDMode="Static" Value='<%#Eval("Deduccion")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HabTot" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkHabilitaValorTotal" runat="server" ClientIDMode="Static" Enabled="false" Checked='<%#Eval("HabilitaValorTotal") %>' />
                                <asp:HiddenField ID="HabilitaValorTotal" runat="server" ClientIDMode="Static" Value='<%#Eval("HabilitaValorTotal")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saldo" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txvSaldo" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("saldo") %>' Width="80%">0</asp:TextBox>
                                <asp:HiddenField ID="saldo" runat="server" ClientIDMode="Static" Value='<%#Eval("saldo")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="Items" Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
                <div style="text-align: right;">
                    <asp:Label ID="lblTotal" Text="Total Liquidación: " runat="server"></asp:Label><asp:TextBox ID="txtTotal" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Enabled="false"></asp:TextBox>
                </div>
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
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/js/Liquidación/ModificacionLiquidacion.js?v=20170710"></script>
</body>
</html>
