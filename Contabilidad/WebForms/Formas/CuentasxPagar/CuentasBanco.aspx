<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuentasBanco.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxPagar.CuentasBanco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
   
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
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación"  />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos"  />
                    </td>
                </tr>
            </table>
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <br />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px;">
                        <asp:Label ID="Label8" runat="server" Text="Código" ></asp:Label></td>
                    <td style="width: 500px; text-align: left">
                        <table class="w-100">
                            <tr>
                                <td style="text-align: left; width: 120px">
                        <asp:TextBox ID="txtCodigo" runat="server"  Width="100px" AutoPostBack="True" CssClass="input" OnTextChanged="txtCodigo_TextChanged" ></asp:TextBox>
                                </td>
                                <td>
                        <asp:CheckBox ID="chkActivo" runat="server" Text="Activo"  OnCheckedChanged="chkControlaChequera_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px;">
                        <asp:Label ID="Label9" runat="server" Text="Descripción" ></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDescripcion" runat="server"  Width="100%" CssClass="input" ></asp:TextBox>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px;">
                        <asp:Label ID="Label7" runat="server" Text="Banco" ></asp:Label></td>
                    <td style="width: 500px; text-align: left">
                        <asp:DropDownList ID="ddlBanco" runat="server"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged" style="left: 0px; top: 0px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px;">
                        <asp:Label ID="Label10" runat="server" Text="No. cuenta" ></asp:Label></td>
                    <td style="width: 500px; text-align: left">
                        <asp:TextBox ID="txtNocuenta" runat="server"  Width="100%" CssClass="input" ></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text="Auxiliar de cuenta" ></asp:Label></td>
                    <td style="width: 500px; text-align: left">
                        <asp:DropDownList ID="ddlCuenta" runat="server"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">
                        <asp:CheckBox ID="chkControlaChequera" runat="server" Text="Controla chequera"  AutoPostBack="True" OnCheckedChanged="chkControlaChequera_CheckedChanged" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">
                        <table class="w-100">
                            <tr>
                                <td style="text-align: left; width: 50px">
                        <asp:Label ID="Label11" runat="server" Text="Inicio" ></asp:Label></td>
                                <td>
                        <asp:TextBox ID="txvInicioCheque" runat="server"  Width="100%" CssClass="input" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                </td>
                                <td style="text-align: center; width: 50px">
                        <asp:Label ID="Label12" runat="server" Text="Final" ></asp:Label></td>
                                <td>
                        <asp:TextBox ID="txvFinalCheque" runat="server"  Width="100%" CssClass="input" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">
                        <asp:CheckBox ID="chkControlaConsecutivo" runat="server" Text="Controla concecutivo"  AutoPostBack="True" OnCheckedChanged="chkControlaConsecutivo_CheckedChanged" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left">
                        <asp:Label ID="Label15" runat="server" Text="No cheque actual" ></asp:Label>
                    </td>
                    <td style="width: 500px; text-align: left">
                        <asp:TextBox ID="txvNumeroCheque" runat="server"  Width="100%" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">
                        <asp:CheckBox ID="chkPagoElectronico" runat="server" Text="Pago electronico"  AutoPostBack="True" OnCheckedChanged="chkPagoElectronico_CheckedChanged" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" class="auto-style1">
                        <asp:Label ID="Label14" runat="server" Text="Tercero" ></asp:Label>
                    </td>
                    <td style="width: 500px; text-align: left">
                        <asp:DropDownList ID="ddlTercero" runat="server"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left" class="auto-style1">
                        <asp:Label ID="Label13" runat="server" Text="Formato" ></asp:Label>
                    </td>
                    <td style="width: 500px; text-align: left">
                        <asp:DropDownList ID="ddlFormato" runat="server"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                </table>
            <div class="tablaGrilla">
               <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
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
                        <asp:BoundField DataField="codigo" HeaderText="Código" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="banco" HeaderText="Banco" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="nombreBanco" HeaderText="NombreBanco" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                         <asp:BoundField DataField="cuenta" HeaderText="NoCuenta" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="auxiliar" HeaderText="Auxiliar" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                         <asp:CheckBoxField DataField="controlaChequera" HeaderText="CCheque">
                            <ItemStyle  HorizontalAlign="Left"  Width="10px"/>
                        </asp:CheckBoxField>
                         <asp:BoundField DataField="inicial" HeaderText="Inicio" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="final" HeaderText="Final" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                         <asp:CheckBoxField DataField="controlaConsecutivo" HeaderText="CConstvo">
                            <ItemStyle  HorizontalAlign="Left"  Width="10px"/>
                        </asp:CheckBoxField>
                         <asp:BoundField DataField="numeroActual" HeaderText="NoChequeAct" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="pagoElectronico" HeaderText="PElectr">
                            <ItemStyle  HorizontalAlign="Left"  Width="10px"/>
                        </asp:CheckBoxField>
                         <asp:BoundField DataField="tercero" HeaderText="Tercero" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreTercero" HeaderText="NombreTercero" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                         <asp:BoundField DataField="formato" HeaderText="Fomato" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                         <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle  HorizontalAlign="Left"  Width="10px"/>
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
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


     <script type="text/jscript">
        function soloNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "1234567890";
            especiales = "";

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
        function soloLetrasNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "1234567890abcdefghijklmnopqrstuvwxyz";
            especiales = "";

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
        function soloLetras(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "abcdefghijklmnopqrstuvwxyz";
            especiales = "";

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
     </script>

</body>
</html>
