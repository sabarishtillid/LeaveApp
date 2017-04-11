using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace LeaveApplication
{
    class Timerjobleaves : SPJobDefinition
    {
        public const string jobname = "Employee leaves";
        public Timerjobleaves() : base() { }
        public Timerjobleaves(SPWebApplication webapplication)
            : base(jobname, webapplication, null, SPJobLockType.Job)
        {
            Title = "EmployeeLeaves Timerjob";
        }

        public override void Execute(Guid targetInstanceId)
        {
            SPWebApplication webapp = this.Parent as SPWebApplication;
            SPSite site = new SPSite(SPContext.Current.Web.Url);
            SPWeb web = site.OpenWeb();
            SPList list = web.Lists["Employee Leaves"];
            SPQuery qry = new SPQuery();
                qry.ViewFields = @"<FieldRef Name='Leave_x0020_Balance' />";
                qry.Query =
                @"   <Where>
                     <And>
         <Eq>
            <FieldRef Name='Employee_x0020_Type' />
            <Value Type='Text'>Probationary</Value>
         </Eq>
         <Eq>
            <FieldRef Name='Year' />
            <Value Type='Text'>2014-2015</Value>
         </Eq>
                    </And>
                   </Where>";
               
            SPListItemCollection listItems = list.GetItems(qry);

            foreach (SPListItem item in listItems)
            {  



            }



        }

    
    }
}
