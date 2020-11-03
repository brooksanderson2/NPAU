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
            { data: "title", width: "40%" },
            { data: "date", width: "30%" },
            {
                data: "id", width: "30%",
                "render": function (data) {
                    return `
                        <div class="text-center">
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