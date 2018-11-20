<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminPageBlockEdit" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=redirect_url %>" class="add"><span>返回</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
    
    <div class="tableList">
	<form id="formEditOption" action="<%=Html.HrefLink("Core","AdminPageBlockEdit","btype=" + Get("btype"), "pid=" + Get("pid"), "pgid=" + Get("pgid"), "pbid=" + Get("pbid")) %>" method="post">
        <input type="hidden" name="action" value="save_block" id="hidAction"/>
        <table class="form" width="100%"> 
            <tbody>
                <tr class="group">
                    <td colspan="2">区块基本信息</td>
                </tr>
                <tr>
                    <th><%=T("区块名称") %>：</th>
                    <td><%=page_block.BlockName%></td>
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
                    <td><%=Html.InputText("Title", "Title", page_block.Title, "textInput valid", "size=40")%></td>
                </tr>
                <tr>
                    <th><%=T("默认框架") %>：</th>
                    <td><%=Html.RadioList("UseDefaultFrame", yesno, page_block.UseDefaultFrame.ToString())%></td>
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
                    }%>
            </tbody>
            <tfoot>
                <tr>
                    <th></th>
                    <th>
                        <div class="button"><div class="buttonContent"><button type="submit"><%=T("保存区块")%></button></div></div>                                   
                    </th>
                </tr>
            </tfoot>
        </table>
        </form>
        </div>
</div>