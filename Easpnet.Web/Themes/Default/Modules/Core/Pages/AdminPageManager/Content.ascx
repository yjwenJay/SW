<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminPageManager" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=Html.HrefLink("Core", "AdminPageDetails", "") %>" class="add"><span>添加静态页面</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
	<form action="<%=Html.HrefLink("") %>">
	    <div>
        <%=T("页面名称") %>: <%=Html.InputText("pagename", "pageName", Get("pageName"), "textbox") %>
        <%=Html.Submit("submit", "submit", "查询")%>
        </div>
    </form> 
	<div class="tableList">
	    <table class="list" width="100%">
	        <thead>
	            <tr class="trhead1">
	                <th colspan="4">页面基本信息</th>
	                <th colspan="5">栏目设置</th>
	                <th>操作</th>
	            </tr>
	            <tr class="trhead2">
	                <th>页面名称</th>
	                <th>页面类型</th>
	                <th>所属模块</th>
	                <th>所属分组</th>	                
	                <th class="displayBox">顶栏</th>
	                <th class="displayBox">左栏</th>
	                <th class="displayBox">右栏</th>
	                <th class="displayBox">底栏</th>	                
	                <th class="displayBox"></th>
	            </tr>
	        </thead>
	        <tbody>
        <%foreach (ModelBase item in modules)
          {
              Easpnet.Modules.Models.PageInfo md = item as Easpnet.Modules.Models.PageInfo;
              %>
              <tr itemid="<%=md.PageId %>">
                    <td>
                        <h4><%=md.PageName %></h4>
                        <div class="desc"><%=md.Remark %></div>
                    </td>
                    <td><%=md.DisplayStaticHtml ? "<span class=\"static_html\">" + T("静态页面") + "</span>" : "常规"%></td>
	                <td><%=md.Module %></td>
	                <td><%=md.PageGroupName %></td>
	                <td><%=md.DisplayTopBox ? T("√") : ""%></td>
	                <td><%=md.DisplayLeftBox ? T("√") : ""%></td>
	                <td><%=md.DisplayRightBox ? T("√") : ""%></td>
	                <td><%=md.DisplayBottomBox ? T("√") : ""%></td>
	                <td><a href="<%=Html.HrefLink("Core", "AdminPageBlockManage", "pid=" + md.PageId) %>"><%=T("设置")%></a></td>
	                <td><a href="<%=Html.HrefLink("Core", "AdminPageDetails", "pid=" + md.PageId) %>"><%=T("编辑") %></a></td>
              </tr>
        <%  } %>
        
            </tbody>
        </table>
    </div>
</div>
