var $content = null,
    url;
//方法
var mainSidebarFunction = {
    //高度
    initBaseListHeight: function () {
        var winW=0, winH = 0;
        if (window.innerHeight) { // all except IE
            winW = window.innerWidth;
            winH = window.innerHeight;
        } else if (document.documentElement && document.documentElement.clientHeight) { // IE 6 Strict Mode
            winW = document.documentElement.clientWidth;
            winH = document.documentElement.clientHeight;
        } else if (document.body) { // other
            winW = document.body.clientWidth;
            winH = document.body.clientHeight;
        }
        Language.mainHeight = winH - 168;
        Language.mainWidth = winW;
    },

    //初始化面板
    initDashboard: function () {
        $content.load("/Console/Home/Dashboard", function (responseTxt, statusTxt, xhr) {
            if (statusTxt == "success") {
                //alert("外部内容加载成功！");
            }
            if (statusTxt == "error") {
                if (xhr.status == "404") {
                    $(".sidebar-menu").html("/Common/Error/NotFind");
                }
                alert("Error: " + xhr.status + ": " + xhr.statusText);
            }
        });
    },

    //处理点击菜单事件
    handleSidebarAjaxContent: function () {
        $('.sidebar-menu').on('click', ' li > a.ajaxify', function (e) {
            e.preventDefault();
            var $this = $(this);
            url = $this.attr("href");
            if (url != "#") {
                $content.load(url, function (responseTxt, statusTxt, xhr) {
                    if (statusTxt == "success") {
                        $("ul.treeview-menu li").removeClass("active");
                        $this.parent().addClass("active");
                    }
                    if (statusTxt == "error") {
                        if (xhr.status == "404") {
                            $content.load("/Common/Error/NotFind");
                        }
                    }
                });
            }
        });

        //点击修改头像
        $('#headImage').click(function (e) {
            mainSidebarFunction.headImage();
        });
    },

    //初始化菜单
    initMenu: function () {
        $.ajax({
            url: Language.common.apiurl + "/Home/LoadMenuPermission", // 跳转到地址
            type: "post",
            async: true,
            headers: {
                'Authorization': UtilGetAuthorizationLocalStorage()
            },
            success: function (data) {
                $(".sidebar-menu").html(data);
            },
            error: function (e) {
                if (e.status == 401) {
                    DialogTipsMsgWarn(e.responseJSON.Message + " 请重新登录授权", 1000);
                    setTimeout("mainSidebarFunction.login()", 1000);
                } else {
                    DialogTipsMsgWarn(e.responseJSON.Message, 1000);
                }
            }
        });
    },

    //去登录
    login: function () {
        window.location.href = "/Account/Login";
    },

    //赋予名字和头像
    setNameAndHeadImage: function () {
        $("#user-name-sidebar").html(UtilGetLocalStorage("OrganizationName") + "-" + UtilGetLocalStorage("UserName"));
        $(".img-circle").attr('src', UtilGetLocalStorage("HeadImage"));
    },

    //修改头像
    headImage: function () {
        DialogOpen("/System/User/HeadImage", "修改头像-" + UtilGetLocalStorage("UserName"), 800);
    }
}

$(function () {
    $content = $('#tab-page-content');
    mainSidebarFunction.initDashboard();
    mainSidebarFunction.initMenu();
    mainSidebarFunction.handleSidebarAjaxContent();
    mainSidebarFunction.initBaseListHeight();
    mainSidebarFunction.setNameAndHeadImage();
});

//加载滚动条
$(document).ajaxStart(function () {
    Pace.restart();
});