var options = {
    url: {
        get: Language.common.apiurl + "District/GetDistrictByParentId",
        edit: "/System/District/Edit",
        delete: "/District/DeleteDistrict"
    },
    dialog: {
        width: 610,
        title: "行政区划"
    },
    table: {
        name: "district-list",
        primaryKey: "DistrictId"
    }
};
$(function () {
    $tree = $('#tree');
    $tree.height(Language.mainHeight);
    initTree();
    initGird();
});

var pId,
    $grid,
    $tree,
    tableObj,
    height;

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
            height: Language.mainHeight + 10,

            cols: [[
                { type: "checkbox", fixed: 'left', width: 30 },
                { title: '序号', type: "numbers", fixed: 'left' },
                { field: 'DistrictId', title: '代码', width: "80", sort: true, fixed: 'left' },
                { field: 'Name', title: '名称', width: "80", sort: true, fixed: 'left' },
                { field: 'ShortName', title: '简称', width: "80", sort: true },
                { field: 'CityCode', title: '城市代码', width: "80", sort: true },
                { field: 'ZipCode', title: '邮编', sort: true },
                { field: 'Lng', title: '经度', sort: true, width: "100" },
                { field: 'Lat', title: '纬度', sort: true, width: "100" },
                { field: 'PinYin', title: '拼音', sort: true, width: "80" },
                { field: 'IsFreeze', title: '冻结', width: "80", align: "center", templet: '#templetFreeze', sort: true },
                { field: 'OrderNo', title: '排序号', sort: true, width: "80" }
            ]],
            where: { id: "0" },
            even: true,
            page: false
        });
    });
}

//初始化组织机构
function initTree() {
    $tree.jstree({
        core: {
            data: function (obj, callback) {
                var id = obj.id;
                UtilAjaxPostWait("/District/GetDistrictTreeByParentId", { id: id == "#" ? "0" : id }, function (data) {
                    callback.call(this, data);
                });
            }
        },
        plugins: ["types"],
        types: {
            default: {
                icon: 'fa fa-file-text-o'
            }
        }
    }).bind('activate_node.jstree', function (obj, e) {
        pId = e.node.id;
        getGridData();
    });
}

//获取表格数据
function getGridData() {
    $grid.reload({
        where: { 
            id: pId
        }
    });
}

//操作:新增
function add() {
    DialogOpen(options.url.edit, "新增" + options.dialog.title, options.dialog.width);
}

//操作:编辑
function edit() {
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen(options.url.edit + "?id=" + info[options.table.primaryKey], "编辑" + options.dialog.title + "-" + info.Name, options.dialog.width);
    });
}

//删除匹配项
function del() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        DialogConfirm("此操作为级联删除,将删除对应的子项!删除后不可恢复,确认要删除?", function () {
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

//刷新
function reload(reset) {
    if (reset) pId = null;
    $tree.jstree("destroy");
    initTree();
    getGridData();
}
