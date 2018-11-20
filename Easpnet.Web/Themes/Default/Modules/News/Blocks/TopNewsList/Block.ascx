<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.News.Blocks.TopNewsList" %>
<div>
<ul>
    <%foreach (Easpnet.Modules.News.Models.Article item in ArticleDataList)
      {%>
         <li><a href="<%=Html.HrefLink("News","ShowArticle","article_id=" + item.ArticleId ) %>"><%=item.Title %></a></li> 
     <%} %>
</ul>
</div>
