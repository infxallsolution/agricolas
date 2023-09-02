﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modulos.aspx.cs" Inherits="Administracion.WebForms.Formas.Pcontrol.Modulos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modulos</title>
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
            <table id="tdCampos" width="100%">
                <tr>
                    <td></td>
                    <td width="150px" style="text-align: left; width: 150px">
                        <asp:Label ID="Label1" runat="server" Text="Código"  CssClass="label col-2" Visible="False"></asp:Label>
                    </td>
                    <td width="400px" style="text-align: left">
                        <asp:TextBox ID="txtCodigo" runat="server"  AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input col-9" Visible="False" Width="40%"></asp:TextBox>
                        <asp:CheckBox ID="chkActivo" runat="server" Text="Activo"  CssClass="checkbox checkbox-primary" Style="padding: 0px" Visible="False" />
                        <asp:CheckBox ID="chkFormula" runat="server" Text="Formula"  CssClass="checkbox checkbox-primary" Style="padding: 0px" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label2" runat="server" Text="Descripción"  CssClass="label col-2" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDescripcion" runat="server"  CssClass="input col-9" Visible="False" Width="100%"></asp:TextBox>
                    </td>

                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label3" runat="server" Text="Url del modulo"  CssClass="label col-2" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtUrl" runat="server" CssClass="input col-9" Visible="False" Width="100%" ></asp:TextBox>
                    </td>

                    <td></td>
                </tr>
                 <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label6" runat="server" Text="Url para formatos"  CssClass="label col-2" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtUrlFormatos" runat="server" CssClass="input col-9" Visible="False" Width="100%" ></asp:TextBox>
                    </td>

                    <td></td>
                </tr>
                 <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label7" runat="server" Text="Url para reportes"  CssClass="label col-2" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtUrlReportes" runat="server" CssClass="input col-9" Visible="False" Width="100%" ></asp:TextBox>
                    </td>

                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label4" runat="server" Text="Imagen"  CssClass="label  col-2" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtImagen" runat="server" CssClass="input col-9" Visible="False" Width="100%" ></asp:TextBox>
                    </td>

                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label5" runat="server" Text="Orden en menu"  CssClass="label col-2" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtOrden" runat="server" CssClass="input col-9" TextMode="Number" ToolTip="Orden para ver los modulos" ValidateRequestMode="Enabled" Visible="False" Width="100%" ></asp:TextBox>
                    </td>

                    <td></td>
                </tr>
                
            </table>

            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" HeaderText="Edit" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                            <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                            <ItemStyle Width="20px" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Elim">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Código">
                            <ItemStyle Width="20px"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="dirUrl" HeaderText="Url">
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="orden" HeaderText="orden">
                            <ItemStyle HorizontalAlign="Right" Width="5px"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="imagen" HeaderText="Img">
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                         <asp:BoundField DataField="urlFormatos" HeaderText="UrlFormatos">
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                         <asp:BoundField DataField="urlReportes" HeaderText="UrlReportes">
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Act">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%#Eval("activo")%>' Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Act">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%#Eval("formula")%>' Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
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