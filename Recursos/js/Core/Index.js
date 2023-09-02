function Reloj() {
    var tiempo = new Date();
    $("#lbFecha").text(tiempo.toLocaleString());
    setTimeout('Reloj()', 1000);
}

function mainmenu() {
    $('.navigation li').hover(
        function () {
            $('ul', this).fadeIn();
        },
        function () {
            $('ul', this).fadeOut();
        }
    );
    //por ricardo gomez
    $("#nav ul").css({ display: "none" });
    $(" #nav li ul li").hover(function () {
        $(this).find('ul:first:hidden').css({ visibility: "visible", display: "none" }).slideDown(300);
    }, function () {
        $(this).find('ul:first').slideUp(800);
    });

    $("#nav li").click(function (e) {
        if (!$(this).hasClass('selected')) {
            $("body").find("#nav li.selected").find('ul').slideUp(800);
            $("body").find("#nav li").removeClass('selected');
            $(this).addClass("selected");
            $(this).find('ul:first:hidden').css({ visibility: "visible", display: "none" }).slideDown(300);
        }
        else {

            $(this).find('ul:first').slideUp(800);
            $("#nav li").removeClass("selected");
        }
        e.stopPropagation();
    }
    );

    $("body").click(function () { // binding onclick to body
        $("body").find("#nav li.selected").find('ul').slideUp(800); // hiding popups
        $("body").find("#nav li").removeClass('selected');
    });
}
var contador = 0;
function contadorMenos() {
    contador--;
}
function addTab(title, url) {
    if (contador < 8) {
        if ($('#tt').tabs('exists', title)) {
            $('#tt').tabs('select', title);
        } else {
            contador++;
            var content = '<iframe  frameborder="0"  src="' + url + '" style="width:100%;height:100vh"></iframe>';
            $('#tt').tabs('add', {
                title: title,
                content: content,
                closable: true
            });

        }
    }
}

$(document).ready(function () {
    mainmenu();
    Reloj();
    $('#tt').on('click', '.tabs-close', function () {
        contadorMenos();
    });
});