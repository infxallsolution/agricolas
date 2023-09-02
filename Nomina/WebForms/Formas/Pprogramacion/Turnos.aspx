<%@ Page Language="C#" UnobtrusiveValidationMode="None" CodeBehind="Turnos.aspx.cs" Inherits="Nomina.WebForms.Formas.Pprogramacion.Turnos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
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
                        <asp:Button ID="niimbBuscar" runat="server" CssClass="botones" OnClick="niimbBuscar_Click" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" OnClick="nilbNuevo_Click" Text="Nuevo Registro" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            
            <hr />
            <table style="width: 100%;" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label1" runat="server" Text="Código" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 370px">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 160px">
                                    <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txtConcepto_TextChanged" Visible="False" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label2" runat="server" Text="Descripción" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 370px">
                        <asp:TextBox ID="txtDescripcion" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label3" runat="server" Text="Hora de Inicio" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 370px">
                        <table cellpadding="0" " style="width: 360px">
                            <tr>
                                <td style="width: 70px">
                                    <asp:Label ID="Label7" runat="server" Text="Hora (hh)" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 90px">
                                    <asp:TextBox ID="txvHoraInicio" runat="server" CssClass="input" ToolTip="Digite aquí la hora de inicio del turno en formato de hora militar en el rango entre 0 y 23" Visible="False" Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 110px">
                                    <asp:Label ID="Label6" runat="server" Text="Minutos (mm)" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 90px">
                                    <asp:TextBox ID="txvMinutoInicio" runat="server" CssClass="input" ToolTip="Digite aquí los minutos en el rango entre 0 y 59" Visible="False" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:RangeValidator ID="RangeValidatorMinuto" runat="server" ControlToValidate="txvMinutoInicio" Display="Dynamic" ErrorMessage="El rango de minutos debe estar entre 0 y 59" MaximumValue="59" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                        <asp:RangeValidator ID="RangeValidatorHora" runat="server" ControlToValidate="txvHoraInicio" Display="Dynamic" ErrorMessage="El rango de horas debe estar entre 0 y 23" MaximumValue="23" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left; width: 150px">
                        <asp:Label ID="Label4" runat="server" Text="Horas Turno (00 - 24)" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 370px">
                        <asp:TextBox ID="txvHorasTurno" runat="server" CssClass="input" Visible="False" Width="100px"></asp:TextBox>
                        <br />
                        <asp:RangeValidator ID="RangeValidatorHorasTurno" runat="server" ControlToValidate="txvHorasTurno" Display="Dynamic" ErrorMessage="El rango de horas turno debe estar entre 0 y 23" MaximumValue="23" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
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
                                <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                            <HeaderStyle CssClass="action-item" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Código" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True" SortExpression="descripcion">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="horaInicio" HeaderText="HoraInicio">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="horas" HeaderText="HorasTurno">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="Items" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </form>
    </div>
       <script src="http://app.infos.com/recursosinfos/lib/chosen-js/chosen.jquery.js" type="text/javascript"></script>
    <script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/lib/chosen-js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/lou-multi-select/js/jquery.multi-select.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/moment/moment.js"></script>
    <script src="http://app.infos.com/RecursosInfos/lib/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://app.infos.com/recursosinfos/lib/sweetalert2/dist/sweetalert2.min.js"></script>
    <script type="text/javascript" src="http://app.infos.com/RecursosInfos/js/Core/core.js"></script>


</body>
</html>
