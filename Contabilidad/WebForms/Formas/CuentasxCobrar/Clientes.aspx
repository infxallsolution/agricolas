<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Contabilidad.WebForms.Formas.CuentasxCobrar.Clientes" %>


<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <title>Seguridad</title>
    <link href="../../css/Formularios.css" rel="stylesheet" />
     <script type="text/javascript" >
         javascript: window.history.forward(1);
     </script>
     </head>
    <body>
        <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="text-center">
                <label>Busqueda</label>
                <asp:TextBox ID="nitxtBusqueda" runat="server" Width="350px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox>
            </div>
                     <asp:Panel ID="formContainer" ClientIDMode="Static" runat="server">
                <div class="text-center">
                    <asp:Button ID="niimbBuscar" CssClass="botones" runat="server" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="niimbBuscar_Click" />
                    <asp:Button ID="nilbNuevo" CssClass="botones" runat="server" Text="Nuevo" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbNuevo_Click" />
                    <asp:Button ID="lbCancelar" CssClass="botones" runat="server" Text="Cancelar" ToolTip="Haga clic aqui para realizar la busqueda" OnClick="lbCancelar_Click" Visible="False"  />
                    <asp:Button ID="lbRegistrar" CssClass="botones" runat="server" Text="Guardar" ToolTip="Habilita el formulario para un nuevo registro" OnClick="lbRegistrar_Click"  OnClientClick="return confirmSwal(this,'Guardado','Desea insertar el registro ?')" Visible="False" />
                </div>
                <hr />
                <div class="row">
                    <div class="col-8 col-xs-12 offset-2 offset-xs-0" runat="server">
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label4" runat="server" Text="Tercero"  Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:DropDownList ID="ddlTercero" runat="server"  Width="100%" AutoPostBack="True" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Style="left: 0px; top: 5px" OnSelectedIndexChanged="ddlTercero_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label1" runat="server" Text="Código"  Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged"  Width="100%" CssClass="input" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label2" runat="server" Text="Descripción"  Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtDescripcion" runat="server"  Width="100%" CssClass="input" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label8" runat="server" Text="Clase de cliente" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:DropDownList ID="ddlClaseCliente" runat="server"  Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Visible="False"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label10" runat="server" Text="Contácto" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtContacto" runat="server"  Width="100%" CssClass="input" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                          <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label5" runat="server" Text="País" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:DropDownList ID="ddlPais" AutoPostBack="true"  runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  TabIndex="14" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                               <div class="col-2">
                                <asp:Label ID="Label3" runat="server" Text="Depto/Provincia" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:DropDownList ID="ddlMunicipio" AutoPostBack="true"  runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  TabIndex="14" OnSelectedIndexChanged="ddlMunicipio_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label22" runat="server" Text="Ciudad" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:DropDownList ID="ddlCiudad" runat="server" Width="100%" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  TabIndex="14" Visible="False"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label19" runat="server" Text="Dirección" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtDireccion" runat="server"  Width="100%" CssClass="input" TabIndex="15" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label23" runat="server" Text="Teléfono" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtTelefono" runat="server"  Width="100%" CssClass="input" TabIndex="12" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label24" runat="server" Text="Email" Width="100%" Visible="False"></asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox ID="txtEmail" runat="server"  Width="100%" CssClass="input" TabIndex="17" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="offset-2 col-10">
                                <asp:CheckBox ID="chkActivo" runat="server" Text=" Activo"  Visible="False" />
                            </div>
                        </div>
                                                <div class="row">
                            <div class="col-12">
                                <asp:GridView ID="gvClaseIR" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%"  OnRowUpdating="gvClaseIR_RowUpdating">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sel">
                                            <ItemTemplate>
                                                <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Items" Width="20px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo" HeaderText="Id">
                                            <ItemStyle CssClass="Items" Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Clase">
                                            <ItemStyle CssClass="Items" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Valor">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlConcepto" runat="server" Width="180px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlConcepto_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Items" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="llave">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlLlave" runat="server" Width="250px" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Enabled="False">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Items" Width="250px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead" />
                                    <PagerStyle CssClass="footer" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
                <asp:GridView ID="gvLista" runat="server" PageSize="20" AllowPaging="True" Width="100%" CssClass="table table-bordered table-sm  table-hover table-striped grillas" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting" AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging">
                    <AlternatingRowStyle CssClass="alt" />
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
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
                        <asp:BoundField DataField="idTercero" HeaderText="idTercero" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo" HeaderText="Codigo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="clase" HeaderText="Clase" SortExpression="Clase">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreclase" HeaderText="NombreCalse" SortExpression="NombreCalse">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="contacto" HeaderText="Contácto">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreCiudad" HeaderText="NombreCiudad">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefono">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="Email">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="pais" HeaderText="idPais">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="departamento" HeaderText="Dto/Prov">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activo" HeaderText="Activo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="5px" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="thead" />
                    <PagerStyle CssClass="footer" />
                </asp:GridView>
            </div>
        </div>
    </form>
    </body>
</html>
