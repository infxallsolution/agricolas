<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Presupuesto.aspx.cs" Inherits="Contabilidad.WebForms.App_Code.General.Presupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>

    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <%-- repetido <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <%-- repetido <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>--%>
    <script src="http://app.infos.com/recursosinfos/js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/maxazan-jquery-treegrid-447d662/js/jquery.treegrid.js"></script>
    <link href="../../js/maxazan-jquery-treegrid-447d662/css/jquery.treegrid.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/moment/moment.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <%-- repetido <script src="http://app.infos.com/recursosinfos/js/chosen.jquery.js" type="text/javascript"></script>--%>
    <script src="http://app.infos.com/recursosinfos/lib/iCheck/icheck.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/sweetalert2/dist/sweetalert2.min.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/js/core.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

    </script>
    <style type="text/css">
        input {
            width: 85px;
        }

        tpresupuesto td {
            width: 80px;
        }
        .auto-style1 {
            text-align: left;
        }
    </style>

</head>

<body style="text-align: center">
    <div class="loading">
        <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
    </div>

    <div class="container">
        <form id="form1" runat="server">
              <table style="width: 100%" cellspacing="0">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">
                        <asp:Label ID="nillblAño" runat="server" Text="Año"></asp:Label></td>
                    <td style="width: 150px" class="auto-style1">
                        <asp:DropDownList ID="niddlAño" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="300px" AutoPostBack="True">
                        </asp:DropDownList>

                    </td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">No de cuenta</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div id="modalPresupuesto" class="modal modal-wide fade">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Asignar presupuesto</h4>
                        </div>
                        <h6>
                            <asp:Label ID="lblidCuentaContable" runat="server" Text=""></asp:Label>
                            -<asp:Label ID="lblCuentaContable" runat="server" Text=""></asp:Label></h6>
                        <hr />
                        <h6>
                            <asp:Label ID="Label4" runat="server" Text="Total Cuenta"></asp:Label>
                            <input id="txtTotalCuenta" runat="server" style="width: 200px;" disabled type="text" value="0" /></h6>
                        <div class="tablaGrilla">
                            <div class="modal-body">
                                <asp:DataList ID="dlCcosto" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfIndex" runat="server" Value="" />
                                        <div class="modal-header" style="text-align: left">
                                            <h7>
                                <asp:Label ID="lblidCcosto" runat="server" Text='<%# Eval("ccostoPresupuesto") %>'></asp:Label>
                                -<asp:Label ID="lblCcosto" runat="server" Text='<%# Eval("desccosto") %>'></asp:Label></h7>
                                        </div>
                                        <div style="text-align: left">
                                            <asp:Label ID="Label1" runat="server" Text="Periodo"></asp:Label>
                                            <asp:DropDownList ID="ddlPeriodo" runat="server" Width="100px">
                                                <asp:ListItem Text="Mensual" Value="1" />
                                                <asp:ListItem Text="Bimestral" Value="2" />
                                                <asp:ListItem Text="TrimesTral" Value="3" />
                                                <asp:ListItem Text="Semestral" Value="6" />
                                                <asp:ListItem Text="Anual" Value="12" />
                                            </asp:DropDownList>
                                            <asp:Label ID="Label2" runat="server" Text="Valor Total"></asp:Label>
                                            <input id="txtValorTotal" runat="server" type="text" onkeyup="sumarCampos(this)" value="0" />
                                        </div>
                                        <div style="padding: 5px">
                                            </h7>
                                            <table class="table table-bordered table-sm  table-hover table-striped grillas" id="tpresupuesto">
                                                <thea class="blue-grey lighten-4">
                                          
                                        </thea>
                                                <tbody>
                                                    <tr style="width: 100px">
                                                        <th scope="row">Enero</th>
                                                        <td>
                                                            <input id="txtEnero" runat="server" onkeyup="sumarValorTotal(this)" type="text" value='<%# Eval("enero") %>' /></td>
                                                        <th scope="row">Febrero</th>
                                                        <td>
                                                            <input id="txtFebrero" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("febrero") %>' /></td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Marzo</th>
                                                        <td>
                                                            <input id="txtMarzo" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("marzo") %>' /></td>
                                                        <th scope="row">Abril</th>
                                                        <td>
                                                            <input id="txtAbril" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("abril") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Mayo</th>
                                                        <td>
                                                            <input id="txtMayo" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("mayo") %>' /></td>

                                                        <th scope="row">Junio</th>
                                                        <td>
                                                            <input id="txtJunio" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("junio") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Julio</th>
                                                        <td>
                                                            <input id="txtJulio" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("julio") %>' /></td>
                                                        <th scope="row">Agosto</th>
                                                        <td>
                                                            <input id="txtAgosto" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("agosto") %>' />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Septiembre</th>
                                                        <td>
                                                            <input id="txtSeptiembre" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("septiembre") %>' /></td>

                                                        <th scope="row">Octubre</th>
                                                        <td>
                                                            <input id="txtOctubre" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("octubre") %>' /></td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Noviembre</th>
                                                        <td>
                                                            <input id="txtNoviembre" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("noviembre") %>' /></td>
                                                        <th scope="row">Diciembre</th>
                                                        <td>
                                                            <input id="txtDiciembre" runat="server" type="text" onkeyup="sumarValorTotal(this)" value='<%# Eval("diciembre") %>' /></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="True" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->
            <table width="100%">
                <tr>
                    <td></td>
                    <td style="text-align: left" width="150px">
                        </td>
                    <td style="text-align: left" width="250px">
                        </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvPresupuesto" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" PageSize="20" OnRowCommand="gvPresupuesto_RowCommand">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="Limpiar">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnLimpiar" runat="server" aria-hidden="true" CommandName="Limpiar" ForeColor="Red" CssClass="fa fa-eraser fa-2x" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de borrar los datos el registro?');" ToolTip="Limpiar presupuesto" />
                            </ItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Presupuestar">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPresupuesto" CommandName="addPresupuesto" ToolTip="Presupuestar" runat="server" CssClass="fa fa-money-bill-alt fa-2x" aria-hidden="true" />
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="idCuentaPresupuesto" HeaderText="id Cuenta" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="Items" HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="desCuenta" HeaderText="Nombre cuenta" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Presupuestado">
                            <ItemTemplate>
                                <i id="iChequeado" class="fa fa-check-square-o fa-2x" runat="server" aria-hidden="true" visible='<%# Eval("presupuestado") %>' style="color: green"></i>
                                <i id="iNoChequeado" class="fa fa-times fa-2x" runat="server" aria-hidden="true" visible='<%# !Convert.ToBoolean(Eval("presupuestado")) %>' style="color: red"></i>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtFecha').daterangepicker({ singleDatePicker: true }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tdCampos input[type="checkbox"]').each(function () {
                var self = $(this),
                  label = self.next(),
                  label_text = label.text();

                label.remove();
                self.iCheck({
                    checkboxClass: 'icheckbox_line-blue',
                    radioClass: 'iradio_line-blue',
                    insert: '<div class="icheck_line-icon"></div>' + label_text
                });
            });
            $('#txtCodigo').focus();
        });

        $('#txtNivel').keyup(function () {
            this.value = (this.value + '').replace(/[^0-9]/g, '');
        });

    </script>
    <script type="text/javascript" src="../../js/Forms/Presupuesto/jPresupuesto.js"></script>
</body>
</html>
