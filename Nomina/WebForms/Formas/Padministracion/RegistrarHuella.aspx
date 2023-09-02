<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarHuella.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.RegistrarHuella" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="http://app.infos.com/recursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosInfos/lib/jquery/dist/jquery.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#nilbNuevo').click(function (e) {
                e.preventDefault();
                window.location.href = 'HuellaR:huella';
            });

            var url = 'ws://app.infos.com/Nomina/wsHandler.ashx';
            function llamarServidor() {
                $("#form1").submit();
            }
            ws = new WebSocket(url);

            ws.onopen = function () {
            };

            var contadorlinea = 0;
            ws.onmessage = function (e) {

                if (e.data.length < 50) {

                    if (e.data != "*") {
                        $('#txtMensaje').prepend(e.data);
                    }
                    return;
                }

                if (e.data != null) {
                    var obj = jQuery.parseJSON(e.data);
                    var data = obj.Imagehuella;
                    console.log(obj.Empresa);
                    console.log($('#hfempresa').val());

                    if ($('#hfempresa').val() == obj.Empresa) {
                        $('#hfHuella').val(obj.Huella);
                        $('#hfIntentos').val(obj.Intento);
                        $('#txtMensaje').prepend("Huellas para terminar el registro: " + String(obj.Intento) + "\n");
                        $("#pbFoto").removeAttr("src");
                        $("#pbFoto").attr("src", "data:image/jpeg;base64," + data);
                        if (obj.Intento == 0) {
                            llamarServidor();
                        }
                    }

                }

            };
            ws.onclose = function () {

                ws.send("");

            };

            ws.onerror = function (e) {
                ws.send("");

            };

        });

    </script>
</head>
<body>

    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <div class="row">
                <div class="col-2">
                </div>
                <div class="col-8">
                    <asp:HiddenField ID="hfempresa" runat="server" />
                    <asp:HiddenField ID="hfHuella" runat="server" />
                    <asp:HiddenField ID="hfIntentos" runat="server" />
                    <h4 style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #003399">Registro de huella digital</h4>
                </div>
            </div>

            <div class="row">

                <div class="col-2">
                </div>
                <div class="col-8">

                    <div class="row">
                        <div class="col-5">
                            <div style="padding: 15px; margin: 15px; min-height: 420px; border: 1px solid #003399">
                                <asp:Image ID="pbFoto" runat="server" Width="100%" Height="420px" />
                            </div>
                        </div>
                        <div class="col-7">
                            <div class="row text-center">
                            </div>
                            <div class="row text-center">
                                <div class="col-9 text-justify">
                                    <asp:DropDownList ID="ddlFuncionario" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlFuncionario_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-3 text-justify">
                                    <a id="nilbNuevo" runat="server" class="botones">Abrir servicio</a>
                                </div>
                            </div>
                            <div class="row text-center">
                                <div class="col-9 text-justify">
                                    <asp:DropDownList ID="ddlDedo" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%">
                                        <asp:ListItem Value="P">Pulgar</asp:ListItem>
                                        <asp:ListItem Value="I">Índice</asp:ListItem>
                                        <asp:ListItem Value="M">Medio</asp:ListItem>
                                        <asp:ListItem Value="A">Anular</asp:ListItem>
                                        <asp:ListItem Value="ME">Meñique</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-3 text-justify">
                                    <asp:LinkButton runat="server" ID="btnEliminar" CssClass="btn btn-default btn-sm btn-danger fa fa-trash" ToolTip="Clic aquí para eliminar la huella" OnClick="btnEliminar_Click"></asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            <div class="row">
                                <asp:Label ID="lbMensaje" Width="100%" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <asp:TextBox ID="txtMensaje" runat="server" CssClass="input" Font-Size="15pt" Height="250px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </div>
                            <div id="divHistory">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-2">
                </div>
                <div class="col-10">
                </div>
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
