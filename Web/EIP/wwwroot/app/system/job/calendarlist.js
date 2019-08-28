var options = {
    url: {
        get: Language.common.apiurl + "/Job/GetCalendar",
        edit: "/System/Job/CalendarEdit",
        delete: "/Job/DeleteCalendar"
    },
    dialog: {
        width: 610,
        title: "排除日历"
    },
    table: {
        name: "list",
        primaryKey: "CalendarName"
    }
};

var $grid,
    tableObj;

$(function () {
    initGird();
});

//初始化表格
function initGird() {
    layui.use('table', function () {
        tableObj = layui.table;
        //方法级渲染
        $grid = tableObj.render({
            elem: '#' + options.table.name,
            size: "sm",
            method: 'post',
            url: options.url.get,
            cols: [[
                { type: "checkbox", width: 30 },
                { title: '序号', type: "numbers"},
                { title: "名称", field: "CalendarName", width: 300},
                { title: "描述", field: "Description" }
            ]],
            limit: 50,
            limits: [50, 100, 200, 500],
            even: true,
            height: Language.mainHeight + 10,
            page: false
        });
    });
}
//获取表格数据
function getGridData() {
    $grid.reload();
}



//新增
function add() {
    DialogOpen(options.url.edit, "新增" + options.dialog.title, options.dialog.width);
}

//编辑
function edit() {
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen(options.url.edit + "?calendarName=" + info[options.table.primaryKey], "编辑" + options.dialog.title + "-" + info[options.table.primaryKey], options.dialog.width);
    });
}

//删除
function del() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        DialogConfirm(Language.common.deletemsg, function () {
            UtilAjaxPostWait(
                options.url.delete,
                { id: GridSelectIdsLayTable(tableObj, options.table.name, options.table.primaryKey) },
                perateStatus
            );
        });
    });
}

//请求完成
function perateStatus(data) {
    DialogAjaxResult(data);
    if (data.ResultSign === 0) {
        getGridData();
    }
}