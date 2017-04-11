using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace LeaveApplication.AllEmployeeLeaveList
{
    [ToolboxItemAttribute(false)]
    public partial class AllEmployeeLeaveList : WebPart
    {
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

            if(!Page.IsPostBack)
                LoadDropDownList();
        }

        private DataTable LeavestableStructure()
        {
            var eTable = new DataTable();
            eTable.Columns.Add("Requested From");
            eTable.Columns.Add("Requested To");
            eTable.Columns.Add("Leave Type");
            eTable.Columns.Add("Starting Date");
            eTable.Columns.Add("Ending Date");
            eTable.Columns.Add("Leave Days");
            eTable.Columns.Add("Remarks");
            eTable.Columns.Add("Status");
            eTable.Columns.Add("Reason");
            eTable.Columns.Add("Cancel");

            //eTable.Columns.Add("Cancel");
            return eTable;
        }

        private void LoadLeaveDetails()
        {
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
                            //SPListItemCollection activeusers;
                            if (IsMemberInGroup("Admin"))
                                //currentUserDetails = GetListItemCollection(leaveList, "Employee Status", "Active");
                                currentUserDetails = leaveList.GetItems();
                            else
                                currentUserDetails = GetListItemCollection(leaveList, "RequestedTo",
                                                                           currentUser, "Employee Status", "Active");
                            LoadData(currentUserDetails);
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

        protected void CmbRequestFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        protected void CmbLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void LoadData(SPListItemCollection currentUserDetails)
        {
            DataTable leavetable = LeavestableStructure();

            // leaveList.GetItems();
            if (currentUserDetails.Count > 0)
            {
                // var itemCollection = leaveList.Items;

                foreach (SPListItem spListItem in currentUserDetails)
                {
                    DataRow dataRow = leavetable.NewRow();

                    var requestedto = new SPFieldLookupValue(spListItem["RequestedTo"].ToString());

                    dataRow["Requested To"] = requestedto.LookupValue;

                    var requestedFrom = new SPFieldLookupValue(spListItem["RequestedFrom"].ToString());

                    dataRow["Requested From"] = requestedFrom.LookupValue;
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

                    string url = "'" + SPContext.Current.Site.Url + "/SitePages/CancelLeaves.aspx?LeaveId=" + spListItem.ID + "'"; 
                    //string url = "'" + SPContext.Current.Site.Url +
                    //             "/_layouts/LeaveApplication/CancelLeaves.aspx?LeaveId=" + spListItem.ID +
                    //             "'";
                    //if (spListItem[Utilities.Status].ToString() == "Approved")
                    //    dataRow["Cancel"] = "<a href=\"JavaScript:openDialog(" + url + ");\">Cancel</a>"; 

                    //leavetable.Rows.Add(dataRow);


                    if (spListItem[Utilities.Status].ToString() == "Approved")
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

        private void LoadDropDownList()
        {
            CmbRequestFrom.Items.Clear();
            CmbRequestFrom.Items.Add(new ListItem
            {
                Text = "All",
                Value = "0",
                Selected = true
            });

            CmbLeaveType.Items.Clear();


            CmbLeaveType.Items.Add(new ListItem
            {
                Text = "ALL",
                Value = "ALL"
            });

            CmbLeaveType.Items.Add(new ListItem
            {
                Text = "Paid Leave",
                Value = "Paid Leave"
            });

            CmbLeaveType.Items.Add(new ListItem
            {
                Text = "Sick Leave",
                Value = "Sick Leave"
            });

            CmbLeaveType.Items.Add(new ListItem
            {
                Text = "Casual Leave",
                Value = "Casual Leave"
            });

            CmbLeaveType.Items.Add(new ListItem
            {
                Text = "Optional",
                Value = "Optional"
            });

            CmbLeaveType.Items.Add(new ListItem
            {
                Text = "LOP",
                Value = "LOP"
            });

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        foreach (SPGroup spGroup in web.Groups) {

                            if (spGroup.Name.ToString() == "Employee")
                            {
                                SPUserCollection spuseringroup = spGroup.Users;
                                foreach (SPUser spUser in spuseringroup)
                                {
                                    if (!spUser.IsDomainGroup && !spUser.IsSiteAdmin && !spUser.IsSiteAuditor && spUser.Name != "System Account")
                                    {
                                        CmbRequestFrom.Items.Add(new ListItem
                                        {
                                            Text = spUser.Name,
                                            Value = spUser.ID.ToString()
                                        });
                                    }
                                }
                            
                            }
                        
                        
                        }
                        
                    }
                }
            });
        }

        private void ApplyFilter()
        {
            try
            {
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        var leaveList = web.Lists.TryGetList(Utilities.LeaveRequest);

                        if (leaveList != null)
                        {
                            int selectedUser;

                            int.TryParse(CmbRequestFrom.SelectedItem.Value, out selectedUser);

                            SPListItemCollection currentUserDetails;

                            if (selectedUser!= 0 && CmbLeaveType.Text != "ALL")
                            {
                                currentUserDetails = leaveList.GetItems(new SPQuery
                                {
                                    Query = @" <Where>
                                          <And>
                                             <Eq>
                                                <FieldRef Name='Leave_x0020_Type' />
                                                <Value Type='Text'>" + CmbLeaveType.Text + @"</Value>
                                             </Eq>
                                             <Eq>
                                                <FieldRef Name='RequestedFrom' LookupId='True' />
                                                <Value Type='Integer'>" + selectedUser + @"</Value>
                                             </Eq>
                                          </And>
                                       </Where>"
                                });
                            }
                            else if (selectedUser != 0 && CmbLeaveType.Text == "ALL")
                            {
                                currentUserDetails = leaveList.GetItems(new SPQuery
                                {
                                    Query = @" <Where>
                                                    <Eq>
                                                        <FieldRef Name='RequestedFrom' LookupId='True' />
                                                        <Value Type='Integer'>" + selectedUser + @"</Value>
                                                    </Eq>
                                                   </Where>"
                                });
                            }
                            else if (selectedUser == 0 && CmbLeaveType.Text != "ALL")
                            {
                                currentUserDetails = leaveList.GetItems(new SPQuery
                                {
                                    Query = @" <Where>
                                                    <Eq>
                                                        <FieldRef Name='Leave_x0020_Type' />
                                                        <Value Type='Text'>" + CmbLeaveType.Text + @"</Value>
                                                    </Eq>
                                                   </Where>"
                                });
                            }
                            else
                                currentUserDetails = leaveList.GetItems();
                            

                            //GetListItemCollection(leaveList, "RequestedTo",currentUser, "Employee Status", "Active");
                            LoadData(currentUserDetails);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}
