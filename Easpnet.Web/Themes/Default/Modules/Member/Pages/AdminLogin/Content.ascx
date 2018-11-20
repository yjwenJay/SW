<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.AdminLogin" %>
<% =ErrorMessage%><%=SuccessMessage %>
<div class="main2">
    <div class="col51">
        <div class="left_b">
        </div>
        <div class="left_r">
            <div class="ceng1">
                <div class="top">
                    <img width="268" height="7" src="<%=ImageUrl("left_r_t.gif") %>"></div>
                <div class="bg">
                    <p>
                        请登录<%=Configs["site_name"]%></p>
                    <form action="<%=Html.HrefLink("") %>" method="post" class="fm-v" name="loginForms"
                    id="formLogin">
                    <object id="input_password" width="0" height="0" classid="CLSID:C6D8FFD0-7562-4AAF-B64C-E1051A9CCAD0"
                        codebase="client/ECardClient.dll#version=1,0,0,8">
                    </object>
                    <input type="hidden" value="send" name="action" />
                    <div class="wr" id="status" style="visibility: hidden;">
                    </div>
                    <h1 class="mt10">
                        <span>用户名：</span> <em>
                            <input type="text" autocomplete="false" maxlength="16" value="<%=user.UserName %>"
                                accesskey="n" tabindex="1" class="input1" name="username" id="username" style="color: rgb(102, 102, 102);">
                        </em>
                    </h1>
                    <h1>
                        <span>密&nbsp;&nbsp;&nbsp;&nbsp;码：</span> <em>
                            <input type="password" name="password" />
                        </em>
                    </h1>
                    <h1>
                        <span>验证码：</span> <em>
                            <%=Html.InputText("yzmk", "CodeValue", "", "", "")%>
                            <img id="imgCheckCode" imgurl="<%=Html.HrefLink("Member", "CheckCode")%>" src="<%=Html.HrefLink("Member", "CheckCode")%>" />
                        </em>
                    </h1>
                    <h1>
                        <em>
                            <input type="checkbox" tabindex="3" value="true" id="rememberMe" name="rememberMe"
                                checked="checked">
                            下次自动登录</em>
                    </h1>
                    <input type="hidden" value="submit" name="_eventId">
                    <h2 class="mt10">
                        <input type="image" src="<%=ImageUrl("icon1.gif") %>" />
                    </h2>
                    <div class="pd10 tc">
                        还没有开通你的<%=Configs["site_name"]%>账户？<a style="text-decoration: underline;" class="cBlue"
                            href="<%=Html.HrefLink("Member","Register") %>">快速注册</a></div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
