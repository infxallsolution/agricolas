<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Incapacidades.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.Incapacidades" %>

<!DOCTYPE html>

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
            <table class="w-100" >
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
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label13" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlTercero" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="95%" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Contrato" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <table class="auto-style1">
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
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblPeriodoInicial1" runat="server" Text="Concepto" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblPeriodoInicial0" runat="server" Text="Tipo ausentismo" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlTipoIncapacidad" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblPeriodoInicial" runat="server" Text="Diagnostico" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlDiagnostico" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkProrroga" runat="server" OnCheckedChanged="chkProrroga_CheckedChanged" Text="Prorroga" Visible="False" />
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlProrroga" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label3" runat="server" Text="Doc referencia" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left;" colspan="3">
                        <asp:TextBox ID="txtReferencia" runat="server" CssClass="input" Visible="False" Width="95%">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lbFechaInicial" runat="server"  Visible="False">Fecha inicial</asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="input fecha" Font-Bold="True" placeholder="DD/MM/YYYY" ReadOnly="True" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label7" runat="server" Text="No. días" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txvNoDias" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="100px">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label18" runat="server" Text="Fecha Final" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="input" Font-Bold="True" ReadOnly="True" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label14" runat="server" Text="Valor novedad" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txvValorIncapacidad" runat="server" CssClass="input" Visible="False" Width="200px">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label15" runat="server" Text="No. días a pagar" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txvDiasPagar" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txvDiasPagar_TextChanged" Visible="False" Width="100px">0</asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label16" runat="server" Text="Valor a pagar" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txvValorPagar" runat="server" CssClass="input" Visible="False" Width="200px">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label17" runat="server" Text="Pagar a partir de" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlDiasInicio" runat="server" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlDiasInicio_SelectedIndexChanged" AutoPostBack="True" Visible="False" Width="95%">
                            <asp:ListItem Value="1">Primer día</asp:ListItem>
                            <asp:ListItem Value="2">Segundo día</asp:ListItem>
                            <asp:ListItem Value="3">Tercer día</asp:ListItem>
                            <asp:ListItem Value="4">Cuarto día</asp:ListItem>
                            <asp:ListItem Value="5">Quinto día</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:Button ID="btnLiquidar" runat="server" CssClass="botones" OnClick="btnLiquidar_Click" Text="Liquidar" ToolTip="Liquidar" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="4">

                                    <asp:TextBox ID="txtObservacion" runat="server" placeholder="Observaciones y/o notas del ausentismo..." CssClass="input" Height="50px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="4">

                                    <table class="auto-style1">
                                        <tr>
                                            <td></td>
                                            <td style="width: 600px">
                                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" RowHeaderColumn="cuenta" OnRowDeleting="gvSaludPension_RowDeleting" Width="700px" Visible="False">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="imElimina0" CssClass="btn btn-default btn-sm btn-danger fas fa-times " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                    <HeaderStyle CssClass="action-item" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                               
                                               
                                                <asp:TemplateField HeaderText="ValorTotal">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValorTotal" runat="server" onkeyup="formato_numero(this)"
                                                            Text='<%# Eval("valor") %>' Width="100px" CssClass="input"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="ValorPagado">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValorPagado" runat="server" onkeyup="formato_numero(this)"
                                                            Text='<%# Eval("valorPagar") %>' Width="100px" CssClass="input"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                </asp:TemplateField>
                                              
                                            </Columns>
                                            <HeaderStyle CssClass="thead" />
                                            <PagerStyle CssClass="footer" />
                                        </asp:GridView>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
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
                        <asp:BoundField DataField="tercero" HeaderText="Cod" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="nombreEmpleado" HeaderText="NombreEmpleado" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="contrato" HeaderText="Contrato" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero" HeaderText="No." ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaInicial" HeaderText="FechaInicial" ReadOnly="True" DataFormatString="{0:dd/MM/yyy}" SortExpression="Fecha">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaFinal" HeaderText="FechaFinal" ReadOnly="True" DataFormatString="{0:dd/MM/yyy}" SortExpression="Fecha">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreTipoIncapacidad" HeaderText="Ausentismo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:CheckBoxField DataField="liquidada" HeaderText="Liq">
                            <ItemStyle Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="prorroga" HeaderText="Pro">
                            <ItemStyle Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="anulado" HeaderText="Anu">
                            <ItemStyle Width="20px" />
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

    <script type="text/javascript">
        function cuotas() {
            var valorTotal = 0;
            var noCuota = 0;
            var valorCuita = 0;
            if (document.getElementById("txvCuotas").value != null & document.getElementById("txvValor").value != null) {
                noCuota = document.getElementById("txvCuotas").value.replace(/\,/g, '');;
                valorTotal = document.getElementById("txvValor").value.replace(/\,/g, '');;
                if (parseFloat(noCuota) == 0) {
                    document.getElementById("txvValorCuota").value = 0;
                }
                else {
                    document.getElementById("txvValorCuota").value = Math.round(parseFloat(valorTotal) / parseFloat(noCuota));
                    formato_numero(document.getElementById("txvValorCuota"));
                }
            }
        }
    </script>


</body>
</html>
