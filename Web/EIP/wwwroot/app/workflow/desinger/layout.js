var startId = UtilNewGuid(),
    endId = UtilNewGuid(),
    processId,
    $jsonViewer,
    workflow;
//取代setNodeRemarks方法，采用更灵活的注释配置
GooFlow.prototype.remarks.toolBtns = {
    cursor: "选择指针",
    direct: "结点连线",
    dashed: "关联虚线",
    start: "入口结点",
    end: "结束结点",
    task: "任务结点",
    node: "自动结点",
    chat: "决策结点",
    state: "状态结点",
    plug: "附加插件",
    fork: "分支结点",
    join: "联合结点",
    complex: "复合结点",
    group: "组织划分框编辑开关"
};
GooFlow.prototype.remarks.headBtns = {
    new: "新建流程",
    open: "查看设计器值",
    save: "保存结果",
    undo: "撤销最近一次操作",
    redo: "重做最近一次被撤销的操作",
    reload: "刷新重载流程图",
    print: "打印流程图"
};
GooFlow.prototype.remarks.extendRight = "工作区向右扩展";
GooFlow.prototype.remarks.extendBottom = "工作区向下扩展";

var workflowFunction = {
    initWorkflow: function () {
        workflow = $.createGooFlow($("#workflow"), {
            width: Language.mainWidth - 204,
            height: 590,
            toolBtns: [
                "start round mix", "end round", "task", "node", "chat", "state", "plug", "join", "fork", "complex mix"
            ],
            haveHead: true,
            headLabel: true,
            headBtns: [/*"new",*/ "open", "save", "undo", "redo", "reload", "print"], //如果haveHead=true，则定义HEAD区的按钮
            haveTool: true,
            haveDashed: true,
            haveGroup: true,
            useOperStack: true
        });
    },
    initEvent: function () {
        //打开
        workflow.onBtnOpenClick = function () {
            var options = {
                collapsed: false,
                withQuotes: false
            };

            $jsonViewer.jsonViewer(workflow.exportData(), options);
            BootstrapDialog.show({
                title: "流程图Json结构",
                message: $jsonViewer.html(),
                buttons: [{
                    icon: 'fa fa-times',
                    label: '关闭',
                    title: 'Mouse over Button 3',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }]
            });

        };

        //保存
        workflow.onBtnSaveClick = function () {
            DialogConfirm("是否保存流程信息？", function () {
                var activity = "", exportObj = workflow.exportData();
                $.each(exportObj.nodes, function (i, node) {
                    activity +=
                        "{\"ActivityId\": \"" + i + "\""
                        + ",\"Name\": \"" + node.name + "\""
                        + ",\"Left\":  " + node.left + ""
                        + ",\"Top\":  " + node.top + ""
                        + ",\"Type\":  \"" + node.type + "\""
                        + ",\"Width\":  " + node.width + ""
                        + ",\"Height\":  " + node.height + ""
                        + ",\"Alt\":  " + node.alt + ""
                        + ",\"Opinion\":  " + checkActivity(node.activityOpinion) + ""
                        + ",\"CommentsBelow\":  " + checkActivity(node.activityCommentsBelow) + ""
                        + ",\"TimeoutRemind\":  " + checkActivity(node.activityTimeoutRemind) + ""
                        + ",\"Archive\":  " + checkActivity(node.activityArchive) + ""
                        + ",\"WorkTime\":  " + checkActivity(node.activityWorkTime) + ""
                        + ",\"TimeoutRemindTypeEmail\":  " + checkActivity(node.activityTimeoutRemindTypeEmail) + ""
                        + ",\"TimeoutRemindTypeNote\":  " + checkActivity(node.activityTimeoutRemindTypeNote) + ""
                        + ",\"TimeoutRemindTypeWx\":  " + checkActivity(node.activityTimeoutRemindTypeWx) + ""
                        + ",\"Remark\":  " + checkActivity(node.activityRemark) + ""
                        + ",\"ProcessorType\":  " + checkActivity(node.activityProcessorType) + ""
                        + ",\"ProcessorHandler\":  " + checkActivity(node.activityProcessorHandler) + ""
                        + ",\"HandlingStrategy\":  " + checkActivity(node.activityHandlingStrategy) + ""
                        + ",\"HandlingStrategyPercentage\":  " + checkActivity(node.activityHandlingStrategyPercentage) + ""
                        + ",\"ButtonList\":  " + (((node.activityButtons === "") || typeof (node.activityButtons) === "undefined") ? "null" : JSON.stringify(node.activityButtons)) + ""
                        + ",\"EventSubmitBefore\":  " + checkActivity(node.activityEventSubmitBefore) + ""
                        + ",\"EventSubmitAfter\":  " + checkActivity(node.activityEventSubmitAfter) + ""
                        + ",\"EventBackBefore\":  " + checkActivity(node.activityEventBackBefore) + ""
                        + ",\"EventBackAfter\":  " + checkActivity(node.activityEventBackAfter) + ""
                        + ",\"FormId\":  " + checkActivity(node.activityForm) + "},";
                });

                if (activity !== "") {
                    activity = "[" + activity.substring(0, activity.length - 1) + "]";
                }
                //连接线
                var lineData = "";

                $.each(exportObj.lines, function (i, line) {
                    lineData +=
                        "{\"LineId\": \"" + i + "\""
                        + ",\"Name\": \"" + line.name + "\""
                        + ",\"LineType\": " + (checkActivity(line.lineType) == null ? 2 : line.lineType) + ""
                        + ",\"ReturnPolicy\": " + checkActivity(line.lineReturnPolicy) + ""
                        + ",\"Conditions\": " + checkActivity(line.lineConditions) + ""
                        + ",\"Type\":  \"" + line.type + "\""
                        + ",\"M\":  " + checkActivity(line.M) + ""
                        + ",\"From\":  \"" + line.from + "\""
                        + ",\"To\":  \"" + line.to + "\"},";
                });

                if (lineData !== "") {
                    lineData = "[" + lineData.substring(0, lineData.length - 1) + "]";
                }

                //区域
                var areasData = "";

                $.each(exportObj.areas, function (i, areas) {
                    areasData +=
                        "{\"AreasId\": \"" + i + "\""
                        + ",\"Name\": \"" + areas.name + "\""
                        + ",\"Left\":  " + areas.left + ""
                        + ",\"Top\":  " + areas.top + ""
                        + ",\"Color\":  \"" + areas.color + "\""
                        + ",\"Width\":  " + areas.width + ""
                        + ",\"Height\":  " + areas.height + ""
                        + ",\"Alt\":  \"" + areas.alt + "\"},";
                });

                if (areasData !== "") {
                    areasData = "[" + areasData.substring(0, areasData.length - 1) + "]";
                }
                //获取流程数据
                UtilAjaxPostWait("/Workflow/SaveWorkflowProcessJson", {
                    activity: activity,
                    line: lineData,
                    areas: areasData,
                    designJson: JSON.stringify(exportObj),
                    processId: processId
                }, function (data) {
                    DialogAjaxResult(data);
                });
            });
        };

        //刷新
        workflow.onFreshClick = function () {

        };

        //单元由不选中变为选中触发
        workflow.onItemFocus = function (id, type) {
            //开始和结束节点无删除图标
            if (id === startId || id === endId) {
                $(".rs_close", $("#" + id)).remove();
            }
            return true;
        };

        //单元连接线双击事件
        var tmpClk = "PolyLine";
        if (GooFlow.prototype.useSVG !== "")
            tmpClk = "g";
        $(workflow.$draw).delegate(tmpClk, "dblclick", { inthis: workflow }, function (e) {
            debugger;
            var $this = $(this),
                //连线Id
                lineId = $this.attr("id"),
                //连线对象
                line = workflow.getItemInfo(lineId, "line"),
                //传递给弹出框对象
                lineObj = {
                    LineId: lineId,
                    LineName: line.name,
                    LineType: (typeof (line.lineType) == "undefined") ? "2" : line.lineType,
                    LineConditions: (typeof (line.lineConditions) == "undefined") ? "" : line.lineConditions,
                    LineReturnPolicy: (typeof (line.lineReturnPolicy) == "undefined") ? "2" : line.lineReturnPolicy,
                    LineRemark: (typeof (line.lineRemark) == "undefined") ? "" : line.lineRemark
                };
            //如果是条件
            //art.dialog.data("lineObj", lineObj); // 存储数据
            //art.dialog.open("/Workflow/Designer/LineActivity", {
            //    title: "连线节点配置",
            //    height: 340,
            //    padding: 1,
            //    width: 420,
            //    resize: false,
            //    lock: true,
            //    opacity: 0.3,
            //    close: function () { }
            //}, false);

        });
    },
    getData: function () {
        //获取值
        processId= $("#processId").val();
        if (processId != '') {
            //获取后台数据
            UtilAjaxPostWait("Workflow/GetById", { id: processId }, function (data) {
                var json = JSON.parse(data.DesignJson);
                $.each(json.nodes, function (i, node) {
                    if (node.type === "start round") {
                        startId = i;
                    }
                    if (node.type === "end round") {
                        endId = i;
                    }
                });
                //清楚界面信息
                workflow.clearData();
                //重绘窗口
                workflow.loadData(json);
                //从新赋予标题
                workflow.setTitle(data.Name + " " + data.Version);
            });
        }
    }
}

//初始化
$(document).ready(function () {
    //初始化流程
    workflowFunction.initWorkflow();
    workflowFunction.getData();
    workflowFunction.initEvent();
    $jsonViewer = $('#json-renderer');
});

//检查活动
function checkActivity(value) {
    var compareValue = null;
    if (value !== "" && typeof (value) !== "undefined") {
        compareValue = "\"" + value + "\"";
    }
    return compareValue;
}

//打开节点
function openNode() {
    debugger;
    var activityId = $(".item_focus").attr("id"),
        focusNode = workflow.getItemInfo(activityId, "node"),
        activityObj = {
            ActivityId: activityId,
            ActivityName: focusNode.name,
            ActivityOpinion: (typeof (focusNode.activityOpinion) == "undefined") ? "0" : focusNode.activityOpinion,
            ActivityCommentsBelow: (typeof (focusNode.activityCommentsBelow) == "undefined") ? "0" : focusNode.activityCommentsBelow,
            ActivityTimeoutRemind: (typeof (focusNode.activityTimeoutRemind) == "undefined") ? "0" : focusNode.activityTimeoutRemind,
            ActivityArchive: (typeof (focusNode.activityArchive) == "undefined") ? "0" : focusNode.activityArchive,
            ActivityWorkTime: (typeof (focusNode.activityWorkTime) == "undefined") ? "0" : focusNode.activityWorkTime,
            ActivityForm: (typeof (focusNode.activityForm) == "undefined") ? "" : focusNode.activityForm,
            ActivityTimeoutRemindTypeEmail: (typeof (focusNode.activityTimeoutRemindTypeEmail) == "undefined") ? "false" : focusNode.activityTimeoutRemindTypeEmail,
            ActivityTimeoutRemindTypeNote: (typeof (focusNode.activityTimeoutRemindTypeNote) == "undefined") ? "false" : focusNode.activityTimeoutRemindTypeNote,
            ActivityTimeoutRemindTypeWx: (typeof (focusNode.activityTimeoutRemindTypeWx) == "undefined") ? "false" : focusNode.activityTimeoutRemindTypeWx,
            ActivityRemark: (typeof (focusNode.activityRemark) == "undefined") ? "" : focusNode.activityRemark,
            //策略
            ActivityProcessorType: (typeof (focusNode.activityProcessorType) == "undefined") ? "0" : focusNode.activityProcessorType,
            ActivityProcessorHandler: (typeof (focusNode.activityProcessorHandler) == "undefined") ? "" : focusNode.activityProcessorHandler,
            ActivityHandlingStrategy: (typeof (focusNode.activityHandlingStrategy) == "undefined") ? "0" : focusNode.activityHandlingStrategy,
            ActivityHandlingStrategyPercentage: (typeof (focusNode.activityHandlingStrategyPercentage) == "undefined") ? "100" : focusNode.activityHandlingStrategyPercentage,
            //按钮
            ActivityButtons: (typeof (focusNode.activityButtons) == "undefined") ? [] : focusNode.activityButtons,
            //数据

            //事件
            ActivityEventSubmitBefore: (typeof (focusNode.activityEventSubmitBefore) == "undefined") ? "" : focusNode.activityEventSubmitBefore,
            ActivityEventSubmitAfter: (typeof (focusNode.activityEventSubmitAfter) == "undefined") ? "" : focusNode.activityEventSubmitAfter,
            ActivityEventBackBefore: (typeof (focusNode.activityEventBackBefore) == "undefined") ? "" : focusNode.activityEventBackBefore,
            ActivityEventBackAfter: (typeof (focusNode.activityEventBackAfter) == "undefined") ? "" : focusNode.activityEventBackAfter

        };
    //art.dialog.data("activityObj", activityObj); // 存储数据
    //art.dialog.open("/Workflow/Designer/ApproveActivity", {
    //    title: focusNode.name + "节点配置",
    //    height: 440,
    //    padding: 1,
    //    width: 740,
    //    resize: false,
    //    lock: true,
    //    opacity: 0.3
    //}, false);
};
