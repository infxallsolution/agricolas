<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiquidacionExcel.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.LiquidacionExcel" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/FileSaver.js"></script>
    <script type="text/javascript">
        $('document').ready(function () {
            $("#btnLiquidar").on('click', function (event) {
                event.preventDefault();
                var _type = "";
                var _company = $("#hfEmpresa").val();
                var _year = $("#ddlAño").chosen().val();
                var _month = $("#ddlMes").chosen().val();
                var _type = $("#ddlOpcionLiquidacion").chosen().val();
                var url = "http://app.infos.com/ExcelApi/api/Excel"
                var _costCenter = _type === "2" ? $("#ddlccosto").chosen().val() : "";
                var _majorCostCenter = _type === "4" ? $("#ddlccosto").chosen().val() : "";
                var _employed = _type === "3" ? $("#ddlEmpleado").chosen().val():"";
                console.log(_company, _year, _month)
                $(".loading").show();
                var data = JSON.stringify({
                    Type: _type, Company: _company, Year: _year, Month: _month,
                    costCenter: _costCenter, MajorCostCenter: _majorCostCenter,
                    Employed: _employed
                });

                var request = new XMLHttpRequest();
                request.open('POST', url, true);
                request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
                request.responseType = 'blob';


                request.onload = function (e) {
                    if (this.status === 200) {
                        var blob = this.response;
                        if (window.navigator.msSaveOrOpenBlob) {
                            window.navigator.msSaveBlob(blob, fileName);
                        }
                        else {
                            var downloadLink = window.document.createElement('a');
                            var contentTypeHeader = request.getResponseHeader("Content-Type");
                            downloadLink.href = window.URL.createObjectURL(new Blob([blob], { type: contentTypeHeader }));
                            downloadLink.download = "download.xlsx";
                            document.body.appendChild(downloadLink);
                            downloadLink.click();
                            document.body.removeChild(downloadLink);
                            $(".loading").hide();
                        }
                    }
                };
                request.send(data);

                //    $.ajax({
                //        type: "POST",
                //        data: data,
                //        url: url,
                //        contentType: "application/json; charset=utf-8",
                //    })
                //        .done(function (response, textStatus, jqXHR) {
                //            let url = window.URL.createObjectURL(new Blob([response]));
                //            let a = document.createElement('a');
                //            a.href = url;
                //            a.download = "dashboardleadreport.xlsx";
                //            document.body.appendChild(a);
                //            a.click();
                //            a.remove();
                //        })
                //        .fail(function (jqXHR, textStatus, errorThrown) {
                //            // do something here
                //        })

            });
        });

        function base64ToArrayBuffer(base64) {
            const binaryString = window.atob(base64);
            decodeURIComponent(escape(window.atob(b64)));
            const binaryLen = binaryString.length;
            const bytes = new Uint8Array(binaryLen);
            for (let i = 0; i < binaryLen; i++) {
                let ascii = binaryString.charCodeAt(i);
                bytes[i] = ascii;
            }
            return bytes;
        };

        function Visualizacion(informe) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }

        function VisualizacionLiquidacion(informe, ano, periodo, numero) {
            var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
            sTransaccion = "ImprimeInforme.aspx?informe=" + informe + "&ano=" + ano + "&periodo=" + periodo + "&numero=" + numero;
            x = window.open(sTransaccion, "", opciones);
            x.focus();
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 120px;
            height: 19px;
        }
        .auto-style2 {
            width: 400px;
            height: 19px;
        }
    </style>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; text-align: left;"></td>
                    <td style="width: 170px; text-align: left;">
                        </td>
                    <td style="width: 170px; text-align: left;">
                        </td>
                    <td style="width: 170px; text-align: left;">
                        <asp:Label ID="lblOpcionLiquidacion" runat="server" Text="Forma liquidación" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <div class="text-left">
                        <asp:DropDownList ID="ddlOpcionLiquidacion" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="200px" OnSelectedIndexChanged="ddlOpcionLiquidacion_SelectedIndexChanged">
                            <asp:ListItem Value="1">General</asp:ListItem>
                            <asp:ListItem Value="4">Por mayor centro costo</asp:ListItem>
                            <asp:ListItem Value="2">Por centro de costo</asp:ListItem>
                            <asp:ListItem Value="3">Por empleado</asp:ListItem>
                        </asp:DropDownList>
                        </div>
                        <asp:HiddenField ID="hfEmpresa" runat="server" />
                    </td>
                    <td style="width: 200px; text-align: left;"></td>
                </tr>
                <tr>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 120px; text-align: left;">
                        </td>
                    <td style="width: 120px; text-align: left;">
                        </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="200px" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 400px; text-align: left;"></td>
                </tr>
                <tr>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 120px; text-align: left;">
                        </td>
                    <td style="width: 120px; text-align: left;">
                        </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblaño0" runat="server" Text="Mes" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="200px" OnSelectedIndexChanged="ddlAño_SelectedIndexChanged1">
                            <asp:ListItem Value="1">Enero</asp:ListItem>
                            <asp:ListItem Value="2">Febrero</asp:ListItem>
                            <asp:ListItem Value="3">Marzo</asp:ListItem>
                            <asp:ListItem Value="4">Abril</asp:ListItem>
                            <asp:ListItem Value="5">Mayo</asp:ListItem>
                            <asp:ListItem Value="6">Junio</asp:ListItem>
                            <asp:ListItem Value="7">Julio</asp:ListItem>
                            <asp:ListItem Value="8">Agosto</asp:ListItem>
                            <asp:ListItem Value="9">Septiembre</asp:ListItem>
                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 400px; text-align: left;"></td>
                </tr>
                <tr>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 120px; text-align: left;">
                        </td>
                    <td style="width: 120px; text-align: left;">
                        </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblCcosto" runat="server" Text="Centro costo" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:DropDownList ID="ddlccosto" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." OnSelectedIndexChanged="ddlccosto_SelectedIndexChanged" Visible="False" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 400px; text-align: left;"></td>
                </tr>
                <tr>
                    <td style="text-align: left;" class="auto-style1"></td>
                    <td style="text-align: left;" class="auto-style1">
                    </td>
                    <td style="text-align: left;" class="auto-style1">
                    </td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:Label ID="lblEmpleado" runat="server" Text="Empleado" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;" class="auto-style2"></td>
                </tr>
                <tr>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 120px; text-align: left;"></td>
                    <td style="width: 400px; text-align: left;">
                        <asp:Button ID="btnLiquidar" runat="server" CssClass="botones" Text="Generar excel" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                    <td style="width: 400px; text-align: left;"></td>
                </tr>
            </table>
            <hr />

            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fas fa-times-circle " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero" HeaderText="Número" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noPeriodo" HeaderText="Periodo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Observacion" HeaderText="Observación" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="anulado" HeaderText="Anulado">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
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
