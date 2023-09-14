var formacao = formacao || {}

formacao = {
    obter: function () {
        let candidatoId = document.querySelector('#input-candidato-id').value;
        httpClient.send('GET', `/candidatos/${candidatoId}/formacoes`, null, this.obterSuccess, this.fail, httpClient.complete, '');
    },
    incluir: function (element) {
        let model = this.modelRequest(element);
        httpClient.send('POST', `/candidatos/formacao/incluir`, model, this.incluirSuccess, this.fail, httpClient.complete, '');
    },
    alterar: function (element) {
        let model = this.modelRequest(element);
        httpClient.send('POST', `/candidatos/formacao/alterar`, model, this.alterarSuccess, this.fail, httpClient.complete, '');
    },
    modelRequest: function (element) {
        request = {            
            CandidatoId: document.querySelector('#input-candidato-id').value,
            Id: element.querySelector('#formacao-id').value,
            NomeInstituicao: element.querySelector('#txt-instituicao').value,
            Descricao: element.querySelector('#txt-descricao').value,
            Tipo: element.querySelector('input[name="Tipo"]:checked').value,
            Ensino: element.querySelector('#slct-ensino').value,
            Curso: element.querySelector('#slct-curso').value,
            Periodo: element.querySelector('#slct-periodo').value,
            DataInicioString: element.querySelector('#txt-data-inicio').value,
            DataFimString: element.querySelector('#txt-data-fim').value,
            Status: element.querySelector('#slct-status').value
        };
        return request;
    },
    obterSuccess: function (response) {
        let partial = document.getElementById('tbl-formacoes');

        if (!partial.querySelector('div') && partial.querySelector('div') != undefined)
            partial.querySelector('div').remove();

        partial.innerHTML = response;

        partial = document.getElementById('tbl-formacoes');

        partial.querySelectorAll('.selectize').forEach(($select) => {
            $($select).selectize();
        })

        formacao.onClick();

        (document.querySelectorAll('.alter-formacao, .new-formacao') || []).forEach(($trigger) => {
            $trigger.querySelectorAll("[type='radio'][checked='checked']").forEach(($radio) => {

                formacao.apresentar($radio, $trigger);
            });

            $trigger.querySelectorAll('[type="radio"]').forEach(($radio) => {

                $radio.addEventListener('click', (event) => {
                    formacao.apresentar(event.currentTarget, $trigger);
                });
            })
        });  
    },
    incluirSuccess: function (response) {
        let partial = document.getElementById('nova-formacao');
        let content = partial.querySelector('.modal-content');

        content.querySelector('div').remove();

        content.innerHTML = response;

        (document.querySelectorAll('.new-formacao') || []).forEach(($trigger) => {
            $trigger.querySelector('.is-success').addEventListener('click', () => {
                formacao.incluir($trigger)
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

        partial = document.getElementById('tbl-formacoes');

        (partial.querySelectorAll('.alter-formacao') || []).forEach(($trigger) => {
            $trigger.querySelector('.is-success').addEventListener('click', () => {
                formacao.alterar($trigger)
            });
        });    

        (partial.querySelectorAll('.new-formacao') || []).forEach(($trigger) => {
            $trigger.querySelector('.is-success').addEventListener('click', () => {
                formacao.incluir($trigger)
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
    },
    apresentar: function (element, $trigger) {
        $trigger.querySelectorAll('.ai-form-tab-content').forEach((tab) => {
            tab.classList.remove('active');
        });

        let radioItem = element;
        radioItem.parentElement.classList.add('active');
        let id = radioItem.getAttribute('id');

        let content = $trigger.querySelector(`#tab-${id}`);
        content.classList.add('active');
    }
}

