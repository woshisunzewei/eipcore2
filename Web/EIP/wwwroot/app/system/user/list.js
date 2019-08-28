var options = {
    url: {
        get: Language.common.apiurl + "/User/GetPagingUser",
        edit: "/System/User/Edit",
        delete: "/User/DeleteUser"
    },
    dialog: {
        width: 590,
        title: "用户"
    },
    table: {
        name: "list",
        primaryKey: "UserId"
    }
};

var $grid,
    tableObj,
    $tree,
    $arrowin,
    $orgId,
    treeId = Language.common.guidorg;

$(function () {
    $tree = $('#tree');
    $arrowin = $("#arrowin");
    $orgId = $("#PrivilegeMasterValue");
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
                { field: 'Code', title: '登录名', width: 150, sort: true, fixed: 'left' },
                { field: 'Name', title: '名称', width: 100, sort: true, fixed: 'left' },
                { field: 'OrganizationNames', title: '归属单位', width: '40%', sort: true },
                { field: 'IsFreeze', title: "冻结", width: 80, align: "center", templet: '#templetFreeze' },
                { field: 'Mobile', title: "手机号码", width: 100 },
                { field: 'CreateTime', title: "创建时间", width: 120, align: "center" },
                { field: 'CreateUserName', title: "创建人", width: 80 },
                { field: 'UpdateTime', title: "修改时间", width: 120, align: "center" },
                { field: 'UpdateUserName', title: "修改人", width: 80 },
                { field: 'LastVisitTime', title: "最后登录时间", width: 150 },
                { field: 'Remark', title: "备注", width: 250 }
            ]],
            limit: 50,
            limits: [50, 100, 200, 500],
            where: {
                privilegeMaster: Language.privilegeMaster.organization,
                privilegeMasterValue: treeId,
                sidx: "u.CreateTime"
            },
            even: true,
            height: Language.mainHeight + 10,
            page: true
        });
    });
}

//初始化组织机构
function initTree() {
    UtilAjaxPostAsync("/User/GetOrganizationDataTree", {}, function (data) {
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
            $orgId.val(treeId);
            getGridData();
        });
    });
}

//获取表格数据
function getGridData() {
    $grid.reload({
        where: {
            filters: getFilters($("#user-search")),
            privilegeMaster: Language.privilegeMaster.organization,
            privilegeMasterValue: treeId,
            sidx: "u.CreateTime"
        },
        page: {
            curr: 1
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

//重置密码
function resetPassword() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        var dialog = BootstrapDialog.show({
            size: BootstrapDialog.SIZE_SMALL,
            title: '请输入重置密码-' + info.Name,
            message: $('<textarea id="resetPassword" class="form-control" placeholder="请输入重置密码..." value="">123456</textarea>'),
            buttons: [{
                label: '确定',
                cssClass: 'eip-btn',
                hotkey: 13,
                action: function () {
                    dialog.close();
                    UtilAjaxPostWait("/User/ResetPassword",
                        {
                            id: info[options.table.primaryKey],
                            EncryptPassword: $("#resetPassword").val()
                        },
                        function (data) {
                            DialogAjaxResult(data);
                            if (data.ResultSign === 0) {
                                reload(false);
                                dialog.close();
                            }
                        }
                    );
                }
            }]
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
    if (reset) {
        treeId = null;
        $orgId.val(treeId);
    }
    $tree.jstree("destroy");
    initTree();
    getGridData();
}


//模块权限
function menuPermission() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/System/Permission/Menu?privilegeMasterValue=" + info.UserId + "&privilegeMaster=" + Language.privilegeMaster.user, "模块权限授权-" + info.Name, 380);
    });
}

//模块按钮权限
function functionPermission() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/System/Permission/Button?privilegeMasterValue=" + info.UserId + "&privilegeMaster=" + Language.privilegeMaster.user, "模块按钮权限授权-" + info.Name, 810);
    });
}

//数据权限
function dataPermission() {
    //查看是否选中
    GridIsSelectLayTable(tableObj, options.table.name, function () {
        var info = GridGetSingSelectDataLayTable(tableObj, options.table.name);
        DialogOpen("/System/Permission/Data?privilegeMasterValue=" + info.UserId + "&privilegeMaster=" + Language.privilegeMaster.user, "数据权限授权-" + info.Name, 810);
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