<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>
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
                    <li>
                        <a href="/overdue.aspx">Jobplaneraren - Obetalda Hyror</a>
                    </li>
                                        <li>
                        <a href="/view.aspx">Jobplaneraren - Display</a>
                    </li>
                    <li>
                        <a class="selected" href="/admin.aspx">Jobplaneraren - ADMIN</a>
                    </li>
                </ul>
            </div>

            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    Start Import Jobs from Mamut: <asp:Button runat="server" ID="job" Text="Start" OnClick="InitMamut" />
    <br /><br />
    Start Import Employees from Mamut: <asp:Button runat="server" ID="emp" Text="Start" OnClick="InitMamut" />

    <asp:Label ID="errorLabel" runat="server" />
    
    <asp:Panel ID="ResultJob" Visible="false" runat="server">        
      Jobb importerade: <asp:Label runat="server" ID="count" />
    <asp:Repeater runat="server" ID="repeater">
        <ItemTemplate>
        <div style="border:2px solid #000000;padding:5px; width:300px;">
            JobID: <%# DataBinder.Eval(Container.DataItem, "JobId")%> <br />
            JobName: <%# DataBinder.Eval(Container.DataItem, "JobName")%> <br />
            OrderID: <%# DataBinder.Eval(Container.DataItem, "OrderID")%> <br />
            ContactName: <%# DataBinder.Eval(Container.DataItem, "ContactName")%> <br />
            Address: <%# DataBinder.Eval(Container.DataItem, "Address")%> <br />
            JobStartDate: <%# DataBinder.Eval(Container.DataItem, "JobStartDate")%> <br />
            JobEndDate: <%# DataBinder.Eval(Container.DataItem, "JobEndDate")%> <br />
        </div>
        </ItemTemplate>
    </asp:Repeater>
    </asp:Panel>

    <asp:Panel ID="ResultPerson" Visible="false" runat="server">        
      Anställda importerade: <asp:Label runat="server" ID="countperson" />
    <asp:Repeater runat="server" ID="repeaterperson">
        <ItemTemplate>
        <div style="border:2px solid #000000;padding:5px; width:300px;">
            PersonID: <%# DataBinder.Eval(Container.DataItem, "PersonId")%> <br />
            Förnamn: <%# DataBinder.Eval(Container.DataItem, "Firstname")%> <br />
            Efternamn: <%# DataBinder.Eval(Container.DataItem, "Lastname")%> <br />
            E-mail: <%# DataBinder.Eval(Container.DataItem, "Email")%> <br />
            Mobil: <%# DataBinder.Eval(Container.DataItem, "Mobile")%> <br />            
        </div>
        </ItemTemplate>
    </asp:Repeater>
    </asp:Panel>

</asp:Content>

