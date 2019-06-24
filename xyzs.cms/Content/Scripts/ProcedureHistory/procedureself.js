//根目录
var hidRootUrl = $("#hidRootNode").val();
var all_resource_type = [{ Id: 1, name: '图片' }];
var resultVm = new Vue({
    el: '#resultTable',
    data: {
        dataList: [],
        dataIds: []
    }
});

/*
 * 分页方法名
 */
var pager = new PagerView('pager');
pager.itemCount = '0';
pager.index = '1';
pager.size = '20';
pager.methodName = "Search";
pager.render();

var searchVm = new Vue({
    el: "#form1",
    data: {
        pageIndex: pager.index,
        pageSize: pager.size,
        procedureCode: "",
        contentTypeNum: []
    }
});

/*
 * 获取用户
 */
function LoadingActivityResultDetailDate() {
    //加载
    $(".loading-container").removeClass("loading-inactive");
    searchVm.$data.pageIndex = $("#currentPageIndex").val();
    $.ajax({
        url: hidRootUrl + "/ProcedureHistory/ResourceListPage",
        type: "POST",
        data: searchVm.$data,
        success: function (data) {
            if (data && data.ResultCode == 0) {
                resultVm.$data.dataList = data.Data.dataList;
                resultVm.$data.dataIds = [];
                pager.itemCount = "" + data.Data.count;
                pager.index = $("#currentPageIndex").val();
                pager.render();
                layer.ready(function () {
                    layer.photos({
                        photos: '#Images'
                    });
                });
            }
            else {
                layer.msg('查询失败');
            }
            //取消加载
            $(".loading-container").addClass("loading-inactive");
        }
    });
}

/*
 * 分页查询功能
 */
function Search(pageIndex) {
    $("#currentPageIndex").val(pageIndex);
    LoadingActivityResultDetailDate();
}

/**
 * 加载所有的类型
 */
function GetAllType() {
    $.ajax({
        url: hidRootUrl + "/ProcedureHistory/GetContentType",
        type: "POST",
        data: { code: "ProcedureType" },
        success: function (data) {
            if (data && data.ResultCode == 0) {
                searchVm.$data.contentTypeNum = data.Data;
                searchVm.$data.contentTypeNum.splice(0, 0, { Id: "", Lable: "全部" });
            }
        }
    });
}

layer.ready(function () {
    layer.photos({
        photos: '#Images'
    });
});
LoadingActivityResultDetailDate();
GetAllType();