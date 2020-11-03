﻿var dataTable;

$(document).ready(function () {
	loadList();
});

function loadList() {
	dataTable = $('#DT_load').DataTable({
		"ajax": {
			"url": "/api/donations",
			"type": "GET",
			"datatype": "json"
		},
		"columns": [
			{ "data": "donorName", width: "20%" },
			{
				"data": "donationTotal", render: $.fn.dataTable.render.number(',', '.', 2, '$'), width: "20%"
			},
			{
				"data": "donationDate", "render": function (data, type) {
					return type === 'sort' ? data : moment(data).format('MM/DD/YYYY');
				}, "width": "20%"
			},
			{ "data": "email", "width": "20%" },
			{"data": "donationCauseId", "width": "20%" },
		],
		"language": {
			"emptyTable": "no data found."
		},
		"width": "100%",
		"order": [[2, "asc"]]
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