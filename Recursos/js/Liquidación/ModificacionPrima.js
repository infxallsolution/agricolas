(function ($) {
    $(document).ready(function () {
        $(".numeric-field").on('keyup', function () {
            var n = parseInt($(this).val().replace(/\D/g, ''), 10);
            if (isNaN(n)) {
                $(this).val($(this).val().replace(/\D/g, ''));
            } else {
                $(this).val(n.toLocaleString("EN"));
            }
        });
        $("table tr #txvValorPromedio,table tr #txvDiasPromedio").keyup(function () {
            var parent = $(this).closest("tr");
            var valorPromedio = parent.find("#txvValorPromedio");
            var diasPromedio = parent.find("#txvDiasPromedio");
            var valorPromedioVal = valorPromedio.val().replace(/\D/g, '');
            if (isNaN(valorPromedioVal)) {
                return;
            }
            var diasPromedioVal = diasPromedio.val().replace(/\D/g, '');
            if (isNaN(diasPromedioVal) || parseFloat(diasPromedioVal) <= 0) {
                return;
            }
            var baseVal = parseFloat(valorPromedioVal) / parseFloat(diasPromedioVal) * 30;
            parent.find("#txvBase").val(baseVal.toFixed(0));
            parent.find("#txvBase").keyup();
        });
        $("table tr #txvBase,table tr #txvDiasPrima").keyup(function () {
            var parent = $(this).closest("tr");
            var txvBase = parent.find("#txvBase");
            var txvDiasPrima = parent.find("#txvDiasPrima");
            var baseVal = txvBase.val().replace(/\D/g, '');
            if (isNaN(baseVal)) {
                return;
            }
            var diasPrimaVal = txvDiasPrima.val().replace(/\D/g, '');
            if (isNaN(diasPrimaVal)) {
                return;
            }
            var valorPrimaVal = parseFloat(baseVal) * parseFloat(diasPrimaVal) / 360;
            parent.find("#txvValorPrima").val(valorPrimaVal.toFixed(0));
            parent.find("#hfValorPrima").val(valorPrimaVal.toFixed(0));
            parent.find("#txvValorPrima").keyup();
        });

        $(".numeric-field").keyup();
    });
})(jQuery);