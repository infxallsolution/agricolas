<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visualizacion.aspx.cs" Inherits="Agronomico.WebForms.Formas.Pinformes.Visualizacion" %>

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
            <table style="width: 1000px">
                <tr>
                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;"></td>
                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;"><strong>Visualización</strong></td>
                    <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;"></td>
                </tr>
            </table>
            <table style="width: 1000px">
                <tr>
                    <td style="background-image: none; vertical-align: top; width: 300px; height: 70px; background-color: transparent; text-align: left">
                        <asp:TreeView ID="tvInformes" runat="server" ForeColor="#404040" ImageSet="WindowsHelp" NodeWrap="True" PopulateNodesFromClient="False" OnSelectedNodeChanged="tvInformes_SelectedNodeChanged">
                            <ParentNodeStyle Font-Bold="False" />
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" BackColor="#B5B5B5" />
                            <Nodes>
                                <asp:TreeNode Expanded="True" SelectAction="Expand" Text="Administración" Value="Contable">
                                    <asp:TreeNode Text="Lotes por fincas" Value="lote01"></asp:TreeNode>
                                    <asp:TreeNode Text="Lotes por sec/bloq" Value="admon01"></asp:TreeNode>
                                    <asp:TreeNode Text="Lotes por variedad" Value="admon07"></asp:TreeNode>
                                    <asp:TreeNode Text="Lotes por año siembra" Value="admon08"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores" Value="admon03"></asp:TreeNode>
                                    <asp:TreeNode Text="Peso promedio racimos por lote" Value="admon02"></asp:TreeNode>
                                    <asp:TreeNode Text="Lista de precio novedades" Value="admon04"></asp:TreeNode>
                                    <asp:TreeNode Text="Finca - Lote metros de canal" Value="admon05"></asp:TreeNode>
                                    <asp:TreeNode Text="Lote linea palmas" Value="admon06"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Expanded="True" SelectAction="Expand" Text="Revisión Labores" Value="Contable">
                                    <asp:TreeNode Text="Labores tercero por fecha" Value="revi01"></asp:TreeNode>
                                    <%--<asp:TreeNode Text="Labores tercero por fecha - finca" Value="prod02"></asp:TreeNode>--%>
                                    <asp:TreeNode Text="Labores tercero detallada por fecha" Value="revi02"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores detallada por fecha por novedad" Value="revi03"></asp:TreeNode>
                                    <%--<asp:TreeNode Text="Cargadores por finca y terceros" Value="tra019"></asp:TreeNode>
                                    <asp:TreeNode Text="Cargadores por finca - sección y terceros" Value="tra020"></asp:TreeNode>
                                    <asp:TreeNode Text="Relación para control de corte" Value="tra021"></asp:TreeNode>--%>
                                    <%--<asp:TreeNode Text="Producción de corte por finca" Value="prod01"></asp:TreeNode>--%>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Transacciones" Value="Transacciones">
                                    <asp:TreeNode Text="Labores por fecha" Value="tran01"></asp:TreeNode>
                                    <asp:TreeNode Text="Registro tiquetes por fecha" Value="tran02"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores por trabajador en fechas" Value="tran03"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores por lote en fecha" Value="tran06"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores por centro de costo en fecha" Value="tran04"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores por centor de costo en fecha con lote" Value="tran19"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores detalle" Value="tran05"></asp:TreeNode>
                                    <asp:TreeNode Text="Producción lote fecha" Value="tran07"></asp:TreeNode>
                                    <asp:TreeNode Text="Producción lote Anual" Value="tran13"></asp:TreeNode>
                                    <asp:TreeNode Text="Venta de fruta por extractora" Value="tran08"></asp:TreeNode>
                                    <asp:TreeNode Text="Liquidación contratistas por periodo" Value="tran09"></asp:TreeNode>
                                    <asp:TreeNode Text="Liquidación contratistas por periodo y lotes" Value="tran10"></asp:TreeNode>
                                    <asp:TreeNode Text="Resumen contratistas por periodo" Value="tran11"></asp:TreeNode>
                                    <asp:TreeNode Text="Hoja de vida de Labores" Value="tran12"></asp:TreeNode>
                                    <asp:TreeNode Text="Tiquetes pendientes por registrar" Value="tran15"></asp:TreeNode>
                                    <asp:TreeNode Text="Hoja de Vida Lote Novedad y fecha" Value="tran18"></asp:TreeNode>
                                    <asp:TreeNode Text="Diferencia días cosecha y recepción" Value="tra001"></asp:TreeNode>
                                </asp:TreeNode>

                                <asp:TreeNode Text="Indicadores " Value="Indicadores ">
                                     <asp:TreeNode Text="Indicadores Año" Value="tran16"></asp:TreeNode>
                                    <asp:TreeNode Text="Indicadores Año por año de siembre" Value="indi02"></asp:TreeNode>
                                    <asp:TreeNode Text="Ciclos de corte de lotes por racimos" Value="tran17"></asp:TreeNode>
                                    <asp:TreeNode Text="Ciclos de labores" Value="indi01"></asp:TreeNode>
                                    <asp:TreeNode Text="Rendimiento trabajador por mes" Value="rendi01"></asp:TreeNode>
                                    <asp:TreeNode Text="Rendimiento trabajador cosecha" Value="rendi02"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Sanidad " Value="Sanidad ">
                                    <asp:TreeNode Text="Grupo de características" Value="san01"></asp:TreeNode>
                                    <asp:TreeNode Text="Sanidad Detalle" Value="san02"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Fertilización" Value="Fertilización">
                                    <asp:TreeNode Text="Plan de fertilización con saldos" Value="fer01"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores fecha lote" Value="fer02"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores fertilización detalle" Value="fer03"></asp:TreeNode>
                                    <asp:TreeNode Text="Plan de fertilización con saldos por transacción" Value="fer04"></asp:TreeNode>
                                </asp:TreeNode>
                            </Nodes>
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                NodeSpacing="0px" VerticalPadding="1px" />
                        </asp:TreeView>
                    </td>
                    <td style="background-image: none; width: 700px; background-color: transparent; text-align: left; vertical-align: top; height: 70px;"></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>