var options = {
    url: {
        get: Language.common.apiurl + "/Dictionary/GetDictionariesByParentId",
        edit: "/System/Dictionary/Edit",
        delete: "/Dictionary/DeleteDictionary"
    },
    dialog: {
        width: 610,
        title: "字典"
    },
    table: {
        name: "list",
        primaryKey: "DictionaryId"
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
    $("#reload").click(function() {
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
            cols: [[
                { type: "checkbox", fixed: 'left', width: 30 },
                { title: '序号', type: "numbers", fixed: 'left' },
                { field: 'Name', title: '名称', width: '200', sort: true, fixed: 'left' },
                { field: 'ParentNames', title: '层级', width: '40%', sort: true },
                { field: 'Value', title: '值', sort: true },
                { field: 'IsFreeze', title: "冻结", width: 80, align: "center", templet: '#templetFreeze' },
                { field: 'OrderNo', title: '排序', sort: true, align: 'center' },
                { field: 'Remark', title: '备注', sort: true }
            ]],
            even: true,
            height: Language.mainHeight + 10,
            page: false
        });
    });
}

//初始化树
function initTree() {
    UtilAjaxPostAsync("Dictionary/GetDictionaryTree", {}, function (data) {
        $tree.jstree({
            core: {
                data: data,
                strings: {
                    'Loading...': '正在努力加载中...'
                }
            },
            plugins: ['types'],
            types: {
                default: {
                    icon: 'fa fa-file-text-o'
                }
            }
        }).bind('activate_node.jstree', function (obj, e) {
            treeId = e.node.id;
            getGridData();
        }).bind('loaded.jstree', function () {
            if (treeId !== null) {
                $tree.jstree('select_node', treeId, true);
            }
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