var options = {
    url: {
        get: Language.common.apiurl + "/Job/GetAllJobs",
        edit: "/System/Job/Edit",
        delete: "/Job/DeleteJob"
    },
    dialog: {
        width: 610,
        title: "作业"
    },
    table: {
        name: "list"
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
                { title: '序号', type: "numbers" },
                //{ title: "触发器名称", field: "TriggerName" },
                //{ title: "触发器组名", field: "TriggerGroupName" },
                //{ title: "作业组名称", field: "JobGroup", width: 100},
                { title: "作业名称", field: "JobName", width: 270 },
                //{ title: "触发器类型", field: "TriggerType", width: 100, fixed: true },
                { title: "方法", field: "ClassName", width: 230 },
                { title: "状态", templet: '#templetTriggerState', field: "TriggerState", width: 60, align: "center" },
                { title: "下一次运行时间", field: "NextFireTime", width: 120 },
                { title: "上一次运行时间", field: "PreviousFireTime", width: 120 },
                { title: "作业描述", field: "JobDescription", width: 200 }
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
    $grid.reload({});
}

//新增
function add() {
    DialogOpen(options.url.edit, "新增" + options.dialog.title, options.dialog.width);
}

//编辑
function edit() {
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen(options.url.edit + "?JobName=" + info.JobName,
            "编辑" + options.dialog.title + "-" + info.JobName, options.dialog.width);
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

//启动
function resumeJob() {
    DialogConfirm("确定启动所有作业?", function () {
        UtilAjaxPostWait(
            "/Job/ResumeAll",{},perateStatus
        );
    });
}

//暂停
function pauseJob() {
    DialogConfirm("确定暂停所有作业?", function () {
        UtilAjaxPostWait(
            "/Job/PauseAll",{},perateStatus
        );
    });
}

//请求完成
function perateStatus(data) {
    DialogAjaxResult(data);
    if (data.ResultSign === 0) {
        getGridData();
    }
}