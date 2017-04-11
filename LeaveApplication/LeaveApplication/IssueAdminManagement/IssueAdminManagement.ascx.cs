using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.WebControls;
namespace LeaveApplication.IssueAdminManagement
{
    [ToolboxItemAttribute(false)]
    public partial class IssueAdminManagement : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public IssueAdminManagement()
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
                DataBind();
            }
        }

        public void DataBind()
        {

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite Osite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb Oweb = Osite.OpenWeb())
                    {
                        SPList Olist = Oweb.Lists[Utilities.IssueTrackerListName];
                        var Ospquery = new SPQuery();
                        Ospquery.Query = @"<Where><Eq><FieldRef Name='Issue_x0020_Status' /><Value Type='Choice'>New</Value></Eq></Where>";
                        SPListItemCollection Olistcollection = Olist.GetItems(Ospquery);
                        var dt = new DataTable();
                        dt.Columns.Add("Issue No");
                        dt.Columns.Add("Issue Details");
                        foreach (SPListItem item in Olistcollection)
                        {
                            DataRow row = dt.NewRow();

                            row["Issue No"] = item["Issue No"].ToString();
                            row["Issue Details"] = Regex.Replace(Convert.ToString(item["Issue Details"]), "<[^>]*>", string.Empty);
                            dt.Rows.Add(row);

                        }

                        DataView mydataview = new DataView(dt);
                        dt = mydataview.ToTable();
                        gvIssueAdminView.DataSource = dt;
                        gvIssueAdminView.DataBind();

                    }

                }
            });



        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {

            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            int index = row.RowIndex;
            string IssueNo = row.Cells[0].Text; // here we are
            DateTimeControl FirstrowDate = (DateTimeControl)gvIssueAdminView.Rows[index].FindControl("txtIssueDueDate");
            PeopleEditor ppAuthor = (PeopleEditor)gvIssueAdminView.Rows[index].FindControl("txtAssignTo");
            TextBox Comments = (TextBox)gvIssueAdminView.Rows[index].FindControl("txtcomments");

            if (ppAuthor.Entities.Count != 0)
            {
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
                                if (!FirstrowDate.IsDateEmpty)
                                {
                                    item["Issue Deadline"] = FirstrowDate.SelectedDate.ToShortDateString();
                                }
                                item["Assign To"] = Utilities.UserValueCollection(Oweb, ppAuthor.CommaSeparatedAccounts.ToString() + ";");
                                item["Comments"] = Comments.Text;
                                item["Issue Status"] = "Assigned";
                                Oweb.AllowUnsafeUpdates = true;
                                item.Update();
                                Oweb.AllowUnsafeUpdates = false;
                              
                                SPUser user = SPContext.Current.Web.EnsureUser(ppAuthor.CommaSeparatedAccounts.ToString());
                                Utilities.SendNotification(Oweb, user.Email + ";", "New Leave Application Issue has been assigned.", IssueNo, "Yes");
                                DataBind();
                            }

                        }

                    }

                });
                

            }
            else
            {

                lblerror.Text = "Please Enter Assigne To";
            
            }

            
            foreach (GridViewRow currentrow in gvIssueAdminView.Rows)
            {
                PeopleEditor ppAuthorNew = (PeopleEditor)currentrow.FindControl("txtAssignTo");
                ppAuthorNew.Accounts.Clear();
                ppAuthorNew.Entities.Clear();
                ppAuthorNew.ResolvedEntities.Clear();
            
            }

           
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            int index = row.RowIndex;
            string IssueNo = row.Cells[0].Text;
            TextBox Comments = (TextBox)gvIssueAdminView.Rows[index].FindControl("txtcomments");
            string ApproverComments = Comments.Text.Trim();
            if (!string.IsNullOrEmpty(ApproverComments))
            {
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
                                string Author = item["Author"].ToString();
                                string[] AuthorEmail = Author.Split('#');
                                SPUser user = SPContext.Current.Web.EnsureUser(AuthorEmail[1]);
                                item["Comments"] = Comments.Text;
                                item["Issue Status"] = "Rejected";
                                Oweb.AllowUnsafeUpdates = true;
                                item.Update();
                                Oweb.AllowUnsafeUpdates = false;
                                Utilities.SendNotification(Oweb, user.Email + ";", "New Leave Application Issue has been assigned.", IssueNo, "Yes");
                                DataBind();
                            }

                        }

                    }

                });
            }
            else
            {

                lblerror.Text = "Please Enter Comments";

            }

            DataBind();
            foreach (GridViewRow currentrow in gvIssueAdminView.Rows)
            {
                PeopleEditor ppAuthorNew = (PeopleEditor)currentrow.FindControl("txtAssignTo");
                ppAuthorNew.Accounts.Clear();
                ppAuthorNew.Entities.Clear();
                ppAuthorNew.ResolvedEntities.Clear();

            }
        }


    }
}
