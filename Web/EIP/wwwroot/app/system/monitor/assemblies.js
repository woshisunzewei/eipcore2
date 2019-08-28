var options = {
    url: {
        get: Language.common.apiurl + "/Monitor/GetAllAssemblies"
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
                { title: '序号', type: "numbers"},
                { field: 'Name', title: "名称", width: 350, sort: true },
                { field: 'Version', title: "版本号", width: 80, sort: true },
                { field: 'ClrVersion', title: "Clr运行时", width: 150, sort: true },
                { field: 'Location', title: '位置路径', width: 500 }
            ]],
            limit: 50,
            limits: [50, 100, 200, 500],
            even: true,
            height: Language.mainHeight + 10
        });
    });
}
//获取表格数据
function getGridData() {
    $grid.reload({
        where: {
           
        }
    });
}
