var options = {
    url: {
        get: Language.common.apiurl + "/Workflow/GetButton",
        edit: "/Workflow/Button/Edit",
        delete: "/Workflow/DeleteUser"
    },
    dialog: {
        width: 590,
        title: "流程按钮"
    },
    table: {
        name: "list",
        primaryKey: "ButtonId"
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
                { title: "标题", field: "Name", width: 150},
                { title: "图标", field: "Icon", width: 50, templet: '#templetIcon', align: "center"},
                { title: "脚本", field: "Script", width: 350 },
                { title: "备注", field: "Remark", width: 350 },
                { title: "序号", field: "OrderNo", width: 50, align: "left"}
            ]],
            limit:50,
            limits: [50, 100, 200, 500],
            even: true,
            height: Language.mainHeight + 10
        });
    });
}


//获取表格数据
function getGridData() {
    $grid.reload({
        
    });
}

//新增
function add() {
    DialogOpen(options.url.edit, "新增" + options.dialog.title, options.dialog.width);
}

//编辑
function edit() {
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen(options.url.edit + "?id=" + info[options.table.primaryKey], "编辑" + options.dialog.title + "-" + info.Name, options.dialog.width);
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
        reload(false);
    }
}
