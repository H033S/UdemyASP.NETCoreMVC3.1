﻿var datatable;

$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url" : "/Admin/Category/GetAll"
        },
        "columns": [
            {"data": "name", "width":"20%"},
            {
                "data": "state",
                "render": function (data) {
                    if (data == true) {
                        return "Active";
                    }
                    else {
                        return "Inactive";
                    }
                }, "width" : "20%"
            }, 
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Category/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                        `;
                }, "width" : "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure do you want to delete the category?",
        text: "This record cannot be retrieved ",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((remove) => {
        if (remove) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}