﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaveApplication.CancelEmployeeLeaves {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    
    
    public partial class CancelEmployeeLeaves {
        
        protected global::System.Web.UI.WebControls.Label lblEmpID;
        
        protected global::System.Web.UI.WebControls.Label lblDesgination;
        
        protected global::System.Web.UI.WebControls.Label lblDepartment;
        
        protected global::System.Web.UI.WebControls.DropDownList ddlTypeofLeave;
        
        protected global::System.Web.UI.WebControls.TextBox txtPurpose;
        
        protected global::Microsoft.SharePoint.WebControls.DateTimeControl dateTimeStartDate;
        
        protected global::Microsoft.SharePoint.WebControls.DateTimeControl dateTimeEndDate;
        
        protected global::System.Web.UI.HtmlControls.HtmlTableRow Selecteddates;
        
        protected global::System.Web.UI.WebControls.TextBox txtOptionalLeaves;
        
        protected global::System.Web.UI.HtmlControls.HtmlTableRow optinalDates;
        
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl txtDuration;
        
        protected global::System.Web.UI.WebControls.TextBox ddlReportingTo;
        
        protected global::System.Web.UI.WebControls.Button btnSubmit;
        
        protected global::System.Web.UI.WebControls.Button btnReset;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnCurrentUsername;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnEmployeeType;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnHolidayList;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnStrtDate;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnEndDate;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnLeaveId;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnLeaveDuration;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnCurrentYear;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnReportingTo;
        
        protected global::System.Web.UI.WebControls.Label lblError;
        
        public static implicit operator global::System.Web.UI.TemplateControl(CancelEmployeeLeaves target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllblEmpID() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lblEmpID = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblEmpID";
            @__ctrl.Text = "";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllblDesgination() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lblDesgination = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblDesgination";
            @__ctrl.Text = "";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllblDepartment() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lblDepartment = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblDepartment";
            @__ctrl.Text = "";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.DropDownList @__BuildControlddlTypeofLeave() {
            global::System.Web.UI.WebControls.DropDownList @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.DropDownList();
            this.ddlTypeofLeave = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "ddlTypeofLeave";
            @__ctrl.AutoPostBack = false;
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(150D, global::System.Web.UI.WebControls.UnitType.Pixel);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtPurpose() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtPurpose = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtPurpose";
            @__ctrl.TextMode = global::System.Web.UI.WebControls.TextBoxMode.MultiLine;
            @__ctrl.ReadOnly = false;
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(500D, global::System.Web.UI.WebControls.UnitType.Pixel);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control3() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "label");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                <label>\r\n                    From Date (MM/DD/YYYY) </label>&nb" +
                        "sp;"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::Microsoft.SharePoint.WebControls.DateTimeControl @__BuildControldateTimeStartDate() {
            global::Microsoft.SharePoint.WebControls.DateTimeControl @__ctrl;
            @__ctrl = new global::Microsoft.SharePoint.WebControls.DateTimeControl();
            this.dateTimeStartDate = @__ctrl;
            @__ctrl.ID = "dateTimeStartDate";
            @__ctrl.DateOnly = true;
            @__ctrl.LocaleId = 1033;
            @__ctrl.OnValueChangeClientScript = "javascript:DateCompare()";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control4() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                "));
            global::Microsoft.SharePoint.WebControls.DateTimeControl @__ctrl1;
            @__ctrl1 = this.@__BuildControldateTimeStartDate();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control5() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "label");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                <label>\r\n                    To Date (MM/DD/YYYY) </label>\r\n   " +
                        "         "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::Microsoft.SharePoint.WebControls.DateTimeControl @__BuildControldateTimeEndDate() {
            global::Microsoft.SharePoint.WebControls.DateTimeControl @__ctrl;
            @__ctrl = new global::Microsoft.SharePoint.WebControls.DateTimeControl();
            this.dateTimeEndDate = @__ctrl;
            @__ctrl.ID = "dateTimeEndDate";
            @__ctrl.DateOnly = true;
            @__ctrl.LocaleId = 1033;
            @__ctrl.OnValueChangeClientScript = "javascript:DateCompare()";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control6() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                "));
            global::Microsoft.SharePoint.WebControls.DateTimeControl @__ctrl1;
            @__ctrl1 = this.@__BuildControldateTimeEndDate();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control2(System.Web.UI.HtmlControls.HtmlTableCellCollection @__ctrl) {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control3();
            @__ctrl.Add(@__ctrl1);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control4();
            @__ctrl.Add(@__ctrl2);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl3;
            @__ctrl3 = this.@__BuildControl__control5();
            @__ctrl.Add(@__ctrl3);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl4;
            @__ctrl4 = this.@__BuildControl__control6();
            @__ctrl.Add(@__ctrl4);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableRow @__BuildControlSelecteddates() {
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableRow();
            this.Selecteddates = @__ctrl;
            @__ctrl.ID = "Selecteddates";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "data double");
            this.@__BuildControl__control2(@__ctrl.Cells);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control8() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "label");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                <label>\r\n                    Optional Leave</label>\r\n          " +
                        "  "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtOptionalLeaves() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtOptionalLeaves = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtOptionalLeaves";
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(100D, global::System.Web.UI.WebControls.UnitType.Pixel);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control9() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            @__ctrl.ColSpan = 4;
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                "));
            global::System.Web.UI.WebControls.TextBox @__ctrl1;
            @__ctrl1 = this.@__BuildControltxtOptionalLeaves();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control7(System.Web.UI.HtmlControls.HtmlTableCellCollection @__ctrl) {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control8();
            @__ctrl.Add(@__ctrl1);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control9();
            @__ctrl.Add(@__ctrl2);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableRow @__BuildControloptinalDates() {
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableRow();
            this.optinalDates = @__ctrl;
            @__ctrl.ID = "optinalDates";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "data double");
            this.@__BuildControl__control7(@__ctrl.Cells);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlGenericControl @__BuildControltxtDuration() {
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlGenericControl("label");
            this.txtDuration = @__ctrl;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("type", "text");
            @__ctrl.ID = "txtDuration";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControlddlReportingTo() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.ddlReportingTo = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "ddlReportingTo";
            @__ctrl.ReadOnly = true;
            @__ctrl.CssClass = "ReadOnly";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnSubmit() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnSubmit = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnSubmit";
            @__ctrl.Text = "Ok";
            @__ctrl.Click -= new System.EventHandler(this.BtnSubmitClick);
            @__ctrl.Click += new System.EventHandler(this.BtnSubmitClick);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnReset() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnReset = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnReset";
            @__ctrl.Text = "Close";
            @__ctrl.Click -= new System.EventHandler(this.BtnResetClick);
            @__ctrl.Click += new System.EventHandler(this.BtnResetClick);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnCurrentUsername() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnCurrentUsername = @__ctrl;
            @__ctrl.ID = "hdnCurrentUsername";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnEmployeeType() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnEmployeeType = @__ctrl;
            @__ctrl.ID = "hdnEmployeeType";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnHolidayList() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnHolidayList = @__ctrl;
            @__ctrl.ID = "hdnHolidayList";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnStrtDate() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnStrtDate = @__ctrl;
            @__ctrl.ID = "hdnStrtDate";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnEndDate() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnEndDate = @__ctrl;
            @__ctrl.ID = "hdnEndDate";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnLeaveId() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnLeaveId = @__ctrl;
            @__ctrl.ID = "hdnLeaveId";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnLeaveDuration() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnLeaveDuration = @__ctrl;
            @__ctrl.ID = "hdnLeaveDuration";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnCurrentYear() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnCurrentYear = @__ctrl;
            @__ctrl.ID = "hdnCurrentYear";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnReportingTo() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnReportingTo = @__ctrl;
            @__ctrl.ID = "hdnReportingTo";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllblError() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lblError = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblError";
            @__ctrl.ForeColor = global::System.Drawing.Color.Red;
            @__ctrl.Font.Bold = true;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::LeaveApplication.CancelEmployeeLeaves.CancelEmployeeLeaves @__ctrl) {
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControllblEmpID();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
            global::System.Web.UI.WebControls.Label @__ctrl2;
            @__ctrl2 = this.@__BuildControllblDesgination();
            @__parser.AddParsedSubObject(@__ctrl2);
            global::System.Web.UI.WebControls.Label @__ctrl3;
            @__ctrl3 = this.@__BuildControllblDepartment();
            @__parser.AddParsedSubObject(@__ctrl3);
            global::System.Web.UI.WebControls.DropDownList @__ctrl4;
            @__ctrl4 = this.@__BuildControlddlTypeofLeave();
            @__parser.AddParsedSubObject(@__ctrl4);
            global::System.Web.UI.WebControls.TextBox @__ctrl5;
            @__ctrl5 = this.@__BuildControltxtPurpose();
            @__parser.AddParsedSubObject(@__ctrl5);
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl6;
            @__ctrl6 = this.@__BuildControlSelecteddates();
            @__parser.AddParsedSubObject(@__ctrl6);
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl7;
            @__ctrl7 = this.@__BuildControloptinalDates();
            @__parser.AddParsedSubObject(@__ctrl7);
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl8;
            @__ctrl8 = this.@__BuildControltxtDuration();
            @__parser.AddParsedSubObject(@__ctrl8);
            global::System.Web.UI.WebControls.TextBox @__ctrl9;
            @__ctrl9 = this.@__BuildControlddlReportingTo();
            @__parser.AddParsedSubObject(@__ctrl9);
            global::System.Web.UI.WebControls.Button @__ctrl10;
            @__ctrl10 = this.@__BuildControlbtnSubmit();
            @__parser.AddParsedSubObject(@__ctrl10);
            global::System.Web.UI.WebControls.Button @__ctrl11;
            @__ctrl11 = this.@__BuildControlbtnReset();
            @__parser.AddParsedSubObject(@__ctrl11);
            global::System.Web.UI.WebControls.HiddenField @__ctrl12;
            @__ctrl12 = this.@__BuildControlhdnCurrentUsername();
            @__parser.AddParsedSubObject(@__ctrl12);
            global::System.Web.UI.WebControls.HiddenField @__ctrl13;
            @__ctrl13 = this.@__BuildControlhdnEmployeeType();
            @__parser.AddParsedSubObject(@__ctrl13);
            global::System.Web.UI.WebControls.HiddenField @__ctrl14;
            @__ctrl14 = this.@__BuildControlhdnHolidayList();
            @__parser.AddParsedSubObject(@__ctrl14);
            global::System.Web.UI.WebControls.HiddenField @__ctrl15;
            @__ctrl15 = this.@__BuildControlhdnStrtDate();
            @__parser.AddParsedSubObject(@__ctrl15);
            global::System.Web.UI.WebControls.HiddenField @__ctrl16;
            @__ctrl16 = this.@__BuildControlhdnEndDate();
            @__parser.AddParsedSubObject(@__ctrl16);
            global::System.Web.UI.WebControls.HiddenField @__ctrl17;
            @__ctrl17 = this.@__BuildControlhdnLeaveId();
            @__parser.AddParsedSubObject(@__ctrl17);
            global::System.Web.UI.WebControls.HiddenField @__ctrl18;
            @__ctrl18 = this.@__BuildControlhdnLeaveDuration();
            @__parser.AddParsedSubObject(@__ctrl18);
            global::System.Web.UI.WebControls.HiddenField @__ctrl19;
            @__ctrl19 = this.@__BuildControlhdnCurrentYear();
            @__parser.AddParsedSubObject(@__ctrl19);
            global::System.Web.UI.WebControls.HiddenField @__ctrl20;
            @__ctrl20 = this.@__BuildControlhdnReportingTo();
            @__parser.AddParsedSubObject(@__ctrl20);
            global::System.Web.UI.WebControls.Label @__ctrl21;
            @__ctrl21 = this.@__BuildControllblError();
            @__parser.AddParsedSubObject(@__ctrl21);
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write(@"

<link href=""../_layouts/15/LeaveApplication/StyleSheet.css"" rel=""stylesheet"" />
<script src=""../_layouts/15/LeaveApplication/jquery.min.js""></script>

<script type=""text/javascript"" lang=""javascript "">

    function DateCompare() {
        var jsondata = document.getElementById(""");
                                        @__w.Write( hdnHolidayList.ClientID );

            @__w.Write("\").value;\r\n        var leaveType = document.getElementById(\"");
                                         @__w.Write( ddlTypeofLeave.ClientID );

            @__w.Write("\");\r\n        var fromdate = document.getElementById(\"");
                                        @__w.Write( dateTimeStartDate.Controls[0].ClientID );

            @__w.Write("\").value;\r\n        var endDate = document.getElementById(\"");
                                       @__w.Write( dateTimeEndDate.Controls[0].ClientID );

            @__w.Write(@""").value;
        var optionalDates;

        var obj = jQuery.parseJSON(jsondata);

        var fValue = new Date(fromdate);
        var eValue = new Date(endDate);

        var selectedLeave = leaveType.options[leaveType.selectedIndex].value;
        var tempfromdate = fValue;

        while (IsHoliday(tempfromdate, obj)) {
            //  alert(tempfromdate);

            tempfromdate.setDate(tempfromdate.getDate() + 1);
        }

        var tempenddate = eValue;

        while (IsHoliday(tempenddate, obj)) {
            tempenddate.setDate(tempenddate.getDate() - 1);
        }

        var leaveDays;
        leaveDays = DayDifference(tempfromdate, tempenddate);
        // alert(tempfromdate + ""----"" + tempenddate);
        if (tempfromdate.toString() != tempenddate.toString()) {
            if (leaveDays > 0)
                leaveDays = leaveDays + 1;
            else
                leaveDays = 0;
        } else {
            leaveDays = 1;
        }

        // alert(selectedLeave);
        var i;
        if (selectedLeave == ""Comp off"") {
            var countWorkingDays = 0;
            //  var tfdate = tempfromdate;
            for (i = tempfromdate; tempfromdate.getTime() != tempenddate.getTime() ; i.setDate(i.getDate() + 1)) {
                if (!IsHoliday(tempfromdate, obj))
                    countWorkingDays++;
            }
            document.getElementById(""");
                             @__w.Write( txtDuration.ClientID );

            @__w.Write(@""").innerText = countWorkingDays + 1;
            //alert(countWorkingDays);
        }
        else if (selectedLeave == ""Optional"") {
            var leavesselected = 0;
            for (i = 0; i < optionalDates.options.length; i++) {
                var isSelected = optionalDates.options[i].selected;
                isSelected = (isSelected) ? ""selected"" : ""not selected"";

                if (isSelected == ""selected"")
                    leavesselected++;
            }

            document.getElementById(""");
                             @__w.Write( txtDuration.ClientID );

            @__w.Write("\").innerText = leavesselected;\r\n            // alert(selectedLeave);\r\n        } e" +
                    "lse {\r\n            // alert(selectedLeave);\r\n\r\n            document.getElementBy" +
                    "Id(\"");
                             @__w.Write( txtDuration.ClientID );

            @__w.Write(@""").innerText = leaveDays;
        }
}

function DayDifference(tempfromdate, tempenddate) {
    var oneDay = 1000 * 60 * 60 * 24;

    var dayDiff = (Math.ceil((tempenddate.getTime() - tempfromdate.getTime()) / (oneDay)));

    return dayDiff;
}

function IsHoliday(fValue, jsondata) {
    var fdate = new Date(fValue);
    var tdate = fdate.getMonth() + 1 + ""/"" + fdate.getDate() + ""/"" + fdate.getFullYear();

    if (jsondata.toString().indexOf(tdate) != -1) {
        return true;
    }

    return IsSatOrSun(tdate);
}
function IsSatOrSun(fValue) {
    var tdate = new Date(fValue);

    if (tdate.getDay() == 0 || tdate.getDay() == 6) {
        return true;
    } else {
        return false;
    }
}
</script>
<div class=""Container"">
    <table>
        <tr class=""header"">
            <th colspan=""4"">
                <h3>
                    Cancellation Form</h3>
            </th>
        </tr>
        <tr class=""data double"">
            <td class=""label"">
                <label>
                    Employee Id</label>
            </td>
            <td>
                ");
            parameterContainer.Controls[0].RenderControl(@__w);
            @__w.Write("\r\n            </td>\r\n            <td class=\"label\">\r\n                <label>\r\n   " +
                    "                 Designation</label>\r\n            </td>\r\n            <td>\r\n     " +
                    "           ");
            parameterContainer.Controls[1].RenderControl(@__w);
            @__w.Write("\r\n            </td>\r\n        </tr>\r\n        <tr class=\"data double\">\r\n           " +
                    " <td class=\"label\">\r\n                <label>\r\n                    Department</la" +
                    "bel>\r\n            </td>\r\n            <td>\r\n                ");
            parameterContainer.Controls[2].RenderControl(@__w);
            @__w.Write("\r\n            </td>\r\n            <td class=\"label\">\r\n                <label>\r\n   " +
                    "                 Type of Leave</label>\r\n            </td>\r\n            <td>\r\n   " +
                    "             ");
            parameterContainer.Controls[3].RenderControl(@__w);
            @__w.Write("\r\n            </td>\r\n        </tr>\r\n        <tr class=\"data double\">\r\n           " +
                    " <td class=\"label\">\r\n                <label>\r\n                    Purpose</label" +
                    ">\r\n            </td>\r\n            <td colspan=\"4\">\r\n                ");
            parameterContainer.Controls[4].RenderControl(@__w);
            @__w.Write("\r\n            </td>\r\n        </tr>\r\n        ");
            parameterContainer.Controls[5].RenderControl(@__w);
            @__w.Write("\r\n        ");
            parameterContainer.Controls[6].RenderControl(@__w);
            @__w.Write("\r\n        <tr class=\"data double\">\r\n            <td class=\"label\">\r\n             " +
                    "   <label>\r\n                    Duration</label>\r\n            </td>\r\n           " +
                    " <td>\r\n                ");
            parameterContainer.Controls[7].RenderControl(@__w);
            @__w.Write("\r\n                \r\n            </td>\r\n            <td class=\"label\">\r\n          " +
                    "      <label>\r\n                    Reporting To</label>\r\n            </td>\r\n    " +
                    "        <td>\r\n                ");
            parameterContainer.Controls[8].RenderControl(@__w);
            @__w.Write("\r\n                \r\n            </td>\r\n        </tr>\r\n        <tr class=\"data dou" +
                    "ble controls\">\r\n            <td colspan=\"4\" class=\"noborders\">\r\n                " +
                    "");
            parameterContainer.Controls[9].RenderControl(@__w);
            @__w.Write("&nbsp;&nbsp;\r\n                ");
            parameterContainer.Controls[10].RenderControl(@__w);
            @__w.Write("\r\n            </td>\r\n        </tr>\r\n        <tr class=\"data double\">\r\n           " +
                    " <td class=\"noborders\" colspan=\"4\">\r\n                \r\n                ");
            parameterContainer.Controls[11].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[12].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[13].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[14].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[15].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[16].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[17].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[18].RenderControl(@__w);
            @__w.Write("\r\n                ");
            parameterContainer.Controls[19].RenderControl(@__w);
            @__w.Write("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n     ");
            parameterContainer.Controls[20].RenderControl(@__w);
            @__w.Write("\r\n</div>\r\n");
        }
        
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}