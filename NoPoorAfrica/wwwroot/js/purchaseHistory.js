﻿var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/PurchaseHistory",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

            { "data": "id", "width": "10%" },
            { "data": "applicationUser.fullName", "width": "25%" },
            { "data": "storeItem.name", "width": "25%" },
            { "data": "count", "width": "15%" },
            {
                "data": "purchaseDate",
                "render": function (data, type) {
                    return type === 'sort' ? data : moment(data).format('MM/DD/YYYY');
                }, width: "25%"
            },
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%",
        "order": [[2, "asc"]],
        "dom": 'Blfrtip',
        "buttons": [
            'csv', 'pdf', 'print'
        ],
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
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