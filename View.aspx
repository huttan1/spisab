<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta http-equiv="refresh" content="30">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
<div style="float:left;margin-left:770px;font-weight:bold;font-size:x-large;">
    <asp:Label ID="weeknumber" runat="server" />
</div>
<br /><br />
<asp:Repeater runat="server" ID="jobRepeater" OnLoad="GetAllJobs" OnItemDataBound="initJobs">
<ItemTemplate>
        
         <div id="subnavdisplay" class="grid_3 alpha" style="">
         <div style="background-color:Red;">
                  <asp:Label CssClass="LabelDay" runat="server" ID="day" />
         </div>
                   <ul class="nav grid_3 alpha"> 
    <asp:Repeater runat="server" ID="jobs" OnItemDataBound="initJob"> 
        <ItemTemplate>
	                <li><a href="<%# DataBinder.Eval(Container.DataItem, "JobId")%>"><div style="float:left;margin-left:5px;"> <asp:Literal runat="server" ID="JobStartTime" /> </font> - <font color="red"><asp:Literal runat="server" ID="JobEndTime" /></font></div><div style="float:right;margin-right:5px;">Bil: <asp:Literal runat="server" ID="JobCar" /></div><br />
                    <div style="float:left;margin-left:5px;"><%# DataBinder.Eval(Container.DataItem, "JobName")%></div>
                    <div style="float:right;margin-right:5px;"><%# DataBinder.Eval(Container.DataItem, "ContactName")%></div>
                    <br />
	                <div style="float:left;margin-left:5px;"><%# DataBinder.Eval(Container.DataItem, "Address")%></div>
                    <br />
	                <div style="font-style:italic;color:#FF2600; font-size:95%;width:310px;float:left;margin-left:5px;margin-right:5px;margin-bottom:4px;border-bottom:1px solid #000000;"><%# DataBinder.Eval(Container.DataItem, "PersonsInThisJob")%> </div>
                    <br />            
                    <div style="float:left;color:#808080; margin-left:5px;margin-right:5px;font-size:95%;margin-bottom:3px;">
                    <%# DataBinder.Eval(Container.DataItem, "Comments")%>
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
</ItemTemplate>
</asp:Repeater>
</asp:Content>


