var candidato = candidato || {}

candidato.index = {
    candidatosRequest: function (pagenumber = 1) {
        httpClient.send('POST', '/candidatos', this.modelRequest(pagenumber), this.success, this.fail, httpClient.complete, '');
    },

    modelRequest: function (pagenumber) {
        request = {
            Nome: document.getElementById('txt-nome').value,
            Email: document.getElementById('txt-email').value,
            Telefone: document.getElementById('txt-telefone').value,
            Celular: document.getElementById('txt-celular').value,
            CelularAlternativo: document.getElementById('txt-celular-alternativo').value,
            Bairro: document.getElementById('txt-bairro').value,
            Estado: document.getElementById('slct-estado').value,
            Cidade: document.getElementById('slct-cidade').value,
            PageSize: parseInt(document.getElementById('slct-exibir').value),
            OrderBy: document.getElementById('slct-ordernar').value,
            PageNumber: pagenumber
        };
        return request;
    },

    success: function (response) {
        let content = document.getElementsByClassName('table-content')[0];
        content.querySelector('div').remove();

        content.innerHTML = response;

        candidato.index.onClick();
    },
    fail: function (response) {

    },
    clear: function () {
        var inputs = document.querySelector('[class="box"]').querySelectorAll('[type="text"]');
        inputs.forEach(function (input) {
            input.value = "";
            input.removeAttribute('selected')
            input.readOnly = false;
        })

        atribuirEstados('slct-estado', 'reload');
        buscaCidades($("#slct-estado").val(), 'slct-cidade', 'reload');
    },
    onClick: function () {
        $('.search').on('click', function () {
            if (this.value === "" || this.value === undefined)
                this.value = 1;

            candidato.index.candidatosRequest(this.value);
        });

        $('.clear').on('click', function () {
            candidato.index.clear();
            candidato.index.candidatosRequest();
        });
    }
}

window.onload = function () {
    candidato.index.onClick();


}