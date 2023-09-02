<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vacaciones2.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.Vacaciones2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var x = null;
        function Visualizacion(informe, periodoInicial, periodoFinal, empleado, registro) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&empleado=" + empleado + "&periodoInicial=" + periodoInicial + "&periodoFinal=" + periodoFinal + "&registro=" + registro;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function formato(entrada) {
            if (window.event.keyCode != 9) {
                var num = entrada.value.replace(/\,/g, '');
                var totalGlobal = 0;
                var signo = '';

                if (!isNaN(num)) {
                    num = num.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                    num = num.split('').reverse().join('').replace(/^[\,]/, '');
                    entrada.value = num;

                    controlGrillaRef = document.getElementById("gvSaludPension");

                    if (controlGrillaRef != null) {
                        for (i = 1; i < controlGrillaRef.rows.length; i++) {
                            objeto = controlGrillaRef.rows[i].getElementsByTagName("input");

                            if (objeto != null) {
                                signo = controlGrillaRef.rows[i].cells[5].innerHTML + '1';
                                totalGlobal = totalGlobal + parseFloat(signo) * parseFloat(objeto[1].value.replace(/\,/g, ''));
                            }
                        }
                    }
                    totalGlobal = Math.round(totalGlobal, 2);
                    controlValorTotal = document.getElementById("txtValorVacaciones");

                    if (controlValorTotal != null) {
                        totalGlobal = totalGlobal.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        totalGlobal = totalGlobal.split('').reverse().join('').replace(/^[\,]/, '');


                        controlValorTotal.value = totalGlobal;
                        hdValorTotalRef = document.getElementById("hdValorTotalRef");
                        hdValorTotalRef.value = totalGlobal;
                    }

                }
                else {
                    alert('Solo se permiten números');
                    entrada.value = entrada.value.replace(/[^\d\.\,]*/g, '');
                }
            }
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
                    <td style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="lblaño" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 120px;">
                        <asp:Label ID="lblPeriodo0" runat="server" Text="Periodo Nomina" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 130px;">
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="320px" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td width="50px" style="width: 15%"></td>
                </tr>
                <tr>
                    <td width="50px" style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Empleado" Visible="False"></asp:Label></td>
                    <td style="text-align: left" colspan="4">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"
                            Visible="False" Width="90%" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkPromedio" runat="server" Text="Promedio" Visible="False" />
                    </td>
                    <td width="50px" style="width: 15%"></td>
                </tr>
                <tr>
                    <td width="50px" style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="lblECcosto" runat="server" Text="Centro de costo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <asp:Label ID="lblCodCcosto" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblCentroCosto" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="lblEUltimaLiquidacion" runat="server" Text="Ultimas vacaciones" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px;">
                        <asp:Label ID="lblUltimaLiquidación" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td width="50px" style="width: 15%"></td>
                </tr>
                <tr>
                    <td width="50px" style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="lblESueldo" runat="server" Text="Sueldo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <asp:Label ID="lblSueldo" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="lblEUltimaLiquidacion0" runat="server" Text="Periodo" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblPeriodoIUvacaciones" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblEguion" runat="server" Text="-" Visible="False"></asp:Label>
                        <asp:Label ID="lblPeriodoFUVacaciones" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td width="50px" style="width: 15%"></td>
                </tr>
                <tr>
                    <td width="50px" style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="lblEDepartamento" runat="server" Text="Departamento" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <asp:Label ID="lblCodDepartamento" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblDepartamento" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="lblEDiasPendientes" runat="server" Text="Dias pendientes" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 130px; text-align: left;">
                        <asp:Label ID="lblDiasPendientes" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td width="50px" style="width: 15%"></td>
                </tr>
                <tr>
                    <td width="50px" style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo vacaciones" Visible="False"></asp:Label></td>
                    <td style="text-align: left; ">
                        <asp:Label ID="lblPeriodoVacaciones" runat="server" Visible="False"></asp:Label></td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="Label6" runat="server" Text="Tipo vacacion" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px;">
                        <asp:DropDownList ID="ddlTipoVacaciones" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"
                            Visible="False" OnSelectedIndexChanged="ddlTipoVacaciones_SelectedIndexChanged">
                            <asp:ListItem Value="1">Disfrutada</asp:ListItem>
                            <asp:ListItem Value="2">Compensada</asp:ListItem>
                            <asp:ListItem Value="3">Compensadas 8/7</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="50px" style="width: 15%"></td>
                </tr>
                <tr>
                    <td width="50px" style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label32" runat="server" Text="Periodo Inicial" Visible="False"></asp:Label></td>
                    <td style="text-align: left; ">
                        <asp:TextBox ID="txtPeriodoInicial" runat="server" CssClass="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" AutoPostBack="True" Visible="False" Enabled="False"></asp:TextBox>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="Label33" runat="server" Text="Periodo Final" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px;">
                        <asp:TextBox ID="txtPeriodoFinal" runat="server" CssClass="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" AutoPostBack="True" Visible="False" Enabled="False"></asp:TextBox>
                    </td>
                    <td width="50px" style="width: 15%"></td>
                </tr>
                <tr>
                    <td style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label34" runat="server" Text="Registro" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="input" Font-Bold="True" Width="150px" AutoPostBack="True" Visible="False" Enabled="False"></asp:TextBox>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="Label31" runat="server" Text="Días  causados" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 250px;">
                        <asp:TextBox ID="txvNoDiasCausados" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="100px" Enabled="False" OnTextChanged="txvNoDiasCausados_TextChanged">0</asp:TextBox>
                    </td>
                    <td style="width: 15%"></td>
                </tr>
                <tr>
                    <td style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="lbFecha" runat="server" Visible="False">Fecha salida</asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" AutoPostBack="True" Visible="False" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="Label30" runat="server" Text="Fecha retorno" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 250px;">
                        <asp:TextBox ID="txtFechaRetorno" runat="server" CssClass="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" AutoPostBack="True" Visible="False" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="width: 15%"></td>
                </tr>
                <tr>
                    <td style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label35" runat="server" Text="Dias tomados" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <asp:TextBox ID="txvNoDiasDisfrutados" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="100px">0</asp:TextBox>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="Label40" runat="server" Text="Dias pagar" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px;">
                        <asp:TextBox ID="txvDiasPagar" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="100px" Enabled="False">0</asp:TextBox>
                    </td>
                    <td style="width: 15%"></td>
                </tr>
                <tr>
                    <td style="width: 15%"></td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="Label37" runat="server" Text="Valor Base" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 110px">
                                    <asp:TextBox ID="txvValorBase" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="100px" Enabled="False">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkPromedioNuevo" runat="server" Text="Nuevo promedio" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="2" style="width: 120px; text-align: left">
                        <asp:Label ID="Label39" runat="server" Text="Días pendientes" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 250px;">
                        <asp:TextBox ID="txvNoDiasPendientes" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="100px">0</asp:TextBox>
                    </td>
                    <td style="width: 15%"></td>
                </tr>
                <tr>
                    <td style="width: 15%"></td>
                    <td style="text-align: left" colspan="5">
                        <asp:TextBox ID="txtObservacion" runat="server" placeholder="Observaciones y/o notas..." CssClass="input" Height="40px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 15%"></td>
                </tr>
                <tr>
                    <td style="width: 15%"></td>
                    <td style="text-align: center" colspan="5">
                        <asp:Button ID="lbPreLiquidar" runat="server" CssClass="botones" OnClick="lbPreLiquidar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de preliquidar?');" Text="Pre-liquidar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                    <td style="width: 15%"></td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align: center">
                        <asp:Panel ID="pnConceptos" runat="server" Visible="False">
                            <table cellspacing="0" id="datosDet" style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td valign="top" style="width: 700px">
                                        <asp:GridView ID="gvSaludPension" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" RowHeaderColumn="cuenta" OnRowDeleting="gvSaludPension_RowDeleting" Width="800px">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                    <HeaderStyle CssClass="action-item" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="codConcepto" HeaderText="Concepto">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="desConcepto" HeaderText="NombreConcepto">
                                                    <HeaderStyle />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cantidad">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Vlr. Total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValorTotal" runat="server" onchange="formato(this)" OnDataBinding="txtValorTotal_DataBinding"
                                                            onkeyup="formato(this)"
                                                            Text='<%# Eval("valorTotal") %>' Width="100px" CssClass="input"></asp:TextBox>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                   
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="signo">
                                                    <HeaderStyle Width="5px" />
                                                    <ItemStyle Width="5px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="noPrestamo">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="bss">
                                                    <ItemStyle Width="5px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <HeaderStyle CssClass="thead" />
                                            <PagerStyle CssClass="footer" />
                                        </asp:GridView>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td valign="top">
                                        <table class="w-100">
                                            <tr>
                                                <td style="width: 100px" valign="top">
                                                    <asp:Label ID="Label41" runat="server" Text="Valor vacaciones" Visible="False"></asp:Label>
                                                </td>
                                                <td style="width: 100px" valign="top">
                                                    <asp:TextBox ID="txtValorVacaciones" runat="server" AutoPostBack="True" CssClass="input" Enabled="False" onkeyup="formato_numero(this)" Visible="False" Width="200px">0</asp:TextBox>
                                                    <asp:HiddenField ID="hdValorTotalRef" runat="server" Value="0" />
                                                </td>
                                                <td style="width: 100px" valign="top">
                                                    <asp:CheckBox ID="chkLiquiNomina" runat="server" AutoPostBack="True" OnCheckedChanged="chkLiquiNomina_CheckedChanged" Text="Paga nomina" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="30" AllowPaging="True" OnRowUpdating="gvLista_RowUpdating">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                 <asp:HiddenField ID="hfRegistro" runat="server" Value='<%# Eval("registro") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="periodoInicial" HeaderText="PeriodoI" DataFormatString="{0:yyyy/MM/dd}">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodoFinal" DataFormatString="{0:yyyy/MM/dd}" HeaderText="PeriodoF">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaSalida" DataFormatString="{0:yyyy/MM/dd}" HeaderText="FSalida">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaRetorno" DataFormatString="{0:yyyy/MM/dd}" HeaderText="FRetorno">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="registro" HeaderText="Cod">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="TipVaca" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="idEmpleado" HeaderText="Cedula">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tercero" HeaderText="CodTer" ShowHeader="False">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="empleado" HeaderText="Empleado">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="diasCausados" HeaderText="DiasC">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="diasPendientes" HeaderText="DiasP">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="año" HeaderText="Año">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodo" HeaderText="Periodo">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorPagado" HeaderText="Val. Pagado">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="pagaNomina" HeaderText="Pag. N" />
                        <asp:CheckBoxField DataField="ejecutado" HeaderText="Eje">
                            <ItemStyle Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                            <ItemStyle HorizontalAlign="Center" Width="10px" CssClass="Items" />
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
