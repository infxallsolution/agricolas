<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeInforme.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.ImprimeInforme" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jqu<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>ery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%">
    
            <table  style="width: 100%">
                <tr>
                    <td style=" width: 250px;
                        background-repeat: no-repeat; height: 25px; text-align: left">
                        </td>
                    <td style="text-align: center; ">
                        <strong style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 12px; color: #003366; text-align: center;">Visualización Informes</strong></td>
                    <td style="width: 250px; background-repeat: no-repeat; height: 25px; text-align: left">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        </td>
                </tr>
            </table>

        <table style="width: 100%; height: 700px;" >
            <tr>
                <td style="vertical-align: top; width: 100%; text-align: left; height: 700px;">
                    
                    <rsweb:ReportViewer ID="rvImprimir" runat="server" Width="100%" Height="100%" ProcessingMode="Remote">
                    </rsweb:ReportViewer>
                    
                </td>
            </tr>
        </table>

        </div>     
    </form>
</body>
</html>
