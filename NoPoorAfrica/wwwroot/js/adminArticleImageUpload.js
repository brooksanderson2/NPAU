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
        "url": "/api/articleImages/Left/?ArticleId=" + $('#articleId').prop("value") + "&Path=" + imgPath,
        "type": "PATCH",
        "datatype": "json"
    }).done(function (result) {
        if (result.success == true) {
            var origin = result.target1.replace(/\\/gi, '').replace('.', '');
            var swap = result.target2.replace(/\\/gi, '').replace('.', '');
            $('#card-' + swap).before($('#card-' + origin));
        }
    });
}

function moveRight(imgPath) {
    $.ajax({
        "url": "/api/articleImages/Right/?ArticleId=" + $('#articleId').prop("value") + "&Path=" + imgPath,
        "type": "PATCH",
        "datatype": "json"
    }).done(function (result) {
        if (result.success == true) {
            var origin = result.target1.replace(/\\/gi, '').replace('.', '');
            var swap = result.target2.replace(/\\/gi, '').replace('.', '');
            $('#card-' + swap).before($('#card-' + origin));
        }
    });
}

function deleteImage(imgPath){
    $.ajax({
        "url": "/api/articleImages/Delete/?ArticleId=" + $('#articleId').prop("value") + "&Path=" + imgPath,
        "type": "DELETE",
        "datatype": "json"
    }).done(function (result) {

    });
}
