<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminLogDetails" %>
<%@ Register Assembly="xacc.propertygrid" Namespace="Xacc" TagPrefix="xacc" %>
<input id="btn" value="显示更改项" type="button" style="clear:both;"/><br /><br />

<form id="form1" runat="server">
<div>
    <xacc:PropertyGrid ID="pg1" runat="server" ShowHelp="True">
    </xacc:PropertyGrid>
</div>
</form>
