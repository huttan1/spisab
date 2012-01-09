<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Jobs.aspx.cs" Inherits="Jobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<div>
             <div id="mainmenu"> <!-- Main menu. -->
		        <ul id="navi" class="navi container container_16">
	                <li class="navifirst">
                        <a class="selected" href="/jobs.aspx">Jobplaneraren</a>
                    </li>
                    <li>
                        <a href="/jobsoverview.aspx">Jobplaneraren - Årsöversikt</a>
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
<input onserverclick="PreviousWeekLink" id="Previous" runat="server" value="Föregående vecka" type="button" class="litInputButton litInputButtonRemove" />
</div>
<div style="float:left;margin-left:270px;font-weight:bold;font-size:large;">
<asp:Label ID="weeknumber" runat="server" />
</div>
<div style="float:right;margin-right:360px;">
<input onserverclick="NextWeekLink" id="next" runat="server" value="Nästa vecka" type="button" class="litInputButton litInputButtonRemove" />
</div>
<br /><br />
<hr style="width:975px;float:left;"/>
<br />

<asp:Panel ID="NoDate" runat="server"  OnLoad="GetAllNewJobs">	
<div id="subnav" class="grid_3 alpha">
<b>
<asp:Label CssClass="LabelDayAdmin" runat="server" ID="NoDateLabel"></asp:Label>
<br />
<div class="PersonalBox" style="visibility:hidden;">
<asp:Label CssClass="Personal" runat="server" ID="Label2"></asp:Label>
</div>
	<ul class="nav grid_3 alpha">
   <asp:Repeater ID="NoDayRepeater" runat="server" OnItemDataBound="InitJob">
	<ItemTemplate>
	<li id="width" runat="server"><a class="" href="Jobs.aspx?JobID=<%# DataBinder.Eval(Container.DataItem, "JobId")%>"><%# DataBinder.Eval(Container.DataItem, "OrderID")%><br /><%# DataBinder.Eval(Container.DataItem, "JobName")%><br />
	<div style="margin-bottom:3px;"> <%# DataBinder.Eval(Container.DataItem, "Address")%></div>
	<div style="font-style:italic;color:#FF2600; font-size:95%;margin-left:5px;margin-right:5px;margin-bottom:4px;border-top:1px solid red;"><%# DataBinder.Eval(Container.DataItem, "PersonsInThisJob")%> </div>
    </li></a>
    </ItemTemplate>
</asp:Repeater>
	</ul>
</div>
</asp:Panel>

<asp:Repeater runat="server" ID="jobRepeater" OnLoad="GetAllJobs" OnItemDataBound="initJobs">
<ItemTemplate>
        
         <div id="subnav" class="grid_3 alpha">
                  <asp:Label CssClass="LabelDayAdmin" runat="server" ID="day" />
                  <div class="PersonalBox">
                  <asp:Label CssClass="Personal" runat="server" ID="personal"></asp:Label>
                   </div>                   
                   <ul class="nav grid_3 alpha"> 
    <asp:Repeater runat="server" ID="jobs" OnItemDataBound="InitJob"> 
        <ItemTemplate>
	              <li id="width" runat="server"><a class="" href="Jobs.aspx?JobID=<%# DataBinder.Eval(Container.DataItem, "JobId")%>"><%# DataBinder.Eval(Container.DataItem, "OrderID")%><br /><%# DataBinder.Eval(Container.DataItem, "JobName")%><br />
	<div style="margin-bottom:3px;"> <%# DataBinder.Eval(Container.DataItem, "Address")%></div>
	<div style="font-style:italic;color:#FFFFFF; font-size:95%;margin-left:5px;margin-right:5px;margin-bottom:4px;border-top:1px solid red;"><%# DataBinder.Eval(Container.DataItem, "PersonsInThisJob")%> </div>
    </li></a>
                    	      
        </ItemTemplate>      
    </asp:Repeater>
       </ul>
                    </div>
</ItemTemplate>
</asp:Repeater>
</asp:Content>
<asp:Content ContentPlaceHolderID="Left"  runat="server">
<asp:Panel ID="EditArea" runat="server">

<script type="text/javascript" src="/Scripts/listBoxSelection.js"></script>
	
    
    Namn: <asp:TextBox runat="server" Width="150" ID="JobName" TextMode="SingleLine" Height="10" />    
    <br /><br />
    Adress: <asp:TextBox runat="server" Width="280" ID="JobAddress" TextMode="MultiLine" Height="40" />
	<br /><br />
    Telefon: <asp:TextBox runat="server" Width="250" ID="JobContactPhone" TextMode="SingleLine" Height="10" />    
    <br /><br />
    Kontakt Person: </b><asp:Label ID="JobContact" runat="server" />
    <b>
    <br /><br />
    <div id="JobDateSet" runat="server">
    <div style="margin-right:10px;float:left;margin-top:5px;">Start datum:</div><div style="float:left;"> <asp:TextBoX ID="JobDate" OnInit="JobDateInit" Width="70" runat="server" /></div>
    <br /><br />
    <div style="margin-right:16px;float:left;margin-top:5px;">Slut datum:</div><div style="float:left;"> <asp:TextBoX ID="JobDateEnd" OnInit="JobDateInit" Width="70" runat="server" /></div>
    </div>
    <br /><br />        <div style="width:102px;float:left;">
                        Starttid:
                        </div>
                        <div style="width:100px;float:left;">
                        <asp:DropDownList runat="server" Width="75" ID="StartTime">             							
							<asp:ListItem Text="07:00" Value="1" />
							<asp:ListItem Text="07:30" Value="2" />
							<asp:ListItem Text="08:00" Value="3" />
							<asp:ListItem Text="08:30" Value="4" />
							<asp:ListItem Text="09:00" Value="5" />
							<asp:ListItem Text="09:30" Value="6" />
							<asp:ListItem Text="10:00" Value="7" />
							<asp:ListItem Text="10:30" Value="8" />
							<asp:ListItem Text="11:00" Value="9" />
							<asp:ListItem Text="11:30" Value="10" />
							<asp:ListItem Text="12:00" Value="11" />
							<asp:ListItem Text="12:30" Value="12" />
							<asp:ListItem Text="13:00" Value="13" />
							<asp:ListItem Text="13:30" Value="14" />
							<asp:ListItem Text="14:00" Value="15" />
							<asp:ListItem Text="14:30" Value="16" />
							<asp:ListItem Text="15:00" Value="17" />
							<asp:ListItem Text="15:30" Value="18" />
							<asp:ListItem Text="16:00" Value="19" />
							<asp:ListItem Text="16:30" Value="20" />
							<asp:ListItem Text="17:00" Value="21" />
							<asp:ListItem Text="17:30" Value="22" />
							<asp:ListItem Text="18:00" Value="23" />
                       </asp:DropDownList>
                       </div>
                       <br /><br />
                       <div style="width:102px;float:left;">
                       Sluttid:
                       </div>
                       <div style="width:100px;float:left;">
                            <asp:DropDownList runat="server" Width="75" ID="EndTime">                        							
							<asp:ListItem Text="07:00" Value="1" />
							<asp:ListItem Text="07:30" Value="2" />
							<asp:ListItem Text="08:00" Value="3" />
							<asp:ListItem Text="08:30" Value="4" />
							<asp:ListItem Text="09:00" Value="5" />
							<asp:ListItem Text="09:30" Value="6" />
							<asp:ListItem Text="10:00" Value="7" />
							<asp:ListItem Text="10:30" Value="8" />
							<asp:ListItem Text="11:00" Value="9" />
							<asp:ListItem Text="11:30" Value="10" />
							<asp:ListItem Text="12:00" Value="11" />
							<asp:ListItem Text="12:30" Value="12" />
							<asp:ListItem Text="13:00" Value="13" />
							<asp:ListItem Text="13:30" Value="14" />
							<asp:ListItem Text="14:00" Value="15" />
							<asp:ListItem Text="14:30" Value="16" />
							<asp:ListItem Text="15:00" Value="17" />
							<asp:ListItem Text="15:30" Value="18" />
							<asp:ListItem Text="16:00" Value="19" />
							<asp:ListItem Text="16:30" Value="20" />
							<asp:ListItem Text="17:00" Value="21" />
							<asp:ListItem Text="17:30" Value="22" />
							<asp:ListItem Text="18:00" Value="23" />
                       </asp:DropDownList>
                       </div>                        
                        <br /><br />
                              <div style="width:102px;float:left;">
                       Veckors fri hyra:
                       </div>
                       <div style="width:220px;float:left;">
                            <asp:DropDownList runat="server" Width="100" ID="FreeWeeksOfRental">  
                            <asp:ListItem Text="0 veckor" Value="0" />
							<asp:ListItem Text="1 vecka" Value="1" />
							<asp:ListItem Text="2 veckor" Value="2" />
							<asp:ListItem Text="3 veckor" Value="3" />
							<asp:ListItem Text="4 veckor" Value="4" />
							<asp:ListItem Text="5 veckor" Value="5" />
							<asp:ListItem Text="6 veckor" Value="6" />
							<asp:ListItem Text="7 veckor" Value="7" />
							<asp:ListItem Text="8 veckor" Value="8" />
							<asp:ListItem Text="9 veckor" Value="9" />
							<asp:ListItem Text="10 veckor" Value="10" />					
                            <asp:ListItem Text="11 veckor" Value="11" />
                            <asp:ListItem Text="12 veckor" Value="12" />
                       </asp:DropDownList>
                       &nbsp;»                      
                       <asp:Label ID="RentalEndDate" runat="server"  />                       
                       </div>
                       <br /><br />
                       <table id="Table1" border="0" cellspacing="0" cellpadding="0" runat="server">
			            <tr>
				            <td class="litBoldText"><b>Personal:</b></td>
				            <td>&nbsp;</td>
				            <td class="litBoldText"><b>Anmälda:</b></td>
			            </tr>
			            <tr>
				            <td><asp:ListBox Runat="server" ID="c_listBoxExistingItems" Rows="13" Width="140" SelectionMode="Multiple" CssClass="litInputSelect340"/></td>
				            <td>
				            <input id="c_buttonAdd" style="margin-left:5px;margin-right:5px;"  runat="server" type="button" class="litInputButton litInputButtonAdd" /><br /><br /><br /><br />
					            <input style="margin-left:5px;margin-right:5px;" id="c_buttonRemove" runat="server" type="button" class="litInputButton litInputButtonRemove" />
					            <input id="c_hiddenSelectedItems" runat="server" type="hidden" />
				            </td>
				            <td><asp:ListBox Runat="server" ID="c_listBoxSelectedItems" Rows="13" Width="140" SelectionMode="Multiple" CssClass="litInputSelect340"/></td>
			            </tr>
			          
			            		    
		            </table><br />
                   <b> Kommentar:</b><br />
                    <asp:TextBox runat="server" Width="280" ID="JobComments" TextMode="MultiLine" Height="100" />
                    <br />
                    <asp:CheckBoxList OnInit="InitCarsList"  ID="CarsCheckBoxList" RepeatDirection="Vertical" RepeatColumns="4" runat="server" />
                    
                    
                    <br />
                    <div style="margin-top:3px;">
                    Är jobbet slutfört? <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="InitFinnished" />
                    <asp:Panel ID="JobNewDatePanel" runat="server" Visible="false">
                    <div style="margin-right:10px;float:left;margin-top:5px;">Nytt datum:</div><div style="float:left;"> <asp:TextBox runat="server" Width="280" ID="JobNewDate" TextMode="SingleLine" /> </div>
                        <br /><br />
                    </asp:Panel>
                    </div>
                    <div style="margin-top:3px;">
                    
            <input id="Button1" type="button" runat="server" onserverclick="ButtonSend_Save" class="litfrmPreview" Value="Spara" /> 
            
            </div>
</asp:Panel>
</asp:Content>
