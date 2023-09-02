<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preliquidar.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Contabilizacion.Preliquidar" %>

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
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 250px;
            height: 200px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $.localise('ui-multiselect', { language: 'es', path: '../../js/locale/' });
            $(".multiselect").multiselect();
            // $('#switcher').themeswitcher();
        });

        function Visualizacion(informe, año, periodo, tipo) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&año=" + año + "&periodo=" + periodo + "&tipo=" + tipo;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }

        function VisualizacionResumen(informe, año, periodo, tipo) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&año=" + año + "&periodo=" + periodo + "&tipo=" + tipo;
            y = window.open(sTransaccion, "", opciones);
            y.focus();
        }

        function alerta(mensaje) {
            alert(mensaje);
        }
    </script>




    <%-- Este es el estilo de combobox --%>

    <link href="../../css/chosen.css" rel="stylesheet" />


    <%-- Aqui termina el estilo es el estilo de combobox --%>

    <script charset="utf-8" type="text/javascript">
        var contador = 0;
    </script>



</head>
<body style="text-align: center">
    <form id="form1" runat="server">
          <div class="loading" align="center">
            Procesando. Espere.<br />
            <br />
            <img alt="Cargando" src="../../Imagen/Utilidades/cargando.gif" width="150px" />
        </div>
        <div class="principal">
            <table cellspacing="0" style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;" id="Table2">
                <tr>
                    <td colspan="4" style="border-top-style: solid; border-top-width: 1px; border-color: silver">
                        <strong>Proceso de Pre-Contabilización</strong></td>
                </tr>
                <tr>
                    <td colspan="4" style="border-top-style: solid; border-top-width: 1px; border-color: silver">
                        <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos">
                        <asp:LinkButton ID="lbFecha" runat="server" OnClick="lbFecha_Click"
                            Visible="False" Style="color: #003366">Fecha transacción</asp:LinkButton></td>
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
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos">
                        <asp:Label ID="Label6" runat="server" Text="Tipo" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos">
                        <asp:DropDownList ID="ddlTipo" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select" ToolTip="Código nacional de ocupación" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                            <asp:ListItem Value="CA">Causación</asp:ListItem>
                            <asp:ListItem Value="PA">Pago</asp:ListItem>
                            <asp:ListItem Value="PR">Provisiones</asp:ListItem>
                            <asp:ListItem Value="SS">Seguridad Social</asp:ListItem>
                            <asp:ListItem Value="CC">Causación Contratista</asp:ListItem>
                            <asp:ListItem Value="PS">Prestaciones sociales</asp:ListItem>
                            <asp:ListItem Value="CI">Causacion incapacidades</asp:ListItem>
                            <asp:ListItem Value="SSC">Seguridad Social Contratista</asp:ListItem>
                            <asp:ListItem Value="PRC">Provisiones Contrartista</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos">
                        <asp:Label ID="lblaño" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo Nomina" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos">
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" colspan="2">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="500px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos"></td>
                    <td class="Campos">
                        <asp:ImageButton ID="lbPreLiquidar" runat="server" ImageUrl="~/Imagen/Bonotes/btnPreliquidar.png" OnClick="lbPreLiquidar_Click" OnClientClick="if(!confirm('Desea preliquidar ?')){return false;};" onmouseout="this.src='../../Imagen/Bonotes/btnPreliquidar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnPreliquidarN.png'" ToolTip="Preliquidar documento" Visible="False" />
                        </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 10px;" colspan="4">
                        <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 10px;" colspan="4">
                        </td>
                </tr>
            </table>



        </div>

    </form>
    <script src="../../js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</body>
</html>
