<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lotes.aspx.cs" Inherits="Agronomico.WebForms.Formas.Padministracion.Lotes" %>

<%@ OutputCache Location="None" %>
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
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label11" runat="server" Text="Año Siembra" Visible="False" ></asp:Label>

                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvAñoSiembra" runat="server"  Width="150px"   onkeyup="formato_numero(this)"  CssClass="input" MaxLength="20" Visible="False" ></asp:TextBox>

                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label4" runat="server" Text="Finca" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList data-placeholder="Seleccione una opción..." ID="ddlFinca" runat="server" class="chzn-select" Width="95%"  CssClass="chzn-select" Visible="False">
                        </asp:DropDownList>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label2" runat="server" Text="Código" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtCodigo" runat="server"  Width="150px" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input" MaxLength="20" ToolTip="Campo de cinco (5) caracteres" Visible="False"></asp:TextBox>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActivo" runat="server" Text="Activo" Visible="False"  />
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDesarrollo" runat="server" Text="En desarrollo" Visible="False"  />

                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label3" runat="server" Text="Descripción" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtDescripcion" runat="server"  Width="95%" CssClass="input" MaxLength="550" Visible="False"></asp:TextBox>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSession" runat="server" Text="Sección"  AutoPostBack="True" OnCheckedChanged="chkSession_CheckedChanged" ToolTip="Habilitar si el lote maneja sección" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList data-placeholder="Seleccione una opción..." ID="ddlSeccion" runat="server" CssClass="chzn-select" Width="95%" Visible="False" >
                        </asp:DropDownList>

                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label5" runat="server" Text="Variedad" Visible="False" ></asp:Label>

                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList data-placeholder="Seleccione una opción..." CssClass="chzn-select" ID="ddlVariedad" runat="server" Width="95%" Visible="False" >
                        </asp:DropDownList>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label16" runat="server" Text="No. (Ha) brutas" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvHaBruta" runat="server"  Width="150px" CssClass="input" onkeyup="formato_numero(this)" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label79" runat="server" Text="Distancia Siembra" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvDistancia" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txvDistancia_TextChanged1"  Width="150px" onkeyup="formato_numero(this)" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label78" runat="server" Text="Densidad" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvDensidad" runat="server"  Width="150px" CssClass="input" onkeyup="formato_numero(this)" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label6" runat="server" Text="No plantas brutas" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvPalmasBruta" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txvPalmasBruta_TextChanged1"  Width="150px" onkeyup="formato_numero(this)" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label83" runat="server" Text="No. (Ha) netas" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvHaNetas" runat="server"  Width="150px" CssClass="input" onkeyup="formato_numero(this)" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label8" runat="server" Text="Plantas Producción" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txvPalmasProduccion" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="txvPalmasProduccion_TextChanged1"  Width="150px" onkeyup="formato_numero(this)" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label81" runat="server" Text="Tipo Canal" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:DropDownList ID="ddlTipoCanal" runat="server"  Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select" Visible="False">
                        </asp:DropDownList>
                         <asp:LinkButton runat="server" ID="imbCargarCanal" CssClass="btn btn-default btn-sm btn-success fa fa-plus "
                        ToolTip="Agrega sello" OnClick="imbCargarCanal_Click" Visible="False" ></asp:LinkButton>
                       
                    </td>
                    <td style="width: 150px; text-align: left;">
                        <asp:Label ID="Label10" runat="server" Text="No. de lineas" Visible="False" ></asp:Label>
                    </td>
                    <td style="text-align: left; width: 400px">
                        <asp:TextBox ID="txtNoLinea" runat="server"  Width="150px" CssClass="input" onkeyup="formato_numero(this)" Visible="False"></asp:TextBox>
                         <asp:LinkButton runat="server" ID="imbCargarLineas" CssClass="btn btn-default btn-sm btn-success fa fa-plus "
                        ToolTip="Agrega sello" OnClick="imbCargar_Click" Visible="False" ></asp:LinkButton>
                    </td>
                    <td></td>
                </tr>
            </table>
            <div style="padding: 10px;">

                <div style="display: inline-block; padding: 0px 10px 0px 10px; vertical-align: top;">
                    <asp:GridView ID="gvCanal" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvCanal_RowDeleting" PageSize="30" Width="510px" >
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                <HeaderStyle CssClass="action-item" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="tipoCanal" HeaderText="TipoCanal" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="NombretipoCanal" HeaderText="NombreTipoCanal" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero" HeaderText="No." ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="5px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="(mts)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMetros" runat="server" onkeyup="formato_numero(this)" Text='<%# Eval("metros") %>' CssClass="input" Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="footer" />
                    </asp:GridView>
                </div>
                <div style="display: inline-block; padding: 0px 10px 0px 10px">
                    <asp:GridView ID="gvLineas" runat="server" CssClass="table table-bordered table-sm  table-hover table-striped grillas" AutoGenerateColumns="False"  PageSize="30" Width="350px"  OnRowDeleting="gvLineas_RowDeleting">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" OnClientClick="return confirmSwal(this,'Advertencia','¿Desea eliminar el registro ?');" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                <HeaderStyle CssClass="action-item" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="linea" HeaderText="Linea" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="NoPlantasBrutas">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNoPalma" runat="server" onkeyup="formato_numero(this)" Text='<%# Eval("NoPalma") %>' CssClass="input"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="N-S">
                                <ItemTemplate>
                                    <asp:CheckBox  ID="chkOrden" runat="server" Checked='<%# Eval("Orden") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="footer" />
                    </asp:GridView>
                </div>

            </div>
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
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="finca" HeaderText="Finca">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="nombreFinca" HeaderText="NombreFinca">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="seccion" HeaderText="Sección">
                            <ItemStyle ></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="variedad" HeaderText="Veriedad">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                          <asp:BoundField DataField="nombrevariedad" HeaderText="NombreVariedad">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="palmasBrutas" HeaderText="PlantasBrut">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="palmasProduccion" HeaderText="PlantasProd">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="añoSiembra" HeaderText="Año">
                            <ItemStyle Width="30px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="NoLineas" HeaderText="NoLinea">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="palmasBrutas" HeaderText="PlantasB">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="palmasProduccion" HeaderText="PlantasP">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="hBrutas" HeaderText="haBrutas">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="hNetas" HeaderText="haNetas">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="densidad" HeaderText="densidad">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dSiembra" HeaderText="dSiembra">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaSeccion" HeaderText="mSec">
                            <ItemStyle  HorizontalAlign="Center" Width="30px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="desarrollo" HeaderText="Des">
                            <ItemStyle Width="20px" />
                        </asp:CheckBoxField>
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
