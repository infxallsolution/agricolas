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