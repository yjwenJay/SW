<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Huif.Pages.NewsShow" %>
<div class="question_rt">
	<div class="question_rtc">
    	<div class="question_rtct"></div>
        <div class="question_rtcb">您当前的位置：<a href="<%=Html.WebRootUrl %>">网站首页</a> > 
            <a href="<%= Html.HrefLink("Huif", "NewsList") %>">新闻公告</a></div>
    </div>
</div>
<div class="question_kj">
<div class="question_rtct"></div>
<div class="news_c"><%=md_news.Title %></div>
<div class="news_ct">作者：<%=Configs["site_name"] %>   日期：<%=md_news.AddTime %></div>	
<div class="news_content">        
    <div>
        <%=md_news.Content %>
    </div>
    <div class="fanye">
        <%if (has_preview)
          {%>
            <div class="fanyet">上一篇：<span class="fanyet_color"><a href="<%= Html.HrefLink("Huif", "NewsShow", "news_id=" + md_preview.NewsId) %>"><%=md_preview.Title %></a></span></div>    
        <%} %>
    	<%if (has_next)
          {%>
            <div class="fanyet">下一篇：<span class="fanyet_color"><a href="<%= Html.HrefLink("Huif", "NewsShow", "news_id=" + md_next.NewsId) %>"><%=md_next.Title%></a></span></div>
        <%} %>
    </div>
</div>
</div>
       