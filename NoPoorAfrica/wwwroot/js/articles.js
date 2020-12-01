$(document).ready(function () {
    loadList();
});



function Publish(url, id) {
    $.ajax({
        "url": url,
        "type": "PATCH",
        "datatype": "json"
    }).done(function (result) {
        if (result.success == true) {
            $('#pub-' + id).prop("text", result.message);
        }
    });
}

function GetPublishStatus() {
    $.ajax({
        "url": "/api/articles/PublishStatus/",
        "type": "GET",
        "datatype": "json"
    }).done(function (result) {
        for (var i = 0; i < result.item.length; i++) {
            if (result.status[i] == true)
                $('#pub-' + result.item[i]).prop("text", "Hide");
        }
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete this article?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

function SetCategory(id) {
    $.ajax({
        "url": "/api/articles/GetArticlesInCategory/"+id,
        "type": "GET",
        "datatype": "json"
    }).done(function (result) {

    });
}

