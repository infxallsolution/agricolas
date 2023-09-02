<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notas.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxCobrar.Notas" %>

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
                                                            <td style="width: 125px; height: 25px; text-align: left">
                                                                <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" ></asp:Label></td>
                                                            <td style="width: 400px; height: 25px; text-align: left">
                                                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                                                     Width="370px">
                                                                </asp:DropDownList></td>
                                                            <td style="width: 65px; height: 25px; text-align: left">
                                                                <asp:Label ID="lblNumero" runat="server" Text="Numero" ></asp:Label></td>
                                                            <td style="width: 150px; height: 25px; text-align: left">
                                                                <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" OnTextChanged="txtNumero_TextChanged"
                                                                     Width="150px" CssClass="input"></asp:TextBox></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
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
                                                                                <asp:LinkButton ID="lbFecha" runat="server" OnClick="lbFecha_Click"
                                                                                     Style="color: #003366">Fecha</asp:LinkButton></td>
                                                                            <td style="text-align: left">
                                                                                <asp:Calendar ID="niCalendarFecha" runat="server" BackColor="White" BorderColor="#999999"
                                                                                    CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                                                    ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFecha_SelectionChanged"
                                                                                     Width="150px">
                                                                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                    <SelectorStyle BackColor="#CCCCCC" />
                                                                                    <WeekendDayStyle BackColor="FloralWhite" />
                                                                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                    <OtherMonthDayStyle ForeColor="Gray" />
                                                                                    <NextPrevStyle VerticalAlign="Bottom" />
                                                                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                                </asp:Calendar>
                                                                                <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" placeholder="DD/MM/AAAA" ForeColor="Gray" ReadOnly="True"  CssClass="input" Width="20%" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:Label ID="lblCondicionPago" runat="server" Text="Condición Pago" ></asp:Label>
                                                                            </td>
                                                                            <td style="width: 250px; text-align: left">
                                                                                <asp:DropDownList ID="ddlCondicionPago" runat="server" CssClass="chzn-select-deselect"  Width="250px">
                                                                                </asp:DropDownList>
                                                                                <asp:HiddenField ID="hdValorTotalRef" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hdValorNetoRef" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
                                                                                <asp:HiddenField ID="hdCantidad" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:Label ID="lblTercero" runat="server" Text="Tercero" ></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left">
                                                                                <asp:TextBox ID="txtFiltroProveedor" runat="server" CssClass="input" placeholder="Filtro cliente" AutoPostBack="true"  Width="20%" OnTextChanged="txtFiltroProveedor_TextChanged"></asp:TextBox>

                                                                                <asp:DropDownList ID="ddlTercero" runat="server"  Width="75%" CssClass="chzn-select-deselect" AutoPostBack="True" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged" Style="left: 0px; top: 0px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="width: 100px; text-align: left;">
                                                                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" ></asp:Label>
                                                                            </td>
                                                                            <td style="width: 250px; text-align: left;">
                                                                                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="chzn-select-deselect"  Width="250px" style="left: 0px; top: 0px" AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:Label ID="lblDocRef" runat="server" Text="Doc referencia" ></asp:Label>
                                                                            </td>
                                                                            <td colspan="3" style="text-align: left">
                                                                                <asp:DropDownList ID="ddlDocRef" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlDocRef_SelectedIndexChanged" Style="left: 0px; top: 0px"  Width="85%">
                                                                                </asp:DropDownList>
                                                                                <asp:Button ID="btnRefrescarDocRef" runat="server" CssClass="botones" OnClick="btnRefrescarDocRef_Click" Text="Refrescar"  Width="129px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; text-align: left">
                                                                                <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" ></asp:Label>
                                                                            </td>
                                                                            <td style="height: 10px; text-align: left" colspan="3">
                                                                                <asp:TextBox ID="txtObservacion" runat="server" placeholder="Notas y observaciones..." CssClass="input" Height="40px" TextMode="MultiLine"  Width="100%"></asp:TextBox>
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
                                                        <div style="border: 1px solid silver; padding: 5px;">
                                                            <table style="width: 1200px; padding: 0; border-collapse: collapse;">
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%; padding: 5px;">
                                                                            <tr>
                                                                                <td style="text-align: left;" colspan="3">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td style="width: 60px">
                                                                                                <asp:Label ID="lblProducto" runat="server" Text="Item" ></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtFiltroProducto" placeholder="Filtro item" runat="server" CssClass="input" AutoPostBack="true"  Width="20%" OnTextChanged="txtFiltroProducto_TextChanged"></asp:TextBox>

                                                                                                <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged"  Width="78%" style="left: 0px; top: 0px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="text-align: left; width: 80px">
                                                                                                <asp:Label ID="lblUmedida" runat="server" Text="Und Medida" ></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 160px; text-align: left">
                                                                                                <asp:DropDownList ID="ddlUmedida" runat="server" CssClass="chzn-select-deselect"  Width="100%">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left; vertical-align: top; width: 160px;">
                                                                                    <table style="padding: 0px">
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:TextBox ID="txvCantidad" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px" Wrap="False"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblValorUnitario" runat="server" Text="Valor Unitario" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:TextBox ID="txvValorUnitario" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px" Wrap="False"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblDestino" runat="server" Text="Destino" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:DropDownList ID="ddlDestino" runat="server" CssClass="chzn-select-deselect"  Width="200px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblBodega" runat="server" Text="Bodega" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:DropDownList ID="ddlBodega" runat="server" CssClass="chzn-select-deselect"  Width="200px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblCcosto" runat="server" Text="C. Costo" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:DropDownList ID="ddlCcosto" runat="server" CssClass="chzn-select-deselect"  Width="200px">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:Label ID="lblDetalle" runat="server" Text="Detalle" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: left">
                                                                                                <asp:TextBox ID="txtDetalle" runat="server" TextMode="MultiLine"  Width="200px" CssClass="input" Height="80px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; text-align: center">
                                                                                                <asp:Button ID="btnRegistrar" runat="server" CssClass="botones" Text="Cargar" OnClick="btnRegistrar_Click"  Width="129px" />
                                                                                                <%--<asp:ImageButton ID="btnRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCargar.png" OnClick="btnRegistrar_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCargar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnCargarN.png'" Style="margin-bottom: 0px; height: 21px;"  />--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="text-align: left; width: 1px; border-left-style: solid; border-left-width: 1px; border-left-color: silver;"></td>
                                                                                <td style="text-align: left; vertical-align: top;">
                                                                                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" Width="100%">
                                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="imbEditar" CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"
                                                                                                        CommandName="Select" ToolTip="Edita el registro seleccionado"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                                                <HeaderStyle CssClass="action-item" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                                                <HeaderStyle CssClass="action-item" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="producto" HeaderText="Item">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="uMedida" HeaderText="UMed">
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cant">
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="valorUnitario" DataFormatString="{0:N2}" HeaderText="VUnit">
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="valorTotal" DataFormatString="{0:N2}" HeaderText="VTotal">
                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="5px" />
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
                                                                                        <HeaderStyle CssClass="thead" />
                                                                                        <PagerStyle CssClass="footer" />
                                                                                    </asp:GridView>
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td style="text-align: left; vertical-align: top">
                                                                                                <asp:GridView ID="gvImpuesto" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Font-Size="Small" GridLines="None" Width="95%">
                                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="concepto" HeaderText="Id">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="nombreConcepto" HeaderText="Impuesto">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="tasa" DataFormatString="{0:N2}" HeaderText="%Tasa">
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="baseGravable" DataFormatString="{0:N2}" HeaderText="%Base">
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="baseMinima" DataFormatString="{0:N2}" HeaderText="BaseMin">
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="valorImpuesto" DataFormatString="{0:N2}" HeaderText="ValorImpuesto                                                                              ">
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                    <HeaderStyle CssClass="thead" />
                                                                                                    <PagerStyle CssClass="footer" />
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                            <td style="text-align: left; width: 240px; vertical-align: top;">
                                                                                                <table style="width: 240px">
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 80px">
                                                                                                            <asp:Label ID="nilblValorTotal" runat="server" CssClass="auto-style2" Text="Total bruto"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: left;">
                                                                                                            <asp:TextBox ID="nitxtTotalValorBruto" runat="server" CssClass="input" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 80px">
                                                                                                            <asp:Label ID="nilblValorTotal1" runat="server" CssClass="auto-style3" Text="- Retención"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: left;">
                                                                                                            <asp:TextBox ID="nitxtTotalRetencion" runat="server" CssClass="input" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 80px">
                                                                                                            <asp:Label ID="nilblValorTotal0" runat="server" CssClass="auto-style3" Text="+ Impuesto"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: left;">
                                                                                                            <asp:TextBox ID="nitxtTotalImpuesto" runat="server" CssClass="input" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="text-align: left; width: 80px">
                                                                                                            <asp:Label ID="nilblValorTotal2" runat="server" CssClass="auto-style2" Text="Total"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: left;">
                                                                                                            <asp:TextBox ID="nitxtTotal" runat="server" CssClass="input" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: left; vertical-align: top">
                                                                                                <h5 style="text-align: center">Cuenta por cobrar</h5>
                                                                                                <hr />
                                                                                                <asp:GridView ID="gvCxC" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Font-Size="Small" GridLines="None" Width="95%" ShowFooter="True" OnRowDataBound="gvCxC_RowDataBound">
                                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="Auxiliar" FooterText="Total">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:DropDownList ID="ddlAuxiliarCxC" data-placeholder="Seleccione una opción..." DataSource='<%# dvPuc() %>' SelectedValue='<%# Eval("cuenta") %>' DataTextField="cadena" DataValueField="codigo" runat="server" CssClass="chzn-select-deselect" Width="100%">
                                                                                                                </asp:DropDownList>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="debito" HeaderText="Debito" DataFormatString="{0:N2}">
                                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="credito" HeaderText="Credito" DataFormatString="{0:N2}">
                                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                    <FooterStyle CssClass="footer" />
                                                                                                    <HeaderStyle CssClass="thead" />
                                                                                                    <PagerStyle CssClass="footer" />

                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                            <td style="text-align: center; width: 240px; vertical-align: middle;">
                                                                                                <asp:Button ID="btnRefrezcar" runat="server" CssClass="botones" OnClick="btnRefrezcar_Click" Text="Calcular CxC"  Width="129px" />
                                                                                            </td>
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlProducto" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="RowUpdating" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="upConsulta" runat="server"  UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="padding: 3px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                            <div style="text-align: center">
                                                <div>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td></td>
                                                            <td style="width: 300px; height: 25px; text-align: left">
                                                                <asp:DropDownList ID="niddlCampo" runat="server" ToolTip="Selección de campo para busqueda"
                                                                    Width="95%" CssClass="chzn-select-deselect">
                                                                </asp:DropDownList></td>
                                                            <td style="width: 200px; height: 25px; text-align: left">
                                                                <asp:DropDownList ID="niddlOperador" runat="server" ToolTip="Selección de operador para busqueda"
                                                                    Width="95%" AutoPostBack="True" CssClass="chzn-select-deselect">
                                                                    <asp:ListItem Value="like">Contiene</asp:ListItem>
                                                                    <asp:ListItem Value="&lt;&gt;">Diferente</asp:ListItem>
                                                                    <asp:ListItem Value="between">Entre</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="=">Igual</asp:ListItem>
                                                                    <asp:ListItem Value="&gt;=">Mayor o Igual</asp:ListItem>
                                                                    <asp:ListItem Value="&gt;">Mayor que</asp:ListItem>
                                                                    <asp:ListItem Value="&lt;=">Menor o Igual</asp:ListItem>
                                                                    <asp:ListItem Value="&lt;">Menor</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td style="width: 160px; height: 25px; text-align: left">
                                                                <asp:TextBox ID="nitxtValor1" runat="server" Width="95%" CssClass="input"></asp:TextBox><asp:TextBox
                                                                    ID="nitxtValor2" runat="server"  Width="95%" CssClass="input"></asp:TextBox></td>
                                                            <td style="width: 50px; height: 25px; text-align: center">
                                                                <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter " OnClick="niimbAdicionar_Click"></asp:LinkButton>

                                                            </td>
                                                            <td style="width: 50px; text-align: center">
                                                                <asp:LinkButton runat="server" ID="imbBusqueda"  CssClass="btn btn-default btn-sm btn-success fa fa-search" OnClick="imbBusqueda_Click"></asp:LinkButton>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                    <table style="width: 100%; padding: 0; border-collapse: collapse;">
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="text-align: center">
                                                                    <table class="w-100">
                                                                        <tr>
                                                                            <td></td>
                                                                            <td style="width: 400px">
                                                                                <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" Width="400px">
                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="imElimina" runat="server" CommandName="Delete" CssClass="btn btn-default btn-sm btn-danger fa fa-minus-circle " ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="action-item" HorizontalAlign="Center" Width="20px" />
                                                                                            <HeaderStyle CssClass="action-item" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="campo" HeaderText="Campo" />
                                                                                        <asp:BoundField DataField="operador" HeaderText="Operador" />
                                                                                        <asp:BoundField DataField="valor" HeaderText="Valor" />
                                                                                        <asp:BoundField DataField="valor2" HeaderText="Valor 2" />
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="thead" />
                                                                                    <PagerStyle CssClass="footer" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                            <td></td>
                                                                        </tr>
                                                                    </table>
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
                                                                        <asp:BoundField DataField="notas" HeaderText="Observaciones"></asp:BoundField>
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="RowUpdating" />
                            </Triggers>
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
