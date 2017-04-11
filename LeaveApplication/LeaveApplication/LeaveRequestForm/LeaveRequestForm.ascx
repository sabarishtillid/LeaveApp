<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveRequestForm.ascx.cs" Inherits="LeaveApplication.LeaveRequestForm.LeaveRequestForm" %>

<script src="../_layouts/15/LeaveApplication/jquery.min.js"></script>
<link href="../_layouts/15/LeaveApplication/StyleSheet.css" rel="stylesheet" />
<script src="../_layouts/15/LeaveApplication/LeaveRequest.js"></script>



<script type="text/javascript" lang="javascript ">
    
    function OnPickerFinish(resultfield) {
        clickDatePicker(null, "", "");
        DateCompare();
       
    }
    var durationValue = "testvalue";

    function DateCompare() {
      
        debugger;
        var jsondata = document.getElementById("<%= hdnHolidayList.ClientID %>").value;
        var leaveType = document.getElementById("<%= ddlTypeofLeave.ClientID %>");
        var fromdate = document.getElementById("<%= dateTimeStartDate.Controls[0].ClientID %>").value;
        var endDate = document.getElementById("<%= dateTimeEndDate.Controls[0].ClientID %>").value;
        var optionalDates = document.getElementById("<%= lstboxOptionalLeaves.ClientID %>");

        var obj = jQuery.parseJSON(jsondata);
        for (var i = 0; i < obj.length; i++)
        {
            if (fromdate == obj[i]) {
                alert("Your Start Date comes under Company Holidays.Please Look at it.")
                break;
            }
            if (endDate == obj[i]) {
                alert("Your End Date comes Under Company Holidays.Please Look at it.")
                break;
            }
        }
        var fValue = new Date(fromdate);
        var eValue = new Date(endDate);


        //if (fromdate == obj)
        //{
        //    alert("you are trying to select company holiday as leave requesting day");
        //}
     

        if (fromdate == endDate) {
            document.getElementById("<%= pnlHalfDay.ClientID %>").style.display = 'none';
        }
        else
            document.getElementById("<%= pnlHalfDay.ClientID %>").style.display = 'block';
        var selectedLeave = leaveType.options[leaveType.selectedIndex].value;
        var tempfromdate = fValue;

        while (IsHoliday(tempfromdate, obj)) {
            //  alert(tempfromdate);

            tempfromdate.setDate(tempfromdate.getDate() + 1);
        }

        var tempenddate = eValue;

        while (IsHoliday(tempenddate, obj)) {
            tempenddate.setDate(tempenddate.getDate() - 1);
        }

        var leaveDays;
        leaveDays = DayDifference(tempfromdate, tempenddate);
        // alert(tempfromdate + "----" + tempenddate);
        if (tempfromdate.toString() != tempenddate.toString()) {
            if (leaveDays > 0)
                leaveDays = leaveDays + 1;
            else
                leaveDays = 0;
        } else {
            leaveDays = 1;
        }

        // alert(selectedLeave);
        var i;
        if (selectedLeave == "Comp off") {
            var countWorkingDays = 0;
            //  var tfdate = tempfromdate;
            for (i = tempfromdate; tempfromdate.getTime() <= tempenddate.getTime() ; i.setDate(i.getDate() + 1)) {
                if (!IsHoliday(tempfromdate, obj))
                    countWorkingDays++;
            }
            if (countWorkingDays > 0) {
                document.getElementById("<%= lblDuration.ClientID %>").innerText = countWorkingDays;
                durationValue = countWorkingDays;
                document.getElementById("<%= txtDuration.ClientID %>").value = countWorkingDays;
            }
            else {
                document.getElementById("<%= lblDuration.ClientID %>").innerText = 0;
                durationValue = 0;
                document.getElementById("<%= txtDuration.ClientID %>").value = 0;
            }
            //alert(countWorkingDays);
        }
        else if (selectedLeave == "Optional") {
            var leavesselected = 0;
            for (i = 0; i < optionalDates.options.length; i++) {
                var isSelected = optionalDates.options[i].selected;
                isSelected = (isSelected) ? "selected" : "not selected";
                
                if (isSelected == "selected")
                    leavesselected++;
            }
            document.getElementById("<%= lblDuration.ClientID %>").innerText = leavesselected;
            durationValue = leavesselected;
            document.getElementById("<%= txtDuration.ClientID %>").value = leavesselected;
            //alert(leavesselected);
        } else {
            // alert(selectedLeave);
            document.getElementById("<%= lblDuration.ClientID %>").innerText = leaveDays;
            durationValue = leaveDays;
            document.getElementById("<%= txtDuration.ClientID %>").value = leaveDays;
        }
        


        var durationdays = document.getElementById("<%= txtDuration.ClientID %>").value;

        var isfromhalfday = document.getElementById("<%= rbFromHalfday.ClientID %>");
        var istohalfday = document.getElementById("<%= rbToHalfday.ClientID %>");
        if (isfromhalfday.checked) {

            durationdays = parseFloat(durationdays) - 0.5;
            document.getElementById("<%= txtDuration.ClientID %>").value = durationdays;
            document.getElementById("<%= lblDuration.ClientID %>").innerText = durationdays;

        }
        if (istohalfday.checked) {

            durationdays = parseFloat(durationdays) - 0.5;
            document.getElementById("<%= txtDuration.ClientID %>").value = durationdays;
            document.getElementById("<%= lblDuration.ClientID %>").innerText = durationdays;
           
        }
    }

    function DayDifference(tempfromdate, tempenddate) {
        var oneDay = 1000 * 60 * 60 * 24;

        var dayDiff = (Math.ceil((tempenddate.getTime() - tempfromdate.getTime()) / (oneDay)));

        return dayDiff;
    }

    function IsHoliday(fValue, jsondata) {
        var fdate = new Date(fValue);
        var tdate = fdate.getMonth() + 1 + "/" + fdate.getDate() + "/" + fdate.getFullYear();

        if (jsondata.toString().indexOf(tdate) != -1) {
            return true;
        }

        return IsSatOrSun(tdate);
    }

    function IsSatOrSun(fValue) {
        var tdate = new Date(fValue);

        if (tdate.getDay() == 0 || tdate.getDay() == 6) {
            return true;
        } else {
            return false;
        }
    }

    function ishalfday()
    {

        var durationdays = document.getElementById("<%= txtDuration.ClientID %>").value;

        var isfromhalfday = document.getElementById("<%= rbFromHalfday.ClientID %>");
        var istohalfday = document.getElementById("<%= rbToHalfday.ClientID %>");
        if (isfromhalfday.checked) {

            durationdays = parseFloat(durationdays) - 0.5;
            document.getElementById("<%= txtDuration.ClientID %>").value = durationdays;
            document.getElementById("<%= lblDuration.ClientID %>").innerText = durationdays;

        }
        else {
            durationdays = parseFloat(durationdays) + 0.5;
            document.getElementById("<%= txtDuration.ClientID %>").value = durationdays;
            document.getElementById("<%= lblDuration.ClientID %>").innerText = durationdays;
        }

    }

    function istohalfday() {

        var durationdays = document.getElementById("<%= txtDuration.ClientID %>").value;

        var isfromhalfday = document.getElementById("<%= rbFromHalfday.ClientID %>");
        var istohalfday = document.getElementById("<%= rbToHalfday.ClientID %>");
        if (istohalfday.checked) {

            durationdays = parseFloat(durationdays) - 0.5;
            document.getElementById("<%= txtDuration.ClientID %>").value = durationdays;
            document.getElementById("<%= lblDuration.ClientID %>").innerText = durationdays;
            ////            durationValue = countWorkingDays;
        }
        else {
            durationdays = parseFloat(durationdays) + 0.5;
            document.getElementById("<%= txtDuration.ClientID %>").value = durationdays;
            document.getElementById("<%= lblDuration.ClientID %>").innerText = durationdays;
            return;
        }
    }
    
    //$(document).ready(function () {
    //    // Handler for .ready() called.
    //    $("#ctl00_ctl40_g_49650fc8_9b7d_41f9_9243_3c88d2ce80eb_dateTimeEndDate_dateTimeEndDateDate").blur(function () {
    //        //add input fields
    //        DateCompare();
    //    });
    //});
</script>

<div class="Container">
    <table class="lrftdmain">
        <tr class="header">
            <th colspan="4">
                <h3 class="header font">
                    Leave Request Form</h3>
            </th>
        </tr>
        <tr class="data double">
            <td class="label">
                <label class="font">
                    Employee Id</label>
            </td>
            <td>
                <asp:Label ID="lblEmpID" runat="server" Text=""></asp:Label>
            </td>
            <td class="label">
                <label class="font">
                    Designation</label>
            </td>
            <td>
                <asp:Label ID="lblDesgination" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="data double">
            <td class="label">
                <label class="font">
                    Department</label>
            </td>
            <td>
                <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
            </td>
            <td class="label">
                <label class="font">
                    Type of Leave</label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlTypeofLeave" AutoPostBack="false" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="data double">
            <td class="label">
                <label class="font">
                    Purpose</label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtPurpose" TextMode="MultiLine" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <asp:Panel ID="PnlSelecteddate" runat="server">
        </asp:Panel>
        <tr id="Selecteddates" runat="server" class="data double">
            <td class="label">
                <label class="font">
                    From Date (MM/DD/YYYY)
                </label>
                &nbsp;
            </td>
            <td>
                <SharePoint:DateTimeControl ID="dateTimeStartDate" runat="server" DateOnly="true"
                    LocaleId="1033" OnValueChangeClientScript="javascript:DateCompare()" />
                <br />
                <asp:CheckBox ID="rbFromHalfday" runat="server" Text="Half Day" onclick="javascript:ishalfday();" />
                <asp:HiddenField runat="server" ID="hdnhalfday"></asp:HiddenField>
            </td>
            <td class="label">
                <label class="font">
                    To Date (MM/DD/YYYY)
                </label>
            </td>
            <td>
                <SharePoint:DateTimeControl ID="dateTimeEndDate" runat="server" DateOnly="true" LocaleId="1033"
                    OnValueChangeClientScript="javascript:DateCompare()"  />
                <br />
                <asp:Panel runat="server" ID="pnlHalfDay">
                    <asp:CheckBox ID="rbToHalfday" runat="server" Text="Half Day" onclick="javascript:istohalfday();" /></asp:Panel>
            </td>
        </tr>
        <asp:Panel ID="PnloptinalDates" runat="server">
            <tr id="optinalDates" runat="server" class="data double">
                <td class="label">
                    <label class="font">
                        Optional Leave</label>
                </td>
                <td colspan="4">
                    <asp:DropDownList runat="server" AutoPostBack="False" ID="lstboxOptionalLeaves" Width="100px"
                        onchange="DateCompare()" />
                </td>
            </tr>
        </asp:Panel>
        <tr class="data double">
            <td class="label">
                <label class="font">
                    Duration</label>
            </td>
            <td>
                <label runat="server" id="lblDuration" />
                <label runat="server" id="lbltest" />
                <asp:HiddenField runat="server" ID="txtDuration" />
            </td>
            <td class="label">
                <label class="font">
                    Reporting To</label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="ddlReportingTo" ReadOnly="True" CssClass="ReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr class="data double controls">
            <td colspan="4" class="noborders">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="BtnSubmitClick" />&nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="BtnResetClick" />
                &nbsp;
            </td>
        </tr>
        <tr class="data double">
            <td class="noborders" colspan="4">
                <asp:HiddenField runat="server" ID="hdnCurrentUsername" />
                <asp:HiddenField runat="server" ID="hdnReportingTo"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnCurrentYear"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnEmployeeType" />
                <asp:HiddenField runat="server" ID="hdnHolidayList"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnFnclStarts"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnFnclEnds"></asp:HiddenField>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" runat="server" ForeColor="red" Font-Bold="True"></asp:Label>
</div>
