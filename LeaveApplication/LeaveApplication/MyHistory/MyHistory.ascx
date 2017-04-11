﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyHistory.ascx.cs" Inherits="LeaveApplication.MyHistory.MyHistory" %>



<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />

<% System.Data.DataTable result = ViewState["Result"] as System.Data.DataTable;

   if (result != null && result.Rows.Count > 0)
   {
%>
<div class="Container">
    <table>
        <tr class="header">
            <th  colspan="9"><h3 class="font">Leave History</h3></th>
        </tr>
        <tr class="header font">
            <th class="font">
                Requested From
            </th>
            <th class="font">
                Requested To
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
                Leave Days
            </th>
            <th class="font">
                Reason
            </th>
            <th class="font">
                Remarks
            </th>
            <th class="font">
                Status
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
                    <%=row["Reason"].ToString()%></div>
            </td>
            <td>
                <%=row["Remarks"].ToString() %>
            </td>
            <td>
                <%if (row["Status"].ToString().Trim() == "Pending")
                  {%>
                <div style="color:#e84743 ">
                    <%= row["Status"].ToString() %></div>
                <% }
                  else
                  { %>
                <div>
                    <%= row["Status"].ToString() %></div>
                <% }%>
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
                <asp:Label ID="lblErr" runat="server" ForeColor="red" ></asp:Label>
            </td>
        </tr>
        <tr class="header">
            <th>
                There is no records.
            </th>
        </tr>
    </table>
</div>
<% } %><br />
<br />
<asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>