<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiquidarPrecios.aspx.cs" Inherits="Agronomico.WebForms.Formas.Padministracion.LiquidarPrecios" %>

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

            <hr />
            <h4>Proceso de Re-liquidación de precios de labores</h4>
              <hr />
            <table style="width: 100%" >
                <tr>

                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lbFechaInicial" runat="server" >Fecha inicio</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaInicio" runat="server" AutoPostBack="True" CssClass="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)"  Width="150px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lbFechaFinal" runat="server" >Fecha final</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)"  Width="150px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">&nbsp;</td>
                    <td style="text-align: left; width: 400px">
                        <asp:Button ID="btnLiquidar" runat="server" CssClass="botones" OnClick="btnLiquidar_Click" Text="Re-liquidar" ToolTip="Haga clic aqui para realizar la liquidación" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de liquidar los registros?');"  />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 10px;" colspan="4">
                        <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 10px;" colspan="4"></td>
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
