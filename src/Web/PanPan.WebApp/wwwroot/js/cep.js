function BuscaCep() {
    $(document).ready(function () {
        function limpa_formulário_cep() {

            var session = document.getElementsByClassName('endereco')[0];

            var inputs = session.querySelectorAll('[type="text"]');
            inputs.forEach(function (input) {
                input.value = "";
                input.removeAttribute('selected')
                input.readOnly = false;
            })

            atribuirEstados('slct-estado', 'reload');
            buscaCidades($("#slct-estado").val(), 'slct-cidade', 'reload');
        }

        //Quando o campo cep perde o foco.
        $("#txt-cep").blur(function () {

            var cep = $(this).val().replace(/\D/g, '');
            if (cep != "") {

                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    $("#txt-logradouro").val("...");
                    $("#txt-bairro").val("...");
                    $("#slct-estado").val("...");
                    $("#slct-cidade").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {
                            if (!("erro" in dados)) {

                                $("#txt-logradouro").val(dados.logradouro);
                                $("#txt-bairro").val(dados.bairro);


                                atribuirEstados('slct-estado', 'reload', dados.uf);
                                buscaCidades($("#slct-estado").val(), 'slct-cidade', 'reload', dados.localidade);

                                document.getElementById('txt-logradouro').readOnly = true;
                                document.getElementById('txt-bairro').readOnly = true;
                            }
                            else {
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                }
                else {
                    limpa_formulário_cep();
                    alert("Formato de CEP inválido.");
                }
            }
            else {
                limpa_formulário_cep();
            }
        });
    });
}