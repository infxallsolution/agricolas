<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametrosAño.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.ParametrosAño" %>

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
                    <td>
                        <div id="upCabeza" runat="server" visible="false">
                            <table style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:Label ID="Label1" runat="server" Text="Año" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 220px; text-align: left;">
                                        <asp:DropDownList ID="ddlAño" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged" Visible="False" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 200px; text-align: left;"></td>
                                    <td style="width: 220px; text-align: left;"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:Label ID="Label6" runat="server" Text="Valor salario minimo legal vigente" Visible="False"></asp:Label></td>
                                    <td style="width: 220px; text-align: left;">
                                        <asp:TextBox ID="txvSalarioMinimo" runat="server" Visible="False" Width="200px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:Label ID="Label7" runat="server" Text="Valor auxilo transporte" Visible="False"></asp:Label></td>
                                    <td style="width: 220px; text-align: left;">
                                        <asp:TextBox ID="txvAuxilioTransporte" runat="server" Visible="False" Width="200px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:Label ID="Label25" runat="server" Text="No SMLV para sub. transporte" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 220px; text-align: left;">
                                        <asp:TextBox ID="txvCantidadSMLV" runat="server" CssClass="input" MaxLength="2" onkeyup="formato_numero(this)" ToolTip="Número de salarios para sub. de transporte" Visible="False" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:Label ID="Label26" runat="server" Text="Pago minimo por periodo" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 220px; text-align: left;">
                                        <asp:TextBox ID="txvPagoMinPeriodo" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="200px"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <hr />
                            <table style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td style="width: 400px; text-align: left;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="font-size: 11px; width: 270px">
                                                    <asp:Label ID="Label8" runat="server" Text="Valor unidad tributaria (U.V.T)" Visible="False"></asp:Label></td>
                                                <td style="font-size: 11px; width: 120px">
                                                    <asp:TextBox ID="txvValorUVT" runat="server" Visible="False" Width="100px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 270px">
                                                    <asp:Label ID="Label9" runat="server" Text="Porcentaje exento de retención" Visible="False"></asp:Label></td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvPorcentajeRete" runat="server" Visible="False" Width="100px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 270px">
                                                    <asp:Label ID="Label10" runat="server" Text="Porcentaje máximo de aportes en pensión y AFC" Visible="False"></asp:Label></td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvPorcentajeMaximoAFC" runat="server" Visible="False" Width="100px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 270px">
                                                    <asp:Label ID="Label11" runat="server" Text="Patrimonio bruto (DIAN)" Visible="False"></asp:Label></td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvPatrimonioBruto" runat="server" Visible="False" Width="100px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 270px">
                                                    <asp:Label ID="Label12" runat="server" Text="Ingresos Brutos (DIAN)" Visible="False"></asp:Label></td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvIngresosBrutos" runat="server" Visible="False" Width="100px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 270px">
                                                    <asp:Label ID="Label13" runat="server" Text="Porc. Exento de pagos no Const. de salario L.1393" Visible="False"></asp:Label></td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvpExentoSalario1393" runat="server" Visible="False" Width="100px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                    <td style="width: 550px; text-align: left;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="font-size: 11px; width: 230px">
                                                    <asp:Label ID="Label3" runat="server" Text="Máximo valor exento" Visible="False"></asp:Label></td>
                                                <td style="width: 90px">
                                                    <asp:TextBox ID="txvValorMaxExento" runat="server" Visible="False" Width="80px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                                <td style="width: 80px">
                                                    <asp:Label ID="Label18" runat="server" Text="(U.V.T) =" Visible="False"></asp:Label>
                                                </td>
                                                <td style="font-size: 11px;">
                                                    <asp:TextBox ID="txvUVT1" runat="server" Visible="False" Width="120px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 230px">
                                                    <asp:Label ID="Label4" runat="server" Text="Valor máx. de aporte en pensión y AFC" Visible="False"></asp:Label></td>
                                                <td style="width: 90px;">
                                                    <asp:TextBox ID="txvValorMaxAFC" runat="server" Visible="False" Width="80px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                                <td style="width: 80px;">
                                                    <asp:Label ID="Label19" runat="server" Text="(U.V.T) =" Visible="False"></asp:Label>
                                                </td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvUVT2" runat="server" Visible="False" Width="120px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 230px">
                                                    <asp:Label ID="Label14" runat="server" Text="Valor máx. de deducible por vivienda" Visible="False"></asp:Label></td>
                                                <td style="width: 90px;">
                                                    <asp:TextBox ID="txvValorMaxDeducible" runat="server" Visible="False" Width="80px" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                                <td style="width: 80px;">
                                                    <asp:Label ID="Label20" runat="server" Text="(U.V.T) =" Visible="False"></asp:Label>
                                                </td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvUVT3" runat="server" Visible="False" Width="120px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 230px">
                                                    <asp:Label ID="Label15" runat="server" Text="Valor máx. de pagos por salud" Visible="False"></asp:Label></td>
                                                <td style="width: 90px;">
                                                    <asp:TextBox ID="txvValorMaximoSalud" runat="server" Visible="False" Width="80px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                                <td style="width: 80px;">
                                                    <asp:Label ID="Label21" runat="server" Text="(U.V.T) =" Visible="False"></asp:Label>
                                                </td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvUVT4" runat="server" Visible="False" Width="120px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 230px; vertical-align: middle;">
                                                    <asp:Label ID="Label16" runat="server" Text="Valor de dependientes" Visible="False"></asp:Label>
                                                    <asp:TextBox ID="txvpDep" runat="server" Visible="False" Width="50px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                                <td style="width: 90px;">
                                                    <asp:TextBox ID="txvValorDependientes" runat="server" Visible="False" Width="80px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                                <td style="width: 80px;">
                                                    <asp:Label ID="Label22" runat="server" Text="(U.V.T) =" Visible="False"></asp:Label>
                                                </td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvUVT5" runat="server" Visible="False" Width="120px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 11px; width: 230px">
                                                    <asp:Label ID="Label17" runat="server" Text="Valor minimo ingreso para declarante" Visible="False"></asp:Label></td>
                                                <td style="width: 90px;">
                                                    <asp:TextBox ID="txvValorMinimoIngresos" runat="server" Visible="False" Width="80px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                                <td style="width: 80px;">
                                                    <asp:Label ID="Label23" runat="server" Text="(U.V.T) =" Visible="False"></asp:Label>
                                                </td>
                                                <td style="width: 130px; text-align: left;">
                                                    <asp:TextBox ID="txvUVT6" runat="server" Visible="False" Width="120px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>



                                <tr>
                                    <td></td>
                                    <td style="text-align: left;" colspan="2">



                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:CheckBox ID="chkAplicaArt385" runat="server" Text="Aplicar Art. 385 Regimen de impuesto a la renta" Visible="False" CssClass="checkbox checkbox-primary" /></td>
                                                <td style="text-align: left">
                                                    <asp:CheckBox ID="chkRestaIncapacidad" runat="server" Text="Resta Incapadiades de los ingresos base retefuente" Visible="False" CssClass="checkbox checkbox-primary" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:CheckBox ID="chkSalarioIntegral" runat="server" Text="Salario Integral toma base pagos no const. de salario (70%)" Visible="False" CssClass="checkbox checkbox-primary" />
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:CheckBox ID="chkDiasTNL" runat="server" Text="Días TNL entre periodos suma para calculo deducible" Visible="False" CssClass="checkbox checkbox-primary" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                    <td></td>
                                </tr>



                                <tr>
                                    <td></td>
                                    <td style="text-align: left;" colspan="2">
                                        <asp:TextBox ID="txtObservacion" runat="server" placeholder="Observaciones y/o notas..." Visible="False" Width="100%" CssClass="input" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                    </td>

                                    <td></td>
                                </tr>



                            </table>
                        </div>
                    </td>
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
                        <asp:BoundField DataField="ano" HeaderText="Año" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vSalarioMinimo" HeaderText="SMLV" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vAuxilioTransporte" HeaderText="VAT" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Observacion" HeaderText="Observación" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vUVT" HeaderText="vUVT" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vPatrimonioBruto" HeaderText="VPB" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vIngresoBruto" HeaderText="VIB" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pExentoRetencion" HeaderText="pER" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pMaximoaportePension" HeaderText="pMAP" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pExentoSalario1393" HeaderText="pES1393" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pDependientes" HeaderText="PD" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vMaximoExento" HeaderText="VME" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vMaxAporteAFC" HeaderText="VMAAFC" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vMaxDeducibleVivienda" HeaderText="VMDV" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vDependientes" HeaderText="VDepe" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vMinimoingresosDeclarante" HeaderText="VMID" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vUVT1" HeaderText="vUVT1" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vUVT2" HeaderText="vUVT2" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vUVT3" HeaderText="vUVT3" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vUVT4" HeaderText="vUVT4" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vUVT5" HeaderText="vUVT5" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vUVT6" HeaderText="vUVT6" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="cAplicarArt385" HeaderText="cArt385">
                            <ItemStyle HorizontalAlign="Center" Width="5px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="cSalarioIntegral" HeaderText="cSI">
                            <ItemStyle HorizontalAlign="Center" Width="4px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="cRestaIncapacidad" HeaderText="cRI">
                            <ItemStyle HorizontalAlign="Center" Width="4px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="cDiasTNL" HeaderText="dTNL">
                            <ItemStyle HorizontalAlign="Center" Width="4px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="vUVT1" HeaderText="vUVT1" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vMinimoPeriodo" HeaderText="vPMP" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noSueldoST" HeaderText="noSST" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vMaxPagoSalud" HeaderText="VMPS" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
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
