
$(document).ready(function () {
    var urlTercero = "EntradasDirectas.aspx/GetProveedores";
    var urlTipoDocumetno = "EntradasDirectas.aspx/GetTipoDocumento";
    var urlItems = "EntradasDirectas.aspx/GetItems";
    var urlBodega = "EntradasDirectas.aspx/GetBodegas";
    var urlAddItems = "EntradasDirectas.aspx/addItem";
    var urlConsecutivo = "EntradasDirectas.aspx/GetConsecutivoTransaccion";
    var urlSucursal = "EntradasDirectas.aspx/GetSucursalProveedor";
    var arrayTerceros = null;

    var aitems = new Array();
    //Seleccion de tipo de documento y terceros 
    $(".mModal").click(function () {
        event.preventDefault();
        if ($.fn.DataTable.isDataTable('#dtModal')) {
            $('#dtModal').dataTable().fnDestroy();
        }
        if ($.fn.DataTable.isDataTable('#dtModalTerceros')) {
            $('#dtModalTerceros').dataTable().fnDestroy();
        }
        $("#divModal").data("btn", $(this).attr("id"));
        cargar($(this).attr("id"));
        $('#divModal').modal('show');
    });
    $('.modalSeleccion').on('click', 'tr', function () {
        var codigo = "", descripcion = "";
        codigo = $(this).find("td:eq(0)").html();
        descripcion = $(this).find("td:eq(1)").html();
        switch ($("#divModal").data('btn')) {
            case "btnTipo":
                $("#txtTipo").val(codigo);
                RetornaConsecutivo(codigo);
                break;
            case "btnTercero":
                $("#txtTercero").val(codigo);
                $("#txtTerceroDescripcion").val(descripcion);
                $("#txtSucursal").val("");
                break;
            case "btnBodega":
                $("#txtBodega").val(codigo);
                $("#txtDesBodega").val(descripcion);
                $('#divModal').modal('hide');
                $('#divAddItem').modal('show');
                break;

            case "btnSucursal":

                $("#txtSucursal").val(codigo);
                break;
        }

        $('#divModal').modal('hide');

        if ($("#divModal").data('btn') == "btnTercero")
            $("#dtModalTerceros").dataTable().fnDestroy();
        else
            $("#dtModal").dataTable().fnDestroy();

        function RetornaConsecutivo(tipo) {
            var parametro = "{tipotra:'" + tipo + "'}";
            $.ajax({
                type: "POST",
                url: urlConsecutivo,
                contentType: "application/json;charset=utf-8",
                data: parametro,
                dataType: "json",
                success: function (data) {
                    $("#txtNumero").val(data.d);
                },
                error: function (result) {
                    alert("Error cargar datos" + result);
                    return null;
                }
            });
        }

    });

    //agregando items

    $("#lbAgregarItem").click(function () {

        if ($("#txtTipo").val().trim().length == 0) {
            alert("Seleccione un tipo de documento válido");
            return;
        }

        if ($("#txtTercero").val().trim().length == 0) {
            alert("Seleccione un proveedor válido");
            return;
        }

        if ($("#txtSucursal").val().trim().length == 0) {
            alert("Seleccione una sucursal válido");
            return;
        }

        $("#txtItem").val("");
        $("#txtDescripcionItem").val("");
        $("#txtDesBodega").val("");
        $("#txtBodega").val("");
        $("#txtCantidad").val("0");
        $("#txtUmedida").val("");
        $("#txtValorUnitario").val("0");
        $('#divAddItem').modal('show');
        $("#hfMetodo").val(1);
        $("#hfRegistro").val(-1);
        event.preventDefault();

    });

    $("#btnItem").click(function () {
        event.preventDefault();
        $('#divAddItem').modal('hide');
        cargarItems();
        $('#divItem').modal('show');
    });
    $('#btnBodega').click(function () {
        event.preventDefault();
        $('#divAddItem').modal('hide');
        $("#divModal").data("btn", $(this).attr("id"));
        cargar($(this).attr("id"));
        $("#divModal").modal('show');

    });

    $('#dtItems').on('click', 'tr', function () {
        var codigo = "", descripcion = "", umedida = "";
        codigo = $(this).find("td:eq(0)").html();
        descripcion = $(this).find("td:eq(1)").html();
        umedida = $(this).find("td:eq(5)").html();
        $("#txtItem").val(codigo);
        $("#txtDescripcionItem").val(descripcion);
        $('#txtUmedida').val(umedida);
        $('#divItem').modal('hide');
        $('#divAddItem').modal('show');
        $("#dtItems").dataTable().fnDestroy();

    });

    //adicionando item a array de items
    $("#btnAddItem").click(function () {
        event.preventDefault();

        var _codigo = $('#txtItem').val(), _descripcion = $('#txtDescripcionItem').val(), _umedida = $('#txtUmedida').val(),
            _bodega = $('#txtBodega').val(), _cantidad = $('#txtCantidad').val().split(',').join(''),
            _valUnitario = $('#txtValorUnitario').val().split(',').join(''), _desBodega = $('#txtDesBodega').val(), _metodo = $("#hfMetodo").val(),
            _registro = $("#hfRegistro").val();;

        $.ajax({
            type: "POST",
            url: urlAddItems,
            contentType: "application/json;charset=utf-8",
            data: "{ codigo: '" + _codigo + "',descripcion:'" + _descripcion + "',umedida:'" + _umedida + "',bodega:'" + _bodega + "',cantidad:'" + _cantidad + "',valUnitario:'" + _valUnitario + "',metodo:'" + _metodo + "', desBodega:'" + _desBodega + "', registro:" + _registro + "}",
            dataType: "json",
            success: function (data) {
                aitems = data.d;
                agregarTablaItem(data);
                totales();
            },
            error: function (result) {
                alert("Error al enviar datos" + result);
                return null;
            }
        });
        $('#divAddItem').modal('hide');
    });

    //modificando items
    $('#gvItems').on('dblclick', 'tr', function () {
        $("#txtItem").val("");
        $("#txtDescripcionItem").val("");
        $("#txtDesBodega").val("");
        $("#txtCantidad").val("0");
        $("#txtUmedida").val("");
        $("#txtValorUnitario").val("0");
        $("#hfMetodo").val(2);
        var registro = $(this).find('#hffRegistro').val();
        $("#hfRegistro").val(registro);
        var desBodega = $(this).find('#hfDesbodega').val();
        var id = $(this).attr('id');
        var k;
        var codigo = $(this).find("td:eq(1)").html();
        var descripcion = $(this).find("td:eq(2)").html();
        var umedida = $(this).find("td:eq(3)").html();
        var cantidad = $(this).find("td:eq(5)").html();
        var valUnitario = $(this).find("td:eq(6)").html();
        $("#txtItem").val(codigo);
        $("#txtDescripcionItem").val(descripcion);
        $('#txtUmedida').val(umedida);
        $('#txtDesBodega').val(desBodega);
        $('#txtCantidad').val(cantidad);
        $('#txtValorUnitario').val(valUnitario);
        $('#divAddItem').modal('show');
        $('#divItem').modal('hide');
    });


    // funciones
    function cargar(boton) {
        var response;
        var url;
        var parametros = "{}";

        $('#dtModalTerceros').hide();
        $('#dtModal').show();

        if (boton == "btnTipo") {
            url = urlTipoDocumetno
        }
        else
            if (boton == "btnTercero") {
                url = urlTercero;
                $('#dtModalTerceros').show();
                $('#dtModal').hide();
            }
            else if (boton == "btnBodega") {
                url = urlBodega;
            }
            else if (boton == "btnSucursal") {
                if ($("#txtTercero").val().trim().length == 0) {
                    alert("Debe seleccionar un tercero válido");
                    $("#btnTercero").focus();
                    $('#dtModal').hide();
                    return;
                } else {
                    url = urlSucursal;
                    parametros = "{tercero:'" + $('#txtTercero').val().trim() + "'}";
                    console.log(parametros);
                }

            }


        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json;charset=utf-8",
            data: parametros,
            dataType: "json",
            success: function (data) {
                llenarArray(data, boton);
            },
            error: function (result) {
                alert("Error cargar datos" + result);
                return null;
            }
        });



        function llenarArray(data, boton) {

            var datos = new Array();
            var array = new Array();
            var array1 = new Array();
            var x = 0;

            if (boton == "btnTercero" & arrayTerceros == null) {
                $.each(data.d, function (key, value) {
                    a = new Array();
                    $.each(value, function (key, value1) {
                        a.push(value1)
                    });
                    array.push(a);
                });

                arrayTerceros = array;
            }
            else if (boton == "btnTercero" & arrayTerceros != null) {
                array = arrayTerceros;
            }
            else {
                $.each(data.d, function (key, value) {
                    a = new Array();
                    $.each(value, function (key, value1) {
                        a.push(value1)
                    });
                    array.push(a);
                });
            }




            if (boton == "btnTercero") {

                $('#dtModalTerceros').dataTable({
                    data: array,
                    columns: [
                        { title: "Codigo" },
                        { title: "Descripcion" },
                    ]
                });


            }
            else {
                $('#dtModal').dataTable({
                    data: array,
                    columns: [
                        { title: "Codigo" },
                        { title: "Descripcion" },
                    ]
                });


            }


        }


    }
    //cargando items

    function cargarItems() {


        $.ajax({
            type: "POST",
            url: urlItems,
            contentType: "application/json;charset=utf-8",
            data: "{}",
            dataType: "json",
            success: function (data) {

                llenarArrayItems(data);
            },
            error: function (result) {
                alert("Error cargar datos" + result);
                return null;
            }
        });

        function llenarArrayItems(data) {

            var datos = new Array();
            var array = new Array();
            var array1 = new Array();
            var x = 0;


            $.each(data.d, function (key, value) {
                a = new Array();
                $.each(value, function (key, value1) {
                    a.push(value1)
                });
                array.push(a);
            });

            $('#dtItems').dataTable({
                data: array,
                columns: [
                    { title: "Codigo" },
                    { title: "Descripcion" },
                    { title: "Des. corta" },
                    { title: "Referencia" },
                    { title: "U.M. compra" },
                    { title: "U.M. consumo" },
                    { title: "Tipo item" },
                ]
            });
        }

    }

    //calculando totales
    function totales() {
        var valorTotal = 0;
        $('#gvItems tr').each(function () {

            var cantidad = String($(this).find("td").eq(5).html());
            var valorU = String($(this).find("td").eq(6).html());

            cantidad = cantidad.split(',').join('');
            valorU = valorU.split(',').join('');


            if (!isNaN(parseFloat(cantidad) * parseFloat(valorU)))
                valorTotal += parseFloat(cantidad) * parseFloat(valorU);

        });

        console.log(valorTotal);

        valorTotal = valorTotal.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
        valorTotal = valorTotal.split('').reverse().join('').replace(/^[\,]/, '');
        $("#txtValorBruto").val(valorTotal);
        $("#txtImpuestos").val("0");
        $("#txtRetencion").val("0");
        $("#txvValorTotal").val(valorTotal);

    }

    //agregando a la tabla
    function agregarTablaItem(data) {

        var html = "";
        var clonarfila = $("#gvItems").find("tr:first").clone();

        $("#gvItems").empty();


        //console.log(data.d);
        var y = 0;
        $.each(data.d, function (key, value) {

            var x = 0;
            y++;
            html += "<tr id='" + y + "' >";
            html += "<td align='left' style='Width:15px' ><a class='btn btn-danger delete btn-sm' href='#'><i class='fa fa-trash-o fa-sm'></i></a></td>";
            $.each(value, function (key, value1) {
                x++;
                switch (x) {
                    case 7:
                        html += "<input type='hidden' id='hffRegistro' value='" + value1 + "'>";
                        break;
                    case 8:
                        html += "<input type='hidden' id='hfDesbodega' value='" + value1 + "'>";
                        break;
                    case 6:
                        value1 = value1.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        value1 = value1.split('').reverse().join('').replace(/^[\,]/, '');
                        html += "<td align='left'>" + value1 + "</td>";
                        break;
                    case 5:
                        value1 = value1.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        value1 = value1.split('').reverse().join('').replace(/^[\,]/, '');
                        html += "<td align='left'>" + value1 + "</td>";
                        break;
                    default:
                        html += "<td align='left'>" + value1 + "</td>";
                        break;
                }
            });
            html += "</tr >";
        });

        $("#gvItems").append(clonarfila);
        $("#gvItems").append(html);

        $('.delete').on('click', function () {

            var _codigo = "", _descripcion = "", _umedida = "",
                _bodega = "", _cantidad = "0",
                _valUnitario = "0", _desBodega = "", _metodo = "3",
                _registro = $(this).parent().parent('tr').find('#hffRegistro').val();
            //console.log(papa);
            //console.log(_registro);

            $.ajax({
                type: "POST",
                url: urlAddItems,
                contentType: "application/json;charset=utf-8",
                data: "{ codigo: '" + _codigo + "',descripcion:'" + _descripcion + "',umedida:'" + _umedida + "',bodega:'" + _bodega + "',cantidad:'" + _cantidad + "',valUnitario:'" + _valUnitario + "',metodo:'" + _metodo + "', desBodega:'" + _desBodega + "', registro:" + _registro + "}",
                dataType: "json",
                success: function (data) {
                    aitems = data.d;
                    agregarTablaItem(data);
                    totales();
                },
                error: function (result) {
                    alert("Error al enviar datos" + result);
                    return null;
                }
            });
        });

    }





});