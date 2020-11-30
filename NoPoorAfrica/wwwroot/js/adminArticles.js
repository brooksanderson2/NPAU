var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/articles/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "image", "render": function (data) { return `<img src="${data}" alt="Label" style="max-height: 225px;">`; }, "width": "15%" },
            { data: "title", width: "20%" },
            {
                "data": "publishDate", "render": function (data, type) {
                    return type === 'sort' ? data : moment(data).format('MM/DD/YYYY');
                }, "width": "15%"
            },
            {
                "data": "updateDate", "render": function (data, type) {
                    return type === 'sort' ? data : moment(data).format('MM/DD/YYYY');
                }, "width": "15%"
            },
            {
                data: "id", width: "35%",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a onClick=Publish('/api/articles/'+${data})
                                class="btn btn-primary text-white style="cursor:pointer; width:100px;">
                                <i class="fas fa-eye"></i>
                                Publish
                            </a>
                            <a href="/Admin/Articles/Upsert?id=${data}"
                                class="btn btn-success text-white style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i>
                                Edit
                            </a>
                            <a onClick=Delete('/api/articles/'+${data})
                                class="btn btn-danger text-white style="cursor:pointer; width:100px;">
                                <i class="far fa-trash-alt"></i>
                                Delete
                            </a>
                        </div>`;
                }
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}

function Publish(url) {
    

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

