using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication
{
    internal class Utilities
    {
        public static string Department = "Department";
        public static string Designation = "Designation";
        public static string EmployeeType = "Employee Type";
        public static string HolidayList = "Holiday List";
        public static string EmployeeLeaves = "Employee Leaves";
        public static string EmployeeScreen = "Employee Screen";
        public static string ReportingTo = "Reporting Members";
        public static string EmployeeId = "Title";
        public static string EmployeeName = "Employee Name";
        public static string Manager = "Manager";
        public static string FirstName = "First Name";
        public static string LastName = "Last Name";
        public static string DateofJoin = "Date of Join";
        public static string OLupdate = "OLupdate";
        public static string OLupdatemonth = "OLupdatemonth";

        //public static string EmployeeType = "Employee Type";
        public static string DOB = "DOB";

        public static string Email = "Email";
        public static string Mobile = "Mobile";
        public static string LeaveRequest = "Leave Request";

        public static string Status = "Status";

        public static string LeaveType = "Leave Type";
       

        public static string StartingDate = "Starting Date";
        public static string EndingDate = "Ending Date";
        public static string LeaveDays = "Leave Days";
        public static string Remarks = "Remarks";
        public static string CurrentYear = "CurrentYear";
        public static string Year = "Year";
        public static string UpdatingMonth = "UpdatingMonth";
        public static string UpdatedDate = "UpdatedDate";
        public static string Financialstartmonth = "Financial start month";
        public static string CompoffList = "Compoff Details";
        public static string TimerJobName = "Update Leave Balance in Leave Application";

        public static string LeaveBalancecolname = "Leave Balance";




        public static string EmployeeScreenListName = "Employee Screen";
        public static string DepartmentListName = "Department";
        public static string DesignationListName = "Designation";
        public static string EmployeeDetailsListName = "Employee Details";
        public static string EmployeeExcessLeavesListName = "Employee Excess Leaves";
        public static string EmployeeLeavesListName = "Employee Leaves";
        public static string EmployeeTypeListName = "Employee Type";
        public static string FinancialstartmonthListName = "Financial start month";
        public static string HolidayListListName = "Holiday List";
        public static string HolidayTypeListName = "Holiday Type";
        public static string LeaveDaysListName = "Leave Days";
        public static string LeaveRequestListName = "Leave Request";
        public static string LeaveTypeListName = "Leave Type";
        public static string ReportingMembersListName = "Reporting Members";
        public static string UpdatedDateListName = "UpdatedDate";
        public static string UpdatingMonthListName = "UpdatingMonth";
        public static string CompoffDetailsListName = "Compoff Details";
        public static string CurrentYearListName = "CurrentYear";


        public static string IssueTrackerListName = "Issue Tracker";


        public static int itemcounter(SPWeb web)
        {
            int ID = 1;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(web.Site.ID))
                {

                    using (SPWeb Oweb = oSite.OpenWeb())
                    {

                        SPList Olist = Oweb.Lists[IssueTrackerListName];
                        SPQuery oSPQuery = new SPQuery();
                        oSPQuery.Query = @"<Where><Neq><FieldRef Name='Issue_x0020_No' /><Value Type='Text'>0</Value></Neq></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";
                        SPListItemCollection oCollection = Olist.GetItems(oSPQuery);
                        if (oCollection.Count != 0)
                        {
                            ID = Convert.ToInt32(oCollection[0]["Issue No"]);
                            ID = ID + 1;
                        }
                    }

                }

            });

            return ID;
        }


        public static void SendNotification(SPWeb Web, string To, string Subject, string IssueNo, string Assigned)
        {
            StringDictionary headers = new StringDictionary();
            headers.Add("to", To);
            headers.Add("from", "tillidadmin@tillidsoft.com");
            headers.Add("subject", Subject);
            headers.Add("content-type", "text/html");
            var sb = new StringBuilder();
            if (Assigned == "Yes")
            {
                sb.Append(@" <table"); sb.Append(@"><tr><td style='font-family:Calibri'>Dear Developer");

            }else
            { sb.Append(@" <table"); sb.Append(@"><tr><td style='font-family:Calibri'>Dear Initiator"); }
          
            sb.Append(@"</td><td></td></tr>");
            sb.Append("<tr><td>&nbsp;</td><td><br/></td></tr>");
            if (Assigned == "Yes")
            {
                sb.Append(@"<tr><td style='font-family:Calibri'>New Leave Application Issue No: "); sb.Append(IssueNo); sb.Append(" has been Assigned as Task to work on.</td><td></td></tr>");
            }
            else
            {
                sb.Append(@"<tr><td style='font-family:Calibri'>There is a New Issue has been Initiated. </td><td></td></tr>");

            }
            if (Assigned == "Yes")
            {
                sb.Append(@"<tr><td style='font-family:Calibri'>Please "); sb.Append("<a href="); sb.Append(Web.Url.ToString() + "/SitePages/DeveloperView.aspx"); sb.Append(">Click here</a>"); sb.Append(@" to view Form.</td><td></td></tr>");

            }
            else
            { sb.Append(@"<tr><td style='font-family:Calibri'>Please "); sb.Append("<a href="); sb.Append(Web.Url.ToString() + "/SitePages/IssueAdminView.aspx"); sb.Append(">Click here</a>"); sb.Append(@" to view Form.</td><td></td></tr>"); }
            
            sb.Append("</table>");
            sb.Append(@"<table>");
            sb.Append(@"<tr><td>&nbsp;</td></tr>");
            sb.Append(@"<tr><td style='font-family:Calibri'>Regards,</td></tr>");
            sb.Append(@"<tr><td style='font-family:Calibri'>Tillid Administrator</td></tr>");
            sb.Append("</table>");

            SPUtility.SendEmail(Web, headers, sb.ToString());
        }

        public static void SendNotification(SPWeb Web, string To, string Subject, string IssueNo)
        {
            StringDictionary headers = new StringDictionary();
            headers.Add("to", To);
            headers.Add("from", "tillidadmin@tillidsoft.com");
            headers.Add("subject", Subject);
            headers.Add("content-type", "text/html");
            var sb = new StringBuilder();
           
            sb.Append(@" <table"); sb.Append(@"><tr><td style='font-family:Calibri'>Dear Admin");            

            sb.Append(@"</td><td></td></tr>");
            sb.Append("<tr><td>&nbsp;</td><td><br/></td></tr>");
           
                sb.Append(@"<tr><td style='font-family:Calibri'>Issue No: ");sb.Append(IssueNo); sb.Append(", Issue has be solved, Kindly check."); 
           
           
                        sb.Append("</table>");
            sb.Append(@"<table>");
            sb.Append(@"<tr><td>&nbsp;</td></tr>");
            sb.Append(@"<tr><td style='font-family:Calibri'>Regards,</td></tr>");
            sb.Append(@"<tr><td style='font-family:Calibri'>Tillid Administrator</td></tr>");
            sb.Append("</table>");

            SPUtility.SendEmail(Web, headers, sb.ToString());
        }

        public static string GetEmailAddressFromGroup(SPWeb web)
        {
            string EmailAddress = string.Empty;

            SPSecurity.RunWithElevatedPrivileges(delegate() {
                using (SPSite Osite = new SPSite(web.Site.ID))
                {
                    using (SPWeb OWeb = Osite.OpenWeb())
                    {
                        SPGroup Ogroup = OWeb.Groups["IssueAdmin"];
                        SPUserCollection users = Ogroup.Users;
                        foreach (SPUser user in users)
                        {

                            EmailAddress += user.Email.ToString() + ";";
                                       
                        }
                    }
                }
            
            
            });


            return EmailAddress;
        }

        
        public static SPFieldUserValueCollection UserValueCollection(SPWeb web, string users)
        {
            SPFieldUserValueCollection usercollection = new SPFieldUserValueCollection();
            string[] userarray = users.Split(';');
            string email = string.Empty; string name = string.Empty;
            string cpnameori = string.Empty;

            for (int j = 0; j < userarray.Length - 1; j++)
            {
                SPFieldUserValue usertoadd = ConvertLoginName(userarray[j], web);
                usercollection.Add(usertoadd);
            }

            return usercollection;
        }

        public static SPFieldUserValue ConvertLoginName(string userid, SPWeb web)
        {
            SPUser requireduser = web.EnsureUser(userid);
            SPFieldUserValue uservalue = new SPFieldUserValue(web, requireduser.ID, requireduser.LoginName);
            return uservalue;
        }
              
    }
}
