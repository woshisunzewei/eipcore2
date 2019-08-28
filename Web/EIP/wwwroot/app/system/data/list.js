var options = {
    url: {
        get: Language.common.apiurl + "/Data/GetDataByMenuId",
        edit: "/System/Data/Edit",
        delete: "/Data/DeleteByDataId"
    },
    dialog: {
        width: 710,
        title: "数据权限"
    },
    table: {
        name: "list",
        primaryKey: "DataId"
    }
};

var $grid,
    tableObj,
    $tree,
    treeId = null;

$(function () {
    $tree = $('#tree');
    $tree.height(Language.mainHeight);
    initTree();
    initGird();
    initEvent();
});

//初始化事件
function initEvent() {
    $("#reload").click(function () {
        reload(true);
    });

    $("#search").click(function () {
        getGridData();
    });
}
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
            loading: true,
            even: true,
            height: Language.mainHeight + 10,
            cols: [[
                { type: "checkbox", fixed: 'left', width: 30 },
                { title: '序号', type: "numbers", fixed: 'left' },
                { field: 'Name', title: '名称', width: '120', sort: true, fixed: 'left' },
                { field: 'MenuNames', title: '归属菜单', width: '30%', sort: true },
                { field: 'IsFreeze', title: '冻结', sort: true, templet: '#templetFreeze', width: '100', align: 'center' },
                { field: 'OrderNo', title: '排序', sort: true, align: "center" },
                { field: 'Remark', title: '备注', sort: true }
            ]],
            where: { id: Language.common.guidempty },
            page: false
        });
    });
}

//初始化组织机构
function initTree() {
    UtilAjaxPostAsync("Menu/GetHaveDataPermissionMenu", {}, function (data) {
        $tree.jstree({
            core: {
                data: data,
                strings: {
                    'Loading ...': '正在加载...'
                }
            },
            plugins: ['types', 'dnd'],
            types: {
                default: {
                    icon: 'fa fa-tasks'
                }
            }
        }).bind('activate_node.jstree', function (obj, e) {
            treeId = e.node.id;
            getGridData();
        });
    });
}

//获取表格数据
function getGridData() {
    $grid.reload({
        where: {
            id: treeId,
            filters: getFilters($("#search-filters"))
        }
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

//刷新
function reload(reset) {
    if (reset) treeId = null;
    $tree.jstree("destroy");
    initTree();
    getGridData();
}