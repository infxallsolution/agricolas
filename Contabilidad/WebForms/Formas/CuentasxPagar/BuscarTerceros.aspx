<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuscarTerceros.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxPagar.BuscarTerceros" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Busqueda de Terceros</title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js"></script>
    <link href="http://app.infos.com/RecursosInfos/css/general.css" rel="stylesheet" />
    <link href="http://app.infos.com/RecursosInfos/lib/chosen187/chosen.css" rel="stylesheet" />

    <script type="text/javascript">

        function ActualizaCampo(indice) {
            controlGrilla = document.getElementById('gvLista');
            hfTipo = document.getElementById('hfTipo');

            if (controlGrilla != null) {
                var fila = "";
                var nombre = "";
                var tercero = "";
                var ccosto = "";
                var base = "";
                var id = "";

                fila = controlGrilla.rows[indice].cells[1].innerHTML;
                nombre = controlGrilla.rows[indice].cells[2].innerHTML;
                id = controlGrilla.rows[indice].cells[3].innerHTML;

                console.log(controlGrilla.rows[indice].cells[3].innerHTML);

                if (hfTipo.value == "1")
                    window.opener.ActualizarTerceroEncabezado(fila, nombre, id);
                else
                    window.opener.ActualizarTerceroBeneficiario(fila, nombre,id);
            }

            window.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <div style="display: inline-block">
                <table >
                    <tr>
                        <asp:HiddenField ID="hfTipo" runat="server" />
                        <td style="width: 100px; text-align: left">Busqueda</td>
                        <td style="width: 350px; text-align: left">
                            <asp:TextBox ID="nitxtBusqueda" runat="server" Width="350px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                        <td style="width: 50px; text-align: left">
                            <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Buscar cuentas" />
                        </td>
                    </tr>
                </table>
                <table >
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <div style="text-align: center">
                                <div style="display: inline-block">
                                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="10" OnPageIndexChanging="gvLista_PageIndexChanging" Width="500px">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sel">
                                                <ItemTemplate>
                                                    <a class="btn btn-default btn-sm btn-primary fa fa-check text-white" onclick='javascript:ActualizaCampo(<%# Container.DataItemIndex +1 %>)'></a>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="White" />
                                                <ItemStyle Width="20px" HorizontalAlign="Center"  />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigo" HeaderText="Código">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Nombre">
                                            </asp:BoundField>
                                             <asp:BoundField DataField="id" HeaderText="id">
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
            </div>
        </div>
    </form>
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
</body>
</html>
