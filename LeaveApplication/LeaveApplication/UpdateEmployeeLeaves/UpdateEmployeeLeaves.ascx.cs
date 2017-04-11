using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LeaveApplication.UpdateEmployeeLeaves
{
    [ToolboxItemAttribute(false)]
    public partial class UpdateEmployeeLeaves : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public UpdateEmployeeLeaves()
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
                btnnewupdate.Enabled = false;
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
                        btnnewupdate.Enabled = false;
                    }
                    else 
                    { btnnewupdate.Enabled= true; ; }   // Recent change.
                }
            }


            // CHANGE IN Optional Button reset code..

            var olcurrentYear = DateTime.Now.Year;
            string optionalcurrentyear = olcurrentYear.ToString();

            var olupdatingMonths = SPContext.Current.Web.Lists.TryGetList(Utilities.OLupdatemonth).GetItems();
            foreach (SPListItem updatingmonth in olupdatingMonths)
            {
                hdnoptionalmonth.Value = updatingmonth["Title"].ToString();
            }
            //}
            if (int.Parse(hdnoptionalmonth.Value) != DateTime.Now.Month )
            {
                btbolreset.Enabled = false;
            }
            else
            {
                var olupdatedDates = SPContext.Current.Web.Lists.TryGetList(Utilities.OLupdate).GetItems();
                foreach (SPListItem updatedDate in olupdatedDates)
                {

                    int updatedmonth = int.Parse(updatedDate["Month"].ToString());
                    string updatedYear = updatedDate["Year"].ToString();
                    bool iscurrentYear = (optionalcurrentyear == updatedYear);
                    if (iscurrentYear)
                    {
                        btbolreset.Enabled = true;
                    }
                    else
                    {
                        btbolreset.Enabled = false;
                    }                                   // Recent change.
                }
            }


         //// Button reset for  probationary people

         
  var probationaryupdmonth = SPContext.Current.Web.Lists.TryGetList("probationaryUpdating").GetItems();
              var updatedDates1 = SPContext.Current.Web.Lists.TryGetList(Utilities.UpdatedDate).GetItems();
              foreach (SPListItem updatedDate in updatedDates1)
              {

                 hdnupdated.Value =updatedDate["Month"].ToString();

              }
            
            foreach (SPListItem updatingmonth in probationaryupdmonth)
            {
               hdnprobupdating.Value = updatingmonth["Title"].ToString(); /// new hidden field
            }
            //}
            if (int.Parse(hdnprobupdating.Value) !=DateTime.Now.Month)
            {
                btnprobationary.Enabled = false;
            }

            else
            {
                var probationaryupdated = SPContext.Current.Web.Lists.TryGetList("ProbationaryUpdated").GetItems();
                foreach (SPListItem updatedDate in probationaryupdated)
                {

                    int probupdatedmonth = int.Parse(updatedDate["Month"].ToString());
                    int CheckMonth = DateTime.Today.Month;
                    if (probupdatedmonth == int.Parse(hdnprobupdating.Value) && hdnprobupdating.Value == hdnupdated.Value)
                    {
                        btnprobationary.Enabled = false;
                    }

                    else if (CheckMonth==4)
                    {
                        btnprobationary.Enabled = false;
                    }
                  
                        btnprobationary.Enabled = true; 
                  
                }
            }

             PendingLeaves();

            }


           
  
        //        protected void BtnCalculateClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ViewState["Result"] = GetEmployeeLeaves();

        //        //grdvwEmployeeLeaves.DataBind();
        //    }

        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message;
        //    }
        //}

        internal DataTable GetEmpleaves_probationary()
        {
            DataTable dataTable = EmployeeLeavesStructure();
            try
            {
                var employeeList = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeScreen), "Status", "Active");
                SPUser User = SPContext.Current.Web.CurrentUser;
                string UserName = User.Name;

                string previousyear = DateTime.Now.Year - 1 + "-" + DateTime.Now.Year;

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
                                              "2014-2015", "Employee Status", "Active");
                    foreach (SPListItem empleaves in employeeLeaveList)
                    {
                        var spv = new SPFieldLookupValue(empleaves[Utilities.LeaveType].ToString());

                        decimal leaveBalance = 0;
                        decimal leaveDays = 0;
                        decimal myopl = 0;





                        if (employee[Utilities.EmployeeType].ToString().ToLower().Trim() != "permanent")   // Leave balance for probationary.
                        {
                            // Adding carry forward logic to probationary people.

                            var leave_probationary = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveDays),
                                             "Leave Type", spv.LookupValue.Trim(), Utilities.EmployeeType,
                                             "Probationary");
                            foreach (SPListItem leavetype in leave_probationary)   //calculating leave days for Permanent people Only here 
                            {
                                leaveDays = decimal.Parse(leavetype["Leave Days"].ToString());

                            }
                            if (spv.LookupValue.Trim() == "Paid Leave")
                            {
                                //leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());  
                                //// carry forward logic should be updated  here too like in paid leave of permanent.
                                decimal leaves = decimal.Parse(empleaves["Leave Balance"].ToString()) + 1;

                                leaveBalance = leaves;

                            }
                            //else {

                            //    dataRow["Sick Leave"] = 6;
                            //    dataRow["Casual Leave"] = 6;
                            //    dataRow["OPtional"] = 2;

                            //}

                            else if (spv.LookupValue.Trim() == "Comp off")
                            {
                                leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());
                            }
                            else if (spv.LookupValue.Trim() == "Optional")
                            {
                                leaveBalance = leaveDays;
                                var oplspv = new SPFieldLookupValue(empleaves[Utilities.LeaveBalancecolname].ToString());
                                myopl = oplspv.LookupId;
                                leaveBalance = myopl;
                            }
                            else
                            {
                                leaveBalance = 0;

                            }
                            dataRow["Sick Leave"] = decimal.Parse(empleaves["Leave Balance"].ToString());
                            dataRow["Casual Leave"] = decimal.Parse(empleaves["Leave Balance"].ToString());

                        }
                        dataRow[spv.LookupValue.Trim()] = leaveBalance;
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



        internal DataTable GetEmployeeLeaves()
        {
            DataTable dataTable = EmployeeLeavesStructure();
            try
            {
                //  var employeeList = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeScreen).GetItems();
                var employeeList = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeScreen), "Status", "Active");
                SPUser User = SPContext.Current.Web.CurrentUser;
                string UserName = User.Name;

                string previousyear = DateTime.Now.Year - 1 + "-" + DateTime.Now.Year;

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
                                              previousyear, "Employee Status", "Active");
                    foreach (SPListItem empleaves in employeeLeaveList)
                    {
                        var spv = new SPFieldLookupValue(empleaves[Utilities.LeaveType].ToString());

                        decimal leaveBalance = 0;
                        decimal leaveDays = 0;
                        decimal myopl = 0;




                        var leaveTypeList = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveDays),
                                               "Leave Type", spv.LookupValue.Trim(), Utilities.EmployeeType,
                                               "Permanent");
                        foreach (SPListItem leavetype in leaveTypeList)   //calculating leave days for Permanent people Only here 
                        {
                            leaveDays = decimal.Parse(leavetype["Leave Days"].ToString());

                        }


                        if (employee[Utilities.EmployeeType].ToString().Trim() == "Permanent")
                        {

                            if (spv.LookupValue.Trim() == "Paid Leave")
                            {
                                decimal leaveExcess;
                                decimal leaves = decimal.Parse(empleaves["Leave Balance"].ToString()) + (leaveDays * 12);
                                if (leaves > 30)
                                {
                                    //leaves = 30;
                                    leaveExcess = leaves - 30;

                                    leaves = 30;
                                    using (var site = new SPSite(SPContext.Current.Site.Url))
                                    {
                                        using (var web = site.OpenWeb())
                                        {
                                            UpdatedExcessLeaves(dataRow["Employee Id"].ToString(), web.AllUsers[dataRow["TempEmployeeName"].ToString()], spv.LookupValue.Trim(), leaveExcess);

                                        }
                                    }
                                    leaveBalance = leaves;
                                }

                                if (leaves % 1 == 0)
                                {
                                    int noOfleaves = Convert.ToInt16(leaves); /// change required. if and else here should be alternative.(Illogical)
                                    leaveBalance = noOfleaves;
                                }
                                else
                                {
                                    leaveBalance = leaves;
                                }

                            }
                            else if (spv.LookupValue.Trim() == "Casual Leave")
                            {
                                //leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());
                                leaveBalance = 6;
                            }
                            else if (spv.LookupValue.Trim() == "Sick Leave")
                            {
                                leaveBalance = 6;
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
                                decimal leaves = (leaveDays * 12);  // Unnecessary condition check here. directly can assign leaves to leavebalance..instead it should be flooring or ceiling.
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
                        else if (employee[Utilities.EmployeeType].ToString().ToLower().Trim() != "permanent")   // Leave balance for probationary.
                        {
                            // Adding carry forward logic to probationary people.

                            var leave_probationary = GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveDays),
                                             "Leave Type", spv.LookupValue.Trim(), Utilities.EmployeeType,
                                             "Probationary");
                            foreach (SPListItem leavetype in leave_probationary)   //calculating leave days for Permanent people Only here 
                            {
                                leaveDays = decimal.Parse(leavetype["Leave Days"].ToString());

                            }
                            if (spv.LookupValue.Trim() == "Paid Leave")
                            {
                                //leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());  
                                //// carry forward logic should be updated  here too like in paid leave of permanent.
                                decimal leaves = decimal.Parse(empleaves["Leave Balance"].ToString()) + 1;
                                if (leaves > 30)
                                {
                                    leaves = 30;
                                }


                                else
                                {
                                    leaveBalance = leaves;
                                }
                            }
                            //else {

                            //    dataRow["Sick Leave"] = 6;
                            //    dataRow["Casual Leave"] = 6;
                            //    dataRow["OPtional"] = 2;

                            //}

                            else if (spv.LookupValue.Trim() == "Comp off")
                            {
                                leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());
                            }
                            else if (spv.LookupValue.Trim() == "Optional")
                            {
                                leaveBalance = leaveDays;
                                var oplspv = new SPFieldLookupValue(empleaves[Utilities.LeaveBalancecolname].ToString());
                                myopl = oplspv.LookupId;
                                leaveBalance = myopl;
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

                        dataRow["Sick Leave"] = 0;
                        dataRow["Casual Leave"] = 0;

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

        //protected void BtnUpdateClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string newYear = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
        //        using (var site = new SPSite(SPContext.Current.Site.Url))
        //        {
        //            using (var web = site.OpenWeb())
        //            {
        //                UpdateCurrentYear(newYear);
        //                var employeeLeaves = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeLeaves);
        //                var leaveTypes =
        //                    GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveDays),
        //                                          Utilities.EmployeeType, "Permanent");
        //                DataTable dataTable = GetEmployeeLeaves();
        //                foreach (DataRow dataRow in dataTable.Rows)
        //                {
        //                    foreach (SPListItem leaveType in leaveTypes)
        //                    {
        //                        var spv = new SPFieldLookupValue(leaveType[Utilities.LeaveType].ToString());
        //                        var empleaveCollection = GetListItemCollection(employeeLeaves, "Employee ID",
        //                                                                       dataRow["Employee Id"].ToString(),
        //                                                                       "Leave Type", spv.LookupValue, "Year",
        //                                                                       newYear);

        //                        //employeeleavelist.GetItems(employeeleaveDaysQuery);
        //                        if (empleaveCollection.Count == 0)
        //                        {
        //                            decimal leaveBalance;
        //                            leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "0" : dataRow[spv.LookupValue.Trim()].ToString());
        //                            decimal leaveExcess;
        //                            if (IsAccured(spv.LookupValue.Trim()))
        //                            {
        //                                decimal maximum = GetMaximumLeave(spv.LookupValue.Trim());
        //                                if (leaveBalance > maximum)
        //                                {
        //                                    leaveExcess = leaveBalance - maximum;
        //                                    leaveBalance = maximum;

        //                                    UpdatedExcessLeaves(dataRow["Employee Id"].ToString(), web.AllUsers[dataRow["TempEmployeeName"].ToString()], spv.LookupValue.Trim(), leaveExcess);
        //                                }
        //                            }
        //                            var newItem = employeeLeaves.Items.Add();

        //                            newItem["Employee ID"] = dataRow["Employee Id"];
        //                            newItem["Employee Name"] = web.AllUsers[dataRow["TempEmployeeName"].ToString()];
        //                            newItem["Reporting Manager"] = web.AllUsers[dataRow["TempReportingTo"].ToString()];
        //                            newItem[Utilities.LeaveType] = leaveType[Utilities.LeaveType].ToString();
        //                            newItem["Leave Balance"] = leaveBalance;
        //                            newItem["Employee Type"] = dataRow["Employee Type"];
        //                            newItem["Leave utilized"] = 0;
        //                            newItem["Leave Requested"] = 0;
        //                            newItem["Year"] = newYear;
        //                            newItem["Employee Status"] = "Active";
        //                            newItem.Update();


        //                        }
        //                        else
        //                        {
        //                            foreach (SPListItem spListItemItem in empleaveCollection)
        //                            {
        //                                decimal leaveBalance;
        //                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "0" : dataRow[spv.LookupValue.Trim()].ToString());
        //                                decimal leaveExcess;
        //                                if (IsAccured(spv.LookupValue.Trim()))
        //                                {
        //                                    decimal maximum = GetMaximumLeave(spv.LookupValue.Trim());

        //                                    if (leaveBalance > maximum)
        //                                    {
        //                                        leaveExcess = leaveBalance - maximum;
        //                                        leaveBalance = maximum;

        //                                        UpdatedExcessLeaves(dataRow["Employee Id"].ToString(), web.AllUsers[dataRow["TempEmployeeName"].ToString()], spv.LookupValue.Trim(), leaveExcess);
        //                                    }
        //                                }

        //                                spListItemItem["Employee ID"] = dataRow["Employee Id"];
        //                                spListItemItem["Employee Name"] =
        //                                    web.AllUsers[dataRow["TempEmployeeName"].ToString()];
        //                                spListItemItem["Reporting Manager"] =
        //                                    web.AllUsers[dataRow["TempReportingTo"].ToString()];
        //                                spListItemItem[Utilities.LeaveType] =
        //                                    leaveType[Utilities.LeaveType].ToString();
        //                                spListItemItem["Leave Balance"] = leaveBalance;
        //                                spListItemItem["Leave utilized"] = 0;
        //                                spListItemItem["Leave Requested"] = 0;
        //                                spListItemItem["Year"] = newYear;
        //                                spListItemItem.Update();
        //                            }
        //                        }
        //                    }
        //                }

        //                UpdateLeaveUpdatedDate();
        //                lblupdation.Text = "Employee Leaves List contents are Updated.";
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message;
        //    }
        //}

        protected void BtnOLresetClick(object sender, EventArgs e)
        {
            try
            {
                string newyear = DateTime.Now.Year.ToString(); ;
                var employeeleaves = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeLeaves);
                var optionalleavecollection = GetListItemCollection(employeeleaves, "Leave Type", "Optional");

                foreach (SPListItem optionalleaves in optionalleavecollection)
                {
                    optionalleaves["Leave Balance"] = 2;
                    optionalleaves.Update();
                    btbolreset.Enabled = false;

                }

                updateoptionallist();
                lblupdation.Text = "Employee Optional Leaves Updated for The Next Year" + " '" + newyear + "'";


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
            newItem["Title"] = DateTime.Now.ToString("MMMM");
            newItem["Month"] = DateTime.Now.Month;
            newItem["Year"] = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
            newItem.Update();
        }

        internal void updateoptionallist()
        {

            var list = SPContext.Current.Web.Lists.TryGetList("OLupdate");
            if (list != null)
            {
                for (int i = list.ItemCount - 1; i >= 0; i--)
                {
                    list.Items[i].Delete();
                }
                list.Update();
            }


            var newItem = list.Items.Add();
            int str = DateTime.Now.Year + 1;
            newItem["Year"] = str.ToString();
            newItem["Title"] = DateTime.Now.ToString("MMMM");
            newItem["Month"] = DateTime.Now.Month;
            newItem.Update();
        }

        internal void Updateprobationary()
        {

            var list = SPContext.Current.Web.Lists.TryGetList("probationaryupdated");
            if (list != null)
            {
                for (int i = list.ItemCount - 1; i >= 0; i--)
                {
                    list.Items[i].Delete();
                }
                list.Update();
            }


            var newItem = list.Items.Add();
            newItem["Title"] = DateTime.Now.ToString("MMMM");
            newItem["Month"] = DateTime.Now.Month.ToString();
            
            newItem.Update();
            /// 
            var list1 = SPContext.Current.Web.Lists.TryGetList("ProbationaryUpdating");
            if (list1 != null)
            {
                for (int i = list1.ItemCount - 1; i >= 0; i--)
                {
                    list1.Items[i].Delete();
                }
                list1.Update();
            }


            var newItem1 = list1.Items.Add();
            int month = DateTime.Now.Month;
            int month2 = month + 1;

            newItem1["Title"] = month2.ToString();


            newItem1.Update();

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
                                btnnewupdate.Enabled = false;
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


        // New View Report Code
        protected void btnnewviewreport_Click(object sender, EventArgs e)
        {
            var currentYear = SPContext.Current.Web.Lists.TryGetList(Utilities.CurrentYear).GetItems();
            foreach (SPListItem currentYearValue in currentYear)
            {
                hdnCurrentYear.Value = currentYearValue["Title"].ToString();
            }
            DateTime now = DateTime.Now;
            int var = now.Month;


            try
            {
                ViewState["Result"] = GetEmployeeLeaves1();

                lblupdation.Text = "Employee Leaves List Status After the changes.";

                //grdvwEmployeeLeaves.DataBind();
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }


        }

        internal DataTable GetEmployeeLeaves1()
        {
            DataTable dataTable = EmployeeLeavesStructure();

            try
            {
                SPListItemCollection employeeList;
                SPUser user = SPContext.Current.Web.CurrentUser;

                string currentUser = user.Name;
                if (IsMemberInGroup("Admin"))
                {

                    employeeList = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeScreen).GetItems();

                }
                else
                {
                    employeeList =
                        GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeScreen),
                                              Utilities.Manager, currentUser);
                }

                foreach (SPListItem employee in employeeList)
                {
                    DataRow dataRow = dataTable.NewRow();

                    dataRow["Employee Id"] = employee["Employee ID"].ToString();
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
                    dataRow["Reporting To"] = entityRep.DisplayText.ToString();

                    // var spvemptype = new SPFieldLookupValue(employee[Utilities.EmployeeType].ToString());
                    dataRow["Employee Type"] = employee[Utilities.EmployeeType].ToString();
                    var employeeLeaveList =
                        GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeLeaves),
                                              "Employee ID", employee["Employee ID"].ToString(), "Year",
                                              hdnCurrentYear.Value);


                    foreach (SPListItem empleaves in employeeLeaveList)
                    {
                        var spv = new SPFieldLookupValue(empleaves[Utilities.LeaveType].ToString());



                        decimal leaveBalance = 0;


                        leaveBalance = decimal.Parse(empleaves["Leave Balance"].ToString());


                        if (leaveBalance > 0)
                        {
                            dataRow[spv.LookupValue.Trim()] = leaveBalance;
                        }
                        else
                        {

                            dataRow[spv.LookupValue.Trim()] = 0;
                        }

                    }
                    if (employee[Utilities.EmployeeType].ToString().ToLower().Trim() != "permanent")
                    {

                        dataRow["Sick Leave"] = 0;
                        dataRow["Casual Leave"] = 0;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            return dataTable;

        }
        internal DataTable EmployeeLeavesStructure1()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Employee Id");
            dataTable.Columns.Add("Employee Name");
            dataTable.Columns.Add("Employee Type");
            dataTable.Columns.Add("Reporting To");

            var items = SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveType).GetItems();
            foreach (SPListItem item in items)
            {
                //  var spv = new SPFieldLookupValue(item[Utilities.LeaveType].ToString());
                dataTable.Columns.Add(item[Utilities.LeaveType].ToString());
            }
            return dataTable;
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

        internal SPListItemCollection GetListItemCollection1(SPList spList, string key, string value)
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

        internal SPListItemCollection GetListItemCollection1(SPList spList, string keyOne, string valueOne, string keyTwo, string valueTwo)
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

        protected void btnnewupdate_Click(object sender, EventArgs e)
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






            //// Updating the list items of Employee Leaves for next Financial year.

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
                        foreach (DataRow dataRow in dataTable.Rows)  // datarows already created based on the year 2013-2014.
                        {
                               
                            
                            foreach (SPListItem leaveType in leaveTypes)
                            {
                                var spv = new SPFieldLookupValue(leaveType[Utilities.LeaveType].ToString());
                                var empleaveCollection = GetListItemCollection(employeeLeaves, "Employee ID",
                                                                               dataRow["Employee Id"].ToString(),
                                                                               "Leave Type", spv.LookupValue, "Year",
                                                                               newYear);

                               
                              
                                   
                                    if (empleaveCollection.Count == 0)
                                {


                                        string str = dataRow["Employee Type"].ToString().Trim();
                                        if (dataRow["Employee Type"].ToString().Trim() == "Permanent")
                                    
                                        
                                        {

                                        

                                            decimal leaveBalance = 0;

                                            if (IsAccured(spv.LookupValue.Trim()))
                                            {


                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "12" : dataRow[spv.LookupValue.Trim()].ToString());


                                            }
                                            else if (spv.LookupValue.Trim() == "Sick Leave" || spv.LookupValue.Trim() == "Casual Leave")
                                            {

                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "6" : dataRow[spv.LookupValue.Trim()].ToString());
                                            }
                                            else
                                            {
                                                leaveBalance = 0;
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
                                        if (dataRow["Employee Type"].ToString().Trim() == "Probationary")
                                        {
                                            decimal leaveBalance = 0;

                                            if (IsAccured(spv.LookupValue.Trim()))
                                            {


                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "1" : dataRow[spv.LookupValue.Trim()].ToString());



                                            }
                                            else if (spv.LookupValue.Trim() == "Sick Leave" || spv.LookupValue.Trim() == "Casual Leave")
                                            {

                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "0" : dataRow[spv.LookupValue.Trim()].ToString());
                                            }
                                            else
                                            {
                                                leaveBalance = 0;
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


                                    }
                                }

                                else
                                {

                                    foreach (SPListItem spListItemItem in empleaveCollection)
                                    {

                                        if (spListItemItem[Utilities.EmployeeType].ToString().Trim() == "Permanent")
                                        {
                                            decimal leaveBalance = 0;
                                            //leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "0" : dataRow[spv.LookupValue.Trim()].ToString());
                                            decimal leaveExcess;
                                            //if (IsAccured(spv.LookupValue.Trim()))
                                            //{
                                            //    decimal maximum = GetMaximumLeave(spv.LookupValue.Trim());

                                            //    if (leaveBalance > maximum)
                                            //    {
                                            //        leaveExcess = leaveBalance - maximum;
                                            //        leaveBalance = maximum;

                                            //        UpdatedExcessLeaves(dataRow["Employee Id"].ToString(), web.AllUsers[dataRow["TempEmployeeName"].ToString()], spv.LookupValue.Trim(), leaveExcess);
                                            //    }
                                            //}
                                            if (IsAccured(spv.LookupValue.Trim()))
                                            {


                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "12" : dataRow[spv.LookupValue.Trim()].ToString());



                                            }
                                            else if (spv.LookupValue.Trim() == "Sick Leave" || spv.LookupValue.Trim() == "Casual Leave")
                                            {

                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "6" : dataRow[spv.LookupValue.Trim()].ToString());
                                            }
                                            else
                                            {
                                                leaveBalance = 0;
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

                                        else
                                        {
                                            decimal leaveBalance = 0;

                                            if (IsAccured(spv.LookupValue.Trim()))
                                            {


                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "1" : dataRow[spv.LookupValue.Trim()].ToString());



                                            }
                                            else if (spv.LookupValue.Trim() == "Sick Leave" || spv.LookupValue.Trim() == "Casual Leave")
                                            {

                                                leaveBalance = decimal.Parse(string.IsNullOrEmpty(dataRow[spv.LookupValue.Trim()].ToString()) ? "0" : dataRow[spv.LookupValue.Trim()].ToString());
                                            }
                                            else
                                            {
                                                leaveBalance = 0;
                                            }
                                            spListItemItem["Leave Balance"] = leaveBalance;
                                            spListItemItem.Update();

                                        }
                                    }

                                }
                            }
                        }

                        UpdateLeaveUpdatedDate();
                        lblupdation.Text = "Employee Paid Leaves Updated for The Current Year =>" + " '" + newYear + "'";
                    }
                }
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnprobationary_Click(object sender, EventArgs e)
        {
            Updateprobationary();
            try
            {
                ViewState["Result"] = GetEmpleaves_probationary();

                //grdvwEmployeeLeaves.DataBind();
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            try
            {
                string newYear = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;

                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                       
                        var employeeLeaves = SPContext.Current.Web.Lists.TryGetList(Utilities.EmployeeLeaves);
                        var leaveTypes =
                            GetListItemCollection(SPContext.Current.Web.Lists.TryGetList(Utilities.LeaveDays),
                                                  Utilities.EmployeeType, "Probationary");
                        DataTable dataTable = GetEmpleaves_probationary();
                        foreach (DataRow dataRow in dataTable.Rows)  // datarows already created based on the year 2013-2014.
                        {
                            foreach (SPListItem leaveType in leaveTypes)
                            {
                                var spv = new SPFieldLookupValue(leaveType[Utilities.LeaveType].ToString());
                                var empleaveCollection = GetListItemCollection(employeeLeaves, "Employee ID",
                                                                               dataRow["Employee Id"].ToString(),
                                                                               "Leave Type", spv.LookupValue, "Year",
                                                                              hdnCurrentYear.Value);


                                    foreach (SPListItem spListItemItem in empleaveCollection)
                                    {

                                        if (spListItemItem[Utilities.EmployeeType].ToString().Trim() == "Probationary")
                                        {
                                            decimal leaveBalance = 0;
                                           
                                            if (IsAccured(spv.LookupValue.Trim()))
                                            {


                                                leaveBalance = decimal.Parse( dataRow[spv.LookupValue.Trim()].ToString());



                                            }
                                            else if (spv.LookupValue.Trim() == "Sick Leave" || spv.LookupValue.Trim() == "Casual Leave")
                                            {

                                                leaveBalance = decimal.Parse( dataRow[spv.LookupValue.Trim()].ToString());
                                            }
                                            else if (spv.LookupValue.Trim() == "Optional")
                                            {
                                                leaveBalance = decimal.Parse(dataRow[spv.LookupValue.Trim()].ToString());
                                            }
                                            else { }


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
                                            
                                            spListItemItem.Update();
                                        }

                                     
                                    }

                                }
                            }
                        }

                  
                        lblupdation.Text = "Paid leaves updated for Probationary people";
                        btnprobationary.Enabled = false;
                    }
                }
            
    

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

       
}


        }

    }

