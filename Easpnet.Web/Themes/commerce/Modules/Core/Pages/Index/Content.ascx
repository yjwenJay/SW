<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.Index" %>

        
<div class="loginc">
	<div class="logincc">
    	<div class="loginccl">
        	<div class="logincclc">
        	    <%--<Easpnet:BlockImport ID="merchant_login_box" BlockName="merchant_login_box" runat="server" />
        	    <Easpnet:BlockImport ID="BillingCheckoutAffiche" BlockName="BillingCheckoutAffiche" runat="server" />--%>
            </div>
        </div>
        <div class="content_m">
        	<div class="content_mt">
            	<div class="content_mtc">
                	<div id="banner">                	    
                        <img src="<%=ImageUrl("banners/1.jpg") %>" width="515" height="200">
                        <img src="<%=ImageUrl("banners/2.jpg") %>" width="515" height="200">
                        <img src="<%=ImageUrl("banners/3.jpg") %>" width="515" height="200">
                    </div>
                </div>
            <div id="btn"></div>
                </div>
                <div class="bank">
                	<div class="bankt"></div>
                    <div class="bankb">
                    	<div class="bankbt">
                        	<div class="bankbtc"><div class="bankbtcbg"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg2"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg3"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg4"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg5"></div></div>
                        </div>
                        <div class="bankbt">
                        	<div class="bankbtc"><div class="bankbtcbg bankcbg6"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg7"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg8"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg9"></div></div>
                            <div class="bankbtc"><div class="bankbtcbg bankcbg10"></div></div>
                        </div>
                    </div>
                </div>
                <div class="showpic"></div>
            </div>
            <div class="content_r">
            	<div class="content_rc">
                	<%--<Easpnet:BlockImport ID="HuifNewsAndAfficheOnIndex" BlockName="HuifNewsAndAfficheOnIndex" runat="server" />--%>
                    <div class="content_rm">
                    	<div class="content_rmt">
                        	<div class="content_rmta"><a href="<%=Html.HrefLink("Huif","SomeQuestions") %>"></a></div>
                        </div>
                        <div class="content_rmc">
                        	<ul>
                            	<li><a href="javascript:void(0)">Q:如何成为汇丰支付合作商户？</a></li>
                                <li>A:先注册成为我们的会员，然后联系客
服开通。</li>
								<li><a href="javascript:void(0)">Q:在线支付的确认信息是实时反馈吗？</a></li>
                                <li>A:目前银行卡在线支付确认信息时都是
实时反馈的。</li>
                            </ul>
                        </div>
                    </div>
                    <Easpnet:BlockImport ID="ServiceContactBox" BlockName="ServiceContactBox" runat="server" />
                </div>
            </div>
        </div>
    </div>
 
<div class="lc_show">
    	<div class="lc_showt">
        	<div class="lc_showtc">
            	<div class="lc_showtcl">
                	<div class="lc_showtcll">
                    	<div class="lc_showtcllwzn"><a href="#">商户指南</a></div>
                        <div class="lc_showtcllwz"><a href="#">申请流程</a></div>
                        <div class="lc_showtcllwz"><a href="#">结算周期</a></div>
                        <div class="lc_showtcllwz"><a href="#">商户信息</a></div>
                    </div>
                    <div class="lc_showtclr"></div>
                </div>
                <div class="zfcp">
                	<div class="lc_showtcll">
                    	<div class="lc_showtcllwzn"><a href="#">支付产品</a></div>
                        <div class="lc_showtcllwz"><a href="#">网银支付</a></div>
                        <div class="lc_showtcllwz"><a href="#">点卡支付</a></div>
                        <div class="lc_showtcllwz"><a href="#">短信声讯</a></div>
                    </div>
                    <div class="zfcpr"></div>
                </div>
                <div class="shjs">
                	<div class="lc_showtcll">
                    	<div class="lc_showtcllwzn"><a href="#">商户结算</a></div>
                        <div class="lc_showtcllwz"><a href="#">结算费率</a></div>
                        <div class="lc_showtcllwz"><a href="#">结算周期</a></div>
                        <div class="lc_showtcllwz"><a href="#">委托结算</a></div>
                    </div>
                    <div class="shjsr"></div>
                </div>
                <div class="ddcx">
                	<div class="lc_showtcll">
                    	<div class="lc_showtcllwzn"><a href="#">订单查询</a></div>
                        <div class="lc_showtcllwz"><a href="#">站内查询</a></div>
                        <div class="lc_showtcllwz"><a href="#">点卡查询</a></div>
                        <div class="lc_showtcllwz"><a href="#">官方查询</a></div>
                    </div>
                    <div class="ddcxr"></div>
                </div>
                <div class="fwdx">
                	<div class="lc_showtcll">
                    	<div class="lc_showtcllwzn"><a href="#">服务对象</a></div>
                        <div class="lc_showtcllwz"><a href="#">个人商户</a></div>
                        <div class="lc_showtcllwz"><a href="#">企业商户</a></div>
                        <div class="lc_showtcllwz"><a href="#">集团商户</a></div>
                    </div>
                    <div class="fwdxr"></div>
                </div>
            </div>
        </div>
    </div>

        
<Easpnet:BlockImport ID="BlockImport3331" Module="Member" BlockName="LatestRegisterMember" runat="server" />