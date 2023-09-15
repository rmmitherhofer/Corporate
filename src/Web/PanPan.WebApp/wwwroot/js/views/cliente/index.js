var cliente = cliente || {}

cliente.index = {
    clientesRequest: function (pagenumber = 1) {
        httpClient.send('POST', '/clientes', this.modelRequest(pagenumber), this.success, this.fail, httpClient.complete, '');
    },

    modelRequest: function (pagenumber) {
        request = {
            Cnpj: document.getElementById('txt-cnpj').value,
            Nome: document.getElementById('txt-nome').value,
            Representante: document.getElementById('txt-representante').value,
            Email: document.getElementById('txt-email').value,
            Telefone: document.getElementById('txt-telefone').value,
            Celular: document.getElementById('txt-celular').value,   
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

        cliente.index.onClick();
    },
    fail: function (response) {

    },
    clear: function () {
        var inputs = document.querySelector('[class="box"]').querySelectorAll('[type="text"]');

        inputs.forEach(function (input) {
            input.value = "";
            input.removeAttribute('selected')
            input.readOnly = false;
        });

        atribuirEstados('slct-estado', 'reload');
        buscaCidades($("#slct-estado").val(), 'slct-cidade', 'reload');
    },
    onClick: function () {
        $('.search').on('click', function () {
            if (this.value === "" || this.value === undefined)
                this.value = 1;

            cliente.index.clientesRequest(this.value);
        });

        $('.clear').on('click', function () {
            cliente.index.clear();
            cliente.index.clientesRequest();
        });
    }
}

window.onload = function () {
    cliente.index.onClick();
    $('.selectize').selectize();
}