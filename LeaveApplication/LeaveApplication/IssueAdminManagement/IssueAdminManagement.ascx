<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueAdminManagement.ascx.cs" Inherits="LeaveApplication.IssueAdminManagement.IssueAdminManagement" %>
<%@ Register TagPrefix="SharePointSD" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>

<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />
<script src="../_layouts/15/LeaveApplication/jquery.min.js"></script>
<style>
    .CalTxtbox {
        max-width: 100px;
    }
    .error {
        color:red
    }
</style>

<div class="Container">
    <table style="width: 700px">
        <tr class="header">
            <th colspan="9">
                <h3 class="font">Current Issue</h3>
            </th>
        </tr>
        <tr>
            <td>

                <asp:GridView ID="gvIssueAdminView" AutoGenerateColumns="false" CssClass="Container" runat="server" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Issue">
                    <HeaderStyle CssClass="header" />
                    <Columns>
                        <asp:BoundField DataField="Issue No" HeaderText="Issue No" />
                        <asp:BoundField DataField="Issue Details" HeaderText="Issue Details" />
                        <asp:TemplateField HeaderText="Issue Due Date">
                            <ItemTemplate>
                                <SharePoint:DateTimeControl ID="txtIssueDueDate" CssClassTextBox="CalTxtbox" runat="server" DateOnly="true" LocaleId="1033" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assign To">
                            <ItemTemplate>
                                <SharePointSD:PeopleEditor Rows="1" ID="txtAssignTo" runat="server" AllowEmpty="false"
                                    SelectionSet="User" MultiSelect="false" Width="300px" PlaceButtonsUnderEntityEditor="False" />
                                <br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:TextBox ID="txtcomments" TextMode="MultiLine" AutoPostBack="false" runat="server"
                                    MaxLength="500" Width="300px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approve/Reject">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAssign"  runat="server" Text="Assign" OnClick="btnAssign_Click" />
                                <asp:LinkButton ID="btnReject"  runat="server" Text="Reject" OnClick="btnReject_Click"/>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>

        </tr>
    </table>


    <asp:Label ID="lblerror" runat="server" Text="" CssClass="error"></asp:Label>


</div>

