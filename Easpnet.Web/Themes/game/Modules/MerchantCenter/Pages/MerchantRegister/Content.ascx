<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Huif.MerchantCenter.Pages.MerchantRegister" %>


<form id="form1" action="<%=Html.HrefLink()%>" method="post">
<input type="hidden" name="action" value="add" />

    	<div class="question_rt">
        	<div class="question_rtc">
            	<div class="question_rtct"></div>
                <div class="question_rtcb">您当前的位置：<a href="<%=Html.WebRootUrl %>">网站首页</a> &gt; 商户加盟</div>
            </div>
        </div>  
        
      <%=this.MasterPage.Error.Message %>              
      <div class="question_kj">
       	<div class="rgsterc">
        	<div class="rgsterct"></div>
            <div class="rgsterct">
            	<div class="rgsterctwz">设置登录账号和密码：</div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">登录邮箱账号</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                	    <%=Html.InputText("UserName", "UserName", Post("UserName"), "rgstercbmcc", "")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请输入正确的邮箱号。</div>
                </div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">登录密码</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                	    <%=Html.InputText("Password", "Password", "", "rgstercbmcc", "")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">密码必须大于6位。</div>
                </div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">确认密码</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                	    <%=Html.InputText("IsPassword", "IsPassword", "", "rgstercbmcc", "")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">输入有误请重新输入。</div>
                </div>
            </div>
            <div class="rgsterct">
            	<div class="rgsterctwz">填写个人信息：</div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">用户类型</div>
                <div class="rgstercbm">
                  <div class="rgstercbmc">
                      <%=GetHtmlCheckBox()%>
                  </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请选择用户类型。</div>
                </div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">手机/电话</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                	    <%=Html.InputText("ContactTelephone", "ContactTelephone", Post("ContactTelephone"), "rgstercbmcc")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请输入正确的手机/电话号码。</div>
                </div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">QQ号码</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                    	<%=Html.InputText("ContactQQ", "ContactQQ", Post("ContactQQ"), "rgstercbmcc")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请输入正确的QQ号码。</div>
                </div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">商户网站</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                    	<input name="text" class="rgstercbmcc" type="text">
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请输入正确的网站地址。</div>
                </div>
            </div>
            <div class="rgsterct">
            	<div class="rgsterctwz">填写个人信息：</div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">收款人</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                    	<%=Html.InputText("BankRealName", "BankRealName", Post("BankRealName"), "rgstercbmcc", "")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请输入正确的收款人名字。</div>
                </div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">开户银行</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                    	<%=Html.InputText("BankName", "BankName", Post("BankName"), "rgstercbmcc", "")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请选择开户银行。</div>
                </div>
            </div>
            <div class="rgstercb">
            	<div class="rgstercbl">银行账号</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                    	<%=Html.InputText("BankAccount", "BankAccount", Post("BankAccount"), "rgstercbmcc", "")%>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请输入正确的银行账号。</div>
                </div>
            </div>
            
            <div class="rgstercb">
            	<div class="rgstercbl">开户地址</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                    	<div class="select_sc">
                    	    <select name="selProvince" id="selProvince" class="select_scc" onchange="loadCity(this,'selCity');"></select>
                        </div>
                        <div class="select_sc">
                            <select name="selCity" class="select_scc" id="selCity"></select>
                        </div>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请选择开户地址。</div>
                </div>
            </div>
            <div class="select_tianc"></div>
            <div class="rgstercb">
            	<div class="rgstercbl">验证码</div>
                <div class="rgstercbm">
                	<div class="rgstercbmc">
                    	<div class="wbk_middlel">
                        	<%=Html.InputText("CodeValue", "CodeValue", "", "wbk_150")%>
                        </div>
                        <div class="wbk_middler">
                        	<div class="yzpic">
                            	<div class="yzpicbg"><img src="<%=Html.HrefLink("Member", "CheckCode", "")%>" /></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="rgstercbr">
                	<div class="rgstercbrbg"></div>
                	<div class="rgstercbrbgwz">请输入正确的验证码。</div>
                </div>
            </div>
            <div class="fgx_t"></div>
            <div class="fgx_b">
            	<div class="fgx_bbg"></div>
            </div>
            <div class="xieyic">
            <div class="xieyi">
           <div class="xieyil">
           <input name="" value="" checked="checked" type="checkbox"></div>
           <div class="xieyir"><a href="#">已阅读并同意"汇丰支付合作商户电子协议"。</a></div>
           </div>
            </div>
            <div class="rgstercb">
            	<div class="button_ty">
                	<input value="" class="button_tyc" type="submit">
                </div>
            </div>
        </div>
        </div>
    

</form>