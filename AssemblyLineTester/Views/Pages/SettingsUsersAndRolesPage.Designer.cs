using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    partial class SettingsUsersAndRolesPage
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tblMainLayout = new TableLayoutPanel();
            cardRoleSelector = new AssemblyLineTester.Views.Controls.RoundedPanel();
            flpRolesList = new FlowLayoutPanel();
            btnRoleAdmin = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRoleOperator = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRoleEngineer = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRoleTechnician = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRoleQuality = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnRoleSupervisor = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblRolesSubheader = new Label();
            lblRolesHeader = new Label();
            pnlRightContent = new Panel();
            tblCardsContainer = new TableLayoutPanel();
            cardHeader = new AssemblyLineTester.Views.Controls.RoundedPanel();
            pnlActionsHeader = new Panel();
            btnSaveChanges = new AssemblyLineTester.Views.Controls.RoundedButton();
            btnResetDefaults = new AssemblyLineTester.Views.Controls.RoundedButton();
            lblSelectedRoleDesc = new Label();
            lblSelectedRoleTitle = new Label();
            cardPageAccess = new AssemblyLineTester.Views.Controls.RoundedPanel();
            tblPageAccessToggles = new TableLayoutPanel();
            lblCardPageAccessTitle = new Label();
            chkPageDash = new CheckBox();
            chkPageTestExec = new CheckBox();
            chkPageHarness = new CheckBox();
            chkPageHistory = new CheckBox();
            chkPageReports = new CheckBox();
            chkPageSysLog = new CheckBox();
            chkPageSettings = new CheckBox();
            chkPageUserMgmt = new CheckBox();
            cardTestOps = new AssemblyLineTester.Views.Controls.RoundedPanel();
            tblTestOpsToggles = new TableLayoutPanel();
            lblCardTestOpsTitle = new Label();
            chkActRun = new CheckBox();
            chkActConfig = new CheckBox();
            chkActCalibrate = new CheckBox();
            cardSystemLogs = new AssemblyLineTester.Views.Controls.RoundedPanel();
            tblSystemLogsToggles = new TableLayoutPanel();
            lblCardSystemLogsTitle = new Label();
            chkActUserEdit = new CheckBox();
            chkActExport = new CheckBox();
            chkActLogClear = new CheckBox();
            chkActDbBackup = new CheckBox();
            tblMainLayout.SuspendLayout();
            cardRoleSelector.SuspendLayout();
            flpRolesList.SuspendLayout();
            pnlRightContent.SuspendLayout();
            tblCardsContainer.SuspendLayout();
            cardHeader.SuspendLayout();
            pnlActionsHeader.SuspendLayout();
            cardPageAccess.SuspendLayout();
            tblPageAccessToggles.SuspendLayout();
            cardTestOps.SuspendLayout();
            tblTestOpsToggles.SuspendLayout();
            cardSystemLogs.SuspendLayout();
            tblSystemLogsToggles.SuspendLayout();
            SuspendLayout();
            // 
            // tblMainLayout
            // 
            tblMainLayout.ColumnCount = 2;
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 430F));
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMainLayout.Controls.Add(cardRoleSelector, 0, 0);
            tblMainLayout.Controls.Add(pnlRightContent, 1, 0);
            tblMainLayout.Dock = DockStyle.Fill;
            tblMainLayout.Location = new Point(0, 0);
            tblMainLayout.Margin = new Padding(6);
            tblMainLayout.Name = "tblMainLayout";
            tblMainLayout.Padding = new Padding(22, 26, 22, 26);
            tblMainLayout.RowCount = 1;
            tblMainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMainLayout.Size = new Size(2229, 1707);
            tblMainLayout.TabIndex = 0;
            // 
            // cardRoleSelector
            // 
            cardRoleSelector.BackColor = Color.White;
            cardRoleSelector.BorderColor = Color.FromArgb(34, 40, 49);
            cardRoleSelector.BorderRadius = 12;
            cardRoleSelector.BorderSize = 2.5F;
            cardRoleSelector.Controls.Add(flpRolesList);
            cardRoleSelector.Controls.Add(lblRolesSubheader);
            cardRoleSelector.Controls.Add(lblRolesHeader);
            cardRoleSelector.Dock = DockStyle.Fill;
            cardRoleSelector.Location = new Point(28, 32);
            cardRoleSelector.Margin = new Padding(6, 6, 22, 6);
            cardRoleSelector.Name = "cardRoleSelector";
            cardRoleSelector.Padding = new Padding(30, 34, 30, 34);
            cardRoleSelector.Size = new Size(402, 1643);
            cardRoleSelector.TabIndex = 0;
            // 
            // flpRolesList
            // 
            flpRolesList.AutoScroll = true;
            flpRolesList.Controls.Add(btnRoleAdmin);
            flpRolesList.Controls.Add(btnRoleOperator);
            flpRolesList.Controls.Add(btnRoleEngineer);
            flpRolesList.Controls.Add(btnRoleTechnician);
            flpRolesList.Controls.Add(btnRoleQuality);
            flpRolesList.Controls.Add(btnRoleSupervisor);
            flpRolesList.Dock = DockStyle.Fill;
            flpRolesList.FlowDirection = FlowDirection.TopDown;
            flpRolesList.Location = new Point(30, 179);
            flpRolesList.Margin = new Padding(6);
            flpRolesList.Name = "flpRolesList";
            flpRolesList.Size = new Size(342, 1430);
            flpRolesList.TabIndex = 2;
            flpRolesList.WrapContents = false;
            // 
            // btnRoleAdmin
            // 
            btnRoleAdmin.BackColor = Color.FromArgb(59, 130, 246);
            btnRoleAdmin.Cursor = Cursors.Hand;
            btnRoleAdmin.FlatAppearance.BorderSize = 0;
            btnRoleAdmin.FlatStyle = FlatStyle.Flat;
            btnRoleAdmin.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnRoleAdmin.ForeColor = Color.White;
            btnRoleAdmin.HoverBackColor = Color.FromArgb(250, 204, 21);
            btnRoleAdmin.HoverForeColor = Color.FromArgb(15, 23, 42);
            btnRoleAdmin.Location = new Point(0, 0);
            btnRoleAdmin.Margin = new Padding(0, 0, 0, 8);
            btnRoleAdmin.Name = "btnRoleAdmin";
            btnRoleAdmin.PressedForeColor = Color.Empty;
            btnRoleAdmin.Size = new Size(334, 52);
            btnRoleAdmin.TabIndex = 0;
            btnRoleAdmin.Tag = 1;
            btnRoleAdmin.Text = "  👑    Administrator";
            btnRoleAdmin.TextAlign = ContentAlignment.MiddleLeft;
            btnRoleAdmin.UseVisualStyleBackColor = false;
            // 
            // btnRoleOperator
            // 
            btnRoleOperator.BackColor = Color.FromArgb(248, 250, 252);
            btnRoleOperator.Cursor = Cursors.Hand;
            btnRoleOperator.FlatAppearance.BorderSize = 0;
            btnRoleOperator.FlatStyle = FlatStyle.Flat;
            btnRoleOperator.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnRoleOperator.ForeColor = Color.FromArgb(30, 41, 59);
            btnRoleOperator.HoverBackColor = Color.FromArgb(250, 204, 21);
            btnRoleOperator.HoverForeColor = Color.FromArgb(15, 23, 42);
            btnRoleOperator.Location = new Point(0, 60);
            btnRoleOperator.Margin = new Padding(0, 0, 0, 8);
            btnRoleOperator.Name = "btnRoleOperator";
            btnRoleOperator.PressedForeColor = Color.Empty;
            btnRoleOperator.Size = new Size(334, 52);
            btnRoleOperator.TabIndex = 1;
            btnRoleOperator.Tag = 2;
            btnRoleOperator.Text = "  🏭    Operator";
            btnRoleOperator.TextAlign = ContentAlignment.MiddleLeft;
            btnRoleOperator.UseVisualStyleBackColor = false;
            // 
            // btnRoleEngineer
            // 
            btnRoleEngineer.BackColor = Color.FromArgb(248, 250, 252);
            btnRoleEngineer.Cursor = Cursors.Hand;
            btnRoleEngineer.FlatAppearance.BorderSize = 0;
            btnRoleEngineer.FlatStyle = FlatStyle.Flat;
            btnRoleEngineer.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnRoleEngineer.ForeColor = Color.FromArgb(30, 41, 59);
            btnRoleEngineer.HoverBackColor = Color.FromArgb(250, 204, 21);
            btnRoleEngineer.HoverForeColor = Color.FromArgb(15, 23, 42);
            btnRoleEngineer.Location = new Point(0, 120);
            btnRoleEngineer.Margin = new Padding(0, 0, 0, 8);
            btnRoleEngineer.Name = "btnRoleEngineer";
            btnRoleEngineer.PressedForeColor = Color.Empty;
            btnRoleEngineer.Size = new Size(334, 52);
            btnRoleEngineer.TabIndex = 2;
            btnRoleEngineer.Tag = 3;
            btnRoleEngineer.Text = "  📐    Engineer";
            btnRoleEngineer.TextAlign = ContentAlignment.MiddleLeft;
            btnRoleEngineer.UseVisualStyleBackColor = false;
            // 
            // btnRoleTechnician
            // 
            btnRoleTechnician.BackColor = Color.FromArgb(248, 250, 252);
            btnRoleTechnician.Cursor = Cursors.Hand;
            btnRoleTechnician.FlatAppearance.BorderSize = 0;
            btnRoleTechnician.FlatStyle = FlatStyle.Flat;
            btnRoleTechnician.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnRoleTechnician.ForeColor = Color.FromArgb(30, 41, 59);
            btnRoleTechnician.HoverBackColor = Color.FromArgb(250, 204, 21);
            btnRoleTechnician.HoverForeColor = Color.FromArgb(15, 23, 42);
            btnRoleTechnician.Location = new Point(0, 180);
            btnRoleTechnician.Margin = new Padding(0, 0, 0, 8);
            btnRoleTechnician.Name = "btnRoleTechnician";
            btnRoleTechnician.PressedForeColor = Color.Empty;
            btnRoleTechnician.Size = new Size(334, 52);
            btnRoleTechnician.TabIndex = 3;
            btnRoleTechnician.Tag = 4;
            btnRoleTechnician.Text = "  🔧    Technician";
            btnRoleTechnician.TextAlign = ContentAlignment.MiddleLeft;
            btnRoleTechnician.UseVisualStyleBackColor = false;
            // 
            // btnRoleQuality
            // 
            btnRoleQuality.BackColor = Color.FromArgb(248, 250, 252);
            btnRoleQuality.Cursor = Cursors.Hand;
            btnRoleQuality.FlatAppearance.BorderSize = 0;
            btnRoleQuality.FlatStyle = FlatStyle.Flat;
            btnRoleQuality.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnRoleQuality.ForeColor = Color.FromArgb(30, 41, 59);
            btnRoleQuality.HoverBackColor = Color.FromArgb(250, 204, 21);
            btnRoleQuality.HoverForeColor = Color.FromArgb(15, 23, 42);
            btnRoleQuality.Location = new Point(0, 240);
            btnRoleQuality.Margin = new Padding(0, 0, 0, 8);
            btnRoleQuality.Name = "btnRoleQuality";
            btnRoleQuality.PressedForeColor = Color.Empty;
            btnRoleQuality.Size = new Size(334, 52);
            btnRoleQuality.TabIndex = 4;
            btnRoleQuality.Tag = 5;
            btnRoleQuality.Text = "  🔎    Quality";
            btnRoleQuality.TextAlign = ContentAlignment.MiddleLeft;
            btnRoleQuality.UseVisualStyleBackColor = false;
            // 
            // btnRoleSupervisor
            // 
            btnRoleSupervisor.BackColor = Color.FromArgb(248, 250, 252);
            btnRoleSupervisor.Cursor = Cursors.Hand;
            btnRoleSupervisor.FlatAppearance.BorderSize = 0;
            btnRoleSupervisor.FlatStyle = FlatStyle.Flat;
            btnRoleSupervisor.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnRoleSupervisor.ForeColor = Color.FromArgb(30, 41, 59);
            btnRoleSupervisor.HoverBackColor = Color.FromArgb(250, 204, 21);
            btnRoleSupervisor.HoverForeColor = Color.FromArgb(15, 23, 42);
            btnRoleSupervisor.Location = new Point(0, 300);
            btnRoleSupervisor.Margin = new Padding(0, 0, 0, 8);
            btnRoleSupervisor.Name = "btnRoleSupervisor";
            btnRoleSupervisor.PressedForeColor = Color.Empty;
            btnRoleSupervisor.Size = new Size(334, 52);
            btnRoleSupervisor.TabIndex = 5;
            btnRoleSupervisor.Tag = 6;
            btnRoleSupervisor.Text = "  📋    Supervisor";
            btnRoleSupervisor.TextAlign = ContentAlignment.MiddleLeft;
            btnRoleSupervisor.UseVisualStyleBackColor = false;
            // 
            // lblRolesSubheader
            // 
            lblRolesSubheader.Dock = DockStyle.Top;
            lblRolesSubheader.Font = new Font("Segoe UI", 9.5F);
            lblRolesSubheader.ForeColor = Color.FromArgb(100, 116, 139);
            lblRolesSubheader.Location = new Point(30, 102);
            lblRolesSubheader.Margin = new Padding(6, 0, 6, 0);
            lblRolesSubheader.Name = "lblRolesSubheader";
            lblRolesSubheader.Size = new Size(342, 77);
            lblRolesSubheader.TabIndex = 1;
            lblRolesSubheader.Text = "Select a role to configure permissions";
            // 
            // lblRolesHeader
            // 
            lblRolesHeader.Dock = DockStyle.Top;
            lblRolesHeader.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblRolesHeader.ForeColor = Color.FromArgb(15, 23, 42);
            lblRolesHeader.Location = new Point(30, 34);
            lblRolesHeader.Margin = new Padding(6, 0, 6, 0);
            lblRolesHeader.Name = "lblRolesHeader";
            lblRolesHeader.Size = new Size(342, 68);
            lblRolesHeader.TabIndex = 0;
            lblRolesHeader.Text = "System Roles";
            // 
            // pnlRightContent
            // 
            pnlRightContent.Controls.Add(tblCardsContainer);
            pnlRightContent.Dock = DockStyle.Fill;
            pnlRightContent.Location = new Point(458, 32);
            pnlRightContent.Margin = new Padding(6);
            pnlRightContent.Name = "pnlRightContent";
            pnlRightContent.Size = new Size(1743, 1643);
            pnlRightContent.TabIndex = 1;
            // 
            // tblCardsContainer
            // 
            tblCardsContainer.AutoScroll = true;
            tblCardsContainer.ColumnCount = 1;
            tblCardsContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCardsContainer.Controls.Add(cardHeader, 0, 0);
            tblCardsContainer.Controls.Add(cardPageAccess, 0, 1);
            tblCardsContainer.Controls.Add(cardTestOps, 0, 2);
            tblCardsContainer.Controls.Add(cardSystemLogs, 0, 3);
            tblCardsContainer.Dock = DockStyle.Fill;
            tblCardsContainer.Location = new Point(0, 0);
            tblCardsContainer.Margin = new Padding(6);
            tblCardsContainer.Name = "tblCardsContainer";
            tblCardsContainer.Padding = new Padding(12, 17, 24, 17);
            tblCardsContainer.RowCount = 5;
            tblCardsContainer.RowStyles.Add(new RowStyle());
            tblCardsContainer.RowStyles.Add(new RowStyle());
            tblCardsContainer.RowStyles.Add(new RowStyle());
            tblCardsContainer.RowStyles.Add(new RowStyle());
            tblCardsContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCardsContainer.Size = new Size(1743, 1643);
            tblCardsContainer.TabIndex = 1;
            // 
            // cardHeader
            // 
            cardHeader.AutoSize = true;
            cardHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardHeader.BackColor = Color.White;
            cardHeader.BorderColor = Color.FromArgb(34, 40, 49);
            cardHeader.BorderRadius = 12;
            cardHeader.BorderSize = 2.5F;
            cardHeader.Controls.Add(pnlActionsHeader);
            cardHeader.Dock = DockStyle.Fill;
            cardHeader.Location = new Point(12, 17);
            cardHeader.Margin = new Padding(0, 0, 0, 24);
            cardHeader.Name = "cardHeader";
            cardHeader.Size = new Size(1707, 192);
            cardHeader.TabIndex = 0;
            // 
            // pnlActionsHeader
            // 
            pnlActionsHeader.BackColor = Color.Transparent;
            pnlActionsHeader.Controls.Add(btnSaveChanges);
            pnlActionsHeader.Controls.Add(btnResetDefaults);
            pnlActionsHeader.Controls.Add(lblSelectedRoleDesc);
            pnlActionsHeader.Controls.Add(lblSelectedRoleTitle);
            pnlActionsHeader.Dock = DockStyle.Top;
            pnlActionsHeader.Location = new Point(0, 0);
            pnlActionsHeader.Margin = new Padding(6);
            pnlActionsHeader.Name = "pnlActionsHeader";
            pnlActionsHeader.Padding = new Padding(37, 34, 37, 34);
            pnlActionsHeader.Size = new Size(1707, 192);
            pnlActionsHeader.TabIndex = 0;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveChanges.BackColor = Color.FromArgb(250, 204, 21);
            btnSaveChanges.BorderRadius = 8;
            btnSaveChanges.Cursor = Cursors.Hand;
            btnSaveChanges.FlatAppearance.BorderSize = 0;
            btnSaveChanges.FlatStyle = FlatStyle.Flat;
            btnSaveChanges.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnSaveChanges.ForeColor = Color.Black;
            btnSaveChanges.HoverForeColor = Color.Empty;
            btnSaveChanges.Location = new Point(1404, 85);
            btnSaveChanges.Margin = new Padding(6);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.PressedForeColor = Color.Empty;
            btnSaveChanges.Size = new Size(260, 67);
            btnSaveChanges.TabIndex = 3;
            btnSaveChanges.Text = "💾 Save Changes";
            btnSaveChanges.UseVisualStyleBackColor = false;
            // 
            // btnResetDefaults
            // 
            btnResetDefaults.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnResetDefaults.BackColor = Color.FromArgb(241, 245, 249);
            btnResetDefaults.BorderRadius = 8;
            btnResetDefaults.Cursor = Cursors.Hand;
            btnResetDefaults.FlatAppearance.BorderSize = 0;
            btnResetDefaults.FlatStyle = FlatStyle.Flat;
            btnResetDefaults.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnResetDefaults.ForeColor = Color.FromArgb(71, 85, 105);
            btnResetDefaults.HoverBackColor = Color.FromArgb(226, 232, 240);
            btnResetDefaults.HoverForeColor = Color.Empty;
            btnResetDefaults.Location = new Point(1132, 85);
            btnResetDefaults.Margin = new Padding(6);
            btnResetDefaults.Name = "btnResetDefaults";
            btnResetDefaults.NormalBackColor = Color.FromArgb(241, 245, 249);
            btnResetDefaults.PressedBackColor = Color.FromArgb(203, 213, 225);
            btnResetDefaults.PressedForeColor = Color.Empty;
            btnResetDefaults.Size = new Size(260, 67);
            btnResetDefaults.TabIndex = 2;
            btnResetDefaults.Text = "🔄 Reset Defaults";
            btnResetDefaults.UseVisualStyleBackColor = false;
            // 
            // lblSelectedRoleDesc
            // 
            lblSelectedRoleDesc.AutoSize = true;
            lblSelectedRoleDesc.Font = new Font("Segoe UI", 10F);
            lblSelectedRoleDesc.ForeColor = Color.FromArgb(100, 116, 139);
            lblSelectedRoleDesc.Location = new Point(41, 107);
            lblSelectedRoleDesc.Margin = new Padding(6, 0, 6, 0);
            lblSelectedRoleDesc.Name = "lblSelectedRoleDesc";
            lblSelectedRoleDesc.Size = new Size(762, 37);
            lblSelectedRoleDesc.TabIndex = 1;
            lblSelectedRoleDesc.Text = "Configure what this role can access and perform across the app";
            // 
            // lblSelectedRoleTitle
            // 
            lblSelectedRoleTitle.AutoSize = true;
            lblSelectedRoleTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblSelectedRoleTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblSelectedRoleTitle.Location = new Point(37, 34);
            lblSelectedRoleTitle.Margin = new Padding(6, 0, 6, 0);
            lblSelectedRoleTitle.Name = "lblSelectedRoleTitle";
            lblSelectedRoleTitle.Size = new Size(647, 59);
            lblSelectedRoleTitle.TabIndex = 0;
            lblSelectedRoleTitle.Text = "Role Permissions: Administrator";
            // 
            // cardPageAccess
            // 
            cardPageAccess.AutoSize = true;
            cardPageAccess.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardPageAccess.BackColor = Color.White;
            cardPageAccess.BorderColor = Color.FromArgb(34, 40, 49);
            cardPageAccess.BorderRadius = 12;
            cardPageAccess.BorderSize = 2.5F;
            cardPageAccess.Controls.Add(tblPageAccessToggles);
            cardPageAccess.Dock = DockStyle.Fill;
            cardPageAccess.Location = new Point(18, 233);
            cardPageAccess.Margin = new Padding(6, 0, 6, 24);
            cardPageAccess.Name = "cardPageAccess";
            cardPageAccess.Size = new Size(1695, 352);
            cardPageAccess.TabIndex = 1;
            // 
            // tblPageAccessToggles
            // 
            tblPageAccessToggles.AutoSize = true;
            tblPageAccessToggles.BackColor = Color.Transparent;
            tblPageAccessToggles.ColumnCount = 2;
            tblPageAccessToggles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPageAccessToggles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPageAccessToggles.Controls.Add(lblCardPageAccessTitle, 0, 0);
            tblPageAccessToggles.Controls.Add(chkPageDash, 0, 1);
            tblPageAccessToggles.Controls.Add(chkPageTestExec, 1, 1);
            tblPageAccessToggles.Controls.Add(chkPageHarness, 0, 2);
            tblPageAccessToggles.Controls.Add(chkPageHistory, 1, 2);
            tblPageAccessToggles.Controls.Add(chkPageReports, 0, 3);
            tblPageAccessToggles.Controls.Add(chkPageSysLog, 1, 3);
            tblPageAccessToggles.Controls.Add(chkPageSettings, 0, 4);
            tblPageAccessToggles.Controls.Add(chkPageUserMgmt, 1, 4);
            tblPageAccessToggles.Dock = DockStyle.Top;
            tblPageAccessToggles.Location = new Point(0, 0);
            tblPageAccessToggles.Margin = new Padding(8);
            tblPageAccessToggles.Name = "tblPageAccessToggles";
            tblPageAccessToggles.Padding = new Padding(24, 16, 24, 24);
            tblPageAccessToggles.RowCount = 5;
            tblPageAccessToggles.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tblPageAccessToggles.RowStyles.Add(new RowStyle());
            tblPageAccessToggles.RowStyles.Add(new RowStyle());
            tblPageAccessToggles.RowStyles.Add(new RowStyle());
            tblPageAccessToggles.RowStyles.Add(new RowStyle());
            tblPageAccessToggles.Size = new Size(1695, 352);
            tblPageAccessToggles.TabIndex = 0;
            // 
            // lblCardPageAccessTitle
            // 
            tblPageAccessToggles.SetColumnSpan(lblCardPageAccessTitle, 2);
            lblCardPageAccessTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblCardPageAccessTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblCardPageAccessTitle.Location = new Point(24, 16);
            lblCardPageAccessTitle.Margin = new Padding(0);
            lblCardPageAccessTitle.Name = "lblCardPageAccessTitle";
            lblCardPageAccessTitle.Size = new Size(1532, 56);
            lblCardPageAccessTitle.TabIndex = 0;
            lblCardPageAccessTitle.Text = "📁 Page & Menu Access (Sidebar Navigation)";
            lblCardPageAccessTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkPageDash
            // 
            chkPageDash.AutoSize = true;
            chkPageDash.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageDash.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageDash.Location = new Point(24, 82);
            chkPageDash.Margin = new Padding(0, 10, 10, 14);
            chkPageDash.Name = "chkPageDash";
            chkPageDash.Size = new Size(327, 40);
            chkPageDash.TabIndex = 1;
            chkPageDash.Tag = "Page.Dashboard";
            chkPageDash.Text = "Access Dashboard Page";
            chkPageDash.UseVisualStyleBackColor = true;
            // 
            // chkPageTestExec
            // 
            chkPageTestExec.AutoSize = true;
            chkPageTestExec.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageTestExec.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageTestExec.Location = new Point(847, 82);
            chkPageTestExec.Margin = new Padding(0, 10, 10, 14);
            chkPageTestExec.Name = "chkPageTestExec";
            chkPageTestExec.Size = new Size(362, 40);
            chkPageTestExec.TabIndex = 2;
            chkPageTestExec.Tag = "Page.TestExecution";
            chkPageTestExec.Text = "Access Test Execution Page";
            chkPageTestExec.UseVisualStyleBackColor = true;
            // 
            // chkPageHarness
            // 
            chkPageHarness.AutoSize = true;
            chkPageHarness.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageHarness.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageHarness.Location = new Point(24, 146);
            chkPageHarness.Margin = new Padding(0, 10, 10, 14);
            chkPageHarness.Name = "chkPageHarness";
            chkPageHarness.Size = new Size(408, 40);
            chkPageHarness.TabIndex = 3;
            chkPageHarness.Tag = "Page.HarnessDatabase";
            chkPageHarness.Text = "Access Harness Database Page";
            chkPageHarness.UseVisualStyleBackColor = true;
            // 
            // chkPageHistory
            // 
            chkPageHistory.AutoSize = true;
            chkPageHistory.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageHistory.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageHistory.Location = new Point(847, 146);
            chkPageHistory.Margin = new Padding(0, 10, 10, 14);
            chkPageHistory.Name = "chkPageHistory";
            chkPageHistory.Size = new Size(334, 40);
            chkPageHistory.TabIndex = 4;
            chkPageHistory.Tag = "Page.TestHistory";
            chkPageHistory.Text = "Access Test History Page";
            chkPageHistory.UseVisualStyleBackColor = true;
            // 
            // chkPageReports
            // 
            chkPageReports.AutoSize = true;
            chkPageReports.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageReports.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageReports.Location = new Point(24, 210);
            chkPageReports.Margin = new Padding(0, 10, 10, 14);
            chkPageReports.Name = "chkPageReports";
            chkPageReports.Size = new Size(407, 40);
            chkPageReports.TabIndex = 5;
            chkPageReports.Tag = "Page.Reports";
            chkPageReports.Text = "Access Reports & Analytics Page";
            chkPageReports.UseVisualStyleBackColor = true;
            // 
            // chkPageSysLog
            // 
            chkPageSysLog.AutoSize = true;
            chkPageSysLog.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageSysLog.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageSysLog.Location = new Point(847, 210);
            chkPageSysLog.Margin = new Padding(0, 10, 10, 14);
            chkPageSysLog.Name = "chkPageSysLog";
            chkPageSysLog.Size = new Size(331, 40);
            chkPageSysLog.TabIndex = 6;
            chkPageSysLog.Tag = "Page.SystemLog";
            chkPageSysLog.Text = "Access System Log Page";
            chkPageSysLog.UseVisualStyleBackColor = true;
            // 
            // chkPageSettings
            // 
            chkPageSettings.AutoSize = true;
            chkPageSettings.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageSettings.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageSettings.Location = new Point(24, 274);
            chkPageSettings.Margin = new Padding(0, 10, 10, 14);
            chkPageSettings.Name = "chkPageSettings";
            chkPageSettings.Size = new Size(291, 40);
            chkPageSettings.TabIndex = 7;
            chkPageSettings.Tag = "Page.Settings";
            chkPageSettings.Text = "Access Settings Page";
            chkPageSettings.UseVisualStyleBackColor = true;
            // 
            // chkPageUserMgmt
            // 
            chkPageUserMgmt.AutoSize = true;
            chkPageUserMgmt.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkPageUserMgmt.ForeColor = Color.FromArgb(15, 23, 42);
            chkPageUserMgmt.Location = new Point(847, 274);
            chkPageUserMgmt.Margin = new Padding(0, 10, 10, 14);
            chkPageUserMgmt.Name = "chkPageUserMgmt";
            chkPageUserMgmt.Size = new Size(414, 40);
            chkPageUserMgmt.TabIndex = 8;
            chkPageUserMgmt.Tag = "Page.UserManagement";
            chkPageUserMgmt.Text = "Access User Management Page";
            chkPageUserMgmt.UseVisualStyleBackColor = true;
            // 
            // cardTestOps
            // 
            cardTestOps.AutoSize = true;
            cardTestOps.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardTestOps.BackColor = Color.White;
            cardTestOps.BorderColor = Color.FromArgb(34, 40, 49);
            cardTestOps.BorderRadius = 12;
            cardTestOps.BorderSize = 2.5F;
            cardTestOps.Controls.Add(tblTestOpsToggles);
            cardTestOps.Dock = DockStyle.Fill;
            cardTestOps.Location = new Point(18, 609);
            cardTestOps.Margin = new Padding(6, 0, 6, 24);
            cardTestOps.Name = "cardTestOps";
            cardTestOps.Size = new Size(1695, 224);
            cardTestOps.TabIndex = 2;
            // 
            // tblTestOpsToggles
            // 
            tblTestOpsToggles.AutoSize = true;
            tblTestOpsToggles.BackColor = Color.Transparent;
            tblTestOpsToggles.ColumnCount = 2;
            tblTestOpsToggles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblTestOpsToggles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblTestOpsToggles.Controls.Add(lblCardTestOpsTitle, 0, 0);
            tblTestOpsToggles.Controls.Add(chkActRun, 0, 1);
            tblTestOpsToggles.Controls.Add(chkActConfig, 1, 1);
            tblTestOpsToggles.Controls.Add(chkActCalibrate, 0, 2);
            tblTestOpsToggles.Dock = DockStyle.Top;
            tblTestOpsToggles.Location = new Point(0, 0);
            tblTestOpsToggles.Margin = new Padding(8);
            tblTestOpsToggles.Name = "tblTestOpsToggles";
            tblTestOpsToggles.Padding = new Padding(24, 16, 24, 24);
            tblTestOpsToggles.RowCount = 3;
            tblTestOpsToggles.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tblTestOpsToggles.RowStyles.Add(new RowStyle());
            tblTestOpsToggles.RowStyles.Add(new RowStyle());
            tblTestOpsToggles.Size = new Size(1695, 224);
            tblTestOpsToggles.TabIndex = 0;
            // 
            // lblCardTestOpsTitle
            // 
            tblTestOpsToggles.SetColumnSpan(lblCardTestOpsTitle, 2);
            lblCardTestOpsTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblCardTestOpsTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblCardTestOpsTitle.Location = new Point(24, 16);
            lblCardTestOpsTitle.Margin = new Padding(0);
            lblCardTestOpsTitle.Name = "lblCardTestOpsTitle";
            lblCardTestOpsTitle.Size = new Size(1532, 56);
            lblCardTestOpsTitle.TabIndex = 0;
            lblCardTestOpsTitle.Text = "⚡ Test Operations & Hardware Capabilities";
            lblCardTestOpsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkActRun
            // 
            chkActRun.AutoSize = true;
            chkActRun.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkActRun.ForeColor = Color.FromArgb(15, 23, 42);
            chkActRun.Location = new Point(24, 82);
            chkActRun.Margin = new Padding(0, 10, 10, 14);
            chkActRun.Name = "chkActRun";
            chkActRun.Size = new Size(437, 40);
            chkActRun.TabIndex = 1;
            chkActRun.Tag = "Action.TestExecution.Run";
            chkActRun.Text = "Start and Run Testing Operations";
            chkActRun.UseVisualStyleBackColor = true;
            // 
            // chkActConfig
            // 
            chkActConfig.AutoSize = true;
            chkActConfig.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkActConfig.ForeColor = Color.FromArgb(15, 23, 42);
            chkActConfig.Location = new Point(847, 82);
            chkActConfig.Margin = new Padding(0, 10, 10, 14);
            chkActConfig.Name = "chkActConfig";
            chkActConfig.Size = new Size(574, 40);
            chkActConfig.TabIndex = 2;
            chkActConfig.Tag = "Action.TestConfig.Edit";
            chkActConfig.Text = "Add and Edit Test Parameters & Harness Specs";
            chkActConfig.UseVisualStyleBackColor = true;
            // 
            // chkActCalibrate
            // 
            chkActCalibrate.AutoSize = true;
            chkActCalibrate.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkActCalibrate.ForeColor = Color.FromArgb(15, 23, 42);
            chkActCalibrate.Location = new Point(24, 146);
            chkActCalibrate.Margin = new Padding(0, 10, 10, 14);
            chkActCalibrate.Name = "chkActCalibrate";
            chkActCalibrate.Size = new Size(602, 40);
            chkActCalibrate.TabIndex = 3;
            chkActCalibrate.Tag = "Action.Instruments.Calibrate";
            chkActCalibrate.Text = "Calibrate and Configure Hardware Instruments";
            chkActCalibrate.UseVisualStyleBackColor = true;
            // 
            // cardSystemLogs
            // 
            cardSystemLogs.AutoSize = true;
            cardSystemLogs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardSystemLogs.BackColor = Color.White;
            cardSystemLogs.BorderColor = Color.FromArgb(34, 40, 49);
            cardSystemLogs.BorderRadius = 12;
            cardSystemLogs.BorderSize = 2.5F;
            cardSystemLogs.Controls.Add(tblSystemLogsToggles);
            cardSystemLogs.Dock = DockStyle.Fill;
            cardSystemLogs.Location = new Point(18, 857);
            cardSystemLogs.Margin = new Padding(6, 0, 6, 24);
            cardSystemLogs.Name = "cardSystemLogs";
            cardSystemLogs.Size = new Size(1695, 224);
            cardSystemLogs.TabIndex = 3;
            // 
            // tblSystemLogsToggles
            // 
            tblSystemLogsToggles.AutoSize = true;
            tblSystemLogsToggles.BackColor = Color.Transparent;
            tblSystemLogsToggles.ColumnCount = 2;
            tblSystemLogsToggles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblSystemLogsToggles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblSystemLogsToggles.Controls.Add(lblCardSystemLogsTitle, 0, 0);
            tblSystemLogsToggles.Controls.Add(chkActUserEdit, 0, 1);
            tblSystemLogsToggles.Controls.Add(chkActExport, 1, 1);
            tblSystemLogsToggles.Controls.Add(chkActLogClear, 0, 2);
            tblSystemLogsToggles.Controls.Add(chkActDbBackup, 1, 2);
            tblSystemLogsToggles.Dock = DockStyle.Top;
            tblSystemLogsToggles.Location = new Point(0, 0);
            tblSystemLogsToggles.Margin = new Padding(8);
            tblSystemLogsToggles.Name = "tblSystemLogsToggles";
            tblSystemLogsToggles.Padding = new Padding(24, 16, 24, 24);
            tblSystemLogsToggles.RowCount = 3;
            tblSystemLogsToggles.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tblSystemLogsToggles.RowStyles.Add(new RowStyle());
            tblSystemLogsToggles.RowStyles.Add(new RowStyle());
            tblSystemLogsToggles.Size = new Size(1695, 224);
            tblSystemLogsToggles.TabIndex = 0;
            // 
            // lblCardSystemLogsTitle
            // 
            tblSystemLogsToggles.SetColumnSpan(lblCardSystemLogsTitle, 2);
            lblCardSystemLogsTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblCardSystemLogsTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblCardSystemLogsTitle.Location = new Point(24, 16);
            lblCardSystemLogsTitle.Margin = new Padding(0);
            lblCardSystemLogsTitle.Name = "lblCardSystemLogsTitle";
            lblCardSystemLogsTitle.Size = new Size(1532, 56);
            lblCardSystemLogsTitle.TabIndex = 0;
            lblCardSystemLogsTitle.Text = "🛠️ System Administration & Maintenance";
            lblCardSystemLogsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkActUserEdit
            // 
            chkActUserEdit.AutoSize = true;
            chkActUserEdit.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkActUserEdit.ForeColor = Color.FromArgb(15, 23, 42);
            chkActUserEdit.Location = new Point(24, 82);
            chkActUserEdit.Margin = new Padding(0, 10, 10, 14);
            chkActUserEdit.Name = "chkActUserEdit";
            chkActUserEdit.Size = new Size(540, 40);
            chkActUserEdit.TabIndex = 1;
            chkActUserEdit.Tag = "Action.UserManagement.Edit";
            chkActUserEdit.Text = "Manage User Accounts & Role Assignments";
            chkActUserEdit.UseVisualStyleBackColor = true;
            // 
            // chkActExport
            // 
            chkActExport.AutoSize = true;
            chkActExport.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkActExport.ForeColor = Color.FromArgb(15, 23, 42);
            chkActExport.Location = new Point(847, 82);
            chkActExport.Margin = new Padding(0, 10, 10, 14);
            chkActExport.Name = "chkActExport";
            chkActExport.Size = new Size(550, 40);
            chkActExport.TabIndex = 2;
            chkActExport.Tag = "Action.Reports.Export";
            chkActExport.Text = "Export Reports, Analytics & Test Data to CSV";
            chkActExport.UseVisualStyleBackColor = true;
            // 
            // chkActLogClear
            // 
            chkActLogClear.AutoSize = true;
            chkActLogClear.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkActLogClear.ForeColor = Color.FromArgb(15, 23, 42);
            chkActLogClear.Location = new Point(24, 146);
            chkActLogClear.Margin = new Padding(0, 10, 10, 14);
            chkActLogClear.Name = "chkActLogClear";
            chkActLogClear.Size = new Size(479, 40);
            chkActLogClear.TabIndex = 3;
            chkActLogClear.Tag = "Action.SystemLog.Clear";
            chkActLogClear.Text = "Clear and Archive System Event Logs";
            chkActLogClear.UseVisualStyleBackColor = true;
            // 
            // chkActDbBackup
            // 
            chkActDbBackup.AutoSize = true;
            chkActDbBackup.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            chkActDbBackup.ForeColor = Color.FromArgb(15, 23, 42);
            chkActDbBackup.Location = new Point(847, 146);
            chkActDbBackup.Margin = new Padding(0, 10, 10, 14);
            chkActDbBackup.Name = "chkActDbBackup";
            chkActDbBackup.Size = new Size(553, 40);
            chkActDbBackup.TabIndex = 4;
            chkActDbBackup.Tag = "Action.Database.Backup";
            chkActDbBackup.Text = "Perform Database Backup & Restore Actions";
            chkActDbBackup.UseVisualStyleBackColor = true;
            // 
            // SettingsUsersAndRolesPage
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(tblMainLayout);
            Margin = new Padding(6);
            Name = "SettingsUsersAndRolesPage";
            Size = new Size(2229, 1707);
            tblMainLayout.ResumeLayout(false);
            cardRoleSelector.ResumeLayout(false);
            flpRolesList.ResumeLayout(false);
            pnlRightContent.ResumeLayout(false);
            tblCardsContainer.ResumeLayout(false);
            tblCardsContainer.PerformLayout();
            cardHeader.ResumeLayout(false);
            pnlActionsHeader.ResumeLayout(false);
            pnlActionsHeader.PerformLayout();
            cardPageAccess.ResumeLayout(false);
            cardPageAccess.PerformLayout();
            tblPageAccessToggles.ResumeLayout(false);
            tblPageAccessToggles.PerformLayout();
            cardTestOps.ResumeLayout(false);
            cardTestOps.PerformLayout();
            tblTestOpsToggles.ResumeLayout(false);
            tblTestOpsToggles.PerformLayout();
            cardSystemLogs.ResumeLayout(false);
            cardSystemLogs.PerformLayout();
            tblSystemLogsToggles.ResumeLayout(false);
            tblSystemLogsToggles.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tblMainLayout;
        private AssemblyLineTester.Views.Controls.RoundedPanel cardRoleSelector;
        private FlowLayoutPanel flpRolesList;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRoleAdmin;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRoleOperator;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRoleEngineer;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRoleTechnician;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRoleQuality;
        private AssemblyLineTester.Views.Controls.RoundedButton btnRoleSupervisor;
        private Label lblRolesHeader;
        private Label lblRolesSubheader;
        private Panel pnlRightContent;
        private Panel pnlActionsHeader;
        private Label lblSelectedRoleTitle;
        private Label lblSelectedRoleDesc;
        private AssemblyLineTester.Views.Controls.RoundedButton btnSaveChanges;
        private AssemblyLineTester.Views.Controls.RoundedButton btnResetDefaults;
        private TableLayoutPanel tblCardsContainer;
        private AssemblyLineTester.Views.Controls.RoundedPanel cardHeader;
        private AssemblyLineTester.Views.Controls.RoundedPanel cardPageAccess;
        private AssemblyLineTester.Views.Controls.RoundedPanel cardTestOps;
        private AssemblyLineTester.Views.Controls.RoundedPanel cardSystemLogs;
        private Label lblCardPageAccessTitle;
        private TableLayoutPanel tblPageAccessToggles;
        private Label lblCardTestOpsTitle;
        private TableLayoutPanel tblTestOpsToggles;
        private Label lblCardSystemLogsTitle;
        private TableLayoutPanel tblSystemLogsToggles;
        private CheckBox chkPageDash;
        private CheckBox chkPageTestExec;
        private CheckBox chkPageHarness;
        private CheckBox chkPageHistory;
        private CheckBox chkPageReports;
        private CheckBox chkPageSysLog;
        private CheckBox chkPageSettings;
        private CheckBox chkPageUserMgmt;
        private CheckBox chkActRun;
        private CheckBox chkActConfig;
        private CheckBox chkActCalibrate;
        private CheckBox chkActUserEdit;
        private CheckBox chkActExport;
        private CheckBox chkActLogClear;
        private CheckBox chkActDbBackup;
    }
}

