<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroInOutContratistas.aspx.cs" Inherits="Nomina.WebForms.Formas.Pprogramacion.RegistroInOutContratistas" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/jquery-ui/ui/widget.js" type="text/javascript"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/jquery-ui-muliselect/js/plugins/localisation/jquery.localisation-min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/jquery-ui-muliselect/js/plugins/scrollTo/jquery.scrollTo-min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/jquery-ui-muliselect/js/ui.multiselect.js" type="text/javascript"></script>
    <link href="http://app.infos.com/RecursosInfos/lib/jquery-ui-muliselect/css/ui.multiselect.css" rel="stylesheet" />

</head>
<body>
    <div class="loading">
        <i class="fas fa-spinner fa-spin fa-5x"></i>
    </div>
    <div class="container">
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox>

                    </td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td></td>
                                <td style="width: 120px; text-align: left">
                                    <asp:Label ID="nilbFI" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td style="width: 150px; text-align: left">
                                    <asp:TextBox ID="nitxtFI" runat="server" CssClass="input fechaNormal" Width="120px"></asp:TextBox>
                                </td>
                                <td style="width: 120px; text-align: left">
                                    <asp:Label ID="nilbFF" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td style="width: 150px; text-align: left">
                                    <asp:TextBox ID="nitxtFF" runat="server" CssClass="input fechaNormal" Width="120px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: center" colspan="2">
                        <asp:Button ID="nibtnBuscar" runat="server" CssClass="botones" OnClick="nibtnBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="botones" OnClick="nibtnCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nibtnNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="nibtnGuardar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" Style="height: 26px" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <table style="width: 100%;" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="Label5" runat="server" Text="Fecha" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:TextBox ID="txtFecha" runat="server" Enabled="False" Font-Bold="True" ForeColor="Gray" ReadOnly="True" Visible="False" CssClass="input fechaNormal" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="Label6" runat="server" Text="Fecha entrada" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:TextBox ID="txtFechaEntrada" runat="server" Visible="False" Width="150px" CssClass="input fecha"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left;"></td>
                    <td style="text-align: left;">
                        <asp:Label ID="Label7" runat="server" Text="Fecha salida" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:TextBox ID="txtFechaSalida" runat="server" CssClass="input fecha" Visible="False" Width="150px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="Label4" runat="server" Text="Opciones" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:RadioButtonList ID="rblOpcion" runat="server" AutoPostBack="True" Height="28px" OnSelectedIndexChanged="rblEmpresa_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="False">
                            <asp:ListItem Selected="True" Value="IN">Solo Entrada</asp:ListItem>
                            <asp:ListItem Value="OUT">Entrada y salidaEntrada y salida</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblCedula" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:Label ID="lblNombre" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left;" colspan="2">
                        <select runat="server" id="selFuncionarios" class="multiselect" multiple="true" name="countries[]" visible="False" style="width: 600px; height: 200px">
                        </select></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center"></td>
                </tr>
            </table>

            <hr />
            <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
            <hr />
            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas check" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True" SortExpression="fecha" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo" HeaderText="Cedula" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                            <HeaderStyle />
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaEntrada" HeaderText="FechaEntrada" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle />
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaSalida" HeaderText="FechaSalida" DataFormatString="{0:dd/MM/yyyy HH:mm}" ConvertEmptyStringToNull="False" HtmlEncode="False" HtmlEncodeFormatString="False">
                            <HeaderStyle />
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
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

    <script type="text/javascript">
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
    <script type="text/javascript">
        $(function () {
            $.localise('ui-multiselect', { language: 'es', path: 'http://app.infos.com/RecursosInfos/js/locale/' });
            $(".multiselect").multiselect({ sortable: false });
        });
    </script>
    <script type="text/javascript">


        $(document).ready(function () {

            $('.fechaNormal').daterangepicker({ singleDatePicker: true, "locale": { "format": "DD/MM/YYYY" } }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $(".fecha").daterangepicker({
                "timePicker": true,
                singleDatePicker: true,
                "timePicker24Hour": true,
                "locale": { "format": "DD/MM/YYYY HH:mm:ss" }
            });


        });


    </script>

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
            });
        });

        $(document).ready(function () {
            $('.check input[type="checkbox"]').each(function () {
                var self = $(this),
                    label = self.next(),
                    label_text = label.text();

                label.remove();
                self.iCheck({
                    checkboxClass: 'icheckbox_line-blue',
                    radioClass: 'iradio_line-blue',
                    insert: '<div class="icheck_line-icon"></div>' + label_text
                });
            });
        });
    </script>

</body>
</html>