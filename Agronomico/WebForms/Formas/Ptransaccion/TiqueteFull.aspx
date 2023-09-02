<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TiqueteFull.aspx.cs" Inherits="Agronomico.WebForms.Formas.Ptransaccion.TiqueteFull" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Transacciones Agro</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <style>
        .modal-full {
            text-align: center;
            min-width: 90%;
            margin-bottom: 20px;
            left: 3%;
        }

        .btn-success {
            font-size: 6pt !important;
        }

        .modal-full .modal-content {
            min-height: 100%;
        }

        .fieldsetdetalle {
            border: 1px solid #ddd !important;
            margin: 0;
            xmin-width: 0;
            border-radius: 4px;
            padding-left: 10px !important;
            background-color: lightgray;
            display: inline-grid;
            font-size: 7pt !important;
        }

            .fieldsetdetalle legend {
                font-size: 14px;
                font-weight: bold;
                margin-bottom: 0px;
                width: 70%;
                border: 1px solid #ddd;
                border-radius: 4px;
                padding: 5px 5px 5px 10px;
                background-color: #ffffff;
            }

        fieldset {
            border: 1px solid #ddd !important;
            margin: 0;
            xmin-width: 0;
            padding: 10px;
            position: relative;
            border-radius: 4px;
            padding-left: 10px !important;
            display: inline-block;
        }



        legend {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 0px;
            width: 35%;
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 5px 5px 5px 10px;
            background-color: #ffffff;
        }

        .ui-widget.ui-widget-content {
            border: 1px solid #c5c5c5 !important;
        }

        .ui-widget-content {
            border: 1px solid #dddddd !important;
            background: #ffffff !important;
            color: #333333 !important;
            min-height: 100px !important;
            overflow-y: scroll;
            width: 100%;
            max-height: 120px !important;
        }

            .ui-widget-content a {
                color: #333333 !important;
            }

        .contenedor {
            background: #ffffff !important;
            margin-bottom: 2px;
            display: inline;
            border-bottom-style: solid;
            border-color: darkblue;
            border-width: 1px;
        }

        #transaccionDetalle {
            width: 100%;
            max-height: 500px;
            overflow-y: scroll;
        }

        ui-corner-bottom {
            border-style: none;
        }

        .tabs {
            min-height: 170px !important;
            width: 100%;
            display: inline-block !important;
            border-style: none !important;
            padding-top: 2em !important;
        }

        .ui-widget-content .ui-icon {
            background-image: url("images/ui-icons_444444_256x240.png");
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <div class="loading">
            <i class="fas fa-spinner fa-spin fa-5x"></i>
        </div>
        <form id="form1" runat="server" class="card-block card text-justify">
            <asp:HiddenField ID="hfEdicion" runat="server" />
            <div id="modalTercero" role="dialog" class="modal" tabindex="-1">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 id="modalTitulo" class="modal-title text-center w-100"></h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-1"></div>
                                    <div class="col-2">Funcionarios</div>
                                    <div class="col-8">
                                        <asp:DropDownList multiple ID="ddlTerceros" Width="100%" Height="100%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1"></div>
                                </div>
                                <div class="row">
                                    <div class="col-1"></div>
                                    <div class="col-2">Actividad/Labor</div>
                                    <div class="col-8">
                                        <asp:DropDownList ID="ddlActividad" Width="100%" Height="100%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1"></div>
                                </div>
                                <div class="row">
                                    <div class="col-1"></div>
                                    <div class="col-2">Fecha</div>
                                    <div class="col-8 text-left">
                                        <input type="text" id="txtFechaDetalle" class="form-control input w-100 " />
                                    </div>
                                    <div class="col-1"></div>
                                </div>
                                <div class="row">
                                    <div class="col-1"></div>
                                    <div class="col-2">Jornales</div>
                                    <div class="col-8 text-left">
                                        <input type="text" id="txtJornalesDetalle" value="0" class="form-control input w-100 numero" />
                                    </div>
                                    <div class="col-1"></div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnAgregarTerceros" class="btn btn-primary">Agregar Terceros</button>
                                <button type="button" id="btnCancelarAgregar" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalTiquete" role="dialog" class="modal" tabindex="-1">
                <div class="modal-dialog modal-full text-center d-inline-block">
                    <div class="modal-content modal-full text-center d-inline-block">
                        <div class="modal-header">
                            <h5 class="modal-title">Filtro de tiquete</h5>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-1">
                                    # Tiquete
                                </div>
                                <div class="col-2">
                                    <input type="text" id="txtTiqueteFiltro" class="form-control input w-100" />
                                </div>
                                <div class="col-1">
                                    Extractora
                                </div>
                                <div class="col-4">
                                    <asp:DropDownList ID="ddlExtractoraFiltro" Height="100%" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-2">
                                    <a id="iBusquedaTiqueteFiltro" class="btn btn-sm btn-primary text-white">Buscar Tiquete <i class="fas fa-search iNivel"></i></a>
                                </div>
                            </div>
                            <div class="row m-2">
                                <div class="col-12 text-center">
                                    <table id="tTiquetes" style="font-size: 11pt;" cellspacing="0" rules="all" border="1" class="text-justify table table-bordered table-sm  table-hover table-striped grillas w-100 border-0">
                                        <tbody>
                                            <tr class="thead">
                                                <th scope="col" style="width: 5%">Sel</th>
                                                <th scope="col" style="width: 5%">#Tiquete</th>
                                                <th scope="col" style="width: 25%">Extractora</th>
                                                <th scope="col" style="width: 10%">Fecha</th>
                                                <th scope="col" style="width: 10%">Neto</th>
                                                <th scope="col" style="width: 10%">Racimos</th>
                                                <th scope="col" style="width: 10%">Sacos</th>
                                                <th scope="col" style="width: 25%">Conductor</th>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:HiddenField ID="hfEmpresa" runat="server" />
                    <asp:Button ID="niimbRegistro" Text="Registro" OnClick="niimbRegistro_Click" CssClass="botones" runat="server" />
                    <asp:Button ID="imbConsulta" runat="server" OnClick="imbConsulta_Click" CssClass="botones" Text="Consulta" />
                </div>
            </div>
            <div id="upGeneral">
                <div class="card-block card">
                    <div class="row text-center">
                        <div class="col-12 ">
                            <asp:Button ID="nilbNuevo" runat="server" CssClass="botones" Text="Nuevo" ToolTip="Habilita el formulario para un nuevo registro" />
                            <asp:Button ID="lbGuardar" runat="server" CssClass="botones" Text="Guardar" ToolTip="Habilita el formulario para un nuevo registro" />
                            <asp:Button ID="lbCancelar" runat="server" CssClass="botones" Text="Cancelar" ToolTip="Cancela la operación" OnClick="lbCancelar_Click" Visible="False" />
                        </div>
                    </div>
                    <div id="edicion" class="alert alert-warning text-center w-100" style="display: none" role="alert">
                        <asp:HiddenField ID="hfTipo" runat="server" />
                        <asp:HiddenField ID="hfNumero" runat="server" />
                        <asp:HiddenField ID="hfTiquete" runat="server" />
                    </div>
                    <fieldset id="encabezado" class="my-2">
                        <legend>Tiquete</legend>
                        <div class="row">
                            <div class="col-1">
                                # Tiquete
                            </div>
                            <div class="col-2">
                                <input type="text" id="txtTiquete" class="form-control input w-100" />
                            </div>
                            <div class="col-2">
                                <a id="iBuscarTiquete" class="btn btn-sm btn-primary text-white">Buscar Tiquete <i class="fas fa-search iNivel"></i></a>
                            </div>
                            <div class="col-2 text-center">
                                ¿Tiquete externo?
                            </div>
                            <div class="col-1">
                                <input type="checkbox" id="chkInterno" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1">
                                Extractora
                            </div>
                            <div class="col-4">
                                <asp:DropDownList Width="100%" disabled="disabled" data-placeholder="Seleccione una opción" ID="ddlExtractora" CssClass="chzn-select-deselect" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-1">
                                Fecha
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" id="txtFecha" class="form-control input w-100 " />
                            </div>

                            <div class="col-1">
                                Neto(KG)
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" id="txtNeto" class="form-control input w-100 numero" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-1">
                                Conductor
                            </div>
                            <div class="col-4">
                                <input type="hidden" id="hfConductor" />
                                <input type="text" disabled="disabled" id="txtConductor" class="form-control input w-100 " />
                            </div>
                            <div class="col-1">
                                Racimos
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" id="txtRacimos" class="form-control input w-100 numero" />
                            </div>
                            <div class="col-1">
                                Sacos
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" id="txtSacos" class="form-control input w-100 numero" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1">
                                Vehículo
                            </div>
                            <div class="col-4">
                                <input type="text" disabled="disabled" id="txtVehiculo" class="form-control input w-100 " />
                            </div>
                            <div class="col-1">
                                Remolque
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" id="txtRemolque" class="form-control input w-100 numero" />
                            </div>
                        </div>
                    </fieldset>
                    <fieldset id="detalle" class="my-2">
                        <legend>Detalle</legend>
                        <div class="row">
                            <div class="col-1">Nivel</div>
                            <div class="col-4">
                                <asp:DropDownList Width="100%" data-placeholder="Seleccione una opción" ID="ddlNivel" CssClass="chzn-select-deselect" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-1 text-left">
                                <div class="row">
                                    <div class="col-6">
                                        <a id="btnFiltrar" class="btn btn-primary btn-sm text-white"><i style="font-size: 7pt" class="fas fa-filter"></i></a>
                                    </div>
                                    <div class="col-6">
                                        <a id="iNivel" class="btn btn-sm btn-primary text-white"><i style="font-size: 7pt" class="fas fa-redo-alt iNivel"></i></a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-1">Racimos</div>
                            <div class="col-1 text-left">
                                <asp:TextBox ID="txtRacimosDetalle" Text="0" onkeyup="formato_numero(this)" Width="100%" CssClass="input text-right" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-1">Sacos</div>
                            <div class="col-1 text-left">
                                <asp:TextBox ID="txtSacosDetalle" Text="0" onkeyup="formato_numero(this)" Width="100%" CssClass="input text-right" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-2 text-left text-white">
                                <a id="btnAgregar" class="btn btn-primary btn-sm text-left">Agregar <i class="fas fa-save"></i></a>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-2 h6">Total Cosecha:</div>
                            <div class="col-2 h6 text-right" id="totalCosecha">0</div>
                            <div class="col-2 h6">Total Cargue:</div>
                            <div id="totalCargue" class="col-2 h6 text-right">0</div>
                            <div class="col-2 h6">Total Transporte:</div>
                            <div id="totalTransporte" class="col-2 h6 text-right">0</div>
                        </div>
                        <div class="row">
                            <div class="col-2 h6">Total Racimos:</div>
                            <div class="col-2 h6 text-right" id="totalRacimos">0</div>
                            <div class="col-2 h6">Total Jornales:</div>
                            <div class="col-2 h6 text-right" id="totalJornales">0</div>
                            <div class="col-2 h6">Total Sacos:</div>
                            <div class="col-2 h6 text-right" id="totalSacos">0</div>
                        </div>
                        <div class="row">
                        </div>
                        <hr />
                        <div class="col-12">
                            <div id="transaccionDetalle" class="row">
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div id="upConsulta" runat="server">
                    <div style="text-align: center">
                        <div style="padding-top: 10px;">
                            <table style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td style="text-align: left; width: 400px;">
                                        <asp:DropDownList ID="niddlCampo" runat="server" ToolTip="Selección de campo para busqueda" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" Width="95%">
                                            <asp:ListItem Value="tiquete">Tiquete</asp:ListItem>
                                            <asp:ListItem Value="fecha">Fecha</asp:ListItem>
                                            <asp:ListItem Value="numero">Número</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 200px;">
                                        <asp:DropDownList ID="niddlOperador" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect" AutoPostBack="True" OnSelectedIndexChanged="niddlOperador_SelectedIndexChanged" ToolTip="Selección de operador para busqueda" Width="90%">
                                            <asp:ListItem Value="like">Contiene</asp:ListItem>
                                            <asp:ListItem Value="&lt;&gt;">Diferente</asp:ListItem>
                                            <asp:ListItem Value="between">Entre</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="=">Igual</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Mayor o Igual</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Mayor que</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Menor o Igual</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Menor</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 220px;">
                                        <asp:TextBox ID="nitxtValor1" runat="server" AutoPostBack="True" CssClass="input" OnTextChanged="nitxtValor1_TextChanged" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="nitxtValor2" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:LinkButton runat="server" ID="niimbAdicionar" CssClass="btn btn-default btn-sm btn-info fa fa-filter" OnClick="niimbAdicionar_Click" ToolTip="Clic aquí para adicionar parámetro a la busqueda"></asp:LinkButton>
                                    </td>
                                    <td style="text-align: left; width: 40px;">
                                        <asp:LinkButton runat="server" ID="imbBusqueda" CssClass="btn btn-default btn-sm btn-success fa fa-search " OnClick="imbBusqueda_Click" ToolTip="Clic aquí para realizar la busqueda"></asp:LinkButton>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="7" style="text-align: center">
                                        <table class="ui-accordion">
                                            <tr>
                                                <td></td>
                                                <td style="width: 300px">
                                                    <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvParametros_RowDeleting" Width="400px">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="imElimina" CssClass="btn btn-default btn-sm btn-danger fa fa-trash " CommandName="Delete" ToolTip="Elimina el registro seleccionado"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="action-item" />
                                                                <HeaderStyle CssClass="action-item" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="campo" HeaderText="Campo">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="operador" HeaderText="Operador">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor" HeaderText="Valor">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor2" HeaderText="Valor 2">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="thead" />
                                                        <PagerStyle CssClass="footer" />
                                                    </asp:GridView>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td></td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>


                            </table>
                        </div>
                        <div style="width: 100%;">
                            <div style="width: 100%;">
                                <asp:Label ID="nilblRegistros" runat="server" Text="Nro. Registros 0"></asp:Label>
                                <asp:GridView ID="gvTransaccion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" GridLines="None" OnRowDeleting="gvTransaccion_RowDeleting" OnRowUpdating="gvTransaccion_RowUpdating" Width="100%" AllowPaging="True" OnPageIndexChanging="gvTransaccion_PageIndexChanging" PageSize="30">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:ButtonField CommandName="Update" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
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
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero" HeaderText="Numero">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tiquete" HeaderText="Tiquete">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="observacion" HeaderText="Observación">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="anulado" HeaderText="Anul">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:CheckBoxField>
                                    </Columns>
                                    <HeaderStyle CssClass="thead" />
                                    <PagerStyle CssClass="footer" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
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
            <script src="../../js/TiqueteCompleto/jquery-ui.js"></script>
            <script src="../../js/TiqueteCompleto/jsCCosecha.js"></script>
            <script src="../../js/TiqueteCompleto/jsTiquetesFull.js"></script>
        </form>
    </div>
</body>
</html>

