var Depotconfig = {
    pageSize: 20,
    pageIndex: 1,
}
var DepotController = {
    init: function () {
        DepotController.loadData();
        DepotController.registerEvent();

    },
    registerEvent: function () {

        $('#btn-edit').off('click').on('click', function () {
            debugger;
            $('#modalAddUpdate').modal('show');

            var id = $(this).data('id');
            DepotController.loadDetail(id);


        });

        

        $('#btnUpdate').click(function () {
            
            DepotController.updateData();



        });


        $('#save').click(function () {
            var nom = $('#txtNom').val();
            var adr = $('#txtAdresse').val();
            var tel = $('#txtTel').val();
            $('#lblNom').html(nom);
            $('#lblAdresse').html(adr);
            $('#lblTel').html(tel);
            $('#lblNom').show();
            $('#txtNom').hide();
            $('#lblAdresse').show();
            $('#txtAdresse').hide();
            $('#lblTel').show();
            $('#txtTel').hide();
            $('#div2').show();
            $('#div1').hide();
            DepotController.saveData();



        });

        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            DepotController.resetForm();
            $.ajax({
                url: '/Depot/LoadData',
                type: 'GET',
                data: {

                    page: 1,
                    pageSize: 10
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        // 
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
          

            DepotController.saveProd();
        });
        

    

       
        $('#btnReset').on('click', function () {
            debugger;
            $('#txtNameS').val('');
            $('#ddlStatusS').val('');
            DepotController.loadData(true);
        });





        $('.btn-delete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.confirm("Are you sure to delete this employee?", function (result) {
                DepotController.deleteDepot(id);
            });
        });

    },
    deleteDepot: function (id) {
        $.ajax({
            url: '/Depot/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    

                    new PNotify({
                        title: 'Supprission',
                        text: 'Dêpot supprimé',
                        type: 'success',
                        hide: false,
                        styling: 'bootstrap3'
                    });

                    DepotController.loadData(true);

                }
                
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadDetail: function (id) {
        $.ajax({
            url: '/Depot/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#hidID1').val(data.DepotId);
                    $('#txtNom1').val(data.Nom);
                    $('#txtAdresse1').val(data.Adresse);
                    $('#txtTel1').val(data.Tel);



                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });

        $.ajax({
            url: '/Depot/LoadData',
            type: 'GET',
            data: {

                page: 1,
                pageSize: 10
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    debugger;
                    var data = response.droplist;
                    var html = "<option> -- Choisir un produit --</option>";
                    var template = $('#selprod').html();
                    $.each(data, function (i, item) {


                        html += "<option>" + item.Libelle + "</option>";

                    });
                    $('#selprod').html(html);


                }
            }
        });



    },



    updateData: function () {
        var nom = $('#txtNom1').val();
        var adr = $('#txtAdresse1').val();
        var tel = $('#txtTel1').val();


        var id = parseInt($('#hidID1').val());
        var depot = {
            Nom: nom,
            Adresse: adr,
            Tel: tel,

            DepotId: id

        }
        $.ajax({
            url: '/Depot/UpdateData',
            data: {
                strDepot: JSON.stringify(depot)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    new PNotify({
                        title: 'Modification',
                        text: 'Dêpot Modifié',
                        type: 'success',
                        hide: false,
                        styling: 'bootstrap3'
                    });

                    $('#modalUpdate').modal('hide');
                    DepotController.loadData(true);


                }
                // else {
                //  bootbox.alert(response.message);

                //}


            },
            error: function (err) {
                console.log(err);
            }
        });
    },


    saveData: function () {
        var nom = $('#txtNom').val();
        var adr = $('#txtAdresse').val();
        var tel = $('#txtTel').val();


        var id = parseInt($('#hidID').val());
        var depot = {
            Nom: nom,
            Adresse: adr,
            Tel: tel,

            DepotId: id

        }
        $.ajax({
            url: '/Depot/SaveData',
            data: {
                strDepot: JSON.stringify(depot)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    new PNotify({
                        title: 'Enregistrment',
                        text: 'Dêpot Ajouté',
                        type: 'success',
                        hide: false,
                        styling: 'bootstrap3'
                    });
                    DepotController.loadData(true);


                }
               // else {
                  //  bootbox.alert(response.message);

                //}


            },
            error: function (err) {
                console.log(err);
            }
        });
    },


    saveProd: function () {
       
        var qt = $('#txtQte').val();
        var prod = $('#selprod').val();


        var id = parseInt($('#hidProdID').val());
        var stock = {
            
            designation: prod,
            qte: qt,

            Id: id

        }
        $.ajax({
            url: '/Depot/SaveProd',
            data: {
                strStock: JSON.stringify(stock)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    new PNotify({
                        title: 'Enregistrment',
                        text: 'Produit Ajouté',
                        type: 'success',
                        hide: false,
                        styling: 'bootstrap3'
                    });

                    $('#modalAddUpdate').modal('hide');
                    DepotController.loadData(true);


                }
                // else {
                //  bootbox.alert(response.message);

                //}


            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtNom').val('');
        $('#txtAdresse').val('');
        $('#txtTel').val('');
       

    },

    loadData: function (changePageSize) {
        debugger;

        $.ajax({
            url: '/Depot/LoadData',
            type: 'GET',
            data: {

                page: Depotconfig.pageIndex,
                pageSize: Depotconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            DepotId: item.DepotId,
                            Nom: item.Nom,
                            Adresse: item.Adresse,
                            Tel: item.Tel,


                        });

                    });
                    $('#tblData').html(html);
                    DepotController.paging(response.total, function () {
                        DepotController.Data();
                    }, changePageSize);

                }
            }
        })
    },
  
}
DepotController.init();

function fncEdit(thisme) {
    debugger;
    $('#modalUpdate').modal('show');
    var id = $(thisme).data("id");

    DepotController.loadDetail(id);
}


function fncStock(thisme) {


    var id = $(thisme).data('id');
    
    if (id) {
        window.location = '/Stock/List/' + id;
    }

}

function fncDelelte(thisme) {


    var id = $(thisme).data('id');

    bootbox.dialog({
        message: "La suppression de ce Dêpot est irréversible.",
        title: "Êtes-vous sûr de vouloir supprimer cette emplacement?",
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
                    DepotController.deleteDepot(id);
                }
            }
        }
    });




}


