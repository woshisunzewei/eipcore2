var options = {
    url: {
        get: Language.common.apiurl + "/Menu/GetMenuByParentId",
        edit: "/System/Menu/Edit",
        delete: "/Menu/DeleteMenuAndChilds"
    },
    dialog: {
        width: 610,
        title: "模块"
    },
    table: {
        name: "list",
        primaryKey: "MenuId"
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
            even: true,
            height: Language.mainHeight + 10,
            cols: [[
                { type: "checkbox", fixed: 'left', width: 30 },
                { title: '序号', type: "numbers", fixed: 'left' },
                { field: 'Name', title: '名称', width: '130', sort: true, fixed: 'left' },
                { field: 'ParentNames', title: '上级', width: '30%', sort: true },
                { field: 'Icon', title: '图标', templet: '#templetIcon', align: "center", width: '80', sort: true },
                { field: 'Area', title: '区域', sort: true, width: 80 },
                { field: 'Controller', title: '控制器', sort: true, width: '130' },
                { field: 'Action', title: '方法', sort: true, width: '90' },
                { field: 'OpenUrl', title: '地址', sort: true, width: '180' },
                { field: 'IsShowMenu', title: '页面显示', sort: true, templet: '#templetIsShowMenu', width: '100', align: 'center' },
                { field: 'HaveMenuPermission', title: '菜单权限', sort: true, templet: '#templetHaveMenuPermission', width: '100', align: 'center' },
                { field: 'HaveFunctionPermission', title: '按钮权限', sort: true, templet: '#templetHaveFunctionPermission', width: '100', align: 'center' },
                { field: 'HaveDataPermission', title: '数据权限', sort: true, templet: '#templetHaveDataPermission', width: '100', align: 'center' },
                { field: 'HaveFieldPermission', title: '字段权限', sort: true, templet: '#templetHaveFieldPermission', width: '100', align: 'center' },
                { field: 'IsFreeze', title: '冻结', sort: true, templet: '#templetFreeze', width: '100', align: 'center' },
                { field: 'OrderNo', title: '排序', sort: true, align: 'center' },
                { field: 'Remark', title: '备注', sort: true }
            ]],
            initSort: {
                field: 'OrderNo',
                type: 'asc'
            },
            page: false
        });
    });
}

//初始化树结构:同步
function initTree() {
    UtilAjaxPostAsync("Menu/GetAllMenuTree", {}, function (data) {
        $tree.jstree({
            core: {
                data: data,
                strings: {
                    'Loading ...': '正在加载...'
                }
            },
            plugins: ['types'],
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
        DialogOpen(options.url.edit + "?id=" + info[options.table.primaryKey] + "&iscopy=0", "编辑" + options.dialog.title + "-" + info.Name, options.dialog.width);
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

//复制
function copy() {
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen(options.url.edit + "?id=" + info[options.table.primaryKey] + "&iscopy=1", "复制" + options.dialog.title + "-" + info.Name, options.dialog.width);
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