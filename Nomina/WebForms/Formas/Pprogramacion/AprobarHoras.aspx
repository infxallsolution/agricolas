<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AprobarHoras.aspx.cs" Inherits="Nomina.WebForms.Formas.Pprogramacion.AprobarHoras" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://app.infos.com/RecursosInfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/RecursosInfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <div class="loading">
        <i class="fas fa-spinner fa-spin fa-5x"></i>
    </div>
    <div class="container">
        <form id="form1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="width: 500px; height: 25px; text-align: center"><strong>Aprobación de horas extras funcionarios</strong></td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <table style="width: 100%;" id="tdCampos">
                <tr>
                    <td></td>
                    <td style="width: 190px; text-align: left">
                        <asp:Label ID="Label2" runat="server">Programación para la semana</asp:Label></td>
                    <td style="width: 150px; text-align: left">
                        <asp:TextBox ID="txtFecha" runat="server" Enabled="False" Font-Bold="True" ForeColor="Gray" CssClass="input fecha" OnTextChanged="txtFecha_TextChanged" Width="120px"></asp:TextBox></td>
                    <td style="width: 80px; text-align: left">
                        <asp:Label ID="Label1" runat="server">Cuadrilla</asp:Label>
                    </td>
                    <td style="width: 300px; text-align: left;">
                        <asp:DropDownList ID="ddlCuadrilla" runat="server" Width="350px" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" OnSelectedIndexChanged="ddlCuadrilla_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnCancelar" runat="server" CssClass="botones" OnClick="btnCancelar_Click" Text="Cancelar" ToolTip="Cancela" />
                        <asp:Button ID="lbRefresca" runat="server" CssClass="botones" OnClick="lbRefresca_Click" Text="Refrescar" ToolTip="Actualiza grilla" />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click1" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                        </td>
                    <td>

                        &nbsp;</td>
                </tr>
            </table>

            <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            <hr />
            <table style="width: 100%" id="ChkGrilla">
                <tr>
                    <td style="width:20px">
                        </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:GridView ID="gvExtras" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%">
                            <%--<asp:GridView ID="gvExtras" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" Visible="False" CssClass="Grid">--%>
                            <Columns>
                                <asp:BoundField DataField="funcionario" HeaderText="Identificación">
                                    <HeaderStyle  HorizontalAlign="Left" />
                                    <ItemStyle  HorizontalAlign="Left" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Nombre">
                                    <HeaderStyle  HorizontalAlign="Left" />
                                    <ItemStyle  HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cargo" HeaderText="Cargo">
                                    <HeaderStyle  HorizontalAlign="Left" />
                                    <ItemStyle  HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center" Width="70px" />
                                    <ItemTemplate>
                                        <asp:CheckBox  ID="chkSab" runat="server" />
                                         
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox  ID="chkSabT" runat="server" Text="Todo"   AutoPostBack="True" OnCheckedChanged="chkSabT_CheckedChanged" />
                                        
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lun">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasLun" runat="server" Visible="False" onkeyup="formato_numero(this)" Width="30px" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center"  Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mar">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasMar" runat="server" Visible="False" onkeyup="formato_numero(this)" Width="30px" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center"  Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mie">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasMie" runat="server" Visible="False" onkeyup="formato_numero(this)" Width="30px" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center"  Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasJue" runat="server" Visible="False" onkeyup="formato_numero(this)" Width="30px" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center"  Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vie">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasVie" runat="server" Visible="False" onkeyup="formato_numero(this)" Width="30px" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center"  Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sab">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasSab" runat="server" Visible="False" onkeyup="formato_numero(this)" Width="30px" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center"  Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dom">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtrasDom" runat="server" Visible="False" onkeyup="formato_numero(this)" Width="30px" CssClass="input"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle  
                                        HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center"  Width="40px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td style="width:20px">
                        </td>
                </tr>
            </table>
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
