<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarProgramacion.aspx.cs" Inherits="Nomina.WebForms.Formas.Pprogramacion.EditarProgramacion" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>

    <link rel="stylesheet" href="../../css/common.css" type="text/css" />
    <link href="../../css/Formularios.css" rel="stylesheet" />
    <script src="../../js/Numero.js" type="text/javascript"></script>
    <link href="../../css/jquery-ui.css" rel="stylesheet" />
    <link type="text/css" href="../../css/ui.multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/jqueryv1.5.1.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-ui1.8.10.min.js"></script>
    <script type="text/javascript" src="../../js/plugins/localisation/jquery.localisation-min.js"></script>
    <script type="text/javascript" src="../../js/plugins/scrollTo/jquery.scrollTo-min.js"></script>
    <script type="text/javascript" src="../../js/ui.multiselect.js"></script>


    <script type="text/javascript">
        $(function () {
            $.localise('ui-multiselect', { language: 'es', path: '../../js/locale/' });
            $(".multiselect").multiselect();
            // $('#switcher').themeswitcher();
        });
    </script>


    <%-- Este es el estilo de combobox --%>

    <link href="../../css/chosen.css" rel="stylesheet" />


    <%-- Aqui termina el estilo es el estilo de combobox --%>

    <script charset="utf-8" type="text/javascript">
        var contador = 0;
    </script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div class="principal">
            <table " style="width: 1000px">
                <tr>
                    <td class="bordes">
                        </td>
                    <td class="nombreCampos">Busqueda</td>
                    <td class="Campos" colspan="3">
                        <asp:TextBox ID="nitxtBusqueda" runat="server" CssClass="input" ToolTip="Escriba el texto para la busqueda" Width="350px"></asp:TextBox>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td class="bordes">
                        </td>
                    <td style="width: 80px; text-align: left">
                        <asp:LinkButton ID="nilbFI" runat="server" ForeColor="#003366" onclick="nilbFI_Click">Fecha Inicial</asp:LinkButton>
                    </td>
                    <td style="width: 180px; text-align: left">
                        <asp:Calendar ID="niCalendarFI" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" FirstDayOfWeek="Monday" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFI_SelectionChanged" Visible="False" Width="150px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="FloralWhite" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                        <asp:TextBox ID="nitxtFI" runat="server" CssClass="input" Width="100px"></asp:TextBox>
                    </td>
                    <td style="width: 80px; text-align: left">
                        <asp:LinkButton ID="nilbFF" runat="server" ForeColor="#003366" onclick="nilbFF_Click">Fecha Final</asp:LinkButton>
                    </td>
                    <td style="width: 180px; text-align: left">
                        <asp:Calendar ID="niCalendarFF" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" FirstDayOfWeek="Monday" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="niCalendarFF_SelectionChanged" Visible="False" Width="150px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="FloralWhite" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                        <asp:TextBox ID="nitxtFF" runat="server" CssClass="input" Width="100px"></asp:TextBox>
                    </td>
                    <td class="bordes"></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:ImageButton ID="niimbBuscar" runat="server" ImageUrl="~/Imagen/Bonotes/btnBuscar.jpg" OnClick="niimbBuscar_Click" onmouseout="this.src='../../Imagen/Bonotes/btnBuscar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnBuscarN.jpg'" ToolTip="Haga clic aqui para realizar la busqueda" />
                        <asp:ImageButton ID="nilbNuevo" runat="server" ImageUrl="~/Imagen/Bonotes/btnNuevo.jpg" OnClick="lbNuevo_Click" onmouseout="this.src='../../Imagen/Bonotes/btnNuevo.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnNuevN.jpg'" ToolTip="Habilita el formulario para un nuevo registro" />
                        <asp:ImageButton ID="lbCancelar" runat="server" ImageUrl="~/Imagen/Bonotes/btnCancelar.jpg" OnClick="lbCancelar_Click" onmouseout="this.src='../../Imagen/Bonotes/btnCancelar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnCancelarNegro.jpg'" ToolTip="Cancela la operación" Visible="False" />
                        <asp:ImageButton ID="lbRegistrar" runat="server" ImageUrl="~/Imagen/Bonotes/btnGuardar.jpg" OnClick="lbRegistrar_Click" OnClientClick="if(!confirm('Desea insertar el registro ?')){return false;};" onmouseout="this.src='../../Imagen/Bonotes/btnGuardar.jpg'" onmouseover="this.src='../../Imagen/Bonotes/btnGuardarN.jpg'" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                    </td>
                </tr>
            </table>
            <table " style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: silver;" id="Table2">
                <tr>
                    <td colspan="6" style="border-top-style: solid; border-top-width: 1px; border-color: silver">
                        <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px"></td>
                    <td style="text-align: left;" colspan="2">
                        <asp:RadioButtonList ID="rblTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="False">
                            <asp:ListItem Selected="True" Value="C">Selección por cuadrilla</asp:ListItem>
                            <asp:ListItem Value="T">Todos los funcionarios</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="Label5" runat="server" Text="Cuadrilla" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:DropDownList ID="ddlCuadrilla" runat="server" AutoPostBack="True" CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlCuadrilla_SelectedIndexChanged" Visible="False" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 50px"></td>
                </tr>
                <tr>
                    <td style="width: 50px"></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="Label3" runat="server" Text="Turno" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left">
                        <asp:DropDownList ID="ddlTurno" runat="server" Visible="False" Width="300px" CssClass="chzn-select-deselect">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px; text-align: left;">
                        <asp:LinkButton ID="lbFecha" runat="server" ForeColor="#003366" onclick="lbFecha_Click" Visible="False">Fecha</asp:LinkButton>
                        </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:Calendar ID="CalendarFecha" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" FirstDayOfWeek="Monday" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFecha_SelectionChanged" Visible="False" Width="150px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="FloralWhite" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                        <asp:TextBox ID="txtFecha" runat="server" Enabled="False" Font-Bold="True" ForeColor="Gray" ReadOnly="True" Visible="False" CssClass="input"></asp:TextBox>
                        </td>
                    <td style="width: 50px"></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: left;">
                        <asp:LinkButton ID="lblFechaEntrada" runat="server" ForeColor="#003366" onclick="lblFechaEntrada_Click" Visible="False">Fecha Entrada</asp:LinkButton>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFechaEntrada" runat="server" Visible="False" Width="150px" CssClass="input"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <asp:LinkButton ID="lblFechaSalida" runat="server" ForeColor="#003366" onclick="lbFechaOut_Click" Visible="False">Fecha Salida</asp:LinkButton>
                        </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:Calendar ID="CalendarFechaOut" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" FirstDayOfWeek="Monday" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="CalendarFechaOut_SelectionChanged" Visible="False" Width="150px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="FloralWhite" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                        <asp:TextBox ID="txtFechaSalida" runat="server" CssClass="input" Visible="False" Width="150px"></asp:TextBox>
                        </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 50px"></td>
                    <td style="width: 120px; text-align: left;">
                        <asp:Label ID="Label4" runat="server" Text="Opciones" Visible="False"></asp:Label>
                        </td>
                    <td style="width: 350px; text-align: left">
                        <asp:RadioButtonList ID="rblOpcion" runat="server" AutoPostBack="True" Height="28px" OnSelectedIndexChanged="rblEmpresa_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="False">
                            <asp:ListItem Selected="True" Value="IN">Solo Entrada</asp:ListItem>
                            <asp:ListItem Value="OUT">Entrada y salidaEntrada y salida</asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
                    <td style="width: 120px; text-align: left;">
                        </td>
                    <td style="width: 350px; text-align: left;">
                        </td>
                    <td style="width: 50px"></td>
                </tr>
                <tr>

                    <td colspan="6" style="text-align: center">
                        <div style="display: inline-block">

                            <select runat="server" id="selFuncionarios" class="multiselect" multiple="true" name="countries[]" visible="False">
                            </select>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 10px;" colspan="6">
                        <asp:Label ID="nilblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div >
                <div style="display: inline-block">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="None" CssClass="Grid" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" HeaderText="Edit" ImageUrl="~/Imagen/TabsIcon/pencil.png" Text="Botón" CommandName="Select">
                                <ItemStyle Width="20px" CssClass="Items" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Elim">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imElimina" runat="server" CommandName="Delete" ImageUrl="~/Imagen/TabsIcon/cancel.png" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado" />
                                </ItemTemplate>
                                <HeaderStyle BackColor="White" />
                                <ItemStyle Width="20px" CssClass="Items" HorizontalAlign="Center" />
                            </asp:TemplateField>
                           <asp:BoundField DataField="fecha" HeaderText="Fecha" ReadOnly="True" SortExpression="fecha"
                                    DataFormatString="{0:d}">
                                    <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="turno" HeaderText="Codigo Turno">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="desTurno" HeaderText="Turno">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="funcionario" HeaderText="Cedula" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cuadrilla" HeaderText="Cod Cuadrilla">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="desCuadrilla" HeaderText="Cuadrilla">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horaInicio" HeaderText="Hora Inicio">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horaEntrada" HeaderText="FechaEntrada">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horaSalida" HeaderText="FechaSalida">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="Estado">
                                    <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <RowStyle CssClass="rw" />
                    </asp:GridView>
                </div>

            </div>



        </div>

    </form>
    <script src="../../js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</body>
</html>