$(document).ready(function () {

    //getPositions();

    //prepareButtonUpload();
});

async function getPositions() {
    var dict = {};
    $.ajax({
        "url": "/api/articleImages/"+$('#articleId').prop("value"),
        "type": "GET",
        "datatype": "json"
    }).done(function (result) {
        for (k in result.data) {
            dict[k] = result.data[k];
        }
    });


}

function moveLeft(imgPath) {
    $.ajax({
        "url": "/api/articleImages/Left/?ArticleId=" + $('#articleId').prop("value") + "&" + imgPath,
        "type": "PATCH",
        "datatype": "json"
    }).done(function (result) {

    });
}

function moveRight(imgPath) {

}

function deleteImage(imgPath){

}

function prepareButtonUpload() {
    $('#btnFUpload').on('click', function () {
        var files = $('#fUpload').prop("files");
        var articleId = $('#articleId').prop("value");
        var images = $('images').prop("value");

        var fdata = new FormData();
        fdata.append("articleId", articleId);
        for (var i = 0; i < images.length; i++) {
            fdata.append("images", images[i]);
        }
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