function mascaraMoeda(campo, evento) {
    var tecla = (!evento) ? window.event.keyCode : evento.which;
    var valor = campo.value.replace(/[^\d]+/gi, '').reverse();
    var resultado = "";
    var mascara = "R$ ###.###.###.###,##".reverse();

    for (var x = 0, y = 0; x < mascara.length && y < valor.length;) {
        if (mascara.charAt(x) != '#') {
            resultado += mascara.charAt(x);
            x++;
        }
        else {
            resultado += valor.charAt(y);
            y++;
            x++;
        }
    }
    campo.value = resultado.reverse();

}

String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

function mascaraData(val) {
    var pass = val.value;
    var expr = /[0123456789]/;

    for (i = 0; i < pass.length; i++) {

        // charAt -> retorna o caractere posicionado no índice especificado
        var lchar = val.value.charAt(i);
        var nchar = val.value.charAt(i + 1);

        if (i == 0) {
            if ((lchar.search(expr) != 0) || (lchar > 3)) {
                val.value = "";
            }

        }

        else if (i == 1) {
           
            if (lchar.search(expr) != 0) {
                var tst1 = val.value.substring(0, (i));
                val.value = tst1;
                continue;
            }

            if ((nchar != '/') && (nchar != '')) {
                var tst1 = val.value.substring(0, (i) + 1);

                if (nchar.search(expr) != 0)
                    var tst2 = val.value.substring(i + 2, pass.length);
                else
                    var tst2 = val.value.substring(i + 1, pass.length);

                val.value = tst1 + '/' + tst2;
            }
        }
        //else if (i == 4) {
        //    debugger;
        //    if (lchar.search(expr) != 0) {
        //        var tst1 = val.value.substring(0, (i));
        //        val.value = tst1;
        //        continue;
        //    }

        //    if ((nchar != '/') && (nchar != '')) {
        //        var tst1 = val.value.substring(0, (i) + 1);

        //        if (nchar.search(expr) != 0)
        //            var tst2 = val.value.substring(i + 2, pass.length);
        //        else
        //            var tst2 = val.value.substring(i + 1, pass.length);

        //        val.value = tst1 + '/' + tst2;
        //    }
        //}

        if (i >= 6) {
            if (lchar.search(expr) != 0) {
                var tst1 = val.value.substring(0, (i));
                val.value = tst1;
            }
        }
    }

    if (pass.length > 7)
        val.value = val.value.substring(0, 10);
    return true;
}



document.addEventListener('DOMContentLoaded', () => {
    notificationEvent();
});

function notificationEvent() {
    (document.querySelectorAll('.notification .delete') || []).forEach(($delete) => {
        const $notification = $delete.parentNode;

        $delete.addEventListener('click', () => {
            $notification.parentNode.removeChild($notification);
        });

        fadeOutEffect($notification);
    });
}

function fadeOutEffect(fadeTarget) {
    setTimeout(() => {
        var fadeEffect = setInterval(() => {
            if (!fadeTarget.style.opacity) {
                fadeTarget.style.opacity = 1;
            }
            if (fadeTarget.style.opacity > 0) {
                fadeTarget.style.opacity -= 0.1;
            } else {
                clearInterval(fadeEffect);
            }
        }, 200);

        setTimeout(() => {
            fadeTarget.parentNode.removeChild(fadeTarget);
        }, 2000);
    }, 6000);
}