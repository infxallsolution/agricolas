<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroNovedades.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.RegistroNovedades" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro Novedades</title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
  
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table class="w-100">
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 80%">
                        <div style="vertical-align: top; width: 100%; text-align: left" class="principal">
                            <table style="width: 100%; padding: 0; border-collapse: collapse;">
                                <tr>
                                    <td style="text-align: left; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver; vertical-align: bottom;">
                                        <asp:Button ID="niimbRegistro" runat="server" CssClass="botones" OnClick="niimbRegistro_Click" Text="Registro" ToolTip="Panel de registro" />
                                        <asp:Button ID="niimbConsulta" runat="server" CssClass="botones" OnClick="niimbConsulta_Click" Text="Consulta" ToolTip="Panel de consulta" />
                                    </td>
                                </tr>
                            </table>
                            <div id="upGeneral" runat="server">
                                <div id="upRegistro" runat="server" updatemode="Conditional">
                                    <table id="encabezado" style="width: 100%; padding: 0; border-collapse: collapse;">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" Text="Nuevo" ToolTip="Habilita el formulario para un nuevo registro" OnClick="nilbNuevo_Click" />
                                                <asp:Button ID="lbCancelar" runat="server" CssClass="botones" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" OnClick="lbCancelar_Click" />
                                                <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 0; border-collapse: collapse;">
                                                <table style="width: 100%; padding: 0; border-collapse: collapse;">
                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 125px; height: 25px; text-align: left">
                                                            <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Transacción" Visible="False"></asp:Label></td>
                                                        <td style="width: 450px; height: 25px; text-align: left">
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                                                Visible="False" Width="95%">
                                                            </asp:DropDownList></td>
                                                        <td style="width: 65px; height: 25px; text-align: left">
                                                            <asp:Label ID="lblNumero" runat="server" Text="Numero" Visible="False"></asp:Label></td>
                                                        <td style="width: 150px; height: 25px; text-align: left">
                                                            <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" OnTextChanged="txtNumero_TextChanged"
                                                                Visible="False" Width="150px" CssClass="input"></asp:TextBox></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="upCabeza" runat="server" visible="False">
                                        <div style="padding: 10px; border: 1px solid silver; border-radius: 5px; margin: 5px;">
                                            <table style="width: 100%;" id="datosCab">
                                                <tr>
                                                    <td style="vertical-align: top; width: 125px; text-align: left">
                                                        <asp:Label ID="lblFecha" runat="server" Visible="False">Fecha transacción</asp:Label></td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" ReadOnly="True"
                                                            Visible="False" CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox></td>
                                                    <td style="vertical-align: top; width: 100px; text-align: left"></td>
                                                    <td style="vertical-align: top; width: 400px; text-align: left">
                                                        <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
                                                        <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdCantidad" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 125px; height: 10px; text-align: left">
                                                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                                                        <asp:Label ID="lblConcepto" runat="server" Text="Concepto" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="96%" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="96%" AutoPostBack="True" OnSelectedIndexChanged="ddlConcepto_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 100px; height: 10px; text-align: left;">
                                                        <asp:Label ID="lblRemision" runat="server" Text="Remisión" Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="width: 400px; height: 10px; text-align: left;">
                                                        <asp:TextBox ID="txtRemision" runat="server" CssClass="input" Visible="False" Width="160px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left" colspan="4">
                                                        <asp:TextBox ID="txtObservacion" placeholder="Observaciones, notas o comentarios..." runat="server" CssClass="input" Height="40px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div id="upDetalle" runat="server" visible="False">
                                        <table class="w-100" id="datosDet">
                                            <tr>
                                                <td style="width: 25%">
                                                    <div style="padding: 10px; border: 1px solid silver; border-radius: 5px; margin: 5px;">
                                                        <table class="w-100 text-left">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblEmpleadoDetalle" runat="server" Text="Empleado" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlEmpleadoDetalle" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblConceptoDetalle" runat="server" Text="Concepto" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="ddlConceptoDetalle" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlConceptoDetalle_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txvCantidad" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="100%">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblValor" runat="server" Text="Valor" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txvValor" runat="server" onkeyup="formato_numero(this)" CssClass="input" Visible="False" Width="100%">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAñoInicial" runat="server" Text="Año Inicial" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtAñoInicial" placeholder="Ejemplo:2015" onkeyup="formato_numero(this)" runat="server" CssClass="input" MaxLength="4" Visible="False" Width="70%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblPeriodoInicial" runat="server" Text="Periodo inicial" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtPeriodoInicial" runat="server" placeholder="Ejemplo:15" onkeyup="formato_numero(this)" CssClass="input" MaxLength="2" Visible="False" Width="70%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAñoFinal" runat="server" Text="Año final" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtAñoFinal" runat="server" placeholder="Ejemplo:2015" onkeyup="formato_numero(this)" CssClass="input" MaxLength="4" Visible="False" Width="70%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblPeriodoFinal" runat="server" Text="Periodo final" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtPeriodoFinal" runat="server" CssClass="input" placeholder="Ejemplo:15" onkeyup="formato_numero(this)" MaxLength="2" Visible="False" Width="70%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblFrecuencia" runat="server" Text="Frecuencia" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txvFrecuencia" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" ToolTip="Número de intervalos de periodos a ejecutar en liquidación" Width="70%">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDetalle" runat="server" TextMode="MultiLine" Visible="False"
                                                                        Width="100%" CssClass="input" Height="60px" placeholder="Observaciones, notas o comentarios del registro..."></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 200px; text-align: center">
                                                                    <asp:Button ID="btnRegistrar" runat="server" OnClick="btnRegistrar_Click" Text="Agregar"
                                                                        Visible="False" CssClass="botones" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td style="width: 1%"></td>
                                                <td style="width: 74%; text-align: left; vertical-align: top;">
                                                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" RowHeaderColumn="cuenta" CssClass="table table-bordered table-sm  table-hover table-striped grillas" AllowSorting="True" OnSorting="gvLista_Sorting">
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
                                                            <asp:BoundField DataField="concepto" HeaderText="IdCon">
                                                                <HeaderStyle />
                                                                <ItemStyle Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nombreConcepto" HeaderText="NombreConcepto">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="empleado" HeaderText="IdEmp">
                                                                <HeaderStyle />
                                                                <ItemStyle Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nombreEmpleado" HeaderText="NombreEmpleado">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cantidad" DataFormatString="{0:N2}" HeaderText="Cant">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor" DataFormatString="{0:N2}" HeaderText="Valor">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="añoInicial" HeaderText="AñoI">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="añoFinal" HeaderText="AñoF">
                                                                <ItemStyle Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="pInicial" HeaderText="PI">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="pFinal" HeaderText="PF">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FR" HeaderText="FR">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="registro" HeaderText="Reg">
                                                                <ItemStyle Width="10px" />
                                                            </asp:BoundField>
                                                            <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                                                                <ItemStyle Width="10px" />
                                                            </asp:CheckBoxField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="thead" />
                                                        <PagerStyle CssClass="footer" />
                                                    </asp:GridView>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="upConsulta" runat="server" visible="False">
                                    <div style="padding: 10px; border: 1px solid silver; border-radius: 5px; margin: 5px; text-align: center;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="datagrid-header-row"></td>
                                                <td style="text-align: left; width:300px" >
                                                    <asp:DropDownList ID="niddlCampo" runat="server" ToolTip="Selección de campo para busqueda"
                                                        Width="95%" CssClass="chzn-select-deselect">
                                                    </asp:DropDownList></td>
                                                <td style="text-align: left; width:200px" >
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
                                                <td style="width: 250px; text-align: left">
                                                    <asp:TextBox ID="nitxtValor1" runat="server" Width="95%" CssClass="input" AutoPostBack="True" OnTextChanged="nitxtValor1_TextChanged1"></asp:TextBox><asp:TextBox
                                                        ID="nitxtValor2" runat="server" Visible="False" Width="95%" CssClass="input"></asp:TextBox></td>
                                                <td style="width: 80px; text-align: center">
                                                    <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter " OnClick="niimbAdicionar_Click"></asp:LinkButton>

                                                </td>
                                                <td style="width: 80px; text-align: center">
                                                    <asp:LinkButton runat="server" ID="imbBusqueda" Visible="false" CssClass="btn btn-default btn-sm btn-success fa fa-search" OnClick="imbBusqueda_Click"></asp:LinkButton>
                                                </td>
                                                <td class="datagrid-header-row"></td>
                                            </tr>
                                        </table>
                                        <div class="row">
                                            <div class="col-4">
                                            </div>
                                            <div class="col-8">
                                                <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" Width="400px">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="imElimina" runat="server" CommandName="Delete" CssClass="btn btn-default btn-sm btn-danger fa fa-minus-circle "  ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="action-item" HorizontalAlign="Center" Width="20px" />
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

                                        </div>
                                        <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label>
                                        <asp:GridView ID="gvTransaccion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" Width="100%">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:ButtonField CommandName="update" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                                    <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt" />
                                                    <ItemStyle CssClass="action-item" Width="20px" />
                                                    <HeaderStyle CssClass="action-item" />
                                                </asp:ButtonField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imElimina" runat="server" CommandName="Delete" CssClass="btn btn-default btn-sm btn-danger fa fa-ban " OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="action-item" Width="20px" />
                                                    <HeaderStyle CssClass="action-item" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                                    <ItemStyle Width="5px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="numero" HeaderText="Numero">
                                                    <ItemStyle Width="5px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                                    <ItemStyle Width="5px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ccosto" HeaderText="cCosto">
                                                    <ItemStyle Width="5px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nota" HeaderText="Observaciones">
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                                                    <ItemStyle Width="5px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <HeaderStyle CssClass="thead" />
                                            <PagerStyle CssClass="footer" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
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
        </form>
    </div>
</body>
</html>
