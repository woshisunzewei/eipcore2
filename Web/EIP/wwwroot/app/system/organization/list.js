var options = {
    url: {
        get: Language.common.apiurl + "/Organization/GetOrganizationsByParentId",
        edit: "/System/Organization/Edit",
        delete: "/Organization/DeleteOrganization"
    },
    dialog: {
        width: 610,
        title: "单位"
    },
    table: {
        name: "list",
        primaryKey: "OrganizationId"
    }
};

var $grid,
    tableObj,
    $tree,
    $arrowin,
    treeId = null;

$(function () {
    $tree = $('#tree');
    $arrowin = $("#arrowin");
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

    $("#arrowinHref").click(function () {
        arrowin();
    });

    $("#search").click(function () {
        getGridData();
    });
}

//初始化表格
function initGird() {
    layui.use('table',
        function () {
            tableObj = layui.table;
            //方法级渲染
            $grid = tableObj.render({
                elem: '#' + options.table.name,
                size: "sm",
                method: 'post',
                url: options.url.get,
                even: true,
                height: Language.mainHeight + 10,
                cols: [
                    [
                        { type: "checkbox", fixed: 'left', width: 30 },
                        { title: '序号', type: "numbers", fixed: 'left' },
                        { field: 'Name', title: '全称', width: '180', sort: true, fixed: 'left' },
                        { field: 'ShortName', title: '简称', width: '180', sort: true },
                        { field: 'ParentNames', title: '层级', width: '40%', sort: true },
                        { field: 'MainSupervisor', title: '主负责人', width: '100', sort: true },
                        { field: 'MainSupervisorContact', title: '主负责人联系方式', width: '180', sort: true },
                        { field: 'IsFreeze', title: '冻结', templet: '#templetFreeze', width: '100', sort: true, align: "center" },
                        //{ field: 'Longitude', title: '经度', width: '95', sort: true },
                        //{ field: 'Latitude', title: '纬度', width: '95', sort: true },
                        { field: 'CreateTime', title: "创建时间", width: 120, align: "center" },
                        { field: 'CreateUserName', title: "创建人", width: 80 },
                        { field: 'UpdateTime', title: "修改时间", width: 120, align: "center" },
                        { field: 'UpdateUserName', title: "修改人", width: 80 },
                        { field: 'OrderNo', title: '排序', sort: true, align: "center" },
                        { field: 'Remark', title: '备注', sort: true },
                        //{ title: '操作', templet: '#previewMap', width: '100', fixed: 'right', align: "center" }
                    ]
                ], initSort: {
                    field: 'OrderNo',
                    type: 'asc'
                },
                page: false
            });
        });
}

//初始化树结构:同步
function initTree() {
    UtilAjaxPostAsync("/Organization/GetOrganizationDataTree",
        {},
        function (data) {
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
                        icon: 'fa  fa-file-text-o'
                    }
                }
            }).bind('activate_node.jstree',
                function (obj, e) {
                    treeId = e.node.id;
                    getGridData();
                });
        });
}

//获取表格数据
function getGridData() {
    $grid.reload({
        where: { 
            filters: getFilters($("#search-filters")),
            id: treeId
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

//图
function orgchart() {
    DialogOpen("/System/Organization/OrgChart", "组织机构结构图", 1024);
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

//地图预览
function previewMap(longitude, latitude, name) {
    if (longitude == "null" || latitude == "null") {
        DialogTipsMsgWarn("请完善单位经纬度信息", 1000);
        return false;
    }
    DialogOpen("/System/Organization/Map?longitude=" + longitude + "&latitude=" + latitude + "&name=" + name, "地图预览" + "-" + name, 890);
}

//折叠/展开
var expand = true;

function arrowin() {
    if (expand) {
        $tree.jstree().close_all();
        expand = false;
        $arrowin.html("展开").attr("class", "fa fa-folder-open");
    } else {
        $tree.jstree().open_all();
        expand = true;
        $arrowin.html("折叠").attr("class", "fa fa-folder");
    }
}