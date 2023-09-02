<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CajaClienteDoble.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxCobrar.CajaClienteDoble" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js"></script>
    <link href="http://app.infos.com/RecursosInfos/css/general.css" rel="stylesheet" />
    <link href="http://app.infos.com/RecursosInfos/lib/chosen187/chosen.css" rel="stylesheet" />

</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%">
            <tr>
                <td></td>
                <td style="width: 1100px">
                    <div style="vertical-align: top; width: 100%; text-align: left" class="principal">
                        <table style="width: 100%; padding: 0; border-collapse: collapse;">
                            <tr>
                                <td style="text-align: left; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver; vertical-align: bottom;">
                                    <asp:Button ID="niimbRegistro" runat="server" CssClass="botones" OnClick="niimbRegistro_Click" Text="Registro" ToolTip="Panel de registro" />
                                    <asp:Button ID="niimbConsulta" runat="server" CssClass="botones" OnClick="niimbConsulta_Click" Text="Consulta" ToolTip="Panel de consulta" />
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="upGeneral" runat="server">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="upRegistro" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="text-align: center">
                                            <div style="padding: 5px 10px 5px 10px;">
                                                <table id="encabezado" style="width: 100%; padding: 0; border-collapse: collapse;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" Text="Nuevo" ToolTip="Habilita el formulario para un nuevo registro" OnClick="nilbNuevo_Click" />
                                                            <asp:Button ID="lbCancelar" runat="server" CssClass="botones" Text="Cancelar" ToolTip="Cancela la operación"  OnClick="lbCancelar_Click" />
                                                            <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos"  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; padding: 0; border-collapse: collapse;">
                                                            <table style="width: 100%; padding: 0; border-collapse: collapse;">
                                                                <tr>
                                                                    <td></td>
                                                                    <td style="width: 90px; height: 25px; text-align: left">
                                                                        <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo docto" ></asp:Label>
                                                                    </td>
                                                                    <td style="width: 350px; height: 25px; text-align: left">
                                                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"  Width="95%">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 65px; height: 25px; text-align: left">
                                                                        <asp:Label ID="lblNumero" runat="server" Text="Número" ></asp:Label>
                                                                    </td>
                                                                    <td style="width: 150px; height: 25px; text-align: left">
                                                                        <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txtNumero_TextChanged"  Width="95%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: left; width: 80px">
                                                                        <asp:LinkButton ID="lbFecha" runat="server" OnClick="lbFecha_Click" Style="color: #003366" >Fecha docto</asp:LinkButton>
                                                                    </td>
                                                                    <td style="text-align: left; width: 100px">
                                                                        <asp:Calendar ID="niCalendarFecha" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFecha_SelectionChanged"  Width="150px">
                                                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                            <SelectorStyle BackColor="#CCCCCC" />
                                                                            <WeekendDayStyle BackColor="FloralWhite" />
                                                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <OtherMonthDayStyle ForeColor="Gray" />
                                                                            <NextPrevStyle VerticalAlign="Bottom" />
                                                                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                        </asp:Calendar>
                                                                        <asp:TextBox ID="txtFecha" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="Gray" OnTextChanged="txtFecha_TextChanged" placeholder="DD/MM/AAAA" ReadOnly="True"  Width="110px"></asp:TextBox>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </div>
                                        </div>
                                        <div style="text-align: center">
                                            <div style="">
                                                <asp:UpdatePanel ID="upCabeza" runat="server" UpdateMode="Conditional" >
                                                    <ContentTemplate>
                                                        <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                                            <div style="text-align: center">
                                                                <div style="">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:Label ID="lblTercero" runat="server" Text="Terceros" ></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;" colspan="5">
                                                                                <asp:TextBox ID="txtFiltroProveedor" runat="server" AutoPostBack="true" CssClass="input" OnTextChanged="txtFiltroProveedor_TextChanged" placeholder="Filtro cliente"  Width="20%"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlTercero" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged" Style="left: 0px; top: 0px"  Width="77%">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:LinkButton ID="lbFechaRecaudo" runat="server" OnClick="lbFechaRecaudo_Click" Style="color: #003366" >Fecha Recaudo</asp:LinkButton>
                                                                            </td>
                                                                            <td style="text-align: left; width: 150px;">
                                                                                <asp:Calendar ID="niCalendarFechaRecaudo" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFechaRecaudo_SelectionChanged"  Width="150px">
                                                                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                    <SelectorStyle BackColor="#CCCCCC" />
                                                                                    <WeekendDayStyle BackColor="FloralWhite" />
                                                                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                    <OtherMonthDayStyle ForeColor="Gray" />
                                                                                    <NextPrevStyle VerticalAlign="Bottom" />
                                                                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                                </asp:Calendar>
                                                                                <asp:TextBox ID="txtFechaRecaudo" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="Gray" OnTextChanged="txtFechaRecaudo_TextChanged" placeholder="DD/MM/AAAA" ReadOnly="True"  Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: left; width: 100px;">
                                                                                <asp:Label ID="lblDocref" runat="server" Text="Referencia" ></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left; width: 120px;">
                                                                                <asp:TextBox ID="txtDocref" runat="server" CssClass="input"  Width="95%"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:Label ID="lblValorRecaudo" runat="server" Text="Valor recaudado" ></asp:Label>
                                                                            </td>
                                                                            <td style="width: 200px; text-align: left">
                                                                                <asp:HiddenField ID="hdValorTotalRef" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hdValorNetoRef" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
                                                                                <asp:HiddenField ID="hdCantidad" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                                                                                <asp:TextBox ID="txvValor" runat="server" CssClass="input" onkeyup="formato_numero(this)"  Width="95%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" ></asp:Label>
                                                                            </td>
                                                                            <td colspan="5" style="text-align: left">
                                                                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" placeholder="Notas y observaciones..." TextMode="MultiLine"  Width="100%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowDeleting" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div style="text-align: center">
                                            <div style="">
                                                <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional" >
                                                    <ContentTemplate>
                                                        <div style="padding: 3px;">
                                                            <table style="width: 1200px; padding: 0; border-collapse: collapse;">
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%; padding: 3px;">
                                                                            <tr>
                                                                                <td style="text-align: left; vertical-align: top; width: 220px;">
                                                                                    <table style="margin: 3px; width: 97%;">
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblMedioPago" runat="server" Text="Medio de pago" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:DropDownList ID="ddlMediosPago" runat="server" CssClass="chzn-select-deselect" Style="left: 0px; top: -1px"  Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMediosPago_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblValorDetalle" runat="server" Text="Valor" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:TextBox ID="txvValorDetalle" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="100%" Wrap="False"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblCaja" runat="server" Text="Cajas" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:DropDownList ID="ddlCaja" runat="server" CssClass="chzn-select-deselect"  Width="100%" style="left: 0px; top: 0px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblCuentaBancaria" runat="server" Text="Cta. Bancaria" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:DropDownList ID="ddlCuentaBancaria" runat="server" CssClass="chzn-select-deselect" Style="left: 0px; top: 0px"  Width="100%">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblBanco" runat="server" Text="Banco" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:DropDownList ID="ddlBanco" runat="server" CssClass="chzn-select-deselect"  Width="100%">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:LinkButton ID="lbFechaDetalle" runat="server" OnClick="lbFechaDetalle_Click" Style="color: #003366" >Fecha detalle</asp:LinkButton>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Calendar ID="niCalendarFechaDetalle" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFechaDetalle_SelectionChanged"  Width="150px">
                                                                                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                                    <SelectorStyle BackColor="#CCCCCC" />
                                                                                                    <WeekendDayStyle BackColor="FloralWhite" />
                                                                                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                                    <OtherMonthDayStyle ForeColor="Gray" />
                                                                                                    <NextPrevStyle VerticalAlign="Bottom" />
                                                                                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                                                </asp:Calendar>
                                                                                                <asp:TextBox ID="txtFechaDetalle" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="Gray" OnTextChanged="txtFechaDetalle_TextChanged" placeholder="DD/MM/AAAA" ReadOnly="True"  Width="110px"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblReferencia" runat="server" Text="Referencia" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:TextBox ID="txtReferencia" runat="server" CssClass="input"  Width="100%" Wrap="False"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblCheque" runat="server" Text="No cheque" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:TextBox ID="txtNoCheque" runat="server" CssClass="input"  Width="100%" Wrap="False"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblDetalle" runat="server" Text="Notas" ></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:TextBox ID="txtDetalle" runat="server" Height="100px" TextMode="MultiLine"  Width="100%"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: center">
                                                                                                <asp:Button ID="btnRegistrar" runat="server" CssClass="botones" OnClick="btnRegistrar_Click" Text="Cargar"  Width="129px" />
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="text-align: left; width: 5px; border-left-style: solid; border-left-width: 1px; border-left-color: silver;"></td>
                                                                                <td style="text-align: left; vertical-align: top;">
                                                                                    <h6 style="text-align: center">Detalle de pago</h6>
                                                                                    <hr />
                                                                                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" Width="100%" ShowFooter="True" OnRowDataBound="gvLista_RowDataBound">
                                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="imbEditar" CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"
                                                                                                        CommandName="Select" ToolTip="Edita el registro seleccionado"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                                                <HeaderStyle CssClass="action-item" />
                                                                                                <FooterStyle CssClass="action-item" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                                                <HeaderStyle CssClass="action-item" />
                                                                                                <FooterStyle CssClass="action-item" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="nombreMedioPago" HeaderText="MedioPago" FooterText="Total">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="valor" DataFormatString="{0:N2}" HeaderText="Valor">
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="nombreBanco" HeaderText="NombreBanco">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="referencia" HeaderText="Referencia">
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="FechaDetalle" HeaderText="Fecha" DataFormatString="{0:d}">
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                               <asp:BoundField DataField="notas" HeaderText="Notas">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="Anul">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAnulado" runat="server" Checked='<%# Eval("anulado") %>' DataField="anulado" Enabled="False" />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="5px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="registro" HeaderText="No.">
                                                                                                <ItemStyle Width="5px" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <FooterStyle CssClass="footer" />
                                                                                        <HeaderStyle CssClass="thead" />
                                                                                        <PagerStyle CssClass="footer" />
                                                                                    </asp:GridView>

                                                                                    <hr />
                                                                                    <h6 style="text-align: center">Detalle Cuenta por cobrar</h6>
                                                                                    <hr />
                                                                                    <asp:GridView ID="gvReferencia" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Font-Size="Small" GridLines="None" Width="100%">
                                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSeleccion" runat="server" Checked="True" CssClass="checkbox checkbox-primary" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle BackColor="White" BorderColor="White" BorderStyle="None" />
                                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" Width="5px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="tipoReferencia" HeaderText="Tipo">
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="numeroReferencia" HeaderText="Número">
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="auxiliar" HeaderText="AuxiliarCxC">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left"  Width="50px"/>
                                                                                            </asp:BoundField>
                                                                                             <asp:BoundField DataField="nombreauxiliar" HeaderText="NombreAuxiliarCxC">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="diasVencimineto" HeaderText="Vence">
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="Valor">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtValorUnitario" runat="server" CssClass="input" onchange="formato(this)" onkeyup="formato(this)" Text='<%# Eval("valor") %>' Width="80px"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Right" />
                                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Right" Width="60px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="Saldo" DataFormatString="{0:N2}" HeaderText="ValorSaldo">
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="thead" />
                                                                                        <PagerStyle CssClass="footer" />
                                                                                    </asp:GridView>
                                                                                    <hr />
                                                                                    <h6 style="text-align: center">Filtros de aplicación CxC</h6>
                                                                                    <hr />
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td style="text-align: left; vertical-align: top">
                                                                                                <table class="w-100">
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 100px">
                                                                                                            <asp:Label ID="lblTercero0" runat="server" Text="Sucursal" ></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlSucursalFiltro" runat="server" CssClass="chzn-select-deselect"  Style="left: 0px; top: 0px"  Width="95%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td colspan="2">
                                                                                                            <asp:CheckBox ID="chkFechaFiltro" runat="server" Text="Filtrar por fecha" AutoPostBack="True" OnCheckedChanged="chkFechaFiltro_CheckedChanged" />
                                                                                                        </td>
                                                                                                        <td colspan="2">
                                                                                                            <asp:CheckBox ID="chkNumeroFiltro" runat="server" Text="Filtrar número" AutoPostBack="True" OnCheckedChanged="chkFichoNumero_CheckedChanged" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 100px">
                                                                                                            <asp:Label ID="lblTercero1" runat="server" Text="Auxiliar" ></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlAuxiliarFiltro" runat="server" CssClass="chzn-select-deselect" Style="left: 0px; top: 0px"  Width="95%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 60px">
                                                                                                            <asp:LinkButton ID="lbFechaInicialFiltro" runat="server" OnClick="lbFechaInicialFiltro_Click" Style="color: #003366" >Inicial</asp:LinkButton>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 180px">
                                                                                                            <asp:Calendar ID="niCalendarFechaInicioFiltro" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFechaInicioFiltro_SelectionChanged"  Width="150px">
                                                                                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                                                <SelectorStyle BackColor="#CCCCCC" />
                                                                                                                <WeekendDayStyle BackColor="FloralWhite" />
                                                                                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                                                <OtherMonthDayStyle ForeColor="Gray" />
                                                                                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                                                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                                                            </asp:Calendar>
                                                                                                            <asp:TextBox ID="txtFechaInicialFiltro" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="Gray" OnTextChanged="txtFechaInicialFiltro_TextChanged" placeholder="DD/MM/AAAA" ReadOnly="True"  Width="110px"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 60px">
                                                                                                            <asp:Label ID="lblTercero4" runat="server" Text="Inicial" ></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 120px">
                                                                                                            <asp:TextBox ID="txvInicialFiltro" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="95%" Wrap="False"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 100px">
                                                                                                            <asp:Label ID="lblTercero2" runat="server" Text="Tipo docto" ></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlTipoDocumentoFiltro" runat="server" CssClass="chzn-select-deselect"  Style="left: 0px; top: 0px"  Width="95%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 60px">
                                                                                                            <asp:LinkButton ID="lbFechaFinalFiltro" runat="server" OnClick="lbFechaFinalFiltro_Click" Style="color: #003366" >Fiinal</asp:LinkButton>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 180px">
                                                                                                            <asp:Calendar ID="niCalendarFechaFinalFiltro" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFechaFinalFiltro_SelectionChanged"  Width="150px">
                                                                                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                                                <SelectorStyle BackColor="#CCCCCC" />
                                                                                                                <WeekendDayStyle BackColor="FloralWhite" />
                                                                                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                                                <OtherMonthDayStyle ForeColor="Gray" />
                                                                                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                                                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                                                            </asp:Calendar>
                                                                                                            <asp:TextBox ID="txtFechaFinalFiltro" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" ForeColor="Gray" OnTextChanged="txtFechaFinalFiltro_TextChanged" placeholder="DD/MM/AAAA" ReadOnly="True"  Width="110px"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 60px">
                                                                                                            <asp:Label ID="lblTercero5" runat="server" Text="Final" ></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 120px">
                                                                                                            <asp:TextBox ID="txvFinalFiltro" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="95%" Wrap="False"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 100px"></td>
                                                                                                        <td colspan="4">
                                                                                                            <asp:Button ID="nibtnBuscar" runat="server" CssClass="botones" OnClick="nibtnBuscar_Click" Text="Buscar" ToolTip="Carga los filtros"  />
                                                                                                        </td>
                                                                                                        <td style="text-align: left; width: 120px"></td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: left; vertical-align: top"></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMediosPago" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlMediosPago" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="RowUpdating" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="RowUpdating" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upConsulta" runat="server"  UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                    <div style="text-align: center">
                                        <div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 100px; height: 25px; text-align: left"></td>
                                                    <td style="width: 150px; height: 25px; text-align: left">
                                                        <asp:DropDownList ID="niddlCampo" runat="server" ToolTip="Selección de campo para busqueda"
                                                            Width="250px" CssClass="chzn-select-deselect">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 100px; height: 25px; text-align: left">
                                                        <asp:DropDownList ID="niddlOperador" runat="server" ToolTip="Selección de operador para busqueda"
                                                            Width="150px" AutoPostBack="True" CssClass="chzn-select-deselect">
                                                            <asp:ListItem Value="like">Contiene</asp:ListItem>
                                                            <asp:ListItem Value="&lt;&gt;">Diferente</asp:ListItem>
                                                            <asp:ListItem Value="between">Entre</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="=">Igual</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">Mayor o Igual</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">Mayor que</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">Menor o Igual</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">Menor</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="width: 110px; height: 25px; text-align: left">
                                                        <asp:TextBox ID="nitxtValor1" runat="server" Width="150px" CssClass="input"></asp:TextBox><asp:TextBox
                                                            ID="nitxtValor2" runat="server"  Width="150px" CssClass="input"></asp:TextBox></td>
                                                    <td style="width: 20px; height: 25px; text-align: center">
                                                        <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter " OnClick="niimbAdicionar_Click"></asp:LinkButton>

                                                    </td>
                                                    <td style="width: 20px; text-align: center">
                                                        <asp:LinkButton runat="server" ID="imbBusqueda"  CssClass="btn btn-default btn-sm btn-success fa fa-search" OnClick="imbBusqueda_Click"></asp:LinkButton>
                                                    </td>
                                                    <td style="width: 100px; height: 25px; text-align: left">
                                                        <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label></td>
                                                    <td style="background-position-x: right; width: 100px; height: 25px"></td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%; padding: 0; border-collapse: collapse;">
                                                <tr>
                                                    <td style="text-align: center;"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="text-align: center">
                                                            <asp:GridView ID="gvParametros" runat="server" Width="400px" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" CssClass="table table-bordered table-sm  table-hover table-striped grillas">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-minus-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                        <HeaderStyle CssClass="action-item" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="campo" HeaderText="Campo"></asp:BoundField>
                                                                    <asp:BoundField DataField="operador" HeaderText="Operador"></asp:BoundField>
                                                                    <asp:BoundField DataField="valor" HeaderText="Valor"></asp:BoundField>
                                                                    <asp:BoundField DataField="valor2" HeaderText="Valor 2"></asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="thead" />
                                                                <PagerStyle CssClass="footer" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%; padding: 0; border-collapse: collapse;">
                                                <tr>
                                                    <td style="width: 100%; text-align: left;">
                                                        <asp:GridView ID="gvTransaccion" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" CssClass="table table-bordered table-sm  table-hover table-striped grillas"  OnRowCommand="gvTransaccion_RowCommand">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Anu">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-ban " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                    <HeaderStyle CssClass="action-item" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="numero" HeaderText="Numero">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="año" HeaderText="Año">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="mes" HeaderText="Mes">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="notas" HeaderText="Observaciones">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Detalle">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="detalle" CssClass="btn btn-default btn-sm btn-primary  fa fa-info-circle" CommandName="Update" ToolTip="Detalle de la transaccion"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5px" />
                                                                </asp:TemplateField>
                                                                <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:CheckBoxField>
                                                                <asp:CheckBoxField DataField="aprobado" HeaderText="Aprd">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:CheckBoxField>
                                                                <asp:CheckBoxField DataField="cerrado" HeaderText="Cerd">
                                                                    <ItemStyle Width="5px" />
                                                                </asp:CheckBoxField>

                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="ibImprimir" CssClass="btn btn-default btn-sm btn-primary fa fa-print " CommandName="imprimir" ToolTip="Imprimir transacción seleccionada"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                    <HeaderStyle CssClass="action-item" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="thead" />
                                                            <PagerStyle CssClass="footer" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </td>
                <td></td>
            </tr>
        </table>
        <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
        <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
        <script src="http://app.infos.com/recursosinfos/lib/jquery-ui/jquery-ui.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/bootstrap/dist/js/bootstrap.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/moment/moment.js"></script>
        <script src="http://app.infos.com/RecursosInfos/js/daterangepicker.js" type="text/javascript"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/iCheck/icheck.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/chosen187/chosen.jquery.js"></script>
        <script src="http://app.infos.com/RecursosInfos/js/core.js"></script>
        <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/plugins/localisation/jquery.localisation-min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/ui.multiselect.js"></script>

    </form>
</body>
</html>
