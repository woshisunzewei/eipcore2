$(function () {
    $tree = $("#menuTree");
    $select = $("#select");
    $arrowin = $("#arrowin");
    initMenuTree();
});
var $tree,
    $select,
    $arrowin,
    json = ""; //菜单设置

//初始化树结构:同步
function initMenuTree() {
    UtilAjaxPostAsync("/Menu/GetHaveMenuPermissionMenu", {}, function (data) {
        $tree.jstree({
            core: {
                data: data,
                check_callback: true,
                multiple: true,
                strings: {
                    'Loading ...': '正在加载...'
                }
            },
            plugins: ["types", "checkbox"],
            types: {
                default: {
                    icon: 'fa fa-tasks'
                }
            }
        }).bind("click.jstree", function (e, data) {
            //获取所有选择项
            var nodes = $tree.jstree().get_checked(true);
            json = "";
            $.each(nodes, function (i, item) {
                json += "{\"P\":\"" + item.id + "\"},";
            });
            if (json != "")
                json = json.substring(0, json.length - 1);
            json = "[" + json + "]";
        }).on("loaded.jstree", function (event, data) {
            $tree.jstree("deselect_all", true);
            UtilAjaxPost("/Permission/GetPermissionByPrivilegeMasterValue", {
                privilegeMasterValue: $("#privilegeMasterValue").val(),
                privilegeMaster: $("#privilegeMaster").val(),
                PrivilegeAccess: Language.privilegeAccess.menu //模块按钮
            }, function (data) {
                json = "";
                $.each(data, function (i, item) {
                    json += "{\"P\":\"" + item.PrivilegeAccessValue + "\"},";
                    $tree.jstree('select_node', item.PrivilegeAccessValue, true);
                });
                if (json != "")
                    json = json.substring(0, json.length - 1);
                json = "[" + json + "]";
            });
        });;
    });
}

//折叠/展开
var expand = true;

function arrowin() {
    if (expand) {
        $tree.jstree().close_all();
        expand = false;
        $arrowin.html("展开").attr("class", "fa fa-folder-open");
        $arrowin.parent().attr("title", "展开");
    } else {
        $tree.jstree().open_all();
        expand = true;
        $arrowin.html("折叠").attr("class", "fa fa-folder");
        $arrowin.parent().attr("title", "折叠");
    }
}

//全选/重置
var checkAll = true;

function selectall() {
    if (checkAll) {
        $tree.jstree().select_all();
        checkAll = false;
        $select.html("反选").attr("class", "fa fa-circle-thin");
        $select.parent().attr("title", "反选");
    } else {
        $tree.jstree().deselect_all();
        checkAll = true;
        $select.html("全选").attr("class", "fa fa-check-circle-o");
        $select.parent().attr("title", "全选");
    }
}

//表单提交
function formSubmit() {
    DialogTipsMsgWait("正在处理中");
    UtilAjaxPostWait("/Permission/SavePermission",
        {
            privilegeAccess: Language.privilegeAccess.menu, //菜单
            menuPermissions: json,
            privilegeMasterValue: $("#privilegeMasterValue").val(),
            privilegeMaster: $("#privilegeMaster").val()
        }, function (data) {
            DialogAjaxResult(data);
            //关闭
            dialog.close();
        });
}