$(function () {
    $form = $("#editform");
    initForm();
    getById();
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
            },
            Version: {
                validators: {
                    notEmpty: {
                        message: '请输入版本号'
                    }
                }
            }
        }
    }).on('success.form.fv', function (e) {
        var submitValue = $form.form().getFormSimpleData();
        submitValue["DesignJson"] = '{"title":"{0}","nodes":{"{1}":{"name":"开始","left":163,"top":220,"type":"start round","width":24,"height":24,"alt":true},"{2}":{"name":"结束","left":803,"top":220,"type":"end round","width":24,"height":24,"alt":true}},"lines":{},"areas":{},"initNum":1}';
        UtilAjaxPostWait("Workflow/SaveWorkflowProcess",
            submitValue , function (data) {
                perateStatus(data);
                dialog.close();
            });
        return false;
    });
}

//根据Id获取数据
function getById() {
    var id = $("#ProcessId").val();
    if (id != '') {
        //获取后台数据
        UtilAjaxPostWait("Workflow/GetById", { id: id }, function (data) {
            $form.form().initFormData(data);
        });
    }
}

//初始化下拉
function initSelect() {
    UtilAjaxPost("/Organization/GetOrganizationTree", {}, function (data) {
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
            $("#OrganizationId").val(e.node.id);
            $organizationName.val(e.node.text).focus().blur();
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

    $("#menuBtn,#OrganizationName").on("click",
        function () {
            showSelect();
        });
}

//显示下拉
function showSelect() {
    var cityObj = $organizationName;
    var cityOffset = $organizationName.offset();
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

var $selectObj = null, $organizationName;

//关键字查询
function search(p) {
    $selectObj.jstree(true).search(p, true, true);
}

//加载完毕
$(document).ready(function () {
    $selectObj = $('#select-tree');
    $organizationName = $("#OrganizationName");
    initSelect();
    initEvent();
    setTimeout("initWith()", 100);
});
function initWith() {
    $("#input-group").width($organizationName.width() + 40);
}