<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Funcionarios.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.Funcionarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
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
                    <td style="vertical-align: top;">
                        <table style="width: 100%; border-top: silver thin solid; border-bottom: silver thin solid;" id="TABLE1">
                            <tr>
                                <td></td>
                                <td style="text-align: left; height: 20px;">
                                    <asp:CheckBox ID="chkManejaTercero" runat="server" AutoPostBack="True" OnCheckedChanged="chkManejaTercero_CheckedChanged1" Text="Crear Tercero" Visible="False" CssClass="checkbox checkbox-primary" />
                                </td>
                                <td style="text-align: left"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left" colspan="2">
                                    <asp:Panel ID="pnTercero" runat="server" Visible="False">
                                        <fieldset>
                                            <h6>Información del tercero</h6>
                                            <div style="padding: 5px">
                                                <table style="width: 700px" width="100%">
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="Label23" runat="server" Text="Código"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txtCodigo_TextChanged" onkeyup="formato_numero(this)" TabIndex="1" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="Label5" runat="server" Text="Tipo Identificación"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:DropDownList ID="ddlTipoID" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Height="20px" TabIndex="2" Width="300px" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="Label4" runat="server" Text="Nro. Identificacion"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px" valign="middle">
                                                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="input" TabIndex="3" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="lblPrimerApellido" runat="server" Text="Primer Apellido"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:TextBox ID="txtApellido1" runat="server" CssClass="input" TabIndex="5" Width="220px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="lblSegundoApellido" runat="server" Text="Segundo Apellido"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:TextBox ID="txtApellido2" runat="server" CssClass="input" TabIndex="6" Width="220px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="lblPrimerNombre" runat="server" Text="Primer Nombre"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:TextBox ID="txtNombre1" runat="server" CssClass="input" TabIndex="7" Width="220px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="lblSegundoNombre" runat="server" Text="Segundo Nombre"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:TextBox ID="txtNombre2" runat="server" CssClass="input" TabIndex="7" Width="220px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="lblSegundoNombre0" runat="server" Text="Teléfono"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="input" TabIndex="7" Width="180px" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="lblDireccion" runat="server" Text="Dirección"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="input" TabIndex="7" Width="400px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="Label25" runat="server" Text="País"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Height="20px" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" TabIndex="21" Width="400px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="Label24" runat="server" Text="Departamento" Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:DropDownList ID="ddlDepartamento" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Height="20px" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" TabIndex="14" Visible="False" Width="400px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px; text-align: left;">
                                                            <asp:Label ID="Label22" runat="server" Text="Ciudad" Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 400px">
                                                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Height="20px" TabIndex="14" Visible="False" Width="400px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblTercero" runat="server" Text="Tercero" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlTercero" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlFuncionario_SelectedIndexChanged" Visible="False" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                    </asp:DropDownList></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblIdentifiacion" runat="server" Text="Identificación" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="input" Width="150px" Visible="False"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input" Width="350px" Visible="False"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="Label16" runat="server" Text="Proveedor"
                                        Visible="False"></asp:Label></td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlProveedor" runat="server" Visible="False" Width="350px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                    </asp:DropDownList></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="Label17" runat="server" Text="Cliente"
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:DropDownList ID="ddlCliente" runat="server" Visible="False" Width="350px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lbFechaContratistaHasta" runat="server"  Visible="False">Fecha contratista hasta</asp:Label>
                                </td>
                                <td style="text-align: left; width: 400px">
                                    <asp:TextBox ID="txtFechaContratista" runat="server" placeholder="DD/MM/YYYY" Font-Bold="True"
                                        Visible="False" CssClass="input" AutoPostBack="True" OnTextChanged="txtFechaContratista_TextChanged"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" Visible="False" CssClass="checkbox checkbox-primary" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkContratista" runat="server" Text="Contratista" Visible="False" AutoPostBack="True" OnCheckedChanged="chkContratista_CheckedChanged" CssClass="checkbox checkbox-primary" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkOtros" runat="server" Text="Otros" Visible="False" CssClass="checkbox checkbox-primary" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkValidaTurno" runat="server" Text="Genera horas extras" Visible="False" CssClass="checkbox checkbox-primary" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkOperador" runat="server" Text="Operador Logístico" Visible="False" CssClass="checkbox checkbox-primary" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkConductor" runat="server" Text="Conductor Interno" Visible="False" CssClass="checkbox checkbox-primary" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
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
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tercero" HeaderText="Tercero" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="proveedor" HeaderText="Proveedor">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cliente" HeaderText="Cliente">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="validaTurno" HeaderText="Turno">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="conductor" HeaderText="Cond">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="operadorLogistico" HeaderText="Port">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="contratista" HeaderText="Contr">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>

                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="otros" HeaderText="Otros">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
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
