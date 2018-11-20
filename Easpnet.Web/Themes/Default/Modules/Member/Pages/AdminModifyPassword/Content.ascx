<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.AdminModifyPassword" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><span>修改登录密码</span></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="form" layouth="56">
	    <form action="<%=Html.HrefLink("") %>" method="post">
	    <%=Html.InputHidden("option", "option", "save")%>
	    <table class="form" width="100%">
	        <thead>	        
	        </thead>	        
	        <tbody>
	            <tr>
	                <th> * <%=T("原始密码") %>：</th>
	                <td>
	                    <%=Html.InputPassword("Password", "Password", "", "textInput")%>
	                    <span class="desc">登陆名</span>
	                </td>
	            </tr>
	            <tr>
	                <th> <%=T("新密码") %>：</th>
	                <td>
	                    <%=Html.InputPassword("PasswordNew", "PasswordNew", "", "textInput")%>
	                    <span class="desc">输入新的登陆密码</span>
	                </td>
	            </tr>
	            <tr>
	                <th> <%=T("确认新密码")%>：</th>
	                <td>
	                    <%=Html.InputPassword("PasswordNew2", "PasswordNew2", "", "textInput")%>
	                    <span class="desc">再次输入新的登陆密码</span>
	                </td>
	            </tr>
	            <tr>
	                <th></th>
	                <td><div class="button"><div class="buttonContent"><button type="submit">确认修改密码</button></div></div> </td>
	            </tr>
	        </tbody>
	    </table>
	    </form>
    </div>
</div>