var httpClient = httpClient || {}

httpClient = {
    sendAsync: function (method, url, parameter, success, fail, complete, sessionKey) {
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: method,
            data: parameter,
            headers: common.getHeader(),
            success: success,
            error: function (response) {
                fail(response, sessionKey);
            },
            complete : complete
        });
    },
    send: function (method, url, parameter, success, fail, complete, sessionKey) {
        var data = '';

        $.ajax({
            url: url,
            async: false,
            cache: false,
            type: method,
            data: parameter,
            headers: common.getHeader(),
            success: function (response) {
                data = success(response);
            },
            error: function (response) {
                fail(response, sessionKey);
            },
            complete: complete
        });
        return data;
    },
    fail: function (response, sessionKey) {
        common.errorNotify(response, sessionKey);
    },
    complete: function () { }
}