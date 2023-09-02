<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salidas.aspx.cs" Inherits="Almacen.WebForms.Formas.Ptransaccion.Salidas" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <table class="w-100">
            <tr>
                <td style="width: 3%"></td>
                <td style="width: 94%">
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
                                            <td colspan="3" style="text-align: center">
                                                <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo" ToolTip="click para realizar nuevo registro" />
                                                <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="click para cancelar el registro" />
                                                <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" Text="Guardar" ToolTip="Guardar transacción" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" />
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="vertical-align: top; width: 125px; text-align: left">
                                                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" Visible="True"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                                                Visible="True" Width="97%" Style="left: 0px; top: 0px">
                                                            </asp:DropDownList></td>
                                                        <td style="vertical-align: top; width: 125px; text-align: left">
                                                            <asp:Label ID="lblNumero" runat="server" Text="Número" Visible="True"></asp:Label></td>
                                                        <td style="text-align: left; width: 30%">
                                                            <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" OnTextChanged="txtNumero_TextChanged"
                                                                Visible="True" Width="95%" CssClass="input"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <asp:UpdatePanel ID="upCabeza" runat="server" UpdateMode="Conditional" Visible="False">
                                        <ContentTemplate>

                                            <table class="w-100">
                                                <tr>
                                                    <td style="width: 150px; text-align: left;">
                                                        <asp:Label ID="lblFecha" runat="server"
                                                            Visible="False" Style="color: #003366">Fecha</asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 400px;">
                                                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" ForeColor="Gray" ReadOnly="True" Visible="False" CssClass="input fecha" Width="90%" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="lblDocref" runat="server" Text="Docto referencia" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtDocref" runat="server" CssClass="input" Visible="False" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; text-align: left;">
                                                        <asp:Label ID="lblRequerimiento" runat="server" Text="Requerimiento" Visible="False"></asp:Label>
                                                    </td>
                                                    <td colspan="3" style="text-align: left">
                                                        <asp:DropDownList ID="ddlTipoRequerimiento" data-placeholder="Seleccione una opción..." runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTipoRequerimiento_SelectedIndexChanged" Visible="False" Width="30%"></asp:DropDownList>
                                                        <asp:DropDownList ID="ddlRequerimiento" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlRequerimiento_SelectedIndexChanged" Visible="False" Width="65%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; text-align: left;">
                                                        <asp:Label ID="lblSolicitante" runat="server" Text="Se entrega a:" Visible="False"></asp:Label>
                                                    </td>
                                                    <td colspan="3" style="text-align: left">
                                                        <asp:DropDownList ID="ddlSolicitante" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: left">
                                                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" placeholder="Observaciones, notas o comentarios..." TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: left">
                                                        <asp:Label ID="lblTercero" runat="server" Text="Proveedor" Visible="False"></asp:Label>
                                                        <asp:DropDownList ID="ddlTercero" data-placeholder="Seleccione una opción..." runat="server" Visible="False" CssClass="chzn-select-deselect" AutoPostBack="True" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged" Width="30%"></asp:DropDownList>
                                                        <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" Visible="False"></asp:Label>
                                                        <asp:DropDownList ID="ddlSucursal" data-placeholder="Seleccione una opción..." runat="server" CssClass="chzn-select-deselect" Visible="False" Width="15%" AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged"></asp:DropDownList>

                                                        <asp:Label ID="lblFechaReferecia" runat="server" Text="" Visible="False"></asp:Label>
                                                        <asp:TextBox ID="txtFechaReferencia" runat="server" CssClass="input" Visible="False" Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowDeleting" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <hr />
                                    <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional" Visible="False">
                                        <ContentTemplate>
                                            <div  style="width:100%">
                                                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" Width="100%">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                     <asp:BoundField DataField="Producto" HeaderText="Item">
                                                                        <ItemStyle Width="2%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nombreProducto" HeaderText="Producto">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="uMedida" HeaderText="UMed">
                                                                        <ItemStyle Width="3%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nombreBodega" HeaderText="Bodega">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nombreDestino" HeaderText="Destino">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nombreCentroCosto" HeaderText="Ccosto">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nombreTercero" HeaderText="Tercero">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cant">
                                                                        <ItemStyle Width="5%" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="valorUnitario" DataFormatString="{0:N2}" HeaderText="Vl. Uni">
                                                                        <ItemStyle Width="8%" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="valorTotal" DataFormatString="{0:N2}" HeaderText="Vl. Total">
                                                                        <ItemStyle Width="7%" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo"  DataFormatString="{0:N2}" >
                                                                        <ItemStyle Width="5%" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                   <%-- <asp:BoundField DataField="registro" HeaderText="No.">
                                                                        <ItemStyle Width="1%" />
                                                                    </asp:BoundField>--%>
                                                                    <asp:TemplateField HeaderText="Nota">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hfRegistro" runat="server" Value='<%# Eval("registro") %>' />
                                                                            <asp:HiddenField ID="hfCantidadOC" runat="server" Value='<%#Eval("saldo") %>' />
                                                                            <asp:Label ID="lblNota" runat="server" ToolTip='<%# Eval("nota") %>' Text="Nota"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="thead" />
                                                                <PagerStyle CssClass="footer" />
                                                            </asp:GridView>
                                            </div>
                                            <div class="row" style="width:100%">
                                                <div class="col-7"></div>
                                                <div class="col-5">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="row  text-right">
                                                                <div class="col-6 text-right">
                                                                    <asp:Label ID="nilblValorTotal2" runat="server" Text="Total"></asp:Label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:TextBox ID="nitxtTotal" Width="100%" CssClass="input text-right" runat="server" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
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

                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlRequerimiento" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="RowUpdating" />
                                </Triggers>
                            </asp:UpdatePanel>
                              <asp:UpdatePanel ID="upConsulta" runat="server" Visible="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                 <div style="padding: 5px; border-right: gray thin solid; border-radius: 5px; border: 1px solid silver; margin: 5px; text-align: center;">
                                            <table class="w-100">
                                                <tr>
                                                    <td></td>
                                                    <td style="width: 400px; height: 25px; text-align: left">
                                                        <asp:DropDownList ID="niddlCampo" runat="server" data-placeholder="Selección de campo para busqueda"
                                                            Width="95%" CssClass="chzn-select-deselect">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 160px; height: 25px; text-align: left">
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
                                                    <td style="width: 150px; height: 25px; text-align: left">
                                                        <asp:TextBox ID="nitxtValor1" runat="server" Width="95%" CssClass="input" AutoPostBack="True" OnTextChanged="nitxtValor1_TextChanged"></asp:TextBox><asp:TextBox
                                                            ID="nitxtValor2" runat="server" Visible="False" Width="95%" CssClass="input"></asp:TextBox></td>
                                                    <td style="width: 50px; text-align: center">
                                                        <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter " OnClick="niimbAdicionar_Click"></asp:LinkButton>

                                                    </td>
                                                    <td style="width: 50px; text-align: center">
                                                        <asp:LinkButton runat="server" ID="imbBusqueda" Visible="false" CssClass="btn btn-default btn-sm btn-success fa fa-search" OnClick="imbBusqueda_Click"></asp:LinkButton>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <table class="w-100">
                                                <tr>
                                                    <td></td>
                                                    <td class="w-25">
                                                        <div style="text-align: center">

                                                            <asp:GridView ID="gvParametros" runat="server" Width="400px" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" CssClass="table table-bordered table-sm  table-hover table-striped grillas">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-minus-circle " CommandName="Delete" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                        <HeaderStyle CssClass="action-item" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="campo" HeaderText="Campo"></asp:BoundField>
                                                                    <asp:BoundField DataField="operador" HeaderText="Operador"></asp:BoundField>
                                                                    <asp:BoundField DataField="valor" HeaderText="Valor"></asp:BoundField>
                                                                    <asp:BoundField DataField="valor2" HeaderText="Valor 2"></asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="thead" />
                                                                <PagerStyle CssClass="footer" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label>
                                            <asp:GridView ID="gvTransaccion" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Visible="False" OnRowCommand="gvTransaccion_RowCommand">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField>
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
                                                    <asp:BoundField DataField="notas" HeaderText="Observaciones">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
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
                                        </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="RowUpdating" />
                        </Triggers>
                    </asp:UpdatePanel>
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

            <script type="text/javascript">

            function cargarMetodos() {

                $(".modalClose").click(function (c, e) {

                    var tamano = $(".container").height() + 20;
                    $('#iframe', window.parent.document).height(tamano);
                    $(".modal-backdrop.fade.show").remove();
                });

                $(".loading").fadeOut(750);

                $(window).on("beforeunload", function () {
                    $(".loading").show();
                });

                $('.botonClick').click(function (e) {
                    e.preventDefault();
                });


                var input;
                $('.fecha').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: false,
                    autoUpdateInput: false,
                    "locale": {
                        "format": "DD/MM/YYYY"
                    }
                }, function (chosen_date) {
                    if (typeof (input) !== "undefined") {
                        $(input).val(chosen_date.format("DD/MM/YYYY"));
                        $(input).change();
                    }
                });
                $(".fecha").click(function () {
                    input = this;
                });
                if ($(".fecha").val() === "") {
                    var f = new Date();
                    var dia = f.getDate();
                    var mes = f.getMonth() + 1;
                    if (dia < 10) {
                        dia = "0" + dia;
                    }
                    if (mes + 1 < 10) {
                        mes = "0" + mes;
                    }

                    $(".fecha").val(dia + "/" + mes + "/" + f.getFullYear());
                }



                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                function confirmSwal($this, title, detalle) {
                    swal({
                        title: title,
                        html: detalle,
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonText: "Aceptar",
                        cancelButtonText: "Cancelar"
                    }).then(function () {
                        $this.onclick = '';
                        $this.click();
                    });
                    return false;
                }

            }

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                onEndRequest();
            });

            function onEndRequest(sender, args) {
                cargarMetodos();
            }

            $(document).ready(function () {
                cargarMetodos();



            });
            </script>
    </form>
</body>
</html>