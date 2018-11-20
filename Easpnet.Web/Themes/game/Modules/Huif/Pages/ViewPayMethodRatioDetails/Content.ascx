<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Huif.Pages.ViewPayMethodRatioDetails" %>
<div class="question_rt">
    <div class="question_rtc">
        <div class="question_rtct">
        </div>
        <div class="question_rtcb">
            您当前的位置：<a href="<%=Html.WebRootUrl %>">网站首页</a> &gt; 佣金比例</div>
    </div>
</div>
<div class="question_kj">
    <div class="news_c">
    </div>
    <div class="yjbl_t">
        <div class="yjbl_tbg">
        </div>
        <div class="yjbl_tbgr">
            结算方式早上8点-晚上10点 随提随结-无节假日！“请提防无注册资金的个人平台利用一个长假期黑单关站让您损失惨重。”</div>
    </div>
    <div class="bl_content">
        <table cellpadding="0px" cellspacing="0px">
            <tbody>
                <tr class="bl_cbg">
                    <td class="bl_ccolor" width="132">
                        充值渠道
                    </td>
                    <td class="bl_ccolor" width="344">
                        资费标准
                    </td>
                    <td class="bl_ccolor" width="86">
                        商户所得
                    </td>
                    <td class="bl_ccolor" width="177">
                        结算周期
                    </td>
                </tr>
                <%foreach (HuifPayMethod.Pay p in pay.PayPethodList)
                  {%>
                <tr>
                    <td class="bl_cbg"><%=p.PayMethodName %></td>
                    <td><%=p.Description %></td>
                    <td><%=p.MerchantRatio.ToString("0.##") %>%</td>
                    <td><%=p.CheckoutMethod %></td>
                </tr>                      
                <% } %>
            </tbody>
        </table>
    </div>
</div>
