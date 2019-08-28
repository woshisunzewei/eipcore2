var $selectObj = null,
    $parentName,
    $content,
    $inputGroup,
    $ParentId,
    $form;

$(function () {
    $form = $("#editform");
    initForm();
    $selectObj = $('#select-tree');
    $parentName = $("#ParentName");
    $ParentId = $("#ParentId");
    $content = $("#content");
    $inputGroup = $("#input-group");
    initSelect();
    initEvent();
    getById();
    setTimeout("initWith()", 100);
});

//初始化表单
function initForm() {
    //数据校验
    $form.formValidation({
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
        UtilAjaxPostWait("Dictionary/SaveDictionary", $form.form().getFormSimpleData(), function (data) {
            perateStatus(data);
            dialog.close();
        });
        return false;
    });
}

//根据Id获取数据
function getById() {
    var id = $("#DictionaryId").val();
    if (id != '') {
        //获取后台数据
        UtilAjaxPostWait("Dictionary/GetById", { id: id }, function (data) {
            $form.form().initFormData(data);
        });
    }
}

//初始化下拉
function initSelect() {
    UtilAjaxPost("UserControl/GetDictionaryRemoveChildren", { id: $("#DictionaryId").val() }, function (data) {
        $selectObj.jstree({
            core: {
                data: data,
                strings: {
                    'Loading ...': '正在加载...'
                }
            },
            plugins: ['types', 'search'],
            types: {
                default: {
                    icon: 'fa  fa-file-text-o'
                }
            }
        }).bind('activate_node.jstree', function (obj, e) {
            $ParentId.val(e.node.id);
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

    $("#menuBtn,#ParentName").on("click", function () {
        showSelect();
    });
}

//显示下拉
function showSelect() {
    var cityObj = $parentName;
    var cityOffset = $parentName.offset();
    var editformOffset = $("#editform").offset();
    $content.css({ left: cityOffset.left - editformOffset.left + "px", top: (cityOffset.top + cityObj.outerHeight() + 2) - editformOffset.top + "px" }).slideDown("fast");
    $("body").bind("mousedown", onBodyDown);
}

//隐藏下拉
function hideSelect() {
    $content.fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

//点击事件
function onBodyDown(event) {
    if (!(event.target.id === "menuBtn" || event.target.id === "content" || $(event.target).parents("#content").length > 0)) {
        hideSelect();
    }
}

//关键字查询
function search(p) {
    $selectObj.jstree(true).search(p, true, true);
}

//初始化宽度
function initWith() {
    $("#input-group").width($parentName.width() + 40);
}