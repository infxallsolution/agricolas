<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroNovedadesExcel.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.RegistroNovedadesExcel" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro Novedades</title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table class="w-100">
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 80%">
                        <div style="vertical-align: top; width: 100%; text-align: center" class="principal">
                            <h6>Importación de registros de novedades</h6>
                            <hr />
                            <div id="upGeneral" runat="server">
                                <div id="upRegistro" runat="server" updatemode="Conditional">
                                    <table id="encabezado" style="width: 100%; padding: 0; border-collapse: collapse;">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" Text="Nuevo" ToolTip="Habilita el formulario para un nuevo registro" OnClick="nilbNuevo_Click" />
                                                <asp:Button ID="lbCancelar" runat="server" CssClass="botones" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" OnClick="lbCancelar_Click" />
                                                <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
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
                                                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" Visible="False"></asp:Label></td>
                                                        <td style="width: 450px; height: 25px; text-align: left">
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                                                Visible="False" Width="95%">
                                                            </asp:DropDownList></td>
                                                        <td style="width: 65px; height: 25px; text-align: left">
                                                            <asp:Label ID="lblNumero" runat="server" Text="Numero" Visible="False"></asp:Label></td>
                                                        <td style="width: 150px; height: 25px; text-align: left">
                                                            <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" OnTextChanged="txtNumero_TextChanged"
                                                                Visible="False" Width="150px" CssClass="input"></asp:TextBox></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="upCabeza" runat="server" visible="False">
                                        <div style="padding: 10px; border: 1px solid silver; border-radius: 5px; margin: 5px;">
                                            <table style="width: 100%;" id="datosCab">
                                                <tr>
                                                    <td style="vertical-align: top; width: 125px; text-align: left">
                                                        <asp:Label ID="lblFecha" runat="server" Visible="False">Fecha transacción</asp:Label></td>
                                                    <td style="text-align: left; width: 200px;">
                                                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" ReadOnly="True"
                                                            Visible="False" CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox></td>
                                                    <td style="vertical-align: top; width: 100px; text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">
                                                        <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
                                                        <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdCantidad" runat="server" Value="0" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: left" colspan="4">
                                                        <asp:TextBox ID="txtObservacion" placeholder="Observaciones, notas o comentarios..." runat="server" CssClass="input" Height="40px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 125px; text-align: left">
                                                        <asp:Label ID="lblfu" runat="server" Text="Archivo a leer" Visible="False"></asp:Label>
                                                    </td>
                                                    <td colspan="3" style="text-align: left">
                                                        <asp:FileUpload ID="fuExcel" runat="server" Style="text-align: left" Visible="False" />
                                                        <asp:Button ID="btnLiquidar" runat="server" CssClass="botones" OnClick="btnLiquidar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea liquidar el documento ?');" Text="Liquidar" ToolTip="Liquidada documento..." Visible="False" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="upDetalle" runat="server" visible="False">
                                            <asp:GridView ID="gvErrores" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="None" RowHeaderColumn="cuenta" CssClass="table table-bordered table-sm  table-hover table-striped grillas" AllowSorting="True">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:BoundField DataField="linea" HeaderText="Linea">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="error" HeaderText="Error">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="thead" />
                                                <PagerStyle CssClass="footer" />
                                            </asp:GridView>

                                            <table class="w-100">
                                                <tr>
                                                    <td style="width: 150px; text-align: left;">
                                                        <asp:Label ID="lblCantidadTotal" runat="server" Text="Cantidad total"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtCantidadTotal" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1" style="text-align: left; width: 150px">
                                                        <asp:Label ID="lblValorTotal" runat="server" Text="Valor total"></asp:Label>
                                                    </td>
                                                    <td class="auto-style1" style="text-align: left">
                                                        <asp:TextBox ID="txtValorTotal" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                                    </td>
                                                    <td class="auto-style1"></td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="gvLista" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" RowHeaderColumn="cuenta" Width="100%">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:BoundField DataField="concepto" HeaderText="Concepto">
                                                        <HeaderStyle />
                                                        <ItemStyle Width="10px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombreConcepto" HeaderText="Nombre Concepto">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado" HeaderText="Empleado">
                                                        <HeaderStyle />
                                                        <ItemStyle Width="10px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombreEmpleado" HeaderText="Nombre del Empleado">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ccosto" HeaderText="C. Costo">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cantidad">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor" DataFormatString="{0:N2}" HeaderText="Valor">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="añoInicial" HeaderText="AñoI">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="añoFinal" HeaderText="AñoF">
                                                        <ItemStyle Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodoInicial" HeaderText="pInicial">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodoFinal" HeaderText="pFinal">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="frecuencia" HeaderText="FR">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="registro" HeaderText="Reg">
                                                        <HeaderStyle BackColor="White" />
                                                        <ItemStyle BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" Width="10px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="thead" />
                                                <PagerStyle CssClass="footer" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td style="width: 5%"></td>
                </tr>
            </table>
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
        </form>
    </div>
</body>
</html>
