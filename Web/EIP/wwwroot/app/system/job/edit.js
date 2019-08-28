$(function () {
    $form = $("#editform");
    initForm();
    getById();
    initEvent();
});

var $form;

function initForm() {
    //数据校验
    $("#editform").formValidation({
        message: '请输入有效值',
        trigger: 'blur',
        fields: {
            JobName: {
                validators: {
                    notEmpty: {
                        message: '请输入作业名称'
                    }
                }
            },
            Expression: {
                validators: {
                    notEmpty: {
                        message: '请输入Cron表达式'
                    }
                }
            },
            NamespaceName: {
                validators: {
                    notEmpty: {
                        message: '请输入命名空间'
                    }
                }
            },
            ClassName: {
                validators: {
                    notEmpty: {
                        message: '请输入类名'
                    }
                }
            }
        }
    }).on('success.form.fv', function (e) {
        var formValue = $form.form().getFormSimpleData();
        formValue["ParametersJson"] = "[]";;
        UtilAjaxPostWait("Job/ScheduleJob", formValue , function (data) {
            DialogAjaxResult(data);
            if (data.ResultSign === 0) {
                getGridData();
                dialog.close();
            }
        });
        return false;
    });
}

//根据Id获取数据
function getById() {
    debugger;
    var jobName = $("#JobName").val();
    if (jobName != '') {
        UtilAjaxPostWait("/Job/JobEdit",
            {
                jobName: jobName,
                jobGroup: $("#JobGroup").val(),
                triggerName: $("#TriggerName").val(),
                triggerGroup: $("#TriggerGroup").val()
            }, function (data) {
            $form.form().initFormData(data);
        });
    }
}

//初始化事件
function initEvent() {
    //表达式点击
    $("#cron").click(function () {
        var el = document.createElement("a");
        document.body.appendChild(el);
        el.href = "http://cron.qqe2.com/"; //url 是你得到的连接
        el.target = '_new'; //指定在新窗口打开
        el.click();
        document.body.removeChild(el);
    });

    var $element = $("#ChoicedCalendar");
    var option = document.createElement("option");
    $(option).val("");
    $(option).text("===请选择===");
    $element.append(option);
    UtilAjaxPost("/Job/GetCalendar", {}, function (data) {
        debugger;
        $.each(data.data,
            function (index, item) {
                var option = document.createElement("option");
                $(option).val(item.CalendarName);
                $(option).text(item.CalendarName);
                $element.append(option);
            });
    });

}
