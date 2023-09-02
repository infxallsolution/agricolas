<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeCarnet.aspx.cs" Inherits="Administracion.WebForms.Formas.Pinformes.ImprimeCarnet" %>


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
            <table cellspacing="0" width:"100%" width="100%">
                <tr>
                    <td style="height: 15px;" colspan="4">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:Label ID="nilblInformacion" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left" width="150px" >
                        <asp:Label ID="lblEmpleado" runat="server" Text="Funcionario"></asp:Label>
                    </td>
                    <td class="text-left" width="350px" >
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td>
                       </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left">
                        <asp:Label ID="Label2" runat="server" Text="Plantilla"></asp:Label></td>
                    <td class="text-left">
                        <asp:DropDownList ID="ddlPlantilla" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="300px">
                            <asp:ListItem Value="palmaceite">Palmaceite S.A.</asp:ListItem>
                            <asp:ListItem Value="aceites">Aceites S.A.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="nombreCampos"></td>
                    <td class="Campos">
                     <asp:Button ID="imbImprimir" runat="server" CssClass="botones" OnClick="imbImprimir_Click"  Text="Buscar" ToolTip="Imprimri el registro" Visible="True" />
                      
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px;" colspan="4">
                        <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="tablaGrilla">
                <div style="display: inline-block">
                    <table  class="auto-style1">
                        <tr>
                            <td></td>
                            <td>
                                <rsweb:ReportViewer ID="rptCarnet" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    Height="594px" ProcessingMode="Remote" ShowBackButton="True" ShowFindControls="False"
                                    ShowPageNavigationControls="False" ShowZoomControl="False"  Width="380px">
                                </rsweb:ReportViewer>


                            </td>
                            <td></td>
                            <td>
                                <rsweb:ReportViewer ID="rptCarnet1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    Height="594px" ProcessingMode="Remote" ShowBackButton="True" ShowFindControls="False"
                                    ShowPageNavigationControls="False" ShowZoomControl="False"  Width="380px">
                                </rsweb:ReportViewer>

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </form>
    </div>
</body>
     <script src="http://app.infos.com/recursosinfos/lib/chosen-js/chosen.jquery.js" type="text/javascript"></script> <script src="http://app.infos.com/recursosinfos/lib/promise-polyfill/promise.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/chosen-js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/lou-multi-select/js/jquery.multi-select.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/moment/moment.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/js/Core/core.js"></script>
</html>
