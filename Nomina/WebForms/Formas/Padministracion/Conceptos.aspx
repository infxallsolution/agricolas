<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Conceptos.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.Conceptos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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

            <table style="width: 100%; text-align: left;" id="upCabeza" runat="server" visible="false">
                <tr>
                    <td>
                        </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Código"></asp:Label></td>
                    <td style="text-align: left; width: 370px">
                        <asp:TextBox ID="txtCodigo" runat="server" Width="150px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input" MaxLength="20"></asp:TextBox>
                    </td>
                    <td style="text-align: left" colspan="2"></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label2" runat="server" Text="Descripción"></asp:Label></td>
                    <td style="text-align: left; width: 370px">
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="350px" CssClass="input"></asp:TextBox></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label7" runat="server" Text="Abreviatura"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtAbreviatura" runat="server" Width="95%" CssClass="input" MaxLength="20"></asp:TextBox></td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label6" runat="server" Text="Naturaleza(signo)"></asp:Label></td>
                    <td style="text-align: left; width: 370px">
                        <asp:DropDownList ID="ddlSigno" runat="server" Width="150px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                            <asp:ListItem Value="0">No aplica</asp:ListItem>
                            <asp:ListItem Value="1">Devengado (+)</asp:ListItem>
                            <asp:ListItem Value="2">Deducido (-)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label8" runat="server" Text="Tipo liquidación"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                            <asp:ListItem Value="1">Horas</asp:ListItem>
                            <asp:ListItem Value="2">Días</asp:ListItem>
                            <asp:ListItem Value="3">Valor fijo</asp:ListItem>
                            <asp:ListItem Value="4">Calculado</asp:ListItem>
                            <asp:ListItem Value="5">Fijo periodo</asp:ListItem>
                            <asp:ListItem Value="6">Fijo mes</asp:ListItem>
                            <asp:ListItem Value="7">Valor unidad</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="width: 130px; text-align: left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaBase" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaBase_CheckedChanged" Text="Maneja Base" />
                    </td>
                    <td style="text-align: left; width: 370px">
                        <asp:DropDownList ID="ddlConceptoBase" runat="server" Width="350px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Valor fijo"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValor" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="width: 130px; text-align: left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkValidaPorcentaje" runat="server" AutoPostBack="True" OnCheckedChanged="chkValidaPorcentaje_CheckedChanged" Text="Porcentaje" />
                    </td>
                    <td style="text-align: left; width: 370px">
                        <asp:TextBox ID="txvPorcentaje" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px"></asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label11" runat="server" Text="Valor minimo"></asp:Label></td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvValorMinimo" runat="server" Width="200px" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox></td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label10" runat="server" Text="Control concepto"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 370px">
                        <asp:DropDownList ID="ddlControlConcepto" runat="server" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                            <asp:ListItem Value="0">No aplica</asp:ListItem>
                            <asp:ListItem Value="1">Suma a sueldo</asp:ListItem>
                            <asp:ListItem Value="2">Resta de sueldo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 130px;">
                        <asp:Label ID="Label13" runat="server" Text="Prioridad" ToolTip="Prioridades para los descuentos, el orden que deben ser descontados"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td style="text-align: left; width: 90px;">
                                    <asp:TextBox ID="txvPrioridad" runat="server" CssClass="input" onkeyup="formato_numero(this)" ToolTip="Prioridades para los descuentos, el orden que deben ser descontados" Width="80px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label14" runat="server" Text="No. veces en mes" ToolTip="Prioridades para los descuentos, el orden que deben ser descontados"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txvNoMes" runat="server" CssClass="input" onkeyup="formato_numero(this)" ToolTip="Prioridades para los descuentos, el orden que deben ser descontados" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="text-align: center" colspan="4">
                        <h6>Parametros del concepto</h6>
                        <hr />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkConceptoFijo" runat="server" Text="Concepto fijo" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkControlaSaldo" runat="server" Text="Concepto controla saldo" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkConceptoAusentismo" runat="server" Text="Concepto Ausentismo" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkIngresoGravado" runat="server" Text="Ingreso gravado" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaRango" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaRango_CheckedChanged" Text="Maneja tabla rango" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPrestacionSocial" runat="server" Text="Concepto prestaciones social" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkMostrarFecha" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaRango_CheckedChanged" Text="Mostrar fecha desprendible" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDescuentaDomingo" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaRango_CheckedChanged" Text="Descuenta domingo" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkHabilitaValorTotalEnModificaLiquidacion" runat="server" Text="Maneja valor total sin base" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLiquiddacionNomina" runat="server" Text="No liquida con nomina" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkMostrarDetalle" runat="server" Text="Mostrar detalle desprendible" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDescuentaTranporte" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaRango_CheckedChanged" Text="Descuenta transporte" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkNoMostraDesprendible" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaRango_CheckedChanged" Text="No mostrar en desprendible" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSumaPrestacionSocial" runat="server" Text="Suma día prestaciones sociales" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkMostrarCantidad" runat="server" Text="No mostrar cantidad desprendible" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="text-align: center" colspan="4">
                        <h6>Base de conceptos</h6>
                        <hr />
                        <div style="padding: 5px">
                            <table style="width: 100%; text-align: left;">
                                <tr>
                                    <td>
                                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBaseCensancias" runat="server" Text="Base  de cesantias" />
                                    </td>
                                    <td>
                                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBaseIntereses" runat="server" Text="Base intereses cesantias" />
                                    </td>
                                    <td>
                                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBaseVaciones" runat="server" Text="Base  de vacaciones" />
                                    </td>
                                    <td>
                                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBaseCaja" runat="server" Text="Base caja compensación" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBasePrimas" runat="server" Text="Base  de primas" />
                                    </td>
                                    <td>
                                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBaseSeguridad" runat="server" Text="Base  de seguridad Social" />
                                    </td>
                                    <td>
                                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBaseEmbargo" runat="server" Text="Base  de embargos" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td style="text-align: center" colspan="4">
                        <div id="upRango" runat="server" visible="False">
                            <h6>Rango por concepto</h6>
                            <hr />
                            <div style="padding: 10px">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 150px; text-align: left">
                                            <asp:Label ID="Label12" runat="server" Text="Rango final" Visible="False"></asp:Label></td>
                                        <td style="width: 250px; text-align: left;">
                                            <asp:TextBox ID="txvRangoFinal" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCargar" runat="server" CssClass="botones" OnClick="btnCargar_Click" Text="Cargar" ToolTip="Cancela la operación" Visible="False" />

                                        </td>
                                        <td style="width: 400px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; text-align: left">
                                            <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPorcentajeRango" runat="server" Text="Procentaje" />
                                        </td>
                                        <td style="width: 150px; text-align: left">
                                            <asp:TextBox ID="txvValorRango" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="150px"></asp:TextBox>

                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" colspan="4">
                                            <div style="text-align: center">
                                                <div>
                                                    <asp:GridView ID="gvRangosConcepto" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvCanal_RowDeleting" PageSize="30" Width="100%" Visible="False">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                <HeaderStyle CssClass="action-item" />
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="numero" HeaderText="No." ReadOnly="True">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="rInicio" HeaderText="Rango Inicial" ReadOnly="True">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="rFinal" HeaderText="Rango Final" ReadOnly="True">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Porcentaje">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txvPorRango" runat="server" onkeyup="formato_numero(this)" Text='<%# Eval("porcentaje") %>' CssClass="input" Width="80px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valor">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txvValorRango" runat="server" onkeyup="formato_numero(this)" Text='<%# Eval("valor") %>' CssClass="input" Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" Width="160px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPorRango" runat="server" Checked='<%# Eval("por") %>' Enabled="False" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <HeaderStyle CssClass="thead" />
                                                        <PagerStyle CssClass="footer" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                    <td>
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
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="abreviatura" HeaderText="Abreviatura" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="signo" HeaderText="NT" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoLiquidacion" HeaderText="TL" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="base" HeaderText="Base" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor" HeaderText="Valor" ReadOnly="True" DataFormatString="{0:N}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorMinimo" HeaderText="ValM" ReadOnly="True" DataFormatString="{0:N}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="basePrimas" HeaderText="BP">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="baseCajaCompensacion" HeaderText="BCC">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="baseCesantias" HeaderText="BC">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="baseVacaciones" HeaderText="BV">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="baseIntereses" HeaderText="BI">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="baseSeguridadSocial" HeaderText="BS">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="controlaSaldo" HeaderText="CS">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaRango" HeaderText="MR">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="ingresoGravado" HeaderText="IG">
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="controlConcepto" HeaderText="CC" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="porcentaje" HeaderText="%" ReadOnly="True" DataFormatString="{0:N}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="validaPorcentaje" HeaderText="v%">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="fijo" HeaderText="fijo">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="baseEmbargo" HeaderText="BE">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="prioridad" HeaderText="Prio" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="descuentaDomingo" HeaderText="DD">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="descuentaTransporte" HeaderText="DT">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mostrarFecha" HeaderText="MF">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="noMostrar" HeaderText="NM">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mostrarDetalle" HeaderText="MD">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="ausentismo" HeaderText="AUS">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="prestacionSocial" HeaderText="PS">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="sumaPrestacionSocial" HeaderText="SDPS">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mostrarCantidad" HeaderText="MCan">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="noMes" HeaderText="NM" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Hab.">
                            <ItemTemplate>
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkHabilitaValorTotalEnModificaLiquidacion" runat="server" Checked='<%# Eval("habilitaValorTotal") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
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
