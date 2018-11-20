<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminPageDetails" %>
<script type="text/javascript">
    KE.show({
        id: 'static_html',imageUploadJson: '<%=Html.Url("Static/kindeditor/asp.net/upload_json.ashx") %>'
    });
</script>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a target="navTab" class="add"><span>返回页面列表</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="form" layouth="56">
	    <form action="<%=Html.HrefLink("Core", "AdminPageDetails", "pid=" + Get("pid")) %>" method="post">
	    <%=Html.InputHidden("action", "action", (is_edit ? "save_page" : "insert_page"))%>
	    <table class="form" width="100%">
	        <thead>
	        
	        </thead>
	        
	        <tbody>
	            <tr>
	                <th>页面名称：</th>
	                <td>
	                    <%=Html.InputText("PageName", "PageName", page.PageName, "textInput", "size=50", page.DisplayStaticHtml ? "" : "readonly=\"readonly\"")%>
	                    <span class="desc">系统识别码（建议使用英文表示）。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>所属模块：</th>
	                <td>
	                    <%=Easpnet.Modules.Core.CoreHtml.ModuleSelect("module", "module", page.Module, page.DisplayStaticHtml ? "" : "disabled=\"disabled\"")%>
	                    <%=Html.CheckBox("isAdminPage","isAdminPage","True", page.IsAdminPage) %> <label for="isAdminPage">是后台管理页面</label>
	                </td>
	            </tr>
	            <tr>
	                <th>页面组：</th>
	                <td>
	                    <%=Html.InputText("PageGroupName", "PageGroupName", page.PageGroupName, "textInput", "size=50")%>
	                </td>
	            </tr>
	            <tr>
	                <th>页面类型：</th>
	                <td>
	                    <%=Html.RadioList("pageType", display_html_list, page.DisplayStaticHtml.ToString())%>
		                <span class="desc">若选择静态页面，则网页中值显示下面编辑器中的静态文本。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>静态内容：</th>
	                <td>
	                    <%=Html.TextArea("static_html", "static_html", page.StaticHtml, 16, 100, "style=\"width:100%;\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>Meta Title：</th>
	                <td>
	                    <%=Html.TextArea("MetaTitle", "MetaTitle", page.MetaTitle, 1, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>Meta Keywords：</th>
	                <td>
	                    <%=Html.TextArea("MetaKeywords", "MetaKeywords", page.MetaKeywords, 3, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>Meta Description：</th>
	                <td>
	                    <%=Html.TextArea("MetaDescription", "MetaDescription", page.MetaDescription, 3, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>CSS样式：</th>
	                <td>
	                    <%=Html.TextArea("Style", "Style", page.Style, 3, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>说明：</th>
	                <td>
	                    <%=Html.TextArea("Remark", "Remark", page.Remark, 3, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th></th>
	                <td><div class="button"><div class="buttonContent"><button type="submit"><%= is_edit ? T("保存页面") : T("添加页面") %></button></div></div> </td>
	            </tr>
	        </tbody>
	    </table>
	    </form>
    </div>
</div>