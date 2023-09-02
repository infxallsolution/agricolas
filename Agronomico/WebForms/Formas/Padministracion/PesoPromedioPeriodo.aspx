<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PesoPromedioPeriodo.aspx.cs" Inherits="Agronomico.WebForms.Formas.Padministracion.PesoPromedioPeriodo" %>

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
                    <td ></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label11" runat="server" Text="Año" Visible="False" ></asp:Label>

                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlAño" runat="server" CssClass="chzn-select-deselect" Width="100px"  OnSelectedIndexChanged="ddlAño_SelectedIndexChanged" AutoPostBack="True" Visible="False">
                        </asp:DropDownList>

                        <asp:Label ID="Label12" runat="server" Text="Mes" Visible="False" ></asp:Label>
                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="chzn-select-deselect" Width="100px" Visible="False" >
                        </asp:DropDownList>

                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label4" runat="server" Text="Finca" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList data-placeholder="Seleccione una opción..." ID="ddlFinca" runat="server" class="chzn-select" Width="350px"  AutoPostBack="True" OnSelectedIndexChanged="ddlFinca_SelectedIndexChanged" CssClass="chzn-select-deselect" Visible="False">
                        </asp:DropDownList>

                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lbSecion" runat="server" Text="Sección" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList data-placeholder="Seleccione una opción..." ID="ddlSecciones" runat="server" class="chzn-select" Width="350px"  OnSelectedIndexChanged="ddlSecciones_SelectedIndexChanged" AutoPostBack="True" CssClass="chzn-select-deselect" Visible="False">
                        </asp:DropDownList>

                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lbLote" runat="server" Text="Lote" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList data-placeholder="Seleccione una opción..." ID="ddlLote" runat="server" class="chzn-select" Width="350px"  CssClass="chzn-select-deselect" Visible="False">
                        </asp:DropDownList>

                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="Label14" runat="server" Text="Peso  Rac. Promedio(Kg)" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvPesoPromedio" runat="server"  Width="150px" CssClass="input" onkeyup="formato_numero(this)" Visible="False">0</asp:TextBox>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAutomatico" runat="server" Text="Automatico"  AutoPostBack="True" OnCheckedChanged="chkAutomatico_CheckedChanged" Visible="False" />
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lbFechaInicial" runat="server" Visible="False"  >Fecha inicial</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaInicial" runat="server" class="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)"  Width="150px" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtFechaInicial_TextChanged" CssClass="input fecha" Visible="False"></asp:TextBox>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 140px">
                        <asp:Label ID="lbFechaFinal" runat="server" Visible="False"  >Fecha final</asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtFechaFinal" runat="server" class="input" Font-Bold="True" ToolTip="Formato fecha (dd/mm/yyyy)"  Width="150px" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtFechaFinal_TextChanged" CssClass="input fecha" Visible="False"></asp:TextBox>
                    </td>
                    <td ></td>
                </tr>
                <tr>
                    <td ></td>
                    <td style="text-align: left; width: 140px"></td>
                    <td style="text-align: right">

                        <asp:Button ID="lbGenerar" runat="server" CssClass="botones" OnClick="lbGenerar_Click" Text="Generar" ToolTip="Genera el peso promedio del mes" Visible="False"  />

                       
                    </td>
                    <td ></td>
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
                            <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mes" HeaderText="Mes" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="finca" HeaderText="Finca" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="50px" />
                            </asp:BoundField>
                        <asp:BoundField DataField="nombrefinca" HeaderText="NombreFinca" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"   />
                            </asp:BoundField>
                            <asp:BoundField DataField="seccion" HeaderText="Bloque">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lote" HeaderText="Lote">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pesoRacimo" HeaderText="PesoRacPro (Kg) ">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="automatico" HeaderText="Automatico">
                                <ItemStyle HorizontalAlign="Center" Width="40px"  />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="fechaInicial" HeaderText="FechaInicial" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaFinal" HeaderText="FechaFinal" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left"  />
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
