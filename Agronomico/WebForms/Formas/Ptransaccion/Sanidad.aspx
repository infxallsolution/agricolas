<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sanidad.aspx.cs" Inherits="Agronomico.WebForms.Formas.Ptransaccion.Sanidad" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro Novedades</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <asp:Button ID="niimbRegistro" runat="server" OnClick="niimbRegistro_Click" Text="Registro" CssClass="botones" />
            <asp:Button ID="imbConsulta" runat="server" OnClick="imbConsulta_Click" CssClass="botones" Text="Consulta" />
            <hr />
            <div id="upGeneral" runat="server">
                <div style="text-align: center;">
                    <asp:Button ID="nilbNuevo" runat="server" ToolTip="Habilita el formulario para un nuevo registro" OnClick="nilbNuevo_Click" CssClass="botones" Text="Nuevo" />
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
                                    <asp:TextBox ID="txtNumero" runat="server" Visible="False" Width="95%" CssClass="input"></asp:TextBox>
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
                                <td style="vertical-align: top; width: 125px; text-align: left">
                                    <asp:Label ID="lbFecha" runat="server"
                                        Visible="False" >Fecha transacción</asp:Label></td>
                                <td style="vertical-align: top; width: 175px; text-align: left">
                                    <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True"
                                         CssClass="input fecha"></asp:TextBox></td>
                                <td style="vertical-align: top; width: 100px; text-align: left">
                                    <asp:Label ID="lblReferencia" runat="server" Text="Referencia" ></asp:Label>
                                </td>
                                <td style="vertical-align: top; width: 400px; text-align: left">
                                    <asp:DropDownList ID="ddlReferencia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged"  Width="300px">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
                                    <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                                </td>
                                <td style="width: 100px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 10px; text-align: left"></td>
                                <td style="width: 125px; height: 10px; text-align: left">
                                    <asp:Label ID="lblFinca" runat="server" Text="Finca" ></asp:Label>
                                </td>
                                <td style="width: 175px; height: 10px; text-align: left">
                                    <asp:DropDownList ID="ddlFinca" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged"  Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 100px; height: 10px; text-align: left;">
                                    <asp:Label ID="lblSeccion" runat="server" Text="Sección" ></asp:Label>
                                </td>
                                <td style="width: 400px; height: 10px; text-align: left;">
                                    <asp:DropDownList ID="ddlSeccion" runat="server"  Width="300px" data-placeholder="Seleccione una opción..." CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSeccion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 100px; height: 10px; text-align: left;"></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 10px; text-align: left"></td>
                                <td style="width: 125px; height: 10px; text-align: left">
                                    <asp:Label ID="lblLote" runat="server" Text="Lote" ></asp:Label>
                                </td>
                                <td style="width: 175px; height: 10px; text-align: left">
                                    <asp:DropDownList ID="ddlLote" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlLote_SelectedIndexChanged"  Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 100px; height: 10px; text-align: left;"></td>
                                <td style="width: 400px; height: 10px; text-align: left;"></td>
                                <td style="width: 100px; height: 10px; text-align: left;"></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 10px; text-align: left"></td>
                                <td style="width: 125px; height: 10px; text-align: left">
                                    <asp:Label ID="lblRemision" runat="server" Text="Remisión" ></asp:Label>
                                </td>
                                <td style="width: 175px; height: 10px; text-align: left">
                                    <asp:TextBox ID="txtRemision" runat="server" CssClass="input"  Width="160px"></asp:TextBox>
                                </td>
                                <td style="width: 100px; height: 10px; text-align: left"></td>
                                <td style="width: 400px; height: 10px; text-align: left"></td>
                                <td style="width: 100px; height: 10px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 10px; text-align: left"></td>
                                <td style="width: 125px; height: 10px; text-align: left">
                                    <asp:Label ID="lblObservacion" runat="server" Text="Notas" ></asp:Label>
                                </td>
                                <td style="height: 10px; text-align: left" colspan="3">
                                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" TextMode="MultiLine"  Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 100px; height: 10px; text-align: left"></td>
                            </tr>
                            </table>
                    </div>
                </div>

                <div style="text-align: center; padding: 5px;">
                    <div id="upDetalle" runat="server" >
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td colspan="6">
                                    <h6>Detalle de la transacción</h6>
                                    <hr />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="6"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblConcepto" runat="server" Text="Concepto" ></asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:DropDownList ID="ddlConcepto" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlConcepto_SelectedIndexChanged"  Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblUmedida" runat="server" Text="Und. Medida" ></asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:DropDownList ID="ddlUmedida" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..."  Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lbFechaD" runat="server" OnClick="lbFechaD_Click"  >Fecha</asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:TextBox ID="txtFechaD" runat="server" CssClass="input fecha" Font-Bold="True" ></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblLinea" runat="server" Text="Linea" ></asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..."  Width="200px">
                                    </asp:DropDownList>
                                </td>

                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblPalma" runat="server" Text="Palma" ></asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:TextBox ID="txtPalma" runat="server" CssClass="input" onkeyup="formato_numero(this)" ></asp:TextBox>
                                </td>
                                <td style="width: 200px; text-align: left">&nbsp;</td>
                                <td style="width: 200px; text-align: left"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblGrupoC" runat="server" Text="Grupo Carac." ></asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:DropDownList ID="ddlGrupoC" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlGrupoC_SelectedIndexChanged"  Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblCaracteristica" runat="server" Text="Caracteristica" ></asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:DropDownList ID="ddlCaracteristica" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..."  Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 200px; text-align: left">&nbsp;</td>
                                <td style="width: 200px; text-align: left"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" ></asp:Label>
                                </td>
                                <td style="width: 200px; text-align: left">
                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="input" onkeyup="formato_numero(this)" ></asp:TextBox>
                                </td>
                                <td style="width: 200px; text-align: left"></td>
                                <td style="width: 200px; text-align: left"></td>
                                <td style="width: 200px; text-align: left"></td>
                                <td style="width: 200px; text-align: left"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblDetalle" runat="server" Text="Detalle" ></asp:Label>
                                </td>
                                <td colspan="5" style="text-align: left">
                                    <asp:TextBox ID="txtDetalle" runat="server" CssClass="input" Height="30px" TextMode="MultiLine"  Width="90%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 200px; text-align: left"></td>
                                <td style="width: 200px; text-align: left"></td>
                                <td style="width: 200px; text-align: left"></td>
                                <td style="width: 200px; text-align: left"></td>
                                <td style="width: 200px; text-align: left">
                                    <asp:Button ID="btnRegistrar" runat="server" OnClick="btnRegistrar_Click" Text="Registrar"  CssClass="botones" />
                                </td>
                                <td style="width: 200px; text-align: left"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="6">
                                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnSorting="gvLista_Sorting" RowHeaderColumn="cuenta" Width="100%">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                                <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                                <ItemStyle Width="20px" CssClass="action-item" />
                                                <HeaderStyle CssClass="action-item" />
                                            </asp:ButtonField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                <HeaderStyle CssClass="action-item" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="concepto" HeaderText="Concepto">
                                                <HeaderStyle />
                                                <ItemStyle Width="25px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="conceptoNombre" HeaderText="NombreConcepto">
                                                <HeaderStyle />
                                                <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="gCaracteristica" HeaderText="G.Car">
                                                <ItemStyle Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NGrupoCaracteristica" HeaderText="Nom G.C">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="caracteristica" HeaderText="Car.">
                                                <ItemStyle Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nCaracteristica" HeaderText="Nom. Cara.">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="linea" HeaderText="Linea">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="palma" HeaderText="Pal.">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cantidad">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="uMedida" HeaderText="uMedida">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="detalle" HeaderText="Detalle" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="registro">
                                                <HeaderStyle BackColor="White" />
                                                <ItemStyle BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="thead" />
                                        <PagerStyle CssClass="footer" />
                                    </asp:GridView>
                                    <br />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>

              <div id="upConsulta" runat="server" >
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
                                        <asp:TextBox ID="nitxtValor2" runat="server" CssClass="input"  Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:Label runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter" OnClick="niimbAdicionar_Click" ToolTip="Clic aquí para adicionar parámetro a la busqueda"></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:Label runat="server" ID="imbBusqueda"  CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="imbBusqueda_Click" ToolTip="Clic aquí para realizar la busqueda"></asp:Label>
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
                                                                    <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
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
                                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                            <ItemStyle Width="20px" CssClass="action-item" />
                                            <HeaderStyle CssClass="action-item" />
                                        </asp:ButtonField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
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