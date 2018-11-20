<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.BlockBase" %>
<form id="MerchantLoginBox" action="<%=Html.HrefLink("MerchantCenter","MerchantLogin") %>" method="post">
<div class="yhdl"></div>
        <div class="yhm">
        	<div class="yhml">
           	  <div class="yhmlr">
                	<div class="yhmlrwbk"><%=Html.InputText("yhmi", "username", Post("username"), "")%></div>
              </div>
            </div>
   
        </div>
        <div class="yhm">
        	<div class="yhml">
            	<div class="yhmlr">
                	<div class="yhmlrwbk"><%=Html.InputPassword("mmk", "password", "", "")%></div>
                </div>
            </div>
   
        </div>
        <div class="yhm">
        	<div class="yhml">
            	<div class="yhmlr">
           	  <div class="yzm">
                    	<div class="yzmwbk"><%=Html.InputText("yzmk", "CodeValue", "", "", "")%></div>
                    </div>
                  <div class="yzmtp">
                    	<div class="yzmtpt">
                        <a href="javascript:void(0);"><img id="imgCheckCode" class="refeshCode" src="<%=Html.HrefLink("Member", "CheckCode", "")%>" /></a>                         </div>
                  </div>
                </div>
            </div>
        </div>
        <div class="wjmm"><span><a href="#">忘记密码</a></span>&nbsp;&nbsp;<span><a href="javascript:void(0)" class="refeshCode">看不清？换一张</a></span></div>
        <div class="denglu">
        	<div class="denglul">
            	<div class="denglulr" id="login"><a href="javascript:void(0)" class="BtnFormSubmit"></a></div>
            </div>
            <div class="zhuce" id="zhuce"><a href="<%=Html.HrefLink("MerchantCenter", "MerchantRegister") %>"></a></div>
        </div>
        
</form>