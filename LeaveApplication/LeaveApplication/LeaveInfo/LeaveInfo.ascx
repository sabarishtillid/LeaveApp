<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveInfo.ascx.cs" Inherits="LeaveApplication.LeaveInfo.LeaveInfo" %>

<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />

<%--<h3>
    Leave Info</h3>--%>
<% System.Data.DataTable result = ViewState["Result"] as System.Data.DataTable;

   if (result != null && result.Rows.Count> 0)
   {
%>
<div class="Container">
    <table>
         <tr class="header">
            
            <th colspan="4"><h3 class="font">Leave Information</h3></th></tr>
        <tr class="header">
            <th class="font">
                Leave Type
            </th>
            <th class="font">
                Leave Balance
            </th>
            <th class="font">
                Leave Requested
            </th>
            <th class="font">
                Leave Utilized
            </th>
        </tr>
        <% foreach (System.Data.DataRow row in result.Rows)
           {%>
        <tr class="data">
            <td>
                <%=row["Leave Type"].ToString()%>
            </td>
            <td>
                <%=row["Balance Leave"].ToString()%>
            </td>
            <td>
                <%=row["Leave Requested"].ToString()%>
            </td>
            <td>
                <%=row["Leave utilized"].ToString()%>
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
            <th>
                There is no records.
            </th>
        </tr>
    </table>
</div>
<% } %><br />
<br />
<%-- <asp:GridView runat="server" ID="grvBalanceLeave">
    </asp:GridView>--%>
<asp:HiddenField runat="server" ID="hdnCurrentUsername" />
<asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>