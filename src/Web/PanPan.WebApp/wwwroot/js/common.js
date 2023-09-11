var common = common || {}

common = {
    getHeader: function () {
        let headers = {};
        headers['RequestVerificationToken'] = $('[name=__RequestVerificationToken]').val();

        return headers;
    },
    errorNotify: function (response, key) {
        if (response.responseJSON) {
            sessionStorage.setItem(key, response.responseJSON.message);
        }
    } 
}