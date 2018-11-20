<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminThemeManage" %>
<div class="pageContent">	
	<div class="tableList">
	<form action="<%=Html.HrefLink("Core", "AdminThemeManage", "") %>" method="post">
	    <%=Html.InputHidden("action", "action", "save")%>
	    <table class="list" width="100%">
	        <thead>
	            <tr>
	                <th style="display:none;"></th>
	                <th>模板名称</th>
	                <th>当前模板</th>
	                <th>模板路径</th>
	                <th>预览</th>
	                <th>程序版本</th>
	                <th>作者</th>
	                
	                <th></th>
	            </tr>
	        </thead>
	        <tbody>
        <%foreach (ModelBase item in themes)
          {
              Easpnet.Modules.Models.Theme md = item as Easpnet.Modules.Models.Theme;
              %>
              <tr itemid="<%=md.ThemeId %>">
                    <td style="display:none;">
                        <%=Html.Radio("selectedTheme", "theme-" + md.DirectoryName, md.ThemeId.ToString(), md.IsCurrentTheme) %>
                    </td>
                    <td>
                        <h4><%=md.ThemeName%></h4>
                        <div class="desc"><%=md.Description%></div>
                    </td>
                    <td><%=md.IsCurrentTheme ? T("是") : "" %></td>                    
                    <td>Themes/<%=md.DirectoryName %></td>
	                <td><%=Html.Image("Themes/" + md.DirectoryName + "/" + md.Snapshot, 100, 120, md.ThemeName)%></td>
	                <td><%=md.CoreVersion %></td>
                    <td><%=md.Author %></td>
                    <td>
                        <%=Html.Submit("usetheme-" + md.DirectoryName, "usetheme", T("应用模板"), "fortheme=\"" + md.DirectoryName + "\"")%>
                    </td>
              </tr>
        <%  } %>
        
            </tbody>
        </table>
    </form>
    </div>
</div>
