var Charts = function () {

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
        DrawPieChart: DrawPieChart
    };

}();