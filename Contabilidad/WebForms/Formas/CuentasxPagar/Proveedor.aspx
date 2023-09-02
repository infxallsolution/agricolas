<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proveedor.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxPagar.Proveedor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
    <div class="container">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server">
            <table style="width: 100%" cellspacing="0">
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
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label4" runat="server" Text="Tercero" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:DropDownList ID="ddlTercero" runat="server" Width="100%" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Style="left: 0px; top: 5px" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label1" runat="server" Text="Código" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged" Width="100%" CssClass="input" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label2" runat="server" Text="Descripción" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="100%" CssClass="input" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label8" runat="server" Text="Clase de Proveedor" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:DropDownList ID="ddlClaseProveedor" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False"></asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label10" runat="server" Text="Contácto" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:TextBox ID="txtContacto" runat="server" Width="100%" CssClass="input" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label5" runat="server" Text="País" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:DropDownList ID="ddlPais" AutoPostBack="true" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="14" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label3" runat="server" Text="Depto/Provincia" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:DropDownList ID="ddlDepartamento" AutoPostBack="true" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="14" OnSelectedIndexChanged="ddlMunicipio_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label22" runat="server" Text="Ciudad" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:DropDownList ID="ddlCiudad" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" TabIndex="14" Visible="False"></asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label19" runat="server" Text="Dirección" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:TextBox ID="txtDireccion" runat="server" Width="100%" CssClass="input" TabIndex="15" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label23" runat="server" Text="Teléfono" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:TextBox ID="txtTelefono" runat="server" Width="100%" CssClass="input" TabIndex="12" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:Label ID="Label24" runat="server" Text="Email" Width="100%" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:TextBox ID="txtEmail" runat="server" Width="100%" CssClass="input" TabIndex="17" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px" class="text-left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkEntradaDirecta" runat="server" Text="Entrada Directa" Visible="False" />
                    </td>
                    <td style="width: 400px" class="text-left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text=" Activo" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left" colspan="2">
                        <asp:GridView ID="gvClaseIR" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" OnRowUpdating="gvClaseIR_RowUpdating">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sel">
                                    <ItemTemplate>
                                        <asp:CheckBox  ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Items" Width="20px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo" HeaderText="Id">
                                    <ItemStyle CssClass="Items" Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Clase">
                                    <ItemStyle CssClass="Items" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlConcepto" runat="server" Width="180px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlConcepto_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Items" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="llave">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlLlave" runat="server" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Enabled="False">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Items" Width="250px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </td>
                    <td></td>
                </tr>
            </table>

            <hr />

            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" PageSize="20" AllowPaging="True" Width="100%" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging">
                    <AlternatingRowStyle CssClass="alt" />
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
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
                        <asp:BoundField DataField="idTercero" HeaderText="idTercero" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo" HeaderText="Codigo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="clase" HeaderText="Clase" SortExpression="Clase Proveedor">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreclase" HeaderText="NombreCalse" SortExpression="NombreCalse">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="contacto" HeaderText="Contácto">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="ciudad" HeaderText="Ciudad">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="nombreCiudad" HeaderText="NombreCiudad">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefono">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="Email">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="pais" HeaderText="idPais">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="departamento" HeaderText="Dto/Prov">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>--%>
                        <asp:CheckBoxField DataField="entradaDirecta" HeaderText="Directa">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="5px" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
            <script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
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
    </div>
</body>
</html>