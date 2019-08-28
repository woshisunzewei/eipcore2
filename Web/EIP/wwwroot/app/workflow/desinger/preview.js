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
            haveHead: false,
            headLabel: true,
            headBtns: [/*"new",*/ "open", "save", "undo", "redo", "reload", "print"], //如果haveHead=true，则定义HEAD区的按钮
            haveTool: false,
            haveDashed: true,
            haveGroup: true,
            useOperStack: true
        });
    },
  
    getData: function () {
        //获取值
        processId = $("#processId").val();
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
            });
        }
    }
}

//初始化
$(document).ready(function () {
    //初始化流程
    workflowFunction.initWorkflow();
    workflowFunction.getData();
    $jsonViewer = $('#json-renderer');
});

