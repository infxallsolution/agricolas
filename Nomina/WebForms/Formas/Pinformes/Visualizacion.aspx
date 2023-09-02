<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visualizacion.aspx.cs" Inherits="Nomina.WebForms.Formas.Pinformes.Visualizacion" %>

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

        function Visualizacion2(informe) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInformeCR.aspx";
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
                                <asp:TreeNode Text="Administración" Value="Nomina">
                                    <asp:TreeNode Text="Conceptos" Value="Nomi01"></asp:TreeNode>
                                    <asp:TreeNode Text="Contratos" Value="Nomi03"></asp:TreeNode>
                                    <asp:TreeNode Text="Funcionarios" Value="Nomi02"></asp:TreeNode>
                                    <asp:TreeNode Text="Funcionario por centro costo" Value="Nomi04"></asp:TreeNode>
                                    <asp:TreeNode Text="Vencimiento de contratos" Value="Nomi05"></asp:TreeNode>
                                    <asp:TreeNode Text="Contratos - Destajo" Value="Cont01"></asp:TreeNode>
                                </asp:TreeNode>
                                <%--<asp:TreeNode Text="Gestion Humana" Value="gestion">
                                    <asp:TreeNode Text="Hoja de vida" Value="ges01"></asp:TreeNode>
                                </asp:TreeNode>--%>
                                <asp:TreeNode Text="Control de Acceso" Value="Seguridad Social">
                                    <asp:TreeNode Text="Liquidación de Horas Extras" Value="liqui13"></asp:TreeNode>
                                    <asp:TreeNode Text="Liquidación de Horas Extras Totales" Value="liqui14"></asp:TreeNode>
                                </asp:TreeNode>

                                <asp:TreeNode Text="Novedades" Value="Novedades">
                                    <asp:TreeNode Text="Registro novedades" Value="liqui02"></asp:TreeNode>
                                    <asp:TreeNode Text="Novedades periodicas activas" Value="liqui03"></asp:TreeNode>
                                    <asp:TreeNode Text="Novedades tercero fecha" Value="liqui04"></asp:TreeNode>
                                    <asp:TreeNode Text="Relación novedades periodo" Value="liqui16"></asp:TreeNode>
                                    <asp:TreeNode Text="Relación descuentos periodo" Value="liqui17"></asp:TreeNode>
                                    <asp:TreeNode Text="Prestamos" Value="nove01"></asp:TreeNode>
                                    <asp:TreeNode Text="Prestamos Saldos" Value="nove02"></asp:TreeNode>
                                    <asp:TreeNode Text="Prestamos saldos por concepto" Value="nove03"></asp:TreeNode>
                                    <asp:TreeNode Text="Ausentismo" Value="liqui28"></asp:TreeNode>
                                </asp:TreeNode>

                                <asp:TreeNode Text="Preliquidación de Nomina" Value="Preliquidación de Nomina">
                                    <asp:TreeNode Text="Desprendible Preliquidación" Value="liqui01"></asp:TreeNode>
                                    <asp:TreeNode Text="Resumen Preliquidación" Value="liqui05"></asp:TreeNode>
                                    <asp:TreeNode Text="Revisión Preliquidación" Value="preli01"></asp:TreeNode>
                                    <asp:TreeNode Text="Resumen descuentos Preliquidación" Value="preli02"></asp:TreeNode>
                                    <asp:TreeNode Text="Revisión Preliquidción supera deducido" Value="supe01"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Liquidación Nomina" Value="Liquidación Nomina">

                                    <asp:TreeNode Text="Liquidación definitiva periodo" Value="liqui06"></asp:TreeNode>
                                    <asp:TreeNode Text="Resumen liquidación por Periodo" Value="liqui07"></asp:TreeNode>
                                    <asp:TreeNode Text="Resumen liquidación Mensual" Value="liqui21"></asp:TreeNode>
                                    <asp:TreeNode Text="Pago de liquidación Periodo" Value="liqui08"></asp:TreeNode>
                                    <asp:TreeNode Text="Desprendibles nomina" Value="liqui09"></asp:TreeNode>
                                    <asp:TreeNode Text="Desprendibles de nomina general" Value="liqui49"></asp:TreeNode>
                                    <asp:TreeNode Text="Descuentos nomina Periodo" Value="liqui10"></asp:TreeNode>
                                    <asp:TreeNode Text="Acumulado empleado Año" Value="liqui50"></asp:TreeNode>
                                    <asp:TreeNode Text="Acumulados periodo" Value="liqui12"></asp:TreeNode>
                                    <asp:TreeNode Text="Labores no liquidadas en nomina" Value="liqui15"></asp:TreeNode>
                                    <asp:TreeNode Text="Liquidación nomina detalle" Value="liqui24"></asp:TreeNode>
                                    <asp:TreeNode Text="Descuentos por periodo de nomina" Value="liqui29"></asp:TreeNode>
                                    <asp:TreeNode Text="Ingresos y retenciones detallado - Formato 2276 " Value="ingre01"></asp:TreeNode>
                                    <asp:TreeNode Text="Formato ingreso y retenciones" Value="liqui46"></asp:TreeNode>
                                    <asp:TreeNode Text="Detalle novedades" Value="liqui45"></asp:TreeNode>
                                    <asp:TreeNode Text="Conceptos acumulado Primas" Value="liqui450"></asp:TreeNode>
                                    <asp:TreeNode Text="Resumen de pagos" Value="liqui47"></asp:TreeNode>
                                    <asp:TreeNode Text="Descuentos por entidad SS" Value="liqui48"></asp:TreeNode>
                                    <asp:TreeNode Text="Descuento por entidad periodo" Value="liqui51"></asp:TreeNode>
                                    <asp:TreeNode Text="Revision Liquidación peridodo" Value="RevisionLiquidacionResumen"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Liquidación Nomina Semanal" Value="Liquidación Nomina Semanal">
                                    <asp:TreeNode Text="Preliquidación" Value="ls01"></asp:TreeNode>
                                    <asp:TreeNode Text="Nomina definitiva" Value="ls02"></asp:TreeNode>
                                    <asp:TreeNode Text="Desprendibles de nomina" Value="ls03"></asp:TreeNode>
                                    <asp:TreeNode Text="Pagos de nomina" Value="ls04"></asp:TreeNode>
                                    <asp:TreeNode Text="Resumen de descuentos" Value="ls05"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Seguridad social" Value="Seguridad Social">
                                    <asp:TreeNode Text="Relación devengado trabajador" Value="liqui18"></asp:TreeNode>
                                    <asp:TreeNode Text="Seguridad social periodo" Value="liqui11"></asp:TreeNode>
                                    <asp:TreeNode Text="Seguridad social x entidad" Value="liqui22"></asp:TreeNode>
                                    <asp:TreeNode Text="IBC Seguridad Social" Value="liqui30"></asp:TreeNode>
                                    <asp:TreeNode Text="IBC nomi vs SS" Value="liqui31"></asp:TreeNode>
                                </asp:TreeNode>

                                <asp:TreeNode Text="Prestaciones sociales" Value="Prestaciones Sociales">
                                    <asp:TreeNode Text="Vacaciones periodo" ToolTip="Vacaciones Periodo" Value="liqui23"></asp:TreeNode>
                                    <asp:TreeNode Text="Consolidado vacaciones" Value="liqui25"></asp:TreeNode>
                                    <asp:TreeNode Text="Acumulados prestaciones" Value="presta01"></asp:TreeNode>
                                    <asp:TreeNode Text="Pre-liquidación primas semestrales" Value="presta02"></asp:TreeNode>
                                    <asp:TreeNode Text="Liquidación de contrato - Tercero" Value="presta05"></asp:TreeNode>
                                    <asp:TreeNode Text="Liquidacion primas definitiva" Value="presta06"></asp:TreeNode>
                                    <asp:TreeNode Text="Liquidacion cesantias definitiva" Value="presta07"></asp:TreeNode>

                                </asp:TreeNode>

                                <asp:TreeNode Text="Pagos" Value="Pagos">
                                    <asp:TreeNode Text="Cheque bancolombia" Value="pag01"></asp:TreeNode>
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
