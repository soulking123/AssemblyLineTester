using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AssemblyLineTester.Views.Pages
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblTop = new System.Windows.Forms.TableLayoutPanel();
            this.cardTester = new System.Windows.Forms.Panel();
            this.lblTesterSub = new System.Windows.Forms.Label();
            this.lblTesterStatus = new System.Windows.Forms.Label();
            this.lblTesterTitle = new System.Windows.Forms.Label();
            this.cardMachine = new System.Windows.Forms.Panel();
            this.lblMachineSub = new System.Windows.Forms.Label();
            this.lblMachineStatus = new System.Windows.Forms.Label();
            this.lblMachineTitle = new System.Windows.Forms.Label();
            this.cardDatabase = new System.Windows.Forms.Panel();
            this.lblDatabaseSub = new System.Windows.Forms.Label();
            this.lblDatabaseStatus = new System.Windows.Forms.Label();
            this.lblDatabaseTitle = new System.Windows.Forms.Label();
            this.cardActiveProg = new System.Windows.Forms.Panel();
            this.btnChangeProg = new System.Windows.Forms.Button();
            this.picHarness = new System.Windows.Forms.PictureBox();
            this.lblProgDetails = new System.Windows.Forms.Label();
            this.lblActiveProgTitle = new System.Windows.Forms.Label();
            this.tblMiddle = new System.Windows.Forms.TableLayoutPanel();
            this.cardSummary = new System.Windows.Forms.Panel();
            this.tblCharts = new System.Windows.Forms.TableLayoutPanel();
            this.chartHistory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartFailReason = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tblKpi = new System.Windows.Forms.TableLayoutPanel();
            this.kpiTotalTested = new System.Windows.Forms.Panel();
            this.lblTotalTestedVal = new System.Windows.Forms.Label();
            this.lblTotalTestedTitle = new System.Windows.Forms.Label();
            this.kpiPass = new System.Windows.Forms.Panel();
            this.lblPassVal = new System.Windows.Forms.Label();
            this.lblPassTitle = new System.Windows.Forms.Label();
            this.kpiFail = new System.Windows.Forms.Panel();
            this.lblFailVal = new System.Windows.Forms.Label();
            this.lblFailTitle = new System.Windows.Forms.Label();
            this.kpiYield = new System.Windows.Forms.Panel();
            this.lblYieldVal = new System.Windows.Forms.Label();
            this.lblYieldTitle = new System.Windows.Forms.Label();
            this.kpiCycleTime = new System.Windows.Forms.Panel();
            this.lblCycleTimeVal = new System.Windows.Forms.Label();
            this.lblCycleTimeTitle = new System.Windows.Forms.Label();
            this.cmbTimeRange = new System.Windows.Forms.ComboBox();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.cardHistory = new System.Windows.Forms.Panel();
            this.gridRecent = new System.Windows.Forms.DataGridView();
            this.lnkViewAll = new System.Windows.Forms.LinkLabel();
            this.lblRecentTitle = new System.Windows.Forms.Label();
            this.tblBottom = new System.Windows.Forms.TableLayoutPanel();
            this.cardModules = new System.Windows.Forms.Panel();
            this.tblModules = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMod1 = new System.Windows.Forms.Panel();
            this.lblMod1Status = new System.Windows.Forms.Label();
            this.lblMod1Title = new System.Windows.Forms.Label();
            this.pnlMod2 = new System.Windows.Forms.Panel();
            this.lblMod2Status = new System.Windows.Forms.Label();
            this.lblMod2Title = new System.Windows.Forms.Label();
            this.pnlMod3 = new System.Windows.Forms.Panel();
            this.lblMod3Status = new System.Windows.Forms.Label();
            this.lblMod3Title = new System.Windows.Forms.Label();
            this.pnlMod4 = new System.Windows.Forms.Panel();
            this.lblMod4Status = new System.Windows.Forms.Label();
            this.lblMod4Title = new System.Windows.Forms.Label();
            this.lblModulesTitle = new System.Windows.Forms.Label();
            this.cardOperatingTime = new System.Windows.Forms.Panel();
            this.lblOpTimeSub = new System.Windows.Forms.Label();
            this.lblOpTimeVal = new System.Windows.Forms.Label();
            this.lblOpTimeTitle = new System.Windows.Forms.Label();
            this.cardMaintenance = new System.Windows.Forms.Panel();
            this.lblMaintSub = new System.Windows.Forms.Label();
            this.lblMaintVal = new System.Windows.Forms.Label();
            this.lblMaintTitle = new System.Windows.Forms.Label();
            this.tblMain.SuspendLayout();
            this.tblTop.SuspendLayout();
            this.cardTester.SuspendLayout();
            this.cardMachine.SuspendLayout();
            this.cardDatabase.SuspendLayout();
            this.cardActiveProg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHarness)).BeginInit();
            this.tblMiddle.SuspendLayout();
            this.cardSummary.SuspendLayout();
            this.tblCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFailReason)).BeginInit();
            this.tblKpi.SuspendLayout();
            this.kpiTotalTested.SuspendLayout();
            this.kpiPass.SuspendLayout();
            this.kpiFail.SuspendLayout();
            this.kpiYield.SuspendLayout();
            this.kpiCycleTime.SuspendLayout();
            this.cardHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecent)).BeginInit();
            this.tblBottom.SuspendLayout();
            this.cardModules.SuspendLayout();
            this.tblModules.SuspendLayout();
            this.pnlMod1.SuspendLayout();
            this.pnlMod2.SuspendLayout();
            this.pnlMod3.SuspendLayout();
            this.pnlMod4.SuspendLayout();
            this.cardOperatingTime.SuspendLayout();
            this.cardMaintenance.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tblTop, 0, 0);
            this.tblMain.Controls.Add(this.tblMiddle, 0, 1);
            this.tblMain.Controls.Add(this.tblBottom, 0, 2);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 3;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblMain.Size = new System.Drawing.Size(1600, 900);
            this.tblMain.TabIndex = 0;
            // 
            // tblTop
            // 
            this.tblTop.ColumnCount = 4;
            this.tblTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblTop.Controls.Add(this.cardTester, 0, 0);
            this.tblTop.Controls.Add(this.cardMachine, 1, 0);
            this.tblTop.Controls.Add(this.cardDatabase, 2, 0);
            this.tblTop.Controls.Add(this.cardActiveProg, 3, 0);
            this.tblTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTop.Location = new System.Drawing.Point(8, 8);
            this.tblTop.Margin = new System.Windows.Forms.Padding(8);
            this.tblTop.Name = "tblTop";
            this.tblTop.RowCount = 1;
            this.tblTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTop.Size = new System.Drawing.Size(1584, 164);
            this.tblTop.TabIndex = 0;
            // 
            // cardTester
            // 
            this.cardTester.BackColor = System.Drawing.Color.White;
            this.cardTester.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardTester.Controls.Add(this.lblTesterSub);
            this.cardTester.Controls.Add(this.lblTesterStatus);
            this.cardTester.Controls.Add(this.lblTesterTitle);
            this.cardTester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTester.Location = new System.Drawing.Point(6, 6);
            this.cardTester.Margin = new System.Windows.Forms.Padding(6);
            this.cardTester.Name = "cardTester";
            this.cardTester.Padding = new System.Windows.Forms.Padding(15);
            this.cardTester.Size = new System.Drawing.Size(336, 152);
            this.cardTester.TabIndex = 0;
            // 
            // lblTesterSub
            // 
            this.lblTesterSub.AutoSize = true;
            this.lblTesterSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTesterSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTesterSub.Location = new System.Drawing.Point(15, 100);
            this.lblTesterSub.Name = "lblTesterSub";
            this.lblTesterSub.Size = new System.Drawing.Size(201, 20);
            this.lblTesterSub.TabIndex = 2;
            this.lblTesterSub.Text = "All tester modules are operational";
            // 
            // lblTesterStatus
            // 
            this.lblTesterStatus.AutoSize = true;
            this.lblTesterStatus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTesterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTesterStatus.Location = new System.Drawing.Point(13, 50);
            this.lblTesterStatus.Name = "lblTesterStatus";
            this.lblTesterStatus.Size = new System.Drawing.Size(155, 37);
            this.lblTesterStatus.TabIndex = 1;
            this.lblTesterStatus.Text = "● ONLINE";
            // 
            // lblTesterTitle
            // 
            this.lblTesterTitle.AutoSize = true;
            this.lblTesterTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTesterTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTesterTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTesterTitle.Name = "lblTesterTitle";
            this.lblTesterTitle.Size = new System.Drawing.Size(131, 23);
            this.lblTesterTitle.TabIndex = 0;
            this.lblTesterTitle.Text = "TESTER STATUS";
            // 
            // cardMachine
            // 
            this.cardMachine.BackColor = System.Drawing.Color.White;
            this.cardMachine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMachine.Controls.Add(this.lblMachineSub);
            this.cardMachine.Controls.Add(this.lblMachineStatus);
            this.cardMachine.Controls.Add(this.lblMachineTitle);
            this.cardMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardMachine.Location = new System.Drawing.Point(354, 6);
            this.cardMachine.Margin = new System.Windows.Forms.Padding(6);
            this.cardMachine.Name = "cardMachine";
            this.cardMachine.Padding = new System.Windows.Forms.Padding(15);
            this.cardMachine.Size = new System.Drawing.Size(336, 152);
            this.cardMachine.TabIndex = 1;
            // 
            // lblMachineSub
            // 
            this.lblMachineSub.AutoSize = true;
            this.lblMachineSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMachineSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMachineSub.Location = new System.Drawing.Point(15, 100);
            this.lblMachineSub.Name = "lblMachineSub";
            this.lblMachineSub.Size = new System.Drawing.Size(121, 20);
            this.lblMachineSub.TabIndex = 2;
            this.lblMachineSub.Text = "No active alarms";
            // 
            // lblMachineStatus
            // 
            this.lblMachineStatus.AutoSize = true;
            this.lblMachineStatus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMachineStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblMachineStatus.Location = new System.Drawing.Point(13, 50);
            this.lblMachineStatus.Name = "lblMachineStatus";
            this.lblMachineStatus.Size = new System.Drawing.Size(139, 37);
            this.lblMachineStatus.TabIndex = 1;
            this.lblMachineStatus.Text = "● READY";
            // 
            // lblMachineTitle
            // 
            this.lblMachineTitle.AutoSize = true;
            this.lblMachineTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMachineTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMachineTitle.Location = new System.Drawing.Point(15, 15);
            this.lblMachineTitle.Name = "lblMachineTitle";
            this.lblMachineTitle.Size = new System.Drawing.Size(152, 23);
            this.lblMachineTitle.TabIndex = 0;
            this.lblMachineTitle.Text = "MACHINE STATUS";
            // 
            // cardDatabase
            // 
            this.cardDatabase.BackColor = System.Drawing.Color.White;
            this.cardDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardDatabase.Controls.Add(this.lblDatabaseSub);
            this.cardDatabase.Controls.Add(this.lblDatabaseStatus);
            this.cardDatabase.Controls.Add(this.lblDatabaseTitle);
            this.cardDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDatabase.Location = new System.Drawing.Point(702, 6);
            this.cardDatabase.Margin = new System.Windows.Forms.Padding(6);
            this.cardDatabase.Name = "cardDatabase";
            this.cardDatabase.Padding = new System.Windows.Forms.Padding(15);
            this.cardDatabase.Size = new System.Drawing.Size(336, 152);
            this.cardDatabase.TabIndex = 2;
            // 
            // lblDatabaseSub
            // 
            this.lblDatabaseSub.AutoSize = true;
            this.lblDatabaseSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDatabaseSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDatabaseSub.Location = new System.Drawing.Point(15, 100);
            this.lblDatabaseSub.Name = "lblDatabaseSub";
            this.lblDatabaseSub.Size = new System.Drawing.Size(133, 20);
            this.lblDatabaseSub.TabIndex = 2;
            this.lblDatabaseSub.Text = "SQL Server: Online";
            // 
            // lblDatabaseStatus
            // 
            this.lblDatabaseStatus.AutoSize = true;
            this.lblDatabaseStatus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDatabaseStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblDatabaseStatus.Location = new System.Drawing.Point(13, 50);
            this.lblDatabaseStatus.Name = "lblDatabaseStatus";
            this.lblDatabaseStatus.Size = new System.Drawing.Size(209, 37);
            this.lblDatabaseStatus.TabIndex = 1;
            this.lblDatabaseStatus.Text = "● CONNECTED";
            // 
            // lblDatabaseTitle
            // 
            this.lblDatabaseTitle.AutoSize = true;
            this.lblDatabaseTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDatabaseTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDatabaseTitle.Location = new System.Drawing.Point(15, 15);
            this.lblDatabaseTitle.Name = "lblDatabaseTitle";
            this.lblDatabaseTitle.Size = new System.Drawing.Size(158, 23);
            this.lblDatabaseTitle.TabIndex = 0;
            this.lblDatabaseTitle.Text = "DATABASE STATUS";
            // 
            // cardActiveProg
            // 
            this.cardActiveProg.BackColor = System.Drawing.Color.White;
            this.cardActiveProg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardActiveProg.Controls.Add(this.btnChangeProg);
            this.cardActiveProg.Controls.Add(this.picHarness);
            this.cardActiveProg.Controls.Add(this.lblProgDetails);
            this.cardActiveProg.Controls.Add(this.lblActiveProgTitle);
            this.cardActiveProg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardActiveProg.Location = new System.Drawing.Point(1050, 6);
            this.cardActiveProg.Margin = new System.Windows.Forms.Padding(6);
            this.cardActiveProg.Name = "cardActiveProg";
            this.cardActiveProg.Padding = new System.Windows.Forms.Padding(15);
            this.cardActiveProg.Size = new System.Drawing.Size(528, 152);
            this.cardActiveProg.TabIndex = 3;
            // 
            // btnChangeProg
            // 
            this.btnChangeProg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeProg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnChangeProg.FlatAppearance.BorderSize = 0;
            this.btnChangeProg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeProg.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChangeProg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnChangeProg.Location = new System.Drawing.Point(365, 105);
            this.btnChangeProg.Name = "btnChangeProg";
            this.btnChangeProg.Size = new System.Drawing.Size(145, 34);
            this.btnChangeProg.TabIndex = 3;
            this.btnChangeProg.Text = "Change Program";
            this.btnChangeProg.UseVisualStyleBackColor = false;
            // 
            // picHarness
            // 
            this.picHarness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picHarness.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.picHarness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHarness.Location = new System.Drawing.Point(365, 15);
            this.picHarness.Name = "picHarness";
            this.picHarness.Size = new System.Drawing.Size(145, 80);
            this.picHarness.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHarness.TabIndex = 2;
            this.picHarness.TabStop = false;
            // 
            // lblProgDetails
            // 
            this.lblProgDetails.AutoSize = true;
            this.lblProgDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblProgDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblProgDetails.Location = new System.Drawing.Point(15, 45);
            this.lblProgDetails.Name = "lblProgDetails";
            this.lblProgDetails.Size = new System.Drawing.Size(225, 100);
            this.lblProgDetails.TabIndex = 1;
            this.lblProgDetails.Text = "Product Model : 6060\r\nProduct Type  : AC\r\nArea          : Engine Control\r\nPart Number   : WH-ENG-001\r\nECN No.       : 123.R001";
            // 
            // lblActiveProgTitle
            // 
            this.lblActiveProgTitle.AutoSize = true;
            this.lblActiveProgTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblActiveProgTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblActiveProgTitle.Location = new System.Drawing.Point(15, 15);
            this.lblActiveProgTitle.Name = "lblActiveProgTitle";
            this.lblActiveProgTitle.Size = new System.Drawing.Size(155, 23);
            this.lblActiveProgTitle.TabIndex = 0;
            this.lblActiveProgTitle.Text = "ACTIVE PROGRAM";
            // 
            // tblMiddle
            // 
            this.tblMiddle.ColumnCount = 2;
            this.tblMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblMiddle.Controls.Add(this.cardSummary, 0, 0);
            this.tblMiddle.Controls.Add(this.cardHistory, 1, 0);
            this.tblMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMiddle.Location = new System.Drawing.Point(8, 188);
            this.tblMiddle.Margin = new System.Windows.Forms.Padding(8);
            this.tblMiddle.Name = "tblMiddle";
            this.tblMiddle.RowCount = 1;
            this.tblMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMiddle.Size = new System.Drawing.Size(1584, 569);
            this.tblMiddle.TabIndex = 1;
            // 
            // cardSummary
            // 
            this.cardSummary.BackColor = System.Drawing.Color.White;
            this.cardSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardSummary.Controls.Add(this.tblCharts);
            this.cardSummary.Controls.Add(this.tblKpi);
            this.cardSummary.Controls.Add(this.cmbTimeRange);
            this.cardSummary.Controls.Add(this.lblSummaryTitle);
            this.cardSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardSummary.Location = new System.Drawing.Point(6, 6);
            this.cardSummary.Margin = new System.Windows.Forms.Padding(6);
            this.cardSummary.Name = "cardSummary";
            this.cardSummary.Padding = new System.Windows.Forms.Padding(15);
            this.cardSummary.Size = new System.Drawing.Size(859, 557);
            this.cardSummary.TabIndex = 0;
            // 
            // tblCharts
            // 
            this.tblCharts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblCharts.ColumnCount = 2;
            this.tblCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tblCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblCharts.Controls.Add(this.chartHistory, 0, 0);
            this.tblCharts.Controls.Add(this.chartFailReason, 1, 0);
            this.tblCharts.Location = new System.Drawing.Point(15, 150);
            this.tblCharts.Name = "tblCharts";
            this.tblCharts.RowCount = 1;
            this.tblCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCharts.Size = new System.Drawing.Size(827, 390);
            this.tblCharts.TabIndex = 3;
            // 
            // chartHistory
            // 
            this.chartHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartHistory.Location = new System.Drawing.Point(3, 3);
            this.chartHistory.Name = "chartHistory";
            this.chartHistory.Size = new System.Drawing.Size(531, 384);
            this.chartHistory.TabIndex = 0;
            this.chartHistory.Text = "chartHistory";
            // 
            // chartFailReason
            // 
            this.chartFailReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartFailReason.Location = new System.Drawing.Point(540, 3);
            this.chartFailReason.Name = "chartFailReason";
            this.chartFailReason.Size = new System.Drawing.Size(284, 384);
            this.chartFailReason.TabIndex = 1;
            this.chartFailReason.Text = "chartFailReason";
            // 
            // tblKpi
            // 
            this.tblKpi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblKpi.ColumnCount = 5;
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblKpi.Controls.Add(this.kpiTotalTested, 0, 0);
            this.tblKpi.Controls.Add(this.kpiPass, 1, 0);
            this.tblKpi.Controls.Add(this.kpiFail, 2, 0);
            this.tblKpi.Controls.Add(this.kpiYield, 3, 0);
            this.tblKpi.Controls.Add(this.kpiCycleTime, 4, 0);
            this.tblKpi.Location = new System.Drawing.Point(15, 55);
            this.tblKpi.Name = "tblKpi";
            this.tblKpi.RowCount = 1;
            this.tblKpi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblKpi.Size = new System.Drawing.Size(827, 85);
            this.tblKpi.TabIndex = 2;
            // 
            // kpiTotalTested
            // 
            this.kpiTotalTested.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.kpiTotalTested.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiTotalTested.Controls.Add(this.lblTotalTestedVal);
            this.kpiTotalTested.Controls.Add(this.lblTotalTestedTitle);
            this.kpiTotalTested.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpiTotalTested.Location = new System.Drawing.Point(4, 4);
            this.kpiTotalTested.Margin = new System.Windows.Forms.Padding(4);
            this.kpiTotalTested.Name = "kpiTotalTested";
            this.kpiTotalTested.Size = new System.Drawing.Size(157, 77);
            this.kpiTotalTested.TabIndex = 0;
            // 
            // lblTotalTestedVal
            // 
            this.lblTotalTestedVal.AutoSize = true;
            this.lblTotalTestedVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalTestedVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalTestedVal.Location = new System.Drawing.Point(8, 25);
            this.lblTotalTestedVal.Name = "lblTotalTestedVal";
            this.lblTotalTestedVal.Size = new System.Drawing.Size(69, 41);
            this.lblTotalTestedVal.TabIndex = 1;
            this.lblTotalTestedVal.Text = "215";
            // 
            // lblTotalTestedTitle
            // 
            this.lblTotalTestedTitle.AutoSize = true;
            this.lblTotalTestedTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalTestedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalTestedTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTotalTestedTitle.Name = "lblTotalTestedTitle";
            this.lblTotalTestedTitle.Size = new System.Drawing.Size(104, 19);
            this.lblTotalTestedTitle.TabIndex = 0;
            this.lblTotalTestedTitle.Text = "TOTAL TESTED";
            // 
            // kpiPass
            // 
            this.kpiPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.kpiPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiPass.Controls.Add(this.lblPassVal);
            this.kpiPass.Controls.Add(this.lblPassTitle);
            this.kpiPass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpiPass.Location = new System.Drawing.Point(169, 4);
            this.kpiPass.Margin = new System.Windows.Forms.Padding(4);
            this.kpiPass.Name = "kpiPass";
            this.kpiPass.Size = new System.Drawing.Size(157, 77);
            this.kpiPass.TabIndex = 1;
            // 
            // lblPassVal
            // 
            this.lblPassVal.AutoSize = true;
            this.lblPassVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPassVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblPassVal.Location = new System.Drawing.Point(8, 25);
            this.lblPassVal.Name = "lblPassVal";
            this.lblPassVal.Size = new System.Drawing.Size(69, 41);
            this.lblPassVal.TabIndex = 1;
            this.lblPassVal.Text = "212";
            // 
            // lblPassTitle
            // 
            this.lblPassTitle.AutoSize = true;
            this.lblPassTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPassTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblPassTitle.Location = new System.Drawing.Point(8, 8);
            this.lblPassTitle.Name = "lblPassTitle";
            this.lblPassTitle.Size = new System.Drawing.Size(111, 19);
            this.lblPassTitle.TabIndex = 0;
            this.lblPassTitle.Text = "PASS (98.60%)";
            // 
            // kpiFail
            // 
            this.kpiFail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.kpiFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiFail.Controls.Add(this.lblFailVal);
            this.kpiFail.Controls.Add(this.lblFailTitle);
            this.kpiFail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpiFail.Location = new System.Drawing.Point(334, 4);
            this.kpiFail.Margin = new System.Windows.Forms.Padding(4);
            this.kpiFail.Name = "kpiFail";
            this.kpiFail.Size = new System.Drawing.Size(157, 77);
            this.kpiFail.TabIndex = 2;
            // 
            // lblFailVal
            // 
            this.lblFailVal.AutoSize = true;
            this.lblFailVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFailVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblFailVal.Location = new System.Drawing.Point(8, 25);
            this.lblFailVal.Name = "lblFailVal";
            this.lblFailVal.Size = new System.Drawing.Size(35, 41);
            this.lblFailVal.TabIndex = 1;
            this.lblFailVal.Text = "3";
            // 
            // lblFailTitle
            // 
            this.lblFailTitle.AutoSize = true;
            this.lblFailTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblFailTitle.Location = new System.Drawing.Point(8, 8);
            this.lblFailTitle.Name = "lblFailTitle";
            this.lblFailTitle.Size = new System.Drawing.Size(96, 19);
            this.lblFailTitle.TabIndex = 0;
            this.lblFailTitle.Text = "FAIL (1.40%)";
            // 
            // kpiYield
            // 
            this.kpiYield.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.kpiYield.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiYield.Controls.Add(this.lblYieldVal);
            this.kpiYield.Controls.Add(this.lblYieldTitle);
            this.kpiYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpiYield.Location = new System.Drawing.Point(499, 4);
            this.kpiYield.Margin = new System.Windows.Forms.Padding(4);
            this.kpiYield.Name = "kpiYield";
            this.kpiYield.Size = new System.Drawing.Size(157, 77);
            this.kpiYield.TabIndex = 3;
            // 
            // lblYieldVal
            // 
            this.lblYieldVal.AutoSize = true;
            this.lblYieldVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblYieldVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(179)))), ((int)(((byte)(8)))));
            this.lblYieldVal.Location = new System.Drawing.Point(8, 25);
            this.lblYieldVal.Name = "lblYieldVal";
            this.lblYieldVal.Size = new System.Drawing.Size(126, 41);
            this.lblYieldVal.TabIndex = 1;
            this.lblYieldVal.Text = "98.60%";
            // 
            // lblYieldTitle
            // 
            this.lblYieldTitle.AutoSize = true;
            this.lblYieldTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblYieldTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(179)))), ((int)(((byte)(8)))));
            this.lblYieldTitle.Location = new System.Drawing.Point(8, 8);
            this.lblYieldTitle.Name = "lblYieldTitle";
            this.lblYieldTitle.Size = new System.Drawing.Size(46, 19);
            this.lblYieldTitle.TabIndex = 0;
            this.lblYieldTitle.Text = "YIELD";
            // 
            // kpiCycleTime
            // 
            this.kpiCycleTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.kpiCycleTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiCycleTime.Controls.Add(this.lblCycleTimeVal);
            this.kpiCycleTime.Controls.Add(this.lblCycleTimeTitle);
            this.kpiCycleTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpiCycleTime.Location = new System.Drawing.Point(664, 4);
            this.kpiCycleTime.Margin = new System.Windows.Forms.Padding(4);
            this.kpiCycleTime.Name = "kpiCycleTime";
            this.kpiCycleTime.Size = new System.Drawing.Size(159, 77);
            this.kpiCycleTime.TabIndex = 4;
            // 
            // lblCycleTimeVal
            // 
            this.lblCycleTimeVal.AutoSize = true;
            this.lblCycleTimeVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCycleTimeVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.lblCycleTimeVal.Location = new System.Drawing.Point(8, 25);
            this.lblCycleTimeVal.Name = "lblCycleTimeVal";
            this.lblCycleTimeVal.Size = new System.Drawing.Size(130, 41);
            this.lblCycleTimeVal.TabIndex = 1;
            this.lblCycleTimeVal.Text = "16.2 sec";
            // 
            // lblCycleTimeTitle
            // 
            this.lblCycleTimeTitle.AutoSize = true;
            this.lblCycleTimeTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCycleTimeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.lblCycleTimeTitle.Location = new System.Drawing.Point(8, 8);
            this.lblCycleTimeTitle.Name = "lblCycleTimeTitle";
            this.lblCycleTimeTitle.Size = new System.Drawing.Size(123, 19);
            this.lblCycleTimeTitle.TabIndex = 0;
            this.lblCycleTimeTitle.Text = "AVG CYCLE TIME";
            // 
            // cmbTimeRange
            // 
            this.cmbTimeRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTimeRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeRange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbTimeRange.FormattingEnabled = true;
            this.cmbTimeRange.Items.AddRange(new object[] {
            "Today"});
            this.cmbTimeRange.Location = new System.Drawing.Point(732, 13);
            this.cmbTimeRange.Name = "cmbTimeRange";
            this.cmbTimeRange.Size = new System.Drawing.Size(110, 28);
            this.cmbTimeRange.TabIndex = 1;
            // 
            // lblSummaryTitle
            // 
            this.lblSummaryTitle.AutoSize = true;
            this.lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSummaryTitle.Location = new System.Drawing.Point(15, 15);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Size = new System.Drawing.Size(248, 25);
            this.lblSummaryTitle.TabIndex = 0;
            this.lblSummaryTitle.Text = "SUMMARY REPORT (Today)";
            // 
            // cardHistory
            // 
            this.cardHistory.BackColor = System.Drawing.Color.White;
            this.cardHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardHistory.Controls.Add(this.gridRecent);
            this.cardHistory.Controls.Add(this.lnkViewAll);
            this.cardHistory.Controls.Add(this.lblRecentTitle);
            this.cardHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardHistory.Location = new System.Drawing.Point(877, 6);
            this.cardHistory.Margin = new System.Windows.Forms.Padding(6);
            this.cardHistory.Name = "cardHistory";
            this.cardHistory.Padding = new System.Windows.Forms.Padding(15);
            this.cardHistory.Size = new System.Drawing.Size(701, 557);
            this.cardHistory.TabIndex = 1;
            // 
            // gridRecent
            // 
            this.gridRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRecent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRecent.Location = new System.Drawing.Point(15, 55);
            this.gridRecent.Name = "gridRecent";
            this.gridRecent.RowHeadersWidth = 51;
            this.gridRecent.RowTemplate.Height = 35;
            this.gridRecent.Size = new System.Drawing.Size(669, 485);
            this.gridRecent.TabIndex = 2;
            // 
            // lnkViewAll
            // 
            this.lnkViewAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkViewAll.AutoSize = true;
            this.lnkViewAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lnkViewAll.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lnkViewAll.Location = new System.Drawing.Point(618, 18);
            this.lnkViewAll.Name = "lnkViewAll";
            this.lnkViewAll.Size = new System.Drawing.Size(66, 20);
            this.lnkViewAll.TabIndex = 1;
            this.lnkViewAll.TabStop = true;
            this.lnkViewAll.Text = "View All";
            // 
            // lblRecentTitle
            // 
            this.lblRecentTitle.AutoSize = true;
            this.lblRecentTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRecentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRecentTitle.Location = new System.Drawing.Point(15, 15);
            this.lblRecentTitle.Name = "lblRecentTitle";
            this.lblRecentTitle.Size = new System.Drawing.Size(214, 25);
            this.lblRecentTitle.TabIndex = 0;
            this.lblRecentTitle.Text = "RECENT TEST HISTORY";
            // 
            // tblBottom
            // 
            this.tblBottom.ColumnCount = 3;
            this.tblBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tblBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tblBottom.Controls.Add(this.cardModules, 0, 0);
            this.tblBottom.Controls.Add(this.cardOperatingTime, 1, 0);
            this.tblBottom.Controls.Add(this.cardMaintenance, 2, 0);
            this.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBottom.Location = new System.Drawing.Point(8, 773);
            this.tblBottom.Margin = new System.Windows.Forms.Padding(8);
            this.tblBottom.Name = "tblBottom";
            this.tblBottom.RowCount = 1;
            this.tblBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBottom.Size = new System.Drawing.Size(1584, 119);
            this.tblBottom.TabIndex = 2;
            // 
            // cardModules
            // 
            this.cardModules.BackColor = System.Drawing.Color.White;
            this.cardModules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardModules.Controls.Add(this.tblModules);
            this.cardModules.Controls.Add(this.lblModulesTitle);
            this.cardModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardModules.Location = new System.Drawing.Point(6, 6);
            this.cardModules.Margin = new System.Windows.Forms.Padding(6);
            this.cardModules.Name = "cardModules";
            this.cardModules.Padding = new System.Windows.Forms.Padding(15);
            this.cardModules.Size = new System.Drawing.Size(859, 107);
            this.cardModules.TabIndex = 0;
            // 
            // tblModules
            // 
            this.tblModules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblModules.ColumnCount = 4;
            this.tblModules.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblModules.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblModules.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblModules.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblModules.Controls.Add(this.pnlMod1, 0, 0);
            this.tblModules.Controls.Add(this.pnlMod2, 1, 0);
            this.tblModules.Controls.Add(this.pnlMod3, 2, 0);
            this.tblModules.Controls.Add(this.pnlMod4, 3, 0);
            this.tblModules.Location = new System.Drawing.Point(15, 38);
            this.tblModules.Name = "tblModules";
            this.tblModules.RowCount = 1;
            this.tblModules.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblModules.Size = new System.Drawing.Size(827, 52);
            this.tblModules.TabIndex = 1;
            // 
            // pnlMod1
            // 
            this.pnlMod1.Controls.Add(this.lblMod1Status);
            this.pnlMod1.Controls.Add(this.lblMod1Title);
            this.pnlMod1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMod1.Location = new System.Drawing.Point(3, 3);
            this.pnlMod1.Name = "pnlMod1";
            this.pnlMod1.Size = new System.Drawing.Size(200, 46);
            this.pnlMod1.TabIndex = 0;
            // 
            // lblMod1Status
            // 
            this.lblMod1Status.AutoSize = true;
            this.lblMod1Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod1Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblMod1Status.Location = new System.Drawing.Point(3, 22);
            this.lblMod1Status.Name = "lblMod1Status";
            this.lblMod1Status.Size = new System.Drawing.Size(84, 20);
            this.lblMod1Status.TabIndex = 1;
            this.lblMod1Status.Text = "● ONLINE";
            // 
            // lblMod1Title
            // 
            this.lblMod1Title.AutoSize = true;
            this.lblMod1Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMod1Title.Location = new System.Drawing.Point(3, 2);
            this.lblMod1Title.Name = "lblMod1Title";
            this.lblMod1Title.Size = new System.Drawing.Size(95, 20);
            this.lblMod1Title.TabIndex = 0;
            this.lblMod1Title.Text = "Relay Matrix";
            // 
            // pnlMod2
            // 
            this.pnlMod2.Controls.Add(this.lblMod2Status);
            this.pnlMod2.Controls.Add(this.lblMod2Title);
            this.pnlMod2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMod2.Location = new System.Drawing.Point(209, 3);
            this.pnlMod2.Name = "pnlMod2";
            this.pnlMod2.Size = new System.Drawing.Size(200, 46);
            this.pnlMod2.TabIndex = 1;
            // 
            // lblMod2Status
            // 
            this.lblMod2Status.AutoSize = true;
            this.lblMod2Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod2Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblMod2Status.Location = new System.Drawing.Point(3, 22);
            this.lblMod2Status.Name = "lblMod2Status";
            this.lblMod2Status.Size = new System.Drawing.Size(84, 20);
            this.lblMod2Status.TabIndex = 1;
            this.lblMod2Status.Text = "● ONLINE";
            // 
            // lblMod2Title
            // 
            this.lblMod2Title.AutoSize = true;
            this.lblMod2Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMod2Title.Location = new System.Drawing.Point(3, 2);
            this.lblMod2Title.Name = "lblMod2Title";
            this.lblMod2Title.Size = new System.Drawing.Size(100, 20);
            this.lblMod2Title.TabIndex = 0;
            this.lblMod2Title.Text = "Power Supply";
            // 
            // pnlMod3
            // 
            this.pnlMod3.Controls.Add(this.lblMod3Status);
            this.pnlMod3.Controls.Add(this.lblMod3Title);
            this.pnlMod3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMod3.Location = new System.Drawing.Point(415, 3);
            this.pnlMod3.Name = "pnlMod3";
            this.pnlMod3.Size = new System.Drawing.Size(200, 46);
            this.pnlMod3.TabIndex = 2;
            // 
            // lblMod3Status
            // 
            this.lblMod3Status.AutoSize = true;
            this.lblMod3Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod3Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblMod3Status.Location = new System.Drawing.Point(3, 22);
            this.lblMod3Status.Name = "lblMod3Status";
            this.lblMod3Status.Size = new System.Drawing.Size(84, 20);
            this.lblMod3Status.TabIndex = 1;
            this.lblMod3Status.Text = "● ONLINE";
            // 
            // lblMod3Title
            // 
            this.lblMod3Title.AutoSize = true;
            this.lblMod3Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMod3Title.Location = new System.Drawing.Point(3, 2);
            this.lblMod3Title.Name = "lblMod3Title";
            this.lblMod3Title.Size = new System.Drawing.Size(110, 20);
            this.lblMod3Title.TabIndex = 0;
            this.lblMod3Title.Text = "PLC Controller";
            // 
            // pnlMod4
            // 
            this.pnlMod4.Controls.Add(this.lblMod4Status);
            this.pnlMod4.Controls.Add(this.lblMod4Title);
            this.pnlMod4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMod4.Location = new System.Drawing.Point(621, 3);
            this.pnlMod4.Name = "pnlMod4";
            this.pnlMod4.Size = new System.Drawing.Size(203, 46);
            this.pnlMod4.TabIndex = 3;
            // 
            // lblMod4Status
            // 
            this.lblMod4Status.AutoSize = true;
            this.lblMod4Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod4Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblMod4Status.Location = new System.Drawing.Point(3, 22);
            this.lblMod4Status.Name = "lblMod4Status";
            this.lblMod4Status.Size = new System.Drawing.Size(84, 20);
            this.lblMod4Status.TabIndex = 1;
            this.lblMod4Status.Text = "● ONLINE";
            // 
            // lblMod4Title
            // 
            this.lblMod4Title.AutoSize = true;
            this.lblMod4Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMod4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMod4Title.Location = new System.Drawing.Point(3, 2);
            this.lblMod4Title.Name = "lblMod4Title";
            this.lblMod4Title.Size = new System.Drawing.Size(87, 20);
            this.lblMod4Title.TabIndex = 0;
            this.lblMod4Title.Text = "I/O Module";
            // 
            // lblModulesTitle
            // 
            this.lblModulesTitle.AutoSize = true;
            this.lblModulesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblModulesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblModulesTitle.Location = new System.Drawing.Point(15, 12);
            this.lblModulesTitle.Name = "lblModulesTitle";
            this.lblModulesTitle.Size = new System.Drawing.Size(217, 23);
            this.lblModulesTitle.TabIndex = 0;
            this.lblModulesTitle.Text = "SYSTEM MODULE STATUS";
            // 
            // cardOperatingTime
            // 
            this.cardOperatingTime.BackColor = System.Drawing.Color.White;
            this.cardOperatingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardOperatingTime.Controls.Add(this.lblOpTimeSub);
            this.cardOperatingTime.Controls.Add(this.lblOpTimeVal);
            this.cardOperatingTime.Controls.Add(this.lblOpTimeTitle);
            this.cardOperatingTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardOperatingTime.Location = new System.Drawing.Point(877, 6);
            this.cardOperatingTime.Margin = new System.Windows.Forms.Padding(6);
            this.cardOperatingTime.Name = "cardOperatingTime";
            this.cardOperatingTime.Padding = new System.Windows.Forms.Padding(15);
            this.cardOperatingTime.Size = new System.Drawing.Size(344, 107);
            this.cardOperatingTime.TabIndex = 1;
            // 
            // lblOpTimeSub
            // 
            this.lblOpTimeSub.AutoSize = true;
            this.lblOpTimeSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOpTimeSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOpTimeSub.Location = new System.Drawing.Point(15, 75);
            this.lblOpTimeSub.Name = "lblOpTimeSub";
            this.lblOpTimeSub.Size = new System.Drawing.Size(100, 20);
            this.lblOpTimeSub.TabIndex = 2;
            this.lblOpTimeSub.Text = "HH : MM : SS";
            // 
            // lblOpTimeVal
            // 
            this.lblOpTimeVal.AutoSize = true;
            this.lblOpTimeVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOpTimeVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblOpTimeVal.Location = new System.Drawing.Point(12, 32);
            this.lblOpTimeVal.Name = "lblOpTimeVal";
            this.lblOpTimeVal.Size = new System.Drawing.Size(206, 46);
            this.lblOpTimeVal.TabIndex = 1;
            this.lblOpTimeVal.Text = "03 : 46 : 22";
            // 
            // lblOpTimeTitle
            // 
            this.lblOpTimeTitle.AutoSize = true;
            this.lblOpTimeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOpTimeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOpTimeTitle.Location = new System.Drawing.Point(15, 12);
            this.lblOpTimeTitle.Name = "lblOpTimeTitle";
            this.lblOpTimeTitle.Size = new System.Drawing.Size(232, 20);
            this.lblOpTimeTitle.TabIndex = 0;
            this.lblOpTimeTitle.Text = "TOTAL OPERATING TIME (Today)";
            // 
            // cardMaintenance
            // 
            this.cardMaintenance.BackColor = System.Drawing.Color.White;
            this.cardMaintenance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMaintenance.Controls.Add(this.lblMaintSub);
            this.cardMaintenance.Controls.Add(this.lblMaintVal);
            this.cardMaintenance.Controls.Add(this.lblMaintTitle);
            this.cardMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardMaintenance.Location = new System.Drawing.Point(1233, 6);
            this.cardMaintenance.Margin = new System.Windows.Forms.Padding(6);
            this.cardMaintenance.Name = "cardMaintenance";
            this.cardMaintenance.Padding = new System.Windows.Forms.Padding(15);
            this.cardMaintenance.Size = new System.Drawing.Size(345, 107);
            this.cardMaintenance.TabIndex = 2;
            // 
            // lblMaintSub
            // 
            this.lblMaintSub.AutoSize = true;
            this.lblMaintSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMaintSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaintSub.Location = new System.Drawing.Point(15, 75);
            this.lblMaintSub.Name = "lblMaintSub";
            this.lblMaintSub.Size = new System.Drawing.Size(114, 20);
            this.lblMaintSub.TabIndex = 2;
            this.lblMaintSub.Text = "Or 6,500 Hours";
            // 
            // lblMaintVal
            // 
            this.lblMaintVal.AutoSize = true;
            this.lblMaintVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMaintVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblMaintVal.Location = new System.Drawing.Point(12, 32);
            this.lblMaintVal.Name = "lblMaintVal";
            this.lblMaintVal.Size = new System.Drawing.Size(159, 46);
            this.lblMaintVal.TabIndex = 1;
            this.lblMaintVal.Text = "145 Days";
            // 
            // lblMaintTitle
            // 
            this.lblMaintTitle.AutoSize = true;
            this.lblMaintTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMaintTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaintTitle.Location = new System.Drawing.Point(15, 12);
            this.lblMaintTitle.Name = "lblMaintTitle";
            this.lblMaintTitle.Size = new System.Drawing.Size(135, 20);
            this.lblMaintTitle.TabIndex = 0;
            this.lblMaintTitle.Text = "Next Maintenance";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.tblMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(1600, 900);
            this.tblMain.ResumeLayout(false);
            this.tblTop.ResumeLayout(false);
            this.cardTester.ResumeLayout(false);
            this.cardTester.PerformLayout();
            this.cardMachine.ResumeLayout(false);
            this.cardMachine.PerformLayout();
            this.cardDatabase.ResumeLayout(false);
            this.cardDatabase.PerformLayout();
            this.cardActiveProg.ResumeLayout(false);
            this.cardActiveProg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHarness)).EndInit();
            this.tblMiddle.ResumeLayout(false);
            this.cardSummary.ResumeLayout(false);
            this.cardSummary.PerformLayout();
            this.tblCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFailReason)).EndInit();
            this.tblKpi.ResumeLayout(false);
            this.kpiTotalTested.ResumeLayout(false);
            this.kpiTotalTested.PerformLayout();
            this.kpiPass.ResumeLayout(false);
            this.kpiPass.PerformLayout();
            this.kpiFail.ResumeLayout(false);
            this.kpiFail.PerformLayout();
            this.kpiYield.ResumeLayout(false);
            this.kpiYield.PerformLayout();
            this.kpiCycleTime.ResumeLayout(false);
            this.kpiCycleTime.PerformLayout();
            this.cardHistory.ResumeLayout(false);
            this.cardHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecent)).EndInit();
            this.tblBottom.ResumeLayout(false);
            this.cardModules.ResumeLayout(false);
            this.cardModules.PerformLayout();
            this.tblModules.ResumeLayout(false);
            this.pnlMod1.ResumeLayout(false);
            this.pnlMod1.PerformLayout();
            this.pnlMod2.ResumeLayout(false);
            this.pnlMod2.PerformLayout();
            this.pnlMod3.ResumeLayout(false);
            this.pnlMod3.PerformLayout();
            this.pnlMod4.ResumeLayout(false);
            this.pnlMod4.PerformLayout();
            this.cardOperatingTime.ResumeLayout(false);
            this.cardOperatingTime.PerformLayout();
            this.cardMaintenance.ResumeLayout(false);
            this.cardMaintenance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TableLayoutPanel tblTop;
        private System.Windows.Forms.Panel cardTester;
        private System.Windows.Forms.Label lblTesterSub;
        private System.Windows.Forms.Label lblTesterStatus;
        private System.Windows.Forms.Label lblTesterTitle;
        private System.Windows.Forms.Panel cardMachine;
        private System.Windows.Forms.Label lblMachineSub;
        private System.Windows.Forms.Label lblMachineStatus;
        private System.Windows.Forms.Label lblMachineTitle;
        private System.Windows.Forms.Panel cardDatabase;
        private System.Windows.Forms.Label lblDatabaseSub;
        private System.Windows.Forms.Label lblDatabaseStatus;
        private System.Windows.Forms.Label lblDatabaseTitle;
        private System.Windows.Forms.Panel cardActiveProg;
        private System.Windows.Forms.Button btnChangeProg;
        private System.Windows.Forms.PictureBox picHarness;
        private System.Windows.Forms.Label lblProgDetails;
        private System.Windows.Forms.Label lblActiveProgTitle;
        private System.Windows.Forms.TableLayoutPanel tblMiddle;
        private System.Windows.Forms.Panel cardSummary;
        private System.Windows.Forms.ComboBox cmbTimeRange;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.TableLayoutPanel tblKpi;
        private System.Windows.Forms.Panel kpiTotalTested;
        private System.Windows.Forms.Label lblTotalTestedVal;
        private System.Windows.Forms.Label lblTotalTestedTitle;
        private System.Windows.Forms.Panel kpiPass;
        private System.Windows.Forms.Label lblPassVal;
        private System.Windows.Forms.Label lblPassTitle;
        private System.Windows.Forms.Panel kpiFail;
        private System.Windows.Forms.Label lblFailVal;
        private System.Windows.Forms.Label lblFailTitle;
        private System.Windows.Forms.Panel kpiYield;
        private System.Windows.Forms.Label lblYieldVal;
        private System.Windows.Forms.Label lblYieldTitle;
        private System.Windows.Forms.Panel kpiCycleTime;
        private System.Windows.Forms.Label lblCycleTimeVal;
        private System.Windows.Forms.Label lblCycleTimeTitle;
        private System.Windows.Forms.TableLayoutPanel tblCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistory;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFailReason;
        private System.Windows.Forms.Panel cardHistory;
        private System.Windows.Forms.DataGridView gridRecent;
        private System.Windows.Forms.LinkLabel lnkViewAll;
        private System.Windows.Forms.Label lblRecentTitle;
        private System.Windows.Forms.TableLayoutPanel tblBottom;
        private System.Windows.Forms.Panel cardModules;
        private System.Windows.Forms.TableLayoutPanel tblModules;
        private System.Windows.Forms.Panel pnlMod1;
        private System.Windows.Forms.Label lblMod1Status;
        private System.Windows.Forms.Label lblMod1Title;
        private System.Windows.Forms.Panel pnlMod2;
        private System.Windows.Forms.Label lblMod2Status;
        private System.Windows.Forms.Label lblMod2Title;
        private System.Windows.Forms.Panel pnlMod3;
        private System.Windows.Forms.Label lblMod3Status;
        private System.Windows.Forms.Label lblMod3Title;
        private System.Windows.Forms.Panel pnlMod4;
        private System.Windows.Forms.Label lblMod4Status;
        private System.Windows.Forms.Label lblMod4Title;
        private System.Windows.Forms.Label lblModulesTitle;
        private System.Windows.Forms.Panel cardOperatingTime;
        private System.Windows.Forms.Label lblOpTimeSub;
        private System.Windows.Forms.Label lblOpTimeVal;
        private System.Windows.Forms.Label lblOpTimeTitle;
        private System.Windows.Forms.Panel cardMaintenance;
        private System.Windows.Forms.Label lblMaintSub;
        private System.Windows.Forms.Label lblMaintVal;
        private System.Windows.Forms.Label lblMaintTitle;
    }
}