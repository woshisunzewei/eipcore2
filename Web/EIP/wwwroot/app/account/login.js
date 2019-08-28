var currentindex = 1,
    timerID,
    $submitButton = $("#submit"),
    $submitMsg = $("#submitMsg"),
    $code = $("#userCode"),
    $pwd = $("#userPwd"),
    isVerify = false;
method = {
    //绑定事件
    bindEvent: function () {
        $submitButton.click(function () {
            method.submit();
        });
        $("#userCode,#userPwd,#verify").bind("keypress", function (event) {
            if (event.keyCode === "13") {
                method.submit();
            }
        });
    },

    submit: function () {
        var loading = $submitButton.button('loading');
        var code = $code.val();
        var pwd = $pwd.val();
        if (code === "") {
            $code.focus();
            loading.button('reset');
            loading.dequeue();
            method.formMessage("请输入用户名", 'warning', 'warning');
            return;
        }
        if (pwd === "") {
            $pwd.focus();
            loading.button('reset');
            loading.dequeue();
            method.formMessage("请输入密码", 'warning', 'warning');
            return;
        }
        if (!isVerify) {
            loading.button('reset');
            loading.dequeue();
            method.formMessage("请按住滑块，拖动到最右边", 'warning', 'warning');
            return;
        }
        method.formMessage("正在登录中,请稍等...", 'success', 'spinner fa-spin');
        $.post(Language.common.apiurl + "/Account/Login",
            {
                code: code,
                pwd: pwd,
                remberme: $("#remember").prop("checked"),
                returnUrl: $("#returnHidden").val()
            },
            function (data) {
                var resultType = data.ResultSign;
                var resultMsg = data.Message;
                $("#btnSubmit").removeAttr("disabled");
                if (resultType === 2) {
                    loading.button('reset');
                    loading.dequeue();
                    method.formMessage(resultMsg, 'danger', "ban");

                } else {
                    method.formMessage('登录验证成功,正在跳转首页', 'success', 'check');
                    setInterval(method.writeAuthorizationBearerLocalStorage(data), 1000);
                }
            }, "json").success(function () {//成功
            }).error(function () {//失败
                loading.button('reset');
                loading.dequeue();
                method.formMessage("登录失败,请稍后重试", 'danger', "ban");

            }).complete(function () {//完成
            });
    },
    
    //写Bearer
    writeAuthorizationBearerLocalStorage: function (data) {
        var storage = window.localStorage;
        storage["Authorization"] = "Bearer " + data.Data;
        storage["UserName"] = data.UserName;
        storage["OrganizationId"] = data.OrganizationId;
        storage["OrganizationName"] = data.OrganizationName;
        storage["Code"] = data.Code;
        storage["HeadImage"] = (data.HeadImage === "" || data.HeadImage === null) ? "/css/user/avatar.jpg" : data.HeadImage;
        window.location.href = "/";
        return false;
    },

    formMessage: function (msg, type, icon) {
        $submitMsg.show().html('').attr("class", "callout callout-" + type);
        $submitMsg.append('<h5><i class="icon fa fa-' + icon + '"></i>' + msg + ' </h5>');
    }
}

$(document).ready(function () {
    $code.focus();
    method.bindEvent();
    $('input').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '20%'
    });
});

$(document).ajaxStart(function () {
    Pace.restart();
});

var box = document.getElementById('verify_box'),
    xbox = document.getElementById('verify_xbox'),
    element = document.getElementById('btn'),
    b = box.offsetWidth,
    o = element.offsetWidth;

element.ondragstart = function () {
    return false;
};

element.onselectstart = function () {
    return false;
};

element.onmousedown = function (e) {
    var disX = e.clientX - element.offsetLeft;
    document.onmousemove = function (e) {
        var l = e.clientX - disX + o;
        if (l < o) {
            l = o;
        }
        if (l > b) {
            l = b;
        }
        xbox.style.width = l + 'px';
    };
    document.onmouseup = function (e) {
        var l = e.clientX - disX + o;
        if (l < b) {
            l = o;
        } else {
            l = b;
            isVerify = true;
            xbox.innerHTML = '验证通过<div id="btn"><img src="../../../css/login/images/ok.png"/></div>';
        }
        xbox.style.width = l + 'px';
        document.onmousemove = null;
        document.onmouseup = null;
    };
}