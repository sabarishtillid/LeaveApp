<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="New Issue Form.ascx.cs" Inherits="LeaveApplication.New_Issue_Form.New_Issue_Form" %>
<%@ Register TagPrefix="spuc" Namespace="Microsoft.SharePoint.WebControls"
         Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />


<style type="text/css">
    .auto-style1 {
        height: 39px;
    }
</style>


<div class="Container">
    <table class="lrftdmain">
        <tr class="header">
            <th colspan="4">
                <h3 class="header font">ISSUE FORM</h3>
            </th>
        </tr>
        <tr class="data double">
            <td class="label" style="height: 39px">
                <label class="font">
                    Issue No</label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txttitle" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
              &nbsp;<asp:RequiredFieldValidator ID="issuetitle" runat="server" Text="Please enter an Issue without leaving blank" ControlToValidate="txttitle"> </asp:RequiredFieldValidator>
            </td>


        </tr>
        <tr class="data double">
            <td class="label">
                <label class="font">
                    Issue Details</label>
            </td>
            <td>
                <asp:TextBox ID="txtdetails" runat="server" TextMode="MultiLine" Height="100px" Width="500px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="issuedetails" Text="Please enter details of an issue" runat="server" ControlToValidate="txtdetails"></asp:RequiredFieldValidator>
           
            </td>


        </tr>






<%--        <tr class="data double">
            <td class="label">
                <label class="font">
                    Issue Status</label>
            </td>
            <td>
                <asp:DropDownList ID="drpdwnstatus" runat="server" Width="190px">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Add an Issue</asp:ListItem>
                    <asp:ListItem>Update an Issue</asp:ListItem>
                    <asp:ListItem>Delete an Issue</asp:ListItem>
                </asp:DropDownList>

            </td>


        </tr>
        <tr class="data double">
            <td class="label">
                <label class="font">
                    Assigned To</label>
            </td>
            <td>
               <div style="background-color: Aqua">
    <spuc:PeopleEditor ID="PeopleEditor1" runat="server" Width="350px" AllowEmpty="true" MultiSelect="false" SelectionSet="User" />
<%--</div>

            </td>


        </tr>--%>
        <tr class="data double controls">
            <td colspan="4" class="noborders">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClientClick="return ValidateForm();" OnClick="btnsubmit_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"  />
                &nbsp;
            </td>
        </tr>
       

    </table>
    <asp:Label ID="lblerror" runat="server" Text="lblerror" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblconfirm" runat="server" Text="lblconfirmation" Visible="false"></asp:Label>
</div>


<script language="javascript" type="text/javascript">
    function ValidateForm() {
        var IsValid = true;

        if (document.getElementById('<%=txtdetails.ClientID%>').value == "") {
            alert("Please select Issue Details");
            return false;
        }
       
        return IsValid;
    }
</script>