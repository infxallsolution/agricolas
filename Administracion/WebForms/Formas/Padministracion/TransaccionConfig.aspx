<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransaccionConfig.aspx.cs" Inherits="Administracion.WebForms.Formas.Padministracion.TransaccionConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administración</title>
    <link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
    <script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
</head>
<body style="text-align: center">
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
                        <asp:Button ID="lbCancelar" runat="server" CssClass="botones" OnClick="lbCancelar_Click" Text="Cancelar" ToolTip="Cancela la operación" Visible="False"  />
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
            <hr />
            <table width="100%">
                <tr>
                    <td></td>
                    <td style="width: 200px; text-align: left">
                        <asp:Label ID="Label2" runat="server" Text="Tipo de transacción" Visible="False" ></asp:Label>
                    </td>
                    <td style="width: 320px; text-align: left">
                        <asp:DropDownList ID="ddlTipoTransaccion" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  Width="95%" style="left: 0px; top: 0px" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 200px; text-align: left">
                        <asp:Label ID="Label3" runat="server" Text="Tipo destino" Visible="False" ></asp:Label>
                    </td>
                    <td style="width: 320px; text-align: left">
                        <asp:DropDownList ID="ddlNivelDestino" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  Width="150px" Visible="False">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem Value="1">Inversion</asp:ListItem>
                            <asp:ListItem Value="1">Gasto</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Formato impresión" Visible="False" ></asp:Label>
                    </td>
                    <td style="width: 320px; text-align: left">
                        <asp:TextBox ID="txtFormatoImpresion" runat="server" CssClass="input"  Width="95%" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 150px; text-align: left">
                        <asp:Label ID="Label5" runat="server" Text="DS referencia detalle" Visible="False" ></asp:Label>
                    </td>
                    <td style="width: 320px; text-align: left">
                        <asp:TextBox ID="txtDsReferenciaDetalle" runat="server" CssClass="input"  Width="300px" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 150px; text-align: left">
                        <asp:CheckBox ID="chkTipoLiquidacionNomina" runat="server" OnCheckedChanged="chkLiquidacion_CheckedChanged" Text="Forma Documento" AutoPostBack="True"  CssClass="checkbox checkbox-primary" Visible="False" />
                    </td>
                    <td style="width: 320px; text-align: left">
                        <asp:DropDownList ID="ddlTipoLiquidacionNomina" runat="server" data-placeholder="Seleccione una opción..." CssClass="chzn-select-deselect"  Width="95%" Style="left: 0px; top: 0px" Visible="False">
                            <asp:ListItem Value="0">Ninguna</asp:ListItem>
                            <asp:ListItem Value="1">Liquidación definitiva</asp:ListItem>
                            <asp:ListItem Value="2">Cesantías</asp:ListItem>
                            <asp:ListItem Value="3">Interes cesantías</asp:ListItem>
                            <asp:ListItem Value="4">Primas</asp:ListItem>
                            <asp:ListItem Value="5">Consolidado vacaciones</asp:ListItem>
                            <asp:ListItem Value="6">Acumulados</asp:ListItem>
                            <asp:ListItem Value="7">Solicitudes</asp:ListItem>
                            <asp:ListItem Value="8">Cotizaciones</asp:ListItem>
                            <asp:ListItem Value="9">Orden compra</asp:ListItem>
                            <asp:ListItem Value="10">Entrada almacen</asp:ListItem>
                            <asp:ListItem Value="11">Entrada almacen desde OC</asp:ListItem>
                            <asp:ListItem Value="12">Salidas de almacen</asp:ListItem>
                            <asp:ListItem Value="13">Salidas de almacen desde Sol</asp:ListItem>
                            <asp:ListItem Value="14">Requerimiento de salida</asp:ListItem>
                            <asp:ListItem Value="15">Devolución de salida</asp:ListItem>
                            <asp:ListItem Value="16">Devolución al proveedor</asp:ListItem>
                            <asp:ListItem Value="20">Trnsacción Producción</asp:ListItem>
                            <asp:ListItem Value="21">Entradas suministro</asp:ListItem>
                            <asp:ListItem Value="22">Salidas suministro</asp:ListItem>
                            <asp:ListItem Value="23">Entradas producto Terminado</asp:ListItem>
                            <asp:ListItem Value="24">Salidas producto terminado</asp:ListItem>
                            <asp:ListItem Value="25">Documento de causación</asp:ListItem>
                            <asp:ListItem Value="26">Comprobante de egreso</asp:ListItem>
                            <asp:ListItem Value="27">Comprobante de ingreso</asp:ListItem>
                            <asp:ListItem Value="28">Factura directa</asp:ListItem>
                            <asp:ListItem Value="29">Notas debito</asp:ListItem>
                            <asp:ListItem Value="30">Notas credito</asp:ListItem>
                            <asp:ListItem Value="31">Diagnóstico</asp:ListItem>
                            <asp:ListItem Value="32">Lectura horómetros</asp:ListItem>
                            <asp:ListItem Value="33">Orden trabajo</asp:ListItem>
                            <asp:ListItem Value="34">Cerrar orden trabajo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 150px; text-align: left">
                        <asp:CheckBox ID="chkVigencia" runat="server" Text="Dias vigencia"  AutoPostBack="True" OnCheckedChanged="chkVigencia_CheckedChanged" CssClass="checkbox checkbox-primary" Visible="False" />
                    </td>
                    <td style="width: 320px; text-align: left">
                        <asp:TextBox ID="txvVigencia" runat="server" CssClass="input"  Width="100px" Height="22px" Visible="False"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
            </table>
            <table style="width: 100%; text-align: left;" id="tdCampos">
                <tr>
                    <td width="10%"></td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDocumento" runat="server" Text="Maneja Documento" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCantidadEditable" runat="server" Text="Cantidad Editable" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaBodega" runat="server" Text="Maneja Bodega" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSalida" runat="server" Text="Salida" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkAjuste" runat="server" Text="Ajuste" Visible="False"  />
                    </td>
                    <td width="10%"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkRegistroProveedor" runat="server" Text="Valida Proveedor" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkValidaSaldo" runat="server" Text="Valida Saldo" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaTercero" runat="server" Text="Maneja Tercero" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaTalonario" runat="server" Text="Maneja Talonario" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkConsignacion" runat="server" Text="Consignación" Visible="False"  />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaBascula" runat="server" Text="Maneja Bascula" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkVentaInventario" runat="server" Text="Venta inventario" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkSalidaPlanta" runat="server" Text="Salida Planta" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkVunitarioEditable" runat="server" Text="Vl. Unitario Editable" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkManejaAprobacion" runat="server" Text="Maneja Aprobación" Visible="False"  />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPivaEditable" runat="server" Text="% Impuesto Editable" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkFecha" runat="server" Text="Fecha Editable" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDias" runat="server" Text="Valida Dias de semana" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkEstudioCompra" runat="server" Text="Estudio Compra" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkReferenciaTercero" runat="server" Text="Tercero en Ds Referencia" Visible="False"  />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkLiberaReferencia" runat="server" Text="Libera Referencia" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkEntradaDirecta" runat="server" Text="Entrada Directa" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCompraInv" runat="server" Text="Compra inventario" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkEntradaPlanta" runat="server" Text="Entrada Planta" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkPdesEditable" runat="server" Text="pDescuento Editable" Visible="False"  />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkRegistroDirecto" runat="server" Text="Registro Directo" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkVentaServicios" runat="server" Text="Venta servicios" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkCompraServ" runat="server" Text="Compra servicios" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkmHorarioProduccion" runat="server" Text="Horario produccion" Visible="False"  />
                    </td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkUmedidaEditable" runat="server" Text="uMedida Editable" Visible="False"  />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox CssClass="checkbox checkbox-primary" ID="chkDocContable" runat="server" Text="Documento contable" Visible="False"  />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <hr />
            <div class="tablaGrilla">
                <asp:Label ID="nilblInformacion" runat="server"></asp:Label>
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
                        <asp:BoundField DataField="tipoTransaccion" HeaderText="Tipo" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreTransaccion" HeaderText="NombreTransaccion" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nivelDestino" HeaderText="ND" ReadOnly="True">

                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="formatoImpresion" HeaderText="FI">

                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dsReferenciaDetalle" HeaderText="RD">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="ajuste" HeaderText="Ajus">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="salida" HeaderText="Salida">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="validaSaldo" HeaderText="VS">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="referenciaTercero" HeaderText="TDS">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="cantidadEditable" HeaderText="CE">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="vUnitarioEditable" HeaderText="VE">
                            <HeaderStyle HorizontalAlign="Center" Width="5px" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="pIvaEditable" HeaderText="PIE">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaTalonario" HeaderText="TA">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaBodega" HeaderText="BDG">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="liberaReferencia" HeaderText="Libr">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="entradaDirecta" HeaderText="DT">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="registroDirecto" HeaderText="RG">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="consignacion" HeaderText="CSG">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="diaSemana" HeaderText="D&#237;as">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaDocumento" HeaderText="Url">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="vigencia" HeaderText="Vig.">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="pDesEditable" HeaderText="PDE">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="UmedidaEditable" HeaderText="UME">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="estudioCompra" HeaderText="ECO">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="registroProveedor" HeaderText="RP">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="fechaActual" HeaderText="FE">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="manejaBascula" HeaderText="BAS">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="mTipoLiquidacionNomina" HeaderText="MTL">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:BoundField DataField="tipoLiquidacionNomina" HeaderText="TLN" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="mAprobacion" HeaderText="MA">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:BoundField HeaderText="DV" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="mTercero" HeaderText="MT">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="compraInv" HeaderText="CI">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="ventaInv" HeaderText="VI">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="compraServ" HeaderText="CS">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="ventaServ" HeaderText="VS">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField HeaderText="EPE">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField HeaderText="SPE">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField HeaderText="mHP">
                            <ItemStyle HorizontalAlign="Left" Width="5px" />
                        </asp:CheckBoxField>
                        <asp:CheckBoxField DataField="documentoContable" HeaderText="DocCont">
                            <ItemStyle HorizontalAlign="Left" Width="10px" />
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
