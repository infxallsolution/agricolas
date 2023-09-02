<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prospecto.aspx.cs" Inherits="Nomina.WebForms.Formas.PgestionHumana.Prospecto" %>

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
                            <td style="vertical-align: top; width: 160px;">
                                <div style="width: 260px; padding: 5px">
                                    <asp:Image ID="imbFuncionario" runat="server" Height="100%" Visible="False" Width="100%"  />
                                </div>
                            </td>
                            <td style="vertical-align: top; width: 2px"></td>
                            <td style="vertical-align: top; width: 700px;">
                                <table style="border-top: silver thin solid; border-bottom: silver thin solid; width: 100%;" id="TABLE1">
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="lblTercero" runat="server" Text="Tercero" Visible="False"></asp:Label></td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:DropDownList ID="ddlTercero" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="lblTercero0" runat="server" Text="Contrato" Visible="False"></asp:Label></td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:DropDownList ID="ddlContratos" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label14" runat="server" Text="Sexo"
                                                Visible="False"></asp:Label></td>
                                        <td colspan="3" style="text-align: left">
                                            <table class="w-100">
                                                <tr>
                                                    <td>
                                            <asp:DropDownList ID="ddlSexo" runat="server" Visible="False" Width="150px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                            </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 130px">
                                            <asp:Label ID="Label9" runat="server" Text="Grupo Sanguineo"
                                                Visible="False"></asp:Label></td>
                                                    <td>
                                            <asp:DropDownList ID="ddlRh" runat="server" Visible="False" Width="150px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                            </asp:DropDownList></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="lblFechaNacimiento" runat="server" OnClick="lbFechaNacimiento_Click"  Visible="False">Fecha nacimiento</asp:Label>
                                        </td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:TextBox ID="txtFechaNacimeinto" runat="server" Font-Bold="True"  placeholder="DD/MM/YYYY"
                                                Visible="False" CssClass="input fecha" AutoPostBack="True" OnTextChanged="txtFechaNacimiento_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="lblDescripcion" runat="server" Text="Edad" Visible="False"></asp:Label>
                                        </td>
                                        <td colspan="3" style="text-align: left">
                                            <table class="auto-style1">
                                                <tr>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:TextBox ID="txvEdad" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" Width="80px">0</asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:Label ID="Label21" runat="server" Text="Altura (mt)"
                                                            Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:TextBox ID="txvAltura" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" Width="80px">0</asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:Label ID="Label16" runat="server" Text="Peso (Kg)"
                                                            Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:TextBox ID="txvPeso" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" Width="80px">0</asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label15" runat="server" Text="Ciudad nacimiento"
                                                Visible="False"></asp:Label></td>
                                        <td class="Campos" colspan="3">
                                            <asp:DropDownList ID="ddlCiudadNacimineto" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label20" runat="server" Text="Estado Civil"
                                                Visible="False"></asp:Label></td>
                                        <td class="Campos" colspan="3">
                                            <asp:DropDownList ID="ddlEstadoCivil" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label19" runat="server" Text="Ocupación"
                                                Visible="False"></asp:Label></td>
                                        <td class="Campos" colspan="3">
                                            <asp:DropDownList ID="ddlOcupacion" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label18" runat="server" Text="Nivel Educativo"
                                                Visible="False"></asp:Label></td>
                                        <td class="Campos" colspan="3">
                                            <asp:DropDownList ID="ddlNivelEducativo" runat="server" Visible="False" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                            </asp:DropDownList></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label22" runat="server" Text="Dirección"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td class="Campos" colspan="3">
                                            <asp:TextBox ID="txtDireccion" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label17" runat="server" Text="Barrio"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <table  style="width: 100%">
                                                <tr>
                                                    <td style="text-align: left; width: 210px">
                                                        <asp:TextBox ID="txtBarrio" runat="server" Visible="False" Width="200px" CssClass="input"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:Label ID="Label23" runat="server" Text="Ciudad"
                                                            Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 260px">
                                                        <asp:DropDownList ID="ddlCiudad" runat="server" Visible="False" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label24" runat="server" Text="Teléfono"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <table  class="auto-style1">
                                                <tr>
                                                    <td style="text-align: left; width: 170px">
                                                        <asp:TextBox ID="txtTelefono" runat="server" Visible="False" Width="150px" CssClass="input"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; width: 70px">
                                                        <asp:Label ID="Label25" runat="server" Text="E-mail"
                                                            Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 250px">
                                                        <asp:TextBox ID="txtEmail" runat="server" Visible="False" Width="230px" CssClass="input"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label26" runat="server" Text="Talla camisa"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <table  class="auto-style1">
                                                <tr>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:DropDownList ID="ddlCamisa" runat="server" Visible="False" Width="70px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                                            <asp:ListItem Value="XXS">XXS</asp:ListItem>
                                                            <asp:ListItem Value="XS">XS</asp:ListItem>
                                                            <asp:ListItem Value="S">S</asp:ListItem>
                                                            <asp:ListItem Value="M">M</asp:ListItem>
                                                            <asp:ListItem Value="L">L</asp:ListItem>
                                                            <asp:ListItem Value="XL">XL</asp:ListItem>
                                                            <asp:ListItem Value="XXL">XXL</asp:ListItem>
                                                            <asp:ListItem Value="XXXL">XXXL</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:Label ID="Label27" runat="server" Text="Talla Pantalon"
                                                            Visible="False"></asp:Label></td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:TextBox ID="txvPantalon" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)" Width="80px">0</asp:TextBox>
                                                    </td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:Label ID="Label28" runat="server" Text="Talla zapatos"
                                                            Visible="False"></asp:Label></td>
                                                    <td style="text-align: left; width: 100px">
                                                        <asp:TextBox ID="txvZapato" runat="server" CssClass="input" Visible="False" onkeyup="formato_numero(this)">0</asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label29" runat="server" Text="Marcas/Cicatrices"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td class="Campos" colspan="3">
                                            <asp:TextBox ID="txtCicatrices" runat="server" Visible="False" Width="100%" CssClass="input" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:Label ID="Label30" runat="server" Text="Limitaciones fisicas"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td class="Campos" colspan="3">
                                            <asp:TextBox ID="txtLimitaciones" runat="server" Visible="False" Width="100%" CssClass="input" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" colspan="4">
                                            <asp:TextBox ID="txtObservacion" placeholder="Observaciones y/o notas..." runat="server" Visible="False" Width="100%" CssClass="input" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left">
                                            <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkValidaFoto" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="chkValidaFoto_CheckedChanged" Text="Maneja Foto" Visible="False" />
                                        </td>
                                        <td style="text-align: left; width: 320px">
                                            <asp:FileUpload ID="fuFoto" runat="server" ToolTip="Haga clic para cargar la foto del funcionario seleccionado"
                                                Visible="False" Width="300px" onchange="hiddenCommand.click();" />
                                            <asp:Button ID="hiddenCommand" runat="server" ClientIDMode="Static" OnClick="hiddenCommand_Click" Style="display: none;" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="Label11" runat="server" Text="Plantilla" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlPlantilla" runat="server" Visible="False" Width="150px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect">
                                                <asp:ListItem Value="0">Plantilla 1</asp:ListItem>
                                                <asp:ListItem Value="1">Plantilla 2</asp:ListItem>
                                                <asp:ListItem Value="2">Plantilla 3</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
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
                                <asp:Label runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                            <asp:BoundField DataField="tercero" HeaderText="Id" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombreTercero" HeaderText="NombreEmpleado" ReadOnly="True"
                                SortExpression="descripcion">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="contrato" HeaderText="Contrato" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sexo" HeaderText="Sexo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaNacimiento" HeaderText="FechaN" DataFormatString="{0:dd/MM/yyy}">
                                <HeaderStyle  HorizontalAlign="Left" />
                                <ItemStyle  HorizontalAlign="Left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="edad" HeaderText="Edad" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rh" HeaderText="Rh">
                                <HeaderStyle  HorizontalAlign="Left" />
                                <ItemStyle  HorizontalAlign="Left" Width="20px" />
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
