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
            Name: {
                validators: {
                    notEmpty: {
                        message: '请输入名称'
                    }
                }
            },
            Expression: {
                validators: {
                    notEmpty: {
                        message: '请输入Cron表达式'
                    }
                }
            }
        }
    }).on('success.form.fv', function (e) {
        UtilAjaxPostWait("Job/SaveCalendar", $form.form().getFormSimpleData(), function (data) {
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
    var calendarName = $("#CalendarName").val();
    if (calendarName != '') {
        UtilAjaxPostWait("/Job/CalendarEdit", { calendarName: calendarName }, function (data) {
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
}
