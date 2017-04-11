using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;



namespace LeaveApplication.DeveloperManagement
{
    [ToolboxItemAttribute(false)]
    public partial class DeveloperManagement : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeveloperManagement()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }


        public void LoadData()
        {
            
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite Osite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb Oweb = Osite.OpenWeb())
                    {
                        string currentuser = SPContext.Current.Web.CurrentUser.Name.ToString();
                        SPList Olist = Oweb.Lists[Utilities.IssueTrackerListName];
                        var Ospquery = new SPQuery();
                        Ospquery.Query = @"<Where><And><Eq><FieldRef Name='Assign_x0020_To' /><Value Type='User'>" + currentuser + "</Value></Eq><And><Eq><FieldRef Name='Issue_x0020_Status' /><Value Type='Choice'>Assigned</Value></Eq><And><Neq><FieldRef Name='Issue_x0020_Status' /><Value Type='Choice'>New</Value></Neq><And><Neq><FieldRef Name='Issue_x0020_Status' /><Value Type='Choice'>Closed</Value></Neq><Neq><FieldRef Name='Issue_x0020_Status' /><Value Type='Choice'>Rejected</Value></Neq></And></And></And></And></Where>";
                        
                        SPListItemCollection Olistcollection = Olist.GetItems(Ospquery);
                        var dt = new DataTable();
                        dt.Columns.Add("Issue No");
                        dt.Columns.Add("Issue Details");
                        dt.Columns.Add("Comments");
                        
                        foreach (SPListItem item in Olistcollection)
                        {
                            DataRow row = dt.NewRow();

                            row["Issue No"] = item["Issue No"].ToString();
                            row["Issue Details"] = Regex.Replace(Convert.ToString(item["Issue Details"]), "<[^>]*>", string.Empty);
                            row["Comments"] = Regex.Replace(Convert.ToString(item["Comments"]), "<[^>]*>", string.Empty); 
                            dt.Rows.Add(row);

                        }

                        DataView mydataview = new DataView(dt);
                        dt = mydataview.ToTable();
                        gvdeveloperview.DataSource = dt;
                        gvdeveloperview.DataBind();

                        //lblerror.Text = "loaded but still more functions have to be set in";
                        
                        


                    }

                }
            });

        }

        protected void btnsave_click(object sender, EventArgs e)
        {

            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            int index = row.RowIndex;
            string IssueNo = row.Cells[0].Text; // here we are
            //DateTimeControl FirstrowDate = (DateTimeControl)gvdeveloperview.Rows[index].FindControl("txtIssueDueDate");
            //PeopleEditor ppAuthor = (PeopleEditor)gvdeveloperview.Rows[index].FindControl("txtAssignTo");
            TextBox Comments = (TextBox)gvdeveloperview.Rows[index].FindControl("txtcomments");
            DropDownList status = (DropDownList)gvdeveloperview.Rows[index].FindControl("dwnstatus");
            string selectedvalue = status.SelectedValue;

           
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite Osite = new SPSite(SPContext.Current.Site.ID))
                    {

                        using (SPWeb Oweb = Osite.OpenWeb())
                        {

                            SPList Olist = Oweb.Lists[Utilities.IssueTrackerListName];
                            var Ospquery = new SPQuery();
                            Ospquery.Query = @"<Where><Eq><FieldRef Name='Issue_x0020_No' /><Value Type='Text'>" + IssueNo + "</Value></Eq></Where>";
                            SPListItemCollection Olistcollection = Olist.GetItems(Ospquery);
                            foreach (SPListItem item in Olistcollection)
                            {
                               
                                //item["Assign To"] = Utilities.UserValueCollection(Oweb, ppAuthor.CommaSeparatedAccounts.ToString() + ";");
                                item["Comments"] = Comments.Text;
                                item["Issue Status"] = selectedvalue;
                                Oweb.AllowUnsafeUpdates = true;
                                item.Update();
                                Oweb.AllowUnsafeUpdates = false;

                                string AdminEmail = Utilities.GetEmailAddressFromGroup(Oweb);
                                string GetmailID = item["Author"].ToString();
                                string[] AuthorMailID =  GetmailID.Split('#');
                                SPUser createdby = SPContext.Current.Web.EnsureUser(AuthorMailID[1]);
                                string user = createdby.Email + ";" + AdminEmail;
                                if(selectedvalue == "Closed")
                                {
                                
                                Utilities.SendNotification(Oweb, user + ";", "Issue "+IssueNo+" is Solved.", IssueNo);
                                }
                                //SPUser user = SPContext.Current.Web.EnsureUser(ppAuthor.CommaSeparatedAccounts.ToString());
                                //Utilities.SendNotification(Oweb, user.Email + ";", "New Leave Application Issue has been assigned.", IssueNo, "Yes");
                              
                            }
                           
                            
                            //PickerEntity pckentity = (PickerEntity)ppAuthor.ResolvedEntities[0];
                            //string email = pckentity.EntityData["Email"].ToString();
                            //SPUtility.SendEmail(Oweb, true, true, email, "Issue Notification", Comments.ToString());
                            LoadData();
                            lblerror.Text = "Issue updated by developer";



                        }

                    }

                });
              

            }
          

            //foreach (GridViewRow currentrow in gvdeveloperview.Rows)
            //{
            //    PeopleEditor ppAuthorNew = (PeopleEditor)currentrow.FindControl("txtAssignTo");
            //    ppAuthorNew.Accounts.Clear();
            //    ppAuthorNew.Entities.Clear();
            //    ppAuthorNew.ResolvedEntities.Clear();

            //}


        }
      
    }

