$(function () {
    initEvent();
    initFunctionTree();
    initButtonData();
});


//初始化事件
function initEvent() {
    $("#accessButton").on("click", "li", function () {
        if (!$(this).hasClass("selected")) {
            $(this).attr("class", "selected");
        } else {
            $(this).attr("class", "");
        }
    });
}

//初始化树结构
function initFunctionTree() {
    UtilAjaxPostAsync("/Permission/GetMenuHavePermissionByPrivilegeMasterValue", {
        privilegeMasterValue: $("#privilegeMasterValue").val(),
        privilegeMaster: $("#privilegeMaster").val(),
        PrivilegeAccess: Language.privilegeAccess.func //模块按钮
    }, function (data) {
        $("#function-tree").jstree({
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
            treeNode = e.node;
            //隐藏提示
            $(".messagebox").hide();
            $("#accessButton").show();
            $("#alertdismiss").hide();
            $("#accessButton li").hide();
            checkAll = true;
            var modelidlength = $("li[modelid='" + treeNode.id + "']").length;
            if (modelidlength > 0) {
                $("li[modelid='" + treeNode.id + "']").show();
            } else {
                $(".msg-content").html("【" + treeNode.text + "】无模块按钮权限");
                $("#alertdismiss").show();
            }
        });
    });
}

//全选/重置
var checkAll = true, treeNode = null;

function selectall() {
    if (treeNode == null) {
        DialogTipsMsgWarn("请选择菜单", 1000);
        return;
    }
    var $module = $("li[modelid='" + treeNode.id + "']");
    if (checkAll) {
        $module.attr("class", "selected");
        checkAll = false;
    } else {
        $module.attr("class", "");
        checkAll = true;
    }
}

//表单提交
function formSubmit() {
    var json = "";
    var $function = $("li[class='selected']").find("a");
    $.each($function, function (i, item) {
        json += "{\"P\":\"" + item.id + "\"},";
    });
    json = json.substring(0, json.length - 1);
    json = "[" + json + "]";
    UtilAjaxPostWait("/Permission/SavePermission", {
        privilegeAccess: Language.privilegeAccess.func, //模块按钮
        privilegeMasterValue: $("#privilegeMasterValue").val(),
        privilegeMaster: $("#privilegeMaster").val(),
        menuPermissions: json //权限json字符串
    }, function (data) {
        DialogAjaxResult(data);
        //关闭
        dialog.close();
    });
}

//初始化用户数据
function initButtonData() {
    UtilAjaxPostAsync("/Permission/GetFunctionByPrivilegeMaster", {
        privilegeMasterValue: $("#privilegeMasterValue").val(),
        privilegeMaster: $("#privilegeMaster").val()
    }, function (data) {
        $.each(data, function (i, item) {
            var li = ' <li title="' + item.Name + '" data-toggle="tooltip" modelid="' + item.MenuId + '"  class="' + item.Remark + '"><a id="' + item.MenuButtonId + '"><i class="' + item.Icon + '"></i>' + item.Name + '</a><i class="check"></i></li>';
            $("#accessButton").append(li);
        });
    });
}