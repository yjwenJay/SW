<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminConfigManage" %>

<%=SuccessMessage %>

<div class="pageContent">		
	<div class="form" layouth="56">
	    <form action="<%=Html.HrefLink("Core", "AdminConfigManage", "type=" + Get("type")) %>" method="post" enctype="multipart/form-data">
	    <%=Html.InputHidden("action", "action", "save")%>
	    <table class="form" width="100%">
	        <thead>	        
	        </thead>	        
	        <tbody>
	            <%foreach (ModelBase item in configs)
               {
                   Easpnet.Modules.Models.Config md = item as Easpnet.Modules.Models.Config;
               %>                   
	            <tr>
	                <th><%=T(md.ConfigName) %>：</th>
	                <td>
	                    <%=PopulateFormItem(md)%>
	                    <span class="desc"><%=T(md.Remark) %></span>
	                </td>
	            </tr>
               <%} %>
	            <tr>
	                <th></th>
	                <td><div class="button"><div class="buttonContent"><button type="submit"><%=T("保存设置") %></button></div></div> </td>
	            </tr>
	        </tbody>
	    </table>
	    </form>
	    
	    
    </div>
</div>
