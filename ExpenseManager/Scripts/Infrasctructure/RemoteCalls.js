var RemoteCalls = function () {
    
    Report_GetCategoryReport = function (callBack, params) {

        $.ajax({
            type: "GET",
            url: AppURLs.RemoteActions.Report_GetCategoryReport + '?category='+params.category ,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                callBack(data);
            },
            error: function (data) {
                callBack(data);
            },
        });
    };

    return {
        Report_GetCategoryReport: Report_GetCategoryReport
    };

}();
