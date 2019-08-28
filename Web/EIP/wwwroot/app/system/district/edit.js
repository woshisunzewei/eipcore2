var $form = $("#editform");
function initForm() {
    //初始化控件
    var form = $form.form();
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
            },
            DistrictId: {
                validators: {
                    notEmpty: {
                        message: '请输入代码'
                    }
                }
            }
        }
    }).on('success.form.fv',
        function (e) {
            var data = form.getFormSimpleData();
            data["ParentId"] = data["parentName"] !== "" ? data["ParentId"] : "";
            UtilAjaxPostWait("/District/SaveDistrict",
                form.getFormSimpleData(),
                function () {
                    if (DialogAjaxResult(data)) {
                        reload(true);
                        dialog.close();
                    }
                });
            return false;
        });
};

//根据Id获取数据
function getById() {
    var id = $("#DistrictId").val();
    if (id != '') {
        //获取后台数据
        UtilAjaxPostWait("/District/GetDistrictById", { id: id }, function (data) {
            $form.form().initFormData(data);
        });
    }
}

//初始化编辑树
function initEditTree() {
    $selectObj.jstree({
        core: {
            data: function (obj, callback) {
                var id = obj.id;
                UtilAjaxPostWait("/District/GetDistrictTreeByParentId", { id: id == "#" ? "0" : id }, function (data) {
                    callback.call(this, data);
                });
            }
        },
        plugins: ["types"],
        types: {
            default: {
                icon: 'fa fa-file-text-o'
            }
        }
    }).bind('activate_node.jstree', function (obj, e) {
        $parentId.val(e.node.id);
        $parentName.val(e.node.text).focus().blur();
        hideEditSelect();
    });
}


//初始化事件
function initEvent() {
    $("#menuBtn,#ParentName").on("click",
        function () {
            showEditSelect();
        });
}

//显示下拉
function showEditSelect() {
    var cityObj = $parentName;
    var cityOffset = $parentName.offset();
    var editformOffset = $("#editform").offset();
    $("#content").css({ left: cityOffset.left - editformOffset.left + "px", top: (cityOffset.top + cityObj.outerHeight() + 2) - editformOffset.top + "px" }).slideDown("fast");
    $("body").bind("mousedown", onBodyDown);
}

//隐藏下拉
function hideEditSelect() {
    $("#content").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

//点击事件
function onBodyDown(event) {
    if (!(event.target.id === "menuBtn" || event.target.id === "content" || $(event.target).parents("#content").length > 0)) {
        hideEditSelect();
    }
}

var $selectObj = null, $parentName, $parentId;

//加载完毕
$(document).ready(function () {
    $selectObj = $('#select-district-tree');
    $parentName = $("#ParentName");
    $parentId = $("#ParentId");
    initEvent();
    setTimeout("initWith()", 100);
    initForm();
    getById();
    initEditTree();
});

function initWith() {
    $("#input-group").width($parentName.width() + 40);
}