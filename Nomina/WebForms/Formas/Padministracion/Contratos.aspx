<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contratos.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.Contratos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var x = null;

        function Visualizacion(informe, empleado, numero) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&empleado=" + empleado + "&numero=" + numero;
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
                    <td></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 1000px">
                        <table style="width: 100%;" id="TABLE1">
                            <tr>
                                <td style="width: 200px; text-align: center" rowspan="5">
                                    <asp:Image ID="imbFuncionario" runat="server" Height="140px" Visible="False" Width="130px" />
                                </td>
                                <td style="width: 100px; text-align: left">
                                    <asp:Label ID="Label1" runat="server" Text="Empleado" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 600px;">
                                    <asp:DropDownList ID="ddlTercero" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlFuncionario_SelectedIndexChanged" Visible="False" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; text-align: left">
                                    <asp:Label ID="Label15" runat="server" Text="Código" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 600px;">
                                    <asp:TextBox ID="txtCodigoTercero" runat="server" CssClass="input" Width="150px" Visible="False" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; text-align: left">
                                    <asp:Label ID="Label13" runat="server" Text="Identificación" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 600px;">
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="input" Width="150px" Visible="False" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; text-align: left">
                                    <asp:Label ID="Label12" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 600px;">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input" Width="100%" Visible="False" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; text-align: left">
                                    <asp:Label ID="Label14" runat="server" Text="Nro. contrato" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 700px;">
                                    <asp:TextBox ID="txtNroContrato" runat="server" CssClass="input" Width="70px" Visible="False" ReadOnly="True" Enabled="False"></asp:TextBox>
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px;" colspan="3">
                                    <div id="pnContratos" runat="server" visible="False">
                                        <h6>Datos generales</h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 150px; text-align: left">
                                                    <asp:Label ID="lblDepartamento39" runat="server" Text="Centro costo"></asp:Label>
                                                </td>
                                                <td width="310px" style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlCcosto" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlCcosto_SelectedIndexChanged" Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lbFechaIngreso" runat="server">Fecha ingreso</asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" placeholder="DD/MM/AAAA" Width="150px" AutoPostBack="True" OnTextChanged="txtFechaIngreso_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento" runat="server" Text="Departamento"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblClaseContrato" runat="server" Text="Clase contrato"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlClaseContrato" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px" OnSelectedIndexChanged="ddlClaseContrato_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblRL" runat="server" Text="Regimen Laboral"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlRegimenLaboral" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="150px">
                                                        <asp:ListItem Value="1">Ley 50</asp:ListItem>
                                                        <asp:ListItem Value="2">Antes Ley 50</asp:ListItem>
                                                        <asp:ListItem Value="3">Jubilado</asp:ListItem>
                                                        <asp:ListItem Value="4">Otros</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblClaseContrato1" runat="server" Text="Días duración contrato"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txtDiasDuracion" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" onkeyup="formato_numero(this)" OnTextChanged="txtDiasDuracion_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px"></td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSalarioIntegral" runat="server" Text="Salario Integral  " />
                                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPactoColectivo" runat="server" Text="Pacto Colectivo " />
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblClaseContrato0" runat="server" Text="Contrato hasta"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txtFechaCH" runat="server" CssClass="input" Enabled="False" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <h6>Seguridad Social </h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 160px; text-align: left">
                                                    <asp:Label ID="lblDepartamento2" runat="server" Text="Entidad de Salud"></asp:Label>
                                                </td>
                                                <td width="310px" style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlEPS" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblDepartamento1" runat="server" Text="Entidad de Pensión"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlAFP" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 160px; text-align: left">
                                                    <asp:Label ID="lblDepartamento6" runat="server" Text="Tipo Salud Adicional"></asp:Label>
                                                </td>
                                                <td width="310px" style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlTipoSaludAdicional" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlSP_SelectedIndexChanged" Width="370px">
                                                        <asp:ListItem Value="01">No aplica</asp:ListItem>
                                                        <asp:ListItem Value="02">U.P.C</asp:ListItem>
                                                        <asp:ListItem Value="03">Prepagada</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblDepartamento11" runat="server" Text="Personas a cargo salud"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvPersonasCargo" runat="server" CssClass="input" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 160px; text-align: left">
                                                    <asp:Label ID="lblDepartamento43" runat="server" Text="Salud Adicional"></asp:Label>
                                                </td>
                                                <td width="310px" style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlEPSAdicional" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblDepartamento38" runat="server" Text="Valor adicional (UPC)"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvValAdicional" runat="server" CssClass="input" Enabled="False" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 160px; text-align: left">
                                                    <asp:Label ID="lblDepartamento0" runat="server" Text="A.R.P"></asp:Label>
                                                </td>
                                                <td width="310px" style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlARP" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblDepartamento3" runat="server" Text="Centro de Trabajo"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlCT" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 160px; text-align: left">
                                                    <asp:Label ID="lblDepartamento4" runat="server" Text="Tipo de cotizante"></asp:Label>
                                                </td>
                                                <td width="310px" style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlTipoCotizante" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    <asp:Label ID="lblDepartamento5" runat="server" Text="Subtipo cotizante"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlSubTipoCotizante" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                        </table>
                                        <hr />
                                        <h6>Parafiscales</h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento8" runat="server" Text="Caja de Compensación"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlCaja" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento9" runat="server" Text="Sena"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlSena" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento10" runat="server" Text="ICBF"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlICBF" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 150px"></td>
                                                <td style="text-align: left; width: 400px"></td>
                                            </tr>

                                        </table>
                                        <hr />
                                        <h6>Fondos</h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento12" runat="server" Text="Fondo de Cesantias"></asp:Label>
                                                </td>
                                                <td style="width: 350px; text-align: left;">
                                                    <asp:DropDownList ID="ddlFondoCesantias" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 150px"></td>
                                                <td style="text-align: left; width: 400px"></td>
                                            </tr>

                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSindicato" runat="server" AutoPostBack="True" OnCheckedChanged="chkSindicato_CheckedChanged" Text="Sindicato" />
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlSindicato" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Enabled="False" OnSelectedIndexChanged="ddlSP_SelectedIndexChanged" Width="370px">
                                                        <asp:ListItem Value="01">No aplica</asp:ListItem>
                                                        <asp:ListItem Value="02">U.P.C</asp:ListItem>
                                                        <asp:ListItem Value="03">Prepagada</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento40" runat="server" Text="Sindicato (%)"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvPorcentajeSindicato" runat="server" CssClass="input" Enabled="False" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkFondoEmpleado" runat="server" AutoPostBack="True" OnCheckedChanged="chkFondoEmpleado_CheckedChanged" Text="Fondo Empleado" />
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlFondoEmpleado" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Enabled="False" OnSelectedIndexChanged="ddlSP_SelectedIndexChanged" Width="370px">
                                                        <asp:ListItem Value="01">No aplica</asp:ListItem>
                                                        <asp:ListItem Value="02">U.P.C</asp:ListItem>
                                                        <asp:ListItem Value="03">Prepagada</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento42" runat="server" Text="Fondo Empleado (%)"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvPorcentajeFondo" runat="server" CssClass="input" Enabled="False" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>

                                        </table>
                                        <hr />
                                        <h6>Información del Salario</h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 150px; text-align: left" style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento13" runat="server" Text="Tipo de nomina"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 150px; text-align: left">
                                                    <asp:Label ID="lblDepartamento20" runat="server" Text="Sueldo basico"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvSueldoBasico" runat="server" CssClass="input" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Solo numeros" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento15" runat="server" Text="Cargo"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlCargo" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento27" runat="server" Text="Sueldo anterior"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvSueldoAnterior" runat="server" CssClass="input" Enabled="False" onkeyup="formato_numero(this)" Font-Bold="True" ToolTip="Solo Numeros" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblDepartamento17" runat="server" Text="Auxilio de transporte"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlAuxTransporte" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                        <asp:ListItem Value="0">No aplica</asp:ListItem>
                                                        <asp:ListItem Value="1">En dinero</asp:ListItem>
                                                        <asp:ListItem Value="2">En especie</asp:ListItem>
                                                        <asp:ListItem Value="3">Menor a 2.S.M.V.L </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento28" runat="server" Text="Cantidad de horas"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvHoras" runat="server" CssClass="input" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Solo numeros" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px">
                                                    <asp:Label ID="lblRL6" runat="server" Text="Fecha ultimo aumento"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txtFechaUltimoAumento" runat="server" CssClass="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento29" runat="server" Text="Tiempo laborado (%)"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvTiempoLaborado" runat="server" CssClass="input" Font-Bold="True" onkeyup="formato_numero(this)" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <h6>Información de Pago</h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento30" runat="server" Text="Forma de pago"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento32" runat="server" Text="Banco"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlBanco" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento37" runat="server" Text="Tipo de cuenta"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="lblDepartamento36" runat="server" Text="Número cuenta"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <h6>Información de Destajo</h6>
                                        <hr />
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaDestajo" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaDestajo_CheckedChanged" Text="Maneja destajo" />
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:DropDownList ID="ddlGrupoLabores" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="370px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 130px">
                                                    <asp:Label ID="Label3" runat="server" Text="Cantidad Destajo"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px">
                                                    <asp:TextBox ID="txvValorContrato" runat="server" CssClass="input" Font-Bold="True" onkeyup="formato_numero(this)" ToolTip="Solo numeros" Width="150px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </td>
                                <td style="height: 15px;"></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True" OnRowUpdating="gvLista_RowUpdating">
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
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tercero" HeaderText="Tercero" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True"
                            SortExpression="descripcion" HtmlEncode="False" HtmlEncodeFormatString="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaNacimiento" HeaderText="FechaN" DataFormatString="{0:dd/MM/yyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sexo" HeaderText="Sexo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="id" HeaderText="No Contrato">
                            <HeaderStyle />
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="conductor" HeaderText="Cond">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="operadorLogistico" HeaderText="Port">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CheckBoxField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imImprime" CssClass="btn btn-default btn-sm btn-primary fa fa-print" CommandName="update" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea imprimir el registro?');" ToolTip="Imprime el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>

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
