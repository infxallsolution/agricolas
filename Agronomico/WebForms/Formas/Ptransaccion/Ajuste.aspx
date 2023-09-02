<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajuste.aspx.cs" Inherits="Agronomico.WebForms.Formas.Ptransaccion.Ajuste" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>

    <style type="text/css">
        .auto-style1 {
            height: 10px;
        }
    </style>

</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <table style="width: 100%">
            <tr>
                <td></td>
                <td style="width: 1100px">
                    <div>
                        <div style="vertical-align: top; width: 100%; text-align: left" class="principal">
                            <div id="upGeneral" runat="server">
                                <div id="upRegistro" runat="server">
                                    <div style="text-align: center">
                                        <div style="padding: 5px 10px 5px 10px;">
                                            <table id="encabezado" style="width: 100%; padding: 0; border-collapse: collapse;">
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" Text="Nuevo" ToolTip="Habilita el formulario para un nuevo registro" OnClick="nilbNuevo_Click" />
                                                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" OnClick="lbCancelar_Click" />
                                                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; padding: 0; border-collapse: collapse;">
                                                        <table style="width: 100%; padding: 0; border-collapse: collapse;">
                                                            <tr>
                                                                <td></td>
                                                                <td style="width: 125px; height: 25px; text-align: left">
                                                                    <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" Visible="False"></asp:Label>
                                                                </td>
                                                                <td style="width: 400px; height: 25px; text-align: left">
                                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged" Visible="False" Width="370px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 65px; height: 25px; text-align: left">
                                                                    <asp:Label ID="lblNumero" runat="server" Text="Numero" Visible="False"></asp:Label>
                                                                </td>
                                                                <td style="width: 150px; height: 25px; text-align: left">
                                                                    <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txtNumero_TextChanged" Visible="False" Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="upCabeza" runat="server">
                                                <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                                    <div>
                                                        <table class="w-100" id="tbCabeza" runat="server" style="width: 100%">
                                                            <tr>
                                                                <td style="vertical-align: top; width: 125px; text-align: left">
                                                                    <asp:Label ID="lblFecha" runat="server">Fecha transacción</asp:Label></td>
                                                                <td style="vertical-align: top; width: 175px; text-align: left">
                                                                    <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" CssClass="input fecha" Width="150px" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; width: 100px; text-align: left">
                                                                    <asp:Label ID="lblFinca" runat="server" Text="Fincal"></asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top; width: 400px; text-align: left">
                                                                    <asp:DropDownList ID="ddlFinca" runat="server" CssClass="chzn-select-deselect" Width="350px" Style="left: 2px; top: 0px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; width: 125px; text-align: left">
                                                                    <asp:Label ID="lblFecha0" runat="server">Fecha inicial</asp:Label></td>
                                                                <td style="vertical-align: top; width: 175px; text-align: left">
                                                                    <asp:TextBox ID="txtFechaInicial" runat="server" Font-Bold="True" CssClass="input fecha" Width="150px" AutoPostBack="True" OnTextChanged="txtFechaInicial_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; width: 100px; text-align: left">
                                                                    <asp:Label ID="lblFecha1" runat="server">Fecha final</asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top; width: 400px; text-align: left">
                                                                    <asp:TextBox ID="txtFechaFinal" runat="server" Font-Bold="True" CssClass="input fecha" Width="150px" AutoPostBack="True" OnTextChanged="txtFechaFinal_TextChanged"></asp:TextBox>
                                                                    <asp:LinkButton runat="server" ID="btnBuscar" CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="btnBuscar_Click" ToolTip="Clic aquí para realizar la busqueda"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 125px; height: 10px; text-align: left">
                                                                    <asp:Label ID="lblTercero" runat="server" Text="Novedad"></asp:Label>
                                                                </td>
                                                                <td style="width: 175px; height: 10px; text-align: left">
                                                                    <asp:DropDownList ID="ddlNovedad" runat="server" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged" Width="350px" CssClass="chzn-select-deselect" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 100px; height: 10px; text-align: left;">
                                                                    <asp:Label ID="lblSucursal" runat="server" Text="Umedida"></asp:Label>
                                                                </td>
                                                                <td style="width: 400px; height: 10px; text-align: left;">
                                                                    <asp:DropDownList ID="ddlUmedida" runat="server" CssClass="chzn-select-deselect" Width="350px" Style="left: 2px; top: 0px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left" class="auto-style1" colspan="4">
                                                                    <asp:TextBox ID="txtObservacion" runat="server" placeholder="Observaciones y/o notas..." CssClass="input" Height="40px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="upReferencia" runat="server" visible="False">
                                    <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                        <asp:GridView ID="gvReferencia" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Font-Size="Small" GridLines="None" Width="100%">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSeleccion" runat="server" Checked="True" />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="White" BorderColor="White" BorderStyle="None" />
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" Width="5px" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chbTotal" runat="server" AutoPostBack="True" OnCheckedChanged="chbTotal_CheckedChanged" Checked="True" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="5px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="idTercero" HeaderText="Tercero">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="5px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="codTercero" HeaderText="CodTercero">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombreTercero" HeaderText="NombreTercero">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valorTotal" DataFormatString="{0:N2}" HeaderText="Valor Total">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="diferencia" DataFormatString="{0:N2}" HeaderText="Ajuste">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
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
                <td></td>
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
</body>
</html>
