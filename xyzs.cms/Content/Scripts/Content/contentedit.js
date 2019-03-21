;
//根目录
var hidRootUrl = $("#hidRootNode").val();
//实例化编辑器
//建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
var ue = UE.getEditor('shareContent');
/**
初始化页面
*/
$(document).ready(function () {
    init();
});
//var shareContent = UE.getEditor('shareContent').getContent();
var costtypenumlist = [{ "id": 0, "isdelete": 0, "isvalid": 1, "name": "未知", "sort": 0 }, { "id": 1, "isdelete": 0, "isvalid": 1, "name": "餐饮", "sort": 0 }, { "id": 2, "isdelete": 0, "isvalid": 1, "name": "乘车", "sort": 0 }, { "id": 3, "isdelete": 0, "isvalid": 1, "name": "景点", "sort": 0 }, { "id": 4, "isdelete": 0, "isvalid": 1, "name": "服饰", "sort": 0 }, { "id": 5, "isdelete": 0, "isvalid": 1, "name": "奢侈品", "sort": 0 }, { "id": 6, "isdelete": 0, "isvalid": 1, "name": "送礼", "sort": 0 }, { "id": 7, "isdelete": 0, "isvalid": 1, "name": "电子产品", "sort": 0 }, { "id": 8, "isdelete": 0, "isvalid": 1, "name": "外借", "sort": 0 }, { "id": 9, "isdelete": 0, "isvalid": 1, "name": "归还", "sort": 0 }, { "id": 10, "isdelete": 0, "isvalid": 1, "name": "工资", "sort": 0 }, { "id": 11, "isdelete": 0, "isvalid": 1, "name": "结余", "sort": 0 }, { "id": 12, "isdelete": 0, "isvalid": 1, "name": "本金", "sort": 0 }, { "id": 13, "isdelete": 0, "isvalid": 1, "name": "取现", "sort": 0 }, { "id": 14, "isdelete": 0, "isvalid": 1, "name": "住宿", "sort": 0 }, { "id": 15, "isdelete": 0, "isvalid": 1, "name": "充话费", "sort": 0 }, { "id": 16, "isdelete": 0, "isvalid": 1, "name": "水电费", "sort": 0 }, { "id": 17, "isdelete": 0, "isvalid": 1, "name": "物管费", "sort": 0 }, { "id": 18, "isdelete": 0, "isvalid": 1, "name": "装修款", "sort": 0 }, { "id": 19, "isdelete": 0, "isvalid": 1, "name": "家具", "sort": 0 }, { "id": 20, "isdelete": 0, "isvalid": 1, "name": "押金", "sort": 0 }, { "id": 21, "isdelete": 0, "isvalid": 1, "name": "生活用品", "sort": 0 }];

var detailVm = new Vue({
    el: '#detailWindow',
    data: {
        content_model: {
            Id:0,
            Title: "",
            ContentType: 0,
            Content: "",
            ContentSource: ""
        },
        all_type_list: costtypenumlist
    }
});

/**
 * 初始化
 */
function init() {
    getContentInfo();
}

/**
 * 添加内容信息
 */
function addContentInfo() {
    var contentInfo = ue.getContent();
    detailVm.$data.content_model.Content = contentInfo;
    //加载
    $(".loading-container").removeClass("loading-inactive");
    $.ajax({
        url: "/Content/AddContentInfo",
        type: "POST",
        data: detailVm.$data.content_model,
        success: function (data) {
            if (data && data.ResultCode == 0) {
                layer.msg(data.Message);
            }
            else {
                layer.msg('处理失败');
            }
            //取消加载
            $(".loading-container").addClass("loading-inactive");
        }
    });
}

/**
 * 获取初始化内容信息
 */
function getContentInfo() {
    var id = $("#id").val();
    $.ajax({
        url: "/Content/GetContentInfo",
        type: "POST",
        data: { id: id },
        success: function (data) {
            if (data && data.ResultCode == 0) {
                detailVm.$data.content_model = data.Data;
                //初始化编辑器内容
                UE.getEditor('shareContent').addListener("ready", function () {
                    UE.getEditor('shareContent').setContent(detailVm.$data.content_model.Content);
                });
            }
            else {
                layer.msg('查询失败');
            }
        }
    });
}