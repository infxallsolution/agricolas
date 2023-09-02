<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiquidacionSeguridadSocial.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.LiquidacionSeguridadSocial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        function VisualizacionLiquidacion(informe, ano, periodo, numero) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&ano=" + ano + "&periodo=" + periodo + "&numero=" + numero;;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function VisualizacionPlano(empresa, año, periodo) {

            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + 0 + ", height =" + 0 + ", top = 0, left = 5";
            sTransaccion = "GenerarPlano.aspx?empresa=" + empresa + "&periodo=" + periodo + "&año=" + año;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function VisualizacionInforme(informe, año, periodo, numero) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&año=" + año + "&periodo=" + periodo + "&numero=" + numero;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }

    </script>

</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%"></td>
                    <td style="text-align: left; width: 70px">Año</td>
                    <td style="width: 150px; text-align: left;">
                        <asp:TextBox ID="nitxvAño" runat="server" CssClass="input" ToolTip="Escriba el texto para la busqueda" Width="120px" MaxLength="4"></asp:TextBox>
                    </td>
                    <td style="width: 70px; text-align: left">Mes</td>
                    <td style="text-align: left; width: 170px;">
                        <asp:DropDownList ID="niddlMes" runat="server" Width="150px" CssClass="chzn-select-deselect">
                            <asp:ListItem Value="1">Enero</asp:ListItem>
                            <asp:ListItem Value="2">Febrero</asp:ListItem>
                            <asp:ListItem Value="3">Marzo</asp:ListItem>
                            <asp:ListItem Value="4">Abril</asp:ListItem>
                            <asp:ListItem Value="5">Mayo</asp:ListItem>
                            <asp:ListItem Value="6">Junio</asp:ListItem>
                            <asp:ListItem Value="7">Julio</asp:ListItem>
                            <asp:ListItem Value="8">Agosto</asp:ListItem>
                            <asp:ListItem Value="9">Septiembre</asp:ListItem>
                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 100px;">Trabajador</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="nitxtFiltro" runat="server" CssClass="input" ToolTip="Escriba el texto para la busqueda" Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 10%"></td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="nilbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                        <asp:Button ID="nibtnLiquidar" runat="server" CssClass="botones" OnClick="btnLiquidar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de liquidar el período?');" Text="Liquidar" />
                        <asp:Button ID="nibtnGenerarPlano" runat="server" CssClass="botones" OnClick="nibtnGenerarPlano_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro generar el plano?');" Text="Genera plano" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnDatos" runat="server" Visible="False">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 5%"></td>
                        <td style="width: 90%">
                            <div id="caja" style="width: 100%; padding: 5px">
                                <fieldset style="border: 1px solid #3366FF; padding: 3px">
                                    <legend style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 14px; color: #3366FF; font-weight: bold; text-align: left">Datos de contrato </legend>
                                    <table style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;" id="Table2">
                                        <tr>
                                            <td style="width: 100px; text-align: left;">
                                                <asp:Label ID="lblaño" runat="server" Text="Año / Mes" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 80px; text-align: left;">
                                                <asp:TextBox ID="txvAño" runat="server" CssClass="input" MaxLength="4" ToolTip="Escriba el texto para la busqueda" Visible="False" Width="70px" AutoPostBack="True" OnTextChanged="txvAño_TextChanged" TextMode="Number"></asp:TextBox>
                                            </td>
                                            <td style="width: 120px; text-align: left;">
                                                <asp:DropDownList ID="ddlMes" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Enero</asp:ListItem>
                                                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                    <asp:ListItem Value="4">Abril</asp:ListItem>
                                                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                    <asp:ListItem Value="6">Junio</asp:ListItem>
                                                    <asp:ListItem Value="7">Julio</asp:ListItem>
                                                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                    <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px; text-align: left;">
                                                <asp:Label ID="lblOpcionLiquidacion" runat="server" Text="Trabajador" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlEmpleado" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged" Visible="False" Width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblOpcionLiquidacion0" runat="server" Text="Contrato" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlContratos" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlContratos_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 50px; text-align: left;">
                                                <asp:Label ID="lbRegistro" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto88" runat="server" Text="Tipo Identi." Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlTipoId" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="95%">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left; width: 110px">
                                                <asp:Label ID="lblCcosto89" runat="server" Text="Identificacion" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtIdentificacion" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" Visible="False" Width="95%"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto90" runat="server" Text="Código" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtCodigoTercero" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 110px"></td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto47" runat="server" Text="Primer apellido" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtApellido1" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" Visible="False" Width="95%"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 110px">
                                                <asp:Label ID="lblCcosto48" runat="server" Text="Segundo apellido" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtApellido2" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" Visible="False" Width="95%"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto49" runat="server" Text="Primer nombre" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtNombre1" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" Visible="False" Width="95%"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 110px">
                                                <asp:Label ID="lblCcosto50" runat="server" Text="Segundo nombre" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtNombre2" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" Visible="False" Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto51" runat="server" Text="Departamento" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlDepartamento" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left; width: 110px">
                                                <asp:Label ID="lblCcosto52" runat="server" Text="Ciudad" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlCiudad" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto53" runat="server" Text="Tipo cotizante" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlTipoCotizante" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left; width: 110px">
                                                <asp:Label ID="lblCcosto54" runat="server" Text="Sub tipo cot" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlSubTipoCotizante" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto46" runat="server" Text="Horas laboradas" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txvNoHoras" runat="server" Font-Bold="True" ForeColor="#336699"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px" TextMode="Number"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 110px">
                                                <asp:CheckBox ID="chkExtrajero" runat="server" Text="Extranjero" Visible="False" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkRecidenteExtranjero" runat="server" Text="Recidente Exterior" Visible="False" />
                                            </td>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="lblCcosto37" runat="server" Text="Fecha rad. ext." Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtFechaRadExtrajero" runat="server" Font-Bold="True" ForeColor="#336699"
                                                    Visible="False" CssClass="input" Width="150px" OnTextChanged="txtFechaRadExtrajero_TextChanged1"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 110px">
                                                <asp:Label ID="lblCcosto55" runat="server" Text="Salario" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txvSalario" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset style="border: 1px solid #3366FF; padding: 3px">
                                    <legend style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 14px; color: #3366FF; font-weight: bold; text-align: left">Novedades </legend>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%; text-align: left;">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkING" runat="server" Text="ING" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlIngreso" runat="server" Width="50px" CssClass="chzn-select-deselect" Visible="False">
                                                                <asp:ListItem>X</asp:ListItem>
                                                                <asp:ListItem>R</asp:ListItem>
                                                                <asp:ListItem>C</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="lblCcosto23" runat="server" Text="Fecha ingreso" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaIngreso" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkRET" runat="server" Text="RET" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRetiro" runat="server" Width="50px" CssClass="chzn-select-deselect" Visible="False">
                                                                <asp:ListItem>X</asp:ListItem>
                                                                <asp:ListItem>P</asp:ListItem>
                                                                <asp:ListItem>R</asp:ListItem>
                                                                <asp:ListItem>C</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto22" runat="server" Text="Fecha retiro" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaRetiro" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkTDE" runat="server" Text="TDE" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkTAE" runat="server" Text="TAE" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkTDP" runat="server" Text="TDP" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkTAP" runat="server" Text="TAP" Visible="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%; text-align: left;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkVSP" runat="server" Text="VSP" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto34" runat="server" Text="Fecha VSP" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaVSP" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkVST" runat="server" Text="VST" Visible="False" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkIGE" runat="server" Text="IGE" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto42" runat="server" Text="Fecha inicial" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaInicialIGE" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto43" runat="server" Text="Fecha final" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaFinalIGE" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkVAC" runat="server" Text="VAC" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlVacaciones" runat="server" Width="50px" CssClass="chzn-select-deselect" Visible="False">
                                                                <asp:ListItem>X</asp:ListItem>
                                                                <asp:ListItem>L</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto24" runat="server" Text="Fecha inicial" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaInicialVacaciones" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto25" runat="server" Text="Fecha final" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaFinalVacaciones" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkIRL" runat="server" Text="IRL" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txvIRP" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto44" runat="server" Text="Fecha inicial" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaInicialIRL" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto45" runat="server" Text="Fecha final" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaFinalIRL" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="width: 100%; text-align: left;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkSLN" runat="server" Text="SLN" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto38" runat="server" Text="Fecha inicial" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaInicialSLN" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto39" runat="server" Text="Fecha final" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaFinalSLN" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkLMA" runat="server" Text="LMA" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto40" runat="server" Text="Fecha inicial" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaInicialLMA" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto41" runat="server" Text="Fecha final" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaFinalLMA" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkAVP" runat="server" Text="AVP" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkVCT" runat="server" Text="VCT" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto32" runat="server" Text="Fecha inicial" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaInicialVCT" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCcosto33" runat="server" Text="Fecha final" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaFinalVCT" runat="server" Font-Bold="True" ForeColor="#336699"
                                                                Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkCorreccion" runat="server" Text="Correcciones" Visible="False" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:CheckBox ID="chkSalarioIntegral" runat="server" Text="Salario integral" Visible="False" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </fieldset>
                                <fieldset style="border: 1px solid #3366FF; padding: 3px">
                                    <legend style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 14px; color: #3366FF; font-weight: bold; text-align: left">Liquidación / Pensión </legend>
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto56" runat="server" Text="Administrador pensión" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:DropDownList ID="ddlPension" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto17" runat="server" Text="Días pensión" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvDiasPension" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto5" runat="server" Text="% Pensión" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvpPension" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto1" runat="server" Text="IBC Pensión" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvIBCPension" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto9" runat="server" Text="$ Pensión" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvValorPension" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto57" runat="server" Text="Inficador de alto riesgo" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:DropDownList ID="ddlAltoRiesgo" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                    <asp:ListItem>0</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto79" runat="server" Text="% Fondo" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvpFondo" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto60" runat="server" Text="$ Fondo solidaridad" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvValorFondo" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto61" runat="server" Text="$ Fondo subsidiado" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvValorFondoSub" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto58" runat="server" Text="$ Volun. afiliado" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvValorVoluntarioAfiliado" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto71" runat="server" Text="Pensión destino" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:DropDownList ID="ddlPensionDestino" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="95%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto59" runat="server" Text="$ Volun. empleador" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvValorVoluntarioEmpleador" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto62" runat="server" Text="$ Retenido" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvValorRetenido" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto63" runat="server" Text="$ Total" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txvValorTotalPension" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="width: 100px"></td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset style="border: 1px solid #3366FF; padding: 3px">
                                    <legend style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 14px; color: #3366FF; font-weight: bold; text-align: left">Liquidación / Salud </legend>
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto64" runat="server" Text="Administrador salud" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSalud" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto16" runat="server" Text="Días salud" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvDiasSalud" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto4" runat="server" Text="% Salud" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvpSalud" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto0" runat="server" Text="IBC salud" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvIBCSalud" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="100px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblCcosto8" runat="server" Text="$ Salud" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorSalud" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto65" runat="server" Text="$ UPC" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorUPC" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="100px"></asp:TextBox></td>
                                            <td colspan="2">
                                                <asp:Label ID="lblCcosto66" runat="server" Text="No. autorización (EG)" Visible="False"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtNoAutorizacionEG" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto68" runat="server" Text="$ Incapacidad (EG)" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorEG" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto93" runat="server" Text="Tipo ID UPC" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoIdUPC" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="95%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto70" runat="server" Text="Salud destino" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSaludDestino" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="95%">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblCcosto67" runat="server" Text="No. autorización (LAM)" Visible="False"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtNoAutorizacionLAM" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto69" runat="server" Text="$ Incapacidad (LAM)" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorLAM" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto94" runat="server" Text="ID UPC" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIdentificacionUPC" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset style="border: 1px solid #3366FF; padding: 3px">
                                    <legend style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 14px; color: #3366FF; font-weight: bold; text-align: left">Liquidación / ARL </legend>
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto72" runat="server" Text="Administrador ARL" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 220px">
                                                <asp:DropDownList ID="ddlARL" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto19" runat="server" Text="Días ARL" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvDiasARP" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblCcosto6" runat="server" Text="% ARL" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvpARP" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto2" runat="server" Text="IBC ARL" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 110px">
                                                <asp:TextBox ID="txvIBCArp" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="120px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="width: 110px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto73" runat="server" Text="Clase ARL" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 220px">
                                                <asp:DropDownList ID="ddlClaseARL" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                    <asp:ListItem>0</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto74" runat="server" Text="Centro trabajo" Visible="False"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlCentroTrabajo" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="95%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto10" runat="server" Text="$ ARL" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 110px">
                                                <asp:TextBox ID="txvValorARP" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)" Visible="False" Width="120px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="width: 110px"></td>
                                        </tr>
                                    </table>
                                </fieldset>

                                <fieldset style="border: 1px solid #3366FF; padding: 3px">
                                    <legend style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 14px; color: #3366FF; font-weight: bold; text-align: left">Liquidación / Caja </legend>
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto75" runat="server" Text="Administrador Caja" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 220px">
                                                <asp:DropDownList ID="ddlCaja" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto18" runat="server" Text="Días Caja" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvDiasCaja" runat="server" Font-Bold="True" ForeColor="#336699"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblCcosto7" runat="server" Text="% Caja" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvpCaja" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto3" runat="server" Text="IBC Caja" Visible="False"></asp:Label>
                                            </td>
                                            <td class="width: 100px">
                                                <asp:TextBox ID="txvIBCCaja" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto11" runat="server" Text="$ Caja" Visible="False"></asp:Label>
                                            </td>
                                            <td class="width: 100px">
                                                <asp:TextBox ID="txvValorCaja" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto78" runat="server" Text="IBC Caja otros Paraf." Visible="False"></asp:Label>
                                            </td>
                                            <td class="width: 100px">
                                                <asp:TextBox ID="txvValorOtrosParafiscales" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>
                                </fieldset>
                                <fieldset style="border: 1px solid #3366FF; padding: 3px">
                                    <legend style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 14px; color: #3366FF; font-weight: bold; text-align: left">Liquidación / Parefiscales </legend>
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto76" runat="server" Text="Administrador Sena" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlSena" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto80" runat="server" Text="% Sena" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvpSena" runat="server" Font-Bold="True" ForeColor="#336699"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblCcosto13" runat="server" Text="Valor Sena" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorSena" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkExoneraSalud" runat="server" Text="Exonera salud" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto77" runat="server" Text="Administrador ICBF" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlICBF" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto81" runat="server" Text="% ICBF" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvpICBF" runat="server" Font-Bold="True" ForeColor="#336699"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblCcosto14" runat="server" Text="Valor ICBF" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorICBF" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto82" runat="server" Text="Administrador ESAP" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlESAP" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto84" runat="server" Text="% ESAP" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvpESAP" runat="server" Font-Bold="True" ForeColor="#336699"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblCcosto86" runat="server" Text="Valor ESAP" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorESAP" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCcosto83" runat="server" Text="Administrador MEN" Visible="False"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlMEN" runat="server" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCcosto85" runat="server" Text="% MEN" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvpMEN" runat="server" Font-Bold="True" ForeColor="#336699"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="50px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblCcosto87" runat="server" Text="Valor MEN" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txvValorMEN" runat="server" Font-Bold="True" ForeColor="#336699" onkeyup="formato_numero(this)"
                                                    Visible="False" CssClass="input" AutoPostBack="True" Width="120px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>

                                </fieldset>
                            </div>
                        </td>
                        <td style="width: 5%"></td>
                    </tr>
                </table>
            </asp:Panel>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="50" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="año" HeaderText="Año">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mes" HeaderText="Mes">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="registro" HeaderText="No.">
                            <HeaderStyle />
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="idTercero" HeaderText="IdEmp">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigoTercero" HeaderText="Identif">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="apellido1" HeaderText="PApellido">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="apellido2" HeaderText="SApellido">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre1" HeaderText="PNombre">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre2" HeaderText="SNombre">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IBCpension" HeaderText="IBCpension">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dpension" HeaderText="dpension">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorPension" HeaderText="vPension">
                            <HeaderStyle />
                            <ItemStyle Width="5px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IBCSalud" HeaderText="IBCSalud">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dSalud" HeaderText="dSalud">
                            <HeaderStyle />
                            <ItemStyle Width="5px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IBCarl" HeaderText="IBCarl">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="darl" HeaderText="darl">
                            <HeaderStyle />
                            <ItemStyle Width="5px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorArl" HeaderText="vARL">
                            <HeaderStyle />
                            <ItemStyle Width="5px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IBCcaja" HeaderText="IBCcaja">
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dcaja" HeaderText="dcaja">
                            <HeaderStyle />
                            <ItemStyle Width="5px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorcaja" HeaderText="vCaja">
                            <HeaderStyle />
                            <ItemStyle Width="5px" HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ING" HeaderText="ING">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RET" HeaderText="RET">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TDE" HeaderText="TDE">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TAE" HeaderText="TAE">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TDP" HeaderText="TDP">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TAP" HeaderText="TAP">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VSP" HeaderText="VSP">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VST" HeaderText="VST">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SLN" HeaderText="SLN">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IGE" HeaderText="IGE">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LMA" HeaderText="LMA">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VAC" HeaderText="VAC">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AVP" HeaderText="AVP">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VCT" HeaderText="VCT">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IRL" HeaderText="IRP">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
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
