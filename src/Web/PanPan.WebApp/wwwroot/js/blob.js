var blob = blob || {};

var types = { pdf: 'application/pdf' };

blob.file = {
    open: function (blob) {
        if (window.navigator.msSaveOrOpenBlob) {
            window.navigator.msSaveOrOpenBlob(blob);
        } else {
            let link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.target = "_blank";
            link.click();
        }
    },
    create: function (data, type) {
        var binary_string = window.atob(data);
        var len = binary_string.length;
        var bytes = new Uint8Array(len);

        for (var i = 0; i < len; i++) {
            bytes[i] = binary_string.charCodeAt(i);
        }

        let bytess = new Uint8Array(bytes);
        let blob = new Blob([bytess], { type: type });

        return blob;
    },
    base64ToByteArray: function (base64String) {
        try {
            var sliceSize = 10000000;
            var byteCharacters = atob(base64String);
            var bytesLength = byteCharacters.length;
            var sliceCount = Math.ceil(bytesLength / sliceSize);
            var bytesArrays = new Array(sliceCount);

            for (var sliceIndex = 0; sliceIndex < sliceCount; ++sliceIndex) {
                var begin = sliceIndex * sliceSize;
                var end = Math.min(begin * sliceSize, bytesLength);

                var bytes = new Array(end - begin);

                for (var offset = begin, i = 0; offset < end; ++i, ++offset) {
                    bytes[i] = byteCharacters[offset].charCodeAt(0);
                }

                bytesArrays[sliceIndex] = new Uint8Array(bytes);
            }
            return bytesArrays;

        } catch (e) {
            return undefined;
        }
    }
}