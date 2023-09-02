(function($) {
    $.organicTabs = function(el, options) {
        var base = this;
        base.$el = $(el);
        base.$nav = base.$el.find(".nav");
 
        base.init = function() {
            base.options = $.extend({},$.organicTabs.defaultOptions, options);
            // Accessible hiding fix
            $(".oculto").css({
                "position": "relative",
                "top": 0,
                "left": 0,
                "display": "none"
            }); 
            base.$nav.delegate("li &gt; a", "click", function() {
                // Determinar el nombre del bloque que va a quedar oculto
                var curList = base.$el.find("a.current").attr("href").substring(1),
                    // Determinar el nombre del nuevo bloque 
                    $newList = $(this),
                    listID = $newList.attr("href").substring(1),
                    // Set outer wrapper height to (static) height of current inner list
                    $allListWrap = base.$el.find(".list-wrap"),
                    curListHeight = $allListWrap.height();
                $allListWrap.height(curListHeight);
 
                if ((listID != curList)( base.$el.find(":animated").length == 0)) {
                    // Ocultar progresivamente el bloque previo
                    base.$el.find("#"+curList).fadeOut(base.options.speed, function() {
                        // En la funcion callback, presentar progresivamente (Fade in) el
                        // nuevo bloque
                        base.$el.find("#"+listID).fadeIn(base.options.speed);
                        // Ajustar la altura del div envolvente a la altura del nuevo bloque
                        var newHeight = base.$el.find("#"+listID).height();
                        $allListWrap.animate({
                            height: newHeight
                        });
 
                        // Marcar la nueva pestaña como pestaña activa
                        base.$el.find(".nav li a").removeClass("current");
                        $newList.addClass("current");
                    });
                }   
 
                // Evitar la propagación del evento click
                return false;
            });
 
        };
        base.init();
    };
 
    $.organicTabs.defaultOptions = {
        "speed": 300
    };
 
    $.fn.organicTabs = function(options) {
        return this.each(function() {
            (new $.organicTabs(this, options));
        });
    };
 
})(jQuery);
 
jQuery("#ejemplo").organicTabs();