<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Member.Pages.MemberIndex" %>
<div class="middle l ml10">
    <div class="basic l">
        <div class="basic_user l">
            <a href="/stockrider/yourWebsites.action"><img width="90" height="90" src="<%=ImageUrl("userhead.jpg") %>"></a>         
            <p><a href="/qindgfly"><%=u.UserName %></a></p>
            <p class="address">四川省成都市</p>
            <p><a href="/stockrider/editUserProfile.action" class="red2">上传照片改资料</a></p>
        </div>
        <div class="basic_userc l">
        <table width="500" border="0">
                  <tbody><tr>
                    <td width="40" valign="middle" class="b">说话：</td>
                    <td width="350"><textarea onkeyup="javascript:textLimitCheck(this,50);" onblur="javascript:if(this.value=='')this.value='说话将以微博的形式发布在全部动态中，您的好友和关注您的人可随时看到。说话字数限制50字。';" onfocus="clearup('mini');" rows="" cols="" id="mini" name="mini" class="input">说话将以微博的形式发布在全部动态中，您的好友和关注您的人可随时看到。说话字数限制50字。</textarea></td>
                    <td width="91" valign="bottom"><a onclick="javascrit:dosubmitMini();" href="#none" class="submit l"></a></td>
                    <td width="1">&nbsp;</td>
                  </tr>
              </tbody></table>
                <table width="406" border="0" class="bot mt10 l">
                  <tbody><tr>
                    <td width="100" valign="top"><span class="b">等级：</span>幼儿园小班</td>
                    <td width="78" valign="top"><a target="_blank" class="cBlue" href="http://msg.9666.cn/scoreCenter.jsp?nwid=2">什么是等级？</a></td>
                    <td><span class="b">金币：</span></td>
                    <td width="74">0</td>
                    <td width="102"><a target="_blank" class="cBlue" href="http://msg.9666.cn/bank/tobankHall.action?role=2">如何得金币？</a></td>
                  </tr>
                  <tr>
                    <td><span class="b">积分：</span><span>5</span></td>
                    <td><a target="_blank" class="cBlue" href="http://msg.9666.cn/scoreCenter.jsp?nwid=1">如何得积分？</a></td>
                    <td width="43"><span class="b">认证：</span></td>
                      
                    <td width="78" class="rz">
                         <span class="telrz_gray"></span> <a target="_blank" href="/stockrider/editUserProfile.action?type=5" class="cBlue fl">手机未认证</a>     
                    </td>
                    <td width="102" class="rz"><a target="_blank" href="/stockrider/editUserProfile.action?type=5" class="cBlue fl">什么是手机认证？</a></td>
                    
                    
                  </tr>
                  <tr>
                    <td colspan="2"><span class="b">好友：</span><span>0</span></td>
                    <td colspan="3"><span class="b">被关注：</span><span>0</span></td>
                  </tr>
                  <tr>
                    <td colspan="5">
                      <span class="mr10"><em class="b">现金券：</em><em id="myCoupon">0</em>点</span>
                      <a href="http://s.9666.cn/simstock/showCoupon.action?username=qindgfly" class="cBlue mr10">查看明细</a>
                      <a href="http://s.9666.cn/stock/changeCoupon.action" class="cBlue">申请兑现</a></td>
                  </tr>                  
                  <tr>
                    <td class="b" colspan="2">
                        找人：<input type="text" onblur="javascript:if(this.value=='')this.value='输入用户名或昵称';" onfocus="javascript:if(this.value=='输入用户名或昵称')this.value='';" value="输入用户名或昵称" id="_searchUser" name="" class="input3">
                    </td>
                    <td colspan="3"><a onclick="searchUser();" class="search_btn l" href="#none"></a></td>
                  </tr>
              </tbody></table>
                             
              
               <div class="happy r">
                  <p><span class="smailimg l"></span></p>
                  <p class="l">我的股市心情</p>
              </div>
               
              
             
        </div>
    </div>    

    <ul class="tell l mt10">
        <li>•&nbsp;<a target="_blank" href="http://act.9666.cn/hd/20101231/" class="red2">2010岁末最给力的股市直播盛会&mdash;&mdash;做直播，帮股友，赢实盘获奖名单</a></li>
        <li>•&nbsp;<a target="_blank" href="http://act.9666.cn/hd/20101009/" class="cBlue">“高手会客厅”诚邀炒股高手加盟</a></li>
    </ul>
    
    <div style="border-top: 1px solid rgb(217, 217, 217);" class="cbox l mt10">
        <h2 class="cbox_s l">
            <span class="cbox_sl l">我的账户</span>
            <span class="cbox_sr l"></span>
        </h2>
        <div class="cbox01_con l">
        <table width="622" border="0">
              <tbody><tr>
                <td width="200">总资产：100000.00</td>
                <td width="200">总收益率： 0.00%</td>
                <td width="376">本周收益率：0.00%</td>
              </tr>
              <tr>
                <td width="200">总收益排名：41213</td>
                <td width="200">周收益率排名  ：1330</td>
                <td width="376">好友收益率排名：1/1 </td>
              </tr>
            </tbody></table>
        </div>
    </div>
    
    
    <div class="cbox l" id="myStockId">
        <h2 class="cbox_s l">
            <span class="cbox_sl l">我的重仓股</span>
            <span class="cbox_sr l"><a href="#" class="cGray">什么是重仓股？</a></span>
        </h2>
        <div class="cbox02_con l">
            <table width="603" cellspacing="0" cellpadding="0" border="0" class="l">
              <tbody>
                  <tr class="b">
                    <td class="t">名称</td>
                    <td class="t">代码</td>
                    <td class="t">总股数</td>
                    <td class="t">可用股数</td>
                    <td class="t">最新价</td>
                    <td class="t">盈亏成本</td>
                    <td class="t">今日涨幅</td>
                    <td class="t">市值</td>
                    <td class="t">参考盈亏</td>
                    <td class="t">买卖</td>
                    <td class="t">同股牛人</td>
                  </tr>              
                  <tr class="line">    
                      <td>暂时没有持仓股票</td> 
                  </tr>                       
                </tbody>
          </table>
        </div>
    </div>
    
<Easpnet:BlockImport Module="Member" BlockName="MemberMenu" ID="MemberMenu1" runat="server" />    
</div>
