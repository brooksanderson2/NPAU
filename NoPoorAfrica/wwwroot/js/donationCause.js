var dataTable;

$(document).ready(function () {
	loadList();
});

function loadList() {
	dataTable = $('#DT_load').DataTable({
		"ajax": {
			"url": "/api/donationCause",
			"type": "GET",
			"datatype": "json"
		},
		"columns": [
			{ "data": "title", "width": "10%" },
			{ "data": "fromDate", "render": function (data, type) {
			  return type === 'sort' ? data : moment(data).format('MM/DD/YYYY');
				}, "width": "8%"
			},
			{ "data": "toDate", "render": function (data, type) {
			  return type === 'sort' ? data : moment(data).format('MM/DD/YYYY');
				}, "width": "8%"
			},
			{ "data": "fundingGoal", render: $.fn.dataTable.render.number(',', '.', 2, '$'), "width": "5%" },
			{ "data": "country", "width": "5%" },
			{ "data": "description", "width": "30%" },
			{ "data": "donationCauseCategory.name", "width": "15%" },
			{ "data": "image", "render": function (data) { return `<img src="${data}" alt="Label" style="width:130px;height:175px;">`; }, "width": "19%" },
			{ "data": "id",
				"render": function (data) {
					return `
						<div class="text-center">
							<a href="/Admin/DonationCause/Upsert?id=${data}"
							class="btn btn-success text-white style="cursor:pointer; width:100px;">
							<i class="far fa-edit"></i>
							Edit
							</a>
							<a onClick=Delete('/api/DonationCause/'+${data})
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
		"order": [[2, "asc"]]
	});
}

function Delete(url) {
	swal({
		title: "*WARNING: THIS WILL DELETE ALL DONATION RECORDS ASSOCIATED WITH THIS CAUSE*",
		text: "If you wish to keep donation records for this cause, click 'Cancel' and de-activate the cause in the 'Edit' page instead of deleting it. Are you sure you want to delete?",
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