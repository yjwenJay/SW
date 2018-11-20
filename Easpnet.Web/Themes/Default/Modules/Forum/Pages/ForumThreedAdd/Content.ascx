<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Forum.Pages.ForumThreedAdd" %>
<%=res.Message %>
<form id="formEditOption" action="<%=Html.HrefLink("Forum","ForumThreedAdd","id="+Get("id")) %>" method="post">
   <input type="hidden" name="action" value="<%=isedit ? "update" : "add" %>" id="hidAction"/>
   标题：<%=Html.InputText("", "title", md.Title, "")%><br />
   内容：<%=Html.TextArea("","body",md.Body,10, 50) %><br />
   <%=Html.Submit("", "submittt", isedit ? "更新" : "添加")%>
</form>

<ul>
<%foreach (ModelBase item in lst)
  {
      Easpnet.Modules.Forum.Models.Theed th = item as Easpnet.Modules.Forum.Models.Theed;
 %>     
     <li><%=th.Title %> [<%=th.AddTime %>]</li> 
 <% } %>
</ul>