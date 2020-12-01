var dataTable;





$(document).ready(function () {
	$.fn.dataTable.ext.search.push(
		function (settings, data, dataIndex) {
			var min = $('#datemin').datepicker('getDate');
			var max = $('#datemax').datepicker('getDate');
			var startDate = new Date(data[2]);
			if (min == null && max == null) return true;
			if (min == null && startDate <= max) return true;
			if (max == null && startDate >= min) return true;
			if (startDate <= max && startDate >= min) return true;
			return false;
		}
	);

	$.fn.dataTable.ext.search.push(
	function (settings, data, dataIndex) {
		var min = parseFloat($('#min').val(), 10);
		var max = parseFloat($('#max').val(), 10);
		var donation = parseFloat(data[1]) || 0;

		if ((isNaN(min) && isNaN(max)) ||
			(isNaN(min) && donation <= max) ||
			(min <= donation && isNaN(max)) ||
			(min <= donation && donation <= max)) {
			return true;
		}
		return false;
	}
	);

	$('#datemin').datepicker({ onSelect: function () { dataTable.draw(); }, changeMonth: true, changeYear: true });
	$('#datemax').datepicker({ onSelect: function () { dataTable.draw(); }, changeMonth: true, changeYear: true });

	loadList();

	$('#min, #max').keyup(function () {
		dataTable.draw();
	});

	$('#datemin, #datemax').change(function () {
		dataTable.draw();
	});
});

function loadList() {

	dataTable = $('#DT_load').DataTable({
		"ajax": {
			"url": "/api/donations",
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
			{ "data": "email", "width": "15%" },
			{ "data": "donationCause.title", "width": "15%" },
			{ "data": "comments", "width": "15%" },
			{ "data": "followUp", "width": "15%" },
			{ "data": "transactionId", "width": "25%" }
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
		"scrollX": true
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