using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace LeaveApplication.EmployeeLeavesList
{
    [ToolboxItemAttribute(false)]
    public partial class EmployeeLeavesList : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public EmployeeLeavesList()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentYear = SPContext.Current.Web.Lists.TryGetList(Utilities.CurrentYear).GetItems();
            foreach (SPListItem currentYearValue in currentYear)
            {
                hdnCurrentYear.Value = currentYearValue["Title"].ToString();
            }
            LoadLeaveDetails();
        }

        private DataTable LeavestableStructure()
        {
            var eTable = new DataTable();

            eTable.Columns.Add("Requested To");
            eTable.Columns.Add("Leave Type");
            eTable.Columns.Add("Starting Date");
            eTable.Columns.Add("Ending Date");
            eTable.Columns.Add("Leave Days");
            eTable.Columns.Add("Remarks");
            eTable.Columns.Add("Status");
            eTable.Columns.Add("Cancel");
            eTable.Columns.Add("Reason");

            //eTable.Columns.Add("Cancel");
            return eTable;
        }

        private void LoadLeaveDetails()
        {
            DataTable leavetable = LeavestableStructure();
            try
            {
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        var leaveList = web.Lists.TryGetList(Utilities.LeaveRequest);
                        if (leaveList != null)
                        {
                            SPUser user = web.CurrentUser;

                            string currentUser = user.Name;
                            SPListItemCollection currentUserDetails;

                            currentUserDetails = GetListItemCollection(leaveList, "RequestedFrom",
                                                                       currentUser, "Year", hdnCurrentYear.Value, "Employee Status", "Active");

                            if (currentUserDetails.Count > 0)
                            {
                                // var itemCollection = leaveList.Items;

                                foreach (SPListItem spListItem in currentUserDetails)
                                {
                                    DataRow dataRow = leavetable.NewRow();

                                    var requestedto = new SPFieldLookupValue(spListItem["RequestedTo"].ToString());

                                    dataRow["Requested To"] = requestedto.LookupValue;
                                    dataRow["Leave Type"] = spListItem[Utilities.LeaveType].ToString();
                                    dataRow["Starting Date"] =
                                        DateTime.Parse(spListItem[Utilities.StartingDate].ToString()).ToShortDateString();
                                    dataRow["Ending Date"] =
                                        DateTime.Parse(spListItem[Utilities.EndingDate].ToString()).ToShortDateString();
                                    dataRow["Leave Days"] = spListItem[Utilities.LeaveDays].ToString();
                                    if (spListItem[Utilities.Remarks] != null)
                                        dataRow["Remarks"] = spListItem[Utilities.Remarks].ToString();
                                    dataRow["Status"] = spListItem[Utilities.Status].ToString();
                                    if (spListItem["Purpose of Leave"] != null)
                                    {
                                        SPFieldMultiLineText mlt = spListItem.Fields.GetField("Purpose of Leave") as SPFieldMultiLineText;

                                        dataRow["Reason"] = mlt.GetFieldValueAsText(spListItem["Purpose of Leave"]);
                                    }
                                    string url = "'" + site.Url + "/SitePages/CancelLeaves.aspx?LeaveId=" + spListItem.ID +"'";
                                    if (spListItem[Utilities.Status].ToString() == "Pending")
                                    {

                                        dataRow["Cancel"] = "<a href=\"JavaScript:openDialog(" + url + ");\">Cancel</a>";
                                    }
                                    leavetable.Rows.Add(dataRow);
                                }
                            }
                            DataView dataView = new DataView(leavetable);
                            dataView.Sort = "Starting Date DESC";

                            ViewState["Result"] = dataView.Table;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
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
