var experiencia = experiencia || {}

experiencia = {
    obter: function () {
        let candidatoId = document.querySelector('#input-candidato-id').value;
        httpClient.send('GET', `/candidatos/${candidatoId}/experiencias`, null, this.obterSuccess, this.fail, httpClient.complete, '');
    },
    incluir: function (element) {
        let model = this.modelRequest(element);
        httpClient.send('POST', `/candidatos/experiencia/incluir`, model, this.incluirSuccess, this.fail, httpClient.complete, '');
    },
    alterar: function (element) {
        let model = this.modelRequest(element);
        httpClient.send('POST', `/candidatos/experiencia/alterar`, model, this.alterarSuccess, this.fail, httpClient.complete, '');
    },
    modelRequest: function (element) {
        request = {            
            CandidatoId: document.querySelector('#input-candidato-id').value,
            //Id: element.querySelector('#formacao-id').value,
            //NomeInstituicao: element.querySelector('#txt-instituicao').value,
            //Descricao: element.querySelector('#txt-descricao').value,
            //Tipo: element.querySelector('input[name="Tipo"]:checked').value,
            //Ensino: element.querySelector('#slct-ensino').value,
            //Curso: element.querySelector('#slct-curso').value,
            //Periodo: element.querySelector('#slct-periodo').value,
            //DataInicioString: element.querySelector('#txt-data-inicio').value,
            //DataFimString: element.querySelector('#txt-data-fim').value,
            //Status: element.querySelector('#slct-status').value
        };
        return request;
    },
    obterSuccess: function (response) {
        let partial = document.getElementById('tbl-experiencias');

        if (!partial.querySelector('div') && partial.querySelector('div') != undefined)
            partial.querySelector('div').remove();

        partial.innerHTML = response;

        partial = document.getElementById('tbl-experiencias');

        partial.querySelectorAll('.selectize').forEach(($select) => {
            $($select).selectize();
        })

        experiencia.onClick();
    },
    incluirSuccess: function (response) {
        let partial = document.getElementById('nova-experiencia');
        let content = partial.querySelector('.modal-content');

        content.querySelector('div').remove();

        content.innerHTML = response;

        (document.querySelectorAll('.new-experiencia') || []).forEach(($trigger) => {
            $trigger.querySelector('.is-success').addEventListener('click', () => {
                experiencia.incluir($trigger)
            });
        });

        content.querySelectorAll('.selectize').forEach(($select) => {
            $($select).selectize();
        })

        (content.querySelectorAll('.back') || []).forEach(($back) => {
            $back.addEventListener('click', () => {
                closeAllModals();
            });
        });
        closeAllModals();

    },
    alterarSuccess: function (response) {

    },

    fail: function (response) {

    },
    onClick: function () {

        partial = document.getElementById('tbl-experiencias');

        (partial.querySelectorAll('.alter-experiencia') || []).forEach(($trigger) => {
            $trigger.querySelector('.is-success').addEventListener('click', () => {
                experiencia.alterar($trigger)
            });
        });    

        (partial.querySelectorAll('.new-experiencia') || []).forEach(($trigger) => {
            $trigger.querySelector('.is-success').addEventListener('click', () => {
                experiencia.incluir($trigger)
            });
        }); 

        (partial.querySelectorAll('.js-modal-trigger') || []).forEach(($trigger) => {
            const modal = $trigger.dataset.target;
            const $target = document.getElementById(modal);

            $trigger.addEventListener('click', () => {
                openModal($target);
            });
        });

        (partial.querySelectorAll('.back') || []).forEach(($back) => {
            $back.addEventListener('click', () => {
                closeAllModals();
            });
        });
    }
}
