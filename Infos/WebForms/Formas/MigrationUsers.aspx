<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MigrationUsers.aspx.cs" Inherits="Infos.WebForms.Formas.MigrationUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://app.infos.com/resources/css/root.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="loading">
            <%--<img src="http://app.infos.com/Resources/docs/images/logoLogin.svg" alt="Inap" width="20%" />--%>
            <i class="fas fa-cog fa-spin fa-spin fa-5x"></i>
        </div>
        <div class="row pt-5">
            <div class=" col-3">
            </div>
            <div class=" col-6 text-center">
                 <img src="http://app.infos.com/Resources/docs/images/logoLogin.svg" alt="Inap" width="50%" />
                <hr />
                <div class="card p-3 m-3">
                    <div class="p-3">
                        <div class="row  text-start">
                            <div class="col-lg-12" style="font-family: Nasalization">
                                <h4>Cambio de contraseña</h4>
                            </div>
                            <%--<div class="col-lg-4 col-md-6 col-sm-12 text-end">
                        <asp:LinkButton ID="nibtnSerch" CssClass="btn btn-secondary" runat="server" OnClick="nibtnSerch_Click" ToolTip="Haga clic aqui para realizar la busqueda"><i class="fas fa-search"></i>  Buscar</asp:LinkButton>
                        <asp:LinkButton ID="nibtnNew" CssClass="btn btn-primary" runat="server" OnClick="nibtnNew_Click" ToolTip="Habilita el formulario para un nuevo registro"><i class="fas fa-plus"></i>  Nuevo</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" CssClass="btn btn-danger" runat="server" OnClick="btnCancel_Click" ToolTip="Cancela la operación" ><i class="fas fa-times"></i>  Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="btnSave" CssClass="btn btn-success" runat="server" OnClick="btnSave_Click" OnClientClick="return confirMessage(this,'Advertencia','¿Esta seguro de guardar el registro?','Si','No');" ToolTip="Guarda el nuevo registro en la base de datos" ><i class="fas fa-save"></i>  Guardar</asp:LinkButton>
                    </div>--%>
                        </div>
                        <hr />
                       
                        <div class="row m-lg-2 text-start" id="formControl" runat="server">

                            <div class="mb-2 col-6">
                                <asp:Label runat="server" for="txtcountry" ID="Label1" CssClass="form-label">Nueva contraseña</asp:Label>
                                <asp:TextBox ID="txtPaword" runat="server" CssClass="form-control w-100" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="mb-2 col-6">
                                <asp:Label runat="server" for="txtdescripcion" ID="lblDescripcio" CssClass="form-label">Confirmación contraseña</asp:Label>
                                <asp:TextBox ID="txtPawordConfirmation" runat="server" CssClass="form-control w-100" TextMode="Password"></asp:TextBox>
                            </div>
                            <input type="hidden" id="txtId" runat="server" />
                            <div class="mb-2 col-12">
                                <asp:Label runat="server" for="txtreference" ID="lblReferenc" CssClass="form-label">Correo electrónico</asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control w-100" TextMode="Email"></asp:TextBox>
                            </div>

                        </div>
                        <hr />

                        <div class="row  text-end">
                            <div class="col-lg-8 col-md-6 col-sm-12 ml-3" style="font-family: Nasalization">
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-12 text-end">
                                <%-- <asp:LinkButton ID="nibtnSerch" CssClass="btn btn-secondary" runat="server" OnClick="nibtnSerch_Click" ToolTip="Haga clic aqui para realizar la busqueda"><i class="fas fa-search"></i>  Buscar</asp:LinkButton>
                        <asp:LinkButton ID="nibtnNew" CssClass="btn btn-primary" runat="server" OnClick="nibtnNew_Click" ToolTip="Habilita el formulario para un nuevo registro"><i class="fas fa-plus"></i>  Nuevo</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" CssClass="btn btn-danger" runat="server" OnClick="btnCancel_Click" ToolTip="Cancela la operación" ><i class="fas fa-times"></i>  Cancelar</asp:LinkButton>--%>
                                <asp:LinkButton ID="btnSave" CssClass="btn btn-success" runat="server" OnClick="btnSave_Click" OnClientClick="return confirMessage(this,'Advertencia','¿Esta seguro de guardar el registro?','Si','No');" ToolTip="Guarda los cambios"><i class="fas fa-save"></i>  Guardar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                      <hr />
                     <p style="font-size:16px">
                            Hola <asp:Label runat="server" for="txtcountry" ID="lblFullName"  CssClass="form-label" Font-Bold="True">Alirio Noche</asp:Label> bienvenido Sistema de información INFOS,
                            por razones de seguridad vimos necesario el cambio de contraseña de tu usuario. Completa los datos y guarda para continuar.
                        </p>
                      
                </div>
            </div>
            <div class=" col-3">
            </div>
        </div>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/jquery.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/bootstrap/js/popper.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/bootstrap/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/jquery.nicescroll.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/sweetalert2.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/highcharts.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/exporting.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/export-data.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/accessibility.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/scripts.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/custom.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/tooltip.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/scroll-up-bar/dist/scroll-up-bar.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/sa-functions.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/summernote/summernote-lite.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/lib/tether/dist/js/tether.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/lib/jquery-easyui/jquery.easyui.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/datatables/DataTables-1.10.16/js/jquery.dataTables.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/modules/datatables/DataTables-1.10.16/js/dataTables.bootstrap4.min.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/chosen.jquery.js"></script>
        <script type="text/javascript" src="http://app.infos.com/resources/js/root.js"></script>
        <script>    </script>
    </form>
</body>
</html>
