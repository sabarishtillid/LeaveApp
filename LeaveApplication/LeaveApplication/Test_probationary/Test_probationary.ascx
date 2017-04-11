﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Test_probationary.ascx.cs" Inherits="LeaveApplication.Test_probationary.Test_probationary" %>

<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />

<asp:Button runat="server" ID="btnCalculate" Text="View Report" OnClick="BtnCalculateClick" />
<asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="BtnUpdateClick" />
<asp:Button runat="server" ID="btbolreset" Text="Optional Leave Reset" OnClick="BtnOLresetClick" Width="167px"/>
<asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnUpdatingMonth"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnOLUpdate"></asp:HiddenField>
<asp:Label runat="server" ID="lblError"></asp:Label>
<% System.Data.DataTable result = ViewState["Result"] as System.Data.DataTable;

   if (result != null && result.Rows.Count > 0)
   {
%>
<div class="Container">
    <table>
        <tr class="header">
            <% foreach (System.Data.DataColumn column in result.Columns)
               {
                   if (column.ColumnName != "TempEmployeeName" && column.ColumnName != "TempReportingTo")
                   {%>
            <th>
                <%= column.ColumnName %>
            </th>
            <% }
               } %>
        </tr>
        <% foreach (System.Data.DataRow row in result.Rows)
           {%>
        <tr class="data">
            <% foreach (System.Data.DataColumn column in result.Columns)
               {
                   if (column.ColumnName != "TempEmployeeName" && column.ColumnName != "TempReportingTo")
                   {%>
            <td>
                <%= row[column.ColumnName].ToString() %>
            </td>
            <% }
               }%>
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
            <th>
                There is no records.
            </th>
        </tr>
    </table>
</div>
<% } %><br />
<br />
<% System.Data.DataTable pendingResult = ViewState["PendingResult"] as System.Data.DataTable;

   if (pendingResult != null && pendingResult.Rows.Count > 0)
   {
%>
<div class="Container">
    <table>
        <tr class="header">
            <% foreach (System.Data.DataColumn column in pendingResult.Columns)
               {%>
            <th>
                <%=column.ColumnName %>
            </th>
            <% } %>
        </tr>
        <% foreach (System.Data.DataRow row in pendingResult.Rows)
           {%>
        <tr class="data">
            <% foreach (System.Data.DataColumn column in pendingResult.Columns)
               {%>
            <td>
                <%= row[column.ColumnName].ToString() %>
            </td>
            <% } %>
        </tr>
        <% } %>
    </table>
</div>
<% } %><br />
<br />
