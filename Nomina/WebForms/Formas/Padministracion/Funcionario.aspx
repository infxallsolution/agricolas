<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Funcionario.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.Funcionario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <asp:Button ID="nibtnBuscar" runat="server" CssClass="botones" OnClick="nibtnBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nibtnNuevo" runat="server" CssClass="botones" OnClick="nibtnNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="botones" OnClick="nibtnCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="btnGuardar" runat="server" CssClass="botones" OnClick="nibtnGuardar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" Style="height: 26px" />
                    </td>
                </tr>
            </table>
            <hr />
            <table class="w-100">
                <tr>
                    <td class="w-25"></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="lblTipoFuncionario" runat="server" Text="Tipo funcionario" Visible="False"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlTipoFuncionario" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione tipo Funcionario..." Height="20px" TabIndex="2" Width="100%" Enabled="False" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoFuncionario_SelectedIndexChanged">
                        </asp:DropDownList>

                    </td>
                    <td class="w-25"></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: center;" colspan="2">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkBDasociada" runat="server" Text="Desea cargar terceros de base de datos asociada?" Visible="False" AutoPostBack="True" OnCheckedChanged="chkBDasociada_CheckedChanged" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <table class="w-100" id="tdCampos" runat="server" visible="false">
                <tr>
                    <td  style="vertical-align: top; text-align:right; width:15%">
                        <div style="margin-top:10px; margin-right:10px">
                        <asp:Image ID="imgFuncionario" runat="server" Height="250px" Visible="False" Width="200px" BorderWidth="1px" />
                            </div>
                    </td>

                    <td class="w-75">
                        <table style="width: 100%;" id="tbDatosDetalle" runat="server">
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblTercero" runat="server" Text="Seleccione tercero" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlTercero" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione el  funcionario..." Height="20px" TabIndex="2" Width="100%" Enabled="False" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblTipoIdentificaicon" runat="server" Text="Tipo Identificación"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlTipoID" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Height="20px" TabIndex="3" Width="100%" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblIdentificacion" runat="server" Text="Nro. Identificación"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtIdentificacion" runat="server" onkeypress="return soloNumeros(event)" CssClass="input" Width="250px" TabIndex="4" AutoPostBack="True" OnTextChanged="txtIdentificacion_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblPrimerApellido" runat="server" Text="Primer Apellido"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtApellido1" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="input" TabIndex="5" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblSegundoApellido" runat="server" Text="Segundo Apellido"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtApellido2" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="input" TabIndex="6" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblPrimerNombre" runat="server" Text="Nombres"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtNombres" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="input" TabIndex="7" Width="100%" Visible="False"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblSexo" runat="server" Text="Sexo"></asp:Label></td>
                                <td style="text-align: left">
                                    <table class="w-100">
                                        <tr>
                                            <td style="width: 220px; text-align: left">
                                                <asp:DropDownList ID="ddlSexo" runat="server" Visible="False" Width="95%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="8">
                                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left; width: 150px">
                                                <asp:Label ID="lblRH" runat="server" Text="Grupo Sanguineo"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlRh" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="9">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha nacimiento"></asp:Label></td>
                                <td style="text-align: left">
                                    <table class="w-100">
                                        <tr>
                                            <td style="width: 220px; text-align: left">
                                                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="input fecha" Width="180px" TabIndex="10"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 150px">
                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkFechaRetiro" runat="server" AutoPostBack="True" Checked="True" Text="Maneja fecha retiro" TabIndex="12" OnCheckedChanged="chkFechaRetiro_CheckedChanged" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtFechaRetiro" runat="server" CssClass="input fecha" Width="180px" TabIndex="11"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblSalario" runat="server" Text="Salario"></asp:Label></td>
                                <td style="text-align: left">
                                    <table class="w-100">
                                        <tr>
                                            <td style="width: 190px; text-align: left">
                                                <asp:TextBox ID="txvSalario" runat="server" CssClass="input " onkeyup="formato_numero(this)" Width="170px" TabIndex="12">0</asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 90px">
                                                <asp:Label ID="lblCargo" runat="server" Text="Cargo"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCargo" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="15">
                                    </asp:DropDownList>
                                                <asp:TextBox ID="txtCargo" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="input" Width="100%" TabIndex="13"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblCcosto" runat="server" Text="Centro costo"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCentroCosto" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="input" Width="100%" TabIndex="14"></asp:TextBox>
                                    <asp:DropDownList ID="ddlCentroCosto" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="15">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblEPS" runat="server" Text="Entidad EPS"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlEPS" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="16">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblARP" runat="server" Text="Entidad ARP"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlARL" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="17">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:Label ID="lblProveedor" runat="server" Text="Proveedor"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlProveedor" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="18">
                                    </asp:DropDownList></td>
                            </tr>


                            <tr>
                                <td style="width: 150px; text-align: left">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkValidaFoto" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="chkValidaFoto_CheckedChanged" Text="Maneja Foto" Visible="False" TabIndex="12" />
                                </td>
                                <td style="text-align: left">
                                    <table class="w-100">
                                        <tr>
                                            <td style="text-align: left; width: 220px;">
                                                <asp:FileUpload ID="fuFoto" runat="server" ToolTip="Haga clic para cargar la foto del funcionario seleccionado"
                                                    Visible="False" Width="200px" onchange="hiddenCommand.click();" />
                                                <asp:Button ID="hiddenCommand" runat="server" ClientIDMode="Static" OnClick="hiddenCommand_Click" Style="display: none;" />
                                            </td>
                                            <td style="text-align: left; width: 150px">
                                                &nbsp;</td>
                                            <td style="text-align: left">
                                                &nbsp;</td>
                                            <td style="text-align: left">
                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" TabIndex="14" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="2">
                                    <table class="w-100 text-left">
                                        <tr>
                                            <td class="w-20">
                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkConductor" runat="server" Text="Conductor" TabIndex="14" /></td>
                                            <td class="w-20">
                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkOperadorLogistico" runat="server" Text="Operador logístico" TabIndex="14" /></td>
                                            <td class="w-20">
                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkValidaTurno" runat="server" Text="Valida turno" TabIndex="14" /></td>
                                            <td class="w-2">
                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaLabores" runat="server" Text="Maneja labores de campo" TabIndex="14" /></td>
                                            <td class="w-20">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">

                                                <div id="multiEmpresa" runat="server" visible="false" class="w-100 text-center">
                                                   <hr />
                                                    <h6 ><strong>  Asociar empresas </strong></h6>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-1"></div>
                                                        <div class="col-8">
                                                            <select runat="server" id="selEmpresas" class="multiselect" multiple="true" name="countries[]" visible="True" style="width: 600px; height: 200px;"></select></div>
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                    </table>


                                </td>
                            </tr>
                        </table>
                    </td>

                    <td style="width:15%" ></td>
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
                        
                        <asp:BoundField DataField="codigo" HeaderText="Identificación" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" BorderWidth="1px" />
                            <ItemStyle HorizontalAlign="Left" BorderWidth="1px" Width="60px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="descripcion" HeaderText="Nombres" ReadOnly="True"                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" BorderWidth="1px" />
                            <ItemStyle HorizontalAlign="Left" BorderWidth="1px" />
                        </asp:BoundField>
                          <asp:BoundField DataField="tipo" HeaderText="IdTipo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" BorderWidth="1px" />
                            <ItemStyle HorizontalAlign="Left" BorderWidth="1px" Width="10px" />
                        </asp:BoundField>
                          <asp:BoundField DataField="nombretipo" HeaderText="NombreTipo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" BorderWidth="1px" />
                            <ItemStyle HorizontalAlign="Left" BorderWidth="1px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sexo" HeaderText="Sexo">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Left" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rh" HeaderText="Rh">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Left" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Center" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="conductor" HeaderText="Conduct">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Center" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="validaTurno" HeaderText="ValTur">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Center" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaLabores" HeaderText="ManLabor">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Center" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="operadorLogistico" HeaderText="OpLog">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Center" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Center" Width="10px" />
                        </asp:CheckBoxField>
                         <asp:CheckBoxField DataField="multiEmpresa" HeaderText="MulEmp">
                            <HeaderStyle BorderWidth="1px" HorizontalAlign="Center" />
                            <ItemStyle BorderWidth="1px" HorizontalAlign="Center" Width="10px" />
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
