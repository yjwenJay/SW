<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminPageBlockAdd" %>
<div class="pageContent">
    
		
   <%
   /// 若没有选择区块，则显示选择区块的列表
   if (!block_selected)
   {%>       
    <div class="panelBar">
        <ul class="toolBar">
			<li>请选择要添加的区块</li>		
		</ul>
		
	</div>
	<div class="tableList">
	    <table class="list" width="100%">
	        <thead>
	            <tr class="trhead1">
	                <th>区块名称</th>
	                <th>模块</th>
	                <th>区块类型</th>
	                <th>区块标题</th>
	                <th>默认框架</th>	                
	                <th></th>
	            </tr>
	        </thead>
	        <tbody>
        <%foreach (Easpnet.Modules.Models.BlockInfo md in blocks)
          {
              %>
              <tr itemid="<%=md.BlockId %>">
                    <td>
                        <h4><%=md.BlockName%></h4>
                    </td>
                    <td><%=md.Module %></td>
                    <td><%=md.DisplayStaticHtml ? T("静态") : T("常规") %></td>
	                <td><%=md.Title %></td>
	                <td><%=md.UseDefaultFrame ? T("是") : T("否") %></td>
	                <td><a href="<%=Html.HrefLink("Core","AdminPageBlockAdd","btype=" + Get("btype"), "pid=" + Get("pid"),"pgid=" + Get("pgid"), "bid=" + md.BlockId) %>"><%=T("选择") %></a></td>
              </tr>
        <%  } %>
        
            </tbody>
        </table>
    </div>
   <%}
   else
   {%>
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=Html.HrefLink("Core","AdminPageBlockAdd","btype=" + Get("btype"), "pid=" + Get("pid"),"pgid=" + Get("pgid")) %>" class="add"><span>重新选择区块</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
	<div class="tableList">
	<form id="formEditOption" action="<%=Html.HrefLink("Core","AdminPageBlockAdd","btype=" + Get("btype"),"pid=" + Get("pid"),"pgid=" + Get("pgid"), "bid=" + block.BlockId) %>" method="post">
        <input type="hidden" name="action" value="add_block" id="hidAction"/>
        <table class="form" width="100%"> 
            <tbody>
                <tr class="group">
                    <td colspan="2">区块基本信息</td>
                </tr>
                <tr>
                    <th><%=T("区块名称") %>：</th>
                    <td><%=block.BlockName %></td>
                </tr>
                <tr>
                    <th><%=T("说明") %>：</th>
                    <td><%=block.Description %></td>
                </tr>
                <tr class="group">
                    <td colspan="2">区块设置</td>
                </tr>
                <tr>
                    <th><%=T("区块标题") %>：</th>
                    <td><%=Html.InputText("Title", "Title", block.Title, "textInput valid", "size=40")%></td>
                </tr>
                <tr>
                    <th><%=T("默认框架") %>：</th>
                    <td><%=Html.RadioList("UseDefaultFrame", yesno, block.UseDefaultFrame.ToString())%></td>
                </tr>
                
                <%
                    if (block.BlockOptionList != null)
                    {
                        foreach (BlockOption item in block.BlockOptionList)
                        {%>
                    <tr>
                        <th><%=T(item.OptionName)%>：</th>
                        <td>
                            <%=PopulateBlockOptionInput(item)%>
                            <%if (!string.IsNullOrEmpty(item.Description))
                              {%>
                                  <span class="desc"><%=T(item.Description)%></span>
                            <%} %>
                        </td>
                    </tr>
                <%      }
                    } %>
            </tbody>
            <tfoot>
                <tr>
                    <th></th>
                    <th>
                        <div class="button"><div class="buttonContent"><button type="submit"><%=T("添加") %></button></div></div>                                   
                    </th>
                </tr>
            </tfoot>
        </table>
        </form>
        </div>
   <%} %>
    
    
</div>