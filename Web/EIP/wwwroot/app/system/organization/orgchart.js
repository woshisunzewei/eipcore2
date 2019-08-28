$(function () {
    UtilAjaxPostWait("Organization/GetOrganizationChart", {}, function (data) {
        debugger;
        $('#chart-container').orgchart({
            'data': data,
            'nodeContent': 'title'
            
        });
    });

});