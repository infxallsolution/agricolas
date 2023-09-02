if (typeof (Sys) !== 'undefined') {
    var instance = Sys.WebForms.PageRequestManager.getInstance();
    instance.add_endRequest(function () {
        cargarEventos();
    });
}

cargarEventos();

function confirmSwal($this, title, text) {
    swal({
        title: title,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar"
    }).then(function () {
        $this.onclick = '';
        //$this= $($this);
        $this.click();
    });
    return false;
}
function formato(entrada) {
    if (window.event.keyCode !== 9) {
        formato_numero(entrada);
        var totalGlobal = 0;
        var total = 0;
        var valorDescuento = 0;
        var netoGlobal = 0;
        var totalNeto = 0;
        var descuentoGloabal = 0;
        var controlNetoTotal = 0;
        var nodecimales = 4;

        controlGrillaRef = document.getElementById("gvReferencia");
        if (controlGrillaRef !== null) {
            for (i = 1; i < controlGrillaRef.rows.length; i++) {
                objeto = controlGrillaRef.rows[i].getElementsByTagName("input");

                if (objeto !== null) {
                    total = parseFloat(parseFloat(objeto[1].value.replace(/\,/g, '')) * parseFloat(objeto[2].value.replace(/\,/g, ''))).toFixed(nodecimales);
                    valorDescuento = total * (parseFloat(objeto[3].value.replace(/\,/g, '')) / 100);
                    totalNeto = parseFloat(total - valorDescuento).toFixed(nodecimales);

                    if (!isNaN(total)) {
                        totalGlobal += parseFloat(total);
                        descuentoGloabal += parseFloat(valorDescuento);
                        netoGlobal += parseFloat(totalNeto).toFixed(nodecimales);
                        controlGrillaRef.rows[i].cells[6].innerHTML = total;
                        controlGrillaRef.rows[i].cells[8].innerHTML = valorDescuento;
                        controlGrillaRef.rows[i].cells[9].innerHTML = totalNeto;
                    }
                }
            }
        }

        controlValorTotal = document.getElementById("nitxtTotalValorBruto");

        if (controlValorTotal !== null) {
            controlValorTotal.value = totalGlobal.toFixed(nodecimales);
            hdValorTotalRef = document.getElementById("hdValorTotalRef");
            hdValorTotalRef.value = totalGlobal;
        }

        controlDescuento = document.getElementById("nitxtTotalDescuento");

        if (controlDescuento !== null) {
            controlDescuento.value = descuentoGloabal;
            hdDescuentoRef = document.getElementById("hdDescuentoRef");
            hdDescuentoRef.value = descuentoGloabal;
        }
        var baseGravable = 0;
        var tasa = 0;
        var baseMinima = 0;
        var valorImpuesto = 0;
        var valorBrutoImpuesto = 0;
        var impuestoGlobal = 0;

        controlGrillaImpuesto = document.getElementById("gvImpuesto");
        if (controlGrillaImpuesto !== null) {
            for (i = 1; i < controlGrillaImpuesto.rows.length; i++) {
                tasa = parseFloat(controlGrillaImpuesto.rows[i].cells[2].innerHTML);
                baseGravable = parseFloat(controlGrillaImpuesto.rows[i].cells[3].innerHTML);
                baseMinima = parseFloat(controlGrillaImpuesto.rows[i].cells[4].innerHTML);
                valorBrutoImpuesto = parseFloat(netoGlobal);

                if (valorBrutoImpuesto > baseMinima) {
                    valorImpuesto = parseFloat(parseFloat(parseFloat(valorBrutoImpuesto) * parseFloat(baseGravable) / 100) * tasa / 100).toFixed(nodecimales);
                }

                impuestoGlobal += parseFloat(valorImpuesto);
                controlGrillaImpuesto.rows[i].cells[5].innerHTML = valorImpuesto;
            }
        }

        controlImpuesto = document.getElementById("nitxtTotalImpuesto");

        if (controlNetoTotal !== null) {
            controlImpuesto.value = impuestoGlobal;
            hdImpuesto = document.getElementById("hdImpuesto");
            hdImpuesto.value = impuestoGlobal;
        }

        controlNetoTotal = document.getElementById("nitxtTotal");
        var netoTotal = 0;
        if (controlNetoTotal !== null) {
            netoTotal = parseFloat(netoGlobal) + parseFloat(impuestoGlobal);
            controlNetoTotal.value = netoTotal.toFixed(nodecimales);
            hdValorNetoRef = document.getElementById("hdValorNetoRef");
            hdValorNetoRef.value = netoTotal;
        }
    }

}
function formato_numero(entrada, decimales, separador_decimal, separador_miles) {

    var separador = ".";
    var ultimoNumero = "";
    var decimales = "";

    if (window.event.keyCode !== 9) {
        var indiceDePunto = entrada.value.indexOf(separador);
        ultimoNumero = entrada.value.substring(0, entrada.value.length - 1);
        if (indiceDePunto > 0) {

            var numero = entrada.value.replace(/\./g, '');
            numero = entrada.value.replace(/\,/g, '');
            if (isNaN(numero)) {
                alert('numero no valido');
                entrada.value = ultimoNumero;
            } else {
                var split = entrada.value.split(separador);
                if (split.length > 2) {
                    alert("numero no valido");
                    entrada.value = ultimoNumero;
                }
                else {
                    if (entrada.value.substring(entrada.value.indexOf(separador), entrada.value.length - 1).length > 4) {
                        var entero = entrada.value.substring(0, entrada.value.indexOf(separador));
                        var intseparador = entrada.value.indexOf(separador);
                        var decimal = entrada.value.substring(intseparador + 1, intseparador + 5);
                        entrada.value = entero + separador + decimal;
                        console.log("entro y el vlor es" + entrada.value);
                        return false;
                    }
                }
            }
        }
        else {

            if (entrada.value.replace(/\,/g, '') !== '') {
                var num = parseFloat(entrada.value.replace(/\,/g, ''));

                if (!isNaN(num)) {
                    num = num.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                    num = num.split('').reverse().join('').replace(/^[\,]/, '');
                    entrada.value = num;

                }
                else {
                    alert('Solo se permiten números');
                    entrada.value = 0;
                }
            }
        }
    }

}
function addTab(title, url) {
    if (contador < 8) {
        if ($('#tt').tabs('exists', title)) {
            $('#tt').tabs('select', title);
        } else {
            contador++;
            var content = '<iframe  frameborder="0"  src="' + url + '" style="width:100%;min-height:100vh;" onload="resizeIframe(this);"></iframe>';
            $('#tt').tabs('add', {
                title: title,
                content: content,
                closable: true
            });
        }
    }
}
function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';
}


function cargarEventos() {
    $(document).ready(function () {
        $('.chosen-select').chosen();

        $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
        $('.chzn-select').chosen();
        $('.chzn-select-deselect').chosen({ allow_single_deselect: true });

        var localise = $(".multiselect").val();

        if (localise != null || typeof (localise) !== "undefined") {
            $.localise('ui-multiselect', { language: 'es', path: 'http://app.infos.com/recursosinfos/js/locale/' });
            $(".multiselect").multiselect();
        }


        $('.mayuscula').keydown(function () {
            $(this).val($(this).val().toUpperCase());
        });

        $(".loading").fadeOut(750);

        $(".fecha").daterangepicker({
            singleDatePicker: true,
            "locale": {
                "format": "DD/MM/YYYY"
            }
        });

        $('#tdCampos input[type="checkbox"]').each(function () {
            var self = $(this),
                label = self.next(),
                label_text = label.text();

            label.remove();
            self.iCheck({
                checkboxClass: 'icheckbox_line-blue',
                radioClass: 'iradio_line-blue',
                insert: '<div class="icheck_line-icon"></div>' + label_text
            });

            self.on('ifClicked', function () {
                this.click();
            });
        });
    });
    function confirmSwal($this, title, text) {
        swal({
            title: title,
            text: text,
            type: "warning",
            showCancelButton: true,
            confirmButtonText: "Aceptar",
            cancelButtonText: "Cancelar"
        }).then(function () {
            $this.onclick = '';
            //$this= $($this);
            $this.click();
        });
        return false;
    }
    function formato(entrada) {
        if (window.event.keyCode !== 9) {
            var num = entrada.value.replace(/\,/g, '');
            var totalGlobal = 0;
            var total = 0;
            var valorDescuento = 0;
            var netoGlobal = 0;
            var totalNeto = 0;
            var descuentoGloabal = 0;
            var controlNetoTotal = 0;

            if (!isNaN(num)) {
                num = num.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                num = num.split('').reverse().join('').replace(/^[\,]/, '');
                entrada.value = num;
                controlGrillaRef = document.getElementById("gvReferencia");
                if (controlGrillaRef !== null) {
                    for (i = 1; i < controlGrillaRef.rows.length; i++) {
                        objeto = controlGrillaRef.rows[i].getElementsByTagName("input");

                        if (objeto !== null) {
                            total = parseFloat(parseFloat(objeto[1].value.replace(/\,/g, '')) * parseFloat(objeto[2].value.replace(/\,/g, ''))).toFixed(2);
                            valorDescuento = total * (parseFloat(objeto[3].value.replace(/\,/g, '')) / 100);
                            totalNeto = parseFloat(total - valorDescuento).toFixed(2);

                            if (!isNaN(total)) {
                                totalGlobal += parseFloat(total);
                                descuentoGloabal += parseFloat(valorDescuento);
                                netoGlobal += parseFloat(totalNeto).toFixed(2);

                                total = total.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                                total = total.split('').reverse().join('').replace(/^[\,]/, '');
                                controlGrillaRef.rows[i].cells[6].innerHTML = total;

                                valorDescuento = valorDescuento.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                                valorDescuento = valorDescuento.split('').reverse().join('').replace(/^[\,]/, '');
                                controlGrillaRef.rows[i].cells[8].innerHTML = valorDescuento;

                                totalNeto = totalNeto.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                                totalNeto = totalNeto.split('').reverse().join('').replace(/^[\,]/, '');
                                controlGrillaRef.rows[i].cells[9].innerHTML = totalNeto;
                            }
                        }
                    }
                }

                controlValorTotal = document.getElementById("nitxtTotalValorBruto");

                if (controlValorTotal !== null) {
                    totalGlobal = totalGlobal.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                    totalGlobal = totalGlobal.split('').reverse().join('').replace(/^[\,]/, '');
                    controlValorTotal.value = totalGlobal;
                    hdValorTotalRef = document.getElementById("hdValorTotalRef");
                    hdValorTotalRef.value = totalGlobal;
                }

                controlDescuento = document.getElementById("nitxtTotalDescuento");

                if (controlDescuento !== null) {
                    descuentoGloabal = descuentoGloabal.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                    descuentoGloabal = descuentoGloabal.split('').reverse().join('').replace(/^[\,]/, '');
                    controlDescuento.value = descuentoGloabal;
                    hdDescuentoRef = document.getElementById("hdDescuentoRef");
                    hdDescuentoRef.value = descuentoGloabal;
                }
                var baseGravable = 0;
                var tasa = 0;
                var baseMinima = 0;
                var valorImpuesto = 0;
                var valorBrutoImpuesto = 0;
                var impuestoGlobal = 0;

                controlGrillaImpuesto = document.getElementById("gvImpuesto");
                if (controlGrillaImpuesto !== null) {
                    for (i = 1; i < controlGrillaImpuesto.rows.length; i++) {
                        tasa = parseFloat(controlGrillaImpuesto.rows[i].cells[2].innerHTML);
                        baseGravable = parseFloat(controlGrillaImpuesto.rows[i].cells[3].innerHTML);
                        baseMinima = parseFloat(controlGrillaImpuesto.rows[i].cells[4].innerHTML);
                        valorBrutoImpuesto = parseFloat(netoGlobal);

                        if (valorBrutoImpuesto > baseMinima) {
                            valorImpuesto = parseFloat(parseFloat(parseFloat(valorBrutoImpuesto) * parseFloat(baseGravable) / 100) * tasa / 100).toFixed(2);
                        }



                        impuestoGlobal += parseFloat(valorImpuesto);
                        valorImpuesto = valorImpuesto.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        valorImpuesto = valorImpuesto.split('').reverse().join('').replace(/^[\,]/, '');
                        controlGrillaImpuesto.rows[i].cells[5].innerHTML = valorImpuesto;
                    }
                }

                controlImpuesto = document.getElementById("nitxtTotalImpuesto");

                if (controlNetoTotal !== null) {
                    impuestoGlobal = impuestoGlobal.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                    impuestoGlobal = impuestoGlobal.split('').reverse().join('').replace(/^[\,]/, '');
                    controlImpuesto.value = impuestoGlobal;
                    hdImpuesto = document.getElementById("hdImpuesto");
                    hdImpuesto.value = impuestoGlobal;
                }

                controlNetoTotal = document.getElementById("nitxtTotal");
                var netoTotal = 0;
                if (controlNetoTotal !== null) {
                    netoTotal = parseFloat(netoGlobal) + parseFloat(impuestoGlobal);
                    netoTotal = netoTotal.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                    netoTotal = netoTotal.split('').reverse().join('').replace(/^[\,]/, '');
                    controlNetoTotal.value = netoTotal;
                    hdValorNetoRef = document.getElementById("hdValorNetoRef");
                    hdValorNetoRef.value = netoTotal;
                }
            }
            else {
                alert('Solo se permiten números');
                entrada.value = entrada.value.replace(/[^\d\.\,]*/g, '');
            }
        }
    }
    function formato_numero(entrada, decimales, separador_decimal, separador_miles) {

        var separador = ".";
        var ultimoNumero = "";
        var decimales = "";

        if (window.event.keyCode !== 9) {
            var indiceDePunto = entrada.value.indexOf(separador);
            ultimoNumero = entrada.value.substring(0, entrada.value.length - 1);
            if (indiceDePunto > 0) {

                var numero = entrada.value.replace(/\./g, '');
                numero = entrada.value.replace(/\,/g, '');
                if (isNaN(numero)) {
                    alert('numero no valido');
                    entrada.value = ultimoNumero;
                } else {
                    var split = entrada.value.split(separador);
                    if (split.length > 2) {
                        alert("numero no valido");
                        entrada.value = ultimoNumero;
                    }
                    else {
                        if (entrada.value.substring(entrada.value.indexOf(separador), entrada.value.length - 1).length > 4) {
                            var entero = entrada.value.substring(0, entrada.value.indexOf(separador));
                            var intseparador = entrada.value.indexOf(separador);
                            var decimal = entrada.value.substring(intseparador + 1, intseparador + 5);
                            entrada.value = entero + separador + decimal;
                            return false;
                        }
                    }
                }
            }
            else {

                if (entrada.value.replace(/\,/g, '') !== '') {
                    var num = parseFloat(entrada.value.replace(/\,/g, ''));

                    if (!isNaN(num)) {
                        num = num.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                        num = num.split('').reverse().join('').replace(/^[\,]/, '');
                        entrada.value = num;

                    }
                    else {
                        alert('Solo se permiten números');
                        entrada.value = 0;
                    }
                }
            }
        }

    }
    function addTab(title, url) {
        if (contador < 8) {
            if ($('#tt').tabs('exists', title)) {
                $('#tt').tabs('select', title);
            } else {
                contador++;
                var content = '<iframe  frameborder="0"  src="' + url + '" style="width:100%;min-height:100vh;" onload="resizeIframe(this);"></iframe>';
                $('#tt').tabs('add', {
                    title: title,
                    content: content,
                    closable: true
                });
            }
        }
    }
    function resizeIframe(obj) {
        obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';
    }

}
