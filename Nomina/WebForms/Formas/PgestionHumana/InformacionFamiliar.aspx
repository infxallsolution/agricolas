<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformacionFamiliar.aspx.cs" Inherits="Nomina.WebForms.Formas.PgestionHumana.InformacionFamiliar" %>

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
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="lblTercero" runat="server" Text="Tercero" Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTercero" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="lblTercero0" runat="server" Text="Contrato" Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlContratos" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="Label33" runat="server" Text="TipoDocumento"
                            Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 320px">
                                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 100px">
                                    <asp:Label ID="Label34" runat="server" Text="Indentificación"
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtIdentificacion" runat="server" Visible="False" Width="150px" CssClass="input"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="Label22" runat="server" Text="Descripción"
                            Visible="False"></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left;">
                        <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="650px" CssClass="input"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="Label35" runat="server" Text="Ocupación"
                            Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlOcupacion" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="Label37" runat="server" Text="Parentesco"
                            Visible="False"></asp:Label>
                    </td>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 110px">
                                    <asp:DropDownList ID="ddlParentesco" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 110px;">
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha nacimiento" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 90px;">
                                    <asp:TextBox ID="txtFechaNacimeinto" runat="server" Visible="False" Width="150px" CssClass="input fecha" placeholder="DD/MM/YYYY" AutoPostBack="True" OnTextChanged="txtFechaFinal_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="Label38" runat="server" Text="Ciudad"
                            Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlCiudad" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="Label36" runat="server" Text="Dirección"
                            Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDireccion" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="width: 110px; text-align: left">
                        <asp:Label ID="Label31" runat="server" Text="Teléfono"
                            Visible="False"></asp:Label>
                    </td>
                    <td>
                        <table class="auto-style1">
                            <tr>
                                <td style="text-align: left; width: 170px">
                                    <asp:TextBox ID="txtTelefono" runat="server" Visible="False" Width="150px" CssClass="input"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 70px">
                                    <asp:Label ID="Label25" runat="server" Text="E-mail"
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtEmail" runat="server" Visible="False" Width="230px" CssClass="input"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td style="text-align: left" colspan="2">
                        <asp:TextBox ID="txtObservacion" placeholder="Observaciones y/o notas..." runat="server" Visible="False" Width="100%" CssClass="input" Height="60px" TextMode="MultiLine"></asp:TextBox>
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
                                <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tercero" HeaderText="Id" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreTercero" HeaderText="NombreEmpleado" ReadOnly="True"
                            SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="contrato" HeaderText="Contrato" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="registro" HeaderText="No" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" Width="5px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="descripcion" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreParentesco" HeaderText="Parentesco" ReadOnly="True">
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