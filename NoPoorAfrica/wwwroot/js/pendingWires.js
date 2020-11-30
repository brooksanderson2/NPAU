var dataTable;

$(document).ready(function () {
	loadList();
});

function loadList() {
	dataTable = $('#DT_load').DataTable({
		"ajax": {
			"url": "/api/pendingWires",
			"type": "GET",
			"datatype": "json"
		},
		"columns": [
			{ "data": "donorName", width: "15%" },
			{
				"data": "donationTotal", render: $.fn.dataTable.render.number(',', '.', 2, '$'), width: "15%"
			},
			{
				"data": "donationDate", "render": function (data, type) {
					return type === 'sort' ? data : moment(data).format('MM/DD/YYYY');
				}, "width": "15%"
			},
			{ "data": "email", "width": "10%" },
			{ "data": "donationCause.title", "width": "15%" },
			{ "data": "id", "width": "15%" },
			{
				"data": "id",
				"render": function (data) {
					return `
						<div class="text-center">
							<a onClick=Approve('/api/PendingWires/'+${data})
							class="btn btn-success text-white style="cursor:pointer; width:100px;">
							<i class="far fa-edit"></i>
							Approve
							</a>
							<a onClick=Delete('/api/PendingWires/'+${data})
							class="btn btn-danger text-white style="cursor:pointer; width:100px;">
							<i class="far fa-trash-alt"></i>
							Delete
							</a>
						</div>`;
				}, width: "15%"
			}
		],
		"language": {
			"emptyTable": "no data found."
		},
		"width": "100%",
		"order": [[2, "asc"]],
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

function Approve(url) {
	swal({
		title: "Are you sure you want to approve?",
		text: "You will not be able to restore the data!",
		icon: "warning",
		buttons: true,
		dangerMode: true
	}).then((willDelete) => {
		if (willDelete) {
			$.ajax({
				type: 'POST',
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