<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametrosGeneral.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.ParametrosGeneral" %>

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
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Refrescar datos" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" />
                    </td>
                </tr>
            </table>
            <hr />

            <table style="width: 100%;">
                <tr>
                    <td style="width: 15%;"></td>
                    <td style="width: 70%;">
                        <h6>Parametros de cálculos</h6>
                        <fieldset style="border: 1px solid #0099FF; border-radius: 5px; padding: 5px;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 10px"></td>
                                    <td style="width: 220px; text-align: left">
                                        <asp:Label ID="Label6" runat="server" Text="No. SMLV para salario integral"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvNoSalarioIntegral" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label ID="Label7" runat="server" Text="Jornada diaria"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvJornadaDiaria" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 120px; text-align: left;">
                                        <asp:Label ID="Label25" runat="server" Text="Tipo jornada diaria"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:DropDownList ID="ddlTipoJornadaDiaria" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" runat="server" Width="96%">
                                            <asp:ListItem Value="C">Corriente</asp:ListItem>
                                            <asp:ListItem Value="A">Anticipado</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 10px"></td>
                                    <td style="width: 220px; text-align: left">
                                        <asp:Label ID="Label75" runat="server" Text="% Seguridad social salario integral"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvPorcentajeSalarioIntegral" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label ID="Label66" runat="server" Text="Días vacaciones"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvDiasVacaciones" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 120px; text-align: left;">
                                        <asp:Label ID="lbFecha" runat="server">Ultima cesantias</asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txtUltimaCesantias" runat="server" class="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 10px"></td>
                                    <td style="width: 220px; text-align: left">
                                        <asp:Label ID="Label26" runat="server" Text="HIJ diurna" ToolTip="Hora inicial jornada diurna"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvHIJD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label ID="Label27" runat="server" Text="HIJ nocturna" ToolTip="Hora inicial jornada nocturna"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvHIJN" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;" colspan="2">
                                        <asp:CheckBox ID="chkPaga31" runat="server" Text="Paga meses con 31" CssClass="checkbox checkbox-primary" />
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 10px"></td>
                                    <td style="width: 220px; text-align: left">
                                        <asp:Label ID="Label69" runat="server" Text="HFJ diurna" ToolTip="Hora final jornada diurna"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvHFJD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label ID="Label70" runat="server" Text="HFJ nocturna" ToolTip="Hora final jornada nocturna"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvHFJN" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;" colspan="2">
                                        <asp:CheckBox ID="chkPromediaFestivo" runat="server" Text="Promedia festivos" CssClass="checkbox checkbox-primary" />
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 10px"></td>
                                    <td style="width: 220px; text-align: left">
                                        <asp:Label ID="Label71" runat="server" Text="No. SMLV Parafiscales (Sena, ICBF)"></asp:Label>
                                    </td>
                                    <td style="width: 100px; text-align: left;">
                                        <asp:TextBox ID="txvNoSMLVParafiscales" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td colspan="2" style="text-align: left">
                                        <asp:CheckBox ID="chkCalculaJornalAutomatico" CssClass="checkbox checkbox-primary" runat="server" Text="Calcula jornales automatico" />
                                    </td>
                                    <td style="text-align: left;" colspan="2">
                                        <asp:CheckBox ID="chkPromediaGD" runat="server" Text="Promedia gana domingo" CssClass="checkbox checkbox-primary" />
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 10px"></td>
                                    <td style="text-align: left" colspan="2">
                                        <asp:CheckBox ID="chkAportesParafiscalesING" CssClass="checkbox checkbox-primary" runat="server" Text="Realiza aportes parafiscales en días de incapacidad" />
                                    </td>
                                    <td colspan="2" style="text-align: left">
                                        </td>
                                    <td style="text-align: left;" colspan="2"></td>
                                    <td style="width: 10px"></td>
                                </tr>
                            </table>
                        </fieldset>
                        <div>
                            <br />
                            <h6>Tipos de transacción Liquidacion</h6>
                            <fieldset style="border: 1px solid #0099FF; border-radius: 5px; padding: 5px;">
                                <table class="w-100">
                                    <tr>
                                        <td></td>
                                        <td class="w-50">
                                            <h6>Tipos de transacción nomina</h6>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="font-size: 11px; width: 120px; text-align: left;">
                                                        <asp:Label ID="Label72" runat="server" Text="Liquidación Nomina" ToolTip="Hora final jornada diurna"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlLiquidacionNomina" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 11px; width: 120px; text-align: left;">
                                                        <asp:Label ID="Label73" runat="server" Text="Acumulado" ToolTip="Hora final jornada diurna"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlAcumulado" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 11px; width: 120px; text-align: left;">
                                                        <asp:Label ID="Label74" runat="server" Text="Liquidación Contrato" ToolTip="Hora final jornada diurna"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlLiquidacionContrato" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="w-50">
                                            <h6>Tipos de transacción liquidación</h6>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="font-size: 11px; width: 120px; text-align: left;">
                                                        <asp:Label ID="Label3" runat="server" Text="Liquidación Primas" ToolTip="Hora final jornada diurna"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlLiquidacionPrimas" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 11px; width: 120px; text-align: left;">
                                                        <asp:Label ID="Label11" runat="server" Text="Liquidación cesantias" ToolTip="Hora final jornada diurna"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlLiquidacionCesantias" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 11px; width: 120px; text-align: left;">
                                                        <asp:Label ID="Label12" runat="server" Text="Liquidación vacaciones" ToolTip="Hora final jornada diurna"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlLiquidacionVacaciones" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <br />
                        <h6>Conceptos de extras y recargos</h6>
                        <fieldset style="border: 1px solid #0099FF; border-radius: 5px; padding: 5px;">
                            <table class="w-100">
                                <tr>
                                    <td></td>
                                    <td colspan="2">
                                        <h6>Días ordinarios</h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label31" runat="server" Text="Horas ordinarias"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlHorasOrdinarias" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label1" runat="server" Text="Horas recargo nocturno"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlHRN" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label2" runat="server" Text="Horas extras diurnas"></asp:Label>
                                                </td>
                                                <td style=" text-align: left;">
                                                    <asp:DropDownList ID="ddlHED" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label5" runat="server" Text="Horas extras nocturnas"></asp:Label>
                                                </td>
                                                <td style=" text-align: left;">
                                                    <asp:DropDownList ID="ddlHEN" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="w-50">
                                        <br />
                                        <h6>Días festivos </h6>
                                        <hr />
                                        <table class="w-100">
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label4" runat="server" Text="Hora festivas"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlHF" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label34" runat="server" Text="Horas recargo nocturno"></asp:Label>
                                                </td>
                                                <td style=" text-align: left;">
                                                    <asp:DropDownList ID="ddlHRF" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label35" runat="server" Text="Horas extras diurnas"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlHEDF" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label36" runat="server" Text="Horas extras nocturnas"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlHENF" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="w-50">
                                        <br />
                                        <h6>Días dominicales</h6>
                                        <hr />
                                        <table class="w-100">
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label32" runat="server" Text="Horas dominicales"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlHD" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label8" runat="server" Text="Horas recargo nocturno"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlRND" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label9" runat="server" Text="Horas extras diurnas"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlHEDD" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label10" runat="server" Text="Horas extras nocturnas"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlHEND" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <h6>Conceptos generales</h6>
                        <fieldset style="padding: 5px; border: 1px solid #0099FF; border-radius: 5px;">
                            <table class="w-100">
                                <tr>
                                    <td></td>
                                    <td class="w-50">
                                        <h6>Conceptos ordinarios</h6>
                                        <hr />
                                        <table class="w-100">
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label28" runat="server" Text="Sueldo"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlSueldo" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label29" runat="server" Text="Jornales"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlJornales" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label30" runat="server" Text="Cesantias"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlCesantias" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label37" runat="server" Text="Intereses cesantias"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlInteresesCesantias" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label38" runat="server" Text="Vacaciones"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlVacaciones" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label39" runat="server" Text="Primas"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlPrimas" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label50" runat="server" Text="Salario integral"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlSalarioIntegral" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label48" runat="server" Text="Permisos"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlPermisos" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label52" runat="server" Text="Subsidio de transporte"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlSubsidioTranasporte" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label55" runat="server" Text="Retroactivo"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlRetroactivo" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label53" runat="server" Text="Retención"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlRetencion" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label56" runat="server" Text="Sunpenciones"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlSuspencion" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label57" runat="server" Text="Incapacidades"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlincapacidades" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label62" runat="server" Text="Embargos"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlEmbargos" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label63" runat="server" Text="Gana domingo campo"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlGanaDomingoCampo" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label65" runat="server" Text="Sindicato"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlSindicato" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label67" runat="server" Text="Paga festivo "></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlPagaFestivo" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="w-50">
                                        <h6>Conceptos entidades / adicionales</h6>
                                        <hr />
                                        <table class="w-100">
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label43" runat="server" Text="Caja compensación"></asp:Label>
                                                </td>
                                                <td style="text-align: left;" style="text-align: left">
                                                    <asp:DropDownList ID="ddlCajaCompensacion" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label60" runat="server" Text="Salud"></asp:Label>
                                                </td>
                                                <td style="text-align: left;" style="text-align: left">
                                                    <asp:DropDownList ID="ddlSalud" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label61" runat="server" Text="Pensión"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlPension" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label44" runat="server" Text="Sena"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlSena" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label45" runat="server" Text="ICBF"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlICBF" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label54" runat="server" Text="ARP"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlARP" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label40" runat="server" Text="Indemnización"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlIndemnizacion" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label41" runat="server" Text="Enfermedad y Maternidad"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlEnfermedadMaternidad" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label42" runat="server" Text="Invalidez, vejez y muerte"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlIVM" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label46" runat="server" Text="A.T.E.P."></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlATEP" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label47" runat="server" Text="Fondo solidaridad"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlFondoSolidaridad" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label49" runat="server" Text="Lic. remunerado"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlLicRemunerada" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label51" runat="server" Text="Lic. no  remunerado"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlLicNoRemunerada" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label58" runat="server" Text="Primas extralegales"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlPrimasExtralegales" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label59" runat="server" Text="Anticipo cesantias"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlAnticipoCesantias" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label64" runat="server" Text="Fondo de empleados"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlFondoEmpleado" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label68" runat="server" Text="Aprendiz Sena"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlAprendizSena" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>


                        </fieldset>
                        <br />
                        <h6>Parametros generales de control</h6>
                        <fieldset style="border: 1px solid #0099FF; border-radius: 5px; padding: 5px;">
                            <table class="w-100">
                                <tr>
                                    <td></td>
                                    <td class="w-50">
                                        <h6>Parametros generales nomina</h6>
                                        <hr />
                                        <table class="w-100">
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label13" runat="server" Text="Entidad general ARL"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlEntidadARL" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="w-50">
                                        <h6>Parametros generales de agronomico</h6>
                                        <hr />
                                        <table class="w-100">
                                            <tr>
                                                <td style="font-size: 11px; width: 150px; text-align: left;">
                                                    <asp:Label ID="Label76" runat="server" Text="Umedida Jornal"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlUmedidaJornal" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>

                        </fieldset>
                    </td>
                    <td style="width: 15%;"></td>
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
