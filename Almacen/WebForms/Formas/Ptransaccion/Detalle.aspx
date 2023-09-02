<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="Almacen.WebForms.Formas.Ptransaccion.Detalle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js"></script>
    <link href="http://app.infos.com/RecursosInfos/css/general.css" rel="stylesheet" />
    <link href="http://app.infos.com/RecursosInfos/lib/chosen187/chosen.css" rel="stylesheet" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div>

            <table id="OPERACIONES" style="width: 900px" cellpadding="0" >
                <tr>
                    <td style="width: 50px; height: 19px;"></td>
                    <td style="width: 500px; height: 19px;">Documento Nro.&nbsp;
                            <asp:Label ID="lblTipo" runat="server" ForeColor="Navy"></asp:Label>-<asp:Label ID="lblNumero" runat="server" ForeColor="Navy"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCerrado" runat="server" Enabled="false" Text="Cerrado" />
                    </td>
                    <td style="width: 100px;">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAnulado" runat="server" Enabled="false" Text="Anulado" />
                    </td>
                    <td style="width: 100px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAprobado" runat="server" Enabled="false" Text="Aprobado" />
                    </td>
                </tr>
            </table>
            <table id="ENCABEZADO" style="width: 900px" cellpadding="0" >
                <tr>
                    <td style="width: 50px"></td>
                    <td style="width: 100px; text-align: left">Fecha</td>
                    <td style="width: 230px; text-align: left">
                        <asp:Label ID="lblFecha" runat="server" ForeColor="Navy"></asp:Label></td>
                    <td style="width: 100px; text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="width: 50px"></td>
                </tr>
                <tr>
                    <td style="width: 50px"></td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblProveedor" runat="server" ForeColor="Navy">Tercero</asp:Label></td>
                    <td style="width: 230px; text-align: left">
                        <asp:Label ID="lblNitProveedor" runat="server" ForeColor="Navy"></asp:Label>
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:Label ID="lblNombreProveedor" runat="server" ForeColor="Navy"></asp:Label>
                    </td>
                    <td style="width: 50px"></td>
                </tr>
                <tr>
                    <td style="width: 50px">&nbsp;</td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblSucursal" runat="server" ForeColor="Navy">Sucursal</asp:Label></td>
                    <td style="width: 230px; text-align: left">
                        <asp:Label ID="lblCodSucursal" runat="server" ForeColor="Navy"></asp:Label>
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:Label ID="lblNombreSucursal" runat="server" ForeColor="Navy"></asp:Label>
                    </td>
                    <td style="width: 50px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 8px;"></td>
                    <td style="width: 100px; text-align: left;">Nota</td>
                    <td style="text-align: left;" colspan="3">
                        <asp:Label ID="lblObservaciones" runat="server" ForeColor="Navy"></asp:Label></td>
                    <td style="width: 50px; height: 8px;"></td>
                </tr>
            </table>
            <table id="DETALLE" style="width: 600px">
                <tr>
                    <td style="width: 900px">
                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" GridLines="None" Width="1000px" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Style="font-size: small">
                            <Columns>
                                <asp:BoundField DataField="item" HeaderText="Item">
                                <ItemStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombreItem" HeaderText="Descripci&#243;n">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="uMedida" HeaderText="UMed">
                                <ItemStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cantidad">
                                <ItemStyle Width="10px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorUnitario" DataFormatString="{0:N2}" HeaderText="VlUnitario">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorTotal" DataFormatString="{0:N2}" HeaderText="VlTotal">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="registro">
                                <ItemStyle Width="5px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 900px; height: 10px">
                        <table style="width: 1000px">
                            <tr>
                                <td style="text-align: left; vertical-align: top">
                                    <asp:GridView ID="gvImpuesto" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Font-Size="Small" GridLines="None" Width="700px">
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
                                <td style="text-align: left; width: 270px; vertical-align: top;">
                                    <table style="width: 270px">
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="nilblValorTotal" runat="server" CssClass="auto-style2" Text="Total bruto"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotalValorBruto" runat="server" CssClass="inputv" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="nilblValorTotal1" runat="server" CssClass="auto-style3" Text="- Retención"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotalDescuento" runat="server" CssClass="inputv" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="nilblValorTotal0" runat="server" CssClass="auto-style3" Text="+ Impuesto"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotalImpuesto" runat="server" CssClass="inputv" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="nilblValorTotal2" runat="server" CssClass="auto-style2" Text="Total"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotal" runat="server" CssClass="inputv" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 900px; text-align: left">&nbsp;</td>
                </tr>
            </table>

            <div id="overlay" class="overlay" style="display: none">
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
