<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.Register" %>
<%=res.Message%>

<div class="login">
      <div class="loginWrapper">            
        <fieldset id="loginMessage">
        <form name="regForm" action="<%=Html.HrefLink("Member","Register") %>" method="post" id="regForm">
        	<input type="hidden" value="send" name="action" />
          <div class="">
            <p style="position: relative;">
              <label for="name">用&nbsp;&nbsp;户&nbsp;&nbsp;名</label>　
              <input type="text" value="<%=user.UserName %>" name="user.username" class="validate[required,custom[noSpecialCaracters],length[4,16],ajax[ajaxUser]]" id="name"/>
              <span class="desc">4-16个字符，由英文字符、数字或下划线组成</span>
            </p>
          </div>
          <div class="">
            <p style="position: relative;">
              <label for="password">密　　码</label>　
              <input type="password" class="validate[required,length[6,16]]" name="user.password" id="password1" />
              <span class="desc">6-16个字符</span>
            </p>
          </div>
          <div class="">
            <p style="position: relative;">
              <label for="password2">确认密码</label>　
              <input type="password" class="validate[required,confirm[password1]]" name="passConfirm" id="password2" />
              <span class="desc">再次输入上面的密码，两次密码必须一直</span>
            </p>
          </div>

          <div>
            <p style="position: relative;">
              <label for="code">验&nbsp;&nbsp;证&nbsp;&nbsp;码</label>　
              <input type="text" value="" class="validate[required]" maxlength="4" size="4" name="code" id="code" tooltipText="输入下面图形中的字符" />
			<div style="padding-left:80px;">
			    <img src="<%=Html.HrefLink("Member","CheckCode") %>" style="cursor:pointer;" id="imgVerify" onclick="$('#imgVerify').attr('src','<%=Html.HrefLink("Member","CheckCode") %>&'+ Math.floor(Math.random()*10+1));">
			    <a href="javascript:void(0)" onclick="$('#imgVerify').attr('src','<%=Html.HrefLink("Member", "CheckCode") %>&'+ Math.floor(Math.random()*10+1));" id="changeone" style="position: relative; left: 5px; color: rgb(170, 170, 170);">换一张</a>
			</div> 
						
			</p>                 
          </div>          
          <div class="terms">
            <input type="checkbox" id="agree" checked="checked">
            <a target="_blank" href="#">我已阅读同意遵守《服务条款》</a>
            <input type="submit" class="hide" />
            </div>
        </form>
        </fieldset>
        <div class="submit"><a id="submitBn" href=""></a></div>
      </div>
      <div>
      </div>
</div>