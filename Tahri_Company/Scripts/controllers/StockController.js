var Stockconfig = {
    pageSize: 20,
    pageIndex: 1,
}
var StockController = {
    init: function () {
        StockController.loadData();
        StockController.registerEvent();

    },
    registerEvent: function () {

        $('#btn-edit').off('click').on('click', function () {
            debugger;
            $('#modalAddUpdate').modal('show');

            var id = $(this).data('id');
            StockController.loadDetail(id);


        });

        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            StockController.resetForm();
            $.ajax({
                url: '/Stock/LoadProd',
                type: 'GET',
                data: {

                    page: 1,
                    pageSize: 10
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        var data = response.droplist;
                        var x = "<option> -- Choisir un produit --</option>";
                        var template = $('#selprod').html();
                        $.each(data, function (i, item) {


                            x += "<option>" + item.Libelle + "</option>";

                        });
                        $('#selprod').html(x);


                    }
                }
            });


        });

        $('#btnSave').off('click').on('click', function () {


            StockController.saveData();


        });






        $('.btn-delete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.confirm("Are you sure to delete this employee?", function (result) {
                StockController.deleteStock(id);
            });
        });

    },
    deleteStock: function (id) {
        $.ajax({
            url: '/Stock/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    new PNotify({
                        title: 'Supprission',
                        text: 'Supprission Annulée',
                        type: 'warning',
                        hide: false,
                        styling: 'bootstrap3'
                    });
                    StockController.loadData(true);

                }
                else {
                    // toastr.error('ERREUR');
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },

    resetForm: function () {
        $('#hidProdID').val('0');
        $('#txtQte').val('0');
       
        
        

    },
    loadDetail: function (id) {
        

        $.ajax({
            url: '/Stock/LoadProd',
            type: 'GET',
            data: {

                page: 1,
                pageSize: 10
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.droplist;
                    var x = "<option> -- Choisir un produit --</option>";
                    var template = $('#selprod').html();
                    $.each(data, function (i, item) {


                        x += "<option>" + item.Libelle + "</option>";

                    });
                    $('#selprod').html(x);


                }
            }
        });

        $.ajax({
            url: '/Stock/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#hidProdID').val(data.Id);
                    //$('#selprod').val(data.designation);
                    $('#txtQte').val(data.qte);


                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });


    },


    
    saveData: function () {
        var qt = $('#txtQte').val();
        var prod = $('#selprod').val();
        var url = window.location.pathname;
        var depid = parseInt(url.substring(url.lastIndexOf('/') + 1));

        var id = parseInt($('#hidProdID').val());
        var stock = {

            designation: prod,
            qte: qt,

            Id: id,
            DepotId: depid
        }
        $.ajax({
            url: '/Stock/SaveData',
            data: {
                strStock: JSON.stringify(stock)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //toastr.success('Stock ajoutée', 'Ajout Stock');

                    $('#modalAddUpdate').modal('hide');
                    StockController.loadData(true);


                }
                else {
                    bootbox.alert(response.message);

                }


            },
            error: function (err) {
                console.log(err);
            }
        });
    },


    loadData: function (changePageSize) {

        var url = window.location.pathname;
        var id = url.substring(url.lastIndexOf('/') + 1);
        $.ajax({
            url: '/Stock/LoadData',
            type: 'GET',
            data: {
                id: id,
                page: Stockconfig.pageIndex,
                pageSize: Stockconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    debugger;
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {

                        html += Mustache.render(template, {
                            Id: item.Id,
                            qte: item.qte,
                            designation: item.designation


                        });

                    });
                    $('#tblData').html(html);
                    StockController.paging(response.total, function () {
                        StockController.loadData();
                    }, changePageSize);

                }
            }
        })
    },

}
StockController.init();

function fncEdit(thisme) {
    //debugger;
    $('#modalAddUpdate').modal('show');
    var id = $(thisme).data("id");

    StockController.loadDetail(id);
}


function fncDelelte(thisme) {
    var id = $(thisme).data('id');



    bootbox.dialog({
        message: "La suppression de ce Stock est irréversible.",
        title: "Êtes-vous sûr de vouloir supprimer cette Stock?",
        buttons: {
            success: {
                label: "Annuler",
                className: "btn-default",
                callback: function () {
                    bootbox.hideAll();
                  

                    new PNotify({
                        title: 'Supprission',
                        text: 'Supprission Annulée',
                        type: 'warning',
                        hide: false,
                        styling: 'bootstrap3'
                    });
                }
            },

            main: {
                label: "Supprimer",
                className: "btn-primary",
                callback: function () {
                    StockController.deleteStock(id);
                }
            }
        }
    });
}