<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.AdminUserPermissions" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=Html.HrefLink("Member", "AdminEmployeeManage", "") %>" class="add"><span>员工列表</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
	
	<h3>对用户 <%=user.UserName %>[<%=user.RealName %>]设置权限</h3>
	
    <form id="formSave" action="<%=Html.HrefLink("") %>" method="post">
	    <%=tree %>	    
	    <input type="hidden" name="option" value="save"/>
	    <input type="hidden" name="values" value="" id="values"/>	    
        <button id="btnSave" type="submit" class="button">保存设置</button>
        <button class="go-back" class="button" type="button">取消</button>
    </form>
</div>