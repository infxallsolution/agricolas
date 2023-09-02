<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuInfos.aspx.cs" Inherits="Infos.WebForms.Formas.MenuInfos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Menu InfoS</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
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
    </script>
    <script type="text/javascript">



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
    </script>
    <script type="">
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
    </script>
    <script type="">
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
    </script>
</head>
<body style="margin: 0; padding: 0; width: 100%;">
    <form id="form1" runat="server" style="width: 100%;">
        <div class="container-fluid">
            <div class="row ">
                <div class="primeraLinea" >
                    <span>Sistema de Información INFOS - © 2015 Todos los derechos reservados - Santa Marta, Colombia</span>
                    <div style="float: right; display: inline-block; z-index:1">
                        <ul class="navigation">
                            <li>
                                <div style="padding-left: 20px">
                                    <i class="fa fa-cog fa-2x" style="background-position: left center; background-repeat: no-repeat"></i>
                                    Opciones
                                </div>
                                <ul>
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
            <div class="row bannerLogo">
                <div class="col-8 pt-3 ">
                    <asp:ImageButton ID="imbPrincipal" runat="server" ImageUrl="http://app.infos.com/recursosinfos/img/Logos/logo.svg" ToolTip="Volver al menu principal" />
                </div>
                <div class="col-4">
                    <table class="pull-right">
                        <tr>
                            <td rowspan="2"></td>
                            <td style="width: 50px; text-align: center;">
                                <a class="fa fa-user fa-2x"></a>
                            </td>
                            <td>
                                <asp:Label ID="lbUsuario" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lbNombreUsuario" runat="server"></asp:Label>
                            </td>
                            <td style="width: 220px; text-align: center;" rowspan="2">
                                <div>
                                </div>
                                <div>
                                </div>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 50px; text-align: center;">
                                <a class="fa fa-building fa-2x"></a></td>
                            <td>
                                <asp:Label ID="lbEmpresa" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lbFecha" runat="server"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div class="col-2">
                </div>
            </div>
            <div class="row ">
                <div class="primeraLinea">
                    <span></span>
                </div>
            </div>
        </div>
        <div class="overlay-container" style="z-index:1">
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
        <div style="width: 100%;">
            <div style="padding-top: 10px; text-align: center;">
                <div style="display: inline-block ">
                    <asp:DataList ID="dlMenu" runat="server" RepeatColumns="4" CellSpacing="10">
                        <ItemTemplate>
                            <div class="row ">
                                <div style="padding: 18px; width: 250px" >
                                    <div class="ih-item square colored effect6 top_to_bottom "
                                        style="width: 200px; padding: 10px; height: 180px; text-align: center; background-color: #0D1D88" >
                                        <a  class="imagen" onclick="javascript:window.location='<%#Eval("dirUrl")+"?u="+this.Session["usuario"].ToString()+
                                                                            "&p="+this.Session["pass"].ToString()+"&e="+this.Session["empresa"]%>'">
                                            <div class="img ">
                                                <i class='<%#Eval("imagen")%>' style="color: white; font-size: 125px"></i>
                                            </div>
                                            <div class="tit">
                                                <h6 style="color: white; vertical-align: bottom;"><%#Eval("descripcion")%></h6>
                                            </div>
                                            <div class="info" style="vertical-align: middle">
                                                <h3 style="color: white; vertical-align: bottom;"><%#Eval("descripcion")%></h3>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>

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
</body>
</html>