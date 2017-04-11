<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeLeaveStatus.ascx.cs" Inherits="LeaveApplication.EmployeeLeaveStatus.EmployeeLeaveStatus" %>

<script src="../_layouts/15/LeaveApplication/LeaveStatus.js"></script>
<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />


<%--<h3>
    Leave Requests</h3>--%>
<% DataTable results = (DataTable)ViewState["Results"];
   if (results != null && results.Rows.Count> 0)
   {
%>
<div class="Container">
    <table>
        <tr class="header"><th colspan="9"><h3 class="font"> Leave Requests</h3></th></tr>
        <tr class="header font">
            <th>
                <input type="checkbox" onclick="checkAll(this);" />
            </th>
            <th class="font">
                Requested From
            </th>
            <th class="font">
                Leave Type
            </th>
            <th class="font">
                Start Date
            </th>
            <th class="font">
                End Date
            </th>
            <th class="font">
                Duration
             </th>
            <th class="font">
                Reason
            </th>
            <th class="font">
                Status
            </th>
            <th class="font">
                Remarks
            </th>
        </tr>
        <% foreach (DataRow dataRow in results.Rows)
           {
        %>
        <tr>
            <td>
                <input type="checkbox" onclick="Check_Click(this);" name='<%= "Chk" + dataRow["Id"].ToString() %>' />
            </td>
            <td>
                <%= dataRow["RequestedFrom"].ToString() %>
            </td>
            <td>
                <%= dataRow["Leave Type"].ToString()%>
            </td>
            <td>
                <%= dataRow["Starting Date"].ToString()%>
            </td>
            <td>
                <%= dataRow["Ending Date"].ToString()%>
            </td>
            <td>
                <%= dataRow["Duration"].ToString()%>
            </td>
            <td>
                  <div class="wordWrap">
                <%= dataRow["Reason"].ToString()%></div>
            </td>
            <td>
                <%if (dataRow["Status"].ToString().Trim() == "Pending")
                  {%>
                <div style="color:#e84743 ">
                    <%= dataRow["Status"].ToString()%></div>
                <% }
                  else
                  { %>
                <div>
                    <%= dataRow["Status"].ToString()%></div>
                <% }%>
            </td>
            <td>
                <input type="text" style="width: 70px" name='<%= "txt" + dataRow["Id"].ToString() %>'
                    id='<%= "txt" + dataRow["Id"].ToString() %>' />
            </td>
        </tr>
        <%} %>
    </table>
    <!--
    <asp:GridView runat="server" EnableModelValidation="True" ID="grid" AutoGenerateColumns="False"
        Width="600">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-CssClass="hideGridColumn"
                ItemStyle-CssClass="hideGridColumn" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkbox" runat="server" onclick="Check_Click(this)" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RequestedFrom" HeaderText="Requested From" />
            <asp:BoundField DataField="Leave Type" HeaderText="Leave Type" />
            <asp:BoundField DataField="Starting Date" HeaderText="Starting Date" />
            <asp:BoundField DataField="Ending Date" HeaderText="Ending Date" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" />
            <asp:BoundField DataField="Reason" HeaderText="Reason" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <header> Remark </header>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="txtreason" runat="server" Width="125px" Height="50px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView> -->
    <ul>
        <li>
            <div class="controls">
                <asp:Button runat="server" Text="Approve" ID="BtnApprove" OnClick="BtnApproveClick" />
                <asp:Button runat="server" Text="Reject" ID="BtnReject" OnClick="BtnRejectClick" />
                <asp:Label runat="server" ID="lblErr"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>
            </div>
        </li>
    </ul>
</div>
<% }
   else
   { %>
<div class="Container">
    <table>
        <tr class="header">
            <th>
                There is no pending leave requests to approve.
            </th>
        </tr>
    </table>
</div>
<% } %><br />
<br />