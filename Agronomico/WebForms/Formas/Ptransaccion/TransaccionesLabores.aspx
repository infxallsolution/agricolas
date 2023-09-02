<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransaccionesLabores.aspx.cs" Inherits="Agronomico.WebForms.Formas.Ptransaccion.TransaccionesLabores" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transacciones Agro</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function MantenSesion() {
            var CONTROLADOR = "refresh_session.ashx";
            var head = document.getElementsByTagName('head').item(0);
            script = document.createElement('script');
            script.src = CONTROLADOR;
            script.setAttribute('type', 'text/javascript');
            script.defer = true;
            head.appendChild(script);
        }


    </script>


</head>
<body>
    <div class="container">
       <%-- <div class="loading">
            <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
        </div>--%>
        <form id="form1" runat="server">
            <asp:Button ID="niimbRegistro" runat="server" OnClick="niimbRegistro_Click" Text="Registro" CssClass="botones" />
            <asp:Button ID="imbConsulta" runat="server" OnClick="imbConsulta_Click" CssClass="botones" Text="Consulta" />
            <hr />
            <div id="upGeneral" runat="server">
                <div style="text-align: center;">
                    <asp:Button ID="nilbNuevo" runat="server" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click" CssClass="botones" Text="Nuevo" />
                    <asp:Button ID="lbCancelar" runat="server" ToolTip="Cancela la operación" OnClick="lbCancelar_Click" Visible="False" CssClass="botones" Text="Cancelar" />
                    <asp:Button ID="lbRegistrar" runat="server" ToolTip="Guarda el nuevo registro en la base de datos" OnClick="lbRegistrar_Click" Visible="False" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" CssClass="botones" Text="Guardar" />
                    <asp:Button ID="niimbImprimir" runat="server" ToolTip="Haga clic aqui para realizar la busqueda" Visible="False" CssClass="botones" Text="Imprimir" />
                </div>
                <div style="text-align: center; padding: 5px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver; width: 100%">
                    <div style="">
                        <table style="width: 100%">
                            <tr>
                                <td></td>
                                <td style="width: 125px; text-align: left">
                                    <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 400px; text-align: left">
                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged" Visible="False" Width="95%">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 65px; text-align: left">
                                    <asp:Label ID="lblNumero" runat="server" Text="Número" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 250px;">
                                    <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" Visible="False" Width="95%" CssClass="input"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="text-align: center; padding: 5px;">
                    <div id="upEncabezado" runat="server" visible="False">
                        <table id="datosCab" style="width: 100%;">
                            <tr>
                                <td></td>
                                <td colspan="4">
                                    <h6>Encabezado de Transacción</h6>
                                    <hr />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left; width: 150px;">
                                    <asp:Label ID="lbFecha" runat="server">Fecha </asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtFecha" runat="server" AutoPostBack="True" CssClass="input fecha" Font-Bold="True" OnTextChanged="txtFecha_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 150px;">
                                    <asp:Label ID="lblRemision" runat="server" Text="Remisión"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtRemision" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left;" colspan="4">
                                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" placeholder="Notas y/o observaciones..." Height="50px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="text-align: center; padding: 5px;">
                    <div id="upDetalle" runat="server" visible="False">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td colspan="4">
                                    <h6>Detalle de la transacción</h6>
                                    <hr />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 100px; text-align: left;">
                                    <asp:Label ID="lblNovedad" runat="server" Text="Labor"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlNovedad" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlNovedad_SelectedIndexChanged" Width="95%" CssClass="chzn-select-deselect">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 100px">
                                    <asp:Label ID="lblUmedida" runat="server" Text="Unidad medida"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlUmedida" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="95%">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 100px; text-align: left;">
                                    <asp:Label ID="lblFinca" runat="server" Text="Finca"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlFinca" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlFinca_SelectedIndexChanged2" Width="95%" CssClass="chzn-select-deselect">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 100px">
                                    <asp:Label ID="lblSeccion" runat="server" Text="Sección"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlSeccion" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlSeccion_SelectedIndexChanged" Width="95%" CssClass="chzn-select-deselect">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 100px; text-align: left;">
                                    <asp:Label ID="lblLote" runat="server" Text="Lote"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlLote" runat="server" data-placeholder="Seleccione una opción..." Width="95%" OnSelectedIndexChanged="ddlLote_SelectedIndexChanged" AutoPostBack="True" CssClass="chzn-select-deselect">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 100px" style="width: 110px; text-align: left;">
                                    <asp:Label ID="lbFechaD" runat="server">Fecha labor</asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtFechaD" runat="server" CssClass="input fecha" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" AutoPostBack="True" OnTextChanged="txtFechaD_TextChanged"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: center;" colspan="4">
                                    <div style="">
                                        <table class="w-100">
                                            <tr>
                                                <td style="width: 20%"></td>
                                                <td style="width: 800px">
                                                    <select id="selTerceroCosecha" runat="server" class="multiselect" multiple="true" name="countries[]" style="width: 100%; height: 150px;" visible="True">
                                                    </select></td>
                                                <td style="width: 20%"></td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left;" colspan="4">
                                    <table class="ui-accordion">
                                        <tr>
                                            <td style="padding-right: 4px; padding-left: 4px; width: 100px">
                                                <asp:Label ID="lblCantidadD" runat="server" Text="Cantidad"></asp:Label>
                                            </td>
                                            <td style="padding-right: 4px; padding-left: 4px; width: 150px">
                                                <asp:TextBox ID="txvCantidadD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="110px">0</asp:TextBox>
                                            </td>
                                            <td style="padding-right: 4px; padding-left: 4px; width: 100px">
                                                <asp:Label ID="lblJornalesD" runat="server" Text="Jornales"></asp:Label>
                                            </td>
                                            <td style="padding-right: 4px; padding-left: 4px">
                                                <asp:TextBox ID="txvJornalesD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="110px">0</asp:TextBox>
                                            </td>
                                            <td style="padding-right: 4px; padding-left: 4px"></td>
                                            <td style="padding-right: 4px; padding-left: 4px"></td>
                                            <td style="padding-right: 4px; padding-left: 4px"></td>
                                            <td style="padding-right: 4px; padding-left: 4px"></td>
                                            <td style="padding-right: 4px; padding-left: 4px"></td>
                                            <td style="padding-right: 4px; padding-left: 4px"></td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style=""></td>
                                <td colspan="4" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver">
                                    <asp:Button ID="imbCargar" runat="server" OnClick="imbCargar_Click1" CssClass="botones" Text="Cargar" />
                                    <asp:Button ID="imbLiquidar" runat="server" OnClick="imbLiquidar_Click" CssClass="botones" Text="Liquidar" />
                                    <asp:Button ID="lbCancelarD" runat="server" OnClick="lbCancelarD_Click" ToolTip="Cancela la operación" Visible="False" CssClass="botones" Text="Cancelar" />
                                </td>
                                <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver"></td>
                            </tr>
                            <tr>
                                <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver"></td>
                                <td colspan="4" style="border-bottom: 1px solid silver; text-align: left;">
                                    <table class="ui-accordion">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvSubTotales" runat="server" CssClass="table table-bordered table-sm  table-hover table-striped grillas" AutoGenerateColumns="False" BorderStyle="None" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="novedades" HeaderText="Codigo">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombreNovedades" HeaderText="Novedad">
                                                            <ItemStyle Width="500px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="subCantidad" HeaderText="SubCantidad">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="subRacimo" HeaderText="SubRacimos">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SubJornal" HeaderText="SubJornales">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="thead" />
                                                    <PagerStyle CssClass="footer" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="4">
                                    <asp:DataList ID="dlDetalle" runat="server" OnDeleteCommand="dlDetalle_DeleteCommand" OnItemCommand="dlDetalle_ItemCommand" RepeatColumns="2" RepeatDirection="Horizontal" Style="margin-right: 0px" Width="100%">
                                        <ItemTemplate>
                                            <div style="padding: 5px; border: solid; border-color: silver; border-width: 1px; width: 570px;">
                                                <div style="border: 1px solid silver;">
                                                    <div style="padding: 2px">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 40px; text-align: left;">
                                                                    <strong>
                                                                    <asp:Label ID="Label16" runat="server" Text="Novedad"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblNovedad" runat="server" Text='<%# Eval("codnovedad") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblDesNovedad" runat="server" Text='<%# Eval("desnovedad") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 60px;">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40px; text-align: left;">
                                                                    <strong>Finca</strong></td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblFinca" runat="server" Text='<%# Eval("codFinca") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblDesFinca" runat="server" Text='<%# Eval("desFinca") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblUmedida" runat="server" Text='<%# Eval("umedida") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40px; text-align: left;">
                                                                    <strong>
                                                                    <asp:Label ID="Label17" runat="server" Text="Sección"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblSeccion" runat="server" Text='<%# Eval("codseccion") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblDesSeccion" runat="server" Text='<%# Eval("desseccion") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 60px;">
                                                                    <asp:Label ID="lblPesoPromedio" runat="server" Text='<%# Eval("PesoRacimo") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40px; text-align: left;">
                                                                    <strong>
                                                                    <asp:Label ID="Label18" runat="server" Text="Lote"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblLote" runat="server" Text='<%# Eval("codlote") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblDesLote" runat="server" Text='<%# Eval("deslote") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 60px;">
                                                                    <asp:Label ID="lblpRacimos" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40px; text-align: left;">
                                                                    <strong>
                                                                    <asp:Label ID="Label19" runat="server" Text="Fecha"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblFechaD" runat="server" Text='<%# Eval("fechaD") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="Label20" runat="server" Text="Precio Labor $"></asp:Label>
                                                                    <asp:Label ID="lblPrecioLabor" runat="server" Text='<%# Eval("precioLabor") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 60px;">
                                                                    <asp:Label ID="lblDifKilos" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40px; text-align: left;">
                                                                    <strong>
                                                                    <asp:Label ID="lblTercero" runat="server" Text="Trabajador"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td colspan="2" style="text-align: left">
                                                                    <asp:DropDownList ID="ddlTerceroGrilla" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="95%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 60px;">
                                                                    <asp:LinkButton runat="server" ID="imbCargarTercero" CssClass="btn btn-default btn-sm btn-success fa fa-plus" ToolTip="Seleccione el tercero que desea agregar" CommandName="Select"></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="imbCargarTercero0" CssClass="btn btn-default btn-sm btn-danger fa fa-minus" ToolTip="Antes chequee el terceros en la grilla que quiere eliminar" CommandName="Update"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div style="padding-top: 2px">
                                                    <table class="ui-accordion">
                                                        <tr>
                                                            <td style="text-align: left; width: 90px;">
                                                                <asp:Label ID="lblCantidadD0" runat="server" CssClass="ui-priority-primary" Text="Cantidad"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txvCantidadG" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblRacimosN0" runat="server" CssClass="ui-priority-primary" Text="Jornales"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txvJornalesD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("jornal") %>' Width="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="padding-top: 3px; padding-bottom: 3px">
                                                    <asp:GridView ID="gvLotes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" PageSize="5" Width="100%">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sel">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSeleccion" runat="server" OnCheckedChanged="chkSeleccion_CheckedChanged" ToolTip="Eliminar tercero" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="codTercero" HeaderText="cod" ReadOnly="True">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="5px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="desTercero" HeaderText="NombreTrabajador">
                                                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Cantidad">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Jornal">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtJornal" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("jornal") %>' Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="precioLabor" HeaderText="Prec$">
                                                                <ItemStyle Width="5px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="thead" />
                                                        <PagerStyle CssClass="footer" />
                                                    </asp:GridView>
                                                </div>
                                                <div>
                                                    <table class="ui-accordion">
                                                        <tr>
                                                            <td style="text-align: left; width: 150px;">
                                                                <asp:Label ID="Label1" runat="server" CssClass="ui-priority-primary" Text="Registro por novedad No."></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblRegistro" runat="server" Text='<%# Eval("registro") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Button ID="btnEliminarRegistro" runat="server" CommandName="Delete" CssClass="botones" Text="Eliminar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <SeparatorStyle />
                                    </asp:DataList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="upConsulta" runat="server" visible="False">
                    <div style="text-align: center">
                        <div style="padding-top: 10px;">
                            <table style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td style="text-align: left; width: 400px;">
                                        <asp:DropDownList ID="niddlCampo" runat="server" ToolTip="Selección de campo para busqueda" data-placeholder="Seleccione una opción..." CssClass="chzn-select" Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 200px;">
                                        <asp:DropDownList ID="niddlOperador" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="niddlOperador_SelectedIndexChanged" ToolTip="Selección de operador para busqueda" Width="90%">
                                            <asp:ListItem Value="like">Contiene</asp:ListItem>
                                            <asp:ListItem Value="&lt;&gt;">Diferente</asp:ListItem>
                                            <asp:ListItem Value="between">Entre</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="=">Igual</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Mayor o Igual</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Mayor que</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Menor o Igual</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Menor</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 220px;">
                                        <asp:TextBox ID="nitxtValor1" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="nitxtValor1_TextChanged" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="nitxtValor2" runat="server" CssClass="input" Visible="False" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter" OnClick="niimbAdicionar_Click" ToolTip="Clic aquí para adicionar parámetro a la busqueda"></asp:LinkButton>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:LinkButton runat="server" ID="imbBusqueda" Visible="false" CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="imbBusqueda_Click" ToolTip="Clic aquí para realizar la busqueda"></asp:LinkButton>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="7" style="text-align: center">
                                        <table class="ui-accordion">
                                            <tr>
                                                <td></td>
                                                <td style="width: 300px">
                                                    <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" Width="400px">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
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
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td></td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>


                            </table>
                        </div>
                        <div style="width: 100%;">
                            <div style="width: 100%;">
                                <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label>
                                <asp:GridView ID="gvTransaccion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" Width="100%" AllowPaging="True" OnPageIndexChanging="gvTransaccion_PageIndexChanging">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:ButtonField CommandName="Update" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fa fa-pencil"></ControlStyle>
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
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero" HeaderText="Numero">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="finca" HeaderText="Finca">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="observacion" HeaderText="Observación">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:CheckBoxField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead" />
                                    <PagerStyle CssClass="footer" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
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
        </form>
    </div>
</body>
</html>
