<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PeriodoNomina.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.PeriodoNomina" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label8" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAño" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged" Visible="False" Width="30%">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="chzn-select-deselect" Visible="False" Width="60%">
                            <asp:ListItem Value="1">Enero</asp:ListItem>
                            <asp:ListItem Value="2">Febrero</asp:ListItem>
                            <asp:ListItem Value="3">Marzo</asp:ListItem>
                            <asp:ListItem Value="4">Abril</asp:ListItem>
                            <asp:ListItem Value="5">Mayo</asp:ListItem>
                            <asp:ListItem Value="6">Junio</asp:ListItem>
                            <asp:ListItem Value="7">Julio</asp:ListItem>
                            <asp:ListItem Value="8">Agosto</asp:ListItem>
                            <asp:ListItem Value="9">Septiembre</asp:ListItem>
                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label16" runat="server" Text="No. Periodo" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvPeriodoDetalle" runat="server" AutoPostBack="True" CssClass="input" Visible="False" Width="60%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label19" runat="server" Text="Tipo Nomina" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label21" runat="server" Text="Días nómina" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvDiasNomina" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="60%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lbFechaIni" runat="server" Visible="False">Fecha inicial</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaIni" runat="server" class="input" Font-Bold="True" ReadOnly="True" ToolTip="Formato fecha (dd/mm/yyyy)" placeholder="DD/MM/YYYY" Visible="False" Width="60%" CssClass="input fecha"></asp:TextBox>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lbFechaFinal" runat="server" Visible="False">Fecha final</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaFinal" runat="server" class="input" Font-Bold="True" ReadOnly="True" ToolTip="Formato fecha (dd/mm/yyyy)" placeholder="DD/MM/YYYY" Visible="False" Width="60%" CssClass="input fecha"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lbFechaCorte" runat="server" Visible="False">Fecha de corte</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaCorte" runat="server" class="input" Font-Bold="True" ReadOnly="True" ToolTip="Formato fecha (dd/mm/yyyy)" placeholder="DD/MM/YYYY" Visible="False" Width="60%" CssClass="input fecha"></asp:TextBox>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="lbFechaPago" runat="server" Visible="False">Fecha de pago</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaPago" runat="server" class="input" Font-Bold="True" ReadOnly="True" ToolTip="Formato fecha (dd/mm/yyyy)" placeholder="DD/MM/YYYY" Visible="False" Width="60%" CssClass="input fecha"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left;"></td>
                    <td style="text-align: left; width: 400px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAgronomico" runat="server" Text="Agronomico" Visible="False" />
                    </td>
                    <td style="width: 150px; text-align: left;"></td>
                    <td style="text-align: left; width: 400px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCerrado" runat="server" Text="Cerrado" Visible="False" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
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
                                <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noPeriodo" HeaderText="No periodo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaInicial" HeaderText="Fecha inicial" ReadOnly="True" DataFormatString="{0:dd/MM/yyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaFinal" HeaderText="Fecha final" ReadOnly="True" DataFormatString="{0:dd/MM/yyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaCorte" DataFormatString="{0:dd/MM/yyy}" HeaderText="Fecha corte">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaPago" DataFormatString="{0:dd/MM/yyy}" HeaderText="Fecha Pago">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoNomina" HeaderText="Tipo nomina" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="diasNomina" HeaderText="Dias N">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="cerrado" HeaderText="Cerrado">
                            <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="agronomico" HeaderText="Agro.">
                            <HeaderStyle />
                            <ItemStyle Width="15px" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="periodo" HeaderText="Periodo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
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
</body>
</html>