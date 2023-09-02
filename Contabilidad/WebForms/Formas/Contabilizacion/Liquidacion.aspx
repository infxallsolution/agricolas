<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Liquidacion.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Contabilizacion.Liquidacion" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link rel="stylesheet" href="../../css/common.css" type="text/css" />
    <link href="../../css/Formularios.css" rel="stylesheet" />
    <script src="../../js/Numero.js" type="text/javascript"></script>
    <link href="../../css/jquery-ui.css" rel="stylesheet" />
    <link type="text/css" href="../../css/ui.multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/jqueryv1.5.1.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-ui1.8.10.min.js"></script>
    <script type="text/javascript" src="../../js/plugins/localisation/jquery.localisation-min.js"></script>
    <script type="text/javascript" src="../../js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
    <script type="text/javascript" src="../../js/ui.multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $.localise('ui-multiselect', { language: 'es', path: '../../js/locale/' });
            $(".multiselect").multiselect();
        });

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
    <link href="../../css/chosen.css" rel="stylesheet" />
    <script charset="utf-8" type="text/javascript">
        var contador = 0;
    </script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div class="principal">
            <table cellspacing="0" style="width: 1000px">
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos">Busqueda</td>
                    <td class="Campos">
                        <asp:TextBox ID="nitxtBusqueda" runat="server" CssClass="input" ToolTip="Escriba el texto para la busqueda" Width="350px"></asp:TextBox>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" OnClick="niimbBuscar_Click" onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" OnClick="lbNuevo_Click" onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" OnClick="lbCancelar_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" ToolTip="Cancela la operación" Visible="False" />
                    </td>
                </tr>
            </table>
            <table cellspacing="0" style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;" id="Table2">
                <tr>
                    <td colspan="6" style="border-top-style: solid; border-top-width: 1px; border-color: silver">
                        <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px"></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:LinkButton ID="lbFecha" runat="server" OnClick="lbFecha_Click"
                            Visible="False" Style="color: #003366">Fecha Contabilización</asp:LinkButton></td>
                    <td class="Campos">
                        <asp:Calendar ID="niCalendarFecha" runat="server" BackColor="White" BorderColor="#999999"
                            CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                            ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFecha_SelectionChanged"
                            Visible="False" Width="150px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="FloralWhite" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" ForeColor="Gray"
                            Visible="False" CssClass="input" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="Label6" runat="server" Text="Tipo" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px; text-align: left;">
                        <asp:DropDownList ID="ddlTipo" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                            <asp:ListItem Value="CA">Causación</asp:ListItem>
                            <asp:ListItem Value="PA">Pago</asp:ListItem>
                            <asp:ListItem Value="PR">Provisión Prestaciones</asp:ListItem>
                            <asp:ListItem Value="SS">Seguridad Social</asp:ListItem>
                            <asp:ListItem Value="CC">Causación Contratistas</asp:ListItem>
                            <asp:ListItem Value="PS">Liquidación de prestaciones</asp:ListItem>
                            <asp:ListItem Value="IN">Incapacidades</asp:ListItem>
                            <asp:ListItem Value="SSC">Seguridad Social Contratista</asp:ListItem>
                            <asp:ListItem Value="PRC">Provisiones Contrartista</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 80px"></td>
                </tr>
                <tr>
                    <td style="width: 80px"></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo Nomina" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 300px; text-align: left;">
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="320px" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 80px"></td>
                </tr>
                <tr>
                    <td style="width: 80px"></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" colspan="3">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="600px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 80px"></td>
                </tr>
                <tr>
                    <td style="width: 80px"></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblObservacion" runat="server" Text="Observación" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" colspan="3">
                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" TextMode="MultiLine" Visible="False" Width="700px"></asp:TextBox>
                    </td>
                    <td style="width: 80px"></td>
                </tr>
                <tr>
                    <td style="width: 80px"></td>
                    <td colspan="4">
                        <asp:ImageButton ID="btnLiquidar" runat="server" ImageUrl="~/Imagen/Bonotes/btnLiquidar.png" OnClick="btnLiquidar_Click" OnClientClick="if(!confirm('Desea liquidar el documento ?')){return false;};" onmouseout="this.src='../../Imagen/Bonotes/btnLiquidar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnLiquidarN.png'" ToolTip="Preliquidar documento" Visible="False" />

                    </td>
                    <td style="width: 80px"></td>
                </tr>
                <tr>

                    <td colspan="6" style="text-align: center"></td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 10px;" colspan="6">
                        <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                <div style="display: inline-block">
                    <asp:GridView ID="gvLista" runat="server" Width="1000px" GridLines="None" CssClass="Grid" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" OnPageIndexChanging="gvLista_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:TemplateField HeaderText="Anul">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imElimina" runat="server" CommandName="Delete" ImageUrl="~/Imagen/TabsIcon/cancel.png" OnClientClick="if(!confirm('Desea eliminar el registro ?')){return false;};" ToolTip="Elimina el registro seleccionado" />
                                </ItemTemplate>
                                <HeaderStyle BackColor="White" />
                                <ItemStyle Width="20px" CssClass="Items" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipoLiquidacion" HeaderText="TipoL">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero" HeaderText="Número" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="periodoContable" HeaderText="Periodo Cont." ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="periodoNomina" HeaderText="Periodo Nomi.">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Observacion" HeaderText="Observación" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="anulado" HeaderText="Anulado">
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="10px" />
                            </asp:CheckBoxField>
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <RowStyle CssClass="rw" />
                    </asp:GridView>
                </div>

            </div>



        </div>

    </form>
    <script src="../../js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</body>
</html>
