var Charts = function () {

    DrawDoubleBarChart = function (params) {
        $(params.parentElement).highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: params.title
            },
            xAxis: {
                categories: params.categories,
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: params.yTitle
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: params.data
        });
    };

    DrawPieChart = function (params) {

        $(params.parentElement).highcharts({
            chart: {
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 70,
                    beta: 0
                }
            },
            title: {
                text: params.title
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    depth: 15,
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    }
                }
            },
            series: params.data
        });
    };

    return {
        DrawPieChart: DrawPieChart,
        DrawDoubleBarChart: DrawDoubleBarChart
    };

}();