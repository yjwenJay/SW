<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminLogList" %>
<fieldset class="search">
    <form method="get" action="<%=Html.HrefLink("") %>">
        类型：<%=Html.SelectEnums("level", "level", typeof(LogLevel), "", Get("level"), "auto=\"true\"")%> 
        类别：<%=Html.Select("title", "title", lstTitle, Get("title"), "auto=\"true\"")%>
        用户：<%=Html.Select("manager", "manager", lstUser, Get("manager"), "auto=\"true\"")%>
        数据操作：<%=Html.SelectEnums("operation", "operation", typeof(System.LogOperation), "", Get("operation"), "auto=\"true\"")%> 
        关联ID：<%=Html.InputText("primaryData","primaryData",Get("primaryData"),"") %>
        <input type="submit" value="查询日志"/>
    </form>
</fieldset>
<div class="dataList">        
        <table class="list" style="width: 100%;" cellpadding="0" cellspacing="0">
            <thead>
                <tr>
                    <th style="display: none;">
                    </th>
                    <th>日志类别</th>
                    <th>日志类型</th>
                    <th>消息</th>
                    <th>数据操作</th>
                    <th>关联ID</th>
                    <th>用户</th>
                    <th>时间</th>
                </tr>
            </thead>
            <%if (lst.Count > 0)
              {
                   %>
            <tbody>
                <%               
                for (int i = 0; i < lst.Count; i++)
                {
                    System.Log item = lst[i] as System.Log;
                    string rowClass = i % 2 == 0 ? "row" : "row rowAlter";          
                %>
                <tr class="<%=rowClass %>" tid="<%=item.LogId %>">
                    <th style="display: none;">
                        <input type="checkbox" name="ids" value="<%=item.LogId %>" />
                    </th>
                    <th valign="middle"><%=item.Title%></th>
                    <td><span class="<%=item.Level.ToString() %>"><%=T(item.Level.ToString()) %></span></td>
                    <td><%=item.Message %></td>
                    <td><%=T(item.Operation.ToString()) %></td>
                    <td><%=item.PrimaryData %></td>
                    <td><%=item.UserName %></td>
                    <td><%=DateTimeUtility.DisplayTime(item.DateTime)  %></td> 
                </tr>
                <%} %>
            </tbody>
            <%}
              else
              {%>
            <tbody class="not-found" >
                <tr>
                    <td colspan="50">未找到数据</td>
                </tr>
            </tbody>
            <% } %>
            <tfoot>
                <tr class="pager">
                    <td colspan="50">
                        <Easpnet:Pager ID="AspNetPager1" PageSize="1" runat="server" HorizontalAlign="Center"
                            AlwaysShow="false" PagingButtonSpacing="8px" OnPageChanged="AspNetPager1_PageChanged"
                            ShowCustomInfoSection="Right" UrlPaging="True" Width="100%" PagingButtonType="Text"
                            NumericButtonType="Text" NavigationButtonType="Text" ButtonImageExtension="gif" EnableUrlRewriting="false"
                            ButtonImageNameExtension="n" DisabledButtonImageNameExtension="g" ShowNavigationToolTip="true"
                            UrlPageIndexName="pageindex">
                        </Easpnet:Pager>
                    </td>
                </tr>
            </tfoot>
        </table>
</div>
