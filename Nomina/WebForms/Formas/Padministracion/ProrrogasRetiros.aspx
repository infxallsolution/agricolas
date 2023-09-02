<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProrrogasRetiros.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.ProrrogasRetiros" %>

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
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Empleado" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 40%">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged"
                            Visible="False" Width="100%">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label3" runat="server" Text="Contrato" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 40%">
                        <asp:DropDownList ID="ddlContrato" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged"
                            Visible="False" Width="100%">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label5" runat="server" Text="Tipo Movimieto" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 40%">
                        <asp:DropDownList ID="ddlTipoRegistro" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"
                            Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoRegistro_SelectedIndexChanged">
                            <asp:ListItem Value="P">Prorroga</asp:ListItem>
                            <asp:ListItem Value="R">Retiro</asp:ListItem>
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Panel ID="pnProrroga" runat="server" Visible="False">
                            <h6>Prorroga de contratos </h6>
                            <hr />
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 120px; text-align: left;">
                                        <asp:Label ID="Label6" runat="server" Text="Codigo" Visible="True"></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 120px">
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="input" Enabled="False" OnTextChanged="txtConcepto_TextChanged" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="width:120px; text-align:left;">
                                        <asp:Label ID="Label7" runat="server" Text="Número días"></asp:Label>
                                    </td>
                                    <td style="width:120px; text-align:left;">
                                        <asp:TextBox ID="txvDias" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" OnTextChanged="txtDias_TextChanged" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="width:120px; text-align:left;">&nbsp;</td>
                                    <td style="width:120px; text-align:left;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 80px; text-align: left;">
                                        <asp:Label ID="Label8" runat="server" Text="Fecha inicio"></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 80px">
                                        <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="input fecha" Enabled="False" OnTextChanged="txtConcepto_TextChanged" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="width:80px; text-align:left;">
                                        <asp:Label ID="Label9" runat="server" Text="Fecha final" Visible="True"></asp:Label>
                                    </td>
                                    <td style="width:80px; text-align:left;">
                                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="input fecha" Enabled="False" OnTextChanged="txtConcepto_TextChanged" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="width:80px; text-align:left;">
                                        <asp:Label ID="Label10" runat="server" Text="Ultima fecha"></asp:Label>
                                    </td>
                                    <td style="width:80px; text-align:left;">
                                        <asp:TextBox ID="txtUltimaFechaFinal" runat="server" CssClass="input fecha" Enabled="False" OnTextChanged="txtConcepto_TextChanged" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" colspan="6">
                                        <asp:TextBox ID="txtObservacion" runat="server" placeholder="Observaciones y/o notas de la prorroga..." CssClass="input" Height="100px" OnTextChanged="txtConcepto_TextChanged" TextMode="MultiLine" Visible="True" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Panel ID="pnRetiro" runat="server" Visible="False">
                            <h6>Retiro de empleado o teminación de contrato </h6>
                            <hr />
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left; width: 150px">
                                        <asp:Label ID="Label12" runat="server" Text="Codigo" Visible="True"></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        <asp:TextBox ID="txtCodigoRetiro" runat="server" CssClass="input" Enabled="False" OnTextChanged="txtConcepto_TextChanged" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 120px">
                                        <asp:Label ID="Label19" runat="server" Text="Fecha ingreso"></asp:Label>
                                    </td>
                                    <td style="text-align: left; ">
                                        <asp:TextBox ID="txtFechaInicialR" runat="server" CssClass="input fecha" Enabled="False" OnTextChanged="txtConcepto_TextChanged" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 150px">
                                        <asp:Label ID="lbFechaFinal" runat="server" >Fecha ultima prorroga</asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        <asp:TextBox ID="txtFechaFinalR" runat="server" CssClass="input fecha" Enabled="False" OnTextChanged="txtFechaFinalR_TextChanged" placeholder="DD/MM/YYYY" Visible="True" Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 120px">
                                        <asp:Label ID="lbFecha" runat="server">Fecha retiro</asp:Label>
                                    </td>
                                    <td style="text-align: left; ">
                                        <asp:TextBox ID="txtFecha" runat="server" AutoPostBack="True" CssClass="input fecha" Font-Bold="True" OnTextChanged="txtFecha_TextChanged" placeholder="DD/MM/YYYY" ToolTip="Formato fecha (dd/mm/yyyy)" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 150px">
                                        <asp:Label ID="Label18" runat="server" Text="Motivo retiro"></asp:Label>
                                    </td>
                                    <td colspan="3" style="text-align: left; ">
                                        <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="text-align: left;" colspan="4">
                                        <asp:TextBox ID="txtObservacionRetiro" runat="server" placeholder="Observaciones y/o notas del retiro..." CssClass="input" Height="100px" TextMode="MultiLine" Visible="True" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="id" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="ProRet" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="contrato" HeaderText="Contrato" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" Width="10px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo" HeaderText="Identificacion">
                            <ItemStyle HorizontalAlign="Left" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Empleado">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaInicial" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechIni">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaFinal" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechFin">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaFinalAnterior" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechFinAnt">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaRetiro" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechRet">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="5px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="tercero" ShowHeader="False">
                            <ItemStyle BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" Width="10px" />
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
