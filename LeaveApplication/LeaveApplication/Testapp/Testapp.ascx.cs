using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;

namespace LeaveApplication.Testapp
{
    [ToolboxItemAttribute(false)]
    public partial class Testapp : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Testapp()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists.TryGetList("Employee Registration");
                        if (list != null)
                        {
                            SPListItem NewItem = list.Items.Add();
                            {
                                web.AllowUnsafeUpdates = true;
                                NewItem["Employee Name"] = txtempname.Text;
                                NewItem["Designation"] = drpdesg.SelectedItem.ToString();
                                NewItem["Address"] = txtaddr.Text;
                                NewItem["Email"] = txtemail.Text;
                                NewItem["Contact No"] = txtcontact.Text;
                                NewItem.Update();
                                Alert.Text = "Registration Successful";

                            }
                        }
                        else
                        {
                            Alert.Text = "List not found";
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Alert.Text = ex.Message.ToString();
            }
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            txtempname.Text = "";
            txtemail.Text = "";
            drpdesg.SelectedIndex = -1;
            txtcontact.Text = "";
            txtaddr.Text = "";
            Alert.Text = "";       
        }  
        }
    }

