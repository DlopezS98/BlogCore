var datatable;

$(document).ready(function(){
    loadDataTable();
});

function loadDataTable(){
    datatable = $("#tblArticles").DataTable({
        "ajax":{
            "url" :"/Admin/Articles/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns":[
            {"data": "articleID", "width": "5%"},
            {"data": "articleName", "width": "25%"},
            {"data": "category.categoryName", "width": "15%"},
            {"data": "articleCreationDate", "width": "15%"},
            {
                "data": "articleID",
                "render": function(data){
                    return `
                    <div class="text-center">
                        <a href="/Admin/Articles/Edit/${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px;">
                            <i class="fa fa-edit"></i> Editar
                        </a>
                        &nbsp;
                        <a onclick=Delete("/Admin/Articles/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-trash-alt'></i> Borrar 
                        </a>
                    </div>
                    `
                }, "width": "30%"
            }
        ],
        "languaje":{
            "emptyTable": "No hay Registros",
        },
        "width": "100%"
    });
}


function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
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
    });
}
