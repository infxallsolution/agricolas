<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConteoFisico.aspx.cs" Inherits="Almacen.WebForms.Formas.Ptransaccion.ConteoFisico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body>

    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="nilbContar" runat="server" CssClass="botones" Text="Contar" ToolTip="Conteo" Visible="True" OnClick="nilbContar_Click" />
                        <asp:Button ID="niCerrar" runat="server" CssClass="botones" OnClick="lbCerrar_Click" Text="Cerrar" ToolTip="Cerrar Conteo" Visible="True" />
                    </td>
                </tr>
            </table>

            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label5" runat="server" Text="Código" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:HiddenField ID="hfConteo" runat="server" />
                        <asp:HiddenField ID="hfEmpresa" runat="server" />
                        <asp:HiddenField ID="hfUsuario" runat="server" />
                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged" Visible="False" Width="99%" CssClass="input" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label14" runat="server" Text="Bodega" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlBodega" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lblNoConteo" runat="server" Text="#Conteos" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtConteos" runat="server" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged" Visible="False" Width="50%" CssClass="input"></asp:TextBox>
                        <asp:Label ID="lblLeyendaConteo" runat="server" Text="(min 1, max 3)" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left">
                        <asp:Label ID="Label13" runat="server" Text="Tipo de conteo" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkFisico" runat="server" Text="Fisico" Visible="False" Checked="True" />
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCiclico" runat="server" Text="Ciclico" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="Label12" runat="server" Text="Observación" Visible="False"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtObservacion" runat="server" Visible="False" Width="100%" CssClass="input" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px">
                        <asp:Label ID="lblInventario" runat="server" Text="# Inventario" Visible="False"></asp:Label>
                    </td>
                    <td colspan="3" class="text-left">
                        <asp:DropDownList ID="ddlInventario" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 130px" class="text-left">
                        <asp:CheckBox ID="chkCriterio" Visible="false" runat="server" Text="Por Criterio" AutoPostBack="True" OnCheckedChanged="chkCriterio_CheckedChanged" />
                    </td>
                    <td colspan="1" class="text-left">
                        <asp:DropDownList ID="ddlCriterio" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False" Width="99%" AutoPostBack="True" OnSelectedIndexChanged="ddlCriterio_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="text-left">
                        <asp:Label ID="lblMayor" runat="server" Text="Mayor" Visible="False"></asp:Label>
                    </td>
                    <td class="text-left">
                        <asp:DropDownList ID="ddlMayor" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False" Width="100%" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="4">
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Procesar conteo" ToolTip="Procesar conteo" Visible="False" />
                        <asp:Button ID="lbEmpezarConteo" runat="server" CssClass="botones" OnClick="lbEmpezarConteo_Click" Text="Empezar conteo" ToolTip="Empezar Conteo" Visible="False" />
                        <asp:Button ID="lbAnular" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Anular" ToolTip="Haga clic aqui para realizar la busqueda" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 130px"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <div class="row">
                <div class="col-12">
                    <div class="row text-center">
                        <div class="col-12">
                            <asp:Label ID="lblConteo" Visible="false" CssClass="alert alert-info" role="alert" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblItemsContados" Visible="false" CssClass="alert alert-info" role="alert" runat="server" Text=""></asp:Label>
                            <button type="button" id="btnGuardarItems" runat="server" class="btn-sm btn-primary save" visible="False">Guardar Conteo</button>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="tablaGrilla">

                <div>
                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvLista_DataBound" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnSelectedIndexChanged="gvLista_SelectedIndexChanged">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:BoundField DataField="item" HeaderText="Item" ReadOnly="True"></asp:BoundField>
                            <asp:BoundField DataField="nombreItem" HeaderText="Nombre" ReadOnly="True"
                                SortExpression="descripcion">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="uMedida" HeaderText="uMedida">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Conteo">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConteo" runat="server" Text="0"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contado">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkConteo1" disabled> </input>
                                    <itemstyle width="20px" horizontalalign="Center" cssclass="action-item" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="footer" />
                    </asp:GridView>
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
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.15/js/dataTables.bootstrap4.min.js"></script>
    <script>
        $(document).ready(function () {
            var myVar = setInterval(ConsultarConteo, 1800);
            var table;
            $(".save").click(function (e) {
                var parametros = Array();
                $.each($("#gvLista tr"), function (i, v) {
                    if (i > 0) {
                        var consecutivo = $("#ddlInventario").chosen().val();
                        var noConteo = $("#txtConteos").val();
                        var conteo = $(this).find("td:eq(3) input").val()
                        var empresa = parseInt($("#hfEmpresa").val());
                        var usuario = $("#hfUsuario").val();
                        var item = $(this).find("td:eq(0)").text();
                        var i = { item: item, consecutivo: consecutivo, conteo: conteo, noConteo: noConteo, empresa: empresa, usuario: usuario };
                        parametros.push(i);
                    }
                });

                console.log(JSON.stringify(parametros));
                llamarAjax(parametros);
                e.preventDefault();

            });

            function ConsultarConteo() {

                if ($("#gvLista").DataTable().rows().count() > 0) {

                    var item = "";
                    var consecutivo = $("#ddlInventario").chosen().val();
                    var noConteo = parseInt($("#txtConteos").val());
                    var conteo = 0;
                    var empresa = parseInt($("#hfEmpresa").val());
                    var usuario = $("#hfUsuario").val();
                    var parametros = { item: item, consecutivo: consecutivo, conteo: conteo, noConteo: noConteo, empresa: empresa, usuario: usuario };
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify(parametros),
                        url: "ConteoFisico.aspx/ConsultarConteo",
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var itemsInventario;
                            var itemsContadosLocal = 0;
                            var ultimoConteoLocal = 0;
                            var ultimoConteoGeneral = 0;
                            $.each($("#gvLista").DataTable().rows().nodes(), function (i, v) {
                                var tr = this;

                                if (data.d != "") {
                                    $.each(JSON.parse(data.d), function (e, i) {
                                        itemsInventario = this.itemsInventario;
                                        if ($(tr).find("td:eq(0)").text() == this.item) {

                                            if (!$(tr).find("td:eq(3) input").is(":focus")) {
                                                $(tr).find("td:eq(3) input").val(this.conteo);
                                            }

                                            $(tr).find("td:eq(4) input").prop("checked", true);

                                            itemsContadosLocal++;
                                        }
                                    })
                                }
                            });
                            var itemcount = 0;
                            if (ultimoConteoGeneral != itemsInventario) {
                                ultimoConteoGeneral = itemsInventario;
                                $("#lblItemsContados").text("Inventario General: Items Contados " + itemcount + " de " + itemsInventario);
                            }
                            if (ultimoConteoLocal != itemsContadosLocal) {
                                ultimoConteoLocal = itemsContadosLocal;
                                if (data.d != "") itemcount = JSON.parse(data.d).length;
                                $("#lblConteo").text("Inventario Seleccionado: Items Contados " + itemsContadosLocal + " de " + $("#gvLista").DataTable().rows().count());
                            }
                        },
                        error: (e) => { console.log("ERROR"); }
                    });
                }
            }

            function llamarAjax(parametros) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify({ conteos: parametros }),
                    url: "ConteoFisico.aspx/GuardarConteo",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        swal("Bien hecho!", "Registros guardados!", "success");
                    },
                    error: function () {

                    }
                });
            }



            $("#gvLista").DataTable({
                language: {
                    "emptyTable": "No hay datos para mostrar",
                    "info": "Mostrando _START_ de _END_ de _TOTAL_ registros totales",
                    "infoEmpty": "Mostrando 0 de 0 de 0 registros totales",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "lengthMenu": "Mostrar _MENU_ registros",
                    "loadingRecords": "Cargando...",
                    "processing": "Provesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Ningun registro encontrado",
                    "aria": {
                        "sortAscending": ": activar para ordernar la columna ascendente",
                        "sortDescending": ": activar para ordernar la columna descendente"
                    }
                },
                "order": [[0, "desc"]],
                "paging": true,
                "destroy": true,
                "pageLength": 20
            });
        });
    </script>
</body>
</html>
