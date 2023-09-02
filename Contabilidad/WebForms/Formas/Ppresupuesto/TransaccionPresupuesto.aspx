<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransaccionPresupuesto.aspx.cs" Inherits="Contabilidad.WebForms.App_Code.General.TransaccionPresupuesto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="http://app.infos.com/recursosInfos/css/root.css" rel="stylesheet" />
    <script type="text/javascript" src="http://app.infos.com/recursosInfos/lib/jquery/dist/jquery.min.js"></script>
    <link href="http://app.infos.com/recursosinfos/lib/jquerytimepicker1.11.14/jquery.timepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/jquerytimepicker1.11.14/jquery.timepicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="loading">
            <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-4">
                    <asp:Button ID="niimbRegistro" runat="server" Visible="true" CssClass="botones" OnClick="niimbRegistro_Click" Text="Registro" ToolTip="Haga clic aqui para realizar el registro" />
                    <asp:Button ID="niimbConsulta" runat="server" Visible="true" CssClass="botones" OnClick="niimbConsulta_Click" Text="Consulta" ToolTip="Haga clic aqui para realizar la consulta" />
                </div>
            </div>
            <hr />
            <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
            <asp:HiddenField ID="tsHora" runat="server" />
            <fieldset id="fsGeneral" runat="server" style="font-size: 11px;">
                <fieldset id="fsRegistro" runat="server">
                    <div class="row">
                        <div class="col-2">
                        </div>
                        <div class="col-8">
                            <asp:Button ID="nilbNuevo" runat="server" Visible="true" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo" ToolTip="Haga clic aqui para realizar nuevo registro" />
                            <asp:Button ID="lbCancelar" runat="server" Visible="false" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Haga clic aqui para cancelar" />
                            <asp:Button ID="lblCalcular" runat="server" Visible="false" CssClass="botones" OnClick="lbICalcular_Click" Text="Calcular" ToolTip="Haga clic aqui para cancelar" />
                            <asp:Button ID="lbRegistrar" runat="server" Visible="false" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos"  />
                        </div>
                    </div>
                    <hr />
                    <div class="row text-justify">
                        <div class="col-1">
                        </div>
                        <div class="col-2">
                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" Visible="False"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                Visible="False" Width="100%">
                            </asp:DropDownList>
                        </div>
                        <div class="col-2">
                            <asp:Label ID="lblNumero" runat="server" Text="Numero" Visible="False"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" OnTextChanged="txtNumero_TextChanged"
                                Visible="False" Width="100%" CssClass="input"></asp:TextBox>
                        </div>
                    </div>
                    <fieldset id="fsCabeza" runat="server">
                        <div class="row text-justify">
                            <div class="col-1">
                            </div>
                            <div class="col-2">
                                <asp:Label ID="lblFormulacion" runat="server" Text="Items" Visible="False"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:DropDownList ID="ddlFormulacion" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" OnSelectedIndexChanged="ddlFormulacion_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="lbl" runat="server" Text="Año" Visible="False"></asp:Label>
                            </div>
                            <div class="col-2">
                                <asp:DropDownList ID="ddlAÑo" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" OnSelectedIndexChanged="ddlAÑo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <hr />
                        <div class="tablaGrilla" style="text-align: center">
                            <div class="row">
                                <div class="col-12">
                                    <asp:GridView ID="gvLista" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas">
                                        <Columns>
                                            <asp:BoundField DataField="movimiento">
                                                <ItemStyle Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Bottom" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:TemplateField ControlStyle-BackColor="White" HeaderStyle-BackColor="White">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txvValorMes" runat="server" CssClass="input numero" Enabled="false" onkeyup="formato_numero(this)" Text="0" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfVariable" runat="server" Value='<%# Eval("variable") %>'></asp:HiddenField>
                                                    <asp:HiddenField ID="hfPeriocidad" runat="server" Value='<%# Eval("periodicidad") %>'></asp:HiddenField>

                                                    <asp:DataList ID="dtAnalisis" runat="server" RepeatDirection="Horizontal">
                                                        <ItemTemplate>
                                                            <div style="width: 80px; max-width: 80px;">
                                                                <div>
                                                                    <asp:HiddenField ID="hfMes" runat="server" Value='<%# Eval("mes") %>'></asp:HiddenField>
                                                                    <asp:Label ID="lblMes" CssClass="h9" Width="70%" runat="server" Text='<%# Eval("nombremes") %>'></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txvValorAnalisis" Style="text-align: right;" CssClass="input numero" onkeyup="formato_numero(this)" runat="server" Width="100%" Text='<%# Eval("Valor") %>' Enabled='<%# !Convert.ToBoolean(Eval("Resultado")) %>'></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle CssClass="thead" />
                                        <PagerStyle CssClass="footer" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </fieldset>
                <fieldset id="fsConsulta" runat="server" visible="False">
                    <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                        <div style="text-align: center">
                            <div style="display: inline-block">
                                <table style="width: 1000px;">
                                    <tr>
                                        <td style="width: 100px; text-align: left"></td>
                                        <td style="width: 150px; text-align: left">
                                            <asp:DropDownList ID="niddlCampo" data-placeholder="Seleccione una opción..." runat="server" ToolTip="Selección de campo para busqueda"
                                                Width="250px" CssClass="chzn-select">
                                            </asp:DropDownList></td>
                                        <td style="width: 100px; text-align: left">
                                            <asp:DropDownList ID="niddlOperador" data-placeholder="Seleccione una opción..." runat="server" ToolTip="Selección de operador para busqueda"
                                                Width="150px" AutoPostBack="True" CssClass="chzn-select">
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
                                        <td style="width: 20px; text-align: center">
                                            <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter" OnClick="niimbAdicionar_Click"></asp:LinkButton>

                                        </td>
                                        <td style="width: 20px; text-align: center">
                                            <asp:LinkButton runat="server" ID="imbBusqueda" Visible="false" CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="imbBusqueda_Click"></asp:LinkButton>
                                        </td>
                                        <td style="width: 100px; text-align: left">
                                            <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label></td>
                                        <td style="background-position-x: right; width: 100px;"></td>
                                    </tr>
                                </table>
                                <table style="width: 1000px; padding: 0; border-collapse: collapse;">
                                    <tr>
                                        <td style="text-align: center;">
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
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="operador" HeaderText="Operador">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor" HeaderText="Valor">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor2" HeaderText="Valor 2">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
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
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvTransaccion" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowUpdating="gvTransaccion_RowUpdating" OnRowDeleting="gvTransaccion_RowDeleting">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:ButtonField CommandName="update" HeaderText="Edit" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                                        <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                                        <ItemStyle Width="20px" CssClass="action-item" />
                                                        <HeaderStyle CssClass="action-item" />
                                                    </asp:ButtonField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="imElimina" runat="server" CommandName="Delete" CssClass="btn btn-default btn-sm btn-danger fa fa-minus-circle " ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="action-item" HorizontalAlign="Center" Width="20px" />
                                                        <HeaderStyle CssClass="action-item" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                                                    <asp:BoundField DataField="numero" HeaderText="Numero"></asp:BoundField>
                                                    <asp:BoundField DataField="año" HeaderText="Año"></asp:BoundField>
                                                    <asp:BoundField DataField="producto" HeaderText="Producto"></asp:BoundField>
                                                    <asp:BoundField DataField="nombreProducto" HeaderText="NombreProducto"></asp:BoundField>
                                                    <asp:BoundField DataField="nota" HeaderText="Observaciones" HtmlEncode="False" HtmlEncodeFormatString="False"></asp:BoundField>
                                                    <asp:CheckBoxField DataField="anulado" HeaderText="Anul"></asp:CheckBoxField>
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
                </fieldset>
            </fieldset>
        </div>
        <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
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
            $(document).ready(function () {
                $('.hora').timepicker({ 'timeFormat': 'H:i:s' });
            });
        </script>
    </form>
</body>
</html>
