<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeveloperManagement.ascx.cs" Inherits="LeaveApplication.DeveloperManagement.DeveloperManagement" %>


 

       
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
    <table style="width: 100%">
        <tr class="header">
            <th colspan="9">
                <h3 class="font">Issue Status From Developer</h3>
            </th>
           
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvdeveloperview" runat="server"  AutoGenerateColumns="false" CssClass="Container" ShowHeaderWhenEmpty="true" EmptyDataText="No Issue">
                    <HeaderStyle CssClass="header" />
                    <Columns>
                        <asp:BoundField DataField="Issue No" HeaderText="Issue No" />
                        <asp:BoundField DataField="Issue Details" HeaderText="Issue Details" ItemStyle-Width="100%" />
                         <asp:BoundField DataField="Comments" HeaderText="Comments" />
                        
                                            
                        <asp:TemplateField HeaderText="Issue Status">
                            <ItemTemplate>
                                <asp:DropDownList  ID="dwnstatus" runat="server">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                <asp:Listitem>Opened</asp:Listitem>
                                <asp:Listitem> Hold</asp:Listitem>
                                <asp:Listitem>Closed</asp:Listitem>
                                    </asp:DropDownList>
                            </ItemTemplate>
                            
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments by Developer">
                            <ItemTemplate>
                                <asp:TextBox id="txtcomments" runat="server" Width="300px" height="50px" TextMode="MultiLine" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Save" >
                           <ItemTemplate>
                               <br />
                               <%--<asp:LinkButton id="btnedit" runat="server" Visible="true" Text="Edit" width="10px" OnClick="btnedit_click"></asp:LinkButton>--%>
                               <asp:LinkButton id="btnsave" runat="server" Visible="true" Text="Save" width="10px" OnClick="btnsave_click"></asp:LinkButton>
                           </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                
            </td>

        </tr>
    </table>


    <asp:Label ID="lblerror" runat="server" Text="" CssClass="error"></asp:Label>


</div>
 