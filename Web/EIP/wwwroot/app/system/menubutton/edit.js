var $form;
$(function () {
    $form = $("#editform");
    $(".icon-picker").iconPicker();
    initForm();
    getById();
});
function initForm() {
    //数据校验
    $("#editform").formValidation({
        message: '请输入有效值',
        trigger: 'blur',
        fields: {
            Name: {
                validators: {
                    notEmpty: {
                        message: '请输入姓名'
                    }
                }
            },
            MenuName: {
                validators: {
                    notEmpty: {
                        message: '请选择归属菜单'
                    }
                }
            }
        }
    }).on('success.form.fv', function (e) {
        var formValue = $form.form().getFormSimpleData();
        formValue.MenuButtonId = formValue.IsCopy == 0 ? formValue.MenuButtonId : null;
        UtilAjaxPostWait("MenuButton/SaveMenuButton", formValue, function (data) {
            if (DialogAjaxResult(data)) {
                getGridData();
                //关闭
                dialog.close();
            }
        });
        return false;
    });
}

//根据Id获取数据
function getById() {
    var id = $("#MenuButtonId").val();
    if (id != '') {
        //获取后台数据
        UtilAjaxPostWait("MenuButton/GetById", { id: id }, function (data) {
            $form.form().initFormData(data);
        });
    }
}

//初始化下拉
function initSelect() {
    UtilAjaxPost("/Menu/GetHaveMenuButtonPermissionMenu", {}, function (data) {
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
            $("#MenuId").val(e.node.id);
            //加载数据
            $menuName.val(e.node.text).focus().blur();
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

    $("#menuBtn,#MenuName").on("click",
        function () {
            showSelect();
        });
}

//显示下拉
function showSelect() {
    var cityObj = $menuName;
    var cityOffset = $menuName.offset();
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

var $selectObj = null,
    $menuName;

//关键字查询
function search(p) {
    $selectObj.jstree(true).search(p, true, true);
}

//加载完毕
$(document).ready(function () {
    $selectObj = $('#select-tree');
    $menuName = $("#MenuName");
    initSelect();
    initEvent();
    setTimeout("initWith()", 100);
});

function initWith() {
    $("#input-group").width($menuName.width() + 40);
}