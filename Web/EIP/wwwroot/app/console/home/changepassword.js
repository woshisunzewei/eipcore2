$(function () {
    initNewPasswordEvent();
    ValidformNeed();
    initData();
});

//初始化事件
function initNewPasswordEvent() {
    var $level = $('#level');
    $('#newPassword').keyup(function () {
        var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
        var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
        var enoughRegex = new RegExp("(?=.{1,}).*", "g");

        if (false == enoughRegex.test($(this).val())) {
            $level.removeClass('pw-weak');
            $level.removeClass('pw-medium');
            $level.removeClass('pw-strong');
            $level.addClass(' pw-defule');
            //密码小于六位的时候，密码强度图片都为灰色
        }
        else if (strongRegex.test($(this).val())) {
            $level.removeClass('pw-weak');
            $level.removeClass('pw-medium');
            $level.removeClass('pw-strong');
            $level.addClass(' pw-strong');
            //密码为八位及以上并且字母数字特殊字符三项都包括,强度最强
        }
        else if (mediumRegex.test($(this).val())) {
            $level.removeClass('pw-weak');
            $level.removeClass('pw-medium');
            $level.removeClass('pw-strong');
            $level.addClass(' pw-medium');
            //密码为七位及以上并且字母、数字、特殊字符三项中有两项，强度是中等
        }
        else {
            $level.removeClass('pw-weak');
            $level.removeClass('pw-medium');
            $level.removeClass('pw-strong');
            $level.addClass('pw-weak');
            //如果密码为6为及以下，就算字母、数字、特殊字符三项都包括，强度也是弱的
        }
        return true;
    });
    //初始化控件
    var form = $("#defaultForm").form();
    $('#defaultForm').formValidation({
        trigger: "blur",
        icon: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Param: {
                validators: {
                    remote: {
                        headers: {
                            'Authorization': UtilGetAuthorizationLocalStorage()
                        },
                        url: Language.common.apiurl + '/User/CheckOldPassword',
                        message: '旧密码不正确'
                    }
                }
            }
        }
    }).on('success.form.fv', function (e) {
        var data = form.getFormSimpleData();
        data["OldPassword"] = data.Param;
        UtilAjaxPostWait("/Console/Main/SaveChangePassword", data, function (data) {
            if (DialogAjaxResult(data)) { }
            return false;
        });
        return false;
    });
}

//初始化数据
function initData() {
    $("#name-code").val(UtilGetLocalStorage("UserName") + "(" + UtilGetLocalStorage("Code") + ")");
}