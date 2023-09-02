<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametrosGeneralConfig.aspx.cs" Inherits="Administracion.WebForms.Formas.Padministracion.ParametrosGeneralConfig" %>

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
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False"  />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%;" id="tdCampos" cellspacing="0">
                <tr>
                    <td style=""></td>
                    <td width="200px" style="text-align: left; width: 150px">
                        <asp:Label ID="Label1" runat="server" Text="Nombre"  CssClass="label" Visible="False"></asp:Label></td>
                    <td width="300px" style="width: 400px; text-align: left">
                        <asp:TextBox ID="txtNombre" runat="server"  Width="100%" AutoPostBack="True" OnTextChanged="txtNombre_TextChanged" CssClass="input" Visible="False"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2" style="text-align: left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary"  ID="chkManejaDs" runat="server" Text="Maneja origen de datos"  OnCheckedChanged="chkManejaDs_CheckedChanged" AutoPostBack="True" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="lblTabla" runat="server" Text="Tabla"  CssClass="label" Visible="False"></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:DropDownList ID="ddlTabla" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="100%"  AutoPostBack="True" OnSelectedIndexChanged="ddlTabla_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="lblCampoValor" runat="server" Text="Campo valor"  CssClass="label" Visible="False"></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:DropDownList ID="ddlCampoValor" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="100%"  AutoPostBack="True" OnSelectedIndexChanged="ddlCampoValor_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="lblEtiqueta" runat="server" Text="Campo etiqueta"  CssClass="label" Visible="False"></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:DropDownList ID="ddlCampoEtiqueta" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="100%"  AutoPostBack="True" OnSelectedIndexChanged="ddlCampoEtiqueta_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="lblTipoDato" runat="server" Text="Tipo de dato"  CssClass="label" Visible="False"></asp:Label></td>
                    <td style="width: 400px; text-align: left">
                        <asp:RadioButtonList ID="rblTipoDato" runat="server"  RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblTipoDato_SelectedIndexChanged" Visible="False">
                            <asp:ListItem class="btn btn-default" Value="varchar(500)" Selected="True">Texto</asp:ListItem>
                            <asp:ListItem class="btn btn-default" Value="date">Fecha</asp:ListItem>
                            <asp:ListItem class="btn btn-default" Value="int">Númerico</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="lblValor" runat="server" CssClass="label" Text="Valor" Visible="False" ></asp:Label>
                    </td>
                    <td style="width: 400px; text-align: left">
                        <asp:TextBox ID="txtValorN" runat="server" CssClass="input"  Width="100%" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtValorF" runat="server" CssClass="input"  Width="100%" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtValor" runat="server" CssClass="input"  Width="100%" Visible="False"></asp:TextBox>
                        <asp:DropDownList ID="ddlValor" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="100%" Visible="False" >
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20">
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
                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="manejaDS" HeaderText="DS">
                            <ItemStyle Width="10px" HorizontalAlign="Left" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="tipoDato" HeaderText="Tipo de dato">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ds" HeaderText="Tabla">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cValor" HeaderText="CampoValor">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cEtiqueta" HeaderText="CampoEtiqueta">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor" HeaderText="Valor">
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
