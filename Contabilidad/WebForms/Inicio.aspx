<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Contabilidad.WebForms.Inicio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

     <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <link href="http://app.infos.com/recursosinfos/css/rootLogin.css" rel="stylesheet" />
</head>
<body>

    <div class="limiter">
        <div class="container-login100"
            style="background-image: url('http://app.infos.com/recursosinfos/Imagen/Fondos/fondoLogin.jpeg');">
            <div class="wrap-login100">
                <div style="width: 100%; padding-top: 2%; padding-bottom: 50px; text-align: center;">
                    <img style="width: 300px" src="http://app.infos.com/recursosinfos/Imagen/Logos/logoInfos.svg" />
                </div>
                <form class="login100-form validate-form" runat="server">
                    <span id="reauth-email" class="reauth-email"></span>
                    <div class="wrap-input100 validate-input" data-validate="Enter username">
                        <asp:TextBox ID="txtUsuario" runat="server" class="input100" placeholder="Usuario" autocomplete="off"></asp:TextBox>
                        <span class="focus-input100" data-placeholder="&#xf207;"></span>
                    </div>
                    <div class="wrap-input100 validate-input" data-validate="Enter password">
                        <asp:TextBox ID="txtClave" runat="server" class="input100" placeholder="Contraseña" required TextMode="Password"></asp:TextBox>
                        <span class="focus-input100" data-placeholder="&#xf191;"></span>
                    </div>
                    <div class="container-login100-form-btn" style="padding-top: 40px">
                        <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar sesión" CssClass="btnInicio " OnClick="btnIniciarSesion_Click" />
                    </div>
                </form>
                <div style="padding-top: 40px; padding-left: 20px; padding-right: 20px; text-align: center; font-size: 12px;">
                    <span>Bienvenidos a nuestro sistema de información INFOS.
                <br />
                        © 2017 Todos los derechos reservados</span>
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
</body>
</html>
