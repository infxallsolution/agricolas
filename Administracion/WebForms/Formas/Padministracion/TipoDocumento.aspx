<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoDocumento.aspx.cs" Inherits="Administracion.WebForms.Formas.Padministracion.TipoDocumento" %>

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
            <table  style="width: 100%;">
                <tr>
                    <td></td>
                    <td class="text-left" width="150px">
                        <asp:Label ID="Label2" runat="server" Text="Código"  CssClass="label" Visible="False"></asp:Label></td>
                    <td class="text-left" width="400px">
                        <asp:TextBox ID="txtCodigo" runat="server"  Width="200px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input" Visible="False"></asp:TextBox>
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaNit" runat="server" Text="Maneja nit"  TabIndex="12" Visible="False" />
                                </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left">
                        <asp:Label ID="Label3" runat="server" Text="Descripción"  CssClass="label" Visible="False"></asp:Label></td>
                    <td class="text-left">
                        <asp:TextBox ID="txtDescripcion" runat="server"  Width="350px" CssClass="input" Visible="False"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left">
                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Abreviatura" Visible="False" ></asp:Label>
                    </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtAbreviatura" runat="server" CssClass="input"  Width="100px" MaxLength="3" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left">
                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Código TD" Visible="False" ></asp:Label>
                    </td>
                    <td class="text-left">
                        <asp:TextBox ID="txvCodigoTD" runat="server" CssClass="input"  Width="100px" MaxLength="3" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="text-left">
                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Código equivalencia" Visible="False" ></asp:Label>
                    </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtEquivalencia" runat="server" CssClass="input"  Width="100px" MaxLength="3" Visible="False"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                </table>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
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
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="if(!confirm('Desea eliminar el registro ?')){return false;};" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Código">
                            <ItemStyle Width="70px"  HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcionCorta" HeaderText="Abreviatura">
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigoTD" HeaderText="CodigoTD">
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:BoundField>
                              <asp:CheckBoxField DataField="mNit" HeaderText="mNit">
                            <ItemStyle Width="40px" />
                        </asp:CheckBoxField>
                            <asp:BoundField DataField="equivalencia" HeaderText="Equivalencia">
                            <ItemStyle  HorizontalAlign="Left" />
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
