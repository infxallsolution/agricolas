<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prestamos.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.Prestamos" %>

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
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label13" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left;" style="text-align: left;" colspan="3">
                        <asp:DropDownList ID="ddlTercero" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged" Visible="False" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;"></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Contrato" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 360px">
                        <asp:DropDownList ID="ddlContratos" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="95%">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label19" runat="server" Text="Número" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 360px">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="input" MaxLength="20" Visible="False" Width="95%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lbFecha" runat="server"  Visible="False">Fecha</asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 360px">
                        <asp:TextBox ID="txtFecha" runat="server" AutoPostBack="True" CssClass="input fecha" Font-Bold="True" OnTextChanged="txtFecha_TextChanged" ReadOnly="True" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label3" runat="server" Text="Concepto" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 360px">
                        <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="95%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblPeriodoInicial0" runat="server" Text="Año inicial" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 360px">
                        <asp:TextBox ID="txvAñoInicial" runat="server" CssClass="input" onkeyup="formato_numero(this);cuotas()" Visible="False">0</asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblPeriodoInicial" runat="server" Text="Periodo inicial" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 360px">
                        <asp:TextBox ID="txvPeriodoInicial" runat="server" CssClass="input" onkeyup="formato_numero(this);cuotas()" Visible="False">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Valor" Visible="False"></asp:Label></td>
                    <td class="Campos" style="text-align: left; width: 360px">
                        <asp:TextBox ID="txvValor" runat="server" Visible="False" Width="200px" CssClass="input" onkeyup="formato_numero(this);cuotas()">0</asp:TextBox></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblCantidad" runat="server" Text="Cantidad cuotas" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 360px">
                        <asp:TextBox ID="txvCuotas" runat="server" Visible="False" onkeyup="formato_numero(this);cuotas()" CssClass="input">0</asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblValorCuota" runat="server" Text="Valor cuota" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 360px">
                        <asp:TextBox ID="txvValorCuota" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this);cuotas()" Width="200px">0</asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblCantidad0" runat="server" Text="Cuotas pendiente" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 360px">
                        <asp:TextBox ID="txvCuotasPendiente" runat="server" onkeyup="formato_numero(this)" CssClass="input" Visible="False">0</asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblSaldo" runat="server" Text="Valor saldo" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 360px">
                        <asp:TextBox ID="txvSaldo" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" Width="200px">0</asp:TextBox>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblCantidad1" runat="server" Text="Frecuencia" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 360px">
                        <asp:DropDownList ID="ddlFrecuencia" runat="server" Style="margin-top: 0px" Width="95%" CssClass="chzn-select-deselect" Visible="False">
                            <asp:ListItem Value="0">Todos los pagos</asp:ListItem>
                            <asp:ListItem Value="1">Primer pago</asp:ListItem>
                            <asp:ListItem Value="2">Segundo pago</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblDepartamento30" runat="server" Text="Forma de pago" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 360px">
                        <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px; text-align: left">
                        <asp:Label ID="lblCantidad2" runat="server" Text="Doc Referencia" ToolTip="Documento de referencia" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 360px">
                        <asp:TextBox ID="txtDocRef" runat="server" AutoPostBack="True" CssClass="input" Enabled="False" ToolTip="Documento de referencia" Visible="False" Width="95%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="4">
                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="50px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="4"></td>
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
                        <asp:BoundField DataField="empleado" HeaderText="Cod" ReadOnly="True">
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="desEmpleado" HeaderText="NombreEmpleado">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="contrato" HeaderText="Contrato" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo" HeaderText="Número" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True" DataFormatString="{0:dd/MM/yyy}" SortExpression="Fecha">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="desConcepto" HeaderText="Concepto">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor" HeaderText="Valor" ReadOnly="True" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorSaldo" HeaderText="VlSaldo" ReadOnly="True" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="liquidado" HeaderText="Liq">
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
                    document.getElementById("txvSaldo").value = Math.round(parseFloat(valorTotal));
                    document.getElementById("txvCuotasPendiente").value = Math.round(parseFloat(noCuota));
                    formato_numero(document.getElementById("txvValorCuota"));

                }
            }
        }
    </script>

</body>
</html>
