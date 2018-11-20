<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Huif.Pages.NewsList" %>
<div class="question_rt">
        	<div class="question_rtc">
            	<div class="question_rtct"></div>
                <div class="question_rtcb">您当前的位置：<a href="<%=Html.WebRootUrl %>">网站首页</a> > 新闻公告</div>
            </div>
        </div>
        <div class="question_kj">
        <div class="question_rtct"></div>
        <div class="question_kjr">
        	<div class="question_kjrc">
       	  <ul>
       	  
       	    <%
                foreach (ModelBase item in lst)
                {                    
                    Easpnet.Modules.Huif.Models.News news = item as Easpnet.Modules.Huif.Models.News;
                    string cls = "question_color";
                    if (news.AddTime.Date == DateTime.Now.Date)
                    {
                        cls = "question_colorn";
                    }
                    string url = Html.HrefLink("Huif", "NewsShow", "news_id=" + news.NewsId);
            %>
            
                <li class="<%=cls %>"><span><%=news.Top > 0 ? "[置顶] " : "" %><a href="<%=url %>"><%=news.Title %></a></span></li>
                <li><span><%=news.Content.GetSubstring(100, "…[<a href=\"" + url + "\">查看详情</a>]")%></span></li>
                <li></li>
            <%  } %>
          </ul>
          </div>
        </div>	
        </div>
<Easpnet:Pager ID="AspNetPager1" PageSize="1" runat="server" HorizontalAlign="Center"
    AlwaysShow="false" PagingButtonSpacing="8px" OnPageChanged="AspNetPager1_PageChanged"
    ShowCustomInfoSection="Right" UrlPaging="True" Width="100%" ImagePath="~/images"
    PagingButtonType="Text" NumericButtonType="Text" NavigationButtonType="Text"
    ButtonImageExtension="gif" ButtonImageNameExtension="n" DisabledButtonImageNameExtension="g"
    ShowNavigationToolTip="true" UrlPageIndexName="pageindex">
</Easpnet:Pager>        