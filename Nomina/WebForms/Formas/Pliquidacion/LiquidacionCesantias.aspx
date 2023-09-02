<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiquidacionCesantias.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.LiquidacionCesantias" %>

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
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año Cesantias" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAñoDesde" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="130px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <asp:CheckBox ID="chkPagaNomina" runat="server" Text="Paga cesantias en nomina?" Visible="False" CssClass="checkbox checkbox-primary" />
                        <asp:CheckBox ID="chkSueldoActual" runat="server" Text="Sueldo actual o anterior?" Visible="False" CssClass="checkbox checkbox-primary" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblañoPago" runat="server" Text="Año pago" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAñoPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAñoPago_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblPeriodoPago" runat="server" Text="Periodo pago " Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px; text-align: left;">
                        <asp:DropDownList ID="ddlPeriodoPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblOpcionLiquidacion" runat="server" Text="Forma liquidación" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlOpcionLiquidacion" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%" OnSelectedIndexChanged="ddlOpcionLiquidacion_SelectedIndexChanged">
                            <asp:ListItem Value="1">General</asp:ListItem>
                            <asp:ListItem Value="4">Por mayor centro costo</asp:ListItem>
                            <asp:ListItem Value="2">Por centro de costo</asp:ListItem>
                            <asp:ListItem Value="3">Por empleado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 300px; text-align: left;"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblCcosto" runat="server" Text="Centro costo" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlccosto" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlccosto_SelectedIndexChanged" Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px; text-align: left;">
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
                            <asp:BoundField DataField="periodo" HeaderText="Periodo" ReadOnly="True">
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

</body>
</html>
