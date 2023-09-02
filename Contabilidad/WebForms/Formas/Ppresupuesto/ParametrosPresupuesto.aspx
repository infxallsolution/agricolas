<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametrosPresupuesto.aspx.cs" Inherits="Contabilidad.WebForms.Formas.Ppresupuesto.ParametrosPresupuesto" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguridad</title>
</head>
<link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
<link href="http://app.infos.com/recursosinfos/css/root.css" rel="stylesheet" />
<script src="http://app.infos.com/recursosinfos/lib/jquery/dist/jquery.min.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/js/chosen.jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="../../js/maxazan-jquery-treegrid-447d662/js/jquery.treegrid.js"></script>
<link href="../../js/maxazan-jquery-treegrid-447d662/css/jquery.treegrid.css" rel="stylesheet" />
<script src="http://app.infos.com/recursosinfos/lib/bluebird/bluebird.min.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/lib/tether/dist/js/tether.min.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/lib/bootstrap/dist/js/bootstrap.min.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/lib/moment/moment.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/lib/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/js/chosen.jquery.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/lib/iCheck/icheck.min.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/lib/sweetalert2/dist/sweetalert2.min.js" type="text/javascript"></script>
<script src="http://app.infos.com/recursosinfos/js/core.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.tree').treegrid();
    });
    setInterval('MantenSesion()', <%#(int) (0.9 * (Session.Timeout * 60000)) %>);
        
    function MantenSesion() 
    {                
        var CONTROLADOR = "refresh_session.ashx";
        var head = document.getElementsByTagName('head').item(0);            
        script = document.createElement('script');            
        script.src = CONTROLADOR ;
        script.setAttribute('type', 'text/javascript');
        script.defer = true;
        head.appendChild(script);
    } 

</script>




<body style="text-align: center">

    <div class="loading">
        <i class="fa fa-circle-o-notch fa-spin fa-5x"></i>
                                
    </div>
    <div class="container">
        <form id="form1" runat="server">    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
            <div id="modalCuenta" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 style="color: red;">Cuentas Contables de presupuesto</h4>
                        </div>
                        <table style="width: 100%" cellspacing="0">
                            <tr>
                                <td class="bordesBusqueda">
                                    </td>
                                <td style="text-align: left; width: 150px">Busqueda</td>
                                <td style="width: 250px">
                                    <asp:TextBox ID="nitxtBusquedaCuenta" placeholder="Digite filtro de busqueda" runat="server" Width="200px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>

                                <td style="width: 250px">
                                    <asp:Button ID="niimbBuscarCuenta" CommandName="BuscarCuenta" runat="server" CssClass="botones" OnClientClick="return false;" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                                    <tr>
                                        <td colspan="4"></td>
                                    </tr>
                        </table>
                        <asp:GridView ID="gvCuenta" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" OnSelectedIndexChanged="gvCuenta_SelectedIndexChanged">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                    <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                    <ItemStyle Width="20px" CssClass="action-item" />
                                    <HeaderStyle CssClass="action-item" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                                    <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="True">
                                    <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </div>

                </div>
            </div>
            <div id="modalCcostoSiesa" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 style="color: red;">Cuentas Contables de presupuesto</h4>
                        </div>
                        <table style="width: 100%" cellspacing="0">
                            <tr>
                                <td class="bordesBusqueda"></td>
                                <td style="text-align: left; width: 150px">Busqueda</td>
                                <td style="width: 250px">
                                    <asp:TextBox ID="nitxtBusquedaCcostoSiesa" placeholder="Digite filtro de busqueda" runat="server" Width="200px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>

                                <td style="width: 250px">
                                    <asp:Button ID="niimbBuscarCcostoSiesa" CommandName="BuscarCuenta" runat="server" CssClass="botones" OnClientClick="return false;" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                                    <tr>
                                        <td colspan="4"></td>
                                    </tr>
                        </table>
                        <asp:GridView ID="gvCcostoSiesa" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" OnSelectedIndexChanged="gvCuenta_SelectedIndexChanged">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                    <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                    <ItemStyle Width="20px" CssClass="action-item" />
                                    <HeaderStyle CssClass="action-item" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                                    <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="True">
                                    <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </div>

                </div>
            </div>
            <div id="modalCcosto" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 style="color: red;">Centro de costo de presupuesto</h4>
                        </div>
                        <table style="width: 100%" cellspacing="0">
                            <tr>
                                <td class="bordesBusqueda"></td>
                                <td style="text-align: left; width: 150px">Busqueda</td>
                                <td style="width: 250px">
                                    <asp:TextBox ID="nitxtBusquedaCcosto" placeholder="Digite filtro de busqueda" runat="server" Width="200px" ToolTip="Escriba el texto para la busqueda" CssClass="input"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 250px"></td>
                                <td colspan="4">
                                    <asp:Button ID="niimbBuscarCcosto" CommandName="BuscarCcosto" runat="server" CssClass="botones" OnClientClick="return false;" Text="Buscar" ToolTip="Haga clic aqui para realizar la busqueda" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvCcosto" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm  table-hover table-striped grillas" Width="100%" OnSelectedIndexChanged="gvCuenta_SelectedIndexChanged">
                            <AlternatingRowStyle CssClass="alt" />
                            <Columns>
                                <asp:ButtonField CommandName="Select" ControlStyle-CssClass="btn btn-default btn-sm btn-primary glyphicon glyphicon-pencil">
                                    <ControlStyle CssClass="btn btn-default btn-sm btn-primary fas fa-pencil-alt"></ControlStyle>
                                    <ItemStyle Width="20px" CssClass="action-item" />
                                    <HeaderStyle CssClass="action-item" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="True">
                                    <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="True">
                                    <ItemStyle CssClass="Items" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="thead" />
                            <PagerStyle CssClass="footer" />
                        </asp:GridView>
                    </div>

                </div>
            </div>
            <table cellspacing="0" width="100%">
                <tr>
                    <td></td>
                    <td class="text-left" width="200px">
                        <asp:Label ID="Label1" runat="server" Text="Nivel 1" CssClass="label"></asp:Label></td>
                    <td class="text-left" width="300px">
                        <asp:DropDownList ID="ddlNivel1" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left">
                        <asp:Label ID="Label2" runat="server" Text="Nivel 2" CssClass="label"></asp:Label></td>
                    <td class="text-left">
                        <asp:DropDownList ID="ddlNivel2" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left">
                        <asp:Label ID="Label3" runat="server" Text="Nivel 3" CssClass="label"></asp:Label></td>
                    <td class="text-left">
                        <asp:DropDownList ID="ddlNivel3" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel3_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left">
                        <asp:Label ID="lblNivel4" runat="server" Text="Nivel 4" CssClass="label" Visible="False"></asp:Label></td>
                    <td class="text-left">
                        <asp:DropDownList ID="ddlNivel4" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel4_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="text-left"></td>
                    <td class="text-left">
                        <asp:Button ID="lbRegistrar" runat="server" CssClass="botones" OnClick="lbRegistrar_Click" OnClientClick="return confirmSwal(this,'Advertencia','¿Esta seguro de guardar el registro?');" Text="Guardar" ToolTip="Guarda el nuevo registro en la base de datos" Visible="False" />
                        </td>
                </tr>
            </table>
            <asp:Repeater ID="rptCuentas" runat="server" OnItemDataBound="rptCuentas_ItemDataBound" OnItemCommand="rptCuentas_ItemCommand">
                <HeaderTemplate>
                    <table id="tbRepeter" cellspacing="0" rules="all" border="1" class="table table-bordered table-sm  table-hover table-striped grillas tree" style="width: 100%">
                        <tr class="thead">
                            <td scope="col">Cuenta 
                            </td>
                            <td scope="col">Nombre cuenta 
                            </td>
                            <td scope="col" style="width: 300px">Presupuestar
                            </td>
                            <td scope="col">Cuenta  Presupuesto
                            </td>
                            <td scope="col">Perfil
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr runat="server" class='<%#("treegrid-" + Eval("codigo").ToString() + (Eval("padre").ToString() != "" ? (" treegrid-parent-" + Eval("padre")):""))%>'>

                        <td style="text-align: left">
                            <asp:Label ID="lblCodigoCuenta" runat="server" Text='<%#  Eval("codigo").ToString().Trim() %> '></asp:Label>
                            <%--<asp:HiddenField ID="hfRowIdCuenta" runat="server" Value='<%#  Eval("rowidAuxiliar") %> ' />--%>
                        </td>
                        <td style="text-align: left">
                            <%# Eval("nombre").ToString().Trim()  %> 
                        </td>
                        <td style="text-align: left; width:400px">
                            <asp:CheckBox ID="chkMpresupuesto" runat="server" Checked='<%# Convert.ToBoolean(Eval("mPresupuesto") ) %>' Text="Cuenta" Visible='<%# Convert.ToBoolean(Eval("auxiliar") ) %>' />
                            <asp:HiddenField ID="hfRowIdCuenta" runat="server" Value='<%#  Eval("rowid") %> ' />
                            <asp:CheckBox ID="chkMccosto" runat="server" Text="Presu. Ccosto" Visible='<%# Convert.ToBoolean(Eval("ccosto") ) %>' Checked='<%# Convert.ToBoolean(Eval("mccosto") ) %>' />
                        </td>
                        <%--  <td style="text-align: left">
                            <asp:Label ID="lblidCcosto" runat="server" Text='<%#  Eval("codccosto").ToString().Trim() %> '></asp:Label>
                        </td>--%>
                        <%--  <td style="text-align: left">
                            <%# Eval("nombreCcosto").ToString().Trim()  %> 
                        </td>--%>
                        <td style="text-align: left">
                            <asp:LinkButton runat="server" ID="imCuenta" CssClass="btn btn-default btn-sm btn-danger fa fa-search " Visible='<%#Convert.ToBoolean( Eval("auxiliar"))%>' ToolTip="Elimina el registro seleccionado"> </asp:LinkButton>
                            <asp:TextBox ID="txtCuenta" runat="server" Width="150px" Visible='<%#Convert.ToBoolean( Eval("auxiliar"))%>' Text='<%#Eval("idcuentapresupuesto").ToString()%>'></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPerfil" Width="300px" runat="server" CssClass="chzn-select" data-placeholder="Seleccione una opción..." Visible='<%#Convert.ToBoolean( Eval("auxiliar"))%>' DataTextField="descripcion" DataValueField="codigo" DataSource='<%# CargarPerfiles() %>'></asp:DropDownList>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </form>
    </div>

    <script type="text/javascript">
        var a = $("#gvCuenta td.action-item").clone();
        $("#niimbBuscarCuenta").click(
          function BindGridView() {
              var texto = $("#nitxtBusquedaCuenta").val();

              $.ajax({
                  type: "POST",
                  url: "ParametrosPresupuesto.aspx/RetornaPlanPresupuesto",
                  contentType: "application/json;charset=utf-8",
                  data: "{ texto: '" + texto + "'}",
                  dataType: "json",
                  success: function (data) {
                      var b = a.html();
                      if (data.d.length > 0) {
                          $("#gvCuenta").empty();
                          $("#gvCuenta").append("<tr class='thead'><th  scope='col'>Código</th><th  scope='col'>Nombre</th></tr>");
                          for (var i = 0; i < data.d.length; i++) {
                              $("#gvCuenta").append("<tr class='target'><td  class='Items' align='left'>"
                              + data.d[i].codigoCuenta + "</td><td  class='Items' align='left'>"
                              + data.d[i].NombreCuenta + "</td> </tr>");
                          }

                      }
                      else {
                          alert("No hay datos retornados");
                      }

                  },
                  error: function (result) {
                      alert("Error cargar datos" + result);

                  }
              });
          });

        $("#niimbBuscarCcosto").click(
         function BindGridView() {
             var texto = $("#nitxtBusquedaCcosto").val();

             $.ajax({
                 type: "POST",
                 url: "ParametrosPresupuesto.aspx/RetornaCcostoPresupuesto",
                 contentType: "application/json;charset=utf-8",
                 data: "{ texto: '" + texto + "'}",
                 dataType: "json",
                 success: function (data) {
                     var b = a.html();
                     if (data.d.length > 0) {
                         $("#gvCcosto").empty();
                         $("#gvCcosto").append("<tr class='thead'><th  scope='col'>Código</th><th  scope='col'>Nombre</th></tr>");
                         for (var i = 0; i < data.d.length; i++) {
                             $("#gvCcosto").append("<tr class='target'><td  class='Items' align='left'>"
                             + data.d[i].codigoCuenta + "</td><td  class='Items' align='left'>"
                             + data.d[i].NombreCuenta + "</td> </tr>");
                         }

                     }
                     else {
                         alert("No hay datos retornados");
                     }

                 },
                 error: function (result) {
                     alert("Error cargar datos" + result);

                 }
             });
         });

        $("#niimbBuscarCcostoSiesa").click(
        function BindGridView() {
            var texto = $("#nitxtBusquedaCcostoSiesa").val();
            var cuenta = $($('#modalCuenta').data('target-texbox')).data('cuentaSiesa').val();

            $.ajax({
                type: "POST",
                url: "ParametrosPresupuesto.aspx/RetornaCcostoSiesa",
                contentType: "application/json;charset=utf-8",
                data: "{ texto: '" + texto +"'  cuenta: '"+ cuenta +"'}",
                dataType: "json",
                success: function (data) {
                    var b = a.html();
                    if (data.d.length > 0) {
                        $("#gvCcosto").empty();
                        $("#gvCcosto").append("<tr class='thead'><th  scope='col'>Código</th><th  scope='col'>Nombre</th></tr>");
                        for (var i = 0; i < data.d.length; i++) {
                            $("#gvCcosto").append("<tr class='target'><td  class='Items' align='left'>"
                            + data.d[i].codigoCuenta + "</td><td  class='Items' align='left'>"
                            + data.d[i].NombreCuenta + "</td> </tr>");
                        }

                    }
                    else {
                        alert("No hay datos retornados");
                    }

                },
                error: function (result) {
                    alert("Error cargar datos" + result);

                }
            });
        });

        $('#gvCuenta').on('click', 'tr.target', function () {
            $(this).toggleClass('selected');
            dato = $(this).find('td:first').text();
            var id = $('#modalCuenta').data('target-texbox');
            $("#" + id).val(dato);
            //alert(dato);
            $('#modalCuenta').modal('hide');
        });

        $('#gvCcosto').on('click', 'tr.target', function () {
            $(this).toggleClass('selected');
            dato = $(this).find('td:first').text();
            var id = $('#modalCcosto').data('target-texbox');
            $("#" + id).val(dato);
            //alert(dato);
            $('#modalCcosto').modal('hide');
        });

        $('#tbRepeter').on('click', 'tr.target', function () {
            $(this).toggleClass('selected');
        });

    </script>
</body>
</html>
