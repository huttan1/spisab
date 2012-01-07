<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JobsOverview.aspx.cs" Inherits="JobsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<div>
             <div id="mainmenu"> <!-- Main menu. -->
		        <ul id="navi" class="navi container container_16">
	                <li class="navifirst">
                        <a href="/jobs.aspx">Jobplaneraren</a>
                    </li>
                    <li>
                        <a class="selected" href="/jobsoverview.aspx">Jobplaneraren - Årsöversikt</a>
                    </li>
                    <li>
                        <a href="/overdue.aspx">Jobplaneraren - Obetalda Hyror</a>
                    </li>
                                        <li>
                        <a href="/view.aspx">Jobplaneraren - Display</a>
                    </li>
                    <li>
                        <a href="/admin.aspx">Jobplaneraren - ADMIN</a>
                    </li>
                </ul>
            </div>

            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

<div style="float:left">
<input onserverclick="PreviousYearLink" id="Previous" runat="server" value="Föregående år" type="button" class="litInputButton litInputButtonRemove" />
</div>
<div style="float:left;margin-left:270px;font-weight:bold;font-size:large;">
<asp:Label ID="weeknumber" runat="server" />
</div>
<div style="float:right;margin-right:360px;">
<input onserverclick="NextYearLink" id="next" runat="server" value="Nästa år" type="button" class="litInputButton litInputButtonRemove" />
</div>
<br /><br />
<hr style="width:975px;float:left;"/>
<br />
<asp:Repeater runat="server" ID="jobRepeater" OnLoad="GetAllMonths" OnItemDataBound="initJobForMonth">
<ItemTemplate>
        
         <div id="subnav" class="grid_3 alpha" style="width:140px;margin-right:0px;">
                  <asp:Label CssClass="LabelDayAdmin" runat="server" ID="month" />
                  <div class="PersonalBox">                  
                   </div>
                   <br />
                   <ul class="nav grid_3 alpha"> 
    <asp:Repeater runat="server" ID="jobs" OnItemDataBound="InitJob"> 
        <ItemTemplate>
        <div style="width:140px;height:57px;">
	              <li id="width" runat="server"><a class="" href="Jobs.aspx?JobID=<%# DataBinder.Eval(Container.DataItem, "JobId")%>"><%# DataBinder.Eval(Container.DataItem, "JobName")%><br />
	                    <div style="margin-bottom:3px;"> <%# DataBinder.Eval(Container.DataItem, "Address")%></div>	
                  </li></a>
                  </div>
                    	      
        </ItemTemplate>      
    </asp:Repeater>
       </ul>
                    </div>
</ItemTemplate>
</asp:Repeater>
</asp:Content>

