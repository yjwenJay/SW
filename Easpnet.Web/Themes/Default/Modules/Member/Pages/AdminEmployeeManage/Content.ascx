<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.AdminEmployeeManage" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=Html.HrefLink("Member", "AdminEmployeeDetails", "") %>" class="add"><span>添加员工</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>

<form id="formList" action="<%=Html.HrefLink("") %>" method="post">
    <%=Html.InputHidden("option", "option", "")%>
	<div class="tableList">
	    <table class="list" width="100%">
	        <thead>
	            <tr>
	                <th style="display: none;"></th>
	                <th>用户名</th>
	                <th>真实姓名</th>
	                <th>所属分组</th>
	                <th>Email</th>
	                <th>手机号码</th>
	                <th>二级密码</th>
	                <th>物理地址</th>	                
	                <th></th>
	            </tr>
	        </thead>
	        <tbody>
        <%foreach (ModelBase item in lst)
          {
              Easpnet.Modules.Member.Models.AdminUser md = item as Easpnet.Modules.Member.Models.AdminUser;
              %>
              <tr itemid="<%=md.UserId %>">
                    <th style="display: none;">
                        <input type="checkbox" name="ids" value="<%=md.UserId%>" />
                    </th>
                    <td>
                        <h4><%=md.UserName%></h4>
                    </td>
                    <td><%=md.RealName%></td>
	                <td><%=md.GroupName %></td>
                    <td><%=md.Email%></td>
	                <td><%=md.Mobile %></td>
                    <td><%=string.IsNullOrEmpty(md.SecondPassword) ? "无" : "已设置" %></td>
	                <td>
	                    <%=string.IsNullOrEmpty(md.MacAddress) ? "未绑定" : "已绑定 [<a class=\"unbind\">解绑</a>]"%>
	                </td>
                    <td>
                        <a class="edit" href="<%=Html.HrefLink("Core", "AdminEmployeeDetails", "user_id=" + md.UserId) %>"><span><%=T("编辑") %></span></a> | 
                        <a href="<%=Html.HrefLink("Core", "AdminUserPermissions", "user_id=" + md.UserId) %>"><%=T("设置权限") %></a> |
                        <a class="btnDelete"><%=T("删除")%></a>                        
                    </td>
              </tr>
        <%  } %>
        
            </tbody>
        </table>
    </div>
  
</form>  

</div>
