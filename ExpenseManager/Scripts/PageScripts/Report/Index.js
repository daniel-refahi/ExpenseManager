
$(document).ready(function () {

    'use strict';

    //var $vars = $('#Index\\.js').data();
    //alert($vars.test);

    $("#Category").change(function (key, value) {

        var params = {category:''};
        $("#Category option:selected").each(function () {
            params.category = $(this).text();
        });        
        
        RemoteCalls.Report_GetCategoryReport(GetReport_Callback, params);
    });

});

function GetReport_Callback(data)
{

}