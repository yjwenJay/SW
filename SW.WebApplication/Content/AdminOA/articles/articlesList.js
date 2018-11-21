$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();
    var E = window.wangEditor;
    var editor = new E('#Infotext');
    editor.create();
    editor.txt.html('<p>请输入文章内容......</p>');
});


var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_articles').bootstrapTable({
            url: '/Article/GetLists',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'Id',
                title: 'ID'
            }, {
                field: 'Title',
                title: '标题'
            }, {
                field: 'Info',
                title: '内容'
            }, {
                field: 'CreateTimes',
                title: '创建时间'
            }, {
                field: 'Click',
                title: '点击量'
            }, {
                field: 'Name',
                title: '归属人'
            }, {
                field: 'ModuleName',
                title: '模块名称'
            }
            ]
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            page: params.offset,  //页码 
        };
        return temp;
    };
    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        //初始化页面上面的按钮事件
        deleted();
        searched();
        aSubmited();
    };

    return oInit;
};
function aSubmited() {
    $("#aSubmit").click(function () {
        var contents = $(".w-e-text").html();
        var infos = $(".w-e-text").text();

        if (infos.length > 30) {
            //$("<div>").text(html).html();
            //$("<div>").html(encodedHtml).text();
            $("#Info").val(infos);
            $("#Contents").val($("<div>").text(contents).html());
            return true;
        }
        else {
            alert("文章内容不少于三十个字符！");
            return false;
        }
    })
};
function deleted() {
    $("#btn_delete").click(function () {
        var a = $('#tb_articles').bootstrapTable('getSelections');

        if (a.length == 1) {
            //var artId = "";
            //for (let index in a) {
            //    artId += a[index].Id + ",";
            //}; 
            $.ajax({
                type: "POST",
                url: '/Article/Deleted',
                data: { IDs: a[0].Guid },
                dataType: "json",
                success: function (data) {
                    if (data.flag == true) {
                        alert("删除成功！");
                    }
                    else {
                        alert("删除失败！");
                    }
                }
            });
        }
        else {
            alert("请选中一行，不可多选或不选！");
        }
    });
};
function searched() {
    $("#btn_search").click(function () {
        var opt = {
            url: '/Article/SearchLists',
            silent: true,
            query: {
                'ModuleId': $("#sModule").val(),
                'Name': $("#sUserName").val()
            }
        }
        //$("#tb_articles").bootstrapTable('destroy');
        $('#tb_articles').bootstrapTable('refresh', opt);
    })
};
// $("#tb_articles").bootstrapTable('destroy');



