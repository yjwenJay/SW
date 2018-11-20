<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminBlockManager" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=Html.HrefLink("Core", "AdminBlockDetails", "") %>" class="add"><span>添加静态区块</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="tableList">
	    <table class="list" width="100%">
	        <thead>
	            <tr>
	                <th>区块名称</th>
	                <th>区块类型</th>
	                <th>所属模块</th>
	                <th>区块标题</th>
	                <th>默认框架</th>
	                
	                <th></th>
	            </tr>
	        </thead>
	        <tbody>
        <%foreach (ModelBase item in blocks)
          {
              Easpnet.Modules.Models.BlockInfo md = item as Easpnet.Modules.Models.BlockInfo;
              %>
              <tr itemid="<%=md.BlockId %>">
                    <td>
                        <h4><%=md.BlockName%></h4>
                        <div class="desc"><%=md.Description%></div>
                    </td>
                    <td><%=md.DisplayStaticHtml ? "<span class=\"static_html\">" + T("静态") + "</span>" : "常规"%></td>
	                <td><%=md.Module %></td>
	                <td><%=md.Title %></td>
                    <td><%=md.UseDefaultFrame ? "是" : "" %></td>
                    <td>
                        <a class="edit" href="<%=Html.HrefLink("Core", "AdminBlockDetails", "block_id=" + md.BlockId) %>"><span><%=T("编辑区块") %></span></a> | 
                        <a href="<%=Html.HrefLink("Core", "AdminBlockEditOptions", "block_id=" + md.BlockId) %>"><%=T("编辑选项") %></a>
                    </td>
              </tr>
        <%  } %>
        
            </tbody>
        </table>
    </div>
</div>