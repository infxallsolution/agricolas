<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.Log" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
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
                    <td style="width: 350px; height: 25px; text-align: left">
                        <table style="width: 800px;">
                            <tr>
                                <td style="text-align: left; width: 120px;">
                                    <asp:Label ID="nilbFechaIni" runat="server" >Fecha inicial</asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="nitxtFechaIni" runat="server" class="input" Font-Bold="True" placeholder="dd/mm/aaaa" ToolTip="Formato fecha (dd/mm/aaaa)" Width="150px" CssClass="input fecha"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:Label ID="nilbFechaFinal" runat="server" >Fecha final</asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="nitxtFechaFinal" runat="server" class="input" Font-Bold="True" placeholder="dd/mm/aaaa" ToolTip="Formato fecha (dd/mm/aaaa)" Width="150px" CssClass="input fecha"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">Filtro</td>
                                <td colspan="3">
                                    <asp:TextBox ID="nitxtBusqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />

                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla">
                <asp:GridView ID="gvEncabezado" runat="server" Width="100%" GridLines="None" OnSelectedIndexChanged="gvEncabezado_SelectedIndexChanged" CssClass="table table-bordered table-sm  table-hover table-striped grillas" AutoGenerateColumns="False" AllowPaging="True" PageSize="4" OnPageIndexChanging="gvEncabezado_PageIndexChanging">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                    <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fa fa-check"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreUsuario" HeaderText="Nombre" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha Registro" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tabla" HeaderText="Tabla" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="operacion" HeaderText="Operación">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                    </Columns>
                   <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
                <asp:Label ID="nilblInformacionDetalle" runat="server"></asp:Label>
                <br />
                <asp:GridView ID="gvDetalle" runat="server" Width="100%" GridLines="None" CssClass="table table-bordered table-sm  table-hover table-striped grillas" AutoGenerateColumns="False">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:BoundField DataField="columna" HeaderText="Columna" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorAnt" HeaderText="Valor Anterior" ReadOnly="True"
                            SortExpression="valorAnt">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valorDes" HeaderText="Valor Nuevo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                 <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
                <br />
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
