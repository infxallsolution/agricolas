<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Puc.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Padministracion.Puc" %>

<%@ OutputCache Location="None" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<httml>
    <head runat="server">
        <title>Plan Unico de cuentas</title>
        <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    </head>

    <body style="text-align: center">
        <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="text-center">
                <label>Busqueda</label>
                <asp:TextBox ID="nitxtBusqueda" runat="server" Width="600px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox>
            </div>
            <asp:Panel ID="formContainer" ClientIDMode="Static" runat="server">
                <div class="text-center">
                    <asp:Button ID="niimbBuscar" Text="Buscar" CssClass="botones" runat="server" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click" />
                    <asp:Button ID="nilbNuevo" Text="Nuevo" CssClass="botones" runat="server" ToolTip="Habilita el formulario para un nuevo registro" OnClick="nilbNuevo_Click" />
                    <asp:Button ID="lbCancelar" Text="Cancelar" CssClass="botones" runat="server" ToolTip="Cancela la operación" OnClick="lbCancelar_Click" Visible="False" />
                    <asp:Button ID="lbRegistrar" Text="Guardar" CssClass="botones" runat="server" ToolTip="Guarda el nuevo registro en la base de datos" OnClick="lbRegistrar_Click" Visible="False" OnClientClick="return confirmSwal(this,'Guardado','Desea insertar el registro ?')" />
                </div>
                <hr />
                <div class="row">
                    <div class="offset-2 col-8" runat="server">
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label7" runat="server" Text="Plan de cuenta" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlPlanCuenta" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%" Style="left: 0px; top: 0px"></asp:DropDownList>
                            </div>
                            <div class="offset-2 col-4">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label1" runat="server" Text="Código Cuenta" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:TextBox ID="txtCodigo" runat="server" Visible="False" Width="100%" AutoPostBack="True" OnTextChanged="txtConcepto_TextChanged" onkeypress="return soloNumeros(event)" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="Label5" runat="server" Text="Nivel de la cuenta" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:TextBox ID="txtNivel" runat="server" Visible="False" Width="100%" CssClass="input" onkeyup="formato_numero(this)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label2" runat="server" Text="Mayor" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:TextBox ID="txtRaiz" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="Label3" runat="server" Text="Descripción" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:TextBox ID="txtNombre" runat="server" Visible="False" Width="100%" CssClass="input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label4" runat="server" Text="Naturaleza" Visible="False" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlNaturaleza" runat="server" Visible="False" Width="100%" CssClass="chzn-select-deselect">
                                    <asp:ListItem Value="D">Debito</asp:ListItem>
                                    <asp:ListItem Value="C">Credito</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="Label6" runat="server" Text="Tipo cuenta" Visible="False"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlTipoCuenta" runat="server" Visible="False" Width="100%" CssClass="chzn-select-deselect">
                                    <asp:ListItem Value="B">Balance</asp:ListItem>
                                    <asp:ListItem Value="E">Estado Resultado</asp:ListItem>
                                    <asp:ListItem Value="O">Otros</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCcosto" runat="server" Text="Centro costo" Visible="False" AutoPostBack="True" OnCheckedChanged="chkCcosto_CheckedChanged" Width="100%" />
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlGrupoCC" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Visible="False" Width="100%"></asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDisponible" runat="server" Text="Disponible" Visible="False" AutoPostBack="True" OnCheckedChanged="chkDisponible_CheckedChanged" Width="100%" />
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlTipoDisponible" runat="server" Visible="False" Width="100%" CssClass="chzn-select-deselect">
                                    <asp:ListItem Value="N">Ninguno</asp:ListItem>
                                    <asp:ListItem Value="B">Bancos</asp:ListItem>
                                    <asp:ListItem Value="C">Caja</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkTercero" runat="server" Text="Maneja tercero" Visible="False" AutoPostBack="True" OnCheckedChanged="chkTercero_CheckedChanged" Width="100%" />
                            </div>
                            <div class="col-6">
                                <asp:DropDownList ID="ddlTipoManejaTercero" runat="server" Visible="False" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" CssClass="chzn-select-deselect">
                                    <asp:ListItem Value="SA">Sólo acumulado</asp:ListItem>
                                    <asp:ListItem Value="SS">Si, saldos</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTipoSaldo" runat="server" Visible="False" Width="100%" CssClass="chzn-select-deselect">
                                    <asp:ListItem Value="C">Cliente</asp:ListItem>
                                    <asp:ListItem Value="P">Proveedor</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-4">
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkActio" runat="server" Text="Activo" Visible="False" Checked="True" Width="100%" />
                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAuxiliar" runat="server" Text="Auxiliar" Visible="False" Checked="True" AutoPostBack="True" OnCheckedChanged="chkAuxiliar_CheckedChanged" />

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <hr />
            <asp:Panel ID="formImpuesto" runat="server" Visible="False">
                <div class="row">
                    <div class="offset-2 col-8">
                        <div class="text-center">
                            <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkImpuesto" runat="server" Text="Impuestos" Visible="False" AutoPostBack="True" OnCheckedChanged="chkImpuesto_CheckedChanged" Width="100%" />
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="lblDepartamento13" runat="server" Text="Tipo" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlTipoIR" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoIR_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem Value="ID">Impuesto descontable</asp:ListItem>
                                    <asp:ListItem Value="IG">Impuesto generado</asp:ListItem>
                                    <asp:ListItem Value="RF">Retención a favor</asp:ListItem>
                                    <asp:ListItem Value="RP">Retención por pagar</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="lblDepartamento14" runat="server" Text="Llave impuesto/retención" Width="100%"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlCalseIR" runat="server" CssClass="chzn-select-deselect" data-placeholder="Seleccione una opción..." Width="100%" Visible="False"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label9" runat="server" Text="Notas" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtNotas" runat="server" CssClass="input" Height="60px" TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnRowDeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging">
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
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="raiz" HeaderText="Raiz" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="naturaleza" HeaderText="Natza">
                            <HeaderStyle />
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nivel" HeaderText="Nivel">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoBalanceResultado" HeaderText="Tipo">
                            <HeaderStyle />
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Act">
                            <HeaderStyle />
                            <ItemStyle Width="10px" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </div>
    </form>
    </body>
</httml>


