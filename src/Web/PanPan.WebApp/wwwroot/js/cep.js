function BuscaCep() {
    $(document).ready(function () {

        function limpa_formulário_cep() {
            // Limpa valores do formulário de cep.
            $("#txt-logradouro").val("");
            $("#txt-numero").val("");
            $("#txt-bairro").val("");
            $("#slct-cidade").val("");
            $("#slct-estado").val("");

            document.getElementById('txt-logradouro').readOnly = false;
            document.getElementById('txt-bairro').readOnly = false;
            document.getElementById('slct-estado').readOnly = false;
            document.getElementById('slct-cidade').readOnly = false;  
        }

        //Quando o campo cep perde o foco.
        $("#txt-cep").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#txt-logradouro").val("...");
                    $("#txt-bairro").val("...");
                    $("#slct-estado").val("...");
                    $("#slct-cidade").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#txt-logradouro").val(dados.logradouro);
                                $("#txt-bairro").val(dados.bairro);  

                                var estados = document.getElementById("slct-estado");

                                setSelectedValue(estados, dados.uf);

                                function setSelectedValue(selectObj, valueToSet) {
                                    for (var i = 0; i < selectObj.options.length; i++) {
                                        if (selectObj.options[i].value == valueToSet) {
                                            selectObj.options[i].selected = true;
                                            return;
                                        }
                                    }
                                }

                                buscaCidades($("#slct-estado").val(), 'slct-cidade', 'reload', dados.localidade);

                                document.getElementById('txt-logradouro').readOnly = true;
                                document.getElementById('txt-bairro').readOnly = true;
                                document.getElementById('slct-estado').readOnly = true;
                                document.getElementById('slct-cidade').readOnly = true;  
                               
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    limpa_formulário_cep();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                limpa_formulário_cep();
            }
        });
    });
}