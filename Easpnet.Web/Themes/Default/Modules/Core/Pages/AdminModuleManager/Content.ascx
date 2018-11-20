<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminModuleManager" %>


<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a target="navTab" href="demo_page4.html" class="add"><span>添加新模块</span></a></li>
			<li class="line">line</li>			
		</ul>
	</div>
		
	<div class="tableList">
	    <table class="list" width="100%">
	        <thead>
	            <tr>
	                <th>模块名称</th>
	                <th>版本</th>
	                <th>依赖项</th>
	                <th>程序集</th>
	                <th></th>
	            </tr>
	        </thead>
	        <tbody>
        <%foreach (ModelBase item in modules)
          {
              Easpnet.Modules.Models.Module md = item as Easpnet.Modules.Models.Module;
              %>
              <tr>
                    <td>
                        <h4><%=md.ModuleName %></h4>
                        <div class="desc"><%=md.Description %></div>
                    </td>
	                <td><%=md.Version %></td>
	                <td><%=md.Dependence %></td>
	                <td><%=md.AssemblyName %></td>
	                <td>[ 卸载 ]</td>
              </tr>
        <%  } %>
        
            </tbody>
        </table>
    </div>


</div>