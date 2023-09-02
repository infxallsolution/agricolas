<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preliquidar.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.Preliquidar" %>

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
</head>
<body style="text-align: center">
    <div class="container">
      <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <h4>Proceso de Pre-liquidación de nómina</h4>
            <hr />
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lbFecha" runat="server" Visible="False">Fecha transacción</asp:Label></td>
                    <td style="width:400px; text-align:left">
                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True"
                            Visible="False" CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td style="width:400px; text-align:left">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo Nomina" Visible="False"></asp:Label>
                    </td>
                    <td style="width:400px; text-align:left">
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblOpcionLiquidacion" runat="server" Text="Forma liquidación" Visible="False"></asp:Label>
                    </td>
                    <td style="width:400px; text-align:left" >
                        <asp:DropDownList ID="ddlOpcionLiquidacion" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" OnSelectedIndexChanged="ddlOpcionLiquidacion_SelectedIndexChanged">
                            <asp:ListItem Value="1">General</asp:ListItem>
                            <asp:ListItem Value="4">Por mayor centro costo</asp:ListItem>
                            <asp:ListItem Value="2">Por centro de costo</asp:ListItem>
                            <asp:ListItem Value="3">Por empleado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblCcosto" runat="server" Text="Centro costo" Visible="False"></asp:Label>
                    </td>
                    <td style="width:400px; text-align:left">
                        <asp:DropDownList ID="ddlccosto" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlccosto_SelectedIndexChanged" Visible="False" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td style="width:400px; text-align:left">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;"></td>
                    <td style="width:400px; text-align:left">
                        <asp:Button ID="lbPreLiquidar" runat="server" CssClass="botones" OnClick="lbPreLiquidar_Click" Text="Preliquidar" />
                    </td>
                    <td></td>
                </tr>
                </table>
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
