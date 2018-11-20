<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.AdminEmployeeDetails" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a class="add" href="<%=back_url %>"><span>用户列表</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="form" layouth="56">
	    <form action="<%=Html.HrefLink("") %>" method="post">
	    <%=Html.InputHidden("option", "option", (is_edit ? "save" : "insert"))%>
	    <table class="form" width="100%">
	        <thead>	        
	        </thead>	        
	        <tbody>
	            <tr>
	                <th> * <%=T("用户名") %>：</th>
	                <td>
	                    <%=Html.InputText("UserName", "UserName", user.UserName, "textInput", (is_edit ? "disabled=\"disabled\"" : ""))%>
	                    <span class="desc">登陆名</span>
	                </td>
	            </tr>
	            <tr>
	                <th> <%=(is_edit ? "" : "*") %><%=T("密码") %>：</th>
	                <td>
	                    <%=Html.InputPassword("Password", "Password", user.Password, "textInput")%>
	                    <span class="desc">登陆密码</span>
	                </td>
	            </tr>
	            <tr>
	                <th> <%=T("二级密码") %>：</th>
	                <td>
	                    <%=Html.InputPassword("SecondPassword", "SecondPassword", user.SecondPassword, "textInput")%>
	                    <span class="desc">关键操作必须使用的密码，普通用户可留空</span>
	                </td>
	            </tr>
	            <tr>
	                <th>* <%=T("真实姓名") %>：</th>
	                <td>
	                    <%=Html.InputText("RealName", "RealName", user.RealName, "textInput")%>
	                </td>
	            </tr>
	            <tr>
	                <th><%=T("用户分组") %>：</th>
	                <td>
	                    <%=Html.InputText("GroupName", "GroupName", user.GroupName, "textInput")%>
	                </td>
	            </tr>
	            <tr>
	                <th><%=T("手机号码") %>：</th>
	                <td>
	                    <%=Html.InputText("Mobile", "Mobile", user.Mobile, "Mobile")%>
	                </td>
	            </tr>
	            <tr>
	                <th><%=T("Email") %>：</th>
	                <td>
	                    <%=Html.InputText("Email", "Email", user.Email, "Email")%>
	                </td>
	            </tr>
	            <tr>
	                <th></th>
	                <td><div class="button"><div class="buttonContent"><button type="submit"><%=is_edit ? T("保存") : T("添加")%></button></div></div> </td>
	            </tr>
	        </tbody>
	    </table>
	    </form>
    </div>
</div>