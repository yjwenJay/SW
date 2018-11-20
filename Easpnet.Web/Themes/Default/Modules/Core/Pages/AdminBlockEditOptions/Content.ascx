<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminBlockEditOptions" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="<%=Html.HrefLink("Core", "AdminBlockManager", "") %>"><span>区块列表</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="form" layouth="56">	    
	    <table  style="width:100%;">
	        <tr>
	            <td style="width:50%;">
	                <table class="list" width="100%">
	                    <thead>
	                        <tr class="trhead2">
	                            <th>属性名称</th>
	                            <th>属性键</th>
	                            <th>录入方式</th>
	                            <th>可选择值</th>
	                            <th>数据约束</th>  
	                            <th>描述</th>   
	                            <th></th>  
	                        </tr>
	                    </thead>
	                    <tbody>
	                        <%if (block.BlockOptionList != null && block.BlockOptionList.Count > 0)
                           {
                               foreach (BlockOption md in block.BlockOptionList)
	                            {%>                       
	                        <tr key="<%=md.OptionKey %>">
	                            <td><%=md.OptionName%></td>
	                            <td><%=md.OptionKey %></td>
	                            <td><%=T(md.InputMethod.ToString()) %></td>
	                            <td>
	                                <%
                                        for (int i = 0; i < md.Values.Count; i++)
                                        {
                                            if (i > 0)
                                            {
                                                Response.Write(",");
                                            }
                                            Response.Write(md.Values[i].Name);                                            
                                        }
                                    %>
	                            </td>
	                            <td></td>
	                            <td><%=md.Description %></td>
	                            <td>
	                                <a href="javascript:void(0);" class="delete_option" confirmText="<%=T("确定删除吗？") %>"><%=T("删除") %></a>
	                                | <a href="javascript:void(0);" class="edit_option"><%=T("编辑") %></a>
	                            </td>
	                        </tr>
	                        <%}                            
                           }%>
	                    </tbody>
	                    
	                </table>
	            </td>
	            <td style="width:50%;">
	                <form id="formEditOption" action="<%=Html.HrefLink("Core", "AdminBlockEditOptions", "block_id=" + Get("block_id")) %>" method="post">
	                <input type="hidden" name="action" value="insert" id="hidAction"/>
	                <table class="form" width="100%"> 
	                    <tbody>
	                        <tr>
	                            <th>属性名称：</th>
	                            <td>
	                                <%=Html.InputText("OptionName", "optionName", "", "textInput valid", "size=\"30\"")%>
	                            </td>
	                        </tr>
	                        <tr>
	                            <th>键名称：</th>
	                            <td>
	                                <%=Html.InputText("key", "key", "", "textInput valid", "size=\"30\"")%>
	                            </td>
	                        </tr>
	                        <tr>
	                            <th>录入方式：</th>
	                            <td>
	                                <%=Html.RadioList("InputMethod", InputMethodList, "")%>
	                            </td>
	                        </tr>
	                        <tr>
	                            <th>可选择值：</th>
	                            <td>
	                                <textarea id="values" name="values" class="textInput" cols="80" rows="5"></textarea>
	                                <div class="desc" style="margin-left:0px;">
	                                    每组值占一行，例如<br />
	                                    是=true<br />
	                                    否=false
	                                </div>
	                            </td>
	                        </tr>
	                        <tr>
	                            <th>数据约束：</th>
	                            <td>
	                                <%=Html.CheckBoxList("InputCheck", InputCheckList, "")%>
	                            </td>
	                        </tr>
	                        <tr>
	                            <th>描述：</th>
	                            <td>
	                                <textarea id="Textarea1" name="description" class="textInput" cols="80" rows="3"></textarea>
	                            </td>
	                        </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th></th>
                                <th>
                                    <div class="button"><div class="buttonContent"><button type="submit"><%=T("提交") %></button></div></div>                                   
                                </th>
                            </tr>
                        </tfoot>
                    </table>
                    </form>
	            </td>
	        </tr>
	    </table>
    </div>
</div>