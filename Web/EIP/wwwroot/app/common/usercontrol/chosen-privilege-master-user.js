$(function () {
    $scrollBar = $(".scrollBar");
    $arrowin = $("#arrowin");
    initUserData();
    loadChosenPrivilegeMasterUserTree();
    initEvent();
});

var chosenPrivilegeMasterUserzNodes,
    chosenPrivilegeMasterUserTreeObj,
    chosenPrivilegeMasterUserTreeSetting,
    $scrollBar,
    $arrowin;

//表单提交
function formSubmit() {
    var users = $("li[class='selected']", $scrollBar).find("a");
    var json = "";

    $.each(users, function (i, item) {
        json += "{\"u\":\"" + item.id + "\"},";
    });
    json = json.substring(0, json.length - 1);
    json = "[" + json + "]";

    UtilAjaxPostWait("/UserControl/SavePrivilegeMasterUser",
        {
            privilegeMasterUser: json,
            privilegeMasterValue: $("#privilegeMasterValue").val(),
            privilegeMaster: $("#privilegeMaster").val()
        },
        success);
}

//提交成功
function success(data) {
    if (DialogAjaxResult(data)) {
        dialog.close();
    }
}

//初始化企业
function initChosenPrivilegeMasterUserTree() {
    UtilAjaxPost("Organization/GetOrganizationTree",
        {},
        function (data) {
            $("#chosenPrivilegeMasterUserTree").jstree({
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
                    searchUserByOrgId(e.node.id);
                });
        });
}


//根据企业id查询对应的人员信息
function searchUserByOrgId(orgId) {
    $("li", $scrollBar).hide();
    $("[name='" + orgId + "']").show();
}

//折叠/展开
var expand = false;

function arrowin() {
    if (expand) {
        chosenPrivilegeMasterUserTreeObj.expandAll(false);
        expand = false;
        $arrowin.html("展开").attr("class", "fa fa-folder-open");
        $arrowin.parent().attr("title", "展开");
    } else {
        chosenPrivilegeMasterUserTreeObj.expandAll(true);
        expand = true;
        $arrowin.html("折叠").attr("class", "fa fa-folder");
        $arrowin.parent().attr("title", "折叠");
    }
}

//加载页面数据
function loadChosenPrivilegeMasterUserTree() {
    initChosenPrivilegeMasterUserTree();
    $("li", $scrollBar).show();
}

//初始化事件
function initEvent() {
    $("#AccessButton").on("click", "li", function () {
        if (!$(this).hasClass("selected")) {
            $(this).attr("class", "selected");
        } else {
            $(this).attr("class", "");
        }
    });
}

//查询
function search() {
    var key = $("#txtKeywords").val().toLocaleLowerCase();
    if (key != "") {
        $("li", $scrollBar).hide();
    } else {
        $("li", $scrollBar).show();
    }
    $("[searchcode*='" + key + "']", $(".scrollBar")).show();
    $("[searchname*='" + key + "']", $(".scrollBar")).show();
}

//全选/重置
var checkAll = true;
function selectall() {
    var treeNode = ZtreeGetSelectedNodes($.fn.zTree.getZTreeObj("chosenPrivilegeMasterUserTree"));
    var $module = $("li[modelid='" + treeNode[0].id + "']");
    if (checkAll) {
        $module.attr("class", "selected");
        checkAll = false;
    } else {
        $module.attr("class", "");
        checkAll = true;
    }
}

//初始化用户数据
function initUserData() {
    UtilAjaxPostAsync("/UserControl/GetChosenPrivilegeMasterUser", {
        privilegeMasterValue: $("#privilegeMasterValue").val(),
        privilegeMaster: $("#privilegeMaster").val()
    }, function (data) {
        $.each(data,
            function (i, item) {
                var classInfo = item.Exist ? "selected" : "";
                var li = ' <li title="' +
                    item.Code +
                    '-' +
                    item.Name +
                    '" data-toggle="tooltip"  searchcode="' +
                    item.Code +
                    '" searchname="' +
                    item.Name +
                    '" name="' +
                    item.OrganizationId +
                    '" class="' +
                    classInfo +
                    '"><a id="' +
                    item.UserId +
                    '"> ' +
                    item.Name +
                    '</a><i class="check"></i></li>';
                $("#AccessButton").append(li);
            });
    });
}