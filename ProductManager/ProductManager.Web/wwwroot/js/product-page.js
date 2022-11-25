$(document).ready(function () {
    $('.date-pickers').datepicker({ format: 'yyyy-mm-dd' }); 

    $(".data-form").submit(function (e) {
        e.preventDefault();

        var form = $(this);
        var actionUrl = form.attr('action');
        var method = form.attr('method');

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
                $('.modal').modal('hide');
                loadProducts();
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
                success: function (data, status, xhr) {
                    $('#products-table tbody').html("");

                    if (data) {
                        for (var i = 0; i < data.length; i++) {
                            $('#products-table').append(
                                '<tr><td>' + data[i].name + '</td>' +
                                '<td>' + data[i].price + '</td>' +
                                '<td>' + data[i].releaseDate + '</td>' +
                                '<td class="text-center">' +
                                '<button type="button" class="btn btn-primary view-comments" data-id="' + data[i].productID +'">Comments</button>' +
                                '<button type="button" class="btn btn-primary edit-product" data-id="' + data[i].productID +'">Edit</button>' +
                                '<button type="button" class="btn btn-danger delete-product" data-id="' + data[i].productID+'">Delete</button>' +
                                '</td></tr>'
                            );
                        }
                    }
                },
                error: function (jqXhr, textStatus, errorMessage) { }
            });
    }

    function loadComments(productId) {
        $.ajax(BaseURL + '/api/Product/' + productId,
            {
                type: 'GET',
                dataType: "json",
                success: function (data, status, xhr) {
                    $('#view-comments-modal').modal('show');
                    $('#add-comment-btn').attr('data-productId', productId);
                    $('#comments-table tbody').html("");

                    if (data && data.comments) {

                        for (var i = 0; i < data.comments.length; i++) {
                            $('#comments-table').append(
                                '<tr><td>' + data.comments[i].email + '</td>' +
                                '<td>' + data.comments[i].dateOfComment + '</td>' +
                                '<td>' + data.comments[i].commentContent + '</td>' +
                                '<td class="text-center">' +
                                '<button type="button" class="btn btn-primary edit-comment" data-id="' + data.comments[i].commentID + '">Edit</button>' +
                                '<button type="button" class="btn btn-danger delete-comment" data-id="' + data.comments[i].commentID + '" data-id="' + data.comments[i].productID +'">Delete</button>' +
                                '</td></tr>'
                            );
                        }
                    }
                },
                error: function (jqXhr, textStatus, errorMessage) { }
            });
    }

    loadProducts();
});