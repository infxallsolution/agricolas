<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Programacion.aspx.cs" Inherits="Nomina.WebForms.Formas.Pprogramacion.Programacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <style>
        .tooltipChk {
            position: relative;
            display: inline-block;
            /*border-bottom: 1px dotted black;*/
        }

        .novedad {
            border-color: red;
            border-width: 2px;
            border-bottom-style: solid;
        }

        .tooltipChk .tooltiptext {
            visibility: hidden;
            width: 120px;
            background-color: #555;
            color: #fff;
            text-align: center;
            border-radius: 6px;
            padding: 5px 0;
            position: absolute;
            z-index: 1;
            bottom: 125%;
            left: 50%;
            margin-left: -60px;
            opacity: 0;
            transition: opacity 0.3s;
        }

            .tooltipChk .tooltiptext::after {
                content: "";
                position: absolute;
                top: 100%;
                left: 50%;
                margin-left: -5px;
                border-width: 5px;
                border-style: solid;
                border-color: #555 transparent transparent transparent;
            }

        .tooltipChk:hover .tooltiptext {
            visibility: visible;
            opacity: 1;
        }
    </style>
       <script type="text/javascript">
        var x = null;
        var y = null;
        var z = null;
        function Visualizacion2(informe) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width = 1300, height = 800, top = 0, left = 5";
            sTransaccion = "../pInformes/ImprimeInforme.aspx?informe=" + informe;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function AutorizaPermiso(funcionario, nombre, turno) {
            sTransaccion = "AutorizaPermiso.aspx?funcionario=" + funcionario + "&nombre=" + nombre + "&turno=" + turno;
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width = 600, height = 600, top = 0, left = 20";
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }
        function Programacion() {
            sTransaccion = "ImprimeProgramacion.aspx";
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width = 1000, height = 600, top = 0, left = 20";
            y = window.open(sTransaccion, "", opciones);
            y.focus();
        }
        function Registro() {
            sTransaccion = "ImprimeRegistro.aspx";
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width = 1000, height = 600, top = 0, left = 20";
            z = window.open(sTransaccion, "", opciones);
            z.focus();
        }

    </script>

</head>
<body>
   
    <div class="container">
         <div class="loading">
        <i class="fas fa-spinner fa-spin fa-5x"></i>
    </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="width: 500px; height: 25px; text-align: center"><strong>Programación de Funcionarios</strong></td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%;" id="tdCampos">
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td></td>
                                <td style="text-align: center; width: 250px;">
                                    <asp:LinkButton ID="imbCuadrilla" runat="server" Height="35px" CssClass=" fa fa-address-book fa-3x"
                                        OnClick="imbCuadrilla_Click" ToolTip="Clic para la administración de las cuadrillas"
                                        Width="35px" />
                                </td>
                                <td style="text-align: center; width: 250px;">
                                    <asp:LinkButton ID="imbTurnos" runat="server"
                                        Height="35px" CssClass="fa fa-calendar fa-3x" ToolTip="Clic para la administración de turnos"
                                        Width="35px" OnClick="imbTurnos_Click" />
                                </td>
                                <td style="text-align: center; width: 250px;">
                                    <asp:LinkButton ID="imbCuadrilla0" runat="server" Height="35px" CssClass="fa fa-safari fa-3x"
                                        OnClick="imbCuadrilla0_Click" ToolTip="Clic para la autorizar horas extras adicionales"
                                        Width="35px" />
                                </td>
                                <td style="text-align: center; width: 250px;">
                                    <asp:LinkButton ID="imbInformeProgramacion" runat="server"
                                        Height="35px" CssClass="fa fa-tasks fa-3x" ToolTip="Clic para ver el informe de programación por fecha"
                                        Width="35px" OnClick="imbInformeProgramacion_Click" />
                                </td>
                                <td style="text-align: center; width: 250px;">
                                    <asp:LinkButton ID="imbInformeEntradas" runat="server"
                                        Height="35px" CssClass="fa fa-file fa-3x" ToolTip="Clic para ver el informe de registro en portería"
                                        Width="35px" OnClick="imbInformeEntradas_Click" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: center; width: 250px;">Administrar Cuadrillas</td>
                                <td style="text-align: center; width: 250px;">Administrar Turnos</td>
                                <td style="text-align: center; width: 250px;">Horas Extras Adicionales</td>
                                <td style="text-align: center; width: 250px;">Informe Programación</td>
                                <td style="text-align: center; width: 250px;">Informe Portería</td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="width: 190px; text-align: left">
                        <asp:Label ID="Label2" runat="server">Programación para la semana</asp:Label></td>
                    <td style="width: 130px; text-align: left">
                        <asp:TextBox ID="txtFecha" runat="server" Font-Bold="True" Width="120px" CssClass="fecha input" AutoPostBack="True" autocomplete="off" OnTextChanged="txtFecha_TextChanged"></asp:TextBox></td>
                    <td style="width: 60px; text-align: left">
                        <asp:Label ID="Label3" runat="server">Turnos</asp:Label></td>
                    <td style="width: 310px; text-align: left">
                        <asp:DropDownList ID="niddlTurno" runat="server" Width="300px" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="niddlTurno_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td style="width: 70px; text-align: left">
                        <asp:Label ID="Label1" runat="server">Cuadrillas</asp:Label></td>
                    <td style="width: 300px; text-align: left;">
                        <asp:DropDownList ID="ddlCuadrilla" runat="server" Width="300px" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlCuadrilla_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="width: 500px; text-align: center;">
                        <asp:Button ID="lbRefresca" runat="server" CssClass="botones" OnClick="lbRefresca_Click" Text="Refrescar" ToolTip="Actualiza grilla" />
                        <asp:Button ID="lbAsignar" runat="server" CssClass="botones" OnClick="btnAsignar_Click" Text="Asignar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                        <asp:Button ID="lbRegistrarExtras" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Guardar horas extras" ToolTip="Cancela la operación" Visible="False" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <table style="width: 100%" id="ChkGrilla">
              
              
                <tr>
                    <td style="text-align: left">
                        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:BoundField DataField="funcionario" HeaderText="Identificación">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Nombre">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cargo" HeaderText="Cargo">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Semana">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAsignacion" runat="server" CssClass="chkAllWeek" />
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lun">
                                    <HeaderStyle
                                        HorizontalAlign="Center" BorderColor="White" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <div class="tooltipChk" runat="server" id="divday1">
                                            <asp:CheckBox ID="chkLun" runat="server" CssClass="check" />
                                            <asp:Label ID="lblNov1" class="tooltiptext" runat="server" Text=""></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkLunT" runat="server" Text="Lun" CssClass="checkday" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar">
                                    <FooterStyle BackColor="White" />
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <div class="tooltipChk" runat="server" id="divday2">
                                            <asp:CheckBox ID="chkMar" runat="server" CssClass="check" />
                                            <asp:Label ID="lblNov2" class="tooltiptext" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkMarT" runat="server" Text="Mar" CssClass="checkday" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mie">
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <div class="tooltipChk" runat="server" id="divday3">
                                            <asp:CheckBox ID="chkMie" runat="server" CssClass="check" />
                                            <asp:Label ID="lblNov3" class="tooltiptext" runat="server" Text=""></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkMieT" runat="server" Text="Mie" CssClass="checkday" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jue">
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <div class="tooltipChk" runat="server" id="divday4">
                                            <asp:CheckBox ID="chkJue" runat="server" CssClass="check" />
                                            <asp:Label ID="lblNov4" class="tooltiptext" runat="server" Text=""></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkJueT" runat="server" Text="Jue" CssClass="checkday" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vie">
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <div class="tooltipChk" runat="server" id="divday5">
                                            <asp:CheckBox ID="chkVie" runat="server" CssClass="check" />
                                            <asp:Label ID="lblNov5" class="tooltiptext" runat="server" Text=""></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkVieT" runat="server" Text="Vie" CssClass="checkday" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sab">
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>

                                        <div class="tooltipChk" runat="server" id="divday6">
                                            <asp:CheckBox ID="chkSab" runat="server" CssClass="check" />
                                            <asp:Label ID="lblNov6" class="tooltiptext" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSabT" runat="server" Text="Sab" CssClass="checkday" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dom">

                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <div class="tooltipChk" runat="server" id="divday0">
                                            <asp:CheckBox ID="chkDom" runat="server" CssClass="check" />
                                            <asp:Label ID="lblNov0" class="tooltiptext" runat="server" Text=""></asp:Label>
                                        </div>

                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkDomT" runat="server" Text="Dom" CssClass="checkday" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </td>
                </tr>

                  <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtObservacionProgramacion" placeholder="Observación y/o notas de la programación..." runat="server" Height="50px" TextMode="MultiLine" Visible="False" Width="90%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:GridView ID="gvExtras" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="funcionario" HeaderText="Identificación">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Nombre">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cargo" HeaderText="Cargo">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Lun">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasLun" runat="server" Visible="False" onkeyup="formato_numero(this)" CssClass="input" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasMar" runat="server" Visible="False" onkeyup="formato_numero(this)" CssClass="input" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mie">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasMie" runat="server" Visible="False" onkeyup="formato_numero(this)" CssClass="input" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasJue" runat="server" Visible="False" onkeyup="formato_numero(this)" CssClass="input" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vie">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasVie" runat="server" Visible="False" onkeyup="formato_numero(this)" CssClass="input" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sab">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasSab" runat="server" Visible="False" onkeyup="formato_numero(this)" CssClass="input" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dom">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasDom" runat="server" Visible="False" onkeyup="formato_numero(this)" CssClass="input" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle
                                        HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtObservacionAdicion" placeholder="Observación y/o notas de horas extras" runat="server" Height="50px" TextMode="MultiLine" Visible="False" Width="90%"></asp:TextBox>
                    </td>
                </tr>
            </table>
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
      
    <script type="text/javascript">

        $(document).ready(function () {
            $(".chkAllWeek").click(function () {

                var input = $(this).find("input[type='checkbox']");
                var chequeado;
                $(input).each(function () { chequeado = this.checked; });
                var padre = $($(this).parent()).parent();

                $(padre).children().each(function (k, v) {
                    $(this).children().each(function (k, v) {
                        $(this).children().each(function (k, v) {
                            var span = $(this).children("span")[0];
                            if (!$(this).children("input:checkbox").prop("disabled"))
                                $(this).children("input:checkbox").each(function () { this.checked = chequeado; });
                        });
                    });
                });
                reccorerChecks();
            });

            $(".checkday").click(function () {
                var input = $(this).find("input");
                var chequeado;
                $(input).each(function () { chequeado = this.checked });
                var index = $($(this).parent()).index();
                var tabla = $("#gvLista tbody");


                $(tabla).children().each(function () {
                    $(this).children().each(function (v) {
                        if (index === v) {
                            var input = $(this).find("input:checkbox");
                            if (!$(this).find("input:checkbox").prop("disabled"))
                                $(input).each(function () { this.checked = chequeado });
                        }
                    });
                });

                reccorerChecks();

            });

            function reccorerChecks() {
                var tabla = $("#gvLista tbody");
                var chequeado;
                var verdadero = true;
                var falso = false;
                var bverdadero = 0;
                var bfalso = 0;
                var contador = 0;
                var columna = 0;
                var fila = 0;

                //recorriendo filas
                $(tabla).children().each(function () {
                    $(this).children().each(function (cell) {
                        if (cell > 3) {
                            var input = $(this).find("input[type='checkbox']");
                            $(input).each(function () { chequeado = this.checked });
                            contador++;
                            if (chequeado)
                                bverdadero++;

                        }
                    });

                    if (contador === bverdadero) {
                        $(this).find("td:eq(3) input:checkbox").each(function () { this.checked = true });
                        contador = 0;
                        bverdadero = 0;
                    }
                    else {
                        $(this).find("td:eq(3) input:checkbox").each(function () { this.checked = false });
                        contador = 0;
                        bverdadero = 0;
                    }
                });
                recorrerfilas(0);

            }

            function recorrerfilas(columna) {
                var tabla = $("#gvLista tbody");
                var bverdadero = 0;
                var contador = 0;
                var chequeado = false;

                $(tabla).children().each(function (row) {

                    $(this).children().each(function (cell) {
                        if (cell > 3 & row > 0) {
                            if (columna === 0) {
                                columna = cell;
                            }
                            if (cell === columna) {
                                var input = $(this).find("input:checkbox");
                                $(input).each(function () { chequeado = this.checked });
                                if (chequeado) {
                                    bverdadero++;
                                }
                                contador++;
                            }

                        }
                    });

                    if (row === $(tabla).children().length - 1) {

                        if (bverdadero === contador) {
                            $("#gvLista tbody th:eq(" + columna + ") input:checkbox").each(function () { this.checked = true });
                            bverdadero = 0;
                            contador = 0;
                        }
                        else {
                            $("#gvLista tbody th:eq(" + columna + ") input:checkbox").each(function () { this.checked = false });
                            contador = 0;
                            bverdadero = 0;
                        }
                        columna++;
                        if (columna <= 10) {
                            recorrerfilas(columna);
                        }
                        else {
                            return false;
                        }
                    }

                });
            }

            $(".check").click(function () {
                var input = $(this).find("input:checkbox");
                var chequeado, bandera = false;
                $(input).each(function () { chequeado = this.checked });
                var cell = $($(this).parent()).index();
                var row = $($($(this).parent()).parent()).index();


                var tabla = $("#gvLista tbody");
                if (!chequeado) {
                    console.log("columna - " + cell);
                    console.log("fila - " + row);
                    $($($($(this).parent()).parent().parent()).find("td:eq(3) input")).each(function () { this.checked = false });
                    $("#gvLista tbody th:eq(" + row + ") input").each(function () { this.checked = false });
                }
                else {
                    $($($($(this).parent()).parent().parent()).find("td").each(
                        function () {
                            if ($(this).index() > 3) {
                                var inputs = $(this).find("input:checkbox");
                                $(inputs).each(function () {
                                    bandera = this.checked;
                                });

                                if (!bandera)
                                    return false;
                            }
                        }
                    ));

                    if (bandera) {
                        $($($($(this).parent().parent()).parent()).find("td:eq(3) input:checkbox")).each(function () { this.checked = true });
                    }

                    bandera = false;

                    var trs = $(tabla);

                    $(trs).each(function () {
                        $($(this).find("td")).each(function () {
                            if ($(this).index() === row) {
                                $($(this).find("input:checkbox")).each(function () {
                                    bandera = this.checked;
                                });
                                if (!bandera)
                                    return false;
                            }

                        });
                        if (!bandera)
                            return false;
                    });

                    if (bandera) {
                        $("#gvLista tbody th:eq(" + row + ") input:checkbox").each(function () { this.checked = true });
                    }

                }

            });

        });
    </script>
 

</body>
</html>
