$(function () {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    $('#calendar').fullCalendar({
        buttonText: {
            today: '今天',
            month: '月视图',
            week: '周视图',
            day: '日视图'
        },
        allDayText: "全天",
        titleFormat: {
            month: 'YYYY年MM月',
            week: "YYYY年MM月DD日",
            day: 'YYYY年MM月DD日 dddd'
        },
        weekends: true,
        monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月",
            "七月", "八月", "九月", "十月", "十一月", "十二月"],
        dayNamesShort: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
        dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
        monthNames: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
        dayNames: ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        firstDay: 1,
        editable: true,
        timeFormat: 'H:mm',
        axisFormat: 'H:mm',
        eventClick: function (date, allDay, jsEvent, view) {

        },
        dayClick: function (date, allDay, jsEvent, view) {
            console.log(date);
        },
        events: [
            {
                title: '全天计划\r\n#####\r\n写代码',
                start: new Date(y, m, 1)
            },
            {
                title: '张家界四日游',
                start: new Date(y, m, d - 5),
                end: new Date(y, m, d - 2)
            },
            {
                id: 999,
                title: '电话回访客户',
                start: new Date(y, m, d - 6, 16, 0),
                allDay: false
            },
            {
                id: 999,
                title: '电话回访客户',
                start: new Date(y, m, d + 4, 16, 0),
                allDay: false
            },
            {
                title: '视频会议',
                start: new Date(y, m, d, 10, 30),
                allDay: false
            },
            {
                title: '中秋放假',
                start: '2013-09-19',
                end: '2013-09-21'
            },
            {
                title: '午饭',
                start: new Date(y, m, d, 12, 0),
                end: new Date(y, m, d, 14, 0),
                allDay: false
            },
            {
                title: '生日聚会',
                start: new Date(y, m, d + 1, 19, 0),
                end: new Date(y, m, d + 1, 22, 30),
                allDay: false
            },
            {
                title: '访问Helloweba主页',
                start: new Date(y, m, 28),
                end: new Date(y, m, 29),
                url: 'http://helloweba.net/'
            },
            {
                title: '实战训练课',
                start: new Date(y, m, d + 23)
            }
        ]
    });

});