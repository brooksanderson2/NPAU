$(document).ready(function () {
    prepareButtonUpload();
});

function prepareButtonUpload() {
    $('#btnFUpload').on('click', function () {
        var files = $('#fUpload').prop("files");
        var id = $('#articleId').prop("value");
        var fdata = new FormData();
        fdata.append("Id", id);
        for (var i = 0; i < files.length; i++) {
            fdata.append("files", files[i]);
        }
        if (files.length > 0) {
            $.ajax({
                type: "POST",
                url: "?handler=Upload",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: fdata,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert('Image(s) uploaded successfully.')
                }
            });
        }
        else {
            alert('Please select an image.')
        }
    });
}