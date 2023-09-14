let experienciaId;
var experiencia = experiencia || {}

experiencia = {

    obter: function () {
        let candidatoId = document.querySelector('#input-candidato-id').value;
        httpClient.send('GET', `/candidatos/${candidatoId}/experiencias`, null, this.obterSuccess, this.fail, httpClient.complete, '');
    },
    incluir: function (element) {
        let model = this.modelRequest(element);
        httpClient.send('POST', `/candidatos/experiencia/incluir`, model, this.incluirSuccess, this.fail, this.complete, '');
    },
    alterar: function (element) {
        let model = this.modelRequest(element);
        httpClient.send('POST', `/candidatos/experiencia/alterar`, model, this.alterarSuccess, this.fail, httpClient.complete, '');
    },
    modelRequest: function (element) {
        request = {            
            CandidatoId: document.querySelector('#input-candidato-id').value,
            Id: element.querySelector('#input-experiencia-id').value,
            Empresa: element.querySelector('#txt-empresa').value,
            Cargo: element.querySelector('#txt-cargo').value,
            Atividade: element.querySelector('#txt-atividade').value,
            RegimeContratacao: element.querySelector('#slct-regime-contratacao').value,
            Habilidade: element.querySelector('#txt-habilidade').value,
            UltimoSalario: element.querySelector('#txt-ultimo-salario').value,
            DataInicioString: element.querySelector('#txt-data-inicio').value,
            DataFimString: element.querySelector('#txt-data-fim').value,
            Status: element.querySelector('#slct-status').value,
            MotivoDesligamento: element.querySelector('#txt-motivo-desligamento').value,
            Observacoes: element.querySelector('#txt-observacoes').value
        };

        experienciaId = request.Id;

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
        });

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

        document.querySelectorAll('.back').forEach(($back) => {
            $back.addEventListener('click', () => {
                closeAllModals();
            });
        });
        closeAllModals();

    },
    alterarSuccess: function (response) {
        let partial = document.getElementById(experienciaId);

        let content = partial.querySelector('.modal-content');

        content.querySelector('section').querySelector('div').remove();

        content.querySelector('section').innerHTML = response;

        content.querySelectorAll('.selectize').forEach(($select) => {
            $($select).selectize();
        });

        notificationEvent();
    },

    fail: function (response) {

    },
    complete: function() {
        experiencia.obter();
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
