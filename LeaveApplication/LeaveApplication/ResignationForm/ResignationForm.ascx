﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResignationForm.ascx.cs" Inherits="LeaveApplication.ResignationForm.ResignationForm" %>

<script src="../_layouts/15/LeaveApplication/LeaveRequest.js"></script>
<script src="../_layouts/15/LeaveApplication/jquery.min.js"></script>
<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />

<div class="Container">
    <table>
        <tr class="header">
            <th colspan="4">
                <h3 class="font">
                    Resignation Process</h3>
            </th>
        </tr>
        <tr class="data double">
            <td class="label">
                <label>
                    Employee Name</label></td>
            <td>
                   <asp:DropDownList runat="server" ID="ddlEmployee" AutoPostBack="True" 
                       Width="150px" onselectedindexchanged="DdlEmployeeSelectedIndexChanged">
                </asp:DropDownList>
            </td>
             <td class="label">
                <label>
                    Employee id</label>
            </td>
            <td>
               <asp:Label ID="lblEmpID" runat="server" Text=""></asp:Label>
            </td>
           
        </tr>
        <tr class="data double">
            <td class="label">
                <label>
                    Department</label>
            </td>
            <td>
                <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
            </td>
            <td class="label">
                <label>
                    Designation</label>
            </td>
            <td>
                <asp:Label ID="lblDesgination" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
        <tr id="Selecteddates" runat="server" class="data double">
            <td class="label">
                <label>
                    Date (MM/DD/YYYY) </label>&nbsp;</td>
            <td >
                <SharePoint:DateTimeControl ID="dateTimeDate" runat="server" DateOnly="true"  LocaleId="1033"
                     />
            </td>
            <td>
                
            </td>
            <td></td>
           
        </tr>
    <tr class="data double">
           <td class="label">
                <label>
                   PL Current Balance</label>
            </td>
            <td>
                  <asp:Label ID="lblplCurrent" runat="server" Text=""></asp:Label>
            </td>
              <td class="label">
                <label>
                   PL Need to pay</label>
            </td>
            <td>
                  <asp:Label ID="lblplneedtopay" runat="server" Text=""></asp:Label>
            </td>
      </tr>
        <tr class="data double controls">
            <td colspan="4" class="noborders">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="BtnSubmitClick" 
                    />&nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="Reset" onclick="BtnResetClick" 
                      />
            </td>
        </tr>
        <tr class="data double">
            <td class="noborders" colspan="4">
                <%--   <asp:CompareValidator ID="valDate" runat="server" ForeColor="Red" ControlToValidate="dateTimeEndDate$dateTimeEndDateDate"
                    ControlToCompare="dateTimeStartDate$dateTimeStartDateDate" Type="Date" Operator="GreaterThanEqual"
                    ErrorMessage="* Please enter End Date Greater or Equal to Start Date." Display="Dynamic"></asp:CompareValidator>--%>
                <asp:HiddenField runat="server" ID="hdnCurrentUsername" />
                <asp:HiddenField runat="server" ID="hdnReportingTo"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnEmployeeType" />
                <asp:HiddenField runat="server" ID="hdnHolidayList"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnFnclStarts"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnFnclEnds"></asp:HiddenField>
                  <asp:HiddenField runat="server" ID="hdnFinancialStratmonth"></asp:HiddenField>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" runat="server" ForeColor="red" Font-Bold="True"></asp:Label>
</div>