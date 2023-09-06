
$(document).ready(function () {
    const radio = $("[type='radio'][checked='checked']")
    radio.map(function () {
        let radioItem = $(this);
        radioItem.parent().addClass("active");
        let id = radioItem.attr("id");
        let content = $(`#tab-${id}`);
        content.addClass("active");
    });

    $("#radio-tipo-formacao-ensino").click(function () {
        apresentar($(this));
    });

    $("#radio-tipo-formacao-curso").click(function () {
        apresentar($(this));
    });

})

function apresentar(element) {

    $(".ai-form-tab-content").removeClass("active");

    let radioItem = element;
    radioItem.parent().addClass("active");
    let id = radioItem.attr("id");
    let content = $(`#tab-${id}`);
    content.addClass("active");
}
