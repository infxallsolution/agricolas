<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultarLog.aspx.cs" Inherits="Administracion.WebForms.Formas.Pcontrol.ConsultarLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>

    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
       <body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table cellspacing="0" style="width: 100%">
                <tr>
                    <td></td>
                    <td width="100px">Consulta desde</td>
                    <td style="text-align: left" width="200px">
                        <asp:TextBox ID="txtDesde" runat="server" CssClass="input fecha"  Width="150px"></asp:TextBox>
                    </td>
                    <td width="100px">Hasta</td>
                    <td style="text-align: left" width="200px">
                        <asp:TextBox ID="txtHasta" runat="server" CssClass="input fecha"  Width="150px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
            </table>
            <table style="width: 100%" cellspacing="0">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="lbImprimir" runat="server" CssClass="botones" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>

            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" ReadOnly="True">
                            <ItemStyle  HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True">
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="entidad" HeaderText="Entidad" ReadOnly="True">
                            <ItemStyle  HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" ReadOnly="True">
                            <ItemStyle  HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mensajeSistema" HeaderText="Mensaje del sistema" ReadOnly="True">
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ip" HeaderText="IP" ReadOnly="True">
                            <ItemStyle  HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                    </Columns>
                   <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
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
        </form>
    </div>
  </body>
</html>
