<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Overdue.aspx.cs" Inherits="Overdue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<div>
             <div id="mainmenu"> <!-- Main menu. -->
		        <ul id="navi" class="navi container container_16">
	                <li class="navifirst">
                        <a href="/jobs.aspx">Jobplaneraren</a>
                    </li>
                    <li>
                        <a href="/jobsoverview.aspx">Jobplaneraren - Årsöversikt</a>
                    </li>
                    <li class="selected">
                        <a class="selected" href="/overdue.aspx">Jobplaneraren - Obetalda Hyror</a>
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
<div style="float:left;margin-left:770px;font-weight:bold;font-size:x-large;">
    Obetalda Hyror
</div>
<br /><br />
       
         <div id="subnavdisplay" class="grid_3 alpha" style="">
                   <ul class="nav grid_3 alpha"> 

    <asp:Repeater runat="server" ID="jobs" OnInit="initOverdue"  OnItemDataBound="initJob"> 
        <ItemTemplate>
	                <li><a href="Jobs.aspx?JobID=<%# DataBinder.Eval(Container.DataItem, "JobId")%>"><div style="float:left;margin-left:5px;"> <asp:Literal runat="server" ID="JobStartTime" /> </font> - <font color="red"><asp:Literal runat="server" ID="JobEndTime" /></font></div><br />
                    <div style="float:left;margin-left:5px;"><%# DataBinder.Eval(Container.DataItem, "JobName")%></div>
                    <div style="float:right;margin-right:5px;"><%# DataBinder.Eval(Container.DataItem, "ContactName")%></div>
                    <br />
	                <div style="float:left;margin-left:5px;"><%# DataBinder.Eval(Container.DataItem, "Address")%></div>
                    <br />
	                <div style="font-style:italic;color:#FF2600; font-size:95%;width:310px;float:left;margin-left:5px;margin-right:5px;margin-bottom:4px;border-bottom:1px solid #000000;"><%# DataBinder.Eval(Container.DataItem, "PersonsInThisJob")%> </div>
                    <br />            
                    <div style="float:left;color:#808080; margin-left:5px;margin-right:5px;font-size:95%;margin-bottom:3px;">
                    <asp:Literal ID="info" runat="server" />
                    </div>
                    <asp:Literal runat="server" ID="JobPhone" /> 
                    <div style="clear:both;"></div>
                    <div style="clear:both;"></div>
                    <div style="clear:both;"></div>
                    </a></li>    	                          
        </ItemTemplate>      
    </asp:Repeater>
       </ul>
                    </div>
</asp:Content>


