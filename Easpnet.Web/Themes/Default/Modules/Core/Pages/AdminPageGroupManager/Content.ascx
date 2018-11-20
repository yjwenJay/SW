<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminPageGroupManager" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=Html.HrefLink("Core", "AdminPageGroupDetails", "") %>" class="add"><span>添加分组</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="tableList">
	    <table class="list" width="100%">
	        <thead>
	            <tr class="trhead1">
	                <th colspan="1">基本信息</th>
	                <th colspan="5">栏目设置</th>
	                <th>操作</th>
	            </tr>
	            <tr class="trhead2">
	                <th>分组名称</th>              
	                <th class="displayBox">顶栏</th>
	                <th class="displayBox">左栏</th>
	                <th class="displayBox">右栏</th>
	                <th class="displayBox">底栏</th>	                
	                <th class="displayBox">&nbsp;</th>
	                <th class="displayBox">&nbsp;</th>
	            </tr>
	        </thead>
	        <tbody>
        <%foreach (ModelBase item in list)
          {
              Easpnet.Modules.Models.PageGroup md = item as Easpnet.Modules.Models.PageGroup;
              %>
              <tr itemid="<%=md.PageGroupId %>">
                    <td>
                        <h4><%=md.PageGroupName %></h4>
                        <div class="desc"><%=md.Remark %></div>
                    </td>
	                <td><%=md.DisplayTopBox ? T("√") : ""%></td>
	                <td><%=md.DisplayLeftBox ? T("√") : ""%></td>
	                <td><%=md.DisplayRightBox ? T("√") : ""%></td>
	                <td><%=md.DisplayBottomBox ? T("√") : ""%></td>
	                <td><a href="<%=Html.HrefLink("Core", "AdminPageGroupBlockManage", "pgid=" + md.PageGroupId) %>"><%=T("设置")%></a></td>
	                <td><a href="<%=Html.HrefLink("Core", "AdminPageGroupDetails", "pgid=" + md.PageGroupId) %>"><%=T("编辑") %></a></td>
              </tr>
        <%  } %>
        
            </tbody>
        </table>
    </div>
</div>