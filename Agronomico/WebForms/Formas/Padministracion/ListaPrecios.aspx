<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaPrecios.aspx.cs" Inherits="Agronomico.WebForms.Formas.Padministracion.ListaPrecios" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <table style="width: 100%" >
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
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False"  />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False"  />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label11" runat="server" Text="Año" Visible="False" ></asp:Label>

                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select-deselect" Width="100px"  OnSelectedIndexChanged="ddlAño_SelectedIndexChanged" AutoPostBack="True" Visible="False">
                        </asp:DropDownList>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Label ID="Label12" runat="server" Text="Replicar tablas en años" Visible="False" ></asp:Label>
                        <hr />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lblAñoAnterior" runat="server" Text="Año anterior" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAñoAnterior" runat="server" CssClass="chzn-select-deselect" Width="100px" Visible="False" >
                        </asp:DropDownList>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lblAñoActual" runat="server" Text="Año actual" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <table class="auto-style3">
                            <tr>
                                <td width="110">
                                    <asp:DropDownList ID="ddlAñoActual" runat="server" CssClass="chzn-select-deselect" Width="100px" Visible="False" >
                                    </asp:DropDownList>

                                </td>
                                <td style="text-align: center">
                                    <asp:Button ID="btnEjecutar" runat="server" CssClass="botones" OnClick="btnEjecutar_Click" Text="Ejecutar" Visible="False"  />
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td></td>
                </tr>
               
            </table>
            <div>

                <asp:GridView ID="gvNovedades" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" PageSize="30" OnPageIndexChanging="gvLista_PageIndexChanging"  Width="100%">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:BoundField DataField="Novedad" HeaderText="idNovedad" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="desNovedad" HeaderText="Novedad">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="($) Destajo">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrecioTerceros" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("PrecioDestajo") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="($) Contratistas">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrecioContratistas" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("PrecioContratistas") %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="($) Otros">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrecioOtros" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("precioOtros") %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="($) Porcentaje">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="input" onkeyup="formato_numero(this)" Text='<%# Eval("Porcentaje") %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BaseSueldo">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBaseSueldo" runat="server" Checked='<%# Eval("baseSueldo") %>' OnCheckedChanged="chkBaseSueldo_CheckedChanged" AutoPostBack="True" />
                            </ItemTemplate>
                            <HeaderStyle />
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>

            <hr />
            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <div class="tablaGrilla">

                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" AllowPaging="True">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="año" HeaderText="Año">
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
