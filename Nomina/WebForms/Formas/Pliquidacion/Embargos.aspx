<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Embargos.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.Embargos" %>

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
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label4" runat="server" Text="Empleado" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTercero" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged"
                            Visible="False" Width="97%">
                        </asp:DropDownList></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label7" runat="server" Text="Contrato" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <table cellpadding="0" cellspacing="0" class="auto-style1">
                            <tr>
                                <td style="width: 160px; text-align: left">
                                    <asp:DropDownList ID="ddlContratos" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 60px; text-align: left">
                                    <asp:Label ID="Label19" runat="server" Text="Número" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="input" MaxLength="20" Visible="False" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label6" runat="server" Text="Tipo embargo" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoEmbargo" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged"
                            Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label9" runat="server" Text="Año" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <table class="auto-style1">
                            <tr>
                                <td style="width: 100px; text-align: left">
                                    <asp:TextBox ID="txtAñoInicial" runat="server" CssClass="input" OnTextChanged="txtPeriodoCobro_TextChanged" placeholder="AAAA" Visible="False" Width="100px" AutoPostBack="True" MaxLength="4"></asp:TextBox>
                                </td>
                                <td style="width: 80px; text-align: left">
                                    <asp:Label ID="Label30" runat="server" Text="No periodo" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPeriodoInicial" runat="server" CssClass="input" OnTextChanged="txtPeriodoCobro_TextChanged" placeholder="PP" Visible="False" Width="60px" AutoPostBack="True" MaxLength="3"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lbFecha" runat="server"  Visible="False">Fecha embargo</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" placeholder="DD/MM/YYYY" Width="150px" AutoPostBack="True" Visible="False" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label8" runat="server" Text="No mandato judicial" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtNumeroMandato" runat="server" CssClass="input" OnTextChanged="txtConcepto_TextChanged" Visible="False" Width="97%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label21" runat="server" Text="Empresa embargante" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlEmpresaEmbarga" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged"
                            Visible="False" Width="97%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label22" runat="server" Text="Tercero embargante" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTerceroEmbargo" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged"
                            Visible="False" Width="97%">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCuotas" runat="server" Text="Maneja cuotas" AutoPostBack="True" OnCheckedChanged="chkCuotas_CheckedChanged" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvCuotas" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label26" runat="server" Text="Valor cuota" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorEmbargo" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False">0</asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label27" runat="server" Text="Valor %" Visible="False"></asp:Label>
                    </td>
                    <td valign="middle" style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorPorcentaje" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False">0</asp:TextBox>
                    </td>
                    <td valign="middle" style="text-align: left; width: 130px">
                        <asp:Label ID="Label23" runat="server" Text="Valor final embargo" Visible="False"></asp:Label></td>
                    <td valign="middle" style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorFinalEmbargo" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False">0</asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCobroPosterior" runat="server" Text="Cobro posterior" OnCheckedChanged="chkCobroPosterior_CheckedChanged" Visible="False" AutoPostBack="True" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSalarioMinimo" runat="server" Text="Salario Minimo" OnCheckedChanged="chkManejaSaldo_CheckedChanged" Visible="False" />
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" OnCheckedChanged="chkManejaSaldo_CheckedChanged" Visible="False" Checked="True" />
                    </td>
                    <td style="text-align: left; ">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCuotasPosteriores" runat="server" Text="Cuotas posterior" Enabled="False" OnCheckedChanged="chkCuotasPosteriores_CheckedChanged" Visible="False" AutoPostBack="True" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvCuotasPosteriores" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label28" runat="server" Text="Valor cuota pos." Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorEmbargoPosterior" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False">0</asp:TextBox></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label29" runat="server" Text="Valor % pos." Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorPorPosterior" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False">0</asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label25" runat="server" Text="Valor final posterior" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorFinalPos" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False">0</asp:TextBox></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label24" runat="server" Text="Valor base" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorBase" runat="server" Visible="False" Width="100px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Enabled="False">0</asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lblDepartamento30" runat="server" Text="Forma de pago" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lblDepartamento37" runat="server" Text="Tipo de cuenta" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lblDepartamento32" runat="server" Text="Banco" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlBanco" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="97%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lblDepartamento36" runat="server" Text="Número cuenta" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaSaldo" runat="server" Text="Maneja Saldo" OnCheckedChanged="chkManejaSaldo_CheckedChanged" Visible="False" AutoPostBack="True" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvSaldo" runat="server" CssClass="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" Visible="False"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lblDepartamento38" runat="server" Text="Fiscal" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFiscal" runat="server" CssClass="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="97%" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
               </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
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
						
                            <asp:BoundField DataField="codEmpleado" HeaderText="Identificación">
                                <ItemStyle  HorizontalAlign="Left" Width="10px" />
                            </asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderText="Id">
                                <ItemStyle  HorizontalAlign="Left" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desEmpleado" HeaderText="Empleado">
                                <HeaderStyle  HorizontalAlign="Left" />
                                <ItemStyle  HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="contrato" HeaderText="Contrato">
                                <ItemStyle  HorizontalAlign="Left" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo" HeaderText="Número">
                                <ItemStyle  HorizontalAlign="Left" Width="40px" />
                            </asp:BoundField>
                               <asp:BoundField DataField="tipo" HeaderText="TE" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  Width="5px"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombreTipo" HeaderText="TipoEmbargo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="activo" HeaderText="Act">
                                <ItemStyle HorizontalAlign="Center" Width="4px" CssClass="Items" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="salarioMinimo" HeaderText="S.M">
                                <ItemStyle  Width="5px" />
                            </asp:CheckBoxField>

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
