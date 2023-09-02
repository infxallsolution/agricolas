<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcesarArchivo.aspx.cs" Inherits="Nomina.WebForms.Formas.Pprogramacion.ProcesarArchivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Seguridad</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>

</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div class="principal">
            <asp:Panel ID="formContainer" ClientIDMode="Static" runat="server">
                <div class="text-center">
                    <asp:Label Text="Cargar Archivo" runat="server"></asp:Label>
                    <asp:FileUpload ID="fileUpload" AllowMultiple="false" runat="server" />
                </div>
                <hr />
                <div class="text-center">
                    <asp:Button ID="btnCargar" Text="Cargar" CssClass="botones" runat="server" OnClick="lbRegistrar_Click" />
                    <asp:Button ID="btnRevisar" runat="server" CssClass="botones" OnClick="btnGuardar_Click" Text="Revisar" />
                    <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="btnGuardar_Click" Text="Guardar" />
                </div>
                <hr />
                <div class="text-center">
                    <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            <hr />
            <div class="tablaGrilla">
                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm table-hover table-striped grillas" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" Width="100%">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:BoundField DataField="campo1" HeaderText="Tercero" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="campo2" HeaderText="Nombre" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="campo3" HeaderText="Fecha-Hora" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="campo4" HeaderText="Evento" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="campo5" HeaderText="Tipo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="footer" />
                    </asp:GridView>
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

</body>
</html>
