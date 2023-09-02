<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntradasSU.aspx.cs" Inherits="Almacen.WebForms.Formas.Ptransaccion.EntradasSU" %>

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
        <div style="text-align: center">
            <div style="display: inline-block">
                <div style="vertical-align: top; width: 1100px; height: 600px; text-align: left" class="principal">
                    <table style="width: 1000px; padding: 0; border-collapse: collapse;">
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
                                            <td style="background-image: url('../../Imagenes/botones/BotonIzq.png'); background-repeat: no-repeat; text-align: center;" colspan="3">
                                                <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo" ToolTip="click para realizar nuevo registro" />
                                                <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="click para cancelar el registro" />
                                                <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" Text="Registrar" ToolTip="Registrar transaccion" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;" colspan="3">
                                                <asp:Label ID="nilblInformacion" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 200px; background-repeat: no-repeat; text-align: right;"></td>
                                            <td style="width: 600px; text-align: center; padding: 0; border-collapse: collapse;">
                                                <table style="width: 600px; padding: 0; border-collapse: collapse;">
                                                    <tr>
                                                        <td style="width: 125px; height: 25px; text-align: left">
                                                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" Visible="False"></asp:Label></td>
                                                        <td style="width: 260px; height: 25px; text-align: left">
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chosen-select-deselect input-group" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                                                Visible="False" Width="250px">
                                                            </asp:DropDownList></td>
                                                        <td style="width: 65px; height: 25px; text-align: left">
                                                            <asp:Label ID="lblNumero" runat="server" Text="Numero" Visible="False"></asp:Label></td>
                                                        <td style="width: 150px; height: 25px; text-align: left">
                                                            <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" OnTextChanged="txtNumero_TextChanged"
                                                                Visible="False" Width="150px" CssClass="input"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 200px"></td>
                                        </tr>
                                    </table>
                                    <div style="text-align: center">
                                        <div style="display: inline-block">
                                            <asp:UpdatePanel ID="upCabeza" runat="server" UpdateMode="Conditional" Visible="False">
                                                <ContentTemplate>
                                                    <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                                        <div style="text-align: center">
                                                            <div style="display: inline-block">
                                                                <table style="width: 1000px;">
                                                                    <tr>
                                                                        <td style="vertical-align: top; width: 125px; text-align: left">
                                                                            <asp:LinkButton ID="lbFecha" runat="server" OnClick="lbFecha_Click"
                                                                                Visible="False" Style="color: #003366">Fecha</asp:LinkButton></td>
                                                                        <td style="vertical-align: top; width: 175px; text-align: left">
                                                                            <asp:Calendar ID="niCalendarFecha" runat="server" BackColor="White" BorderColor="#999999"
                                                                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                                                ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFecha_SelectionChanged"
                                                                                Visible="False" Width="150px">
                                                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                <SelectorStyle BackColor="#CCCCCC" />
                                                                                <WeekendDayStyle BackColor="FloralWhite" />
                                                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                <OtherMonthDayStyle ForeColor="Gray" />
                                                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                            </asp:Calendar>
                                                                            <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" ForeColor="Gray" ReadOnly="True" Visible="False" CssClass="input" Width="150px" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                                                                        </td>
                                                                        <td style="vertical-align: top; width: 120px; text-align: left">
                                                                            <asp:Label ID="lblDocref" runat="server" Text="Docto referencia" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td style="vertical-align: top; width: 400px; text-align: left">
                                                                            <asp:TextBox ID="txtDocref" runat="server" CssClass="input" Visible="False" Width="160px"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdValorTotalRef" runat="server" Value="0" />
                                                                            <asp:HiddenField ID="hdValorNetoRef" runat="server" Value="0" />
                                                                            <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
                                                                            <asp:HiddenField ID="hdCantidad" runat="server" Value="0" />
                                                                            <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 125px; height: 10px; text-align: left">
                                                                            <asp:Label ID="lblTercero" runat="server" Text="Proveedor" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 175px; height: 10px; text-align: left">
                                                                            <asp:DropDownList ID="ddlTercero" runat="server" Visible="False" Width="350px" data-placeholder="Seleccione una opción..." CssClass="chosen-select-deselect input-group" AutoPostBack="True" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 100px; height: 10px; text-align: left;">
                                                                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 400px; height: 10px; text-align: left;">
                                                                            <asp:DropDownList ID="ddlSucursal" runat="server" data-placeholder="Seleccione una opción..." CssClass="chosen-select-deselect input-group" Visible="False" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 125px; height: 10px; text-align: left">
                                                                            <asp:Label ID="lblOc" runat="server" Text="Orden compra" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td class="auto-style4" colspan="3" style="text-align: left">
                                                                            <asp:DropDownList ID="ddlOC" runat="server" AutoPostBack="True" CssClass="chosen-select-deselect input-group" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlOC_SelectedIndexChanged" Visible="False" Width="700px">
                                                                            </asp:DropDownList>
                                                                               <asp:Label ID="lblFechaReferecia" runat="server" Text="" Visible="False"></asp:Label>
                                                                             <asp:TextBox ID="txtFechaReferencia" runat="server" CssClass="input"  Visible="False" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 125px; height: 10px; text-align: left">
                                                                            <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td style="height: 10px; text-align: left" colspan="3">
                                                                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
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
                                        <div style="display: inline-block">
                                            <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional" Visible="False">
                                                <ContentTemplate>
                                                    <div style="padding: 5px">
                                                        <table style="width: 1100px; padding: 0; border-collapse: collapse; border: 1px solid silver;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="text-align: left;" colspan="3">
                                                                                <table style="width: 100%" cellpadding="0">
                                                                                    <tr>
                                                                                        <td style="width: 100px">
                                                                                            <asp:Label ID="lblProducto" runat="server" Text="Selecciona Item" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlProducto" runat="server" data-placeholder="Seleccione una opción..." AutoPostBack="True" CssClass="chosen-select-deselect input-group" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" Visible="False" Width="690px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="text-align: left; width: 80px">
                                                                                            <asp:Label ID="lblUmedida" runat="server" Text="Und Medida" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                        <td style="width: 200px; text-align: left">
                                                                                            <asp:DropDownList ID="ddlUmedida" data-placeholder="Seleccione una opción..." runat="server" CssClass="chosen-select-deselect input-group" Visible="False" Width="200px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left; vertical-align: top; width: 160px;">
                                                                                <table cellpadding="0" style="padding: 0px">
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:TextBox ID="txvCantidad" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px" Wrap="False"></asp:TextBox>
                                                                                            <asp:HiddenField ID="hfCantidadOCR" runat="server" />
                                                                                            <asp:HiddenField ID="hfocd" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:Label ID="lblValorUnitario" runat="server" Text="Valor Unitario" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:TextBox ID="txvValorUnitario" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px" Wrap="False"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:Label ID="lblValorTotal" runat="server" Text="Valor Total" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:TextBox ID="txvValorTotal" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px" Wrap="False"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:Label ID="lblDestino" runat="server" Text="Destino" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:DropDownList ID="ddlDestino" data-placeholder="Seleccione una opción..." runat="server" CssClass="chosen-select-deselect input-group" Visible="False" Width="200px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:Label ID="lblBodega" runat="server" Text="Bodega" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:DropDownList ID="ddlBodega" data-placeholder="Seleccione una opción..." runat="server" CssClass="chosen-select-deselect input-group" Visible="False" Width="200px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:Label ID="lblCcosto" runat="server" Text="C. Costo" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:DropDownList ID="ddlCcosto" data-placeholder="Seleccione una opción..." runat="server" CssClass="chosen-select-deselect input-group" Visible="False" Width="200px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:Label ID="lblDetalle" runat="server" Text="Detalle" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: left">
                                                                                            <asp:TextBox ID="txtDetalle" runat="server" TextMode="MultiLine" Visible="False" Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: center">
                                                                                            <asp:Button ID="btnRegistrar" runat="server" CssClass="botones" OnClick="btnRegistrar_Click" Text="Registrar" ToolTip="click para realizar nuevo registro" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td style="text-align: left; width: 1px; border-left-style: solid; border-left-width: 1px; border-left-color: silver;"></td>
                                                                            <td style="text-align: left; vertical-align: top;">
                                                                                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" Width="100%">
                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                    <Columns>
                                                                                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                                                                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                                                                            <HeaderStyle CssClass="action-item" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="action-item" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="producto" HeaderText="Item"></asp:BoundField>
                                                                                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                                                                            <ItemStyle Font-Size="7.7pt" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="uMedida" HeaderText="UMed"></asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Bodega">
                                                                                            <ItemTemplate>
                                                                                                <asp:HiddenField ID="hfOc" runat="server" Value='<%# Eval("oc") %>' />
                                                                                                <asp:DropDownList ID="ddlBodega" data-placeholder="Seleccione una opción..." runat="server" CssClass="chosen-select-deselect input-group" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlBodega_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="cantidad"  HeaderText="Cant"></asp:BoundField>
                                                                                        <asp:BoundField DataField="valorUnitario"  HeaderText="Vl. Unitario"></asp:BoundField>
                                                                                        <asp:BoundField DataField="valorTotal"  HeaderText="Vl. Total"></asp:BoundField>
                                                                                        <asp:BoundField DataField="destino" HeaderText="Destino"></asp:BoundField>
                                                                                        <asp:BoundField DataField="ccosto" HeaderText="Ccosto"></asp:BoundField>

                                                                                        <asp:TemplateField HeaderText="Anul">
                                                                                            <ItemTemplate>
                                                                                                <asp:HiddenField ID="hfCantidadOC" runat="server" Value='<%#Eval("saldo") %>' />
                                                                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAnulado" AutoPostBack="true" runat="server" Checked='<%# Eval("anulado") %>' DataField="anulado" Enabled="False" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="registro" HeaderText="No."></asp:BoundField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="thead" />
                                                                                    <PagerStyle CssClass="footer" />
                                                                                </asp:GridView>
                                                                                <table cellpadding="0" style="width: 100%">
                                                                                    <tr>
                                                                                        <td style="text-align: left; vertical-align: top">
                                                                                            <asp:GridView ID="gvImpuesto" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Font-Size="Small" GridLines="None" Width="600px">
                                                                                                <AlternatingRowStyle CssClass="alt" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="concepto" HeaderText="Id"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="nombreConcepto" HeaderText="Impuesto"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="tasa" DataFormatString="{0:N2}" HeaderText="%Tasa"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="baseGravable" DataFormatString="{0:N2}" HeaderText="%Base"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="baseMinima" DataFormatString="{0:N2}" HeaderText="BaseMin"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="valorImpuesto" DataFormatString="{0:N2}" HeaderText="ValorImpuesto                                                                              "></asp:BoundField>
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
                                                                                                        <asp:TextBox ID="nitxtTotalValorBruto" runat="server" CssClass="inputv" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 80px">
                                                                                                        <asp:Label ID="nilblValorTotal1" runat="server" CssClass="auto-style3" Text="- Descuento"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">
                                                                                                        <asp:TextBox ID="nitxtTotalDescuento" runat="server" CssClass="inputv" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 80px">
                                                                                                        <asp:Label ID="nilblValorTotal0" runat="server" CssClass="auto-style3" Text="+ Impuesto"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">
                                                                                                        <asp:TextBox ID="nitxtTotalImpuesto" runat="server" CssClass="inputv" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 80px">
                                                                                                        <asp:Label ID="nilblValorTotal2" runat="server" CssClass="auto-style2" Text="Total"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">
                                                                                                        <asp:TextBox ID="nitxtTotal" runat="server" CssClass="inputv" Enabled="False" Width="140px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
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
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlOC" EventName="SelectedIndexChanged" />
                                                </Triggers>
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
                            <asp:UpdatePanel ID="upConsulta" runat="server" Visible="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                        <div style="text-align: center">
                                            <div style="display: inline-block">
                                                <table style="width: 1000px;">
                                                    <tr>
                                                        <td style="width: 100px; height: 25px; text-align: left"></td>
                                                        <td style="width: 150px; height: 25px; text-align: left">
                                                            <asp:DropDownList ID="niddlCampo" data-placeholder="Seleccione una opción..." runat="server" ToolTip="Selección de campo para busqueda"
                                                                Width="250px" CssClass="chosen-select-deselect input-group">
                                                            </asp:DropDownList></td>
                                                        <td style="width: 100px; height: 25px; text-align: left">
                                                            <asp:DropDownList ID="niddlOperador" data-placeholder="Seleccione una opción..." runat="server" ToolTip="Selección de operador para busqueda"
                                                                Width="150px" AutoPostBack="True" CssClass="chosen-select-deselect input-group">
                                                                <asp:ListItem Value="like">Contiene</asp:ListItem>
                                                                <asp:ListItem Value="&lt;&gt;">Diferente</asp:ListItem>
                                                                <asp:ListItem Value="between">Entre</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="=">Igual</asp:ListItem>
                                                                <asp:ListItem Value="&gt;=">Mayor o Igual</asp:ListItem>
                                                                <asp:ListItem Value="&gt;">Mayor que</asp:ListItem>
                                                                <asp:ListItem Value="&lt;=">Menor o Igual</asp:ListItem>
                                                                <asp:ListItem Value="&lt;">Menor</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td style="width: 110px; text-align: left">
                                                            <asp:TextBox ID="nitxtValor1" runat="server" Width="200px" CssClass="input"></asp:TextBox><asp:TextBox
                                                                ID="nitxtValor2" runat="server" Visible="False" Width="200px" CssClass="input"></asp:TextBox></td>
                                                        <td style="width: 20px; height: 25px; text-align: center">
                                                            <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter" OnClick="niimbAdicionar_Click"></asp:LinkButton>

                                                        </td>
                                                        <td style="width: 20px; text-align: center">
                                                            <asp:LinkButton runat="server" ID="imbBusqueda" Visible="false" CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="imbBusqueda_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 100px; height: 25px; text-align: left">
                                                            <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label></td>
                                                        <td style="background-position-x: right; width: 100px; height: 25px"></td>
                                                    </tr>
                                                </table>
                                                <table style="width: 1000px; padding: 0; border-collapse: collapse;">
                                                    <tr>
                                                        <td style="height: 10px; text-align: center;">
                                                            <asp:Label ID="nilblMensajeEdicion" runat="server" ForeColor="Navy"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="text-align: center">
                                                                <div style="display: inline-block">
                                                                    <asp:GridView ID="gvParametros" runat="server" Width="400px" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" CssClass="table table-bordered table-sm  table-hover table-striped grillas">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                                <HeaderStyle CssClass="action-item" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="campo" HeaderText="Campo">
                                                                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="operador" HeaderText="Operador">
                                                                                <HeaderStyle HorizontalAlign="Center" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="valor" HeaderText="Valor">
                                                                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="valor2" HeaderText="Valor 2">
                                                                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="thead" />
                                                                        <PagerStyle CssClass="footer" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="width: 1000px; padding: 0; border-collapse: collapse;">
                                                    <tr>
                                                        <td style="width: 1000px; text-align: left;">
                                                            <asp:GridView ID="gvTransaccion" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" OnRowCommand="gvTransaccion_OnRowCommand" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Visible="False" >
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Anu">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                        <HeaderStyle CssClass="action-item" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                                                                    <asp:BoundField DataField="numero" HeaderText="Numero"></asp:BoundField>
                                                                    <asp:BoundField DataField="año" HeaderText="Año"></asp:BoundField>
                                                                    <asp:BoundField DataField="mes" HeaderText="Mes"></asp:BoundField>
                                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}"></asp:BoundField>
                                                                    <asp:BoundField DataField="notas" HeaderText="Observaciones"></asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Detalle">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="detalle" CssClass="btn btn-default btn-sm btn-primary  fa fa-info-circle" CommandName="Update" ToolTip="Detalle de la transaccion"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CheckBoxField DataField="anulado" HeaderText="Anul"></asp:CheckBoxField>
                                                                    <asp:CheckBoxField DataField="aprobado" HeaderText="Aprd"></asp:CheckBoxField>
                                                                    <asp:CheckBoxField DataField="cerrado" HeaderText="Cerd"></asp:CheckBoxField>
                                                                     <asp:TemplateField HeaderText="Imprimir">
                                                                        <ItemTemplate>
                                                                                <asp:LinkButton runat="server" ID="ibImprimir" CssClass="btn btn-default btn-sm btn-primary fa fa-print " CommandName="imprimir" ToolTip="Imprimir transacción seleccionada"></asp:LinkButton>
                                                                        </ItemTemplate>
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
            </div>
        </div>
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
