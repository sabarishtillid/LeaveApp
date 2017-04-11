<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllEmployeeLeaveList.ascx.cs" Inherits="LeaveApplication.AllEmployeeLeaveList.AllEmployeeLeaveList" %>

<script src="../_layouts/15/LeaveApplication/SPOpenDialog.js"></script>
<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />

<div class="container">

    <table>
        <tr class="data">
            <td>Request From: </td><td><asp:DropDownList runat="server" ID="CmbRequestFrom" AutoPostBack="True" OnSelectedIndexChanged="CmbRequestFrom_SelectedIndexChanged" /></td>
            <td>Leave Type</td><td><asp:DropDownList runat="server" ID="CmbLeaveType" AutoPostBack="True" OnSelectedIndexChanged="CmbLeaveType_SelectedIndexChanged" /></td>
        </tr>
    </table>
   
</div>

<% System.Data.DataTable result = ViewState["Result"] as System.Data.DataTable;

   if (result != null && result.Rows.Count > 0)
   {
%>
<div class="Container">
    <table>
        <tr class="header font">
            <th class="font">Requested From
            </th>
            <th class="font">Requested To
            </th>
            <th class="font">Leave Type
            </th>
            <th class="font">Start Date
            </th>
            <th class="font">End Date
            </th>
            <th class="font">Leave Days
            </th>
            <th class="font">Reason
            </th>
            <th class="font">Remarks
            </th>
            <th class="font">Status
            </th>
            <th class="font">Cancel
            </th>
        </tr>
        <% foreach (System.Data.DataRow row in result.Rows)
           {%>
        <tr class="data">
            <td>
                <%=row["Requested From"].ToString()%>
            </td>
            <td>
                <%=row["Requested To"].ToString()%>
            </td>
            <td>
                <%=row["Leave Type"].ToString()%>
            </td>
            <td>
                <%=row["Starting Date"].ToString()%>
            </td>
            <td>
                <%=row["Ending Date"].ToString()%>
            </td>
            <td>
                <%=row["Leave Days"].ToString()%>
            </td>
            <td>
                <div class="wordWrap">
                    <%=row["Reason"].ToString()%>
                </div>
            </td>
            <td>
                <%=row["Remarks"].ToString() %>
            </td>
            <td>
                <%if (row["Status"].ToString().Trim() == "Pending")
                  {%>
                <div style="color: #e84743">
                    <%= row["Status"].ToString() %>
                </div>
                <% }
                  else
                  { %>
                <div>
                    <%= row["Status"].ToString() %>
                </div>
                <% }%>
            </td>

            <td>

                <%=row["Cancel"].ToString() %>

            </td>

        </tr>
        <% } %>
    </table>
</div>
<% }
   else
   { %>
<div class="Container">
    <table>
        <tr class="data">
            <td colspan="2">
                <asp:Label ID="lblErr" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr class="header">
            <th>There is no records.
            </th>
        </tr>
    </table>
</div>
<% } %><br />
<br />
<asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>
