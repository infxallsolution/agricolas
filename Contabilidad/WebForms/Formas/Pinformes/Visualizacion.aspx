<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visualizacion.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Pinformes.Visualizacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/Formularios.css" rel="stylesheet" />
    <title></title>

    <script language="javascript" type="text/javascript">

        var x = null;

        function Visualizacion(informe) {

            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="principal">
            <table style="width: 1000px" >
                <tr>
                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;"></td>
                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;"><strong>Visualización</strong></td>
                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;"></td>
                </tr>
            </table>
             <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
            <table style="width: 1000px" >
                <tr>
                    <td style="background-image: none; vertical-align: top; width: 300px; height: 70px; background-color: transparent; text-align: left">
                        <asp:TreeView ID="tvInformes" runat="server" ForeColor="#404040" ImageSet="WindowsHelp" NodeWrap="True" PopulateNodesFromClient="False" OnSelectedNodeChanged="tvInformes_SelectedNodeChanged" Width="400px">
                            <ParentNodeStyle Font-Bold="False" />
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" />
                            <Nodes>
                                <asp:TreeNode Expanded="True" SelectAction="Expand" Text="Contabilidad" Value="Contable">
                                    <asp:TreeNode Text="Terceros" Value="Conta01"></asp:TreeNode>
                                    <asp:TreeNode Text="Puc" Value="Conta02"></asp:TreeNode>
                                    <asp:TreeNode Text="Párametros de contabilización" Value="Conta03"></asp:TreeNode>
                                </asp:TreeNode>
                            </Nodes>
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                NodeSpacing="0px" VerticalPadding="1px" />
                        </asp:TreeView>
                    </td>
                    <td style="background-image: none; width: 700px; background-color: transparent; text-align: left; vertical-align: top; height: 70px;"></td>
                </tr>
            </table>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        </div>
    </form>
</body>
</html>
