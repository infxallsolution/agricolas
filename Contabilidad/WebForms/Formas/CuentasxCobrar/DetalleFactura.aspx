<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleFactura.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxCobrar.DetalleFactura" %>

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

            <table id="OPERACIONES" style="width: 900px">
                <tr>
                    <td></td>
                    <td style="width: 500px; height: 19px;"><strong>Documento Nro.</strong>
                        <asp:Label ID="lblTipo" runat="server"></asp:Label>-<asp:Label ID="lblNumero" runat="server"></asp:Label>
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
            <hr />
            <table id="ENCABEZADO" style="width: 900px">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 100px;"><strong>Fecha</strong></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblFecha" runat="server"></asp:Label></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 50px"></td>
                    <td style="width: 100px; text-align: left">
                        <strong>
                            <asp:Label ID="lblProveedor" runat="server">Proveedor</asp:Label></strong></td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblIdTercero" runat="server"></asp:Label>
                        <asp:Label ID="lblNitProveedor" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNombreProveedor" runat="server"></asp:Label>
                    </td>
                    <td style="width: 50px"></td>
                </tr>
                <tr>
                    <td style="width: 50px"></td>
                    <td style="width: 100px; text-align: left">
                        <strong>
                            <asp:Label ID="lblSucursal" runat="server">Sucursal</asp:Label></strong></td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblCodSucursal" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNombreSucursal" runat="server"></asp:Label>
                    </td>
                    <td style="width: 50px"></td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 8px;"></td>
                    <td style="width: 100px; text-align: left;"><strong>Notas</strong></td>
                    <td style="text-align: left;" colspan="2">
                        <asp:Label ID="lblObservaciones" runat="server"></asp:Label></td>
                    <td style="width: 50px; height: 8px;"></td>
                </tr>
            </table>
            <table id="DETALLE" style="width: 600px">
                <tr>
                    <td style="width: 900px">
                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" GridLines="None" Width="1000px" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Style="font-size: small">
                            <Columns>
                                <asp:BoundField DataField="item" HeaderText="Item">
                                    <ItemStyle Width="5px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombreItem" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="uMedida" HeaderText="UMed">
                                    <ItemStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" Width="5px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorUnitario" DataFormatString="{0:N2}" HeaderText="VlUnitario">
                                    <ItemStyle HorizontalAlign="Right" Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorTotal" DataFormatString="{0:N2}" HeaderText="VlTotal">
                                    <ItemStyle HorizontalAlign="Right" Width="5px" />
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
                                                <asp:Label ID="nilblValorTotal" runat="server" Css Text="Total bruto"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotalValorBruto" runat="server" CssClass="input" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="nilblValorTotal1" runat="server" Css Text="- Retención"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotalDescuento" runat="server" CssClass="input" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="nilblValorTotal0" runat="server" Css Text="+ Impuesto"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotalImpuesto" runat="server" CssClass="input" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="nilblValorTotal2" runat="server" Css Text="Total"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <asp:TextBox ID="nitxtTotal" runat="server" CssClass="input" Enabled="False" Width="160px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 900px; text-align: left">
                        <table cellpadding="0"  class="w-100">
                            <tr>
                                <td style="width: 50px"></td>
                                <td style="width: 110px; text-align: left">
                                    <strong>
                                        <asp:Label ID="lblProveedor0" runat="server">Fecha registro</asp:Label></strong></td>
                                <td>
                                    <asp:Label ID="lblFechaRegistro" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px"></td>
                                <td style="width: 110px; text-align: left">
                                    <strong>
                                        <asp:Label ID="lblProveedor1" runat="server">Usuario registro</asp:Label></strong></td>
                                <td>
                                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px"></td>
                                <td style="text-align: left" colspan="2">
                                    <asp:Label ID="lblRegistroAnulado" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
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
