$(function () {
    $form = $("#editform");
    initForm();
    getById();
    $(".icon-picker").iconPicker();
});

var $form;

function initForm() {
    //数据校验
    $("#editform").formValidation({
        message: '请输入有效值',
        trigger: 'blur',
        fields: {
            Name: {
                validators: {
                    notEmpty: {
                        message: '请输入名称'
                    }
                }
            }
        }
    }).on('success.form.fv', function (e) {
        //获取后台数据
        var formValue = $form.form().getFormSimpleData();
        formValue.MenuId = formValue.IsCopy == 0 ? formValue.MenuId : null;
        UtilAjaxPostWait("Menu/SaveMenu", formValue, function (data) {
            perateStatus(data);
            dialog.close();
        });
        return false;
    });
}

//根据Id获取数据
function getById() {
    var id = $("#MenuId").val();
    if (id != '') {
        UtilAjaxPostWait("/Menu/GetById", { id: id }, function (data) {
            $form.form().initFormData(data);
        });
    }
}

//初始化下拉
function initSelect() {
    UtilAjaxPost("/UserControl/GetMenuRemoveChildren", {
        menuId: $("#MenuId").val(),
        isRemove: true
    }, function (data) {
        $selectObj.jstree({
            core: {
                data: data,
                strings: {
                    'Loading ...': '正在加载...'
                }
            },
            plugins: ['types', 'dnd', 'search'],
            types: {
                default: {
                    icon: 'fa  fa-file-text-o'
                }
            }
        }).bind('activate_node.jstree', function (obj, e) {
            $("#ParentId").val(e.node.id);
            $parentName.val(e.node.text).focus().blur();
            hideSelect();
        });
    });
}

//初始化事件
function initEvent() {
    $('#searchValue').on("click", function (e) {
        search($("#searchValueKey").val());
    });

    $('#searchValueKey').on("keyup", function (e) {
        search($(this).val());
    });

    $("#menuBtn,#ParentName").on("click",
        function () {
            showSelect();
        });
}

//显示下拉
function showSelect() {
    var cityObj = $parentName;
    var cityOffset = $parentName.offset();
    var editformOffset = $("#editform").offset();
    $("#content").css({ left: cityOffset.left - editformOffset.left + "px", top: (cityOffset.top + cityObj.outerHeight() + 2) - editformOffset.top + "px" }).slideDown("fast");
    $("body").bind("mousedown", onBodyDown);
}

//隐藏下拉
function hideSelect() {
    $("#content").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

//点击事件
function onBodyDown(event) {
    if (!(event.target.id === "menuBtn" || event.target.id === "content" || $(event.target).parents("#content").length > 0)) {
        hideSelect();
    }
}

var $selectObj = null, $parentName;

//关键字查询
function search(p) {
    $selectObj.jstree(true).search(p, true, true);
}

//加载完毕
$(document).ready(function () {
    $selectObj = $('#select-tree');
    $parentName = $("#ParentName");
    initSelect();
    initEvent();
    setTimeout("initWith()", 100);
});

function initWith() {
    $("#input-group").width($parentName.width() + 40);
}