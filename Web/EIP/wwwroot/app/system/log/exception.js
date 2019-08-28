var options = {
    url: {
        get: Language.common.apiurl + "/Log/GetPagingExceptionLog",
        delete: "/Log/DeleteExceptionLogById"
    },
    table: {
        name: "list",
        primaryKey: "ExceptionId"
    }
};

var $grid,
    tableObj;

$(function () {
    initGird();
    initEvent();
});

//初始化事件
function initEvent() {
    $('#createTime').daterangepicker(
        {
            maxDate: moment(), //最大时间   
            dateLimit: {
                days: 30
            }, //起止时间的最大间隔  
            showDropdowns: true,
            showWeekNumbers: false, //是否显示第几周  
            timePicker: true, //是否显示小时和分钟  
            timePickerIncrement: 15, //时间的增量，单位为分钟  
            timePicker12Hour: false, //是否使用12小时制来显示时间  
            ranges: {
                '最近1小时': [moment().subtract('hours', 1), moment()],
                '今日': [moment().startOf('day'), moment()],
                '昨日': [moment().subtract('days', 1).startOf('day'), moment().subtract('days', 1).endOf('day')],
                '最近7日': [moment().subtract('days', 6), moment()],
                '最近30日': [moment().subtract('days', 29), moment()]
            },
            opens: 'left', //日期选择框的弹出位置  
            buttonClasses: ['btn btn-default'],
            applyClass: 'btn-flat btn-sm eip-btn',
            cancelClass: 'btn-flat btn-sm',
            format: 'YYYY-MM-DD HH:mm:ss', //控件中from和to 显示的日期格式  
            locale: {
                separator: ' 至 ',
                format: "YYYY-MM-DD HH:mm:ss",
                applyLabel: '确定',
                cancelLabel: '清除',  
                fromLabel: '起始时间',
                toLabel: '结束时间',
                customRangeLabel: '自定义',
                daysOfWeek: ['日', '一', '二', '三', '四', '五', '六'],
                monthNames: [
                    '一月', '二月', '三月', '四月', '五月', '六月',
                    '七月', '八月', '九月', '十月', '十一月', '十二月'
                ],
                firstDay: 1
            }
        });
    $('#createTime').val("");
    $('#createTime').on('cancel.daterangepicker', function (ev, picker) {
        $('#createTime').val("");
    });  
    $("#search").click(function () {
        getGridData();
    });
}

//获取表格数据
function getGridData() {
    $grid.reload({
        where: {
            name: $("#name").val(),
            code: $("#code").val(),
            createTime: $("#createTime").val()
        },
        page: {
            curr: 1
        }
    });
}

//初始化表格
function initGird() {
    layui.use('table', function () {
        tableObj = layui.table;
        //方法级渲染
        $grid = tableObj.render({
            elem: '#' + options.table.name,
            size: "sm",
            method: 'post',
            url: options.url.get,
            cols: [[
                { type: "checkbox", fixed: 'left', width: 30 },
                { title: '序号', type: "numbers", fixed: 'left' },
                { field: 'CreateTime', title: "错误时间", width: 120, align: "center", fixed: 'left' },
                { field: 'Message', title: "错误信息", width: 350, fixed: 'left' },
                { field: 'RequestUrl', title: "请求Url", width: 250 },
                { field: 'StackTrace', title: '堆栈信息', width: 250, sort: true },
                { field: 'InnerException', title: '内部信息', width: 100, sort: true },
                { field: 'CreateUserCode', title: '登录名', width: 150, sort: true },
                { field: 'CreateUserName', title: '名称', width: 100, sort: true },
                { field: 'HttpMethod', title: "请求方式", width: 150 },
                { field: 'RemoteIp', title: "客户端IP", width: 80 },
                { field: 'RemoteIpAddress', title: "归属地址", width: 120, align: "center" },
                { field: 'RequestData', title: "请求数据", width: 150 }
            ]],
            limit: 50,
            limits: [50, 100, 200, 500],
            even: true,
            height: Language.mainHeight + 10,
            page: true
        });
    });
}
