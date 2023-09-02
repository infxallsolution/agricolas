<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuInfos.aspx.cs" Inherits="Agronomico.WebForms.Formas.MenuInfos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Infos</title>
    <link href="http://app.infos.com/recursosinfos/css/rootMenuMod.css" rel="stylesheet" />
    <link href="http://app.infos.com/recursosinfos/lib/ihover-gh-pages/src/ihover.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js"></script>
    <script src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="http://app.infos.com/recursosinfos/lib/jquery-easyui/jquery.easyui.min.js"></script>

    <script type="text/javascript">
        Reloj();
        function Reloj() {
            var tiempo = new Date();
            $("#lbFecha").text(tiempo.toLocaleString());
            setTimeout('Reloj()', 1000);
        }
        $(document).ready(function () {
            $('.navigation ul').fadeOut();
            $('.navigation li').hover(
                function () {
                    $('ul', this).fadeIn();
                },
                function () {
                    $('ul', this).fadeOut();
                }
            );
            $(".imagen").mouseover(function () {
                $(this).find(".tit").css("display", "none");
            });
            $(".imagen").mouseout(function () {
                $(this).find(".tit").css("display", "block");
            });

        });
        function validarPasswd() {

            var p1 = document.getElementById("txtContrasenaNueva").value;
            var p2 = document.getElementById("txtContrasenaNueva1").value;
            var p3 = document.getElementById("Clave").value;
            var p4 = document.getElementById("txtContrasenaAnterior").value;

            var espacios = false;
            var cont = 0;
            while (!espacios && (cont < p1.length)) {
                if (p1.charAt(cont) == " ")
                    espacios = true;
                cont++;
            }

            if (espacios) {
                alert("La contraseña no puede contener espacios en blanco");
                return false;
            }

            if (p1.length == 0 || p2.length == 0) {
                alert("Los campos de la password no pueden quedar vacios");
                return false;
            }

            if (p1 != p2) {
                alert("Las passwords deben de coincidir");
                return false;
            }
            if (p3 != p4) {

                alert("Contraseña Anterior no valida"); return false;
            }

            else {

                return true;
            }
        }
        $(document).ready(function () {
            $('.button').click(function () {
                type = $(this).attr('data-type');
                $('.overlay-container').fadeIn(function () {
                    window.setTimeout(function () {
                        $('.window-container.' + type).addClass('window-container-visible');
                    }, 100);
                });
            });
            $('.close').click(function () {
                $('.overlay-container').fadeOut().end().find('.window-container').removeClass('window-container-visible');
            });
        });
        $(document).ready(function () {
            $('.buttonr').click(function () {
                type = $(this).attr('data-type');
                $('.overlay-containerr').fadeIn(function () {
                    window.setTimeout(function () {
                        $('.window-containerr.' + type).addClass('window-containerr-visible');
                    }, 100);
                });
            });
            $('.close').click(function () {
                $('.overlay-containerr').fadeOut().end().find('.window-containerr').removeClass('window-containerr-visible');
            });
        });
        function mainmenu() {
            $("#nav ul").css({ display: "none" });
            $(" #nav li ul li").hover(function () {
                $(this).find('ul:first:hidden').css({ visibility: "visible", display: "none" }).slideDown(300);
            }, function () {
                $(this).find('ul:first').slideUp(800);
            });

            $("#nav li").click(function (e) {
                if (!$(this).hasClass('selected')) {
                    $("body").find("#nav li.selected").find('ul').slideUp(800);
                    $("body").find("#nav li").removeClass('selected');
                    $(this).addClass("selected");
                    $(this).find('ul:first:hidden').css({ visibility: "visible", display: "none" }).slideDown(300);
                }
                else {

                    $(this).find('ul:first').slideUp(800);
                    $("#nav li").removeClass("selected");
                }
                e.stopPropagation();
            }
            );

            $("body").click(function () { // binding onclick to body
                $("body").find("#nav li.selected").find('ul').slideUp(800); // hiding popups
                $("body").find("#nav li").removeClass('selected');
            });

        }
        $(document).ready(function () {
            mainmenu();
        });
        var contador = 0;
        function contadorMenos() {
            contador--;

        }
        function addTab(title, url) {

            if (contador < 8) {
                if ($('#tt').tabs('exists', title)) {
                    $('#tt').tabs('select', title);


                } else {
                    contador++;
                    var content = '<iframe  frameborder="0"  src="' + url + '" style="width:100%;height:100vh"></iframe>';
                    $('#tt').tabs('add', {
                        title: title,
                        content: content,
                        closable: true
                    });
                }
            }
        }
    </script>
    <style type="text/css">
        #footer {
            color: #fff;
            position: fixed;
            bottom: 0;
            width: 100%;
            height: 40px;
            padding-bottom: 16px;
            background: #2E4E9D;
            border-top: 2px solid #1A95A4;
            z-index: 2000;
        }
    </style>

</head>
<body style="margin: 0; padding: 0; width: 100%;">
    <div class="loading">
        <i class="fas fa-spinner fa-spin fa-5x"></i>
    </div>
    <form id="form1" runat="server" style="margin: 0; padding: 0; width: 100%;">
        <div id="menu" class="row" style="margin: 0; padding: 0; width: 100%;">
            <div class="col-1">
                <asp:ImageButton ID="ImageButton1" Width="100px" runat="server" ImageUrl="http://app.infos.com/recursosinfos/imagen/Logos/logoInfosBlanco.svg" OnClick="imbPrincipal_Click" ToolTip="Volver al menu principal" />
            </div>
            <div class="col-10">
                <ul id="nav">
                    <li class="" style="text-align: left"><a class="" href="#">Administración</a>
                        <ul class="submenu">
                            <li class="" style="text-align: left"><a class="" href="#">Parametros</a>
                                <ul class="subsubmenu">
                                    <li><a href="#" onclick="addTab('Variedad','Padministracion/Variedad.aspx',true)">Variedades</a></li>
                                    <li><a href="#" onclick="addTab('UnidadMedida','Padministracion/UnidadMedida.aspx',true)">Unidad de medida</a></li>
                                    <li><a href="#" onclick="addTab('Tipo Canal','Padministracion/TipoCanal.aspx',true)">Tipo canales</a></li>
                                </ul>
                            </li>
                            <li><a href="#" onclick="addTab('Fincas','Padministracion/Finca.aspx',true)">Fincas</a></li>
                            <li><a href="#" onclick="addTab('Secciones','Padministracion/Seccion.aspx',true)">Secciones / Bloques</a></li>
                            <li class="" style="text-align: left"><a class="" href="#">Lotes</a>
                                <ul class="subsubmenu">
                                    <li><a href="#" onclick="addTab('Lotes','Padministracion/Lotes.aspx',true)">Registro...</a></li>
                                    <li><a href="#" onclick="addTab('Peso Periodo','Padministracion/PesoPromedioPeriodo.aspx',true)">Peso RFF por lote </a></li>
                                </ul>
                            </li>

                            <li class="" style="text-align: left"><a class="" href="#">Labores</a>
                                <ul class="subsubmenu">
                                    <li><a href="#" onclick="addTab('GrupoLabor','Padministracion/GrupoLabor.aspx',true)">Grupos...</a></li>
                                    <li><a href="#" onclick="addTab('Labores','Padministracion/Labor.aspx',true)">Registro...</a></li>

                                </ul>
                            </li>

                            <li class="" style="text-align: left"><a class="" href="#">Lista precios</a>
                                <ul class="subsubmenu">
                                    <li><a href="#" onclick="addTab('Lista Pre','Padministracion/ListaPrecios.aspx',true)">Precios x labor</a></li>
                                    <li><a href="#" onclick="addTab('Lista Pre Lot','Padministracion/ListaPreciosLote.aspx',true)">Precios labor por lote</a></li>
                                    <li><a href="#" onclick="addTab('Liquidar Precios','Padministracion/LiquidarPrecios.aspx',true)">Reliquidar precios</a></li>
                                </ul>
                            </li>
                        </ul>
                    </li>

                    <li class="" style="text-align: left"><a class="" href="#">Transacciones</a>
                        <ul class="submenu">
                            <li><a href="#" onclick="addTab('Liquidar jornales','Padministracion/LiquidaJornales.aspx',true)">Reliquidar Jornales</a></li>
                            <li><a href="#" onclick="addTab('RegistroLabores','Ptransaccion/TransaccionesLabores.aspx', true)">Registro de labores</a> </li>
                        </ul>
                    </li>
                    <li class="" style="text-align: left"><a href="#" onclick="addTab('Visualización','Pinformes/Visualizacion.aspx',true)">Informes</a>
                    </li>
                </ul>
            </div>
            <div class="col-1" style="text-align: right; color: #fff; font-size: 12px;">
                <div class="pull-right" style="display: inline-block; position: relative">
                    <ul class=" navigation">
                        <li>
                            <div style="padding-left: 20px; padding-top: 10px; padding-right: 20px;">
                                <i class="fa fa-cog fa-2x" style="background-position: left center; background-repeat: no-repeat"></i>Opciones
                            </div>
                            <ul class="dropdown-menu">
                                <li><a href="#" class="button" data-type="zoomin">Cambiar Contraseña</a></li>
                                <li><a href="#" class="buttonr" data-type="zoominr">Cambiar Empresa</a></li>
                                <li>
                                    <asp:LinkButton ID="hpMenu" runat="server" OnClick="hpMenu_Click">Cerrar Sesión</asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div id="tab">
            <div id="tt" class="easyui-tabs" style="min-width: 900px; min-height: 800px">
            </div>
        </div>


        <div id="footer" style="text-align: right;">
            <table class="w-100">
                <tr>
                    <td style="width: 10px"></td>
                    <td style="text-align: left">
                        <asp:Image ID="Image2" Width="40px" runat="server" ImageUrl="http://app.infos.com/recursosinfos/Imagen/Logos/usuarioBlanco.svg" />
                        <asp:Label ID="lbUsuario" runat="server"></asp:Label>
                        <asp:Label ID="lbNombreUsuario" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Image ID="Image1" Width="50px" ForeColor="#fff" runat="server" ImageUrl="http://app.infos.com/recursosinfos/Imagen/Logos/empresaBlanco.svg" />
                        <asp:Label ID="lbEmpresa" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px; font-size: 15px;">Fecha:
                        <asp:Label ID="lbFecha" runat="server"></asp:Label>
                    </td>
                    <td style="width: 1px; background-color: #1A95A4;"></td>
                    <td>
                        <h5 runat="server" id="nombreModulo"></h5>
                    </td>
                    <td rowspan="2" style="width: 70px"><a id="iconoModulo" style="width: 10px" runat="server"></a></td>
                </tr>
                <tr>
                    <td style="width: 10px"><a class="fa fa-user"></a></td>
                    <td style="text-align: left"></td>
                    <td></td>
                    <td></td>
                    <td style="width: 1px; background-color: #1A95A4"></td>
                    <td></td>
                </tr>
            </table>
        </div>



        <div class="overlay-container" style="z-index: 1">
            <div class="window-container zoomin">
                <h6 style="font-weight: bold">Cambio contraseña</h6>
                <hr />
                <table>
                    <tr>
                        <td style="text-align: left"></td>
                        <td style="vertical-align: top; background-color: transparent; text-align: left">
                            <asp:HiddenField ID="Clave" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px; text-align: left;">
                            <asp:Label ID="lblContrasenaAnterior" runat="server" ForeColor="#003366" Text="Contraseña Anterior" Font-Bold="False" Font-Names="Trebuchet MS" Font-Size="12px"></asp:Label></td>
                        <td style="vertical-align: top; width: 250px; background-color: transparent; text-align: left;">
                            <asp:TextBox ID="txtContrasenaAnterior" runat="server" CssClass="input"
                                TextMode="Password" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblNueva" runat="server" ForeColor="#003366" Text="Nueva Contraseña" Font-Bold="False" Font-Names="Trebuchet MS" Font-Size="12px"></asp:Label></td>
                        <td style="vertical-align: top; background-color: transparent; text-align: left">
                            <asp:TextBox ID="txtContrasenaNueva" runat="server" CssClass="input"
                                TextMode="Password" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="Label1" runat="server" ForeColor="#003366" Text="Confirmar Contraseña" Font-Bold="False" Font-Names="Trebuchet MS" Font-Size="12px"></asp:Label></td>
                        <td style="vertical-align: top; background-color: transparent; text-align: left">
                            <asp:TextBox ID="txtContrasenaNueva1" runat="server" CssClass="input"
                                TextMode="Password" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 5px;" colspan="2"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" colspan="2">
                            <asp:Button ID="btnCambiarClave" runat="server" CssClass="botones" OnClick="btnIniciarSesion_Click" Text="Cambiar contraseña" ToolTip="Haga clic aqui para cambiar contraseña" />
                            <asp:Button ID="btnCancelar" runat="server" CssClass="botones" ToolTip="Haga clic aqui para cancelar" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="overlay-containerr">
            <div class="window-containerr zoominr">
                <table>
                    <tr>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 500px;">
                            <h6 style="font-weight: bold">Listado de empresas</h6>
                            <hr />
                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" Width="550px">
                                <AlternatingRowStyle CssClass="alt" />
                                <Columns>
                                    <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                        <ControlStyle CssClass="btn btn-default btn-sm btn-primary fa fa-check"></ControlStyle>
                                        <ItemStyle Width="20px" CssClass="action-item" />
                                        <HeaderStyle CssClass="action-item" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="id" HeaderText="Id">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nit" HeaderText="Nit">
                                        <HeaderStyle />
                                        <ItemStyle Height="22px" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="razonSocial" HeaderText="Nombre Empresa">
                                        <HeaderStyle />
                                        <ItemStyle Height="22px" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="thead" />
                                <PagerStyle CssClass="footer" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 10px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">

                            <asp:Button ID="Button1" runat="server" CssClass="botones" ToolTip="Haga clic aqui para cancelar" Text="Cancelar" />

                        </td>
                    </tr>
                </table>
            </div>
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
</body>
</html>
