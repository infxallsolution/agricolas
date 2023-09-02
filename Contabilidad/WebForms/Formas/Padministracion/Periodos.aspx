<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Periodos.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.Periodos" MasterPageFile="~/WebForms/Formas/Base.master" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="text-center">
                <label>Busqueda</label>
                <asp:TextBox ID="nitxtBusqueda" runat="server" CssClass="input" ToolTip="Escriba el texto para la busqueda" Width="600px"></asp:TextBox>
            </div>
            <asp:Panel ID="formContainer" ClientIDMode="Static" runat="server">
                <div class="text-center">
                    <asp:Button ID="niimbBuscar" CssClass="botones" Text="Buscar" runat="server" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click" />
                    <asp:Button ID="nilbNuevo" CssClass="botones" Text="Nuevo" runat="server" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click" />
                    <asp:Button ID="lbCancelar" CssClass="botones" Text="Cancelar" runat="server" ToolTip="Cancela la operación" OnClick="lbCancelar_Click" Visible="False" />
                    <asp:Button ID="lbRegistrar" CssClass="botones" Text="Guardar" runat="server" ToolTip="Guarda el nuevo registro en la base de datos" OnClick="lbRegistrar_Click" Visible="False" OnClientClick="return confirmSwal(this,'Guardado','Desea insertar el registro ?')" />

                </div>
                <div class="text-center">
                    <asp:Button ID="nibtnGenerar" runat="server" CssClass="botones" OnClick="btnGenerar_Click" Text="Generar periodos año" ToolTip="Guarda el nuevo registro en la base de datos" Width="180px" />
                    <asp:Button ID="nibtnAbrir" runat="server" CssClass="botones" OnClick="btnAbrir_Click" Text="Abrir/cerrar periodos año" ToolTip="Clic aquí para abrir los periodos de un año determinado" Width="180px" />
                    <asp:Button ID="nibtnEliminar" runat="server" CssClass="botones" OnClick="btnEliminar_Click" Text="Eliminar periodos año" ToolTip="Guarda el nuevo registro en la base de datos" Width="180px" />
                </div>
                <hr />
                <div class="row">
                    <div class="col-8 col-xs-12 offset-2 offset-xs-0" runat="server">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label ID="nilblOperacion" runat="server" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div id="divNiddlAno" runat="server" class="col-5">
                                <asp:DropDownList ID="niddlAno" runat="server" Visible="False" Width="100%" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..."></asp:DropDownList>
                            </div>
                            <div id="divChkCerrarAño" runat="server" class="col-4">
                                <asp:CheckBox ID="nichkCerrarAño" runat="server" Text="Cerrado" Visible="False" />
                            </div>
                            <div id="divNitxtAno" runat="server" class="col-9">
                                <asp:TextBox ID="nitxtAno" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox>
                            </div>
                        </div>

                        <div class="text-center">
                            <asp:Button ID="nilbEjecutar" CssClass="botones" Text="Ejecutar" runat="server" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="nilbEjecutar_Click" Visible="False" />
                            <asp:Button ID="nilblCancelar" CssClass="botones" Text="Cancelar" runat="server" ToolTip="Cancela la operación" OnClick="nilblCancelar_Click" Visible="False" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-6 col-xs-12 offset-3 offset-xs-0" runat="server">
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label1" runat="server" Text="Año" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txvAño" runat="server" Visible="False" Width="100%" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" CssClass="input" onkeyup="formato_numero(this)" MaxLength="6"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label2" runat="server" Text="Mes" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:DropDownList ID="ddlMes" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" style="left: 0px; top: 0px">
                                    <asp:ListItem Value="1">Enero</asp:ListItem>
                                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                                    <asp:ListItem Value="4">Abril</asp:ListItem>
                                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                                    <asp:ListItem Value="6">Junio</asp:ListItem>
                                    <asp:ListItem Value="7">Julio</asp:ListItem>
                                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                                    <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                    <asp:ListItem Value="13">Cierre de año</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="lblFechaIni" runat="server" Visible="False" Width="100%">Fecha inicial</asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtFechaIni" runat="server" CssClass="input fecha" ToolTip="Formato fecha (dd/mm/yyyy)" Visible="False" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="lblFechaFinal" runat="server" Visible="False" Width="100%">Fecha final</asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="input fecha" ToolTip="Formato fecha (dd/mm/yyyy)" Visible="False" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="offset-2 col-10">
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCerrado" runat="server" Text="Periodo Cerrado." Visible="False" Width="100%" />
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>
            <hr />
            <div class="text-center">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="imEdit" CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt" CommandName="Select" ToolTip="Edita el registro seleccionado"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px" CssClass="action-item" />
                                <HeaderStyle CssClass="action-item" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash" CommandName="Delete" OnClientClick="return confirmSwal(this,'Eliminar','Desea eliminar el registro ?')" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                <HeaderStyle CssClass="action-item" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="año" HeaderText="Año" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mes" HeaderText="Mes">
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="periodo" HeaderText="Periodo" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaInicial" HeaderText="Fecha inicial" DataFormatString="{0:dd/MM/yyy}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaFinal" HeaderText="Fecha final" DataFormatString="{0:dd/MM/yyy}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="cerrado" HeaderText="Cerrado">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:CheckBoxField>
                        </Columns>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="footer" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

