/*====================================
 *说明:通用功能封装(基于各种Jquery插件)
 *创建日期:2014/10/14
 *创建人员:孙泽伟
====================================*/


//页面加载完成时
$(function () {
    //检测IE
    if ("undefined" == typeof (document.body.style.maxHeight)) {
        window.location.href = "/Explain/IeUpdate";
    }
});
/*=====================对弹出对话框封装=====================*/
var dialog = null;
/**
 * 作用:在顶层窗体中弹出iframe窗体
 * 参数:url[页面地址],title[显示标题],lock[是否锁屏],height[高度],width[宽度]
 */
function DialogOpen(url, title, width, param) {
    dialog = BootstrapDialog.show({
        draggable: true,
        width: width,
        title: title,
        message: $('<div></div>').load(url, param),
        onshow: function (dialogRef) {

        },
        onshown: function (dialogRef) {

        },
        onhide: function (dialogRef) {

        },
        onhidden: function (dialogRef) {

        }
    });
};

/**
 * 作用:弹出自定义内容信息
 * 参数:content[内容]
 */
function DialogContent(content) {
    BootstrapDialog.show({
        size: BootstrapDialog.SIZE_SMALL,
        message: content,
        buttons: [{
            icon: 'fa fa-times',
            label: '关闭',
            title: 'Mouse over Button 3',
            cssClass: 'btn-danger'
        }]
    });
}

/**
 * 作用:提示是否确认删除
 * 参数:msg[删除提示信息],okfunction[点击确定触发事件]
 */
function DialogConfirm(msg, okfunction) {
    BootstrapDialog.show({
        size: BootstrapDialog.SIZE_SMALL,
        message: "<div style='padding:15px'>" + msg + "</div>",
        title: "操作提示",
        buttons: [{
            icon: 'fa fa-check',
            label: '确定',
            title: '确定',
            cssClass: 'btn-success',
            action: function (dialogItself) {
                dialogItself.close();
                var $button = this; // 'this' here is a jQuery object that wrapping the <button> DOM element.
                $button.disable();
                $button.spin();
                okfunction();
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
};
/**
 * 作用:提示是否确认删除
 * 参数:msg[删除提示信息],okfunction[点击确定触发事件],nofunction取消事件
 */
function DialogConfirmYesNo(msg, okfunction, nofunction) {
    BootstrapDialog.show({
        size: BootstrapDialog.SIZE_SMALL,
        message: msg,
        title: "操作提示",
        buttons: [{
            icon: 'fa fa-check',
            label: '确定',
            title: '确定',
            cssClass: 'btn-success',
            action: function (dialogItself) {
                dialogItself.close();
                var $button = this; // 'this' here is a jQuery object that wrapping the <button> DOM element.
                $button.disable();
                $button.spin();
                okfunction();
            }
        }, {
            icon: 'fa fa-times',
            label: '取消',
            title: '取消',
            cssClass: 'btn-danger',
            action: function (dialogItself) {
                dialogItself.close();
                nofunction();
            }
        }
        ]
    });
};

var ResultSign = {}; //结果类型
ResultSign.Sucess = "success";
ResultSign.Error = "error";
ResultSign.Warn = "warning";
ResultSign.Wait = "wait";

/**
 * 作用:ajax提交后显示到界面的消息
 * 参数:data返回的结果数据
 */
function DialogAjaxResult(data) {
    var result = true;
    var resultType = data.ResultSign;
    var resultMsg = data.Message;
    switch (resultType) {
        case 0:
            DialogTipsMsgSuccess(resultMsg, 1500);
            break;
        case 1:
            DialogTipsMsgWarn(resultMsg, 2000);
            result = false;
            break;
        case 2:
            DialogTipsMsgError(resultMsg, 3000);
            result = false;
            break;
        default:
    }
    return result;
};

/**
 * 作用:成功提示
 * 参数:msg(消息),time(显示时间)
 */
function DialogTipsMsgSuccess(msg, time) {
    DialogShowTopMsg(msg, time, ResultSign.Sucess);
};

/**
 * 作用:错误提示
 * 参数:msg(消息),time(显示时间)
 */
function DialogTipsMsgError(msg, time) {
    DialogShowTopMsg(msg, time, ResultSign.Error);
};

/**
 * 作用:警告
 * 参数:msg(消息),time(显示时间)
 */
function DialogTipsMsgWarn(msg, time) {
    DialogShowTopMsg(msg, time, ResultSign.Warn);
};

/**
 * 作用:等待提示
 * 参数:msg(消息)
 */
function DialogTipsMsgWait(msg) {
    DialogShowTopMsg(msg, 999999999, ResultSign.Wait);
};

/**
 * 作用:顶部显示信息
 * 参数:msg(消息),time(显示时间),type(类型:success,warning,error)
 */
function DialogShowTopMsg(msg, time, type) {
    toastr.options =
        {
            "closeButton": true,
            "debug": true,
            "progressBar": true,
            "preventDuplicates": false,
            "positionClass": "toast-top-center",
            "showDuration": "400",
            "hideDuration": "1000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "timeOut": time,
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    switch (type) {
        case "success":
            toastr.success(msg);
            break;
        case "warning":
            toastr.warning(msg);
            break;
        case "error":
            toastr.options.timeOut = time;
            toastr.error(msg);
            break;
        default:
    }
}
/**
     * 消息弹框
     * @param msg
     * @param wait 等待时间（毫秒）
     */
$.alertMsg = function (title, msg, wait, type) {
    wait = wait ? wait : 2800;
    var dialog = BootstrapDialog.show({
        size: BootstrapDialog.SIZE_SMALL,
        title: title,
        message: msg,
        type: type,
        draggable: true
    });
    setTimeout(function () {
        dialog.close();
    }, wait);
};
/*=====================处理Chosen(基于chosen.jquery.min.js)=====================*/
/**
 * 作用:Chosen下拉改变事件
 * 参数:select[select对象],callBack[改变后事件]
 */
function ChosenChange(select, callBack) {
    $(select).chosen().change(function () {
        $(select).trigger("liszt:updated");
        callBack();
    });
};

/**
 * 作用:初始化Chosen单选值
 * 参数:chosen[chosen对象],value[值]
 */
function ChosenInit(select, value) {
    $(select).attr("value", value);
    $(select).trigger("liszt:updated");
};

/**
 * 作用:初始化Chosen多选值
 * 参数:chosen[chosen对象],value[值]
 */
function ChosenMultInit(select, values) {
    var arr = values.split(",");
    var length = arr.length;
    var value = "";
    for (var i = 0; i < length; i++) {
        value = arr[i];
        $(select + " [value='" + value + "']").attr("selected", "selected");
    }
    $(select).trigger("liszt:updated");
};

/**
 * 作用:获取Chosen选中的Value值
 * 参数:chosen[chosen对象]
 * 返回:选中的值
 */
function ChosenGetValue(select) {
    return $(select).val();
};
/**
 * 作用:获取Chosen选中的Text值(没有用,分隔)
 * 参数:chosen[chosen对象]
 * 返回:选中的值
 */
function ChosenGetText(select) {
    return $(select + " option:selected").text();
};
/*=====================验证方法=====================*/

/**
 * 作用:验证form表单元素准确性
 * 参数:参数:formId(form中id名称),btnSubmit(点击那个按钮触发form提交),completeMethod(验证通过方法),showAllError(是否显示所有错误信息)
 *msg：提示信息;
 *                o:{obj:*,type:*,curform:*}
 *                obj指向的是当前验证的表单元素（或表单对象）；
 *               type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态；
 *                curform为当前form对象;
 *                cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）；
 *                //全部验证通过提交表单时o.obj为该表单对象;
 */
function Validform(formId, btnSubmit, completeMethod, showAllError) {
    $("#" + formId + "").Validform({
        tiptype: function (msg, o, cssctl) {
            var $valiObj = $("#" + o.obj.attr("id"));
            if (!o.obj.is("form")) {
                if (o.type === 2) {
                    $valiObj.qtip("destroy", true).removeClass("input-error");
                    return true;
                }
                if (o.type === 3) {
                    $valiObj.qtip({
                        content: {
                            text: msg
                        },
                        position: {
                            target: "mouse", // 跟随鼠标指针
                            effect: false, // 关闭效果
                            viewport: $(window),
                            adjust: { x: 10, y: 10 } // tip位置偏移，防止遮住鼠标指针
                        },
                        style: {
                            classes: "qtip-red"
                        }
                    });
                    $valiObj.addClass("input-error");
                }
            }
            return true;
        },
        btnSubmit: "#" + btnSubmit + "",
        showAllError: showAllError,
        ignore: ".ng-hide",//不验证的元素
        callback: function () {
            completeMethod();
            return false;
        }
    });
};

/**
 * 作用:将当前表单中带有class为m的元素前加必选红色提示
 */
function ValidformNeed() {
    $(".m", this.form).prepend("<font face='宋体'>*</font>"); // 渲染必选
}; /*=====================操作快捷按键有关操作封装 开始(基于jquery.hotkeys.js)=====================*/

/**
 * 作用:将当前表单中带有class为m的元素前加必选红色提示
 */
function EditFocus() {
    $("input:first", this.form).focus(); // 渲染必选
};
/*=====================操作快捷按键有关操作封装 开始(基于jquery.hotkeys.js)=====================*/

/**
 * 作用:按键快捷菜单方式
 * 参数:key(按钮或者组合键),callBack(回调方法名称)
 */
function HotKey(key, callBack) {
    $.hotkeys.add(key, function () {
        callBack();
    });
};

/*=====================常用工具类=====================*/
/**
 * 作用:根据传入的键值获取浏览器地址中参数
 * 参数:参数名称键
 */
function UtilGetUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var result = window.location.search.substr(1).match(reg);
    return result ? decodeURIComponent(result[2]) : "";
};

/**
 * 作用:重新加载该页面
 * 参数:无
 */
function UtilReloadPage() {
    window.location.reload();
}

/**
 * 作用:返回一个GUID值
 * 参数:无
 */
function UtilNewGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
};
/**
 * 作用:获取当前窗体高度宽度
  */
function UtilWindowHeightWidth() {
    var winW, winH;
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
    return { WinW: winW, WinH: winH };
};

/**
 * 作用:编码地址
  */
function UtilDecodeUri(uri) {
    if (!uri || $.trim(uri).length == 0) {
        return "";
    }
    try {
        return decodeURI(uri);
    }
    catch (e) {
        return uri;
    }
}

/**
 * 作用:获取LocalStorage值
  */
function UtilGetLocalStorage(name) {
    var storage = window.localStorage;
    return storage[name];
}

/**
 * 作用:获取LocalStorage值
  */
function UtilGetAuthorizationLocalStorage() {
    var storage = window.localStorage;
    var authorization = storage["Authorization"];
    if (authorization == null || authorization == "Bearer ") {
        DialogTipsMsgError(Language.common.noAuthorization);
        window.location.href = "/Account/Login";
        return false;
    } else {
        return authorization;
    }
}


/**
 * 作用:Jquery的Post ajax提交,具有等待提示
 * 参数:url(提交地址),postData(提交数据),success(成功后触发方法)
 */
function UtilAjaxPostWait(url, postData, success) {
    var dialogWait = new BootstrapDialog({
        size: BootstrapDialog.SIZE_SMALL,
        message: function () {
            var $message = $('<div><i class="icon fa fa-spinner fa-spin" style="margin-right:10px"></i>' + Language.common.wait + '</div>');
            return $message;
        },
        closable: false
    });
    dialogWait.realize();
    dialogWait.getModalHeader().hide();
    dialogWait.getModalFooter().hide();
    dialogWait.getModalBody().addClass("waitbody");
    dialogWait.getModalBody().css('background-size', '18px 18px');
    dialogWait.getModalBody().css('padding', '15px 10px 17px 15px');
    dialogWait.getModalBody().css('color', '#ffffff');
    dialogWait.open();
    $.ajax({
        url: Language.common.apiurl + url, // 跳转到地址    
        data: postData,
        headers: {
            'Authorization': UtilGetAuthorizationLocalStorage()
        },
        type: "post",
        async: true,
        dataType: "json",
        success: function (data) {
            dialogWait.close();
            success(data);
        },
        error: function (e) {
            if (e.status == 401) {
                DialogTipsMsgError(e.responseJSON.Message + " 请重新登录授权", 1000);
                setTimeout('window.location.href = "/Account/Login"', 1000);
            } else {
                DialogTipsMsgError(Language.common.ajaxposterror, 1000);
            }
            dialogWait.close();
        }
    });
};

/**
 * 作用:Jquery的Post ajax提交,具有等待提示
 * 参数:url(提交地址),postData(提交数据),success(成功后触发方法)
 */
function UtilAjaxPostWithTypeWait(url, postData, type, success) {
    var dialog = new BootstrapDialog({
        size: BootstrapDialog.SIZE_SMALL,
        message: function () {
            var $message = $('<div>' + Language.common.wait + '</div>');
            return $message;
        },
        closable: false
    });
    dialog.realize();
    dialog.getModalHeader().hide();
    dialog.getModalFooter().hide();
    dialog.getModalBody().css("background", "#0088cc url('/Contents/images/wait.gif') no-repeat 15px center");
    dialog.getModalBody().css('background-size', '18px 18px');
    dialog.getModalBody().css('padding', '15px 10px 17px 50px');
    dialog.getModalBody().css('color', '#ffffff');
    dialog.open();
    $.ajax({
        url: Language.common.apiurl + url, // 跳转到地址    
        data: postData,
        headers: {
            'Authorization': UtilGetAuthorizationLocalStorage()
        },
        type: "post",
        async: false,
        dataType: "json",
        success: function (data) {
            dialog.close();
            success(data);
        },
        error: function (e) {
            if (e.status == 401) {
                DialogTipsMsgError(e.responseJSON.Message + " 请重新登录授权", 1000);
                setTimeout('window.location.href = "/Account/Login"', 1000);
            } else {
                DialogTipsMsgError(Language.common.ajaxposterror, 1000);
            }
            dialog.close();
        }
    });
};

/**
 * 作用:Jquery的Post ajax提交,无等待提示
 * 参数:url(提交地址),postData(提交数据),success(成功后触发方法)
 */
function UtilAjaxPost(url, postData, success) {
    $.ajax({
        url: Language.common.apiurl + url, // 跳转到地址    
        data: postData,
        type: "post",
        headers: {
            'Authorization': UtilGetAuthorizationLocalStorage()
        },
        async: false,
        dataType: "json",
        success: success,
        error: function (e) {
            if (e.status == 401) {
                DialogTipsMsgError(e.responseJSON.Message + " 请重新登录授权", 1000);
                setTimeout('window.location.href = "/Account/Login"', 1000);
            } else {
                DialogTipsMsgError(Language.common.ajaxposterror, 1000);
            }
            dialog.close();
        }
    });
};

/**
 * 作用:Jquery的Post ajax提交,无等待提示
 * 参数:url(提交地址),postData(提交数据),success(成功后触发方法)
 */
function UtilAjaxPostAsync(url, postData, success) {
    $.ajax({
        url: Language.common.apiurl + url, // 跳转到地址    
        data: postData,
        type: "post",
        headers: {
            'Authorization': UtilGetAuthorizationLocalStorage()
        },
        async: true,
        dataType: "json",
        success: success,
        error: function (e) {
            if (e.status == 401) {
                DialogTipsMsgError(e.responseJSON.Message + " 请重新登录授权", 1000);
                setTimeout('window.location.href = "/Account/Login"', 1000);
            } else {
                DialogTipsMsgError(Language.common.ajaxposterror, 1000);
            }
            dialog.close();
        }
    });
};

/**
 * 作用:Jquery的Get ajax提交
 * 参数:url(提交地址),postData(提交数据),success(成功后触发方法)
 */
function UtilAjaxGet(url, postData, success) {
    $.ajax({
        url: url, // 跳转到地址    
        data: postData,
        type: "get",
        headers: {
            'Authorization': UtilGetAuthorizationLocalStorage()
        },
        async: false,
        dataType: "json",
        success: success,
        error: function (e) {
            if (e.status == 401) {
                DialogTipsMsgError(e.responseJSON.Message + " 请重新登录授权", 1000);
                setTimeout('window.location.href = "/Account/Login"', 1000);
            } else {
                DialogTipsMsgError(Language.common.ajaxposterror, 1000);
            }
            dialog.close();
        }
    });
};

/**
 * 作用:Jquery的Get ajax提交
 * 参数:url(提交地址),postData(提交数据),success(成功后触发方法)
 */
function UtilAjaxGetAsync(url, postData, success) {
    $.ajax({
        url: url, // 跳转到地址    
        data: postData,
        type: "get",
        headers: {
            'Authorization': UtilGetAuthorizationLocalStorage()
        },
        async: true,
        dataType: "json",
        success: success,
        error: function (e) {
            if (e.status == 401) {
                DialogTipsMsgError(e.responseJSON.Message + " 请重新登录授权", 1000);
                setTimeout('window.location.href = "/Account/Login"', 1000);
            } else {
                DialogTipsMsgError(Language.common.ajaxposterror, 1000);
            }
            dialog.close();
        }
    });
};

/**
 * 作用:获取提示框标记
 */
function UtilEditIsdialogClose() {
    var isDialogClose = $("#IsdialogClose");
    if (isDialogClose.length === 0) {
        DialogTipsMsgWarn("未找到元素id为IsdialogClose", 2000);
        return;
    }
    return isDialogClose[0].checked == false;
}

/**
 * 作用:重置表单
 * 参数:formId(需要重置表单)
 */
function UtilFormReset(formId) {
    document.getElementById("" + formId + "").reset();
};

/**
 * 作用:表单元素不可用
 * 参数:disabledId(需要隐藏的元素Id)
 */
function UtilFormDisabled(disabledId) {
    document.getElementById("" + disabledId + "").disabled = true;
};

/**
 * 作用:时间格式化
 * 参数:格式化参数
 */
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(), //day
        "h+": this.getHours(), //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
        "S": this.getMilliseconds() //millisecond
    };
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1,
            (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1,
                RegExp.$1.length == 1 ? o[k] :
                    ("00" + o[k]).substr(("" + o[k]).length));
    return format;
};

/**
 * 作用:两端去空格函数
 * 参数
 */
String.prototype.trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); }
function getPosition(obj) {
    var top = 0;
    var left = 0;
    var width = obj.offsetWidth;
    var height = obj.offsetHeight;
    while (obj.offsetParent) {
        top += obj.offsetTop;
        left += obj.offsetLeft;
        obj = obj.offsetParent;
    }
    return { "top": top, "left": left, "width": width, "height": height };
}
/**
 * 作用:类似placeholder功能
 */
function UtilPlaceHolde(elem, word) {
    if (!elem || !word) {
        return;
    }
    var value = $.trim(elem.val());
    (!value || value === word) ? elem.val(word).css('color', '#a9a9a9') : elem.css('color', '#333');
    elem.focus(function () {
        value = $.trim(elem.val());
        (value === word) && elem.val('');
        elem.css('color', '#333');
    }).blur(function () {
        value = $.trim(elem.val());
        (!value || value === word) ? elem.val(word).css('color', '#a9a9a9') : elem.css('color', '#333');
    });
};

/**
* 作用:表单元素不可用
* 参数:disabledId(需要焦点的元素Id)
*/
function UtilFocus(disabledId) {
    document.getElementById("" + disabledId + "").focus();
};

/**
* 作用:获取下拉框值
* 参数:id
*/
function UtilGetDropdownListText(id) {
    return $("#" + id + " option:selected").text();
}

/**
* 作用:判断是否选中复选框
* 参数:id
*/
function UtilCheckboxIsCheck(id) {
    return $("#" + id + "").is(':checked');
}

/**
* 作用:判断是否选中复选框
* 参数:obj(checkbox对象)
*/
function UtilCheckboxIsCheckByObj(obj) {
    return obj.is(':checked');
}

//格式化日期
function UtilFormatDate(date) {
    var myyear = date.getFullYear();
    var mymonth = date.getMonth();
    var myweekday = date.getDate();

    if (mymonth < 10) {
        mymonth = "0" + mymonth;
    }
    if (myweekday < 10) {
        myweekday = "0" + myweekday;
    }
    return (myyear + "-" + mymonth + "-" + myweekday);
}

//获得某月的天数 
function UtilGetMonthDays(nowyear, myMonth) {
    var monthStartDate = new Date(nowyear, myMonth, 1);
    var monthEndDate = new Date(nowyear, myMonth + 1, 1);
    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);//格式转换
    return days;
}

//伸缩
function UtilReloadSize() {
    $("[data-layout='sidebar-collapse']").click();
    $('#resetSize').attr("data-original-title", "固定");
}
/*=====================zTree=====================*/

/**
 * 作用:选中树节点特定项
 * 参数:$ztree:树对象,id需要选中的值
 */
function ZtreeAssignCheck($ztree, id) {
    $ztree.checkNode($ztree.getNodeByParam("id", id, null), true, true);
};
/**
 * 作用:获取选中树的数据
 * 参数:$ztree:树对象
 */
function ZtreeGetSelectedNodes($ztree) {
    return $ztree.getSelectedNodes();
};

/**
 * 作用:获取选中树的数据
 * 参数:$ztree:树对象
 */
function ZtreeGetCheckedNodes($ztree) {
    return $ztree.getCheckedNodes(true);
};
/*=====================ueditor=====================*/

/**
 * 作用:初始化Ueditor选项卡
 * 参数:无
 */
function UeditorInitTab() {
    var tabs = $G("tabhead").children;
    for (var i = 0; i < tabs.length; i++) {
        domUtils.on(tabs[i], "click", function (e) {
            var target = e.target || e.srcElement;
            UeditorSetTabFocus(target.getAttribute("data-content-id"));
        });
    }
    if (tabs.length > 0) {
        UeditorSetTabFocus(tabs[0].getAttribute("data-content-id"));
    }
}; /**
 * 作用:选项卡选中
 * 参数:id:选中选项卡元素
 */
function UeditorSetTabFocus(id) {
    if (!id) return;
    var i, bodyId, tabs = $G("tabhead").children;
    for (i = 0; i < tabs.length; i++) {
        bodyId = tabs[i].getAttribute("data-content-id");
        if (bodyId == id) {
            domUtils.addClass(tabs[i], "focus");
            domUtils.addClass($G(bodyId), "focus");
        } else {
            domUtils.removeClasses(tabs[i], "focus");
            domUtils.removeClasses($G(bodyId), "focus");
        }
    }
};

/*=====================数据库=====================*/

/**
 * 作用:得到一连接的所有表
 * 参数:appId:应用Id
 */
function DataBaseGetTables(appId) {
    var tables = [];
    $.ajax({
        url: "/Common/Chosen/GetTableByAppId",
        data: { appId: appId },
        dataType: "json",
        type: "post",
        async: false,
        cache: false,
        success: function (json) {
            for (var i = 0; i < json.length; i++) {
                tables.push(json[i]);
            }
        }
    });
    return tables;
};

/**
 * 作用:得到一个表所有字段
 * 参数:appId:应用Id,table:表名
 */
function DataBaseGetFields(appId, table) {
    var fields = [];
    $.ajax({
        url: "/Common/Chosen/GetColumnByTable",
        data: { appId: appId, table: table },
        dataType: "json",
        type: "post",
        async: false,
        cache: false,
        success: function (json) {
            for (var i = 0; i < json.length; i++) {
                fields.push(json[i]);
            }
        }
    });
    return fields;
};

/**
 * 作用:得到表下拉选择项
 * 参数:connid:应用Id,table:表名
 */
function DataBaseGetTableOps(connid, table) {
    var options = "<option value=\"\"></option>";
    var tableds = DataBaseGetTables(connid);
    for (var i = 0; i < tableds.length; i++) {
        options += "<option value=\"" + tableds[i].Name + "\" " + (tableds[i].Name == table ? "selected=\"selected\"" : "") + ">" + tableds[i].Name + "</option>";
    }
    return options;
};

/**
 * 作用:得到字段下拉选择项
 * 参数:connid:应用Id,table:表名,field:字段名
 */
function DataBaseGetFieldsOps(connid, table, field) {
    var options = "<option value=\"\"></option>";
    var fields = DataBaseGetFields(connid, table);
    for (var i = 0; i < fields.length; i++) {
        options += "<option value=\"" + fields[i].Name + "\" " + (fields[i].Name == field ? "selected=\"selected\"" : "") + ">" + fields[i].Name + (fields[i].Description ? "(" + fields[i].Description + ")" : "") + "</option>";
    }
    return options;
};

/**
 * 作用:初始化绑定字段选择项
 * 参数:connid:应用Id,table:表名,field:字段名
 */
function DataBaseBiddingFileds(attJson, textid, $bindfiled) {
    if (attJson.dbconn && attJson.dbtable) {
        var fieldvalue = "";
        if (textid) {
            var textidarray = textid.split(".");
            if (textidarray.length === 2) {
                fieldvalue = textidarray[1];
            }
        }
        var opts = DataBaseGetFieldsOps(attJson.dbconn, attJson.dbtable, fieldvalue);
        $bindfiled.html(opts);
    }
};



/**
 * 作用:给指定的Grid重新赋予高度和宽度
 * 参数:$grid:列表对象,width:需要减去的宽度,height:需要减去的高度
 */
function GridSetWindowWidthAndHeight($grid, width, height) {
    var size = UtilWindowHeightWidth();
    $grid.jqGrid("setGridWidth", size.WinW - width).jqGrid("setGridHeight", size.WinH - height);
};

/**
 * 作用:给指定的Grid重新赋予高度和宽度
 * 参数:$grid:列表对象,width:需要减去的宽度,height:需要减去的高度
 */
function GridSetWidthAndHeight($grid, width, height) {
    $grid.jqGrid("setGridWidth", width).jqGrid("setGridHeight", height);
};

/**
 * 作用:当界面加载完毕时若无数据则提示
 * 参数:$grid:列表对象
 */
function GridSetNoRecordsMsg($grid, msg) {
    $(".norecords").html("");
    var reRecords = $grid.getGridParam("records");
    if (reRecords === 0 || reRecords == null) {
        if (msg != "") {
            $("#list").parent().append("<div class=\"norecords\">" + msg + "</div>");
        }
        $(".norecords").show();
    } else {
        $(".norecords").hide();
    }
};

/**
 * 作用:重新加载Grid数据:一次性加载
 * 参数:$grid:列表对象,data:Json数据
 */
function GridReloadLoadOnceData($grid, data) {
    $(".norecords").hide();
    $grid.jqGrid("clearGridData", true);
    $grid.jqGrid("setGridParam", {
        datatype: "local",
        data: eval(data) //服务器返回的json格式，此处转换后给数据源赋值
    }).trigger("reloadGrid");
};

/**
 * 作用:重新加载Grid数据:分页
 * 参数:$grid:列表对象,data:Json数据
 */
function GridReloadPagingData($grid, param) {
    $(".norecords").hide();
    $grid.jqGrid("clearGridData", true);
    $grid.jqGrid("setGridParam", param).trigger("reloadGrid");
};

/**
 * 作用:获取选择一行的id，如果你选择多行，那下面的id是最后选择的行的id
 * 参数:$grid:列表对象
 */
function GridGetId($grid) {
    return $grid.jqGrid("getGridParam", "selrow");
}; /**
 * 作用:获取选择多行的id，那这些id便封装成一个id数组
 * 参数:$grid:列表对象
 */
function GridGetIds($grid) {
    return $grid.jqGrid("getGridParam", "selarrrow");
}; /**
 * 作用:获取选择的行的数据
 * 参数:$grid:列表对象
 */
function GridGetSingSelectData($grid) {
    return $grid.jqGrid("getRowData", $grid.jqGrid("getGridParam", "selrow"));
}; /**
 * 作用:指定某行选中
 * 参数:$grid:列表对象,rowId需要选中的行
 */
function GridSetSelection($grid, rowId) {
    $grid.jqGrid("setSelection", rowId);
}; /**
 * 作用:指定某行选中
 * 参数:$grid:列表对象,rowId需要获取的行数
 */
function GridGetRowData($grid, rowId) {
    return $grid.jqGrid("getRowData", rowId);
}; /**
 * 作用:表格是否选中
 * 参数:$grid:列表对象,method选中后执行方法
 */
function GridIsSelect($grid, method) {
    if (GridGetId($grid) == null) {
        DialogTipsMsgWarn("请选择需要操作的数据", 1000);
    } else {
        method();
    }
};

/**
* 作用:表格所有选中值Id,以逗号分割:如删除使用
* 参数:$grid:列表对象,key:主键名称
*/
function GridSelectIds($grid, key) {
    var ids = "";
    var rowData = $grid.jqGrid("getGridParam", "selarrrow");
    if (rowData.length) {
        for (var i = 0; i < rowData.length; i++) {
            var name = $grid.jqGrid('getCell', rowData[i], key); //name是colModel中的一属性
            ids += name + ",";
        }
    }
    return ids.substring(0, ids.length - 1);;
}
/**
* 作用:统计合计列
* 参数:$grid:列表对象,titleCol:合计列标题,summaryCols需要合计的列
* eg:GridSummary(grid,"rn", ["BudgetMoney", "ActualValue"]);
*/
function GridSummary(grid, titleCol, summaryCols) {
    var footerData = {};
    titleCol && (footerData[titleCol] = "合计");
    $.each(summaryCols,
        function (index, value) {
            footerData[value] = grid.getCol(value, false, "sum");
        });
    grid.footerData("set", footerData);
};

//laytable

/**
* 作用:表格是否选中
* 参数:$grid:列表对象,method选中后执行方法
*/
function GridIsSelectLayTable($grid, id, method) {
    if (GridGetIdLayTable($grid, id).data.length == 0) {
        DialogTipsMsgWarn("请选择需要操作的数据", 1000);
    } else {
        method();
    }
};

/**
 * 作用:获取选择一行的id，如果你选择多行，那下面的id是最后选择的行的id
 * 参数:$grid:列表对象
 */
function GridGetIdLayTable($grid, id) {
    return $grid.checkStatus(id);
};

/**
 * 作用:获取选择的行的数据
 * 参数:$grid:列表对象
 */
function GridGetSingSelectDataLayTable($grid, id) {
    return $grid.checkStatus(id).data[0];
};

/**
* 作用:表格所有选中值Id,以逗号分割:如删除使用
* 参数:$grid:列表对象,key:主键名称
*/
function GridSelectIdsLayTable($grid, id, key) {
    var ids = "";
    var rowData = $grid.checkStatus(id).data;
    if (rowData.length) {
        for (var i = 0; i < rowData.length; i++) {
            var name = rowData[i][key];
            ids += name + ",";
        }
    }
    return ids.substring(0, ids.length - 1);;
}

//获取表单条件:需在查询框前指定查询类型(模糊、准确等)
function getFilters($selectbox) {
    var rules = "";
    $('.filter', $selectbox).each(function () {
        var searchField = $(this).attr("search-field"),
            searchOper = $(this).attr("search-type"),
            searchString = $(this).val();
        if ((searchField && searchOper && searchString)) {
            rules += (',{"field":"' + searchField + '","op":"' + searchOper + '","data":"' + searchString + '"}');
        }
    });
    rules && (rules = rules.slice(1));
    var filtersStr = '{"groupOp":"AND","rules":[' + rules + ']}';
    return filtersStr;
}