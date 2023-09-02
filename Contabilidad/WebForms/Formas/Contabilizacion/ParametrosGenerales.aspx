<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametrosGenerales.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Contabilizacion.ParametrosGenerales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
    <link href="../../css/Formularios.css" rel="stylesheet" />
    <script src="../../js/Numero.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <link href="http://app.infos.com/RecursosInfos/css/chosen.css" rel="stylesheet" />
    <link href="http://app.infos.com/RecursosInfos/lib/chosen-js-1.8.7/chosen.min.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 10%"></td>
                    <td>Busqueda
                        <asp:TextBox ID="nitxtBusqueda" runat="server" Width="80%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                    <td style="width: 10%"></td>
                </tr>
                <tr>
                    <td style="width: 10%"></td>
                    <td style="text-align: center">
                        <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" />
                        <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" ToolTip="Cancela la operación" OnClick="lbCancelar_Click"
                            onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" Visible="False" />

                        <asp:ImageButton ID="lbRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" ToolTip="Guarda el nuevo registro en la base de datos"
                            onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'"
                            onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" OnClick="lbRegistrar_Click" Visible="False" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" />
                    </td>
                    <td style="width: 10%"></td>
                </tr>
            </table>
            <table cellspacing="0" style="width: 100%; text-align: center; border-top: silver thin solid; border-bottom: silver thin solid;" id="TABLE1">
                <tr>
                    <td style="width: 20px;" width="30px"></td>
                    <td  colspan="4">
                        <asp:Label ID="nilbMensaje" runat="server"></asp:Label>
                        <br />
                    </td>
                    <td style="width: 20px;"></td>
                </tr>
                <tr>
                    <td style="width: 20px;" width="30px"></td>
                    <td  colspan="4"></td>
                    <td style="width: 20px;"></td>
                </tr>
                <tr>
                    <td style="width: 20px;" width="30px"></td>
                    <td  colspan="4">
                        <fieldset id="fsCTransaccion" runat="server" visible="false">
                            <legend>Campos de Transaccion de nomina:</legend>
                            <table cellpadding="0"   cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Código" Visible="False" Width="30px"></asp:Label>
                                    </td>
                                    <td width="250px">
                                        <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                                    </td>
                                    <td width="100px">
                                        <asp:Label ID="Label6" runat="server" Text="Clase Contable" Visible="False"></asp:Label></td>
                                    <td width="150px">
                                        <asp:DropDownList ID="ddlClase" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Forma de contabilización" Visible="False"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipo" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                                            <asp:ListItem Value="PE">Por empleado</asp:ListItem>
                                            <asp:ListItem Value="PC">Por contratista</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Tipo transación" Visible="False"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoTraNomina" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="width: 20px;"></td>
                </tr>
                <tr>
                    <td style="width: 20px"></td>
                    <td style="text-align: left; width: 115px;"></td>
                    <td style="text-align: left; width: 340px;"></td>
                    <td style="width: 115px; text-align: left;"></td>
                    <td style="text-align: left; width: 340px;"></td>
                    <td style="width: 20px;"></td>
                </tr>
                <tr>
                    <td style="width: 20px"></td>
                    <td style="text-align: left;" colspan="4">
                        <fieldset id="fsCNomina" runat="server" visible="false">
                            <legend>Campos nomina</legend>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Mayor ccosto nomina" Visible="False"></asp:Label></td>
                                    <td width="250px">
                                        <asp:DropDownList ID="ddlCcostoMayorNomi" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlCcostoMayorNomi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100px">
                                        <asp:Label ID="Label11" runat="server" Text="Ccosto nomina" Visible="False"></asp:Label></td>
                                    <td width="150px">
                                        <asp:DropDownList ID="ddlCentroCostoNomi" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlCentroCostoNomi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkManejaDepartamento" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaDepartamento_CheckedChanged" Text="Maneja departamento" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDepartamento" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlCentroCostoNomi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Concepto / Labor" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlConcepto" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkManejaEntidad" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaEntidad_CheckedChanged" Text=" Entidad" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEntidad" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                            <asp:ListItem Value="EEPS">Entidad Eps</asp:ListItem>
                                            <asp:ListItem Value="EP">Entidad Pensión</asp:ListItem>
                                            <asp:ListItem Value="ECESANTIAS">Entidad Cesantias</asp:ListItem>
                                            <asp:ListItem Value="ECAJA">Entidad Caja Compensaciones</asp:ListItem>
                                            <asp:ListItem Value="EARP">Entidad Arp</asp:ListItem>
                                            <asp:ListItem Value="EICBF">Entidad ICBF</asp:ListItem>
                                            <asp:ListItem Value="ESENA">Entidad Sena</asp:ListItem>
                                            <asp:ListItem Value="EFONDO">Entidades de Fondos</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="width: 20px;"></td>
                </tr>
                <tr>
                    <td style="width: 20px"></td>
                    <td style="width: 115px; text-align: left;"></td>
                    <td style="text-align: left; width: 340px"></td>
                    <td style="width: 115px; text-align: left;"></td>
                    <td style="text-align: left; width: 340px;"></td>
                    <td style="width: 20px"></td>
                </tr>
                <tr>
                    <td style="width: 20px"></td>
                    <td style="text-align: left;" colspan="4">
                        <fieldset id="fsContabilidad" runat="server" visible="false">
                            <legend>Campos contables</legend>
                            <table cellpadding="0" cellspacing="0" class="auto-style1">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCuentaGasto" runat="server" Text="Cuenta débito" Visible="False"></asp:Label>
                                    </td>
                                    <td width="250px">
                                        <asp:DropDownList ID="ddlCuentaGasto" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100px">
                                        <asp:Label ID="lblCuentaContable" runat="server" Text="Cuenta activo" Visible="False"></asp:Label>
                                    </td>
                                    <td width="150px">
                                        <asp:DropDownList ID="ddlCuentaContable" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCentroCosto0" runat="server" Text="Mayor ccosto débito/activo" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCentroCostoMayor" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlCentroCostoMayor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCentroCosto" runat="server" Text="Ccosto débito/activo+" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCentroCosto" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkTerceroActivo" runat="server" AutoPostBack="True" OnCheckedChanged="chkTerceroActivo_CheckedChanged" Text="Tercero débito / activo" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTercero" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." ToolTip="Código nacional de ocupación" Visible="False" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCuentaContratista" runat="server" Text="Cuenta contratista" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCuentaContratista" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCuentaCredito" runat="server" Text="Cuenta crédito" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCuentaCredito" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkTerceroCredito" runat="server" AutoPostBack="True" OnCheckedChanged="chkTerceroCredito_CheckedChanged" Text="Tercero crédito" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTerceroCredito" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." ToolTip="Código nacional de ocupación" Visible="False" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCcostoCredito0" runat="server" Text="Mayor ccosto crédito" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCcostoMayorCredito" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación" AutoPostBack="True" OnSelectedIndexChanged="ddlCcostoMayorCredito_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCcostoCredito" runat="server" Text="Ccosto credito" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCentroCostoCredito" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" ToolTip="Código nacional de ocupación">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:RadioButtonList ID="rblTipoCantidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblTipoCantidad_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="False" >
                                            <asp:ListItem Value="1" Selected="True">Sin cantidad</asp:ListItem>
                                            <asp:ListItem Value="2">Porcentaje</asp:ListItem>
                                            <asp:ListItem Value="3">Cantidad</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValor" runat="server" Text="Valor" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txvValor" runat="server" Visible="False" AutoPostBack="True" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="width: 20px"></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 20px;"></td>
                </tr>
            </table>
            <div style="text-align: center; width: 1400px">
                <div style="display: inline-block; padding-left: 15px;">
                    <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                    <asp:GridView ID="gvLista" Width="1400px" runat="server" GridLines="None" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" CssClass="Grid" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" OnPageIndexChanging="gvLista_PageIndexChanging1">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" HeaderText="Edit" ImageUrl="~/Imagen/TabsIcon/pencil.png" Text="Botón" CommandName="Select">
                                <ItemStyle Width="20px" CssClass="Items" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Elim">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imElimina" runat="server" CommandName="Delete" ImageUrl="~/Imagen/TabsIcon/cancel.png" OnClientClick="if(!confirm('Desea eliminar el registro ?')){return false;};" ToolTip="Elimina el registro seleccionado" />
                                </ItemTemplate>
                                <HeaderStyle BackColor="White" />
                                <ItemStyle Width="20px" CssClass="Items" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="id" HeaderText="C&#243;digo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desConcepto" HeaderText="Concepto">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="clase" HeaderText="idClase" ReadOnly="True"
                                SortExpression="descripcion"></asp:BoundField>
                            <asp:BoundField DataField="desClase" HeaderText="Clase" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipoTransaccion" HeaderText="Tra. N" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ccostoMayor" HeaderText="CCMayor" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desCcostoMayor" HeaderText="Ccosto M">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ccosto" HeaderText="CCaux">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cuentaGasto" HeaderText="Cuenta D">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cuentaCredito" HeaderText="Cuenta C">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tercero" HeaderText="Ter. D">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="terceroCredito" HeaderText="Ter. C"></asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <RowStyle CssClass="rw" />
                    </asp:GridView>
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
</body>
</html>
