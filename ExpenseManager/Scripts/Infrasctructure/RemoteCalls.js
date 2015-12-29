var RemoteCalls = function () {
    
    Report_GetCategories = function (callBack) {        

        $.ajax({
            type: "GET",
            url: AppURLs.RemoteActions.Report_GetCategories,
            data: '',
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
        Report_GetCategories: Report_GetCategories
    };

}();
