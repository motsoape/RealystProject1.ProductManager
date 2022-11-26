$(document).ready(function () {
    var $table = $('#stats-table');

    $.ajax(BaseURL + '/api/Stats',
        {
            type: 'GET',
            dataType: "json",
            success: function (results, status, xhr) {
                $('#stats-view').html("Total Products: " + results.totalProducts + " Total Comments: " + results.totalComments);

                var columns = [
                    {
                        field: 'field1',
                        title: 'Product Name',
                        sortable: true,
                        valign: 'middle',
                        formatter: function (val) {
                            return '<div class="item">' + val + '</div>'
                        },
                        events: {
                            'click .item': function () {
                                console.log('click')
                            }
                        }
                    },
                    {
                        field: 'field2',
                        title: 'Number of Comments',
                        sortable: true,
                        valign: 'middle',
                        formatter: function (val) {
                            return '<div class="item">' + val + '</div>'
                        },
                        events: {
                            'click .item': function () {
                                console.log('click')
                            }
                        }
                    }
                ];
                var data = []

                if (results) {

                    for (var i = 0; i < results.productsStats.length; i++) {
                        row = {};
                        row['field1'] = results.productsStats[i].name;
                        row['field2'] = results.productsStats[i].totalComments;
                        data.push(row)
                    }
                }

                buildTable($table, columns, data)
            },
            error: function (jqXhr, textStatus, errorMessage) {
               
            }
        });

    function buildTable($el, columns, data) {
        $el.bootstrapTable('destroy').bootstrapTable({
            height:  400,
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
});