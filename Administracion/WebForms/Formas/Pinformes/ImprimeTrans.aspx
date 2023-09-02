<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeTrans.aspx.cs" Inherits="Administracion.WebForms.Formas.Pinformes.ImprimeTrans" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>


<body style="text-align: center">
    <div class="loading">
        <i class="fas fa-spinner fa-spin fa-5x"></i>
    </div>
    <div class="container">
        <form id="form1" runat="server">
            <table cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 250px; background-repeat: no-repeat; height: 25px; text-align: left"></td>
                    <td style="text-align: center;">Impresion de documentos</td>
                    <td style="width: 250px; background-repeat: no-repeat; height: 25px; text-align: left">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; background-repeat: no-repeat; height: 25px; text-align: left"></td>
                    <td style="text-align: center;">
                        <table style="width: 1000px">
                            <tr>
                                <td style="width: 200px; text-align: left;"></td>
                                <td style="width: 200px; text-align: left;">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo de Transacción"></asp:Label>
                                </td>
                                <td style="width: 500px; text-align: left;">
                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 200px; text-align: left;"></td>
                            </tr>
                            <tr>
                                <td style="width: 200px; text-align: left;"></td>
                                <td style="width: 200px; text-align: left;">
                                    <asp:Label ID="Label2" runat="server" Text="Número de Transacción"></asp:Label>
                                </td>
                                <td style="width: 500px; text-align: left;">
                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                    <asp:Button ID="imbBuscar" runat="server" CssClass="botones" OnClick="imbBuscar_Click" Text="Buscar" ToolTip="Imprimri el registro" Visible="True" />
                                </td>
                                <td style="width: 200px; text-align: left;"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 250px; background-repeat: no-repeat; height: 25px; text-align: left"></td>
                </tr>
            </table>

            <table style="width: 100%; height: 700px;" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 100%; text-align: left; height: 700px;">
                        <div style="font-display:auto; font-size:initial; padding:initial; margin:auto " >
                            <rsweb:ReportViewer ID="rvTransaccion" runat="server" ProcessingMode="Remote" Width="100%" Visible="False"></rsweb:ReportViewer>
                        </div>
                    </td>
                </tr>
            </table>
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
        </form>
    </div>


</body>
</html>
