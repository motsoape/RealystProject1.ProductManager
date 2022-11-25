$(document).ready(function () {

    $.ajax(BaseURL + '/api/Stats',
        {
            type: 'GET',
            dataType: "json",
            success: function (data, status, xhr) {
                $('#stats-table tbody').html("");
                $('#stats-view').html("Total Products: " + data.totalProducts + " Total Comments: " + data.totalComments);
                if (data) {

                    for (var i = 0; i < data.productsStats.length; i++) {
                        $('#stats-table').append(
                            '<tr>'+
                            '<td>' + data.productsStats[i].name + '</td > ' +
                            '<td>' + data.productsStats[i].totalComments + '</td>' +
                            '</tr>'
                        );
                    }
                }
            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('#results').append('Error' + errorMessage);
            }
        });
});