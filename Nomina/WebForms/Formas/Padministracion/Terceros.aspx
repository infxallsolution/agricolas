<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terceros.aspx.cs" Inherits="Nomina.WebForms.Formas.Padministracion.Terceros" %>

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
            <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="bordesBusqueda"></td>
                    <td style="text-align: left; width: 100px">Busqueda</td>
                    <td>
                        <asp:TextBox ID="nitxtBusqueda" placeholder="Digite filtro de busqueda" runat="server" Width="100%" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox>

                    </td>
                    <td class="bordesBusqueda"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación"  />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos"  TabIndex="23" />

                    </td>
                </tr>
            </table>
            <hr />

            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <table style="width: 100%; text-align: left;" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label2" runat="server" Text="Código" ></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server"  Width="200px" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged" onkeypress="return soloNumeros(event)" CssClass="input" TabIndex="1"></asp:TextBox>
                    </td>
                    <td colspan="2">
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo"  TabIndex="10" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label5" runat="server" Text="Tipo Identificación" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoID" runat="server" AutoPostBack="true" Width="380px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  Height="20px" TabIndex="2" OnSelectedIndexChanged="ddlTipoID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label4" runat="server" Text="Nro. Identificacion" ></asp:Label>
                    </td>
                    <td style="width: 400px;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 205px; text-align: left">
                                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="input"  Width="200px" TabIndex="3"></asp:TextBox>
                                </td>
                                <td style="width: 35px; text-align: left">
                                    <asp:Label ID="lblDv" runat="server" Text="  DV -" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDv" runat="server" CssClass="input"  Width="30px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social" ></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRazonSocial" runat="server"  Width="100%" CssClass="input concatenarRazon" TabIndex="4"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="lblPrimerApellido" runat="server" Text="Primer Apellido" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtApellido1" runat="server"  Width="220px" CssClass="input concatenarNombre" TabIndex="5"></asp:TextBox>
                    </td>
                    <td style="width: 120px">
                        <asp:Label ID="lblSegundoApellido" runat="server" Text="Segundo Apellido" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtApellido2" runat="server"  Width="220px" CssClass="input concatenarNombre" TabIndex="6"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="lblPrimerNombre" runat="server" Text="Primer Nombre" ></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNombre1" runat="server"  Width="220px" CssClass="input concatenarNombre" TabIndex="7"></asp:TextBox>
                    </td>
                    <td style="width: 120px">
                        <asp:Label ID="lblSegundoNombre" runat="server" Text="Segundo Nombre" ></asp:Label></td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtNombre2" runat="server"  Width="220px" CssClass="input concatenarNombre" TabIndex="8"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label3" runat="server" Text="Descripción" ></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescripcion" runat="server"  Width="100%" CssClass="input" TabIndex="9"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label17" runat="server" Text="Contácto" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtContacto" runat="server"  Width="380px" CssClass="input" TabIndex="18"></asp:TextBox>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label7" runat="server" Text="Teléfono" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <table class="auto-style1">
                            <tr>
                                <td style="text-align: left; width: 120px">
                                    <asp:TextBox ID="txtTelefono" runat="server"  Width="100px" CssClass="input" TabIndex="19"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 70px">
                                    <asp:Label ID="Label20" runat="server" Text="Celular" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCelular" runat="server"  Width="150px" CssClass="input" TabIndex="20"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label25" runat="server" Text="País" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList ID="ddlPais" runat="server" Width="380px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  Height="20px" TabIndex="21" AutoPostBack="True" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label23" runat="server" Text="Departamento/Provincia" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList ID="ddlDepartamento" runat="server" Width="380px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  Height="20px" TabIndex="22" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label22" runat="server" Text="Ciudad" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList ID="ddlCiudad" runat="server" Width="380px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  Height="20px" TabIndex="23">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label9" runat="server" Text="Barrio/Zona residencial" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtBarrio" runat="server" CssClass="input"  Width="380px" TabIndex="24"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label19" runat="server" Text="Dirección" ></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDireccion" runat="server"  Width="100%" CssClass="input" TabIndex="25"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 120px">
                        <asp:Label ID="Label21" runat="server" Text="Email" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtEmail" runat="server"  Width="380px" CssClass="input" TabIndex="26"></asp:TextBox>
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="Label24" runat="server" Text=" Código Ciiu" ></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList ID="ddlCiiu" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." TabIndex="27"  Width="380px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>

            </table>
            <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
            <hr />
            <div class="tablaGrilla">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imEdit" CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt" CommandName="Select" ToolTip="Edita el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash" CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="id">
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoDocumento" HeaderText="TipDoc">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="TipoPer">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nit" HeaderText="Nit">
                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dv" HeaderText="Dv">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ControlStyle-Width="250px">
                            <ControlStyle Width="250px"></ControlStyle>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <ItemStyle Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="cliente" HeaderText="CLI">
                            <ItemStyle Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="proveedor" HeaderText="PRO">
                            <ItemStyle Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="empleado" HeaderText="EMPL">
                            <ItemStyle Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="extractora" HeaderText="EXTR">
                            <ItemStyle Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="contratista" HeaderText="CONT">
                            <ItemStyle Width="5px" />
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