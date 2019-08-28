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
        UtilAjaxPostWait("Workflow/SaveButton",
            $form.form().getFormSimpleData(), function (data) {
                perateStatus(data);
                dialog.close();
            });
        return false;
    });
}

//根据Id获取数据
function getById() {
    var id = $("#ButtonId").val();
    if (id != '') {
        //获取后台数据
        UtilAjaxPostWait("Workflow/GetButtonById", { id: id }, function (data) {
            $form.form().initFormData(data);
        });
    }
}
