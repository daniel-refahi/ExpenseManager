
$(document).ready(function () {

    'use strict';
    

    //var $vars = $('#Index\\.js').data();
    //alert($vars.test);

    $("#Category").change(function (key, value) {

        var params = {category:''};
        $("#Category option:selected").each(function () {
            params.category = $(this).text();
        });        
        if(params.category != '')
            RemoteCalls.Report_GetCategoryReport(GetReport_Callback, params);
    });

});

function GetReport_Callback(data)
{
    var chartData = [{
                        name: 'Expense',
                        data: [
                            ((data.message.ExpenseMap.Jan == undefined) ? 0 : data.message.ExpenseMap.Jan),
                            ((data.message.ExpenseMap.Feb == undefined) ? 0 : data.message.ExpenseMap.Feb),
                            ((data.message.ExpenseMap.Mar == undefined) ? 0 : data.message.ExpenseMap.Mar),
                            ((data.message.ExpenseMap.Apr == undefined) ? 0 : data.message.ExpenseMap.Apr),
                            ((data.message.ExpenseMap.Aug == undefined) ? 0 : data.message.ExpenseMap.Aug),
                            ((data.message.ExpenseMap.Sep == undefined) ? 0 : data.message.ExpenseMap.Sep),
                            ((data.message.ExpenseMap.Oct == undefined) ? 0 : data.message.ExpenseMap.Oct),
                            ((data.message.ExpenseMap.Nov == undefined) ? 0 : data.message.ExpenseMap.Nov),
                            ((data.message.ExpenseMap.Dec == undefined) ? 0 : data.message.ExpenseMap.Dec)
                        ]
                    },
                    {
                        name: 'Plan',
                        data: [
                            ((data.message.PlanMap.Jan == undefined) ? 0 : data.message.PlanMap.Jan),
                            ((data.message.PlanMap.Feb == undefined) ? 0 : data.message.PlanMap.Feb),
                            ((data.message.PlanMap.Mar == undefined) ? 0 : data.message.PlanMap.Mar),
                            ((data.message.PlanMap.Apr == undefined) ? 0 : data.message.PlanMap.Apr),
                            ((data.message.PlanMap.Aug == undefined) ? 0 : data.message.PlanMap.Aug),
                            ((data.message.PlanMap.Sep == undefined) ? 0 : data.message.PlanMap.Sep),
                            ((data.message.PlanMap.Oct == undefined) ? 0 : data.message.PlanMap.Oct),
                            ((data.message.PlanMap.Nov == undefined) ? 0 : data.message.PlanMap.Nov),
                            ((data.message.PlanMap.Dec == undefined) ? 0 : data.message.PlanMap.Dec)
                        ]
                    }];

    var params = {
        parentElement: $('#container'),
        title: data.message.CategoryName,
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        data: chartData
    };
    DrawDoubleBarChart(params);
}

