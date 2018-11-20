<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AjaxRequestTst.aspx.cs" Inherits="Test_JqueryPlus_jquery_impromptu_AjaxRequestTst_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" media="all" type="text/css" href="css/examples.css" />		
<script charset="utf-8" src="jquery-1.2.6.js" type="text/javascript"></script>
<script charset="utf-8" type="text/javascript" src="impromptu.js"></script>
<script charset="utf-8" type="text/javascript" >
function Search()
    {        
        $.prompt("Search conditions", 
            {
            url: 'html/GoldOrderSearch.htm',
            width: '350',
            height: '280'
            });
    }
    
    function Edit()
    {
         $.prompt("Modify the order form", 
            { 
            url: 'html/GoldOrderEdit.htm',
            width: '580',
            height: '440',
            buttons: {Update: "update", Insert: "insert"},
            submit: submit
            });
    }
    
    function Handler()
    {
        $.prompt("View order form", 
            { 
            url: 'html/GoldOrderHandle.htm',
            width: '580',
            height: '360',
            buttons: {Update: "update", Insert: "insert"},
            submit: submit
            });
    }
    
    function Delete()
    {
         $.prompt("确认删除", 
            { 
            message: '真的删除该项吗?',
            buttons: {Yes: "Yes", No: "No"}
            });
    }
    
    function submit(msg)
    {
        if(msg=="update")
        {
            return true;
        }
        else if(msg=="insert")
        {            
           return false;
        }
        else if(msg=="")
        {
            
        }
        
        
    }
    
</script>
		
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <input id="Button1" type="button" value="Search" onclick="Search();"/>
    <input id="Button2" type="button" value="Edit" onclick="Edit();"/>
    <input id="Button3" onclick="Handler();" type="button" value="Handler" />
        <input id="Button4" onclick="Delete();" type="button" value="Delete" /><br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink19" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink21" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink22" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink23" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink24" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink25" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink26" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink27" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink28" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink30" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink31" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink32" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink33" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink34" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink35" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink36" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink37" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink38" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink39" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink40" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink41" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink42" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink43" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink44" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink45" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink46" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink47" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink48" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink49" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink50" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink51" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink52" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink53" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink54" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink55" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink56" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink57" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink58" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink59" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink60" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink61" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink62" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink63" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink64" runat="server" NavigateUrl="#">HyperLink</asp:HyperLink><br />
    </div>
    </form>
</body>
</html>
