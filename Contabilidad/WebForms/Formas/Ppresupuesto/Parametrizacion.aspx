<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parametrizacion.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Ppresupuesto.Parametrizacion" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parametrización</title>
    <link href="http://app.infos.com/recursosInfos/css/root.css" rel="stylesheet" />
    <script type="text/javascript" src="http://app.infos.com/recursosInfos/lib/jquery/dist/jquery.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            position: fixed;
            left: 0px;
            top: -301px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            opacity: 0.99;
        }
    </style>
    </head>
<body>
 <%--   <div class="auto-style1">
        <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
    </div>--%>
    <div class="container text-left">
        <form id="form1" runat="server">
            <div class="row">
                <div class="col-3">
                </div>
                <div class="col-2">
                    <asp:Label ID="niTipoTransaccion" runat="server" Text="Tipo de transacción"></asp:Label>
                </div>
                <div class="col-4">
                    <asp:DropDownList ID="niddlTipoTransaccion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                </div>
                <div class="col-2">
                    <asp:Label ID="nilblperiodicidad" runat="server" Text="Periodicidad"></asp:Label>
                </div>
                <div class="col-4">
                    <asp:DropDownList ID="niddlPeriodicidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select">
                        <asp:ListItem Value="M">Mensual</asp:ListItem>
                        <asp:ListItem Value="A">Anual</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                </div>
                <div class="col-2">
                    <asp:Label ID="nilblProducto" runat="server" Text="Item"></asp:Label>
                </div>
                <div class="col-4">
                    <asp:DropDownList ID="niddlFormulacion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select">
                    </asp:DropDownList>
                </div>
            </div>
            <hr />
            <div class="row text-center">
                <div class="col-2">
                </div>
                <div class="col-8">
                    <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                    <asp:Button ID="nilbAsociarVariable" runat="server" CssClass="botones" OnClick="nilbAsociarCaracteristica_Click" Text="Asociar movimientos" ToolTip="Haga clic aqui para realizar la asociacion de movimientos" Width="200px" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Haga clic aqui para cancelar operación" />
                    <asp:Button ID="btnRegistrar" runat="server" CssClass="botones" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" OnClick="btnRegistrar_Click" Text="Guardar" ToolTip="Haga clic aqui para guardar registro" />

                </div>
            </div>
            <asp:Panel ID="pAsociacion" runat="server" Visible="False" CssClass="search-field">
                <table style="margin: 5px; width: 100%; text-align: left" id="tdCampos">
                    <tr>
                        <td></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label1" runat="server" Text="Movimientos"></asp:Label>
                        </td>
                        <td style="width: 450px">
                            <asp:DropDownList ID="ddlMovimientos" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlVariable_SelectedIndexChanged" data-placeholder="Seleccione una opción..." CssClass="chzn-select">
                            </asp:DropDownList></td>
                        <td style="width: 60px">
                            <asp:Label ID="Label10" runat="server" Text="Orden"></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:TextBox ID="txtOrden" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="4">
                            <table cellspacing="1" class="w-100">
                                <tr>
                                    <td style="width: 120px">
                                        <asp:CheckBox ID="chkActivo" runat="server" Checked="True" Text="Activo" />
                                    </td>
                                    <td style="width: 200px">
                                        <asp:CheckBox ID="chkResultado" runat="server" AutoPostBack="True" Text="Es Resultado" OnCheckedChanged="chkResultado_CheckedChanged" />
                                    </td>
                                    <td style="width: 200px">
                                        <asp:CheckBox ID="chkMostrarInforme" runat="server" Text="Mostrar Informe" />
                                    </td>
                                    <td style="width: 200px">
                                        <asp:CheckBox ID="chkDecimal" runat="server" Text="Maneja Decimales" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkCalcular" runat="server" Text="Mostrar antes de calcular" />
                                    </td>
                                    <td colspan="1">
                                        <asp:CheckBox ID="chkAlmacena" runat="server" Text="Calcular en informe" />
                                    </td>
                                    <td colspan="1">
                                        <asp:CheckBox ID="chkUtilizarEjecuatado" runat="server" Text="Utilizar ejecutado" />
                                    </td>

                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="4">
                            <asp:Panel ID="pFormula" runat="server" Visible="False">
                                <div style="padding: 10px">
                                    <table style="width: 900px">
                                        <tr>
                                            <asp:HiddenField ID="hdRetornaDatos" runat="server" />
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="Label9" runat="server" Text="Prioridad"></asp:Label>
                                            </td>
                                            <td style="width: 600px">
                                                <asp:TextBox ID="txvPrioridad" runat="server" CssClass="input" Width="50px"></asp:TextBox>
                                            </td>
                                            <td style="width: 180px; text-align: left;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="Label3" runat="server" Text="Sentencia"></asp:Label>
                                            </td>
                                            <td style="width: 600px">
                                                <asp:DropDownList ID="ddlSentencia" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="300px" Height="16px">
                                                    <asp:ListItem Value=" ">Seleccione una opción</asp:ListItem>
                                                    <asp:ListItem>,</asp:ListItem>
                                                    <asp:ListItem>(</asp:ListItem>
                                                    <asp:ListItem>)</asp:ListItem>
                                                    <asp:ListItem>+</asp:ListItem>
                                                    <asp:ListItem>-</asp:ListItem>
                                                    <asp:ListItem>*</asp:ListItem>
                                                    <asp:ListItem>/</asp:ListItem>
                                                    <asp:ListItem Value="ABS(">ABS</asp:ListItem>
                                                    <asp:ListItem Value="ACOS(">ACOS</asp:ListItem>
                                                    <asp:ListItem Value="ASIN(">ASIN</asp:ListItem>
                                                    <asp:ListItem Value="ATAN(">ATAN</asp:ListItem>
                                                    <asp:ListItem Value="CEILING(">CEILING</asp:ListItem>
                                                    <asp:ListItem Value="COS(">COS</asp:ListItem>
                                                    <asp:ListItem Value="COT(">COT</asp:ListItem>
                                                    <asp:ListItem Value="EXP(">EXP</asp:ListItem>
                                                    <asp:ListItem Value="FLOOR(">FLOOR</asp:ListItem>
                                                    <asp:ListItem Value="LOG(">LOG</asp:ListItem>
                                                    <asp:ListItem Value="LOG10(">LOG10</asp:ListItem>
                                                    <asp:ListItem Value="PI()">PI</asp:ListItem>
                                                    <asp:ListItem Value="POWER(">POWER</asp:ListItem>
                                                    <asp:ListItem>ROUND</asp:ListItem>
                                                    <asp:ListItem Value="SIN(">SIN</asp:ListItem>
                                                    <asp:ListItem Value="SQRT(">SQRT</asp:ListItem>
                                                    <asp:ListItem Value="SQUARE(">SQUARE</asp:ListItem>
                                                    <asp:ListItem Value="TAN(">TAN</asp:ListItem>
                                                    <asp:ListItem Value="NULLIF(">NULLIF</asp:ListItem>
                                                    <asp:ListItem Value="ISNULL(">ISNULL</asp:ListItem>
                                                    <asp:ListItem Value=",0)">Cerrar NULLIF</asp:ListItem>
                                                    <asp:ListItem Value=",0)">cerrar ISNULL</asp:ListItem>
                                                    <asp:ListItem Value="dbo.fDatosPrespuesto(">ObtenerDatosPresupuesto</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 180px; text-align: left;">
                                                <asp:Button ID="imbAddSentencia" runat="server" CssClass="botones" OnClick="imbAddSentencia_Click" Text="Adicionar sentencia" Width="160px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="Label5" runat="server" Text="Movimiento"></asp:Label>
                                            </td>
                                            <td style="width: 600px">
                                                <asp:DropDownList ID="ddlAnalisisF" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 180px; text-align: left;">
                                                <asp:Button ID="imbAddVariable" runat="server" CssClass="botones" OnClick="imbAddVariable_Click" Text="Adicionar movimiento" Width="160px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="Label7" runat="server" Text="Constante"></asp:Label>
                                            </td>
                                            <td style="width: 600px">
                                                <asp:TextBox ID="txtConstante" runat="server" CssClass="input"></asp:TextBox>
                                            </td>
                                            <td style="width: 180px; text-align: left;">
                                                <asp:Button ID="imbAddConstante" runat="server" CssClass="botones" OnClick="imbAddConstante_Click" Text="Adicionar constante" Width="160px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="Label2" runat="server" Text="Items RetornaDatos"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top; width: 600px">
                                                <asp:DropDownList ID="ddlItemsRetornaDatos" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="300px">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdConteoItems" runat="server" Value="0" />
                                            </td>
                                            <td style="width: 180px; text-align: left;">
                                                <asp:Button ID="imbItems" runat="server" CssClass="botones" OnClick="imbItems_Click" Text="Adicionar items" Width="160px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="Label6" runat="server" Text="Fórmula"></asp:Label>
                                            </td>
                                            <td rowspan="2" style="width: 600px">
                                                <asp:TextBox ID="txtFormula" runat="server" CssClass="input" Enabled="False" ForeColor="Navy" Height="50px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                            </td>
                                            <td style="width: 180px; text-align: left;">
                                                <asp:Button ID="imbValidarFormula" runat="server" CssClass="botones" OnClick="imbValidarFormula_Click" Text="Validar formula" Width="160px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;"></td>
                                            <td style="width: 180px; text-align: left;">
                                                <asp:Button ID="imbUndo" runat="server" CssClass="botones" OnClick="imbUndo_Click" Text="Eliminar formula" Width="160px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="Label8" runat="server" Text="Expresión"></asp:Label>
                                            </td>
                                            <td style="width: 600px">
                                                <asp:Label ID="lblExpresion" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 180px; text-align: left;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px; text-align: left;"></td>
                                            <td style="width: 600px">
                                                <asp:Label ID="lblResultadoFormula" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 180px; text-align: left;"></td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
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
                        <asp:BoundField DataField="movimiento" HeaderText="CodMov">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Descripcion" HeaderText="DesMovimiento">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="orden" HeaderText="Orden">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="40px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="resultado" HeaderText="EsResultado">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="almacena" HeaderText="Almcena">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mInforme" HeaderText="Informe">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mCalcular" HeaderText="Calcular">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mDecimal" HeaderText="Decimal">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="tipoTransaccion" HeaderText="Tipo">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Periodicidad" HeaderText="Periocidad">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="40px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
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
    </div>
</body>
</html>
