<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyTeamMemberDetails.ascx.cs" Inherits="LeaveApplication.MyTeamMemberDetails.MyTeamMemberDetails" %>

<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />


<% System.Data.DataTable result = ViewState["Result"] as System.Data.DataTable;

   if (result != null && result.Rows.Count> 0)
   {
%>
<div class="Container">
    <table>
        <tr class="header font">
            <% foreach (System.Data.DataColumn column in result.Columns)
               {%>
            <th class="font">
                <%=column.ColumnName %>
            </th>
            <% } %>
        </tr>
        <% foreach (System.Data.DataRow row in result.Rows)
           {%>
        <tr class="data">
            <% foreach (System.Data.DataColumn column in result.Columns)
               {%>
            <td>
                <%= row[column.ColumnName].ToString() %>
            </td>
            <% } %>
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
<asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>
<asp:Label runat="server" ID="lblError"></asp:Label>