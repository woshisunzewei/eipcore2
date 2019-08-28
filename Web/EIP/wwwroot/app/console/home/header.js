$(function () {
    $("#user-name").html(UtilGetLocalStorage("UserName"));
    $("#user-organizationName").html(UtilGetLocalStorage("OrganizationName") + "-" + UtilGetLocalStorage("UserName"));

    $("#changePwd").click(function () {
        $.get($(this).attr("data-href"), {}, function (res) {
            $('#tab-page-content').html(res);
        });
    });

    $("#longinOut").click(function () {
        BootstrapDialog.show({
            size: BootstrapDialog.SIZE_SMALL,
            message: function () {
                var $message = $('<div style="margin:10px;font-size:16px"> 是否退出系统 ? </div>');
                return $message;
            },
         
            title: "退出提示",
            buttons: [
                {
                    icon: 'fa fa-check',
                    label: '确定',
                    title: '确定',
                    cssClass: 'btn-success',
                    action: function (dialogItself) {
                        dialogItself.close();
                        loginOut();
                    }
                }, {
                    icon: 'fa fa-times',
                    label: '取消',
                    title: '取消',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
    });
});

function loginOut() {
    //清空
    localStorage.clear();
    //请求后台并记录日志
    window.location.href = "/Account/Login";
}