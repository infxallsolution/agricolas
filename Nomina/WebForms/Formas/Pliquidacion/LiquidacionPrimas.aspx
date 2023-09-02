<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiquidacionPrimas.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.LiquidacionPrimas" %>

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
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&ano=" + ano + "&numero=" + numero;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function alerta(mensaje) {
            alert(mensaje);
        }
    </script>

</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input">
                        </asp:TextBox>
                    </td>
                    <td class="bordesBusqueda"></td>
                </tr>
            </table>
            <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
            <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
            <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lbFecha" runat="server" Visible="False">Fecha</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True"
                            Visible="False" CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left;"></td>
                    <td style="width: 400px; text-align: left;"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td style="width: 130px; text-align: left;" class="auto-style2">
                        <asp:Label ID="lblaño" runat="server" Text="Año primas desde" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAñoDesde" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left;" class="auto-style4">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Desde perido" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;" class="auto-style5">
                        <asp:DropDownList ID="ddlPeriodoDesde" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1"></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblaño1" runat="server" Text="Año primas hasta" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAñoHasta" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAñoHasta_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblPeriodo1" runat="server" Text="Hasta perido" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlPeriodoHasta" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left;" colspan="2">
                        <asp:CheckBox ID="chkPagaNomina" runat="server" Text="Paga primas en nomina?" AutoPostBack="True" OnCheckedChanged="chkPagaNomina_CheckedChanged" Visible="False" CssClass="checkbox checkbox-primary" />
                    </td>
                    <td style="width: 130px; text-align: left;"></td>
                    <td style="width: 400px; text-align: left;"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblañoPago" runat="server" Text="Año pago primas" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAñoPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAñoPago_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblPeriodoPago" runat="server" Text="Periodo pago primas" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlPeriodoPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblOpcionLiquidacion" runat="server" Text="Forma liquidación" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlOpcionLiquidacion" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%" OnSelectedIndexChanged="ddlOpcionLiquidacion_SelectedIndexChanged">
                            <asp:ListItem Value="1">General</asp:ListItem>
                            <asp:ListItem Value="4">Por mayor centro costo</asp:ListItem>
                            <asp:ListItem Value="2">Por centro de costo</asp:ListItem>
                            <asp:ListItem Value="3">Por empleado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left;">&nbsp;</td>
                    <td style="width: 400px; text-align: left;">&nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblCcosto" runat="server" Text="Centro costo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlccosto" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlccosto_SelectedIndexChanged" Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left;" colspan="4">
                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" placeholder="Observaciones, notas o comentarios..." TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="4" style="text-align: center;">
                        <asp:Button ID="lbPreLiquidar" runat="server" CssClass="botones" OnClick="lbPreLiquidar_Click" Text="Preliquidar" ToolTip="Habilita el formulario para un nuevo registro" Visible="False" />
                        <asp:Button ID="btnLiquidar" runat="server" CssClass="botones" OnClick="btnLiquidar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de liquidar?');" Text="Liquidar definitivo" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                    <td></td>
                </tr>

            </table>
            <table style="width: 100%" id="tdCamposEditar">
                <tr>
                    <td colspan="6" style="text-align: center">
                        <table runat="server" id="tModifica" visible="false" class="w-100">
                            <tr>
                                <td colspan="6">
                                    <table style="width: 100%;" id="Table3">
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                                            </td>
                                            <td class="Campos">
                                                <asp:TextBox ID="txtFechaDetalle" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 120px; text-align: left;"></td>
                                            <td style="width: 300px; text-align: left;"></td>
                                            <td style="width: 80px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                                            </td>
                                            <td class="Campos">
                                                <asp:TextBox ID="txtTipo" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblNumero" runat="server" Text="Numero"></asp:Label>
                                            </td>
                                            <td class="Campos">
                                                <asp:TextBox ID="txtNumero" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 80px"></td>
                                        </tr>

                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblaño2" runat="server" Text="Año primas desde"></asp:Label>
                                            </td>
                                            <td class="Campos">
                                                <asp:TextBox ID="txtAñoDesde" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblPeriodo2" runat="server" Text="Desde perido"></asp:Label>
                                            </td>
                                            <td style="width: 300px; text-align: left;">
                                                <asp:TextBox ID="txtPeriodoDesde" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 80px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblaño3" runat="server" Text="Año primas hasta"></asp:Label>
                                            </td>
                                            <td class="Campos">
                                                <asp:TextBox ID="txtAñoHasta" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblPeriodo3" runat="server" Text="Hasta perido"></asp:Label>
                                            </td>
                                            <td style="width: 300px; text-align: left;">
                                                <asp:TextBox ID="txtPeriodoHasta" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 80px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblañoPago0" runat="server" Text="Año pago primas"></asp:Label>
                                            </td>
                                            <td class="Campos">
                                                <asp:TextBox ID="txtAñoPago" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblPeriodoPago0" runat="server" Text="Periodo pago primas"></asp:Label>
                                            </td>
                                            <td style="width: 300px; text-align: left;">
                                                <asp:TextBox ID="txtPeriodoPago" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 80px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:Label ID="lblObservacion0" runat="server" Text="Observación"></asp:Label>
                                            </td>
                                            <td class="Campos" colspan="3">
                                                <asp:TextBox ID="txtObservacionEdita" runat="server" CssClass="input" Height="40px" TextMode="MultiLine" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width: 80px"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%"></td>
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
                                <td style="width: 10%"></td>
                            </tr>
                            <tr>
                                <td style="width: 10%"></td>
                                <td style="width: 120px; text-align: left;">
                                    <asp:Label ID="lblFechaInicio" runat="server">Fecha Inicio</asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtFechaInicio" runat="server" Font-Bold="True"
                                        CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFechaInicio_TextChanged"></asp:TextBox>
                                </td>
                                <td style="width: 120px; text-align: left;">
                                    <asp:Label ID="lblFechaFin" runat="server">Fecha Final</asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtFechaFin" runat="server" Font-Bold="True"
                                        CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFechaFin_TextChanged"></asp:TextBox>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr>
                                <td style="width: 10%"></td>
                                <td style="text-align: center;" colspan="4">
                                    <asp:Button ID="btnCargar" runat="server" CssClass="botones" OnClick="btnCargar_Click" Text="Cargar" ToolTip="Cargar conceptos" />
                                    <asp:Button ID="Button1" runat="server" CssClass="botones" Text="Cancelar" ToolTip="Cancela la operación" OnClick="lbCancelar_Click" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="btnGuardar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guardar cambios" />
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:GridView ID="gvDetalleLiquidacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" RowHeaderColumn="cuenta" Width="100%" OnRowDeleting="gvDetalleLiquidacion_RowDeleting">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="imElimina0" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
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
                                                    <asp:TextBox ID="txvBase" runat="server" ClientIDMode="Static" CssClass="input numeric-field" Text='<%#Eval("Base") %>'  Width="80%">0</asp:TextBox>
                                                    <asp:HiddenField ID="hfBase" runat="server" ClientIDMode="Static" Value='<%#Eval("ValorPrima") %>' />
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
                                                    <asp:TextBox ID="txvValorPrima" runat="server" ClientIDMode="Static" CssClass="input numeric-field"  Text='<%#Eval("ValorPrima") %>' Width="80%">0</asp:TextBox>
                                                     <asp:HiddenField ID="hfValorPrima" runat="server" ClientIDMode="Static" Value='<%#Eval("ValorPrima") %>' />
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

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <hr />

            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" AllowPaging="True" OnRowEditing="gvLista_RowEditing">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imEdit" ControlStyle-CssClass="btn btn-default btn-sm btn-primary fa fa-pencil-alt" CommandName="Edit" ToolTip="Edita el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero" HeaderText="Número" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodo" HeaderText="Periodo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Observacion" HeaderText="Observación" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="anulado" HeaderText="Anulado">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
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
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/js/Liquidación/ModificacionPrima.js?v=20170710"></script>

</body>
</html>
