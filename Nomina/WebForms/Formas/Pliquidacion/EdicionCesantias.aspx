<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EdicionCesantias.aspx.cs" Inherits="Nomina.WebForms.Formas.Pliquidacion.EdicionCesantias" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
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
        function alerta(mensaje) {
            alert(mensaje);
        }
    </script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input">
                        </asp:TextBox>
                    </td>
                    <td class="bordesBusqueda"></td>
                </tr>
            </table>
            <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
            <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
            <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 130px; text-align: left;"></td>
                    <td style="text-align: left; width: 600px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblaño" runat="server" Text="Año Cesantias" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAñoDesde" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlAñoDesde_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblOpcionLiquidacion" runat="server" Text="Docto Cesantias" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlDoctoCesantia" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="97%" OnSelectedIndexChanged="ddlDoctoCesantia_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="lblOpcionLiquidacion0" runat="server" Text="Filtro empleado" Visible="False"></asp:Label>
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:HiddenField ID="hfEmpleadosSeleccionados" runat="server" />
                        <asp:DropDownList ID="ddlEmpleado" runat="server" multiple CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="75%" OnSelectedIndexChanged="ddlDoctoCesantia_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="lbAplicar" runat="server" CssClass="botones" OnClick="lbAplicar_Click" Text="Aplicar" ToolTip="Cancela la operación" Visible="False" Width="20%" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 120px; text-align: left;">
                        
                        <asp:Label ID="lblOpcionLiquidacion1" runat="server" Text="Empleado" Visible="False"></asp:Label>
                        
                    </td>
                    <td class="Campos" style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlEmpleadoAdd" runat="server"  CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="75%" OnSelectedIndexChanged="ddlDoctoCesantia_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="lbAdicionar" runat="server" CssClass="botones" Text="Agregar funcionario" ToolTip="Cancela la operación" Visible="False" OnClick="lbAdicionar_Click" Width="30%" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />
            <asp:GridView ID="gvCesantias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvCesantias_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                <asp:HiddenField ID="hfNoMostrar" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                    <asp:BoundField DataField="identificacionTercero" HeaderText="Identificación" />
                    <asp:BoundField DataField="nombreTercero" HeaderText="Empleado" />
                    <asp:TemplateField HeaderText="Valor Pro.">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtValorPromedio" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Convert.ToDecimal( Eval("valorPromedio")).ToString("N") %>' Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Base">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hfTercero" Value='<%# Eval("tercero") %>' />
                            <asp:TextBox ID="txtBase" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Convert.ToDecimal(Eval("base")).ToString("N") %>' Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Días Pro.">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiasPro" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Convert.ToInt32( Eval("diasPromedio")) %>' Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Días Cesa.">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiasCesa" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Convert.ToInt32( Eval("diasCesantia")) %>' Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="$ Cesantias">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtValorCesantias" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Convert.ToDecimal( Eval("valorCesantia")).ToString("N") %>' Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="$ Intereses">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtValorIntereses" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Convert.ToDecimal( Eval("valorInteresCesantia")).ToString("N") %>' Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
                <HeaderStyle CssClass="thead" />
                <PagerStyle CssClass="footer" />
            </asp:GridView>
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
                        <asp:BoundField DataField="periodo" HeaderText="Periodo" ReadOnly="True">
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
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/js/Liquidación/ModificacionPrima.js?v=20170710"></script>
    <script type="text/javascript">
        let ddlempleado = $("#ddlEmpleado");
        let hfEmpleado = $("#hfEmpleadosSeleccionados");

        $(document).ready(function () {

            console.log(hfEmpleado.val());
            if (hfEmpleado.val() != "") {
                console.log("entro")
                ddlempleado.val(hfEmpleado.val().split(","));
                ddlempleado.trigger("chosen:updated")
            }

            ddlempleado.chosen().change(function () {
                hfEmpleado.val(ddlempleado.chosen().val());
            });

        });

    </script>

</body>
</html>
