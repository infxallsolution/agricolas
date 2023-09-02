<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanoContabilizacion.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Contabilizacion.PlanoContabilizacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="../../css/Formularios.css" rel="stylesheet" />
    <script src="../../js/Numero.js" type="text/javascript"></script>
    <link href="../../css/chosen.css" rel="stylesheet" />
    <script type="text/javascript">
        function Visualizacion(empresa, año, documento) {

            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + 0 + ", height =" + 0 + ", top = 0, left = 5";
            sTransaccion = "GenerarPlanoContable.aspx?empresa=" + empresa + "&documento=" + documento + "&año=" + año;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
    </script>
</head>

<body style="text-align: center">
    <form id="form1" runat="server">
        <div class="principal">
            <table cellspacing="0" style="width: 1000px">
                <tr>
                    <td style="background-image: url(../../Imagenes/botones/BotonIzq.png); width: 250px; background-repeat: no-repeat; height: 25px; text-align: left;"></td>
                    <td style="width: 100px; height: 25px; text-align: left">Busqueda</td>
                    <td style="width: 350px; height: 25px; text-align: left">
                        <asp:TextBox ID="nitxtBusqueda" runat="server" Width="350px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td style="background-image: url(../../Imagenes/botones/BotonDer.png); width: 250px; background-repeat: no-repeat; height: 25px; text-align: right; background-position-x: right;"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" />
                        <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'" style="height: 21px" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" ToolTip="Cancela la operación" OnClick="lbCancelar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" Visible="False" />

                        <asp:ImageButton ID="lbRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" ToolTip="Guarda el nuevo registro en la base de datos"
                            onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" OnClick="lbRegistrar_Click" Visible="False" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                    </td>
                </tr>
            </table>
            <table cellspacing="0" style="width: 1000px; border-top: silver thin solid; border-bottom: silver thin solid;" id="TABLE1">
                <tr>
                    <td style="height: 15px;" colspan="4">
                        <asp:Label ID="nilblInformacion" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos" width="100px">
                        <asp:Label ID="Label24" runat="server" Text="Año" Visible="False"></asp:Label></td>
                    <td class="Campos">
                        <asp:DropDownList ID="ddlAño" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1" Visible="False" Width="130px">
                        </asp:DropDownList>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos" width="100px">
                        <asp:Label ID="Label6" runat="server" Text="Tipo" Visible="False"></asp:Label></td>
                    <td class="Campos">
                        <asp:DropDownList ID="ddlTipo" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                            <asp:ListItem Value="CA">Causación</asp:ListItem>
                            <asp:ListItem Value="PA">Pago</asp:ListItem>
                            <asp:ListItem Value="PR">Provisión Prestaciones</asp:ListItem>
                            <asp:ListItem Value="SS">Seguridad Social</asp:ListItem>
                            <asp:ListItem Value="CC">Causación Contratistas</asp:ListItem>
                            <asp:ListItem Value="PS">Liquidación de prestaciones</asp:ListItem>
                            <asp:ListItem Value="IN">Incapacidades</asp:ListItem>
                              <asp:ListItem Value="SSC">Seguridad Social Contratista</asp:ListItem>
                            <asp:ListItem Value="PRC">Provisiones Contrartista</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos" width="100px">
                        <asp:Label ID="Label1" runat="server" Text="# Comprobante" Visible="False"></asp:Label></td>
                    <td class="Campos">
                        <asp:TextBox ID="txtNoComprobante" runat="server" Visible="False" Width="200px" AutoPostBack="True" CssClass="input"></asp:TextBox>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos" width="100px">
                        <asp:LinkButton ID="lbFechaIni" runat="server" ForeColor="#003366" OnClick="lbFechaIni_Click" Visible="False">Fecha</asp:LinkButton>
                    </td>
                    <td class="Campos">
                        <asp:Calendar ID="CalendarFechaIni" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFechaIni_SelectionChanged" Visible="False" Width="150px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="FloralWhite" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                        <asp:TextBox ID="txtFechaIni" runat="server" class="input" Font-Bold="True" ReadOnly="True" ToolTip="Formato fecha (dd/mm/yyyy)" Visible="False" Width="150px" AutoPostBack="True" OnTextChanged="txtFechaIni_TextChanged"></asp:TextBox>
                        <asp:Label ID="Label20" runat="server" Text="(dd/MM/aaaa)" Visible="False"></asp:Label>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos" width="100px">
                        <asp:Label ID="lblDocumentoNomina" runat="server" Text="Doc. Contable" Visible="False"></asp:Label></td>
                    <td class="Campos">
                        <asp:DropDownList ID="ddlNumero" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="350px">
                        </asp:DropDownList>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos" width="100px">
                        <asp:Label ID="Label21" runat="server" Text="Consecutivo Cruce" Visible="False"></asp:Label></td>
                    <td class="Campos">
                        <asp:TextBox ID="txtRegistroCruce" runat="server" Visible="False" Width="200px"  CssClass="input">260</asp:TextBox>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes"></td>
                    <td class="nombreCampos" width="100px">
                        <asp:Label ID="Label22" runat="server" Text="Nota" Visible="False"></asp:Label></td>
                    <td class="Campos">
                        <asp:TextBox ID="txtNota" runat="server" Visible="False" Width="300px"  CssClass="input" Height="72px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td style="height: 15px;" colspan="4">
                        <asp:Label ID="nilblMensaje" runat="server"></asp:Label></td>
                </tr>
            </table>
            <div class="tablaGrilla">
                <div style="display: inline-block">
                </div>
            </div>

        </div>
        <script src="../../js/jquery.min.js" type="text/javascript"></script>
        <script src="../../js/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

    </form>
</body>
</html>
