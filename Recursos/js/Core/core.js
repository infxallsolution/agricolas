
$(document).ready(function () {
    $.ajaxSetup({
        beforeSend: function () {
            if (!window.timeout)
                window.timeout = [];
            window.timeout.push(setTimeout(function () {
                $(".loading").show();
            }, 1000));
        },
        complete: function () {
            clearTimeout(window.timeout.pop());
            if (window.timeout.length === 0)
                $(".loading").hide();
        }
    });
    var input;
    $('.fecha').daterangepicker({
        singleDatePicker: true,
        showDropdowns: false,
        autoUpdateInput: false,
        "locale": {
            "format": "DD/MM/YYYY"
        }
    }, function (chosen_date) {
        if (typeof (input) !== "undefined") {
            $(input).val(chosen_date.format("DD/MM/YYYY"));
            $(input).change();
        }
    });
    $(".fecha").click(function () {
        input = this;
    });
    if ($(".fecha").val() == "") {
        var f = new Date();
        var dia = f.getDate();
        var mes = f.getMonth() + 1;
        if (dia < 10) {
            dia = "0" + dia;
        }
        if (mes + 1 < 10) {
            mes = "0" + mes;
        }

        $(".fecha").val(dia + "/" + mes + "/" + f.getFullYear());
    }


    $('.multiselect').multiSelect({
        selectableHeader: "<div class='input' style='background-color: #2E4E9D; color:white'>Items a seleccionar</div><input type='text' class='search-input input' autocomplete='off' placeholder=''>",
        selectionHeader: "<div class='input' style='background-color: #2E4E9D; color:white'>Items seleccionados</div><input type='text' class='search-input input' autocomplete='off' placeholder=''>",
        afterInit: function (ms) {
            var that = this,
                $selectableSearch = that.$selectableUl.prev(),
                $selectionSearch = that.$selectionUl.prev(),
                selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

            that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                .on('keydown', function (e) {
                    if (e.which === 40) {
                        that.$selectableUl.focus();
                        return false;
                    }
                });

            that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                .on('keydown', function (e) {
                    if (e.which == 40) {
                        that.$selectionUl.focus();
                        return false;
                    }
                });
        },
        afterSelect: function () {
            this.qs1.cache();
            this.qs2.cache();
        },
        afterDeselect: function () {
            this.qs1.cache();
            this.qs2.cache();
        }
    });

    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    $("#formContainer input[type=checkbox]").each(function () {
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

    $(".icheck input[type=checkbox]").each(function () {
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

    $("form")[0].onsubmit = function () {
        $(".loading").show();
    };
});

var x = null;

function Visualizacion(informe) {

    var opciones = "toolbar = no, location = no, directories = no, status = no, menubar = no, scrollbars = yes, resizable = yes, width =" + screen.availWidth + ", height =" + screen.availHeight + ", top = 0, left = 5";
    sTransaccion = "ImprimeInforme.aspx?informe=" + informe;
    x = window.open(sTransaccion, "", opciones);
    x.focus();
}

function soloNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "1234567890";
    especiales = "";

    tecla_especial = false;
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}
function soloLetrasNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "1234567890abcdefghijklmnopqrstuvwxyz";
    especiales = "";

    tecla_especial = false;
    for (var i in especiales) {
        if (key === especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) === -1 && !tecla_especial) {
        return false;
    }
}
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "abcdefghijklmnopqrstuvwxyz";
    especiales = "";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}
function confirmSwal($this, title, detalle) {
    swal({
        title: title,
        html: detalle,
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar"
    }).then(function () {
        $this.onclick = '';
        $this.click();
    });
    return false;
}

function escapeSelector(sel) {
    var rcssescape = /([\0-\x1f\x7f]|^-?\d)|^-$|[^\0-\x1f\x7f-\uFFFF\w-]/g;
    var fcssescape = function (ch, asCodePoint) {
        if (asCodePoint) {

            // U+0000 NULL becomes U+FFFD REPLACEMENT CHARACTER
            if (ch === "\0") {
                return "\uFFFD";
            }

            // Control characters and (dependent upon position) numbers get escaped as code points
            return ch.slice(0, -1) + "\\" + ch.charCodeAt(ch.length - 1).toString(16) + " ";
        }

        // Other potentially-special ASCII characters get backslash-escaped
        return "\\" + ch;
    };
    return (sel + "").replace(rcssescape, fcssescape);
}

$(window).load(function () {
    $(".loading").fadeOut(750);
});

$.fn.quicksearch = function (target, opt) {

    var timeout, cache, rowcache, jq_results, val = '', e = this, options = $.extend({
        delay: 100,
        selector: null,
        stripeRows: null,
        loader: null,
        noResults: '',
        matchedResultsCount: 0,
        bind: 'keyup',
        onBefore: function () {
            return;
        },
        onAfter: function () {
            return;
        },
        show: function () {
            this.style.display = "";
        },
        hide: function () {
            this.style.display = "none";
        },
        prepareQuery: function (val) {
            return val.toLowerCase().split(' ');
        },
        testQuery: function (query, txt, _row) {
            for (var i = 0; i < query.length; i += 1) {
                if (txt.indexOf(query[i]) === -1) {
                    return false;
                }
            }
            return true;
        }
    }, opt);

    this.go = function () {

        var i = 0,
            numMatchedRows = 0,
            noresults = true,
            query = options.prepareQuery(val),
            val_empty = (val.replace(' ', '').length === 0);

        for (var i = 0, len = rowcache.length; i < len; i++) {
            if (val_empty || options.testQuery(query, cache[i], rowcache[i])) {
                options.show.apply(rowcache[i]);
                noresults = false;
                numMatchedRows++;
            } else {
                options.hide.apply(rowcache[i]);
            }
        }

        if (noresults) {
            this.results(false);
        } else {
            this.results(true);
            this.stripe();
        }

        this.matchedResultsCount = numMatchedRows;
        this.loader(false);
        options.onAfter();

        return this;
    };

    /*
     * External API so that users can perform search programatically. 
     * */
    this.search = function (submittedVal) {
        val = submittedVal;
        e.trigger();
    };

    /*
     * External API to get the number of matched results as seen in 
     * https://github.com/ruiz107/quicksearch/commit/f78dc440b42d95ce9caed1d087174dd4359982d6
     * */
    this.currentMatchedResults = function () {
        return this.matchedResultsCount;
    };

    this.stripe = function () {

        if (typeof options.stripeRows === "object" && options.stripeRows !== null) {
            var joined = options.stripeRows.join(' ');
            var stripeRows_length = options.stripeRows.length;

            jq_results.not(':hidden').each(function (i) {
                $(this).removeClass(joined).addClass(options.stripeRows[i % stripeRows_length]);
            });
        }

        return this;
    };

    this.strip_html = function (input) {
        var output = input.replace(new RegExp('<[^<]+\>', 'g'), "");
        output = $.trim(output.toLowerCase());
        return output;
    };

    this.results = function (bool) {
        if (typeof options.noResults === "string" && options.noResults !== "") {
            if (bool) {
                $(options.noResults).hide();
            } else {
                $(options.noResults).show();
            }
        }
        return this;
    };

    this.loader = function (bool) {
        if (typeof options.loader === "string" && options.loader !== "") {
            (bool) ? $(options.loader).show() : $(options.loader).hide();
        }
        return this;
    };

    this.cache = function () {

        jq_results = $(target);

        if (typeof options.noResults === "string" && options.noResults !== "") {
            jq_results = jq_results.not(options.noResults);
        }

        var t = (typeof options.selector === "string") ? jq_results.find(options.selector) : $(target).not(options.noResults);
        cache = t.map(function () {
            return e.strip_html(this.innerHTML);
        });

        rowcache = jq_results.map(function () {
            return this;
        });

        /*
         * Modified fix for sync-ing "val". 
         * Original fix https://github.com/michaellwest/quicksearch/commit/4ace4008d079298a01f97f885ba8fa956a9703d1
         * */
        val = val || this.val() || "";

        return this.go();
    };

    this.trigger = function () {
        this.loader(true);
        options.onBefore();

        window.clearTimeout(timeout);
        timeout = window.setTimeout(function () {
            e.go();
        }, options.delay);

        return this;
    };

    this.cache();
    this.results(true);
    this.stripe();
    this.loader(false);

    return this.each(function () {

        /*
         * Changed from .bind to .on.
         * */
        $(this).on(options.bind, function () {

            val = $(this).val();
            e.trigger();
        });
    });


};

function formato_numero(entrada, decimales, separador_decimal, separador_miles) {

    if (window.event.keyCode != 9) {
        var indiceDePunto = entrada.value.indexOf('.');
        if (indiceDePunto > 0) {
            var numero = entrada.value.replace(/\./g, '');
            numero = entrada.value.replace(/\,/g, '');
            if (isNaN(numero)) {
                alert('numero no valido');
                entrada.value = num;
            } else {
                var split = entrada.value.split('.');
                if (split.length > 2) {
                    alert("numero no valido");
                    entrada.value = num;
                }
            }
        }
        else {

            if (entrada.value.replace(/\,/g, '') == '') {

            } else {
                var num = parseFloat(entrada.value.replace(/\,/g, ''));


                if (!isNaN(num)) {
                    num = num.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                    num = num.split('').reverse().join('').replace(/^[\,]/, '');
                    entrada.value = num;

                }
                else {
                    alert('Solo se permiten números');
                    entrada.value = entrada.value.replace(/[^\d\.\,]*/g, '');
                }
            }


        }
    }

}
