<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminPageGroupDetails" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a target="navTab" class="add"><span>取消</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="form" layouth="56">
	    <form action="<%=Html.HrefLink("Core", "AdminPageGroupDetails", "pgid=" + Get("pgid")) %>" method="post">
	    <%=Html.InputHidden("action", "action", (is_edit ? "save" : "insert"))%>
	    <table class="form" width="100%">
	        <thead>
	        
	        </thead>
	        
	        <tbody>
	            <tr>
	                <th>页面分组名称：</th>
	                <td>
	                    <%=Html.InputText("PageGroupName", "PageGroupName", group.PageGroupName, "textInput", "size=50")%>
	                    <span class="desc">系统识别码（建议使用英文表示）。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>Meta Title：</th>
	                <td>
	                    <%=Html.TextArea("MetaTitle", "MetaTitle", group.MetaTitle, 1, 80, "class=\"textInput\"")%>
	                    <span class="desc">网页标题设置，将作为网页标题的一部分。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>Meta Keywords：</th>
	                <td>
	                    <%=Html.TextArea("MetaKeywords", "MetaKeywords", group.MetaKeywords, 3, 80, "class=\"textInput\"")%>
	                    <span class="desc">网页关键字设置，将作为网页关键字的一部分。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>Meta Description：</th>
	                <td>
	                    <%=Html.TextArea("MetaDescription", "MetaDescription", group.MetaDescription, 3, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>CSS样式：</th>
	                <td>
	                    <%=Html.TextArea("Style", "Style", group.Style, 8, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>说明：</th>
	                <td>
	                    <%=Html.TextArea("Remark", "Remark", group.Remark, 3, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th></th>
	                <td><div class="button"><div class="buttonContent"><button type="submit"><%= is_edit ? T("保存") : T("添加") %></button></div></div> </td>
	            </tr>
	        </tbody>