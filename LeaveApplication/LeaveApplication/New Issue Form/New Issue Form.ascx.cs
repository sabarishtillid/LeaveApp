using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WebControls;
using System.Text;
using System.Collections.Specialized;

namespace LeaveApplication.New_Issue_Form
{
    [ToolboxItemAttribute(false)]
    public partial class New_Issue_Form : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public New_Issue_Form()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var site = SPContext.Current.Site.ID;
            var web = SPContext.Current.Web;
            
            if (!Page.IsPostBack)
            {
                txttitle.Text = Utilities.itemcounter(web).ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                   // PickerEntity pckEntity = (PickerEntity)PeopleEditor1.ResolvedEntities[0];
                  //  string email = pckEntity.EntityData["Email"].ToString();


                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList list = web.Lists.TryGetList("Issue Tracker");
                            if (list != null)
                            {
                                SPListItem newitem = list.Items.Add();
                                {
                                    
                                    newitem["Issue No"] =  txttitle.Text;
                                    newitem["Issue Details"] = txtdetails.Text;
                                    newitem["Author"] = SPContext.Current.Web.CurrentUser;

                                    web.AllowUnsafeUpdates = true;
                                    newitem.Update();
                                    web.AllowUnsafeUpdates = false;
                                    string subject = "New Issue has been Initiated.";                                    
                                    Utilities.SendNotification(web, Utilities.GetEmailAddressFromGroup(web), subject, txttitle.Text, string.Empty);
                                    lblconfirm.Visible = true;
                                    txttitle.Text = string.Empty;
                                    txtdetails.Text = string.Empty;
                                    lblconfirm.Text = " ISSUE INSERTED SUCCESSFULLY";
                                    txttitle.Text = Utilities.itemcounter(web).ToString();
                                }
                            }

                            else
                            {
                                lblerror.Visible = true;
                                lblerror.Text = "something is wrong and please check the details";
                            }

                        }

                    }


                });
            }
            catch (Exception ex)
            {
                lblerror.Visible = true;
                lblerror.Text = ex.Message.ToString();

            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            txttitle.Text = string.Empty;
            txtdetails.Text = string.Empty;

        }
    }
}
