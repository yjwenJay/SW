<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Huif.Pages.ServiceCenter" %>

<div class="question_rt">
	<div class="question_rtc">
    	<div class="question_rtct"></div>
        <div class="question_rtcb">您当前的位置：<a href="<%=Html.WebRootUrl %>">网站首页</a> &gt; 客户中心</div>
    </div>
</div>
<div class="server_c">
	<div class="server_ct"></div>
    <div class="server_cb">
    	<div class="server_cbt">
        	<div class="server_cbtm">
            	<div class="server_cbtml"></div>
                <div class="server_cbtmr">
                	<div class="server_cbtmrt">
                    	<div class="server_cbtmrtt">
                        	<div class="server_cbtmrttl"></div>
                            <div class="server_cbtmrttr">客服电话：<%=Configs["site_business_telephone"]%></div>
                        </div>
                    </div>
                    <div class="kfqq">
                    	<div class="kfqqr">
                        	<div class="kfqqrt">
                            	<div class="kfqqrtl">客服QQ</div>
                                <div class="kfqqrtr">
                                	<%foreach (string item in ServiceQQ)
                                    {%>
                                        <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%=item %>&site=qq&menu=yes">
                                            <img border="0" src="http://wpa.qq.com/pa?p=2:<%=item %>:41 &r=0.38539192443750814" alt="点击这里给我发消息" title="点击这里给我发消息">
                                        </a>
                                    <%} %>
                                </div>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="server_cb">
    	<div class="server_cbt">
        	<div class="server_cbtm">
            	<div class="server_cbtmlb"></div>
                <div class="server_cbtmr">
                	<div class="server_cbtmrt">
                    	<div class="server_cbtmrtt">
                        	<div class="server_cbtmrttl"></div>
                            <div class="server_cbtmrttr">商务电话：<%=Configs["site_business_telephone"]%>
                            </div>
                        </div>
                    </div>
                    <div class="kfqq">
                    	<div class="kfqqr">
                        	<div class="kfqqrt">
                            	<div class="kfqqrtl">商务QQ</div>
                                <div class="kfqqrtr">
                                	<div class="kfqqrtrl">
                                    	<div class="kfqqrtrlpic">
                                        	<%foreach (string item in BusinessQQ)
	                                        {%>
                                                <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%=item %>&site=qq&menu=yes">
                                                    <img border="0" src="http://wpa.qq.com/pa?p=2:<%=item %>:41 &r=0.38539192443750814" alt="点击这里给我发消息" title="点击这里给我发消息">
                                                </a>
	                                        <%} %>
                                        </div>
                                    </div>
                                </div>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>  	
