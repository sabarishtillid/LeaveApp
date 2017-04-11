using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Linq;

namespace LeaveApplication.EmployeeInformation
{
    [ToolboxItemAttribute(false)]
    public partial class EmployeeInformation : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public EmployeeInformation()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdncurrentURl.Value = SPContext.Current.Site.Url;
                var currentYear = SPContext.Current.Web.Lists.TryGetList(Utilities.CurrentYear).GetItems();
                foreach (SPListItem currentYearValue in currentYear)
                {
                    hdnCurrentYear.Value = currentYearValue["Title"].ToString();
                }

                //empGridView.DataSource = LoadEmpDetails();
                //empGridView.DataBind();
                LoadEmpDetails();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        private DataTable employeeTableStructure()
        {
            var eTable = new DataTable();

            eTable.Columns.Add("Employee Id");
            eTable.Columns.Add("Employee Name");
            eTable.Columns.Add("Employee Type");
            //eTable.Columns.Add("Sum of Balance Leave");
            eTable.Columns.Add("Edit");

            //   eTable.Columns.Add("Delete");

            return eTable;
        }

        private void LoadEmpDetails()
        {
            DataTable emptable = employeeTableStructure();

            try
            {
                using (var site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (var web = site.OpenWeb())
                    {
                        var emplist = web.Lists.TryGetList(Utilities.EmployeeScreen);
                        var OSPQuery = new SPQuery();
                        OSPQuery.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Choice'>Active</Value></Eq></Where>";
                        var itemCollection = emplist.GetItems(OSPQuery);
                        foreach (SPListItem spListItem in itemCollection)
                        {
                            DataRow dataRow = emptable.NewRow();
                            var empLeavelist = web.Lists.TryGetList(Utilities.EmployeeLeaves);

                            //           var empidField = empLeavelist.Fields["Employee ID"];
                            //                        var query = new SPQuery
                            //                                        {
                            //                                            Query = @"<Where>
                            //                                                            <Eq>
                            //                                                        <FieldRef Name='" + empidField.InternalName + @"' />
                            //                                                                     <Value Type='Text'>" + spListItem[Utilities.EmployeeId] + @"</Value>
                            //                                                            </Eq>
                            //                                                     </Where>"
                            //                                        };
                            var collection = GetListItemCollection(empLeavelist, "Employee ID",
                                                                   spListItem[Utilities.EmployeeId].ToString(), "Year",
                                                                   hdnCurrentYear.Value);

                            // empLeavelist.GetItems(query);

                            float balanceLeave =
                                collection.Cast<SPListItem>().Sum(
                                    listItem => float.Parse(listItem["Leave Balance"].ToString()));
                            var empname = new SPFieldLookupValue(spListItem["Employee Name"].ToString());

                            dataRow["Employee Id"] = spListItem[Utilities.EmployeeId].ToString();

                            // .Replace(spListItem["Title"].ToString().Substring(0, 3),"")
                            dataRow["Employee Name"] = empname.LookupValue;
                            dataRow["Employee Type"] = spListItem[Utilities.EmployeeType].ToString();
                            //dataRow["Sum of Balance Leave"] = balanceLeave;
                            string url = "'" + site.Url + "/SitePages/EditEmployeeInformation.aspx?Empid=" +
                                         spListItem.ID + "'";

                            dataRow["Edit"] =
                                "<a href=\"JavaScript:openDialog(" + url + ");\">Edit</a>";

                            emptable.Rows.Add(dataRow);
                        }

                        ViewState["Result"] = emptable;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
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
    }
}
