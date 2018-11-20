<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminBlockDetails" %>
<script type="text/javascript">
    KE.show({
        id: 'static_html',imageUploadJson: '<%=Html.Url("Static/kindeditor/asp.net/upload_json.ashx") %>'
    });
</script>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a target="navTab" class="add"><span>返回区块列表</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="form" layouth="56">
	    <form action="<%=Html.HrefLink("Core", "AdminBlockDetails", "block_id=" + Get("block_id")) %>" method="post">
	    <%=Html.InputHidden("action", "action", (is_edit ? "save_block" : "insert_block"))%>
	    <table class="form" width="100%">
	        <thead>
	        
	        </thead>
	        
	        <tbody>
	            <tr>
	                <th>区块名称：</th>
	                <td>
	                    <%=Html.InputText("BlockName", "BlockName", block.BlockName, "textInput", block.DisplayStaticHtml ? "" : "disabled=\"disabled\"")%>
	                    <span class="desc">区块系统识别码（建议使用英文表示）。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>区块标题：</th>
	                <td>
	                    <%=Html.InputText("BlockTitle", "BlockTitle", block.Title, "textInput")%>
	                    <span class="desc">区块栏目的标题。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>所属模块：</th>
	                <td>
	                    <%=Easpnet.Modules.Core.CoreHtml.ModuleSelect("module", "module", block.Module, block.DisplayStaticHtml ? "" : "disabled=\"disabled\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>区块类型：</th>
	                <td>
	                    <%=Html.RadioList("blockType", display_html_list, block.DisplayStaticHtml.ToString())%>
		                <span class="desc">若选择静态页面，则网页中值显示下面编辑器中的静态文本。</span>
	                </td>
	            </tr>
	            <tr>
	                <th>静态内容：</th>
	                <td>
	                    <%=Html.TextArea("static_html", "static_html", block.StaticHtml, 16, 100, "style=\"width:100%;\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th>默认框架：</th>
	                <td>
	                    <%=Html.RadioList("UseDefaultFrame", default_frame_html_list, block.UseDefaultFrame.ToString())%>
		                <span class="desc">若使用默认的框架，则在构造区块时，区块内容将被默认的html包裹，这样您可以只是通过修改样式表就可以调整区块的外观。若默认的框架不能满足您的特定外观要求，您可以选择不实用默认框架，这样你需要在区块内容中定义您的样式！</span>
	                </td>
	            </tr>
	            <tr>
	                <th>说明：</th>
	                <td>
	                    <%=Html.TextArea("description", "description", block.Description, 3, 80, "class=\"textInput\"")%>
	                </td>
	            </tr>
	            <tr>
	                <th></th>
	                <td><div class="button"><div class="buttonContent"><button type="submit"><%=T("添加区块") %></button></div></div> </td>
	            </tr>
	        </tbody>
	    </table>
	    </form>
    </div>
</div>