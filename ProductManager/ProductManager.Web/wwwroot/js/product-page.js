$(document).ready(function () {
    $('.date-pickers').datepicker({ format: 'yyyy-mm-dd' }); 
    var $tableProducts = $('#products-table');
    var $tableComments = $('#comments-table');

    $(".product-data-form").submit(function (e) {
        e.preventDefault();

        var form = $(this);
        var actionUrl = form.attr('action');
        var method = form.attr('method');
        var modalId = form.attr('data-modal');

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            type: method,
            dataType: "json",
            url: actionUrl,
            data: JSON.stringify(convertFormToJSON(form)),
            success: function (data) {
                $('#' + modalId).modal('hide');
                form.find("input, textarea").val("");
                loadProducts();
            }
        });
    });

    $(".comment-data-form").submit(function (e) {
        e.preventDefault();

        var form = $(this);
        var actionUrl = form.attr('action');
        var method = form.attr('method');
        var modalId = form.attr('data-modal');

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            type: method,
            dataType: "json",
            url: actionUrl,
            data: JSON.stringify(convertFormToJSON(form)),
            success: function (data) {
                $('#' + modalId).modal('hide');
                $(".comment-form-ctrl").val("");

                if (modalId == 'add-comment-modal') {
                    loadComments($('#add-comment-productId-ctrl').val());
                } else {
                    loadComments($('#edit-comment-productId-ctrl').val());
                }
            }
        });
    });

    $(document).on('click', ".edit-product", function () {
        var productId = $(this).data("id");

        $.ajax(BaseURL + '/api/Product/' + productId,
            {
                type: 'GET',
                dataType: "json",
                success: function (data, status, xhr) {
                    $('#edit-product-modal').modal('show'); 
                    $('#edit-product-form').attr('action', BaseURL + '/api/Product/' + productId);
                    $('#edit-product-name-ctrl').val(data.name);
                    $('#edit-product-price-ctrl').val(data.price);
                    $('#edit-product-release-date-ctrl').val(data.releaseDate);
                },
                error: function (jqXhr, textStatus, errorMessage) { }
            });
    });

    $(document).on('click', ".edit-comment", function () {
        var commentId = $(this).data("id");

        $.ajax(BaseURL + '/api/Comment/' + commentId,
            {
                type: 'GET',
                dataType: "json",
                success: function (data, status, xhr) {
                    $('#edit-comment-modal').modal('show');
                    $('#edit-comment-form').attr('action', BaseURL + '/api/Comment/' + commentId);
                    $('#edit-comment-productId-ctrl').val(data.productID);
                    $('#edit-comment-email-ctrl').val(data.email);
                    $('#edit-comment-content-ctrl').val(data.commentContent);
                },
                error: function (jqXhr, textStatus, errorMessage) { }
            });
    });

    $(document).on('click', ".delete-product", function () {
        var productId = $(this).data("id");

        $.ajax(BaseURL + '/api/Product/' + productId,
            {
                type: 'DELETE',
                dataType: "json",
                success: function (data, status, xhr) {
                    loadProducts();
                },
                error: function (jqXhr, textStatus, errorMessage) { }
            });
    });

    $(document).on('click', ".delete-comment", function () {
        var commentId = $(this).data("id");
        var productId = $(this).data("pid");
        console.log(productId)

        $.ajax(BaseURL + '/api/Comment/' + commentId,
            {
                type: 'DELETE',
                dataType: "json",
                success: function (data, status, xhr) {
                    loadComments(productId);
                },
                error: function (jqXhr, textStatus, errorMessage) { }
            });
    });

    $(document).on('click', ".view-comments", function () {
        var productId = $(this).data("id");
        loadComments(productId);
    });

    $(document).on('click', "#add-comment-btn", function () {
        $('#add-comment-productId-ctrl').val($('#add-comment-btn').attr('data-productId'));
    });

    function convertFormToJSON(form) {
        return form
            .serializeArray()
            .reduce(function (json, { name, value }) {
                if (value === 'true') {
                    json[name] = true;
                } else if (value === 'false') {
                    json[name] = false;
                } else if (!isNaN(value)) {
                    json[name] = parseFloat(value);
                } else {
                    json[name] = value;
                }
                return json;
            }, {});
    }

    function loadProducts() {
        $.ajax(BaseURL + '/api/Product',
            {
                type: 'GET',
                dataType: "json",
                success: function (results, status, xhr) {

                    columns = [
                        {
                            field: 'field1',
                            title: 'Name',
                            sortable: true,
                            valign: 'middle',
                            formatter: function (val) {
                                return '<div class="item">' + val + '</div>'
                            }
                        },
                        {
                            field: 'field2',
                            title: 'Price',
                            sortable: true,
                            valign: 'middle',
                            formatter: function (val) {
                                return '<div class="item">' + val + '</div>'
                            }
                        },
                        {
                            field: 'field3',
                            title: 'Releaase Date',
                            sortable: true,
                            valign: 'middle',
                            formatter: function (val) {
                                return '<div class="item">' + val + '</div>'
                            }
                        },
                        {
                            field: 'field4',
                            title: 'Actions',
                            sortable: true,
                            valign: 'middle',
                            formatter: function (val) {
                                return '<div class="item">' + val + '</div>'
                            }
                        }
                    ];
                    data = []

                    if (results) {
                        for (var i = 0; i < results.length; i++) {
                            row = {};
                            row['field1'] = results[i].name;
                            row['field2'] = results[i].price;
                            row['field3'] = results[i].releaseDate;
                            row['field4'] = '<div  class="text-center"><button type="button" style="margin-right: 10px;" class="btn btn-primary view-comments" data-id="' + results[i].productID + '">Comments</button>' +
                                '<button type="button" style="margin-right: 10px;" class="btn btn-primary edit-product" data-id="' + results[i].productID + '">Edit</button>' +
                                '<button type="button" style="margin-right: 10px;" class="btn btn-danger delete-product" data-id="' + results[i].productID + '">Delete</button></div>';
                            data.push(row)
                        }
                    }

                    buildTable($tableProducts, columns, data)
                },
                error: function (jqXhr, textStatus, errorMessage) {
                }
            });
    }

    function loadComments(productId) {
        $.ajax(BaseURL + '/api/Product/' + productId,
            {
                type: 'GET',
                dataType: "json",
                success: function (results, status, xhr) {
                    $('#view-comments-modal').modal('show');
                    $('#add-comment-btn').attr('data-productId', productId);

                    var toBuildFun = function () {
                        columns = [
                            {
                                field: 'field1',
                                title: 'Email',
                                sortable: true,
                                valign: 'middle',
                                formatter: function (val) {
                                    return '<div class="item">' + val + '</div>'
                                }
                            },
                            {
                                field: 'field2',
                                title: 'Date Of Comment',
                                sortable: true,
                                valign: 'middle',
                                formatter: function (val) {
                                    return '<div class="item">' + val + '</div>'
                                }
                            },
                            {
                                field: 'field3',
                                title: 'Comment',
                                sortable: true,
                                valign: 'middle',
                                formatter: function (val) {
                                    return '<div class="item">' + val + '</div>'
                                }
                            },
                            {
                                field: 'field4',
                                title: 'Actions',
                                sortable: true,
                                valign: 'middle',
                                formatter: function (val) {
                                    return '<div class="item">' + val + '</div>'
                                }
                            }
                        ];
                        data = []

                        if (data && results.comments) {

                            for (var i = 0; i < results.comments.length; i++) {
                                row = {};
                                row['field1'] = results.comments[i].email;
                                row['field2'] = results.comments[i].dateOfComment;
                                row['field3'] = results.comments[i].commentContent;
                                row['field4'] = '<div><button type="button" style="margin-right: 10px;" class="btn btn-primary edit-comment" data-id="' + results.comments[i].commentID + '">Edit</button>' +
                                    '<button type="button" style="margin-right: 10px;" class="btn btn-danger delete-comment" data-id="' + results.comments[i].commentID + '" data-pid="' + results.comments[i].productID + '">Delete</button></div>';
                                data.push(row)
                            }
                        }

                        buildTable($tableComments, columns, data);
                    };
                    
                    if ($("#view-comments-modal").is(':visible')) {
                        toBuildFun();
                    } else {
                        $('#view-comments-modal').on('shown.bs.modal', toBuildFun);
                    }
                },
                error: function (jqXhr, textStatus, errorMessage) {
                }
            });
    }

    function buildTable($el, columns, data) {
        $el.bootstrapTable('destroy').bootstrapTable({
            height: 400,
            columns: columns,
            data: data,
            search: true,
            showColumns: true,
            showToggle: false,
            clickToSelect: true,
            fixedColumns: true,
            fixedNumber: 2,
            fixedRightNumber: 1
        });
    }

    loadProducts();
});