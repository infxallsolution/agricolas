<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Egresos.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxPagar.Egresos" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos contables</title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js"></script>
    <link href="http://app.infos.com/RecursosInfos/css/general.css" rel="stylesheet" />
    <link href="http://app.infos.com/RecursosInfos/lib/chosen187/chosen.css" rel="stylesheet" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <script type="text/javascript"> 
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
            function endReq(sender, args) {

                $(document).ready(function () {
                    $(".check").click(function () {
                        var chequeado;
                        var input = $(this).find("input:checkbox");
                        $(input).each(function () { chequeado = this.checked });
                        if (chequeado) {
                            var valorPagar = $($($(this).parent()).parent()).find("input:text");
                            $(valorPagar).removeAttr("disabled");
                            var filaVlorcxc = $($($(this).parent()).parent()).find("td:eq(9)");
                            var filaSaldo = $($($(this).parent()).parent()).find("td:eq(11)");
                            $(valorPagar).val($(filaVlorcxc).text());
                            $(filaSaldo).val("0");
                        }
                        else {
                            var valorPagar = $($($(this).parent()).parent()).find("input:text");
                            $(valorPagar).val("0");
                            $(valorPagar).attr("disabled", true);
                        }


                        totalizarPago();
                    });

                    $(".valorPago").keydown(function () {
                        var filaSaldo = $($($(this).parent()).parent()).find("td:eq(11)");
                        var filaVlorcxc = $($($(this).parent()).parent()).find("td:eq(9)");
                        var valornumericocxc = parseFloat($(filaVlorcxc).text().split(',').join(''));
                        var valorNumericoPago = parseFloat($(this).val().split(',').join(''));
                        if (valorNumericoPago > valornumericocxc)
                            $(this).val($(filaVlorcxc).text());

                        var resultado = (parseFloat($(filaVlorcxc).text().split(',').join('')) - parseFloat($(this).val().split(',').join(''))).toFixed(2);
                        index = resultado.toString().indexOf(".");
                        if (index > 0) {
                            var part1 = resultado.toString().split('.');
                            resultado = part1[0].split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                            resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '') + ("." + part1[1]);
                        }
                        else {
                            resultado = resultado.split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                            resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '');
                        }

                        console.log(resultado);
                        if (resultado.toString().indexOf("NaN") > 0) {
                            resultado = 0;
                        }

                        filaSaldo.text(resultado);
                        totalizarPago();
                    });

                    $('.solonumero').on('input', function () {
                        this.value = this.value.replace(/[^0-9]/g, '');
                    });
                    $("#imbBuscarCuenta").click(function () {
                        buscarCuenta();
                    });
                    function buscarCuenta() {
                        var width = 650;
                        var height = 400;
                        var left = (screen.width / 2) - (width / 2);
                        var top = (screen.height / 2) - (height / 2);
                        var cuenta = $("#txtCuenta").val();
                        z = window.open("BuscarCuentas.aspx?cuenta=" + cuenta, "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                        z.focus();
                    }

                    $("#btnTerceroEncabezado").click(function () {
                        terceroencabezado();
                    });
                    $("#btnBeneficiario").click(function () {
                        terceroBeneficiario();
                    });

                    function totalizarPago() {

                        var gvLista = $("#gvLista tbody");
                        var total = 0;
                        var resultado = "0";
                        var totalResultado = $("#txtTotalPagar");

                        $(gvLista).each(function (e) {
                            $(this).find("input:text").each(function () {
                                resultado = (parseFloat(resultado.split(',').join('')) + parseFloat($(this).val().split(',').join(''))).toFixed(2);
                            });
                        });

                        index = resultado.toString().indexOf(".");

                        if (index > 0) {
                            var part1 = resultado.toString().split('.');
                            resultado = part1[0].split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                            resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '') + ("." + part1[1]);
                        }
                        else {
                            resultado = resultado.split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                            resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '');
                        }

                        console.log(resultado);
                        if (resultado.toString().indexOf("NaN") > 0) {
                            resultado = 0;
                        }
                        $("#hfTotalPagar").val(resultado);
                        $(totalResultado).val(resultado);

                    }
                    function terceroencabezado() {
                        if ($("#txtFecha").val() == "") {
                            swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                            return;
                        }

                        var width = 650;
                        var height = 400;
                        var left = (screen.width / 2) - (width / 2);
                        var top = (screen.height / 2) - (height / 2);
                        var cuenta = $("#txtIdTerceroEncabezado").val();
                        z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=1", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                        z.focus();
                    }

                    function terceroBeneficiario() {
                        if ($("#txtFecha").val() == "") {
                            swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                            return;
                        }

                        var width = 650;
                        var height = 400;
                        var left = (screen.width / 2) - (width / 2);
                        var top = (screen.height / 2) - (height / 2);
                        var cuenta = $("#txtBeneficiario").val();
                        z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=2", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                        z.focus();
                    }


                    $("#imbBuscarTercero").click(function () {
                        var width = 650;
                        var height = 400;
                        var left = (screen.width / 2) - (width / 2);
                        var top = (screen.height / 2) - (height / 2);
                        var cuenta = $("#txtTercero").val();
                        z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=2", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                        z.focus();
                    })

                    $("#txvBase").keyup(function () {
                        var resultado = ((parseFloat($(this).val().split(',').join('')) * parseFloat($("#txvPorcentaje").val())) / 100).toFixed(2);
                        var index = 0;
                        index = resultado.toString().indexOf(".");
                        if (index > 0) {
                            var part1 = resultado.toString().split('.');
                            resultado = part1[0].split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                            resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '') + ("." + part1[1]);
                        }
                        else {
                            resultado = resultado.split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                            resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '');
                        }

                        console.log(resultado);
                        if (resultado.toString().indexOf("NaN") > 0) {
                            resultado = 0;
                        }

                        $("#txvValor").val(resultado);
                    });

                });


                function terceroBeneficiario() {
                    if ($("#txtFecha").val() == "") {
                        swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                        return;
                    }

                    var width = 650;
                    var height = 400;
                    var left = (screen.width / 2) - (width / 2);
                    var top = (screen.height / 2) - (height / 2);
                    var cuenta = $("#txtBeneficiario").val();
                    z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=2", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                    z.focus();
                }


                function terceroencabezado() {
                    if ($("#txtFecha").val() == "") {
                        swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                        return;
                    }

                    var width = 650;
                    var height = 400;
                    var left = (screen.width / 2) - (width / 2);
                    var top = (screen.height / 2) - (height / 2);
                    var cuenta = $("#txtIdTerceroEncabezado").val();
                    z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=1", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                    z.focus();
                }


                function ActualizarTerceroEncabezado(codigo, nombre, id) {
                    var controlCuenta = document.getElementById("txtIdTerceroEncabezado");
                    var controlNombreCuenta = document.getElementById("txtNombreTerceroEncabezado");
                    var controlid = document.getElementById("hfidTercero");
                    if (controlCuenta != null) {
                        controlCuenta.value = codigo;
                        controlCuenta.focus();
                        controlNombreCuenta.value = nombre;
                        controlid.value = id;
                    }
                    console.log(controlid.value);
                    __doPostBack('<%= txtIdTerceroEncabezado.ClientID  %>', '')
                }

                function ActualizarTerceroBeneficiario(codigo, nombre, id) {
                    var controlCuenta = document.getElementById("txtBeneficiario");
                    var controlNombreCuenta = document.getElementById("txtNombreBeneficiario");
                    var controlid = document.getElementById("hfBeneficiario");
                    var controlidtxt = document.getElementById("txtidBeneficiario");
                    if (controlCuenta != null) {
                        controlCuenta.value = codigo;
                        controlCuenta.focus();
                        controlNombreCuenta.value = nombre;
                        controlid.value = id;
                        controlidtxt.value = id;
                    }
                    console.log(controlid.value);
                }

                function buscarCuenta() {
                    var width = 650;
                    var height = 400;
                    var left = (screen.width / 2) - (width / 2);
                    var top = (screen.height / 2) - (height / 2);
                    var cuenta = $("#txtCuenta").val();
                    z = window.open("BuscarCuentas.aspx?cuenta=" + cuenta, "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                    z.focus();
                }
            }
        </script>
        <div class="container">
            <asp:UpdatePanel ID="upGeneral" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-1">
                            <asp:Button ID="niimbRegistro" Width="100%" runat="server" CssClass="botones" OnClick="niimbRegistro_Click" Text="Registro" ToolTip="Panel de registro" />
                        </div>
                        <div class="col-1">
                            <asp:Button ID="niimbConsulta" Width="100%" runat="server" CssClass="botones" OnClick="niimbConsulta_Click" Text="Consulta" ToolTip="Panel de consulta" />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="upRegistro" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-2">
                                </div>
                                <div class="col-8">
                                    <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo" ToolTip="click para realizar nuevo registro" />
                                    <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="click para cancelar el registro" />
                                    <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" Text="Registrar" ToolTip="Registrar transaccion" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                                    <asp:Button ID="lbAprobar" runat="server" CssClass="botones" Text="Aprobar" ToolTip="Registrar transaccion" OnClientClick="if(!confirm('Desea aprobar el registro ?')){return false;};"  OnClick="lbAprobar_Click" />
                                </div>
                            </div>
                            <hr>
                            <div class="row text-justify">
                                <div class="col-1">
                                </div>
                                <div class="col-2">
                                    <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo transacción..." ></asp:Label>
                                </div>
                                <div class="col-4">
                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                         Style="left: -1px; top: 0px">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-2">
                                    <asp:Label ID="lblNumero" runat="server" Text="Número de docto" ></asp:Label>
                                </div>
                                <div class="col-2">
                                    <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="True" OnTextChanged="txtNumero_TextChanged"
                                         Width="100%" CssClass="input"></asp:TextBox>
                                </div>
                                <div class="col-3">
                                </div>
                            </div>
                            <hr>
                            <asp:UpdatePanel ID="upCabeza" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdTransaccionConfig" runat="server" />
                                    <asp:HiddenField ID="hdRegistro" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdCantidad" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdCliente" runat="server" />
                                    <asp:HiddenField ID="hdSA" runat="server" />
                                    <asp:HiddenField ID="hdProveedor" runat="server" />
                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2">
                                            <asp:LinkButton ID="lbFecha" runat="server" OnClick="lbFecha_Click"
                                                 Style="color: #003366">Fecha transacción</asp:LinkButton>
                                        </div>
                                        <div class="col-2">
                                            <asp:Calendar ID="niCalendarFecha" runat="server" BackColor="White" BorderColor="#999999"
                                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFecha_SelectionChanged"
                                                 Width="150px">
                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                <SelectorStyle BackColor="#CCCCCC" />
                                                <WeekendDayStyle BackColor="FloralWhite" />
                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <OtherMonthDayStyle ForeColor="Gray" />
                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                            </asp:Calendar>
                                            <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" ForeColor="Gray" ReadOnly="True" Width="80%"
                                                 CssClass="input" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lblCuentaCorriente" runat="server"  Text="Cuenta corriente.." Width="80%"></asp:Label>
                                        </div>
                                        <div class="col-4">
                                            <asp:DropDownList ID="ddlCuentaCorriente" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..."  Width="100%" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                    </div>
                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2 text-left ">
                                            <asp:Label ID="lblIdTerceroEncabezado" runat="server"  Text="Tercero.." Width="80%"></asp:Label>
                                        </div>
                                        <div class="col-2">
                                            <asp:TextBox ID="txtIdTerceroEncabezado" runat="server" AutoPostBack="True" CssClass="input solonumero"  Width="80%" OnTextChanged="txtIdTerceroEncabezado_TextChanged"></asp:TextBox>
                                            <asp:LinkButton ID="btnTerceroEncabezado"  CssClass="btn btn-default btn-sm btn-primary fa fa-search" OnClientClick="return false;" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-4">
                                            <asp:TextBox ID="txtNombreTerceroEncabezado" runat="server" AutoPostBack="True" CssClass="input"  Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-1">
                                            <asp:HiddenField ID="hfidTercero" runat="server" />
                                            <asp:TextBox ID="txtidTercero" runat="server" AutoPostBack="True" CssClass="input"  Width="100%"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" ></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..."  Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lblFormaPago" runat="server" Text="Forma de pago" ></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..."  Width="100%">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lblNumeroInicial" runat="server" Text="Número de cheque/comprobante" ></asp:Label>
                                        </div>
                                        <div class="col-2">
                                            <asp:TextBox ID="txtNumeroInicial" runat="server" CssClass="input"  Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row text-left">
                                    </div>
                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lblTipoReferecia" runat="server" Text="Documento cruce" ></asp:Label>
                                        </div>
                                        <div class="col-1">
                                            <asp:TextBox ID="txtTipoReferecia" runat="server" CssClass="input"  Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-2">
                                            <asp:TextBox ID="txtNumeroReerencia" runat="server" CssClass="input"  Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-1">
                                            <asp:Label ID="lblCondicionPago" runat="server" Text="Cond. pago" ></asp:Label>
                                        </div>
                                        <div class="col-2">
                                            <asp:DropDownList ID="ddlCondicionPago" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..."  Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlCondicionPago_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2 text-left ">
                                            <asp:Label ID="lblBeneficiario" runat="server"  Text="Beneficiario.." Width="80%"></asp:Label>
                                        </div>
                                        <div class="col-2">
                                            <asp:TextBox ID="txtBeneficiario" runat="server" AutoPostBack="True" CssClass="input solonumero"  Width="80%" OnTextChanged="txtIdTerceroEncabezado_TextChanged"></asp:TextBox>
                                            <asp:LinkButton ID="btnBeneficiario"  CssClass="btn btn-default btn-sm btn-primary fa fa-search" OnClientClick="return false;" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-4">
                                            <asp:TextBox ID="txtNombreBeneficiario" runat="server" AutoPostBack="True" CssClass="input"  Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-1">
                                            <asp:HiddenField ID="hfBeneficiario" runat="server" />
                                            <asp:TextBox ID="txtidBeneficiario" runat="server" AutoPostBack="True" CssClass="input"  Width="100%"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row text-left">
                                        <div class="col-1">
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lblNota" runat="server" Text="Nota..." ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtNota" Height="60px" runat="server" CssClass="input" TextMode="MultiLine"  Width="100%" OnTextChanged="txtNota_TextChanged"></asp:TextBox>

                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowDeleting" />
                                    <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <div class="row text-left">
                                        <div class="col-1"></div>
                                        <div class="col-4">
                                            <h6>Cuentas por pagar relacionadas</h6>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row text-left">
                                        <div class="col-1"></div>
                                        <div class="col-10" style="max-height: 250px; overflow-y: scroll">
                                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Font-Size="Small" GridLines="None" Width="100%">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sel.">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSeleccion" runat="server" Checked="false" CssClass="checkbox checkbox-primary check" />
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="White" BorderColor="White" BorderStyle="None" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" Width="5px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                                                    <asp:BoundField DataField="numero" HeaderText="Número de causación"></asp:BoundField>
                                                    <asp:BoundField DataField="tipoReferencia" HeaderText="Tipo dcto cruce"></asp:BoundField>
                                                    <asp:BoundField DataField="numeroReferencia" HeaderText="Número cruce"></asp:BoundField>
                                                    <asp:BoundField DataField="fechaFactura" HeaderText="Fecha" DataFormatString="{0:d}"></asp:BoundField>
                                                    <asp:BoundField DataField="auxiliarcxp" HeaderText="AuxiliarCxP"></asp:BoundField>
                                                    <asp:BoundField DataField="nombreauxiliar" HeaderText="NombreAuxiliarCxP"></asp:BoundField>
                                                    <asp:BoundField DataField="diasVencimiento" HeaderText="Vence(días)"></asp:BoundField>
                                                    <asp:BoundField DataField="valorPagar" DataFormatString="{0:N2}" HeaderText="Saldo Actual"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="ValorPago">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValorUnitario" Enabled="false" runat="server" CssClass="input valorPago" onchange="totalizarCaja(this)" onkeyup="totalizarCaja(this)" Text='<%# Eval("saldo") %>' Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="valorPagar" DataFormatString="{0:N2}" HeaderText="Nuevo Saldo"></asp:BoundField>
                                                    <asp:BoundField DataField="valorFactura" DataFormatString="{0:N2}" HeaderText="Valor Factura"></asp:BoundField>
                                                    <asp:BoundField DataField="diasCorridos" HeaderText="días corrientes"></asp:BoundField>
                                                    <asp:BoundField DataField="notaDetalle" HeaderText="Notas"></asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="thead" />
                                                <PagerStyle CssClass="footer" />
                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-6">
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lblTotalPagar" runat="server" Text="$(+)Total a pagar" ></asp:Label>
                                        </div>
                                        <div class="col-2">
                                            <asp:HiddenField ID="hfTotalPagar" runat="server" />
                                            <asp:TextBox ID="txtTotalPagar" runat="server" CssClass="input" onkeyup="formato_numero(this)"  Width="100%">0</asp:TextBox>
                                        </div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="upConsulta" runat="server"  UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="padding: 5px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; border-color: silver; border-width: 1px;">
                                <div style="text-align: center">
                                    <div style="display: inline-block">
                                        <table style="width: 1000px;">
                                            <tr>
                                                <td style="width: 100px; text-align: left"></td>
                                                <td style="width: 150px; text-align: left">
                                                    <asp:DropDownList ID="niddlCampo" data-placeholder="Seleccione una opción..." runat="server" ToolTip="Selección de campo para busqueda"
                                                        Width="250px" CssClass="chzn-select-deselect">
                                                    </asp:DropDownList></td>
                                                <td style="width: 100px; text-align: left">
                                                    <asp:DropDownList ID="niddlOperador" data-placeholder="Seleccione una opción..." runat="server" ToolTip="Selección de operador para busqueda"
                                                        Width="150px" AutoPostBack="True" CssClass="chzn-select-deselect">
                                                        <asp:ListItem Value="like">Contiene</asp:ListItem>
                                                        <asp:ListItem Value="&lt;&gt;">Diferente</asp:ListItem>
                                                        <asp:ListItem Value="between">Entre</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="=">Igual</asp:ListItem>
                                                        <asp:ListItem Value="&gt;=">Mayor o Igual</asp:ListItem>
                                                        <asp:ListItem Value="&gt;">Mayor que</asp:ListItem>
                                                        <asp:ListItem Value="&lt;=">Menor o Igual</asp:ListItem>
                                                        <asp:ListItem Value="&lt;">Menor</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="width: 110px; text-align: left">
                                                    <asp:TextBox ID="nitxtValor1" runat="server" Width="200px" CssClass="input"></asp:TextBox><asp:TextBox
                                                        ID="nitxtValor2" runat="server"  Width="200px" CssClass="input"></asp:TextBox></td>
                                                <td style="width: 20px; text-align: center">
                                                    <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter" OnClick="niimbAdicionar_Click"></asp:LinkButton>

                                                </td>
                                                <td style="width: 20px; text-align: center">
                                                    <asp:LinkButton runat="server" ID="imbBusqueda"  CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="imbBusqueda_Click"></asp:LinkButton>
                                                </td>
                                                <td style="width: 100px; text-align: left">
                                                    <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label></td>
                                                <td style="background-position-x: right; width: 100px;"></td>
                                            </tr>
                                        </table>
                                        <table style="width: 1000px; padding: 0; border-collapse: collapse;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="nilblMensajeEdicion" runat="server" ForeColor="Navy"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="text-align: center">
                                                        <div style="display: inline-block">
                                                            <asp:GridView ID="gvParametros" runat="server" Width="400px" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" CssClass="table table-bordered table-sm  table-hover table-striped grillas">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                        <HeaderStyle CssClass="action-item" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="campo" HeaderText="Campo">
                                                                        <HeaderStyle HorizontalAlign="Left"  />
                                                                        <ItemStyle HorizontalAlign="Left"  />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="operador" HeaderText="Operador">
                                                                        <HeaderStyle HorizontalAlign="Center"  />
                                                                        <ItemStyle HorizontalAlign="Center"  />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="valor" HeaderText="Valor">
                                                                        <HeaderStyle HorizontalAlign="Left"  />
                                                                        <ItemStyle HorizontalAlign="Left"  />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="valor2" HeaderText="Valor 2">
                                                                        <HeaderStyle  HorizontalAlign="Left" />
                                                                        <ItemStyle  HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="thead" />
                                                                <PagerStyle CssClass="footer" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 1000px; padding: 0; border-collapse: collapse;">
                                            <tr>
                                                <td style="width: 1000px; text-align: left;">
                                                    <asp:GridView ID="gvTransaccion" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" CssClass="table table-bordered table-sm  table-hover table-striped grillas"  OnSelectedIndexChanged="gvTransaccion_SelectedIndexChanged">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <%--<asp:TemplateField HeaderText="Sel">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="imEdit" runat="server" CommandName="Select" CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt" ToolTip="Edita el registro seleccionado"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="action-item" Width="20px" />
                                                                <HeaderStyle CssClass="action-item" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Anu">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                <HeaderStyle CssClass="action-item" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                                                            <asp:BoundField DataField="numero" HeaderText="Numero"></asp:BoundField>
                                                            <asp:BoundField DataField="año" HeaderText="Año"></asp:BoundField>
                                                            <asp:BoundField DataField="mes" HeaderText="Mes"></asp:BoundField>
                                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}"></asp:BoundField>
                                                            <asp:BoundField DataField="notas" HeaderText="Observaciones"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Detalle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="detalle" CssClass="btn btn-default btn-sm btn-primary  fa fa-info-circle" CommandName="Update" ToolTip="Detalle de la transaccion"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CheckBoxField DataField="anulado" HeaderText="Anul"></asp:CheckBoxField>
                                                            <asp:CheckBoxField DataField="aprobado" HeaderText="Aprd"></asp:CheckBoxField>
                                                            <asp:TemplateField HeaderText="Imprimir">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="ibImprimir" CssClass="btn btn-default btn-sm btn-primary fa fa-print " CommandName="imprimir" ToolTip="Imprimir transacción seleccionada"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="thead" />
                                                        <PagerStyle CssClass="footer" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvTransaccion" EventName="RowUpdating" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
        <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
        <script src="http://app.infos.com/recursosinfos/lib/jquery-ui/jquery-ui.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/bootstrap/dist/js/bootstrap.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/moment/moment.js"></script>
        <script src="http://app.infos.com/RecursosInfos/js/daterangepicker.js" type="text/javascript"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/iCheck/icheck.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
        <script src="http://app.infos.com/RecursosInfos/lib/chosen187/chosen.jquery.js"></script>
        <script src="http://app.infos.com/RecursosInfos/js/core.js"></script>
        <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/plugins/localisation/jquery.localisation-min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/ui-multiselect/js/ui.multiselect.js"></script>

        <script type="text/javascript"> 
            $(document).ready(function () {
                $(".check").click(function () {
                    var chequeado;
                    var input = $(this).find("input:checkbox");
                    $(input).each(function () { chequeado = this.checked });
                    if (chequeado) {
                        var valorPagar = $($($(this).parent()).parent()).find("input:text");
                        $(valorPagar).removeAttr("disabled");
                        var filaVlorcxc = $($($(this).parent()).parent()).find("td:eq(9)");
                        var filaSaldo = $($($(this).parent()).parent()).find("td:eq(11)");
                        $(valorPagar).val($(filaVlorcxc).text());
                        $(filaSaldo).val("0");
                    }
                    else {
                        var valorPagar = $($($(this).parent()).parent()).find("input:text");
                        $(valorPagar).val("0");
                        $(valorPagar).attr("disabled", true);
                    }


                    totalizarPago();
                });

                $(".valorPago").keydown(function () {
                    var filaSaldo = $($($(this).parent()).parent()).find("td:eq(11)");
                    var filaVlorcxc = $($($(this).parent()).parent()).find("td:eq(9)");
                    var valornumericocxc = parseFloat($(filaVlorcxc).text().split(',').join(''));
                    var valorNumericoPago = parseFloat($(this).val().split(',').join(''));
                    if (valorNumericoPago > valornumericocxc)
                        $(this).val($(filaVlorcxc).text());

                    var resultado = (parseFloat($(filaVlorcxc).text().split(',').join('')) - parseFloat($(this).val().split(',').join(''))).toFixed(2);
                    index = resultado.toString().indexOf(".");
                    if (index > 0) {
                        var part1 = resultado.toString().split('.');
                        resultado = part1[0].split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '') + ("." + part1[1]);
                    }
                    else {
                        resultado = resultado.split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '');
                    }

                    console.log(resultado);
                    if (resultado.toString().indexOf("NaN") > 0) {
                        resultado = 0;
                    }

                    filaSaldo.text(resultado);
                    totalizarPago();
                });

                $('.solonumero').on('input', function () {
                    this.value = this.value.replace(/[^0-9]/g, '');
                });
                $("#imbBuscarCuenta").click(function () {
                    buscarCuenta();
                });
                function buscarCuenta() {
                    var width = 650;
                    var height = 400;
                    var left = (screen.width / 2) - (width / 2);
                    var top = (screen.height / 2) - (height / 2);
                    var cuenta = $("#txtCuenta").val();
                    z = window.open("BuscarCuentas.aspx?cuenta=" + cuenta, "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                    z.focus();
                }

                $("#btnTerceroEncabezado").click(function () {
                    terceroencabezado();
                });
                $("#terceroBeneficiario").click(function () {
                    terceroBeneficiario();
                });

                function totalizarPago() {

                    var gvLista = $("#gvLista tbody");
                    var total = 0;
                    var resultado = "0";
                    var totalResultado = $("#txtTotalPagar");
                    $(gvLista).each(function (e) {
                        $(this).find("input:text").each(function () {
                            resultado = (parseFloat(resultado.split(',').join('')) + parseFloat($(this).val().split(',').join(''))).toFixed(2);
                        });
                    });

                    index = resultado.toString().indexOf(".");

                    if (index > 0) {
                        var part1 = resultado.toString().split('.');
                        resultado = part1[0].split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '') + ("." + part1[1]);
                    }
                    else {
                        resultado = resultado.split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '');
                    }

                    console.log(resultado);
                    if (resultado.toString().indexOf("NaN") > 0) {
                        resultado = 0;
                    }

                    $(totalResultado).val(resultado);

                }
                function terceroencabezado() {
                    if ($("#txtFecha").val() == "") {
                        swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                        return;
                    }

                    var width = 650;
                    var height = 400;
                    var left = (screen.width / 2) - (width / 2);
                    var top = (screen.height / 2) - (height / 2);
                    var cuenta = $("#txtIdTerceroEncabezado").val();
                    z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=1", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                    z.focus();
                }

                function terceroBeneficiario() {
                    if ($("#txtFecha").val() == "") {
                        swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                        return;
                    }

                    var width = 650;
                    var height = 400;
                    var left = (screen.width / 2) - (width / 2);
                    var top = (screen.height / 2) - (height / 2);
                    var cuenta = $("#txtBeneficiario").val();
                    z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=2", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                    z.focus();
                }


                $("#imbBuscarTercero").click(function () {
                    var width = 650;
                    var height = 400;
                    var left = (screen.width / 2) - (width / 2);
                    var top = (screen.height / 2) - (height / 2);
                    var cuenta = $("#txtTercero").val();
                    z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=2", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                    z.focus();
                })

                $("#txvBase").keyup(function () {
                    var resultado = ((parseFloat($(this).val().split(',').join('')) * parseFloat($("#txvPorcentaje").val())) / 100).toFixed(2);
                    var index = 0;
                    index = resultado.toString().indexOf(".");
                    if (index > 0) {
                        var part1 = resultado.toString().split('.');
                        resultado = part1[0].split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '') + ("." + part1[1]);
                    }
                    else {
                        resultado = resultado.split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        resultado = resultado.split('').reverse().join('').replace(/^[\,]/, '');
                    }

                    console.log(resultado);
                    if (resultado.toString().indexOf("NaN") > 0) {
                        resultado = 0;
                    }

                    $("#hfTotalPagar").val(resultado);

                    $("#txvValor").val(resultado);
                });

            });


            function terceroBeneficiario() {
                if ($("#txtFecha").val() == "") {
                    swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                    return;
                }

                var width = 650;
                var height = 400;
                var left = (screen.width / 2) - (width / 2);
                var top = (screen.height / 2) - (height / 2);
                var cuenta = $("#txtBeneficiario").val();
                z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=2", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                z.focus();
            }


            function terceroencabezado() {
                if ($("#txtFecha").val() == "") {
                    swal('validacion de fecha', 'por favor ingrese la fecha primero', 'warning');
                    return;
                }

                var width = 650;
                var height = 400;
                var left = (screen.width / 2) - (width / 2);
                var top = (screen.height / 2) - (height / 2);
                var cuenta = $("#txtIdTerceroEncabezado").val();
                z = window.open("BuscarTerceros.aspx?tercero=" + cuenta + "&tipo=1", "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                z.focus();
            }


            function ActualizarTerceroEncabezado(codigo, nombre, id) {
                var controlCuenta = document.getElementById("txtIdTerceroEncabezado");
                var controlNombreCuenta = document.getElementById("txtNombreTerceroEncabezado");
                var controlid = document.getElementById("hfidTercero");
                if (controlCuenta != null) {
                    controlCuenta.value = codigo;
                    controlCuenta.focus();
                    controlNombreCuenta.value = nombre;
                    controlid.value = id;
                }
                console.log(controlid.value);
                __doPostBack('<%= txtIdTerceroEncabezado.ClientID  %>', '')
            }

            function ActualizarTerceroBeneficiario(codigo, nombre, id) {
                var controlCuenta = document.getElementById("txtBeneficiario");
                var controlNombreCuenta = document.getElementById("txtNombreBeneficiario");
                var controlid = document.getElementById("hfBeneficiario");
                var controlidtxt = document.getElementById("txtidBeneficiario");
                if (controlCuenta != null) {
                    controlCuenta.value = codigo;
                    controlCuenta.focus();
                    controlNombreCuenta.value = nombre;
                    controlid.value = id;
                    controlidtxt.value = id;
                }
                console.log(controlid.value);
            }


            function buscarCuenta() {
                var width = 650;
                var height = 400;
                var left = (screen.width / 2) - (width / 2);
                var top = (screen.height / 2) - (height / 2);
                var cuenta = $("#txtCuenta").val();
                z = window.open("BuscarCuentas.aspx?cuenta=" + cuenta, "popup", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + width + ", height=" + height + ", top=" + top + ", left=" + left);
                z.focus();
            }
        </script>
    </form>
</body>
</html>
