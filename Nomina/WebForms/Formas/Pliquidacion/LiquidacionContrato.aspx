<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiquidacionContrato.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.LiquidacionContrato" %>

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
            <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operaciÃ³n" Visible="False" />
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lbFecha" runat="server" Visible="False">Fecha</asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px">
                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True"
                            Visible="False" CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged" Width="80%"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <asp:CheckBox ID="chkLiquiNomina" runat="server" CssClass="checkbox checkbox-primary" Text="Liquida Conceptos de periodo de nomina" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblEmpleado0" runat="server" Text="Contrato" Visible="False"></asp:Label>
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:DropDownList ID="ddlContratos" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 150px;">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="97%" AutoPostBack="True" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 150px;">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo Nomina" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 350px;">
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                     <td></td>
                    <td style="text-align: left;" colspan="4">
                        <asp:TextBox ID="txtObservacion" runat="server" placeholder="Observaciones, notas o comentarios..." CssClass="input" Height="40px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblConcepto" runat="server" Text="Concepto" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" colspan="3">
                        <table class="ui-accordion">
                            <tr>
                                <td style="width: 350px">
                                    <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="320px" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 180px">&nbsp;</td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="4" style="text-align: center;">
                        <table class="ui-accordion">
                            <tr>
                                <td style="text-align: center;" colspan="3">
                                    <table class="ui-accordion">
                                        <tr>
                                            <td style="text-align: left; width: 120px;">
                                                <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 120px;">
                                                <asp:TextBox ID="txvCantidad" runat="server" CssClass="input" Visible="False" onkeyup="totalTotales(this)" Width="80px">0</asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 100px;">
                                                <asp:Label ID="lblValorUnitario" runat="server" Text="Valor Unitario" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px;">
                                                <asp:TextBox ID="txvValorUnittario" runat="server" CssClass="input" Visible="False" onkeyup="totalTotales(this)" Width="150px">0</asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 100px;">
                                                <asp:Label ID="lblValorTotal" runat="server" Text="Valor Total" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txvValorTotal" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" Width="150px">0</asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" colspan="4">
                                    <asp:Button ID="lbPreLiquidar" runat="server" CssClass="botones" OnClick="lbPreLiquidar_Click" Text="Preliquidar" ToolTip="Habilita el formulario para un nuevo registro" Visible="False" />
                                    <asp:Button ID="btnLiquidar" runat="server" CssClass="botones" OnClick="btnLiquidar_Click" OnClientClick="return confirmSwal(this,'Advertencia','Â¿Esta seguro de liquidar?');" Text="Liquidar contrato" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                                    <asp:Button ID="btnCargar" runat="server" OnClick="btnCargar_Click" CssClass="botones" ToolTip="Cargar concepto" Visible="False" Text="Cargar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="4" style="text-align: center;">
                        <asp:GridView ID="gvDetalleLiquidacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" RowHeaderColumn="cuenta" OnRowDeleting="gvSaludPension_RowDeleting" Width="100%">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                    <HeaderStyle CssClass="action-item" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codConcepto" HeaderText="Concepto">
                                    <HeaderStyle />
                                    <ItemStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="desConcepto" HeaderText="NombreConcepto">
                                    <HeaderStyle />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cantidad">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorUnitario" HeaderText="ValUni(Base)" DataFormatString="{0:N2}">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorTotal" HeaderText="Val Total" DataFormatString="{0:N2}">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="signo" HeaderText="Signo">
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="prestacionSocial" HeaderText="PS">
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />

            <div class="tablaGrilla">
               <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                            <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero" HeaderText="Número" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="noPeriodo" HeaderText="Periodo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Observacion" HeaderText="Observación" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="anulado" HeaderText="Anulado">
                                <HeaderStyle  />
                                <ItemStyle  Width="10px" />
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

    <script type="text/javascript">
        function totalTotales(entrada) {
            if (window.event.keyCode !== 9) {
                formato_numero(entrada);
                var total;
                var cantidad = document.getElementById('txvCantidad');
                var valorUnitario = document.getElementById('txvValorUnittario');
                var valorTotal = document.getElementById('txvValorTotal');

                if (cantidad !== null & valorUnitario !== null) {
                    //console.log(cantidad.value);
                    total = parseFloat(cantidad.value.replace(/\,/g, '')) * parseFloat(valorUnitario.value.replace(/\,/g, ''));
                    valorTotal.value = new Intl.NumberFormat('es-US').format(total);
                    hdTotal = document.getElementById("hdTotal");
                    hdTotal.value = new Intl.NumberFormat('es-US').format(total);
                }
            }

        }

    </script>

</body>
</html>
