using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LeaveApplication.Test_probationary
{
    [ToolboxItemAttribute(false)]
    public partial class Test_probationary : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Test_probationary()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            var currentYear = SPContext.Current.Web.Lists.TryGetList(Utilities.CurrentYear).GetItems();
            foreach (SPListItem currentYearValue in currentYear)
            {
                hdnCurrentYear.Value = currentYearValue["Title"].ToString();
            }


            var updatingMonths = SPContext.Current.Web.Lists.TryGetList(Utilities.UpdatingMonth).GetItems();
            foreach (SPListItem updatingmonth in updatingMonths)
            {
                hdnUpdatingMonth.Value = updatingmonth["Title"].ToString();
            }
            //}
            if (int.Parse(hdnUpdatingMonth.Value) != DateTime.Now.Month)
            {
                btnUpdate.Enabled = false;
            }
            else
            {
                var updatedDates = SPContext.Current.Web.Lists.TryGetList(Utilities.UpdatedDate).GetItems();
                foreach (SPListItem updatedDate in updatedDates)
                {
                    bool iscurrentYear = hdnCurrentYear.Value == DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
                    int updatedmonth = int.Parse(updatedDate["Month"].ToString());
                    string updatedYear = updatedDate["Year"].ToString();
                    if (updatedmonth == int.Parse(hdnUpdatingMonth.Value) && iscurrentYear)
                    {
                        btnUpdate.Visible = false;
                    }
                }
            }

            var optionalleave = SPContext.Current.Web.Lists.TryGetList(Utilities.OLupdate).GetItems();
            foreach (SPListItem collection in optionalleave)
            {
                hdnOLUpdate.Value = collection["Title"].ToString();
            }

            if (int.Parse(hdnOLUpdate.Value) != DateTime.Now.Month)
            {
                btbolreset.Enabled = false;

            }
            else
            {
                var updatedDates = SPContext.Current.Web.Lists.TryGetList(Utilities.OLupdatemonth).GetItems();
                foreach (SPListItem updatedDate in updatedDates)
                {
                    bool iscurrentYear = hdnCurrentYear.Value == DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
                    int updatedmonth = int.Parse(updatedDate["Month"].ToString());
                    string updatedYear = updatedDate["Year"].ToString();
                    if (updatedmonth == int.Parse(hdnUpdatingMonth.Value) && iscurrentYear)
                    {
                        btbolreset.Visible = false;
                    }
                }
            }


            PendingLeaves();
        }

        protected void BtnCalculateClick(object sender, EventArgs e)
        {
            try
            {
                ViewState["Result"] = GetEmployeeLeaves();

                //grdvwEmployeeLeaves.DataBind();
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        internal DataTable GetEmployeeLeaves()
        {
            DataTable dataTable = EmployeeLeavesStructure();
            try
            {
                //  var employeeList = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeScreen).GetItems();
                var employeeList = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeScreen), "Status", "Active");
                SPUser User = SPContext.Current.Web.CurrentUser;
                string UserName = User.Name;

                foreach (SPListItem employee in employeeList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Employee Id"] = employee["Employee ID"].ToString();
                    dataRow["Employee Type"] = employee["Employee Type"].ToString();
                    var entity = new PickerEntity
                    {
                        DisplayText =
                            new SPFieldUserValue(SPContext.Current.Web,
                                                 employee["Employee Name"].ToString()).User.
                            Name
                    };
                    var entityRep = new PickerEntity
                    {
                        DisplayText =
                            new SPFieldUserValue(SPContext.Current.Web,
                                                 employee[Utilities.Manager].ToString()).User.
                            Name
                    };
                    dataRow["Employee Name"] = entity.DisplayText.ToString();
                    dataRow["TempEmployeeName"] = new SPFieldUserValue(SPContext.Current.Web,
                                                                       employee["Employee Name"].ToString()).User.LoginName;

                    dataRow["Reporting To"] = entityRep.DisplayText.ToString();
                    dataRow["TempReportingTo"] = new SPFieldUserValue(SPContext.Current.Web, employee["Manager"].ToString()).User.LoginName;
                    var employeeLeaveList = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeLeaves), "Employee ID", employee["Employee ID"].ToString(), "Year",
                                              hdnCurrentYear.Value, "Employee Status", "Active");
                    foreach (SPListItem empleaves in employeeLeaveList)
                    {
                        var spv = new SPFieldLookupValue(empleaves[Utilities.LeaveType].ToString());

                        decimal leaveBalance = 0;
                        decimal leaveDays = 0;
                        decimal myopl = 0;




                        var leaveTypeList = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveDays),
                                               "Leave Type", spv.LookupValue.Trim(), Utilities.EmployeeType,
                                               "Permanent");
                        foreach (SPListItem leavetype in leaveTypeList)
                        {
                            leaveDays = decimal.Parse(leavetype["Leave Days"].ToString());

                        }


                        if (employee[Utilities.EmployeeType].ToString().Trim() == "Permanent")
                        {

                            if (spv.LookupValue.Trim() == "Paid Leave")
                            {

                                decimal leaves = decimal.Parse(empleaves["Leave Balance"].ToString()) + (leaveDays * 12);
                                if (leaves > 30)
                                {
                                    leaves = 30;
                                }

                                if (leaves % 1 == 0)
                                {
                                    int noOfleaves = Convert.ToInt16(leaves);
                                    leaveBalance = noOfleaves;
                                }
                                else
                                {
                                    leaveBalance = leaves;
                                }

                            }
                            else if (spv.LookupValue.Trim() == "Comp off")
                            {
                                leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());
                            }
                            else if (spv.LookupValue.Trim() == "Optional")
                            {
                                var oplspv = new SPFieldLookupValue(empleaves[Utilities.LeaveBalancecolname].ToString());
                                // leaveBalance = leaveDays;
                                myopl = oplspv.LookupId;
                                leaveBalance = myopl;

                            }
                            else
                            {
                                decimal leaves = (leaveDays * 12);
                                if (leaves % 1 == 0)
                                {
                                    int noOfleaves = Convert.ToInt16(leaves);
                                    leaveBalance = noOfleaves;
                                }
                                else
                                {
                                    leaveBalance = leaves;
                                }

                                //leaveBalance = (leaveDays * 12);
                            }
                        }
                        if (employee[Utilities.EmployeeType].ToString().Trim() == "Probationary")
                        {
                            if (spv.LookupValue.Trim() == "Paid Leave")
                            {
                                leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());
                                //dataRow["Paid Leave"] = leaveBalance;

                            }
                            else if (spv.LookupValue.Trim() == "Comp off")
                            {
                                leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());
                                //dataRow["Sick Leave"] = leaveBalance;
                                //dataRow["Casual Leave"] = leaveBalance;

                                //dataRow["LOP"] = leaveBalance;
                            }
                            else if (spv.LookupValue.Trim() == "Optional")
                            {
                                // leaveBalance = leaveDays;
                                var oplspv = new SPFieldLookupValue(empleaves[Utilities.LeaveBalancecolname].ToString());
                                myopl = oplspv.LookupId;
                                leaveBalance = myopl;
                                //dataRow["Optional"] = leaveBalance;

                            }
                            else
                            {
                                leaveBalance = 0;

                            }
                        }
                        dataRow[spv.LookupValue.Trim()] = leaveBalance;
                    }
                    if (employee[Utilities.EmployeeType].ToString().ToLower().Trim() != "permanent")
                    {
                        dataRow["Paid Leave"] = 6;
                        dataRow["Sick Leave"] = 0;
                        dataRow["Casual Leave"] = 0;
                        dataRow["Optional"] = 2;
                        dataRow["LOP"] = 0;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            catch (Exception e)
            {
                lblError.Text = e.Message;
            }
            return dataTable;
        }

        internal DataTable EmployeeLeavesStructure()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Employee Id");
            dataTable.Columns.Add("Employee Name");
            dataTable.Columns.Add("TempEmployeeName");
            dataTable.Columns.Add("Reporting To");
            dataTable.Columns.Add("TempReportingTo");
            dataTable.Columns.Add("Employee Type");
            var items = SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveType).GetItems();
            foreach (SPListItem item in items)
            {
                //  var spv = new SPFieldLookupValue(item[Utilities.LeaveType].ToString());
                dataTable.Columns.Add(item[Utilities.LeaveType].ToString());
            }
            return dataTable;
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            try
            {
                string newYear = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        UpdateCurrentYear(newYear);
                        var employeeLeaves = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeLeaves);
                        var leaveTypes =
                            GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveDays),
                                                  Utilities.EmployeeType, "Permanent");
                        DataTable dataTable = GetEmployeeLeaves();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            foreach (SPListItem leaveType in leaveTypes)
                            {
                                var spv = new SPFieldLookupValue(leaveType[Utilities.LeaveType].ToString());
                                var empleaveCollection = GetListItemCollection(employeeLeaves, "Employee ID",
                                                                               dataRow["Employee Id"].ToString(),
                                                                               "Leave Type", spv.LookupValue, "Year",
                                                                               newYear);

                                //employeeleavelist.GetItems(employeeleaveDaysQuery);
                                if (empleaveCollection.Count == 0)
                                {
                                    decimal leaveBalance;
                                    leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "0" : dataRow[spv.LookupValue.Trim()].ToString());
                                    decimal leaveExcess;
                                    if (IsAccured(spv.LookupValue.Trim()))
                                    {
                                        decimal maximum = GetMaximumLeave(spv.LookupValue.Trim());
                                        if (leaveBalance > maximum)
                                        {
                                            leaveExcess = leaveBalance - maximum;
                                            leaveBalance = maximum;

                                            UpdatedExcessLeaves(dataRow["Employee Id"].ToString(), web.AllUsers[dataRow["TempEmployeeName"].ToString()], spv.LookupValue.Trim(), leaveExcess);
                                        }
                                    }
                                    var newItem = employeeLeaves.Items.Add();

                                    newItem["Employee ID"] = dataRow["Employee Id"];
                                    newItem["Employee Name"] = web.AllUsers[dataRow["TempEmployeeName"].ToString()];
                                    newItem["Reporting Manager"] = web.AllUsers[dataRow["TempReportingTo"].ToString()];
                                    newItem[Utilities.LeaveType] = leaveType[Utilities.LeaveType].ToString();
                                    newItem["Leave Balance"] = leaveBalance;
                                    newItem["Employee Type"] = dataRow["Employee Type"];
                                    newItem["Leave utilized"] = 0;
                                    newItem["Leave Requested"] = 0;
                                    newItem["Year"] = newYear;
                                    newItem["Employee Status"] = "Active";
                                    newItem.Update();


                                }
                                else
                                {
                                    foreach (SPListItem spListItemItem in empleaveCollection)
                                    {
                                        decimal leaveBalance;
                                        leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "0" : dataRow[spv.LookupValue.Trim()].ToString());
                                        decimal leaveExcess;
                                        if (IsAccured(spv.LookupValue.Trim()))
                                        {
                                            decimal maximum = GetMaximumLeave(spv.LookupValue.Trim());

                                            if (leaveBalance > maximum)
                                            {
                                                leaveExcess = leaveBalance - maximum;
                                                leaveBalance = maximum;

                                                UpdatedExcessLeaves(dataRow["Employee Id"].ToString(), web.AllUsers[dataRow["TempEmployeeName"].ToString()], spv.LookupValue.Trim(), leaveExcess);
                                            }
                                        }

                                        spListItemItem["Employee ID"] = dataRow["Employee Id"];
                                        spListItemItem["Employee Name"] =
                                            web.AllUsers[dataRow["TempEmployeeName"].ToString()];
                                        spListItemItem["Reporting Manager"] =
                                            web.AllUsers[dataRow["TempReportingTo"].ToString()];
                                        spListItemItem[Utilities.LeaveType] =
                                            leaveType[Utilities.LeaveType].ToString();
                                        spListItemItem["Leave Balance"] = leaveBalance;
                                        spListItemItem["Leave utilized"] = 0;
                                        spListItemItem["Leave Requested"] = 0;
                                        spListItemItem["Year"] = newYear;
                                        spListItemItem.Update();
                                    }
                                }
                            }
                        }

                        UpdateLeaveUpdatedDate();
                    }
                }
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void BtnOLresetClick(object sender, EventArgs e)
        {
            try
            {
                var employeeleaves = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeLeaves);
                var optionalleavecollection = GetListItemCollection(employeeleaves, "Leave Type", "Optional");

                foreach (SPListItem optionalleaves in optionalleavecollection)
                {
                    optionalleaves["Leave Balance"] = 2;
                    optionalleaves.Update();
                    btbolreset.Enabled = false;

                }

            }
            catch (Exception ex)
            {

                lblErr.Text = ex.Message;
            }
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
        internal decimal GetMaximumLeave(string leaveType)
        {
            decimal returnValue = 0;

            var leavetypes = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveType), Utilities.LeaveType, leaveType);
            if (leavetypes.Count > 0)
            {
                foreach (SPListItem leavetype in leavetypes)
                {

                    returnValue += decimal.Parse(leavetype["Maximum"].ToString());

                }
            }
            return returnValue;
        }
        internal bool IsAccured(string leaveType)
        {
            bool returnValue = false;

            var leavetypes = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveType), Utilities.LeaveType, leaveType);
            if (leavetypes.Count > 0)
            {
                foreach (SPListItem leavetype in leavetypes)
                {
                    if (bool.Parse(leavetype["Is Accrued"].ToString()))
                    {
                        return true;
                    }


                }
            }
            return returnValue;
        }

        internal void UpdatedExcessLeaves(string empId, SPUser empname, string leaveType, decimal value)
        {
            var list = SPContext.Current.Web.Lists.TryGetList("Employee Excess Leaves");
            var newItem = list.Items.Add();

            newItem["Employee Id"] = empId;
            newItem["Employee Name"] = empname;
            newItem["Leave Type"] = leaveType;
            newItem["Leave Excess"] = value;
            newItem["Year"] = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
            newItem.Update();
        }
        internal void UpdateLeaveUpdatedDate()
        {

            var list = SPContext.Current.Web.Lists.TryGetList(Utilities.UpdatedDate);
            if (list != null)
            {
                for (int i = list.ItemCount - 1; i >= 0; i--)
                {
                    list.Items[i].Delete();
                }
                list.Update();
            }


            var newItem = list.Items.Add();
            newItem["Month"] = DateTime.Now.Month;
            newItem["Year"] = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
            newItem.Update();
        }
        internal void UpdateCurrentYear(string year)
        {

            var list = SPContext.Current.Web.Lists.TryGetList(Utilities.CurrentYear);
            if (list != null)
            {
                for (int i = list.ItemCount - 1; i >= 0; i--)
                {
                    list.Items[i].Delete();
                }
                list.Update();
            }


            var newItem = list.Items.Add();
            newItem["Title"] = year;
            newItem.Update();
        }

        private DataTable PendingstableStructure()
        {
            var eTable = new DataTable();

            eTable.Columns.Add("Manager");
            eTable.Columns.Add("No Of Leave Pending");
            //eTable.Columns.Add("Cancel");
            return eTable;
        }
        public void PendingLeaves()
        {

            try
            {
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        var managerList = web.Lists.TryGetList(Utilities.ReportingTo);
                        var leavelist = web.Lists.TryGetList(Utilities.LeaveRequest);

                        var pendings = GetListItemCollection(leavelist, "Status", "Pending", "Year",
                                                                                 hdnCurrentYear.Value);
                        if (pendings.Count > 0)
                        {
                            DataTable table = PendingstableStructure();

                            if (leavelist != null)
                            {
                                var managerListDetails = managerList.GetItems();
                                foreach (SPListItem managerListDetail in managerListDetails)
                                {
                                    DataRow dataRow = table.NewRow();
                                    var spv = new SPFieldLookupValue(managerListDetail["Reporting Managers"].ToString());

                                    dataRow["Manager"] = spv.LookupValue;
                                    var pendingDetails = GetListItemCollection(leavelist, "Status", "Pending", "Year",
                                                                               hdnCurrentYear.Value, "RequestedTo",
                                                                               spv.LookupValue);
                                    dataRow["No Of Leave Pending"] = pendingDetails.Count;
                                    table.Rows.Add(dataRow);
                                }
                                btnUpdate.Enabled = false;
                            }



                            ViewState["PendingResult"] = table;
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
    