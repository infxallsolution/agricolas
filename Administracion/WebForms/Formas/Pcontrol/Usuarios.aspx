<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Administracion.WebForms.Formas.Pcontrol.Usuarios" %>

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
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
            <hr />
            <table id="tdCampos" width="100%">
                <tr>
                    <td></td>
                    <td width="150px" style="text-align: left; width: 150px">
                        <asp:Label ID="Label2" runat="server" Text="Usuario" Visible="False"></asp:Label>
                    </td>
                    <td class="auto-style1" width="300px" style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" Width="200px" AutoComplete="off" OnTextChanged="txtConcepto_TextChanged" Visible="False"></asp:TextBox>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label3" runat="server" Text="Descripción" Visible="False"></asp:Label>
                    </td>
                    <td class="auto-style1" style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input" Width="100%" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label6" runat="server" Text="E-Mail" Visible="False"></asp:Label>
                    </td>
                    <td class="auto-style1" style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="input" Width="100%" TextMode="Email" ValidateRequestMode="Enabled" ViewStateMode="Enabled" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label4" runat="server" Text="Contraseña" Visible="False"></asp:Label>
                    </td>
                    <td class="auto-style1" style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtContrasena" runat="server" CssClass="input" TextMode="Password" Width="100%" OnTextChanged="txtConcepto_TextChanged" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px"></td>
                    <td class="auto-style1" style="text-align: left; width: 400px">
                        <asp:LinkButton ID="lbCambiarContrasena" runat="server" ForeColor="#003366" ToolTip="Clic aquí para cambiar la contaseña" data-toggle="modal" data-target="#cambiarpass" Visible="False">Cambiar Contraseña</asp:LinkButton>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style1"></td>
                    <td class="auto-style1" style="text-align: left; width: 400px">
                        <asp:LinkButton ID="lbRestablecerContrasena" runat="server" ForeColor="#003366" ToolTip="Clic aquí para cambiar la contaseña" data-toggle="modal" data-target="#restablecerpass" Visible="False">Restablecer Coontraseña</asp:LinkButton>
                    </td>
                    <td></td>
                </tr>
            </table>
            <div class="modal fade" id="cambiarpass" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Cambiar contraseña</h4>
                        </div>
                        <div class="modal-body">
                            <div>
                                <asp:Label ID="lblContrasenaAnterior" runat="server" Text="Contraseña Anterior" Visible="true"></asp:Label>
                            </div>

                            <div>
                                <asp:TextBox ID="txtContrasenaAnterior" runat="server" CssClass="input" TextMode="Password" Visible="true" Width="200px"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="Label1" runat="server" Text="Contraseña Nueva" Visible="true"></asp:Label>
                            </div>
                            <div>
                                <asp:TextBox ID="txtContrasenaNueva" runat="server" CssClass="input" TextMode="Password" Width="200px"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Button ID="lbCambiar" runat="server" Visible="true" CssClass="botones" OnClientClick="return false;" Text="Cambiar contraseña" ToolTip="Cambiar contraseña" />
                            </div>


                        </div>
                    </div>

                </div>
            </div>
            <div class="modal fade" id="restablecerpass" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Restablecer contraseña</h4>
                        </div>
                        <div class="modal-body">
                            <div>
                                <asp:Label ID="Label7" runat="server" Text="Contraseña Nueva" Visible="true"></asp:Label>
                            </div>
                            <div>
                                <asp:TextBox ID="txtRestablecer" runat="server" CssClass="input" TextMode="Password" Width="200px"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Button ID="lbRestablecer" runat="server" CssClass="botones" Text="Restablecer" ToolTip="Restablecer contraseña" OnClientClick="return false;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="Email" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:CheckBoxField>
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

    <script type="text/javascript">
        $("#lbRestablecer").click(
            function comprobarPass() {
                var nueva = $("#txtRestablecer").val();
                var usuario = $("#txtCodigo").val();

                if (nueva.length === 0) {
                    Mensaje("Advertencia", "Ingrese las contraseñas", "warning");
                    return false;
                }

                $.ajax({
                    type: 'POST',
                    url: 'Usuarios.aspx/restablecerpass',
                    data: "{ user: '" + usuario + "', passnue: '" + nueva + "'}",
                    contentType: 'application/json; utf-8',
                    dataType: "json",
                    success: function (data) {
                        if (data.d !== null) {

                            switch (data.d) {
                                case "0":
                                    Mensaje("Exitoso", "Contraseña restablecida exitosamente", "info");
                                    $(restablecerpass).modal('toggle');
                                    return false;
                                case "1":
                                    Mensaje("Error", "Error al restablecer", "warning");
                                    $(restablecerpass).modal('toggle');
                                    return false;
                            }
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        Mensaje("Error", "Error debido a " + errorThrown, "warning");
                    }

                });
            });


        $("#lbCambiar").click(
            function comprobarPass() {
                var anterior = $("#txtContrasenaAnterior").val();
                var nueva = $("#txtContrasenaNueva").val();
                var usuario = $("#txtCodigo").val();

                if (anterior.length == 0 || nueva.length == 0) {
                    Mensaje("Advertencia", "Ingrese las contraseñas", "warning");
                    return false;
                }

                $.ajax({
                    type: 'POST',
                    url: 'Usuarios.aspx/cambiarPass',
                    data: "{ usuario: '" + usuario + "', passAnt: '" + anterior + "', passNue: '" + nueva + "'}",
                    contentType: 'application/json; utf-8',
                    dataType: "json",
                    success: function (data) {
                        if (data.d != null) {

                            switch (data.d) {
                                case "0":
                                    Mensaje("Exitoso", "Contraseña modificada exitosamente", "info");
                                    $(cambiarpass).modal('toggle');
                                    return false;
                                    break;
                                case "1":
                                    Mensaje("Error", "Contraseña anterior incorrecta", "warning");
                                    return false;
                                    break;
                                case "2":
                                    Mensaje("Error", "La nueva contraseña debe tener mas de 4 caracteres", "warning");
                                    return false;
                                    break;
                                case "3":
                                    Mensaje("Error", "Error al cambiar la contraseña operación no realizada", "warning");
                                    return false;
                                    break;



                            }
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        Mensaje("Error", "Error debido a " + errorThrown, "warning");
                    }

                });
            });

        function Mensaje(title, text, type) {
            $(document).ready(function () {
                swal({
                    title: title,
                    html: text,
                    type: type,
                    confirmButtonText: "Aceptar",
                    showCancelButton: true,
                    cancelButtonText: 'Mostrar detalle <i class=""fa fa-angle-right""></i>'
                }).then(
                    function () { },
                    function (dismiss) {
                        if (dismiss === 'cancel') {
                            swal({
                                title: "<small>Detalle de la excepción </small>",
                                html: "<textarea disabled style='width:100%; height:300px;max-height:299px;resize:none;font-size:initial;font-family:monospace;'>" + text + "</textarea>",
                                confirmButtonText: "Aceptar",
                                animation: false,
                            });
                        }
                    });
            });
        }


    </script>
</body>
</html>