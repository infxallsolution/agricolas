<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroTiquete.aspx.cs" Inherits="Agronomico.WebForms.Formas.Ptransaccion.RegistroTiquete" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transacciones Agro</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        setInterval('MantenSesion()', '<%#(int) (0.9 * (Session.Timeout * 60000)) %>');
    </script>
    <script type="text/javascript">
        function MantenSesion() {
            var CONTROLADOR = "refresh_session.ashx";
            var head = document.getElementsByTagName('head').item(0);
            script = document.createElement('script');
            script.src = CONTROLADOR;
            script.setAttribute('type', 'text/javascript');
            script.defer = true;
            head.appendChild(script);
        }
        function igualarRacimos() {
            if (parseFloat(document.getElementById("txvRacimosTiquete").value) > 0) {
                document.getElementById("txvRacimos").value = document.getElementById("txvRacimosTiquete").value
            }
        }
        function obtener_neto() {
            var pesoBruto = document.getElementById("txvPbruto").value.replace(/\,/g, '');
            var pesoTara = document.getElementById("txvPtara").value.replace(/\,/g, '');;
            var pesoNeto = document.getElementById("txvPneto");

            if (!isNaN(pesoBruto) & !isNaN(pesoTara)) {
                if (pesoTara != "") {
                    var resultado = parseFloat(pesoBruto) - parseFloat(pesoTara);
                    if (resultado < 0) {
                        alert("La tara no puede ser mayor al neto");
                        document.getElementById("txvPtara").value = 0;
                        document.getElementById("txvPneto").value = 0;
                    } else {
                        pesoNeto.value = parseFloat(resultado).toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        pesoNeto.value = pesoNeto.value.split('').reverse().join('').replace(/^[\,]/, '');
                    }
                }
                else {
                    pesoNeto.value = 0;
                }

            } else {
                pesoNeto.value = 0;
            }
            if (parseFloat(pesoNeto.value) > 0) {
                document.getElementById("txvCantidad").value = pesoNeto.value;
            } else {
                document.getElementById("txvCantidad").value = 0;
            }
        }
    </script>

</head>
<body>
    <div class="container">
        <%-- <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>--%>
        <form id="form1" runat="server">
            <asp:Button ID="niimbRegistro" runat="server" OnClick="niimbRegistro_Click" Text="Registro" CssClass="botones" />
            <asp:Button ID="imbConsulta" runat="server" OnClick="imbConsulta_Click" CssClass="botones" Text="Consulta" />
            <hr />
            <div id="upGeneral" runat="server">
                <div style="text-align: center;">
                    <asp:Button ID="nilbNuevo" runat="server" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click" CssClass="botones" Text="Nuevo" />
                    <asp:Button ID="lbCancelar" runat="server" ToolTip="Cancela la operación" OnClick="lbCancelar_Click"
                        CssClass="botones" Text="Cancelar" Visible="False" />
                    <asp:Button ID="lbRegistrar" runat="server" ToolTip="Guarda el nuevo registro en la base de datos"
                        OnClick="lbRegistrar_Click1" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" CssClass="botones" Text="Guardar" Visible="False" />
                    <asp:Button ID="niimbImprimir" runat="server" ToolTip="Haga clic aqui para realizar la busqueda"
                        OnClick="niimbImprimir_Click" CssClass="botones" Text="Imprimir" />
                </div>
                <div style="text-align: center">
                    <asp:Label ID="nilblInformacion" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <div id="upBascula" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 10%"></td>
                                <td colspan="2" style="text-align: center">
                                    <h6>Detalle de la Bascula</h6>
                                    <hr />
                                    <asp:Label ID="nilbInofrmacionBascula" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                       
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <table style="width: 100%" id="tbFiltroTiquete" runat="server">
                                             <tr>
                                <td style="text-align: left" colspan="5">
                                    <asp:RadioButtonList ID="rblBascula" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblBascula_SelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">Tiquete del grupo</asp:ListItem>
                                        <asp:ListItem Value="2">Tiquete Externo</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>

                            </tr>
                                        <tr>
                                            <td style="width: 90px; text-align: left">
                                                <asp:Label ID="lblExtractoraFiltro" runat="server" Text="Extractora"></asp:Label>
                                            </td>
                                            <td style="width: 400px; text-align: left;">
                                                <asp:DropDownList ID="ddlExtractoraFiltro" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlExtractoraFiltro_SelectedIndexChanged" Width="320px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 120px; text-align: left">
                                                <asp:Label ID="lblFiltroBusqueda" runat="server" Text="Filtro tiquete"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 180px;">
                                                <asp:TextBox ID="txtFiltroBascula" runat="server" CssClass="input" Width="150px" ToolTip="Ingrese numero tiquete"></asp:TextBox>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button ID="imbBuscarBascula" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" OnClick="imbBuscarBascula_Click" onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" CssClass="botones" Text="Buscar tiquete" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 5px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;">
                                                <asp:GridView ID="gvTiquetes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="5" Width="100%" AllowPaging="True" OnSelectedIndexChanged="gvTiquetes_SelectedIndexChanged">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                                            <ItemStyle Width="20px" CssClass="action-item" />
                                                            <HeaderStyle CssClass="action-item" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="extractora" HeaderText="IdExt">
                                                            <ItemStyle Width="5px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombreextractora" HeaderText="NombreExtractora">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tiquete" HeaderText="Tiquete" ReadOnly="True">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyy}">
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="vehiculo" HeaderText="Vehí.">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="remolque" HeaderText="Remol.">
                                                            <ItemStyle Width="30px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pesoBruto" HeaderText="pBruto(kg)" DataFormatString="{0:N}">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pesoTara" HeaderText="pTara(kg)" DataFormatString="{0:N}">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pesoNeto" HeaderText="pNeto(kg)" DataFormatString="{0:N}">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sacos" HeaderText="Sacos">
                                                            <ItemStyle Width="20px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="racimos" HeaderText="Racimos">
                                                            <ItemStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="codigoConductor" HeaderText="C.C. ">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombreConductor" HeaderText="NombreConductor">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="thead" />
                                                    <PagerStyle CssClass="footer" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="5">
                                                <div id="upTiquete" runat="server">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 120px; text-align: left;">
                                                                <asp:Label ID="lbTiquete1" runat="server" Text="Extractora"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:DropDownList ID="ddlExtractoraTiquete" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 120px; text-align: left">
                                                                <asp:Label ID="lbTiquete" runat="server" Text="Tiquete"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txtTiquete" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 120px; text-align: left;">
                                                                <asp:Label ID="lbFechaTiqueteI" runat="server" OnClick="lbFecha_Click">Fecha Tiquete</asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txtFechaTiqueteI" runat="server" class="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" ReadOnly="True" CssClass="input fecha"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 120px; text-align: left">
                                                                <asp:Label ID="Label3" runat="server" Text="Sacos"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txvSacos" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px">0</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 120px; text-align: left;">
                                                                <asp:Label ID="Label2" runat="server" Text="Peso Bruto"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txvPbruto" runat="server" CssClass="input" onkeyup="formato_numero(this); obtener_neto();" Width="150px" AutoPostBack="True" OnTextChanged="txvPbruto_TextChanged">0</asp:TextBox>
                                                            </td>
                                                            <td style="width: 120px; text-align: left">
                                                                <asp:Label ID="Label6" runat="server" Text="Racimos"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txvRacimosTiquete" runat="server" CssClass="input" onkeyup="formato_numero(this); igualarRacimos();" Width="150px">0</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 120px; text-align: left;">
                                                                <asp:Label ID="Label4" runat="server" Text="Peso Tara"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txvPtara" runat="server" CssClass="input" onkeyup="formato_numero(this); obtener_neto(); " Width="150px" AutoPostBack="True" OnTextChanged="txvPtara_TextChanged">0</asp:TextBox>
                                                            </td>
                                                            <td style="width: 120px; text-align: left">
                                                                <asp:Label ID="Label14" runat="server" Text="Vehículo"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txtVehiculo" runat="server" CssClass="input" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 120px; text-align: left;">
                                                                <asp:Label ID="Label5" runat="server" Text="Peso Neto"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txvPneto" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="150px">0</asp:TextBox>
                                                            </td>
                                                            <td style="width: 120px; text-align: left">
                                                                <asp:Label ID="Label15" runat="server" Text="Remolque"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txtRemolque" runat="server" CssClass="input" Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                    <hr />
                                                </div>
                                                <div id="upCabeza" runat="server">
                                                    <table class="w-100" id="tbCabeza" runat="server">
                                                        <tr>
                                                            <td style="text-align: center;" colspan="4">
                                                                <h6>Datos de transacción</h6>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 120px; text-align: left;">
                                                                <asp:Label ID="lbFecha" runat="server">Fecha Transacción</asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 350px;">
                                                                <asp:TextBox ID="txtFecha" runat="server" CssClass="input fecha" autocomplete="off" OnTextChanged="txtFecha_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 120px; text-align: left">
                                                                <asp:Label ID="lblRemision" runat="server" Text="Remisión"></asp:Label>
                                                            </td>
                                                            <td style="width: 350px; text-align: left">
                                                                <asp:TextBox ID="txtRemision" runat="server" CssClass="input" Width="150px" Wrap="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;" colspan="4">
                                                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="input" placeholder="Observaciones y/o notas..." Height="40px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="upRecolector" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td></td>
                            <td style="text-align: center;" colspan="6">
                                <h6>Detalle del transporte</h6>
                                <hr />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: left; width: 150px;">
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLaborTransporte" runat="server" Text="Labor Transporte" AutoPostBack="True" OnCheckedChanged="chkLaborTransporte_CheckedChanged" Checked="True" />
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlLaborTransporte" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 100px;">
                                <asp:Label ID="lbFechaCargadores0" runat="server" OnClick="lbFechaCargadores_Click">Fecha trasnporte</asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtFechaTransporte" runat="server" AutoPostBack="True" CssClass="input fecha" Font-Bold="True" autocomplete="off" OnTextChanged="txtFechaCargadores_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Width="110px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="Label7" runat="server" Text="Cuadrilla"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlCuadrillaTransporte" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: left;" colspan="6">
                                <table class="w-100">
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td>
                                            <select id="selTerceroTransporte" runat="server" class="multiselect" multiple="true" name="countries[]2" style="width: 800px; height: 150px; text-align: center;">
                                            </select></td>
                                        <td style="width: 20%"></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: center;" colspan="6">
                                <h6>Detalle del cargue</h6>
                                <hr />
                                <asp:Label ID="nilblInformacionCargadoresTiq" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="width: 110px; text-align: left;">
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLaborCargue" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="chkLaborCargue_CheckedChanged" Text="Labor Carga" />
                            </td>
                            <td style="text-align: left; width: 400px">
                                <asp:DropDownList ID="ddlLaborCargadores" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 100px">
                                <asp:Label ID="lbFechaCargadores" runat="server" OnClick="lbFechaCargadores_Click">Fecha cargue</asp:Label>
                            </td>
                            <td style="text-align: left; width: 140px">
                                <asp:TextBox ID="txtFechaCargadores" runat="server" AutoPostBack="True" CssClass="input fecha" Font-Bold="True" autocomplete="off" OnTextChanged="txtFechaCargadores_TextChanged" ToolTip="Formato fecha (dd/mm/yyyy)" Width="110px"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 140px">
                                <asp:Label ID="lbFechaD1" runat="server" OnClick="lbFechaD_Click">Cuadrilla</asp:Label>
                            </td>
                            <td style="text-align: left; width: 400px">
                                <asp:DropDownList ID="ddlCuadrillaCargue" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: left;" colspan="6">
                                <table class="w-100">
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td>
                                            <select id="selTerceroCargue" runat="server" class="multiselect" multiple="true" name="countries[]1" style="width: 800px; height: 150px;">
                                            </select></td>
                                        <td style="width: 20%"></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div id="upDetalle" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td></td>
                            <td colspan="4">
                                <h6>Detalle de la cosecha</h6>
                                <hr />
                                <asp:Label ID="nilblInformacionDetalle" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="width: 110px; text-align: left;">
                                <asp:Label ID="lblFinca" runat="server" Text="Finca"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 400px">
                                <asp:DropDownList ID="ddlFinca" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlFinca_SelectedIndexChanged2" Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 140px">
                                <asp:Label ID="lblSeccion" runat="server" Text="Sección"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 400px">
                                <asp:DropDownList ID="ddlSeccion" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlSeccion_SelectedIndexChanged" Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: left; width: 110px;">
                                <asp:Label ID="lblLote" runat="server" Text="Lote"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 400px" style="vertical-align: middle; line-height: normal">
                                <asp:DropDownList ID="ddlLote" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 110px;">
                                <asp:Label ID="lbFechaD" runat="server" OnClick="lbFechaD_Click">Fecha cosecha</asp:Label>
                            </td>
                            <td style="text-align: left; width: 400px">
                                <asp:TextBox ID="txtFechaD" runat="server" CssClass="input fecha" Font-Bold="True" autocomplete="off" ToolTip="Formato fecha (dd/mm/yyyy)" Width="150px" OnTextChanged="txtFechaD_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: left; width: 110px;">
                                <asp:Label ID="lblRacimosD" runat="server" Text="Racimos"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 400px" style="vertical-align: middle; line-height: normal">
                                <table class="w-100">
                                    <tr>
                                        <td style="width: 140px; text-align: left;">
                                            <asp:TextBox ID="txvRacimosD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="110px">0</asp:TextBox>
                                        </td>
                                        <td style="width: 60px; text-align: left;">
                                            <asp:Label ID="lblSacos" runat="server" Text="Sacos"></asp:Label>
                                        </td>
                                        <td class="bordesBusqueda">
                                            <asp:TextBox ID="txvSacosD" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="60px">0</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left; width: 110px;">
                                <asp:Label ID="lbFechaD0" runat="server" OnClick="lbFechaD_Click">Cuadrilla</asp:Label>
                            </td>
                            <td style="text-align: left; width: 400px">
                                <asp:DropDownList ID="ddlCuadrilla" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver">
                                <div>
                                    <table class="w-100">
                                        <tr>
                                            <td style="width: 20%"></td>
                                            <td>
                                                <select id="selTerceroCosecha" runat="server" class="multiselect" multiple="true" name="countries[]0" style="width: 800px; height: 150px;">
                                                </select></td>
                                            <td style="width: 20%"></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>

                            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4" style="text-align: center;">
                                <asp:Button ID="imbCargar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCargar.png" OnClick="imbCargar_Click1" onmouseout="this.src='../../Imagen/Bonotes/btnCargar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnCargarN.png'" CssClass="botones" Text="Cargar" />
                                <asp:Button ID="imbLiquidar" runat="server" ImageUrl="~/Imagen/Bonotes/btnLiquidar.png" OnClick="imbLiquidar_Click" onmouseout="this.src='../../Imagen/Bonotes/btnLiquidar.png'" onmouseover="this.src='../../Imagen/Bonotes/btnLiquidarN.png'" CssClass="botones" Text="Liquidar" />
                                <asp:Button ID="lbCancelarD" runat="server" OnClick="lbCancelarD_Click" ToolTip="Cancela la operación" CssClass="botones" Text="Cancelar" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver"></td>
                            <td colspan="4" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver">
                                <asp:GridView ID="gvSubTotales" runat="server" CssClass="table table-bordered table-sm  table-hover table-striped grillas" AutoGenerateColumns="False" BorderStyle="None" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="novedades" HeaderText="Novedad">
                                            <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subCantidad" HeaderText="SubCantidad">
                                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subRacimo" HeaderText="SubRacimos">
                                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SubJornal" HeaderText="SubJornales">
                                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombreNovedades" HeaderText="nombreNovedades">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead" />
                                    <PagerStyle CssClass="footer" />
                                </asp:GridView>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4">
                                <asp:DataList ID="dlDetalle" runat="server" RepeatColumns="2" Style="margin-right: 0px" Width="100%" OnDeleteCommand="dlDetalle_DeleteCommand" RepeatDirection="Horizontal" OnItemCommand="dlDetalle_ItemCommand">
                                    <ItemTemplate>
                                        <div style="padding: 5px; border: solid; border-color: silver; border-width: 1px; width: 570px;">
                                            <div style="border: 1px solid silver;">
                                                <div style="padding: 2px">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 70px; text-align: left;"><b>Finca</b></td>
                                                            <td style="width: 80px; text-align: left">
                                                                <asp:Label ID="lblFinca" runat="server" Text='<%# Eval("codFinca") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" colspan="2">
                                                                <asp:Label ID="lblDesFinca" runat="server" Text='<%# Eval("desFinca") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 70px; text-align: left;">
                                                                <strong>
                                                                    <asp:Label ID="Label16" runat="server" Text="Novedad"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 20px; text-align: left">
                                                                <asp:Label ID="lblNovedad" runat="server" Text='<%# Eval("codnovedad") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblDesNovedad" runat="server" Text='<%# Eval("desnovedad") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 80px;">
                                                                <asp:Label ID="lblUmedida" runat="server" Text='<%# Eval("umedida") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 70px; text-align: left;">
                                                                <strong>
                                                                    <asp:Label ID="Label17" runat="server" Text="Sección"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 20px; text-align: left">
                                                                <asp:Label ID="lblSeccion" runat="server" Text='<%# Eval("codseccion") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblDesSeccion" runat="server" Text='<%# Eval("desseccion") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 80px;">
                                                                <asp:Label ID="lblPesoPromedio" runat="server" Text='<%# Eval("PesoRacimo") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 70px; text-align: left;">
                                                                <strong>
                                                                    <asp:Label ID="Label18" runat="server" Text="Lote"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 20px; text-align: left">
                                                                <asp:Label ID="lblLote" runat="server" Text='<%# Eval("codlote") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblDesLote" runat="server" Text='<%# Eval("deslote") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 80px;">
                                                                <asp:Label ID="lblpRacimos" runat="server"></asp:Label>
                                                                <asp:Label ID="lblDifKilos" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 70px; text-align: left;">
                                                                <strong>
                                                                    <asp:Label ID="Label19" runat="server" Text="Fecha"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 20px; text-align: left">
                                                                <asp:Label ID="lblFechaD" runat="server" Text='<%# Eval("fechaD") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="Label20" runat="server" Text="Precio Labor $"></asp:Label>
                                                                <asp:Label ID="lblPrecioLabor" runat="server" Text='<%# Eval("precioLabor") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 80px;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 70px; text-align: left;">
                                                                <strong>
                                                                    <asp:Label ID="lblTercero" runat="server" Text="Trabajador"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td colspan="2" style="text-align: left">
                                                                <asp:DropDownList ID="ddlTerceroGrilla" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="350px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:LinkButton runat="server" ID="imbCargarTercero" CssClass="btn btn-default btn-sm btn-success fa fa-plus" ToolTip="Seleccione el tercero que desea agregar" CommandName="Select"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="imbCargarTercero0" CssClass="btn btn-default btn-sm btn-danger fa fa-minus" ToolTip="Antes chequee el terceros en la grilla que quiere eliminar" CommandName="Update"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div style="padding-top: 2px">
                                                <table class="ui-accordion">
                                                    <tr>
                                                        <td style="text-align: left; width: 90px;">
                                                            <asp:Label ID="lblCantidadD0" runat="server" CssClass="ui-priority-primary" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txvCantidadG" runat="server" CssClass="input" onkeyup="formato_numero(this)" Width="70px" Text='<%# Eval("cantidad") %>'></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left"></td>
                                                        <td></td>
                                                        <td style="text-align: left; width: 90px;">
                                                            <asp:Label ID="lblRacimosN" runat="server" CssClass="ui-priority-primary" Text="No. racimos"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txvRacimoG" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("racimos") %>' Width="70px" AutoPostBack="True" OnTextChanged="txvRacimoG_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left; width: 40px;">
                                                            <asp:Label ID="lblSacosN" runat="server" CssClass="ui-priority-primary" Text="Sacos"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txvSacosG" runat="server" AutoPostBack="True" CssClass="input" onkeyup="formato_numero(this)" OnTextChanged="txvRacimoG_TextChanged" Text='<%# Eval("sacos") %>' Width="50px"></asp:TextBox>
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

                                            <div style="padding-top: 3px; padding-bottom: 3px">
                                                <asp:GridView ID="gvLotes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="5" Width="100%">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Elim">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSeleccion" runat="server" OnCheckedChanged="chkSeleccion_CheckedChanged" ToolTip="Eliminar tercero" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="codTercero" HeaderText="codTrab" ReadOnly="True">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="desTercero" HeaderText="NombreTrabajador">
                                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Cantidad">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("cantidad") %>' Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jornal">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtJornal" runat="server" CssClass="input" Enabled="False" Text='<%# Eval("jornal") %>' Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="precioLabor" HeaderText="Precio($)">
                                                            <ItemStyle Width="10px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="thead" />
                                                    <PagerStyle CssClass="footer" />
                                                </asp:GridView>
                                            </div>


                                            <div>
                                                <table class="ui-accordion">
                                                    <tr>
                                                        <td style="text-align: left; width: 150px;">
                                                            <asp:Label ID="Label1" runat="server" CssClass="ui-priority-primary" Text="Registro por novedad No."></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblRegistro" runat="server" Text='<%# Eval("registro") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 100px"></td>
                                                        <td style="text-align: right">
                                                            <asp:Button ID="Button1" runat="server" CommandName="Delete" CssClass="botones" Text="Eliminar" />

                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <SeparatorStyle />
                                </asp:DataList>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div id="upConsulta" runat="server">
                    <div style="text-align: center">
                        <div style="padding-top: 10px;">
                            <table style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td style="text-align: left; width: 400px;">
                                        <asp:DropDownList ID="niddlCampo" runat="server" ToolTip="Selección de campo para busqueda" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="95%">
                                            <asp:ListItem Value="tiquete">Tiquete</asp:ListItem>
                                            <asp:ListItem Value="fecha">Fecha</asp:ListItem>
                                            <asp:ListItem Value="numero">Número</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 200px;">
                                        <asp:DropDownList ID="niddlOperador" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" AutoPostBack="True" OnSelectedIndexChanged="niddlOperador_SelectedIndexChanged" ToolTip="Selección de operador para busqueda" Width="90%">
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
                                    <td style="text-align: left; width: 220px;">
                                        <asp:TextBox ID="nitxtValor1" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="nitxtValor1_TextChanged" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="nitxtValor2" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter" OnClick="niimbAdicionar_Click" ToolTip="Clic aquí para adicionar parámetro a la busqueda"></asp:LinkButton>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:LinkButton runat="server" ID="imbBusqueda" CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="imbBusqueda_Click" ToolTip="Clic aquí para realizar la busqueda"></asp:LinkButton>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="7" style="text-align: center">
                                        <table class="ui-accordion">
                                            <tr>
                                                <td></td>
                                                <td style="width: 300px">
                                                    <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" Width="400px">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                <HeaderStyle CssClass="action-item" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="campo" HeaderText="Campo">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="operador" HeaderText="Operador">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor" HeaderText="Valor">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor2" HeaderText="Valor 2">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="thead" />
                                                        <PagerStyle CssClass="footer" />
                                                    </asp:GridView>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td></td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>


                            </table>
                        </div>
                        <div style="width: 100%;">
                            <div style="width: 100%;">
                                <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label>
                                <asp:GridView ID="gvTransaccion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" Width="100%" AllowPaging="True" OnPageIndexChanging="gvTransaccion_PageIndexChanging" PageSize="30">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:ButtonField CommandName="Update" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                            <ItemStyle Width="20px" CssClass="action-item" />
                                            <HeaderStyle CssClass="action-item" />
                                        </asp:ButtonField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                            <HeaderStyle CssClass="action-item" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero" HeaderText="Numero">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tiquete" HeaderText="Tiquete">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="observacion" HeaderText="Observación">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:CheckBoxField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead" />
                                    <PagerStyle CssClass="footer" />
                                </asp:GridView>
                            </div>
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
        </form>
    </div>
</body>
</html>
