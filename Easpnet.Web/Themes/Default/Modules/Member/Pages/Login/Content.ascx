<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.Login" %>
<% =res.Message%>
<div class="main2">
<div class="col51">
<div class="left_b"></div>
 <div class="left_r">
<div class="ceng1">
<div class="top"><img width="268" height="7" src="<%=ImageUrl("left_r_t.gif") %>"></div>
<div class="bg">
<p>请登录</p>
<form action="<%=Html.HrefLink("Member","Login") %>" method="post" class="fm-v" name="loginForms" id="fm1"> 

<input type="hidden" value="send" name="action" />
    
        <div class="wr" id="status" style="visibility: hidden;"></div>
	<h1 class="mt10"><span>用户名：</span>
		<em>		  
		  <input type="text" autocomplete="false" maxlength="16" value="<%=user.UserName %>" accesskey="n" tabindex="1" class="input1" name="username" id="username" style="color: rgb(102, 102, 102);">
		</em>
	</h1>
	<h1><span>密&nbsp;&nbsp;&nbsp;&nbsp;码：</span>
        <input type="text" name="password" id="password1">
		<em>		
		    
	   </em>
	</h1>
	<h1>
	    <span>验证码：</span>
	    <em>
	        <%=Html.InputText("yzmk", "CodeValue", "", "", "")%>
	        <img id="imgCheckCode" imgurl="<%=Html.HrefLink("Member", "CheckCode")%>" src="<%=Html.HrefLink("Member", "CheckCode")%>" />
	    </em>
	</h1>
	<h1><em>
		  <input type="checkbox" tabindex="3" value="true" id="rememberMe" name="rememberMe" checked="checked">
		  下次自动登录</em>
	</h1> 
		<input type="hidden" value="_c3A8A8DF3-84B0-DCFC-FD8C-7FCE2EDDD738_kD86B94DC-9629-1E62-C3D8-5A5C7AAD6870" name="lt">
		<input type="hidden" value="submit" name="_eventId">
	<h2 class="mt10">
	    <input type="image" src="<%=ImageUrl("icon1.gif") %>" />
	</h2>
	<div class="pd10 tc">还没有开通你的<%=Configs["site_name"]%>账户？<a style="text-decoration: underline;" class="cBlue" href="<%=Html.HrefLink("Member","Register") %>">快速注册</a></div>
</form>
</div>

</div>

</div>

</div>


