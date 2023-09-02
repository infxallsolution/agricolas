function formato(entrada) {
    if (window.event.keyCode != 9) {
        var indiceDePunto = entrada.value.indexOf('.');
        if (indiceDePunto > 0) {
            var numero = entrada.value.replace(/\./g, '');
            numero = entrada.value.replace(/\,/g, '');
            if (!isNaN(numero)) {
                entrada.value = num;
            } else {
                var split = entrada.value.split('.');
                if (split.length > 3) {
                    entrada.value = num;
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
                    entrada.value = entrada.value.replace(/[^\d\.\,]*/g, '');
                }
            }
        }
    }
}

//function initCurrenciesDetalle(){
//	
//	$(".currency").each(function () {
//        var setting = AutoNumeric.getPredefinedOptions().dollar;
//        setting.eventBubbles = false;
//        new AutoNumeric(this, setting);
//    });
//	
//}

function initNumbers() {
    window.autoNumericGlobalList = undefined;
    $("#Content_gvDetalleFactura .number").each(function () {
        var setting = AutoNumeric.getPredefinedOptions().dotDecimalCharCommaSeparator;
        new AutoNumeric(this, setting);
    });
    
	$(".currency").each(function () {
        var setting = AutoNumeric.getPredefinedOptions().dotDecimalCharCommaSeparator;
		setting.currencySymbol = "$";
        new AutoNumeric(this, setting);
    });
}

function readyFacturacion() {
    ready();
    initNumbers();
}

$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(readyFacturacion);
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
        $(".loading").show();
    });
    readyFacturacion();
	
});