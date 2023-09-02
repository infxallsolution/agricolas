<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Labor.aspx.cs" Inherits="Agronomico.WebForms.Formas.Padministracion.Labor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>

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
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="lbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px;">
                        <asp:Label ID="Label4" runat="server" Text="Grupo Novedad" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlGrupo" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLaborNoPrestacional" runat="server" Text="Labor no prestacional" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label2" runat="server" Text="Código" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtCodigo" runat="server" Visible="False" Width="150px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label8" runat="server" Text="Descripción corta" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcionCorta" runat="server" Visible="False" Width="95%" CssClass="input"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label3" runat="server" Text="Descripción" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="95%" CssClass="input"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label9" runat="server" Text="Unidad Medida" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlUmedida" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label6" runat="server" Text="Ciclos (días)" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvCiclos" runat="server" Visible="False" Width="150px" CssClass="input" TextMode="Number" ValidateRequestMode="Enabled" ViewStateMode="Enabled" onkeyup="formato_numero(this)"></asp:TextBox></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label10" runat="server" Text="Rendimiento" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvTarea" runat="server" Visible="False" Width="150px" CssClass="input" onkeyup="formato_numero(this)" ValidateRequestMode="Enabled" ViewStateMode="Enabled"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label5" runat="server" Text="Naturaleza" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlNaturaleza" runat="server" CssClass="chzn-select-deselect" Width="95%" Visible="False">
                            <asp:ListItem Value="0">No aplica</asp:ListItem>
                            <asp:ListItem Value="1">Suma</asp:ListItem>
                            <asp:ListItem Value="2">Resta</asp:ListItem>
                            <asp:ListItem Value="3">Erradica</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label7" runat="server" Text="Relacionar concepto" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlConcepto" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label12" runat="server" Text="Tipo Aplicación" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="rblTipoHa" runat="server" CssClass="chzn-select-deselect" Width="95%" Visible="False">
                            <asp:ListItem Selected="True" Value="NA">No Aplica</asp:ListItem>
                            <asp:ListItem Value="HN">Por (ha) Netas</asp:ListItem>
                            <asp:ListItem Value="HB">Por (ha) Brutas</asp:ListItem>
                            <asp:ListItem Value="HP">Por plantas Netas</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label13" runat="server" Text="Clase Labor" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="rblClaseLabor" runat="server" CssClass="chzn-select-deselect" Width="95%" Visible="False">
                           <asp:ListItem Value="1">Mantenimiento</asp:ListItem>
                            <asp:ListItem Value="2">Cosecha</asp:ListItem>
                            <asp:ListItem Value="3">Cargue</asp:ListItem>
                            <asp:ListItem Value="4">Transporte</asp:ListItem>
                            <asp:ListItem Value="5" Selected="True">Sanidad</asp:ListItem>
                            <asp:ListItem Value="6">Fertilización</asp:ListItem>
                            <asp:ListItem Value="7">Aseo</asp:ListItem>
                            <asp:ListItem Value="8">Complementarios de embolse</asp:ListItem>
                            <asp:ListItem Value="9">Calidad de empaque</asp:ListItem>
                            <asp:ListItem Value="10">Control de maleza</asp:ListItem>
                            <asp:ListItem Value="11">Control de población</asp:ListItem>
                            <asp:ListItem Value="12">Control de sigatoka</asp:ListItem>
                            <asp:ListItem Value="13">Control de enfermedades</asp:ListItem>
                            <asp:ListItem Value="14">Drenajes</asp:ListItem>
                            <asp:ListItem Value="15">Corte y Empaque</asp:ListItem>
                            <asp:ListItem Value="16">Gastos de personal</asp:ListItem>
                            <asp:ListItem Value="17">Limpia</asp:ListItem>
                            <asp:ListItem Value="18">MTTO Riego y Motor</asp:ListItem>
                            <asp:ListItem Value="19">Motor</asp:ListItem>
                            <asp:ListItem Value="20">Oficios varios</asp:ListItem>
                            <asp:ListItem Value="21">Parcela combinada</asp:ListItem>
                            <asp:ListItem Value="22">Riego</asp:ListItem>
                            <asp:ListItem Value="23">Trincheo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkRagoSiembra" runat="server" Text="Rango Siembra" Visible="False" AutoPostBack="True" OnCheckedChanged="chkRagoSiembra_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvDesde" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" ToolTip="Año desde"></asp:TextBox>
                        <asp:TextBox ID="txvHasta" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" ToolTip="Año hasta"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCalnal" runat="server" Text="Tipo Canal" Visible="False" AutoPostBack="True" OnCheckedChanged="chkCalnal_CheckedChanged" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlCanal" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label11" runat="server" Text="Equivalencia" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtEquivalencia" runat="server" Visible="False" Width="150px" CssClass="input"></asp:TextBox></td>
                    <td style="text-align: left; width: 140px">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkImpuesto" runat="server" Text="Grupo Impuesto" Visible="False" OnCheckedChanged="chkImpuesto_CheckedChanged" AutoPostBack="True" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlGrupoIR" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="95%" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: center;" colspan="4">
                        <table style="width: 100%">
                            <tr>
                                <td></td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkFecha" runat="server" Text="Maneja Fecha" Visible="False" AutoPostBack="True" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLote" runat="server" Text="Maneja Lote" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkRacimos" runat="server" Text="Maneja Racimos" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaDecimal" runat="server" Text="Maneja Decimal" Visible="False" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLinea" runat="server" Text="Maneja Linea" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBascula" runat="server" Text="Agregar Bascula" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaCaracteristica" runat="server" Text="Maneja Caracteristica" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCalculaJornal" runat="server" Text="Calcula jornales" Visible="False" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPalma" runat="server" Text="Maneja plantas" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSaldo" runat="server" Text="Maneja Saldo" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkJornal" runat="server" Text="Maneja Jornales" Visible="False" />
                                </td>
                                <td style="text-align: left; width: 170px">
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
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
                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="desCorta" HeaderText="desCorta">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="grupo" HeaderText="Grupo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="uMedida" HeaderText="uMedida">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ciclos" HeaderText="Ciclos">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tarea" HeaderText="Tarea">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="naturaleza" HeaderText="Signo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50" />
                        </asp:BoundField>
                        <asp:BoundField DataField="equivalencia" HeaderText="Equi">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="concepto" HeaderText="Concep">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

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
