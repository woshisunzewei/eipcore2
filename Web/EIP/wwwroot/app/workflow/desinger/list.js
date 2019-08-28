var options = {
    url: {
        get: Language.common.apiurl + "/Workflow/GetWorkflow",
        edit: "/Workflow/Desinger/Edit",
        delete: "/Workflow/DeleteUser"
    },
    dialog: {
        width: 590,
        title: "流程"
    },
    table: {
        name: "list",
        primaryKey: "ProcessId"
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
                { type: "checkbox", fixed: 'left', width: 30 },
                { title: '序号', type: "numbers", fixed: 'left' },
                { field: 'Name', title: '名称', width: 250, sort: true, fixed: 'left' },
                //{ field: 'OrganizationNames', title: '归属单位', width: '40%' },
                { field: 'TypeName', title: '类别', width: 200, sort: true, fixed: 'left' },
                { field: 'Version', title: '版本号', width: 60 },
                { field: 'IsFreeze', title: "冻结", width: 80, align: "center", templet: '#templetFreeze' },
                { field: 'CreateTime', title: "创建时间", width: 120, align: "center" },
                { field: 'CreateUserName', title: "创建人", width: 80 },
                { field: 'UpdateTime', title: "修改时间", width: 120, align: "center" },
                { field: 'UpdateUserName', title: "修改人", width: 80 },
                { field: 'Remark', title: "备注", width: 250 }
            ]],
            limit: 50,
            limits: [50, 100, 200, 500],
            where: {
                sidx: "Type"
            },
            even: true,
            height: Language.mainHeight + 10
        });
    });
}

//获取表格数据
function getGridData() {
    $grid.reload({
        where: {
            type: treeId
        }
    });
}

//初始化树
function initTree() {
    UtilAjaxPostAsync("Dictionary/GetDictionaryTreeByParentIdsHaveParent", { id: Language.dictionary.workflowtype}, function (data) {
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

//设计布局
function layout() {
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/Workflow/Desinger/Layout?id=" + info[options.table.primaryKey], "编辑" + options.dialog.title + "-" + info.Name+" "+info.Version, Language.mainWidth-200);
    });
}

//设计布局
function preview() {
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/Workflow/Desinger/Preview?id=" + info[options.table.primaryKey], "预览" + options.dialog.title + "-" + info.Name + " " + info.Version, Language.mainWidth - 200);
    });
}