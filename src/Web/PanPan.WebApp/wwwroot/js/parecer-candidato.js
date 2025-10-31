function gerarParecer(id) {
    console.log("chegamos aqui");

    let resp = $('#resp').val();

    if (!resp || resp.trim() === "") {
        alert("Por favor, insira o nome do recrutador antes de gerar o documento.");
        return;
    }

    $.ajax({
        url: `/candidatos/${id}/gerar-parecer?resp=${resp}`,
        contentType: "application/json",
        cache: false,
        type: 'POST',
        data: null,
        headers: null,
        success: function (response) {

            let blob = recuperarBytes(response);

            if (window.navigator.msSaveOrOpenBlob) {
                window.navigator.msSaveOrOpenBlob(blob);
            } else {
                let link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.target = "_blank";
                link.click();
            }            
        },
        error: function (response, status, error) {
            console.log(response);
        }

    })
}

function recuperarBytes(data) {
    var binary_string = window.atob(data);
    var len = binary_string.length;
    var bytes = new Uint8Array(len);

    for (var i = 0; i < len; i++) {
        bytes[i] = binary_string.charCodeAt(i);
    }

    let bytess = new Uint8Array(bytes);
    let blob = new Blob([bytess], { type: 'application/pdf' });

    return blob;
}