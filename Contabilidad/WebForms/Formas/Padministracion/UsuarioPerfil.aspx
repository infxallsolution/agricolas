<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioPerfil.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.UsuarioPerfil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        $(document).ready(function () {
            $.localise('ui-multiselect', { language: 'es', path: 'http://app.infos.com/RecursosInfos/js/locale/' });
            $(".multiselect").multiselect();
            // $('#switcher').themeswitcher();
        });
    </script>
    <script type="text/javascript">

        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }

            if (o.tagName == "INPUT" && o.type == "button") {
                __doPostBack("", "");
            }
        }
    </script>


</head>
<body style="text-align: center">
    <div class="loading">
        <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
    </div>
    <div class="container">
        <form id="form1" runat="server">
            <div style="vertical-align: top; width: 100%;">
                <div style="width: 100%; text-align: left;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 10px"></td>
                            <td style="width: 90px">
                                <asp:Label ID="Label1" runat="server" Text="Empresa"></asp:Label>
                            </td>
                            <td style="width: 300px">
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Width="320px" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select">
                                </asp:DropDownList>

                            </td>

                            <td style="width: 3200px">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />

                            </td>

                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; height: 10px;"></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                    <fieldset style=" float:left">
                        <legend>Perfiles</legend>
                        <div data-spy="scroll" style="width: 100%; height: 300px; overflow-y: scroll;">
                            <div>
                                <asp:GridView ID="gvPerfiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" PageSize="20">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sel">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPerfiles" runat="server" OnCheckedChanged="chkPerfiles_CheckedChanged" AutoPostBack="True" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo" HeaderText="Código">
                                            <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead" />
                                    <PagerStyle CssClass="footer" />
                                </asp:GridView>
                            </div>
                        </div>
                    </fieldset>
                    <hr />
                    <fieldset >
                        <legend>Usuarios</legend>
                        <div data-spy="scroll" style="width: 100%; height: 300px; overflow-y: scroll;">
                            <div>
                                <asp:CheckBox ID="chkMarcarUsuario" runat="server" Text="Marcar Todos" Width="160px" AutoPostBack="True" OnCheckedChanged="chkMarcarUsuario_CheckedChanged" />
                                <asp:CheckBox ID="chkDesmarcarUsuario" runat="server" Text="Desmarcar Todos" Width="160px" AutoPostBack="True" OnCheckedChanged="chkDesmarcarUsuario_CheckedChanged" />
                                <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" PageSize="20">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sel">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkUsuarios" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="usuario" HeaderText="Código">
                                            <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead" />
                                    <PagerStyle CssClass="footer" />
                                </asp:GridView>
                            </div>
                        </div>
                    </fieldset>
                </div>
        </form>
    </div>

    <script src="http://app.infos.com/RecursosInfos/lib/jquery-ui/jquery-ui.min.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/moment/moment.js"></script>
    <script src="http://app.infos.com/RecursosInfos/js/daterangepicker.js" type="text/javascript"></script>
    <script src="http://app.infos.com/RecursosInfos/js/chosen.jquery.js" type="text/javascript"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/iCheck/icheck.min.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
    <script src="http://app.infos.com/RecursosInfos/js/core.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/ui-multiselect/js/plugins/localisation/jquery.localisation-min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/ui-multiselect/js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/ui-multiselect/js/ui.multiselect.js"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tdCampos input[type="checkbox"]').each(function () {
                var self = $(this),
                  label = self.next(),
                  label_text = label.text();

                label.remove();
                self.iCheck({
                    checkboxClass: 'icheckbox_line-blue',
                    radioClass: 'iradio_line-blue',
                    insert: '<div class="icheck_line-icon"></div>' + label_text
                });
                self.on('ifClicked', function () {

                    this.click();
                });
            });

            $('#txtValorF').daterangepicker({
                singleDatePicker: true,
                "locale": {
                    "format": "DD/MM/YYYY"
                }
            });
            $('#txvVigencia').keyup(function () {
                this.value = (this.value + '').replace(/[^0-9]/g, '');
            });
        });


    </script>
</body>
</html>
