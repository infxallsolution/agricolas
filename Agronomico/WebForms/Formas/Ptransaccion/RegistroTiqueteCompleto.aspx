<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroTiqueteCompleto.aspx.cs" Inherits="Agronomico.WebForms.Formas.Ptransaccion.RegistroTiqueteCompleto" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transacciones Agro</title>
    <link href="http://app.infos.com/recursosinfos/css/Formularios.css" rel="stylesheet" />
    <link href="http://app.infos.com/recursosinfos/css/chosen.css" rel="stylesheet" />
    <script src="../../js/CalendarioMin.js" type="text/javascript"></script>
    <script src="../../js/CalendarioUiMin.js" type="text/javascript"></script>
    <link href="../../css/Calendarios.css" rel="stylesheet" />
    <script src="../../js/Numero.js" type="text/javascript"></script>
    <script src="../../js/OcultarMostrar.js"></script>
    <link rel="stylesheet" href="../../css/common.css" type="text/css" />
    <link href="http://app.infos.com/recursosinfos/css/jquery-ui.css" rel="stylesheet" />
    <link type="text/css" href="http://app.infos.com/recursosinfos/css/ui.multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/jqueryv1.5.1.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-ui1.8.10.min.js"></script>
    <script type="text/javascript" src="../../js/plugins/localisation/jquery.localisation-min.js"></script>
    <script type="text/javascript" src="../../js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
    <script type="text/javascript" src="../../js/ui.multiselect.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; color: #003366; font-size: 12px; width: 100%">
            <div style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver; padding: 5px; vertical-align: text-bottom; height: 21px; width: 100%;">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0" ScriptMode="Release"
                    EnablePartialRendering="true" EnablePageMethods="true">
                </asp:ScriptManager>
                <script type="text/javascript">
                    var xPos, yPos;
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
                    function endReq(sender, args) {
                        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                        $.localise('ui-multiselect', { language: 'es', path: '../../js/locale/' });
                        $(".multiselect").multiselect();
                        $(function () {
                            $("#tabs").tabs();
                        });
                        $(function () {
                            var tabName = $("#TabName").val();

                            if (tabName != null & tabName != "") {
                                $("#tabs").tabs({ selected: tabName });
                                //$('#tabs a[href="#' + tabName + '"]').tabs( "option", "active", { effect: "blind", duration: 800 } );
                                //$('#tabs').tabs("refresh");

                            }
                        });

                        function confirmSwal($this, title, detalle) {
                            swal({
                                title: title,
                                html: detalle,
                                type: "warning",
                                showCancelButton: true,
                                confirmButtonText: "Aceptar",
                                cancelButtonText: "Cancelar"
                            }).then(function () {
                                $this.onclick = '';
                                $this.click();
                            });
                            return false;
                        }

                        $("#tabs a").click(function () {

                            if ($(this).attr("href").replace("#", "").substring(0, 4) == "tabs") {
                                $("#TabName").val($(this).attr("href").replace("#", ""));
                                //alert($("#TabName").val());
                            }
                        });
                    }




                </script>
                <asp:ImageButton ID="niimbRegistro" runat="server" ImageUrl="~/Imagen/Bonotes/pRegistro.png"
                    onmouseout="this.src='../../Imagen/Bonotes/pRegistro.png'"
                    onmouseover="this.src='../../Imagen/Bonotes/pRegistroN.png'" OnClick="niimbRegistro_Click" />
                <asp:ImageButton ID="imbConsulta" runat="server" ImageUrl="~/Imagen/Bonotes/pConsulta.png"
                    onmouseout="this.src='../../Imagen/Bonotes/pConsulta.png'"
                    onmouseover="this.src='../../Imagen/Bonotes/pConsultaN.png'" OnClick="imbConsulta_Click" />
                <asp:HiddenField ID="TabName" runat="server" />
            </div>
            <asp:UpdatePanel ID="upGeneral" runat="server">
                <ContentTemplate>
                    <div style="text-align: center;">
                        <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" ToolTip="Cancela la operación" OnClick="lbCancelar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" Visible="False" />
                        <asp:ImageButton ID="lbRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" ToolTip="Guarda el nuevo registro en la base de datos"
                            onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" OnClick="lbRegistrar_Click1" Visible="False" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                        <asp:ImageButton ID="niimbImprimir" runat="server" ImageUrl="~/Imagen/Bonotes/btnImprimir.png" ToolTip="Haga clic aqui para realizar la busqueda"
                            onmouseout="this.src='../../Imagen/Bonotes/btnImprimir.png'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnImprimirN.png'" Visible="False" OnClick="niimbImprimir_Click" />
                    </div>
                    <div style="text-align: center">
                        <asp:Label ID="nilblInformacion" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="text-align: center; padding: 5px;">
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="upBascula" runat="server" Visible="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="cuadroPrincipal">
                                        <div class="cuadroPrincipalTitulo">Detalle de Bascula</div>
                                        <div class="cuadroTexto">
                                            <table cellpadding="0" cellspacing="0" style="width: 950px">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:RadioButtonList ID="rblBascula" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblBascula_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="1">Tiquete del grupo</asp:ListItem>
                                                            <asp:ListItem Value="2">Tiquete Externo</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:HiddenField ID="hfTipo" runat="server" />
                                                        <asp:HiddenField ID="hfNumero" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                            <tr>
                                                                <td style="width: 90px; text-align: left">
                                                                    <asp:Label ID="lblExtractoraFiltro" runat="server" Text="Extractora" Visible="False"></asp:Label>
                                                                </td>
                                                                <td style="width: 350px; text-align: left;">
                                                                    <asp:DropDownList ID="ddlExtractoraFiltro" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlExtractoraFiltro_SelectedIndexChanged" Visible="False" Width="320px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 120px; text-align: left">
                                                                    <asp:Label ID="lblFiltroBusqueda" runat="server" Text="Filtro tiquete" Visible="False"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 180px;">
                                                                    <asp:TextBox ID="txtFiltroBascula" runat="server" CssClass="input" Visible="False" Width="150px" ToolTip="Ingrese numero tiquete" AutoPostBack="True" OnTextChanged="txtFiltroBascula_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:ImageButton ID="imbBuscarBascula" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" OnClick="imbBuscarBascula_Click" onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" Visible="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" style="height: 5px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;">
                                                                    <asp:GridView ID="gvTiquetes" runat="server" AutoGenerateColumns="False" CssClass="Grid" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="5" Visible="False" Width="100%" AllowPaging="True" OnSelectedIndexChanged="gvTiquetes_SelectedIndexChanged">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imbSelecionaTiquete" runat="server" ImageUrl="~/Imagen/Iconos/ok.png" CommandName="select" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Items" HorizontalAlign="Center" Width="20px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="extractora" HeaderText="Extract.">
                                                                                <ItemStyle CssClass="Items" Width="10px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="tiquete" HeaderText="Tiquete" ReadOnly="True">
                                                                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="120px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                                                <ItemStyle CssClass="Items" Width="100px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="vehiculo" HeaderText="Vehí.">
                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="50px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="remolque" HeaderText="Remol.">
                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="30px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="pesoBruto" HeaderText="pBruto(kg)" DataFormatString="{0:N}">
                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="pesoTara" HeaderText="pTara(kg)" DataFormatString="{0:N}">
                                                                                <ItemStyle CssClass="Items" Width="60px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="pesoNeto" HeaderText="pNeto(kg)" DataFormatString="{0:N}">
                                                                                <ItemStyle CssClass="Items" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="sacos" HeaderText="Sacos">
                                                                                <ItemStyle CssClass="Items" Width="20px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="racimos" HeaderText="Racimos">
                                                                                <ItemStyle CssClass="Items" Width="40px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="idFinca" HeaderText="Finca">
                                                                                <ItemStyle CssClass="Items" Width="5px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="nombreFinca" HeaderText="NombreFinca">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle CssClass="Items" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr" />
                                                                        <RowStyle CssClass="rw" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" style="height: 3px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:UpdatePanel ID="upTiquete" runat="server" Visible="False">
                                                                        <ContentTemplate>
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td style="width: 100px; text-align: left;">
                                                                                        <asp:Label ID="lbTiquete1" runat="server" Text="Extractora"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:DropDownList ID="ddlExtractoraTiquete" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="350px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 80px; text-align: left">
                                                                                        <asp:Label ID="lbTiquete" runat="server" Text="Tiquete"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txtTiquete" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100px; text-align: left;">
                                                                                        <asp:LinkButton ID="lbFechaTiqueteI" runat="server" ForeColor="#003366" OnClick="lbFecha_Click" Visible="False">Fecha Tiquete</asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:Calendar ID="niCalendarFechaTiqueteI" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFechaTiqueteI_SelectionChanged1" Visible="False" Width="150px">
                                                                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                            <SelectorStyle BackColor="#CCCCCC" />
                                                                                            <WeekendDayStyle BackColor="FloralWhite" />
                                                                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                            <OtherMonthDayStyle ForeColor="Gray" />
                                                                                            <NextPrevStyle VerticalAlign="Bottom" />
                                                                                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                                        </asp:Calendar>
                                                                                        <asp:TextBox ID="txtFechaTiqueteI" runat="server" class="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Visible="False" Width="150px" ReadOnly="True"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 80px; text-align: left">
                                                                                        <asp:Label ID="Label3" runat="server" Text="Sacos"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txvSacos" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="150px">0</asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100px; text-align: left;">
                                                                                        <asp:Label ID="Label2" runat="server" Text="Peso Bruto"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txvPbruto" runat="server" CssClass="input" onkeyup="formato_numero(this); obtener_neto();" Visible="False" Width="150px" AutoPostBack="True" OnTextChanged="txvPbruto_TextChanged">0</asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 80px; text-align: left">
                                                                                        <asp:Label ID="Label6" runat="server" Text="Racimos"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txvRacimosTiquete" runat="server" CssClass="input" onkeyup="formato_numero(this); igualarRacimos();" Visible="False" Width="150px">0</asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100px; text-align: left;">
                                                                                        <asp:Label ID="Label4" runat="server" Text="Peso Tara"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txvPtara" runat="server" CssClass="input" onkeyup="formato_numero(this); obtener_neto(); " Visible="False" Width="150px" AutoPostBack="True" OnTextChanged="txvPtara_TextChanged">0</asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 80px; text-align: left">
                                                                                        <asp:Label ID="Label14" runat="server" Text="Vehículo"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txtVehiculo" runat="server" CssClass="input" Width="100px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100px; text-align: left;">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Peso Neto"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txvPneto" runat="server" CssClass="input" onkeyup="formato_numero(this)" Visible="False" Width="150px">0</asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 80px; text-align: left">
                                                                                        <asp:Label ID="Label15" runat="server" Text="Remolque"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:TextBox ID="txtRemolque" runat="server" CssClass="input" Width="100px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100px; text-align: left;">
                                                                                        <asp:Label ID="lblFinca" runat="server" Text="Finca" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 350px; text-align: left">
                                                                                        <asp:DropDownList ID="ddlFinca" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlFinca_SelectedIndexChanged2" Visible="False" Width="350px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 80px; text-align: left">&nbsp;</td>
                                                                                    <td style="width: 350px; text-align: left">&nbsp;</td>
                                                                                </tr>
                                                                            </table>

                                                                            <div class="cuadroPrincipal">
                                                                                <div class="cuadroPrincipalTitulo">Datos Transacción</div>
                                                                                <div class="cuadroTexto">
                                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                        <tr>
                                                                                            <td style="width: 120px; text-align: left;">
                                                                                                <asp:LinkButton ID="lbFecha" runat="server" ForeColor="#003366" OnClick="lbFecha_Click2" Visible="False">Fecha Transacción</asp:LinkButton>
                                                                                            </td>
                                                                                            <td style="text-align: left; width: 350px;">
                                                                                                <asp:Calendar ID="niCalendarFecha" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFecha_SelectionChanged" Visible="False" Width="150px">
                                                                                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                                    <SelectorStyle BackColor="#CCCCCC" />
                                                                                                    <WeekendDayStyle BackColor="FloralWhite" />
                                                                                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                                    <OtherMonthDayStyle ForeColor="Gray" />
                                                                                                    <NextPrevStyle VerticalAlign="Bottom" />
                                                                                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                                                </asp:Calendar>
                                                                                                <asp:TextBox ID="txtFecha" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" OnTextChanged="txtFecha_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Visible="False" Width="150px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 120px; text-align: left">
                                                                                                <asp:Label ID="lblRemision" runat="server" Text="Remisión" Visible="False"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 350px; text-align: left">
                                                                                                <asp:TextBox ID="txtRemision" runat="server" CssClass="input" Width="150px" Wrap="False"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 120px; text-align: left;">
                                                                                                <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" Visible="False"></asp:Label>
                                                                                            </td>
                                                                                            <td colspan="3" style="text-align: left">
                                                                                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" Height="40px" TextMode="MultiLine" Visible="False" Width="800px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSeccion" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="imbCargar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="txtFiltroBascula" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>

                    <div style="text-align: center">
                        <asp:UpdatePanel ID="upPestana" runat="server" Visible="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="tabs" runat="server" visible="false" style="width: 1150px; display: inline-block;">
                                    <ul>
                                        <li><a href="#tabs-3">Cosecha</a></li>
                                        <li><a href="#tabs-1">Transporte</a></li>
                                        <li><a href="#tabs-2">Descargue</a></li>
                                    </ul>
                                    <div id="tabs-3">
                                        <div style="text-align: center;">
                                            <div style="display: inline-block">
                                                <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional" Visible="False">
                                                    <ContentTemplate>
                                                        <div class="cuadroPrincipal">
                                                            <div class="cuadroPrincipalTitulo">Detalle de Cosecha</div>
                                                            <div class="cuadroTexto">
                                                                <table cellspacing="0" style="">
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:Label ID="nilblInformacionDetalle" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 80px; text-align: left;">
                                                                            <asp:Label ID="lblSeccion" runat="server" Text="Sección"></asp:Label>
                                                                        </td>
                                                                        <td class="Campos">
                                                                            <asp:DropDownList ID="ddlSeccion" runat="server" AutoPostBack="True" CssClass="chzn-select" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlSeccion_SelectedIndexChanged" Width="350px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="nombreCampos" style="width: 110px; text-align: left;">
                                                                            <asp:Label ID="lblLote" runat="server" Text="Lote"></asp:Label>
                                                                        </td>
                                                                        <td class="Campos">
                                                                            <asp:DropDownList ID="ddlLote" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="350px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; width: 100px;">
                                                                            <asp:LinkButton ID="lbFechaD" runat="server" ForeColor="#003366" OnClick="lbFechaD_Click" Visible="False">Fecha cosecha</asp:LinkButton>
                                                                        </td>
                                                                        <td class="Campos" style="vertical-align: middle; line-height: normal">
                                                                            <asp:Calendar ID="calendarFechaCosecha" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFechaD_SelectionChanged" Visible="False" Width="150px">
                                                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                <SelectorStyle BackColor="#CCCCCC" />
                                                                                <WeekendDayStyle BackColor="FloralWhite" />
                                                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                <OtherMonthDayStyle ForeColor="Gray" />
                                                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                            </asp:Calendar>
                                                                            <asp:TextBox ID="txtFechaCosecha" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" OnTextChanged="txtFechaD_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: left;" colspan="2">
                                                                            <table cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td style="padding-right: 4px; padding-left: 4px; width: 100px">
                                                                                        <asp:Label ID="lblRacimosD" runat="server" Text="Racimos"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 140px; text-align: left;">
                                                                                        <asp:TextBox ID="txvRacimosCosecha" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="110px">0</asp:TextBox>
                                                                                    </td>
                                                                                    <td style="padding-right: 4px; padding-left: 4px; width: 100px;">
                                                                                        <asp:Label ID="lblJornalesD" runat="server" Text="Jornales"></asp:Label>
                                                                                    </td>
                                                                                    <td style="padding-right: 4px; padding-left: 4px">
                                                                                        <asp:TextBox ID="txvJornalesD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="110px">0</asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center;">
                                                                            <div style="display: inline-block">
                                                                                <select id="selTerceroCosecha" runat="server" class="multiselect" multiple="true" name="countries[]0" style="width: 800px; height: 150px;" visible="False">
                                                                                </select>
                                                                            </div>
                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center;">
                                                                            <asp:ImageButton ID="imbCargar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCargar.png" OnClick="imbCargar_Click1" onmouseout="this.src='../../Imagen/Bonotes/btnCargar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnCargarN.png'" Style="height: 21px" />
                                                                            <asp:ImageButton ID="imbLiquidar" runat="server" ImageUrl="~/Imagen/Bonotes/btnLiquidar.png" OnClick="imbLiquidar_Click" onmouseout="this.src='../../Imagen/Bonotes/btnLiquidar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnLiquidarN.png'" />
                                                                            <asp:ImageButton ID="lbCancelarD" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" OnClick="lbCancelarD_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" ToolTip="Cancela la operación" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center;">
                                                                            <asp:GridView ID="gvSubTotales" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="800px">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="novedades" HeaderText="Novedad">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="subCantidad" HeaderText="SubCantidad">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="subRacimo" HeaderText="SubRacimos">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SubJornal" HeaderText="SubJornales">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="nombreNovedades" HeaderText="nombreNovedades">
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="9pt" />
                                                                                <RowStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="8pt" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center;">
                                                                            <asp:DataList ID="dlDetalleCosecha" runat="server" RepeatColumns="2" Style="margin-right: 0px" Width="100%" OnDeleteCommand="dlDetalle_DeleteCommand" RepeatDirection="Horizontal">
                                                                                <ItemTemplate>
                                                                                    <div style="padding: 2px; border: solid; border-color: silver; border-width: 1px; width: 535px;">
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 60px;">
                                                                                                        <asp:Label ID="Label17" runat="server" Text="Sección" CssClass="auto-style3" Style="font-weight: bold"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 50px; text-align: left">
                                                                                                        <asp:Label ID="lblSeccion" runat="server" Text='<%# Eval("codseccion") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesSeccion" runat="server" Text='<%# Eval("desseccion") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left; width: 50px;">
                                                                                                        <asp:Label ID="lblpRacimos" runat="server" Style="color: #FFFFFF"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="Label18" runat="server" Text="Lote" CssClass="auto-style3" Style="font-weight: bold"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 50px; text-align: left">
                                                                                                        <asp:Label ID="lblLote" runat="server" Text='<%# Eval("codlote") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesLote" runat="server" Text='<%# Eval("deslote") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblPesoPromedio" runat="server" Text='<%# Eval("PesoRacimo") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <div style="padding-top: 2px">
                                                                                                            <table cellpadding="0" cellspacing="0" class="ui-accordion">
                                                                                                                <tr>
                                                                                                                    <td style="text-align: left; width: 90px;">
                                                                                                                        <asp:Label ID="lblCantidadD0" runat="server" CssClass="ui-priority-primary" Text="Cantidad"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvCantidadG" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="70px" Text='<%# Eval("cantidad") %>'></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left; width: 90px;">
                                                                                                                        <asp:Label ID="lblRacimosN" runat="server" CssClass="ui-priority-primary" Text="No. racimos"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvRacimoG" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("racimos") %>' Width="70px" AutoPostBack="True" OnTextChanged="txvRacimoG_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:Label ID="lblRacimosN0" runat="server" CssClass="ui-priority-primary" Text="Jornales"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvJornalesD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("jornal") %>' Width="70px" AutoPostBack="True" OnTextChanged="txvJornalesD_TextChanged"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td colspan="4" style="background-color: #0C1D87; color: #FFFFFF"><strong>Detalle de Cosecha</strong></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 70px; text-align: left;">
                                                                                                        <asp:Label ID="Label16" runat="server" Text="Novedad" CssClass="auto-style3" Style="font-weight: bold"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 20px; text-align: left">
                                                                                                        <asp:Label ID="lblNovedadCosecha" runat="server" Text='<%# Eval("codnovedad") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesNovedadCosecha" runat="server" Text='<%# Eval("desnovedad") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblUmedida" runat="server" CssClass="auto-style4" Text='<%# Eval("umedida") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 70px; text-align: left;">
                                                                                                        <asp:Label ID="Label19" runat="server" Text="Fecha" CssClass="auto-style3" Style="font-weight: bold"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 80px; text-align: left">
                                                                                                        <asp:Label ID="lblFechaDCosecha" runat="server" Text='<%# Eval("fechaD") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="Label20" runat="server" Text="Precio Labor $" CssClass="auto-style3" Style="font-weight: bold"></asp:Label>
                                                                                                        <asp:Label ID="lblPrecioLaborCosecha" runat="server" Text='<%# Eval("precioLabor") %>' CssClass="auto-style6"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDifKilosCosecha" runat="server" CssClass="auto-style4"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="padding-top: 3px; padding-bottom: 3px">
                                                                                                <asp:GridView ID="gvLotes" runat="server" AutoGenerateColumns="False" CssClass="Grid" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="5" Width="100%">
                                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="codTercero" HeaderText="Cod" ReadOnly="True">
                                                                                                            <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="5px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="desTercero" HeaderText="NombreTrabajador">
                                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Cantidad">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="Items" Width="70px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Jornal">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtJornal" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("jornal") %>' Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="Items" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="precioLabor" HeaderText="($)">
                                                                                                            <ItemStyle Width="5px" />
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                    <PagerStyle CssClass="pgr" />
                                                                                                    <RowStyle CssClass="rw" />
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>

                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" class="ui-accordion">
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 250px;">
                                                                                                        <asp:Label ID="Label35" runat="server" CssClass="ui-priority-primary" Text="Registro por novedad No."></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px">
                                                                                                        <asp:Label ID="lblRegistro" runat="server" Text='<%# Eval("registro") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: right">
                                                                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagen/Bonotes/btnEliminar.png" onmouseout="this.src='../../Imagen/Bonotes/btnEliminar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnEliminarN.png'" CommandName="Delete" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <SeparatorStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:DataList>

                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="tabs-1">
                                        <div style="text-align: center;">
                                            <div style="display: inline-block">
                                                <asp:UpdatePanel ID="upTransporte" runat="server" UpdateMode="Conditional" Visible="False">
                                                    <ContentTemplate>
                                                        <div class="cuadroPrincipal">
                                                            <div class="cuadroPrincipalTitulo">Detalle de Tranporte</div>
                                                            <div class="cuadroTexto">
                                                                <table cellspacing="0" style="">
                                                                    <tr>
                                                                        <td style="text-align: center;" colspan="6">
                                                                            <asp:Label ID="nilblinformacionTranportador" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; width: 110px;">
                                                                            <asp:Label ID="lblLaborCargue0" runat="server" Text="Labor Transporte"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; width: 360px;">
                                                                            <asp:DropDownList ID="ddlLaborTransporte" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="350px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: left; width: 120px;">
                                                                            <asp:LinkButton ID="lbFechaTransporte" runat="server" ForeColor="#003366" OnClick="lbFechaTransporte_Click" Visible="False">Fecha Transporte</asp:LinkButton>
                                                                        </td>
                                                                        <td style="text-align: left; width: 110px;">
                                                                            <asp:Calendar ID="calendarTransporte" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="calendarTransporte_SelectionChanged" Visible="False" Width="150px">
                                                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                <SelectorStyle BackColor="#CCCCCC" />
                                                                                <WeekendDayStyle BackColor="FloralWhite" />
                                                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                <OtherMonthDayStyle ForeColor="Gray" />
                                                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                            </asp:Calendar>
                                                                            <asp:TextBox ID="txtFechaTransporte" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" OnTextChanged="txtFechaTransporte_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 80px; text-align: left">
                                                                            <asp:Label ID="lblRacimosDT" runat="server" Text="Racimos"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 80px; text-align: left">
                                                                            <asp:TextBox ID="txvRacimoTransporte" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="80px">0</asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td colspan="6" style="text-align: center;">
                                                                            <div style="display: inline-block;">
                                                                                <select id="selTerceroTransporte" runat="server" class="multiselect" multiple="true" name="countries[]" visible="False" style="width: 800px; height: 100px;">
                                                                                </select>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" style="text-align: center;">
                                                                            <asp:ImageButton ID="imbCargarT" runat="server" ImageUrl="~/Imagen/Bonotes/btnCargar.png" OnClick="imbCargarT_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCargar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnCargarN.png'" Style="height: 21px" />
                                                                            <asp:ImageButton ID="imbLiquidarT" runat="server" ImageUrl="~/Imagen/Bonotes/btnLiquidar.png" OnClick="imbLiquidarT_Click" onmouseout="this.src='../../Imagen/Bonotes/btnLiquidar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnLiquidarN.png'" />
                                                                            <asp:ImageButton ID="lbCancelarT" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" OnClick="lbCancelarT_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" ToolTip="Cancela la operación" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" style="text-align: center">
                                                                            <div style="text-align: center">
                                                                                <asp:GridView ID="gvSubTotalesT" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="800px">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="novedades" HeaderText="Novedad">
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="subCantidad" HeaderText="SubCantidad">
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="subRacimo" HeaderText="SubRacimos">
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="SubJornal" HeaderText="SubJornales">
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="nombreNovedades" HeaderText="nombreNovedades">
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="9pt" />
                                                                                    <RowStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="8pt" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" style="text-align: center">
                                                                            <asp:DataList ID="dlDetalleTransporte" runat="server" OnDeleteCommand="dlDetalleTransporte_DeleteCommand" RepeatColumns="2" RepeatDirection="Horizontal" Style="margin-right: 0px" Width="100%">
                                                                                <ItemTemplate>
                                                                                    <div style="padding: 2px; border: solid; border-color: silver; border-width: 1px; width: 535px;">
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 60px;">
                                                                                                        <asp:Label ID="Label77" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Sección"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 50px; text-align: left">
                                                                                                        <asp:Label ID="lblSeccion" runat="server" CssClass="auto-style6" Text='<%# Eval("codseccion") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesSeccion" runat="server" CssClass="auto-style6" Text='<%# Eval("desseccion") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left; width: 50px;">
                                                                                                        <asp:Label ID="lblpRacimos" runat="server" Style="color: #FFFFFF"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="Label78" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Lote"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 50px; text-align: left">
                                                                                                        <asp:Label ID="lblLote" runat="server" CssClass="auto-style6" Text='<%# Eval("codlote") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesLote" runat="server" CssClass="auto-style6" Text='<%# Eval("deslote") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblPesoPromedio" runat="server" CssClass="auto-style6" Text='<%# Eval("PesoRacimo") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <div style="padding-top: 2px">
                                                                                                            <table cellpadding="0" cellspacing="0" class="ui-accordion">
                                                                                                                <tr>
                                                                                                                    <td style="text-align: left; width: 90px;">
                                                                                                                        <asp:Label ID="lblCantidadD3" runat="server" CssClass="ui-priority-primary" Text="Cantidad"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvCantidadG" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left; width: 90px;">
                                                                                                                        <asp:Label ID="lblRacimosN5" runat="server" CssClass="ui-priority-primary" Text="No. racimos"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvRacimoG" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" OnTextChanged="txvRacimoG_TextChanged" Text='<%# Eval("racimos") %>' Width="70px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:Label ID="lblRacimosN6" runat="server" CssClass="ui-priority-primary" Text="Jornales"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvJornalesD" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" OnTextChanged="txvJornalesD_TextChanged" Text='<%# Eval("jornal") %>' Width="70px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td colspan="4" style="background-color: #0C1D87; color: #FFFFFF"><strong>Detalle de Transporte</strong></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 70px; text-align: left;">
                                                                                                        <asp:Label ID="Label79" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Novedad"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 20px; text-align: left">
                                                                                                        <asp:Label ID="lblNovedad" runat="server" CssClass="auto-style6" Text='<%# Eval("codnovedad") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesNovedad" runat="server" CssClass="auto-style6" Text='<%# Eval("desnovedad") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblUmedida" runat="server" CssClass="auto-style4" Text='<%# Eval("umedida") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 70px; text-align: left;">
                                                                                                        <asp:Label ID="Label80" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Fecha"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 80px; text-align: left">
                                                                                                        <asp:Label ID="lblFechaD" runat="server" CssClass="auto-style6" Text='<%# Eval("fechaD") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="Label81" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Precio Labor $"></asp:Label>
                                                                                                        <asp:Label ID="lblPrecioLabor" runat="server" CssClass="auto-style6" Text='<%# Eval("precioLabor") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDifKilos" runat="server" CssClass="auto-style4"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="padding-top: 3px; padding-bottom: 3px">
                                                                                                <asp:GridView ID="gvLotes" runat="server" AutoGenerateColumns="False" CssClass="Grid" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="5" Width="100%">
                                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="codTercero" HeaderText="Cod" ReadOnly="True">
                                                                                                            <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="5px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="desTercero" HeaderText="NombreTrabajador">
                                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Cantidad">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="Items" Width="70px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Jornal">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtJornal" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("jornal") %>' Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="Items" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="precioLabor" HeaderText="($)">
                                                                                                            <ItemStyle Width="5px" />
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                    <PagerStyle CssClass="pgr" />
                                                                                                    <RowStyle CssClass="rw" />
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" class="ui-accordion">
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 250px;">
                                                                                                        <asp:Label ID="Label82" runat="server" CssClass="ui-priority-primary" Text="Registro por novedad No."></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblRegistro" runat="server" Text='<%# Eval("registro") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px">
                                                                                                        <asp:Label ID="lblRegistroCosecha" runat="server" Text='<%# Eval("registroNovedad") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: right">
                                                                                                        <asp:ImageButton ID="ImageButton9" runat="server" CommandName="Delete" ImageUrl="~/Imagen/Bonotes/btnEliminar.png" onmouseout="this.src='../../Imagen/Bonotes/btnEliminar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnEliminarN.png'" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <SeparatorStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:DataList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="tabs-2">
                                        <div style="text-align: center;">
                                            <div style="display: inline-block">
                                                <asp:UpdatePanel ID="upRecolector" runat="server" UpdateMode="Conditional" Visible="False">
                                                    <ContentTemplate>
                                                        <div class="cuadroPrincipal">
                                                            <div class="cuadroPrincipalTitulo">Detalle de Cargadores</div>
                                                            <div class="cuadroTexto">
                                                                <table cellspacing="0" style="">
                                                                    <tr>
                                                                        <td colspan="6">
                                                                            <asp:Label ID="nilblInformacionCargadores" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 110px; text-align: left;">
                                                                            <asp:Label ID="lblLaborCargue" runat="server" Text="Labor Carga"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; width: 360px;">
                                                                            <asp:DropDownList ID="ddlLaborCargue" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible="False" Width="350px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 120px; text-align: left;">
                                                                            <asp:LinkButton ID="lbFechaCargadores" runat="server" ForeColor="#003366" OnClick="lbFechaCargadores_Click" Visible="False">Fecha cargue</asp:LinkButton>
                                                                        </td>
                                                                        <td style="text-align: left; width: 120px;">
                                                                            <asp:Calendar ID="calendarCargadores" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="calendarCargadores_SelectionChanged" Visible="False" Width="150px">
                                                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                                <SelectorStyle BackColor="#CCCCCC" />
                                                                                <WeekendDayStyle BackColor="FloralWhite" />
                                                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                <OtherMonthDayStyle ForeColor="Gray" />
                                                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                            </asp:Calendar>
                                                                            <asp:TextBox ID="txtFechaCargue" runat="server" AutoPostBack="True" CssClass="input" Font-Bold="True" OnTextChanged="txtFechaCargadores_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: left; width: 80px;">
                                                                            <asp:Label ID="lblRacimosDC" runat="server" Text="Racimos"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left; width: 100px;">
                                                                            <asp:TextBox ID="txvRacimoCargue" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="80px">0</asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td colspan="6" style="text-align: center;">
                                                                            <div style="display: inline-block;">
                                                                                <select id="selTerceroCargue" runat="server" class="multiselect" multiple="true" name="countries[]" visible="False" style="width: 800px; height: 100px;">
                                                                                </select>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" style="text-align: center;">
                                                                            <asp:ImageButton ID="imbCargarCargadores" runat="server" ImageUrl="~/Imagen/Bonotes/btnCargar.png" OnClick="imbCargarCargadores_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCargar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnCargarN.png'" Style="height: 21px" ToolTip="Cargar detalle cargadores" />
                                                                            <asp:ImageButton ID="imbLiquidarCargue" runat="server" ImageUrl="~/Imagen/Bonotes/btnLiquidar.png" OnClick="imbLiquidarCargue_Click" onmouseout="this.src='../../Imagen/Bonotes/btnLiquidar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnLiquidarN.png'" />
                                                                            <asp:ImageButton ID="lbCancelarDCC" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" OnClick="lbCancelarDCC_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" ToolTip="Cancela la operación" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" style="text-align: center;">
                                                                            <asp:GridView ID="gvSubTotalesCargue" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="800px">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="novedades" HeaderText="Novedad">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="subCantidad" HeaderText="SubCantidad">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="subRacimo" HeaderText="SubRacimos">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SubJornal" HeaderText="SubJornales">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="nombreNovedades" HeaderText="nombreNovedades">
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="9pt" />
                                                                                <RowStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="8pt" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" style="text-align: center;">
                                                                            <asp:DataList ID="dlDetalleCargue" runat="server" OnDeleteCommand="dlDetalleCargue_DeleteCommand" RepeatColumns="2" RepeatDirection="Horizontal" Style="margin-right: 0px" Width="100%">
                                                                                <ItemTemplate>
                                                                                    <div style="padding: 2px; border: solid; border-color: silver; border-width: 1px; width: 535px;">
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 60px;">
                                                                                                        <asp:Label ID="Label83" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Sección"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 50px; text-align: left">
                                                                                                        <asp:Label ID="lblSeccion" runat="server" CssClass="auto-style6" Text='<%# Eval("codseccion") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesSeccion" runat="server" CssClass="auto-style6" Text='<%# Eval("desseccion") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left; width: 50px;">
                                                                                                        <asp:Label ID="lblpRacimos" runat="server" Style="color: #FFFFFF"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="Label84" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Lote"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 50px; text-align: left">
                                                                                                        <asp:Label ID="lblLote" runat="server" CssClass="auto-style6" Text='<%# Eval("codlote") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesLote" runat="server" CssClass="auto-style6" Text='<%# Eval("deslote") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblPesoPromedio" runat="server" CssClass="auto-style6" Text='<%# Eval("PesoRacimo") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <div style="padding-top: 2px">
                                                                                                            <table cellpadding="0" cellspacing="0" class="ui-accordion">
                                                                                                                <tr>
                                                                                                                    <td style="text-align: left; width: 90px;">
                                                                                                                        <asp:Label ID="lblCantidadD4" runat="server" CssClass="ui-priority-primary" Text="Cantidad"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvCantidadG" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left; width: 90px;">
                                                                                                                        <asp:Label ID="lblRacimosN7" runat="server" CssClass="ui-priority-primary" Text="No. racimos"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvRacimoG" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" OnTextChanged="txvRacimoG_TextChanged" Text='<%# Eval("racimos") %>' Width="70px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:Label ID="lblRacimosN8" runat="server" CssClass="ui-priority-primary" Text="Jornales"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: left">
                                                                                                                        <asp:TextBox ID="txvJornalesD" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" OnTextChanged="txvJornalesD_TextChanged" Text='<%# Eval("jornal") %>' Width="70px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td colspan="4" style="background-color: #0C1D87; color: #FFFFFF"><strong>Detalle de Cargue y Descargue</strong></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 70px; text-align: left;">
                                                                                                        <asp:Label ID="Label85" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Novedad"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 20px; text-align: left">
                                                                                                        <asp:Label ID="lblNovedad" runat="server" CssClass="auto-style6" Text='<%# Eval("codnovedad") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDesNovedad" runat="server" CssClass="auto-style6" Text='<%# Eval("desnovedad") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblUmedida" runat="server" CssClass="auto-style4" Text='<%# Eval("umedida") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 70px; text-align: left;">
                                                                                                        <asp:Label ID="Label86" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Fecha"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 80px; text-align: left">
                                                                                                        <asp:Label ID="lblFechaD" runat="server" CssClass="auto-style6" Text='<%# Eval("fechaD") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="Label87" runat="server" CssClass="auto-style3" Style="font-weight: bold" Text="Precio Labor $"></asp:Label>
                                                                                                        <asp:Label ID="lblPrecioLabor" runat="server" CssClass="auto-style6" Text='<%# Eval("precioLabor") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblDifKilos" runat="server" CssClass="auto-style4"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="padding-top: 3px; padding-bottom: 3px">
                                                                                                <asp:GridView ID="gvLotes" runat="server" AutoGenerateColumns="False" CssClass="Grid" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="5" Width="100%">
                                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="codTercero" HeaderText="Cod" ReadOnly="True">
                                                                                                            <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="5px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="desTercero" HeaderText="NombreTrabajador">
                                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Cantidad">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="Items" Width="70px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Jornal">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtJornal" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("jornal") %>' Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="Items" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="precioLabor" HeaderText="($)">
                                                                                                            <ItemStyle Width="5px" />
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                    <PagerStyle CssClass="pgr" />
                                                                                                    <RowStyle CssClass="rw" />
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="padding: 2px; margin: 1px; border: 1px solid silver; width: 528px;">
                                                                                            <table cellpadding="0" cellspacing="0" class="ui-accordion">
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 250px;">
                                                                                                        <asp:Label ID="Label88" runat="server" CssClass="ui-priority-primary" Text="Registro por novedad No."></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblRegistro" runat="server" Text='<%# Eval("registro") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px">
                                                                                                        <asp:Label ID="lblRegistroCosecha" runat="server" Text='<%# Eval("registroNovedad") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: right">
                                                                                                        <asp:ImageButton ID="ImageButton10" runat="server" CommandName="Delete" ImageUrl="~/Imagen/Bonotes/btnEliminar.png" onmouseout="this.src='../../Imagen/Bonotes/btnEliminar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnEliminarN.png'" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <SeparatorStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:DataList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="text-align: center;">
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="upDatos" runat="server" UpdateMode="Conditional" Visible="False">
                                <ContentTemplate>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <asp:UpdatePanel ID="upConsulta" runat="server" Visible="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <div style="display: inline-block; padding-top: 10px;">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="padding: 2px"></td>
                                                <td style="padding: 2px">
                                                    <asp:DropDownList ID="niddlCampo" runat="server" ToolTip="Selección de campo para busqueda" data-placeholder="Seleccione una opción..." CssClass="chzn-select" Width="250px">
                                                        <asp:ListItem Value="tiquete">Tiquete</asp:ListItem>
                                                        <asp:ListItem Value="fecha">Fecha</asp:ListItem>
                                                        <asp:ListItem Value="numero">Número</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 2px">
                                                    <asp:DropDownList ID="niddlOperador" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="niddlOperador_SelectedIndexChanged" ToolTip="Selección de operador para busqueda" Width="125px">
                                                        <asp:ListItem Value="like">Contiene</asp:ListItem>
                                                        <asp:ListItem Value="&lt;&gt;">Diferente</asp:ListItem>
                                                        <asp:ListItem Value="between">Entre</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="=">Igual</asp:ListItem>
                                                        <asp:ListItem Value="&gt;=">Mayor o Igual</asp:ListItem>
                                                        <asp:ListItem Value="&gt;">Mayor que</asp:ListItem>
                                                        <asp:ListItem Value="&lt;=">Menor o Igual</asp:ListItem>
                                                        <asp:ListItem Value="&lt;">Menor</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 2px">
                                                    <asp:TextBox ID="nitxtValor1" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="nitxtValor1_TextChanged" Width="200px"></asp:TextBox>
                                                    <asp:TextBox ID="nitxtValor2" runat="server" CssClass="input" Visible="False" Width="200px"></asp:TextBox>
                                                </td>
                                                <td style="padding: 2px">
                                                    <asp:ImageButton ID="niimbAdicionar" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Enabled="False" ImageUrl="~/Imagen/TabsIcon/filter.png" OnClick="niimbAdicionar_Click" ToolTip="Clic aquí para adicionar parámetro a la busqueda" Width="18px" />
                                                </td>
                                                <td style="padding: 2px">
                                                    <asp:ImageButton ID="imbBusqueda" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Enabled="False" ImageUrl="~/Imagen/TabsIcon/search.png" OnClick="imbBusqueda_Click" Style="height: 18px" ToolTip="Clic aquí para realizar la busqueda" Visible="False" />
                                                </td>
                                                <td style="padding: 2px">
                                                    <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label>
                                                </td>
                                                <td style="padding: 2px;"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="8">
                                                    <asp:Label ID="nilblMensajeEdicion" runat="server" ForeColor="Navy"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8" style="text-align: center;">
                                                    <div style="text-align: center">
                                                        <div style="display: inline-block">
                                                            <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" Width="400px">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imbEliminarParametro1" runat="server" CommandName="Delete" ImageUrl="~/Imagen/TabsIcon/cancel.png" ToolTip="Elimina el parámetro de la consulta" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle BackColor="White" HorizontalAlign="Center" />
                                                                        <ItemStyle BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="campo" HeaderText="Campo">
                                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="operador" HeaderText="Operador">
                                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="valor" HeaderText="Valor">
                                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="valor2" HeaderText="Valor 2">
                                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <RowStyle CssClass="rw" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>


                                        </table>
                                    </div>
                                    <div style="text-align: center">
                                        <div style="display: inline-block; width: 950px;">
                                            <asp:GridView ID="gvTransaccion" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" Width="950px" AllowPaging="True" OnPageIndexChanging="gvTransaccion_PageIndexChanging">
                                                <PagerStyle CssClass="pgr" />
                                                <RowStyle CssClass="rw" />
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="Update" HeaderText="Edit" ImageUrl="~/Imagen/TabsIcon/pencil.png" Text="Botón">
                                                        <ItemStyle CssClass="Items" Width="20px" />
                                                    </asp:ButtonField>
                                                    <asp:TemplateField HeaderText="Elim">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imElimina" runat="server" CommandName="Delete" ImageUrl="~/Imagen/TabsIcon/cancel.png" OnClientClick="if(!confirm('Desea eliminar el registro ?')){return false;};" ToolTip="Elimina el registro seleccionado" />
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="White" />
                                                        <ItemStyle CssClass="Items" HorizontalAlign="Center" Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="numero" HeaderText="Numero">
                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="160px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tiquete" HeaderText="Tiquete">
                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="observacion" HeaderText="Observación">
                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                                                        <ItemStyle CssClass="Items" HorizontalAlign="Center" Width="20px" />
                                                    </asp:CheckBoxField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>


                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtFiltroBascula" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
        <script src="../../js/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
