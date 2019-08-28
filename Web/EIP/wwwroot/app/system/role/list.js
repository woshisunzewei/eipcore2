var options = {
    url: {
        get: Language.common.apiurl + "/Role/GetRolesByOrganization",
        edit: "/System/Role/Edit",
        delete: "/Role/DeleteRole"
    },
    dialog: {
        width: 610,
        title: "角色"
    },
    table: {
        name: "list",
        primaryKey: "RoleId"
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

    $("#search").click(function () {
        getGridData();
    });

    $arrowin.click(function () {
        arrowin();
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
                { field: 'Name', title: '名称', width: '180', sort: true, fixed: 'left' },
                { field: 'OrganizationNames', title: '归属单位', width: '40%', sort: true },
                { field: 'IsFreeze', title: '冻结', templet: '#templetFreeze', width: '100', sort: true, align: "center" },
                { field: 'CreateTime', title: "创建时间", width: 120, align: "center" },
                { field: 'CreateUserName', title: "创建人", width: 80 },
                { field: 'UpdateTime', title: "修改时间", width: 120, align: "center" },
                { field: 'UpdateUserName', title: "修改人", width: 80 },
                { field: 'OrderNo', title: '排序', sort: true, align: "center" },
                { field: 'Remark', title: '备注', sort: true }
            ]]
        });
    });

}

//初始化组织机构
function initTree() {
    UtilAjaxPostAsync("Organization/GetOrganizationTree", {}, function (data) {
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

//角色用户
function roleUser() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/Common/UserControl/ChosenPrivilegeMasterUser?" +
            "privilegeMaster=" + Language.privilegeMaster.role + "&" +
            "privilegeMasterValue=" + info.RoleId, "维护角色人员-" + info.Name, 800);
    });
}

//角色复制
function copy() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        var dialog = BootstrapDialog.show({
            size: BootstrapDialog.SIZE_SMALL,
            title: '请输入角色名称-' + info.Name,
            message: $('<textarea id="copy-role-name" class="form-control" placeholder="请输入角色名称..." value="">' + info.Name + '</textarea>'),
            buttons: [{
                label: '确定',
                icon: 'fa fa-check',
                cssClass: 'eip-btn',
                hotkey: 13,
                action: function () {
                    //判断是否值为空
                    var name = $("#copy-role-name").val();
                    if (name == "") {
                        DialogTipsMsgWarn("角色名称不能为空", 2000);
                        return false;
                    }

                    UtilAjaxPostWait("/Role/CopyRole",
                        {
                            id: info[options.table.primaryKey],
                            Name: name
                        },
                        function (data) {
                            DialogAjaxResult(data);
                            if (data.ResultSign === 0) {
                                reload(true);
                                dialog.close();
                            }
                        }
                    );
                }
            }, {
                label: '取消',
                icon: 'fa fa-times',
                cssClass: 'btn-danger',
                action: function () {
                    dialog.close();

                }
            }]
        });
    });
}

//模块权限
function menuPermission() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/System/Permission/Menu?privilegeMasterValue=" + info.RoleId + "&privilegeMaster=" + Language.privilegeMaster.role, "模块权限授权-" + info.Name, 380);
    });
}

//模块按钮权限
function functionPermission() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/System/Permission/Button?privilegeMasterValue=" + info.RoleId + "&privilegeMaster=" + Language.privilegeMaster.role, "模块按钮权限授权-" + info.Name, 810);
    });
}

//数据权限
function dataPermission() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/System/Permission/Data?privilegeMasterValue=" + info.RoleId + "&privilegeMaster=" + Language.privilegeMaster.role, "数据权限授权-" + info.Name, 810);
    });
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