<%@ Control Language="C#" AutoEventWireup="true" Inherits="Easpnet.Modules.Core.Pages.AdminPageGroupBlockManage" %>
<div class="pageContent">
    <div class="panelBar">
		<ul class="toolBar">
			<li><a href="javascript:void(0)" id="save_page"><span>保存布局</span></a></li>
			<li class="line">line</li>		
			<li><a href="<%=Html.HrefLink("Core","AdminPageGroupManager", "")%>" id="A1"><span>取消</span></a></li>	
		</ul>
	</div>

    <form action="<%=Html.HrefLink("Core", "AdminPageGroupBlockManage", "pgid=" + Get("pgid")) %>" method="post" id="form-main">	
    <input type="hidden" name="action" value="save_page" />
    <div class="edit-mainbody">
        <div class="column edit-column-top" btype="top">
            <h2>
                <span>
                <ul class="jd_menu">
	                <li class="accessible"><a href="javascript:void(0);" class="accessible">顶部栏目 &raquo;</a>
		                <ul>
			                <%if (page.DisplayTopBox)
                              {%>
                            <li class="edit_column_visibility" action="close">关闭栏目</li>
                              <%}
                              else
                              {%>
                            <li class="edit_column_visibility" action="open">打开栏目</li>      
                             <% }%>
					        <li><a href="<%=Html.HrefLink("Core","AdminPageBlockAdd","btype=top" + "&pgid=" + Get("pgid"))%>">添加区块</a></li>
		                </ul>
	                </li>
                </ul>
                </span>
            </h2>
            <%=BlockItemHtml(page.PageBlockListTop, page.TopBoxesList, "top")%>
        </div>
    </div>
        
    <div class="edit-mainbody">
        <div class="column edit-column-left" btype="left">
            <h2>
                <span>
                <ul class="jd_menu">
		            <li class="accessible"><a href="javascript:void(0);" class="accessible">左侧栏目 &raquo;</a>
			            <ul>
				            <%if (page.DisplayLeftBox)
                              {%>
                            <li class="edit_column_visibility" action="close">关闭栏目</li>
                              <%}
                              else
                              {%>
                            <li class="edit_column_visibility" action="open">打开栏目</li>      
                             <% }%>
					        <li><a href="<%=Html.HrefLink("Core","AdminPageBlockAdd","btype=left" + "&pgid=" + Get("pgid"))%>">添加区块</a></li>
			            </ul>
		            </li>
	            </ul>
	            </span>
	        </h2>
            <%=BlockItemHtml(page.PageBlockListLeft, page.LeftBoxesList, "left")%>
        </div>
        <div class="column edit-column-center">
            
        </div>
        <div class="column edit-column-right column-closed" btype="right">
            <h2>
                <span>
                <ul class="jd_menu">
			        <li class="accessible"><a href="javascript:void(0);" class="accessible">右侧栏目 &raquo;</a>
				        <ul>
				            <%if (page.DisplayRightBox)
                              {%>
                            <li class="edit_column_visibility" action="close">关闭栏目</li>
                              <%}
                              else
                              {%>
                            <li class="edit_column_visibility" action="open">打开栏目</li>      
                             <% }%>
					        <li><a href="<%=Html.HrefLink("Core","AdminPageBlockAdd","btype=right" + "&pgid=" + Get("pgid"))%>">添加区块</a></li>
				        </ul>
			        </li>
		        </ul>
		        </span>
            </h2>
            <%=BlockItemHtml(page.PageBlockListRight, page.RightBoxesList, "right")%>
        </div>
        <div class="c"></div>
    </div>
    
    <div class="edit-mainbody">
        <div class="column edit-column-bottom" btype="bottom">
            <h2>
                <span>
                <ul class="jd_menu">
	                <li class="accessible"><a href="javascript:void(0);" class="accessible">底部栏目 &raquo;</a>
		                <ul>
			                <%if (page.DisplayBottomBox)
                              {%>
                            <li class="edit_column_visibility" action="close">关闭栏目</li>
                              <%}
                              else
                              {%>
                            <li class="edit_column_visibility" action="open">打开栏目</li>      
                             <% }%>
					        <li><a href="<%=Html.HrefLink("Core","AdminPageBlockAdd","btype=bottom" + "&pgid=" + Get("pgid"))%>">添加区块</a></li>
		                </ul>
	                </li>
                </ul>
                </span>
            </h2>
            <%=BlockItemHtml(page.PageBlockListBottom, page.BottomBoxesList, "bottom")%>
        </div>
    </div>

		
    </form>

</div>
<input type="hidden" id="hidOpenCloseBoxUrl" value="<%=Html.HrefLink("Core", "AdminPageManager", "action=set_box_status") %>" />





