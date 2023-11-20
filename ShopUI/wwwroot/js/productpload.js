
$(document).ready(function () {
    var productid = document.getElementById('ProductId').value;
    var defaultParams = {
        productId: productid
    };
    var imageListUrl = "/ProductImage/Index?productId=" + productid;

    var manualUploader = new qq.FineUploader({
        element: document.getElementById('fine-uploader-manual-trigger'),
        template: 'qq-template-manual-trigger',
        request: {
            endpoint: '/ProductImage/UploadFiles'
            /*
            customHeaders: {
            "Authorization": "Bearer YXVjaGFkbWluOkNieWxjZTY3"
        }
            */
        },
        callbacks: {
            onSubmit: function (id, fileName) {
                // Extend the default parameters for all files
                // with the parameters for _this_ file.
                // qq.extend is part of a myriad of Fine Uploader
                // utility functions and cross-browser shims
                // found in client/js/util.js in the source.

                var newParams = {
                    newPar: 321
                },
                    finalParams = defaultParams;

                qq.extend(finalParams, newParams);
                this.setParams(finalParams);
            },
            onComplete: function (id) {
                qq(this.getItemByFileId(id)).remove();
               // window.location = imageListUrl;
            }
        },
        thumbnails: {
            placeholders: {
                waitingPath: '/lib/fine_uploader/placeholders/waiting-generic.png',
                notAvailablePath: '/lib/fine_uploader/placeholders/not_available-generic.png'
            }
        },
        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'png'],
            sizeLimit: 15360000

        },     
        autoUpload: false,
        debug: true
    });

    qq(document.getElementById("trigger-upload")).attach("click", function () {
        manualUploader.uploadStoredFiles();
    });
});
