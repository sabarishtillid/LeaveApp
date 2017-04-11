﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace LeaveApplication.CancelEmployeeLeaves
{
    [ToolboxItemAttribute(false)]
    public partial class CancelEmployeeLeaves : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public CancelEmployeeLeaves()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dateTimeEndDate.Enabled = false;
            dateTimeStartDate.Enabled = false;
            if (!Page.IsPostBack)
            {
                try
                {
                    using (var site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (var web = site.OpenWeb())
                        {
                            var currentYear = SPContext.Current.Web.Lists.TryGetList(Utilities.CurrentYear).GetItems();
                            foreach (SPListItem currentYearValue in currentYear)
                            {
                                hdnCurrentYear.Value = currentYearValue["Title"].ToString();
                            }

                            var employeeLeaveTypes = web.Lists[Utilities.LeaveType].GetItems();
                            ddlTypeofLeave.Items.Clear();
                            foreach (SPListItem employeeLeaveType in employeeLeaveTypes)
                            {
                                ddlTypeofLeave.Items.Add(employeeLeaveType["Leave Type"].ToString());
                            }

                            var holidayList = SPContext.Current.Web.Lists.TryGetList(Utilities.HolidayList);
                            var holidaysdateList = new List<string>();
                            if (holidayList != null)
                            {
                                var holidays = GetListItemCollection(holidayList,
                                                                     "HolidayType", "Company Holiday", "Year",
                                                                     hdnCurrentYear.Value);
                                holidaysdateList.AddRange(from SPListItem holiday in holidays
                                                          select
                                                              DateTime.Parse(holiday["Date"].ToString()).
                                                              ToShortDateString());
                            }

                            var tempList =
                                holidaysdateList.Where(a => DateTime.Now.ToShortDateString().Contains(a)).ToList();

                            // holidaysdateList.Contains(AbortTransaction=)

                            txtDuration.InnerText = (tempList.Any()) ? "0" : "1";
                            txtDuration.InnerText = (DateTime.Now.DayOfWeek == DayOfWeek.Sunday ||
                                                 DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                                                    ? "0"
                                                    : "1";

                            var oSerializer = new JavaScriptSerializer();
                            string sJSON = oSerializer.Serialize(holidaysdateList.ToArray());

                            hdnHolidayList.Value = sJSON;
                        }
                    }
                    var leaveId = System.Web.HttpContext.Current.Request.QueryString["LeaveId"].ToString(CultureInfo.InvariantCulture);
                    LoadLeaves(leaveId);
                }

                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }
        private void LoadLeaves(string leaveId)
        {
            try
            {
                hdnLeaveId.Value = leaveId;
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        var leavelist = web.Lists.TryGetList(Utilities.LeaveRequest);
                        var emplist = web.Lists.TryGetList(Utilities.EmployeeScreen);
                        var levitem = leavelist.GetItemById(Convert.ToInt16(leaveId));
                        var spv = new SPFieldLookupValue(levitem["RequestedTo"].ToString());

                        SPUser user =
                            new SPFieldUserValue(web, levitem["RequestedFrom"].ToString()).User;

                        SPListItemCollection empDetails = GetListItemCollection(emplist, "Employee Name",
                                                                                user.Name);
                        hdnCurrentUsername.Value = user.Name;
                        foreach (SPListItem empDetail in empDetails)
                        {
                            lblEmpID.Text = empDetail["Title"].ToString();
                            lblDepartment.Text = empDetail["Department"].ToString();
                            lblDesgination.Text = empDetail["Designation"].ToString();
                        }
                        txtDuration.InnerText = levitem["Leave Days"].ToString();
                        hdnLeaveDuration.Value = levitem["Leave Days"].ToString();
                        if (levitem["Purpose of Leave"] != null)
                        {
                            SPFieldMultiLineText mlt = levitem.Fields.GetField("Purpose of Leave") as SPFieldMultiLineText;

                            txtPurpose.Text = mlt.GetFieldValueAsText(levitem["Purpose of Leave"]);
                        }
                        ddlTypeofLeave.SelectedItem.Text = levitem["Leave Type"].ToString().Trim();
                        if (levitem["Leave Type"].ToString().Trim() != "Optional")
                        {
                            Selecteddates.Visible = true;
                            optinalDates.Visible = false;
                        }
                        else
                        {
                            txtOptionalLeaves.Text = DateTime.Parse(levitem["Starting Date"].ToString()).ToShortDateString();
                            Selecteddates.Visible = false;
                            optinalDates.Visible = true;
                        }
                        ddlReportingTo.Text = spv.LookupValue;
                        dateTimeEndDate.SelectedDate = DateTime.Parse(levitem["Ending Date"].ToString());
                        dateTimeStartDate.SelectedDate = DateTime.Parse(levitem["Starting Date"].ToString());
                        hdnStrtDate.Value = DateTime.Parse(levitem["Starting Date"].ToString()).ToString(CultureInfo.InvariantCulture);
                        hdnEndDate.Value = DateTime.Parse(levitem["Ending Date"].ToString()).ToString(CultureInfo.InvariantCulture);

                        ddlTypeofLeave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }


        protected void BtnResetClick(object sender, EventArgs e)
        {
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup(); SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, '1');</script>");
        }

        protected void BtnSubmitClick(object sender, EventArgs e)
        {
            try
            {
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        var leavelist = web.Lists.TryGetList(Utilities.LeaveRequest);
                        var levitem = leavelist.GetItemById(Convert.ToInt16(hdnLeaveId.Value));

                        bool isPending = levitem["Status"].ToString().Trim() == "Pending";
                        //if ((DateTime.Parse(hdnStrtDate.Value).Date <= dateTimeStartDate.SelectedDate.Date) && (DateTime.Parse(hdnEndDate.Value).Date >= dateTimeEndDate.SelectedDate.Date))
                        //{
                        // bool isAdmin = IsMemberInGroup("Admin");
                        //  bool isToday = (dateTimeStartDate.SelectedDate.Date < DateTime.Now.Date);

                        //SPListItemCollection leaveDetails = GetListItemCollection(web.Lists[Utilities.EmployeeLeaves], "Employee Name", hdnCurrentUsername.Value, "Leave Type", ddlTypeofLeave.SelectedValue, "Year", hdnCurrentYear.Value);
                        SPListItemCollection leaveDetails = GetListItemCollection(web.Lists[Utilities.EmployeeLeaves], "Employee ID", lblEmpID.Text, "Leave Type", ddlTypeofLeave.SelectedValue, "Year", hdnCurrentYear.Value);
                        foreach (SPListItem leaveDetail in leaveDetails)
                        {

                            if (levitem["Status"].ToString().Trim() == "Pending")
                            {
                                leaveDetail["Leave Requested"] =
                                    decimal.Parse(leaveDetail["Leave Requested"].ToString()) -
                                    decimal.Parse(txtDuration.InnerText);
                            }
                            else
                            {
                                leaveDetail["Leave utilized"] =
                                    decimal.Parse(leaveDetail["Leave utilized"].ToString()) -
                                    decimal.Parse(txtDuration.InnerText);
                            }
                            if (ddlTypeofLeave.SelectedValue.ToLower() != "lop")
                            {
                                leaveDetail["Leave Balance"] =
                                    decimal.Parse(leaveDetail["Leave Balance"].ToString()) +
                                    decimal.Parse(txtDuration.InnerText);
                            }

                            leaveDetail.Update();


                            levitem["Status"] = "Cancelled";
                            levitem.Update();

                            if (SPUtility.IsEmailServerSet(web))
                            {
                                try
                                {
                                    var curuser = web.CurrentUser;
                                    var currentuser = curuser.Name;
                                    SPListItemCollection cancelledleaves = GetListItemCollection(web.Lists[Utilities.LeaveRequest], "RequestedFrom", currentuser, "Status", "Cancelled", "Employee Status", "Active");

                                    if (cancelledleaves != null)
                                    {
                                        foreach (SPListItem cancelledleave in cancelledleaves)
                                        {
                                            var requestedto = new SPFieldUserValue(web, cancelledleave["RequestedTo"].ToString());
                                            var requestedfrom = new SPFieldUserValue(web, cancelledleave["RequestedFrom"].ToString()); ;
                                            var sd = DateTime.Parse(cancelledleave[Utilities.StartingDate].ToString()).ToShortDateString();
                                            var ed = DateTime.Parse(cancelledleave[Utilities.EndingDate].ToString()).ToShortDateString();

                                            if (!string.IsNullOrEmpty(requestedfrom.User.Email))
                                            {
                                                var htmlbody = "<table>";

                                                htmlbody += "       <tr>";
                                                htmlbody += "           <td>";
                                                htmlbody += SPContext.Current.Web.CurrentUser.Name;
                                                htmlbody += "           </td>";
                                                htmlbody += "       </tr>";

                                                htmlbody += "       <tr>";
                                                htmlbody += "           <td>";
                                                htmlbody += SPContext.Current.Web.CurrentUser.Name + "  Cancelled your leave request " + sd + "to" + ed;
                                                htmlbody += "           </td>";
                                                htmlbody += "       </tr>";



                                                htmlbody += "       <tr>";
                                                htmlbody += "           <td> <br /><br />";
                                                htmlbody += "Thank you";
                                                htmlbody += "           </td>";
                                                htmlbody += "       </tr>";
                                                htmlbody += "</table>";

                                                SPSecurity.RunWithElevatedPrivileges(delegate()
                                                {
                                                    using (var tempSite = new SPSite(site.ID))
                                                    {
                                                        using (var tempWeb = tempSite.OpenWeb(web.ID))
                                                        {
                                                            var headers = new StringDictionary();
                                                            headers.Add("to", requestedfrom.User.Email);
                                                            headers.Add("from", requestedto.User.Email);
                                                            headers.Add("subject", SPContext.Current.Web.CurrentUser.Name + " Cancelled your leave request " + sd + " to " + ed);
                                                            headers.Add("content-type", "text/html");

                                                            SPUtility.SendEmail(web, headers, htmlbody);
                                                        }
                                                    }
                                                });
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    lblError.Text = ex.Message;
                                }
                            }
                        }

                        Context.Response.Write(
                                 "<script type='text/javascript'>window.frameElement.commitPopup(); SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, '1');</script>");
                       
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        public bool IsMemberInGroup(string groupName)
        {
            bool memberInGroup;
            using (var site = new SPSite(SPContext.Current.Site.Url))
            {
                using (var web = site.OpenWeb())
                {
                    memberInGroup = web.IsCurrentUserMemberOfGroup(web.Groups[groupName].ID);
                }
            }

            return memberInGroup;
        }

        internal SPListItemCollection GetListItemCollection(SPList spList, string key, string value)
        {
            // Return list item collection based on the lookup field

            SPField spField = spList.Fields[key];
            var query = new SPQuery
            {
                Query =
                    @"<Where>
                                <Eq>
                                    <FieldRef Name='" + spField.InternalName +
                    @"'/><Value Type='" + spField.Type.ToString() + @"'>" + value + @"</Value>
                                </Eq>
                                </Where>"
            };

            return spList.GetItems(query);
        }

        internal SPListItemCollection GetListItemCollection(SPList spList, string keyOne, string valueOne, string keyTwo, string valueTwo)
        {
            // Return list item collection based on the lookup field

            SPField spFieldOne = spList.Fields[keyOne];
            SPField spFieldTwo = spList.Fields[keyTwo];
            var query = new SPQuery
            {
                Query = @"<Where>
                          <And>
                                <Eq>
                                    <FieldRef Name=" + spFieldOne.InternalName + @" />
                                    <Value Type=" + spFieldOne.Type.ToString() + ">" + valueOne + @"</Value>
                                </Eq>
                                <Eq>
                                    <FieldRef Name=" + spFieldTwo.InternalName + @" />
                                    <Value Type=" + spFieldTwo.Type.ToString() + ">" + valueTwo + @"</Value>
                                </Eq>
                          </And>
                        </Where>"
            };

            return spList.GetItems(query);
        }

        internal SPListItemCollection GetListItemCollection(SPList spList, string keyOne, string valueOne, string keyTwo, string valueTwo, string keyThree, string valueThree)
        {
            // Return list item collection based on the lookup field

            SPField spFieldOne = spList.Fields[keyOne];
            SPField spFieldTwo = spList.Fields[keyTwo];
            SPField spFieldThree = spList.Fields[keyThree];
            var query = new SPQuery
            {
                Query = @"<Where>
                          <And>
                             <And>
                                <Eq>
                                   <FieldRef Name=" + spFieldOne.InternalName + @" />
                                   <Value Type=" + spFieldOne.Type.ToString() + @">" + valueOne + @"</Value>
                                </Eq>
                                <Eq>
                                   <FieldRef Name=" + spFieldTwo.InternalName + @" />
                                   <Value Type=" + spFieldTwo.Type.ToString() + @">" + valueTwo + @"</Value>
                                </Eq>
                             </And>
                             <Eq>
                                <FieldRef Name=" + spFieldThree.InternalName + @" />
                                <Value Type=" + spFieldThree.Type.ToString() + @">" + valueThree + @"</Value>
                             </Eq>
                          </And>
                       </Where>"
            };

            return spList.GetItems(query);
        }
    }
}
