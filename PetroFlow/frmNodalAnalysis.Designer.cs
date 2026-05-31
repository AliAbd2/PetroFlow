namespace PetroFlow
{
    partial class frmNodalAnalysis
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNodalAnalysis));
            colorDialog1 = new ColorDialog();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tabMian = new TabControl();
            tpIPR = new TabPage();
            panel1 = new Panel();
            gbGeneralControl = new GroupBox();
            cbSelectCurve = new ComboBox();
            lblSelectCurve = new Label();
            pnlSelectMethod = new Panel();
            rdoJones = new RadioButton();
            rdoFetkovich = new RadioButton();
            rdoStandingMethod = new RadioButton();
            rdoVogelMethod = new RadioButton();
            lblSelectMethod = new Label();
            pnlIPRScenario = new Panel();
            lblIPRScenario = new Label();
            rdoFutureIPR = new RadioButton();
            rdoCurrentIPR = new RadioButton();
            pnlWellType = new Panel();
            rdoGasWell = new RadioButton();
            rdoOilWell = new RadioButton();
            lblWellType = new Label();
            gbTestData = new GroupBox();
            cbUseTestData = new CheckBox();
            txtFlowCoefficient = new TextBox();
            lblFlowCoefficient = new Label();
            txtWellExponent = new TextBox();
            dgvTestData = new DataGridView();
            lblWellExponent = new Label();
            btnAddTestDataRow = new Button();
            cbNewFlowEfficiencyUnit = new ComboBox();
            cbTestFlowEfficiencyUnit = new ComboBox();
            txtNewFlowEfficiency = new TextBox();
            lblNewFlowEfficiency = new Label();
            lblTestFlowEfficiency = new Label();
            txtTestFlowEfficiency = new TextBox();
            gbReservoirData = new GroupBox();
            tabReservoirData = new TabControl();
            tpPresentReservoirData = new TabPage();
            txtReservoirPressure = new TextBox();
            cbOilFormationVolumeFactorUnit = new ComboBox();
            cbOilViscosityUnit = new ComboBox();
            cbOilRelativePermeabilityUnit = new ComboBox();
            cbBubblePointPressureUnit = new ComboBox();
            cbReservoirPressureUnit = new ComboBox();
            lblOilFormationVolumeFactor = new Label();
            txtOilRelativePermeability = new TextBox();
            txtOilFormationVolumeFactor = new TextBox();
            txtOilViscosity = new TextBox();
            txtBubblePointPressure = new TextBox();
            lblOilViscosity = new Label();
            lblOilRelativePermeability = new Label();
            lblBubblePointPressure = new Label();
            lblReservoirPressure = new Label();
            tpFuture = new TabPage();
            cbFutureOilFormationVolumeFactorUnit = new ComboBox();
            cbFutureOilViscosityUnit = new ComboBox();
            cbFutureOilRelativePermeabilityUnit = new ComboBox();
            cbFutureReservoiPressureUnit = new ComboBox();
            txtFutureOilViscosity = new TextBox();
            txtFutureOilFormationVolumeFactor = new TextBox();
            txtFutureOilRelativePermeability = new TextBox();
            txtFutureReservoirPressure = new TextBox();
            lblFutureOilFormationVolumeFactor = new Label();
            lblFutureOilRelativePermeability = new Label();
            lblFutureOilViscosity = new Label();
            lblFutureReservoirPressure = new Label();
            tpVLP = new TabPage();
            tabResult = new TabPage();
            pnlPlot = new Panel();
            pnlFormatPlot = new Panel();
            gbCurveGenerationSettings = new GroupBox();
            nudMinimumPressure = new NumericUpDown();
            nudPressureStepSize = new NumericUpDown();
            label1 = new Label();
            lblPressureStepSize = new Label();
            gbCurveSetting = new GroupBox();
            lblMarkerShape = new Label();
            cbMarkerShape = new ComboBox();
            lblLinePattern = new Label();
            lblPlotLineWidth = new Label();
            nudLineWidth = new NumericUpDown();
            lblPlotMarkerSize = new Label();
            cbLinePattern = new ComboBox();
            txtLegendText = new TextBox();
            btnChooseColor = new Button();
            nudMarkerSize = new NumericUpDown();
            lblPlotLineColor = new Label();
            lblPlotLegendText = new Label();
            gbPlotSetting = new GroupBox();
            lblPlotTitleText = new Label();
            txtPlotTitle = new TextBox();
            lblPlotXAxisLabel = new Label();
            lblPlotYAxisLabel = new Label();
            txtXlabel = new TextBox();
            txtylabel = new TextBox();
            pnlFormatPlotHeader = new Panel();
            lblFormatPlotTitle = new Label();
            pltNodalAnalysis = new ScottPlot.WinForms.FormsPlot();
            btnFomatPlot = new Button();
            pnlMain = new Panel();
            statusStrip1.SuspendLayout();
            tabMian.SuspendLayout();
            tpIPR.SuspendLayout();
            panel1.SuspendLayout();
            gbGeneralControl.SuspendLayout();
            pnlSelectMethod.SuspendLayout();
            pnlIPRScenario.SuspendLayout();
            pnlWellType.SuspendLayout();
            gbTestData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTestData).BeginInit();
            gbReservoirData.SuspendLayout();
            tabReservoirData.SuspendLayout();
            tpPresentReservoirData.SuspendLayout();
            tpFuture.SuspendLayout();
            tabResult.SuspendLayout();
            pnlPlot.SuspendLayout();
            pnlFormatPlot.SuspendLayout();
            gbCurveGenerationSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinimumPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPressureStepSize).BeginInit();
            gbCurveSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLineWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMarkerSize).BeginInit();
            gbPlotSetting.SuspendLayout();
            pnlFormatPlotHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(86, 156, 214);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 754);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1545, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tabMian
            // 
            tabMian.Controls.Add(tpIPR);
            tabMian.Controls.Add(tpVLP);
            tabMian.Controls.Add(tabResult);
            tabMian.Dock = DockStyle.Fill;
            tabMian.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabMian.ItemSize = new Size(25, 25);
            tabMian.Location = new Point(0, 0);
            tabMian.Multiline = true;
            tabMian.Name = "tabMian";
            tabMian.RightToLeft = RightToLeft.No;
            tabMian.SelectedIndex = 0;
            tabMian.Size = new Size(1545, 754);
            tabMian.TabIndex = 21;
            // 
            // tpIPR
            // 
            tpIPR.Controls.Add(panel1);
            tpIPR.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tpIPR.Location = new Point(4, 29);
            tpIPR.Name = "tpIPR";
            tpIPR.Padding = new Padding(3);
            tpIPR.Size = new Size(1537, 721);
            tpIPR.TabIndex = 0;
            tpIPR.Text = "IPR";
            tpIPR.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.FromArgb(245, 247, 251);
            panel1.Controls.Add(gbGeneralControl);
            panel1.Controls.Add(gbTestData);
            panel1.Controls.Add(gbReservoirData);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1531, 715);
            panel1.TabIndex = 20;
            // 
            // gbGeneralControl
            // 
            gbGeneralControl.BackColor = Color.White;
            gbGeneralControl.Controls.Add(cbSelectCurve);
            gbGeneralControl.Controls.Add(lblSelectCurve);
            gbGeneralControl.Controls.Add(pnlSelectMethod);
            gbGeneralControl.Controls.Add(pnlIPRScenario);
            gbGeneralControl.Controls.Add(pnlWellType);
            gbGeneralControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbGeneralControl.ForeColor = Color.Black;
            gbGeneralControl.Location = new Point(697, 11);
            gbGeneralControl.Name = "gbGeneralControl";
            gbGeneralControl.Size = new Size(811, 312);
            gbGeneralControl.TabIndex = 11;
            gbGeneralControl.TabStop = false;
            gbGeneralControl.Text = "General Control";
            // 
            // cbSelectCurve
            // 
            cbSelectCurve.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectCurve.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbSelectCurve.FormattingEnabled = true;
            cbSelectCurve.Location = new Point(134, 243);
            cbSelectCurve.Name = "cbSelectCurve";
            cbSelectCurve.Size = new Size(198, 25);
            cbSelectCurve.TabIndex = 17;
            // 
            // lblSelectCurve
            // 
            lblSelectCurve.AutoSize = true;
            lblSelectCurve.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectCurve.Location = new Point(6, 245);
            lblSelectCurve.Name = "lblSelectCurve";
            lblSelectCurve.Size = new Size(106, 21);
            lblSelectCurve.TabIndex = 16;
            lblSelectCurve.Text = "Select Curve:";
            // 
            // pnlSelectMethod
            // 
            pnlSelectMethod.BackColor = Color.Transparent;
            pnlSelectMethod.Controls.Add(rdoJones);
            pnlSelectMethod.Controls.Add(rdoFetkovich);
            pnlSelectMethod.Controls.Add(rdoStandingMethod);
            pnlSelectMethod.Controls.Add(rdoVogelMethod);
            pnlSelectMethod.Controls.Add(lblSelectMethod);
            pnlSelectMethod.Dock = DockStyle.Top;
            pnlSelectMethod.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pnlSelectMethod.Location = new Point(3, 106);
            pnlSelectMethod.Name = "pnlSelectMethod";
            pnlSelectMethod.Size = new Size(805, 104);
            pnlSelectMethod.TabIndex = 15;
            // 
            // rdoJones
            // 
            rdoJones.AutoSize = true;
            rdoJones.Location = new Point(16, 66);
            rdoJones.Name = "rdoJones";
            rdoJones.Size = new Size(194, 25);
            rdoJones.TabIndex = 16;
            rdoJones.TabStop = true;
            rdoJones.Text = "Jones, Blount, and Glaze";
            rdoJones.UseVisualStyleBackColor = true;
            // 
            // rdoFetkovich
            // 
            rdoFetkovich.AutoSize = true;
            rdoFetkovich.Location = new Point(208, 31);
            rdoFetkovich.Name = "rdoFetkovich";
            rdoFetkovich.Size = new Size(94, 25);
            rdoFetkovich.TabIndex = 15;
            rdoFetkovich.TabStop = true;
            rdoFetkovich.Text = "Fetkovich";
            rdoFetkovich.UseVisualStyleBackColor = true;
            // 
            // rdoStandingMethod
            // 
            rdoStandingMethod.AutoSize = true;
            rdoStandingMethod.Location = new Point(99, 31);
            rdoStandingMethod.Name = "rdoStandingMethod";
            rdoStandingMethod.Size = new Size(89, 25);
            rdoStandingMethod.TabIndex = 14;
            rdoStandingMethod.TabStop = true;
            rdoStandingMethod.Text = "Standing";
            rdoStandingMethod.UseVisualStyleBackColor = true;
            // 
            // rdoVogelMethod
            // 
            rdoVogelMethod.AutoSize = true;
            rdoVogelMethod.Location = new Point(16, 31);
            rdoVogelMethod.Name = "rdoVogelMethod";
            rdoVogelMethod.Size = new Size(67, 25);
            rdoVogelMethod.TabIndex = 13;
            rdoVogelMethod.TabStop = true;
            rdoVogelMethod.Text = "Vogel";
            rdoVogelMethod.UseVisualStyleBackColor = true;
            // 
            // lblSelectMethod
            // 
            lblSelectMethod.AutoSize = true;
            lblSelectMethod.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectMethod.Location = new Point(6, 3);
            lblSelectMethod.Name = "lblSelectMethod";
            lblSelectMethod.Size = new Size(122, 21);
            lblSelectMethod.TabIndex = 12;
            lblSelectMethod.Text = "Select Method:";
            // 
            // pnlIPRScenario
            // 
            pnlIPRScenario.BackColor = Color.Transparent;
            pnlIPRScenario.Controls.Add(lblIPRScenario);
            pnlIPRScenario.Controls.Add(rdoFutureIPR);
            pnlIPRScenario.Controls.Add(rdoCurrentIPR);
            pnlIPRScenario.Dock = DockStyle.Top;
            pnlIPRScenario.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pnlIPRScenario.Location = new Point(3, 69);
            pnlIPRScenario.Name = "pnlIPRScenario";
            pnlIPRScenario.Size = new Size(805, 37);
            pnlIPRScenario.TabIndex = 14;
            // 
            // lblIPRScenario
            // 
            lblIPRScenario.AutoSize = true;
            lblIPRScenario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIPRScenario.Location = new Point(6, 3);
            lblIPRScenario.Name = "lblIPRScenario";
            lblIPRScenario.Size = new Size(105, 21);
            lblIPRScenario.TabIndex = 11;
            lblIPRScenario.Text = "IPR Scenario:";
            // 
            // rdoFutureIPR
            // 
            rdoFutureIPR.AutoSize = true;
            rdoFutureIPR.Location = new Point(229, 1);
            rdoFutureIPR.Name = "rdoFutureIPR";
            rdoFutureIPR.Size = new Size(73, 25);
            rdoFutureIPR.TabIndex = 13;
            rdoFutureIPR.TabStop = true;
            rdoFutureIPR.Text = "Future";
            rdoFutureIPR.UseVisualStyleBackColor = true;
            // 
            // rdoCurrentIPR
            // 
            rdoCurrentIPR.AutoSize = true;
            rdoCurrentIPR.BackColor = Color.Transparent;
            rdoCurrentIPR.Location = new Point(127, 1);
            rdoCurrentIPR.Name = "rdoCurrentIPR";
            rdoCurrentIPR.Size = new Size(81, 25);
            rdoCurrentIPR.TabIndex = 12;
            rdoCurrentIPR.TabStop = true;
            rdoCurrentIPR.Text = "Current";
            rdoCurrentIPR.UseVisualStyleBackColor = false;
            // 
            // pnlWellType
            // 
            pnlWellType.BackColor = Color.Transparent;
            pnlWellType.Controls.Add(rdoGasWell);
            pnlWellType.Controls.Add(rdoOilWell);
            pnlWellType.Controls.Add(lblWellType);
            pnlWellType.Dock = DockStyle.Top;
            pnlWellType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pnlWellType.Location = new Point(3, 25);
            pnlWellType.Name = "pnlWellType";
            pnlWellType.Size = new Size(805, 44);
            pnlWellType.TabIndex = 0;
            // 
            // rdoGasWell
            // 
            rdoGasWell.AutoSize = true;
            rdoGasWell.Location = new Point(229, 9);
            rdoGasWell.Name = "rdoGasWell";
            rdoGasWell.Size = new Size(88, 25);
            rdoGasWell.TabIndex = 7;
            rdoGasWell.TabStop = true;
            rdoGasWell.Text = "Gas Well";
            rdoGasWell.UseVisualStyleBackColor = true;
            // 
            // rdoOilWell
            // 
            rdoOilWell.AutoSize = true;
            rdoOilWell.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rdoOilWell.Location = new Point(127, 9);
            rdoOilWell.Name = "rdoOilWell";
            rdoOilWell.Size = new Size(82, 25);
            rdoOilWell.TabIndex = 6;
            rdoOilWell.TabStop = true;
            rdoOilWell.Text = "Oil Well";
            rdoOilWell.UseVisualStyleBackColor = true;
            // 
            // lblWellType
            // 
            lblWellType.AutoSize = true;
            lblWellType.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWellType.Location = new Point(6, 11);
            lblWellType.Name = "lblWellType";
            lblWellType.Size = new Size(85, 21);
            lblWellType.TabIndex = 5;
            lblWellType.Text = "Well Type:";
            // 
            // gbTestData
            // 
            gbTestData.BackColor = Color.White;
            gbTestData.Controls.Add(cbUseTestData);
            gbTestData.Controls.Add(txtFlowCoefficient);
            gbTestData.Controls.Add(lblFlowCoefficient);
            gbTestData.Controls.Add(txtWellExponent);
            gbTestData.Controls.Add(dgvTestData);
            gbTestData.Controls.Add(lblWellExponent);
            gbTestData.Controls.Add(btnAddTestDataRow);
            gbTestData.Controls.Add(cbNewFlowEfficiencyUnit);
            gbTestData.Controls.Add(cbTestFlowEfficiencyUnit);
            gbTestData.Controls.Add(txtNewFlowEfficiency);
            gbTestData.Controls.Add(lblNewFlowEfficiency);
            gbTestData.Controls.Add(lblTestFlowEfficiency);
            gbTestData.Controls.Add(txtTestFlowEfficiency);
            gbTestData.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbTestData.ForeColor = Color.Black;
            gbTestData.Location = new Point(15, 341);
            gbTestData.Name = "gbTestData";
            gbTestData.Size = new Size(1493, 321);
            gbTestData.TabIndex = 4;
            gbTestData.TabStop = false;
            gbTestData.Text = "Test Data";
            // 
            // cbUseTestData
            // 
            cbUseTestData.AutoSize = true;
            cbUseTestData.BackColor = Color.Transparent;
            cbUseTestData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbUseTestData.Location = new Point(366, 213);
            cbUseTestData.Name = "cbUseTestData";
            cbUseTestData.Size = new Size(121, 25);
            cbUseTestData.TabIndex = 29;
            cbUseTestData.Text = "Use Test Data";
            cbUseTestData.UseVisualStyleBackColor = false;
            // 
            // txtFlowCoefficient
            // 
            txtFlowCoefficient.Font = new Font("Segoe UI", 9.75F);
            txtFlowCoefficient.Location = new Point(582, 166);
            txtFlowCoefficient.Name = "txtFlowCoefficient";
            txtFlowCoefficient.Size = new Size(116, 25);
            txtFlowCoefficient.TabIndex = 28;
            txtFlowCoefficient.Visible = false;
            // 
            // lblFlowCoefficient
            // 
            lblFlowCoefficient.AutoSize = true;
            lblFlowCoefficient.BackColor = Color.Transparent;
            lblFlowCoefficient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFlowCoefficient.Location = new Point(425, 166);
            lblFlowCoefficient.Name = "lblFlowCoefficient";
            lblFlowCoefficient.Size = new Size(124, 21);
            lblFlowCoefficient.TabIndex = 27;
            lblFlowCoefficient.Text = "Flow Coefficient:";
            lblFlowCoefficient.Visible = false;
            // 
            // txtWellExponent
            // 
            txtWellExponent.Font = new Font("Segoe UI", 9.75F);
            txtWellExponent.Location = new Point(582, 124);
            txtWellExponent.Name = "txtWellExponent";
            txtWellExponent.Size = new Size(116, 25);
            txtWellExponent.TabIndex = 26;
            txtWellExponent.Visible = false;
            // 
            // dgvTestData
            // 
            dgvTestData.AllowUserToAddRows = false;
            dgvTestData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTestData.BackgroundColor = Color.Gray;
            dgvTestData.BorderStyle = BorderStyle.None;
            dgvTestData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTestData.EnableHeadersVisualStyles = false;
            dgvTestData.GridColor = SystemColors.ControlDark;
            dgvTestData.Location = new Point(24, 28);
            dgvTestData.Name = "dgvTestData";
            dgvTestData.Size = new Size(320, 258);
            dgvTestData.TabIndex = 19;
            // 
            // lblWellExponent
            // 
            lblWellExponent.AutoSize = true;
            lblWellExponent.BackColor = Color.Transparent;
            lblWellExponent.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWellExponent.Location = new Point(425, 126);
            lblWellExponent.Name = "lblWellExponent";
            lblWellExponent.Size = new Size(134, 21);
            lblWellExponent.TabIndex = 25;
            lblWellExponent.Text = "Well Exponent (n):";
            lblWellExponent.Visible = false;
            // 
            // btnAddTestDataRow
            // 
            btnAddTestDataRow.BackColor = Color.White;
            btnAddTestDataRow.Image = (Image)resources.GetObject("btnAddTestDataRow.Image");
            btnAddTestDataRow.Location = new Point(366, 28);
            btnAddTestDataRow.Name = "btnAddTestDataRow";
            btnAddTestDataRow.Size = new Size(53, 39);
            btnAddTestDataRow.TabIndex = 20;
            btnAddTestDataRow.UseVisualStyleBackColor = false;
            // 
            // cbNewFlowEfficiencyUnit
            // 
            cbNewFlowEfficiencyUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbNewFlowEfficiencyUnit.FormattingEnabled = true;
            cbNewFlowEfficiencyUnit.Location = new Point(704, 86);
            cbNewFlowEfficiencyUnit.Name = "cbNewFlowEfficiencyUnit";
            cbNewFlowEfficiencyUnit.Size = new Size(51, 25);
            cbNewFlowEfficiencyUnit.TabIndex = 24;
            // 
            // cbTestFlowEfficiencyUnit
            // 
            cbTestFlowEfficiencyUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbTestFlowEfficiencyUnit.FormattingEnabled = true;
            cbTestFlowEfficiencyUnit.Location = new Point(704, 44);
            cbTestFlowEfficiencyUnit.Name = "cbTestFlowEfficiencyUnit";
            cbTestFlowEfficiencyUnit.Size = new Size(51, 25);
            cbTestFlowEfficiencyUnit.TabIndex = 23;
            // 
            // txtNewFlowEfficiency
            // 
            txtNewFlowEfficiency.Font = new Font("Segoe UI", 9.75F);
            txtNewFlowEfficiency.Location = new Point(582, 84);
            txtNewFlowEfficiency.Name = "txtNewFlowEfficiency";
            txtNewFlowEfficiency.Size = new Size(116, 25);
            txtNewFlowEfficiency.TabIndex = 22;
            // 
            // lblNewFlowEfficiency
            // 
            lblNewFlowEfficiency.AutoSize = true;
            lblNewFlowEfficiency.BackColor = Color.Transparent;
            lblNewFlowEfficiency.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNewFlowEfficiency.Location = new Point(425, 86);
            lblNewFlowEfficiency.Name = "lblNewFlowEfficiency";
            lblNewFlowEfficiency.Size = new Size(151, 21);
            lblNewFlowEfficiency.TabIndex = 21;
            lblNewFlowEfficiency.Text = "New Flow Efficiency:";
            // 
            // lblTestFlowEfficiency
            // 
            lblTestFlowEfficiency.AutoSize = true;
            lblTestFlowEfficiency.BackColor = Color.Transparent;
            lblTestFlowEfficiency.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTestFlowEfficiency.Location = new Point(425, 46);
            lblTestFlowEfficiency.Name = "lblTestFlowEfficiency";
            lblTestFlowEfficiency.Size = new Size(145, 21);
            lblTestFlowEfficiency.TabIndex = 18;
            lblTestFlowEfficiency.Text = "Test Flow Efficiency:";
            // 
            // txtTestFlowEfficiency
            // 
            txtTestFlowEfficiency.Font = new Font("Segoe UI", 9.75F);
            txtTestFlowEfficiency.Location = new Point(582, 44);
            txtTestFlowEfficiency.Name = "txtTestFlowEfficiency";
            txtTestFlowEfficiency.Size = new Size(116, 25);
            txtTestFlowEfficiency.TabIndex = 17;
            // 
            // gbReservoirData
            // 
            gbReservoirData.BackColor = Color.White;
            gbReservoirData.Controls.Add(tabReservoirData);
            gbReservoirData.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbReservoirData.ForeColor = Color.Black;
            gbReservoirData.Location = new Point(12, 11);
            gbReservoirData.Name = "gbReservoirData";
            gbReservoirData.Size = new Size(662, 312);
            gbReservoirData.TabIndex = 8;
            gbReservoirData.TabStop = false;
            gbReservoirData.Text = "Reservoir Data";
            // 
            // tabReservoirData
            // 
            tabReservoirData.Controls.Add(tpPresentReservoirData);
            tabReservoirData.Controls.Add(tpFuture);
            tabReservoirData.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabReservoirData.Location = new Point(3, 28);
            tabReservoirData.Name = "tabReservoirData";
            tabReservoirData.SelectedIndex = 0;
            tabReservoirData.Size = new Size(648, 268);
            tabReservoirData.TabIndex = 0;
            // 
            // tpPresentReservoirData
            // 
            tpPresentReservoirData.BackColor = Color.White;
            tpPresentReservoirData.BackgroundImageLayout = ImageLayout.None;
            tpPresentReservoirData.BorderStyle = BorderStyle.FixedSingle;
            tpPresentReservoirData.Controls.Add(txtReservoirPressure);
            tpPresentReservoirData.Controls.Add(cbOilFormationVolumeFactorUnit);
            tpPresentReservoirData.Controls.Add(cbOilViscosityUnit);
            tpPresentReservoirData.Controls.Add(cbOilRelativePermeabilityUnit);
            tpPresentReservoirData.Controls.Add(cbBubblePointPressureUnit);
            tpPresentReservoirData.Controls.Add(cbReservoirPressureUnit);
            tpPresentReservoirData.Controls.Add(lblOilFormationVolumeFactor);
            tpPresentReservoirData.Controls.Add(txtOilRelativePermeability);
            tpPresentReservoirData.Controls.Add(txtOilFormationVolumeFactor);
            tpPresentReservoirData.Controls.Add(txtOilViscosity);
            tpPresentReservoirData.Controls.Add(txtBubblePointPressure);
            tpPresentReservoirData.Controls.Add(lblOilViscosity);
            tpPresentReservoirData.Controls.Add(lblOilRelativePermeability);
            tpPresentReservoirData.Controls.Add(lblBubblePointPressure);
            tpPresentReservoirData.Controls.Add(lblReservoirPressure);
            tpPresentReservoirData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tpPresentReservoirData.ForeColor = Color.Black;
            tpPresentReservoirData.Location = new Point(4, 26);
            tpPresentReservoirData.Name = "tpPresentReservoirData";
            tpPresentReservoirData.Padding = new Padding(3);
            tpPresentReservoirData.Size = new Size(640, 238);
            tpPresentReservoirData.TabIndex = 0;
            tpPresentReservoirData.Text = "Present";
            // 
            // txtReservoirPressure
            // 
            txtReservoirPressure.Font = new Font("Segoe UI", 9.75F);
            txtReservoirPressure.ForeColor = Color.FromArgb(212, 212, 212);
            txtReservoirPressure.Location = new Point(253, 24);
            txtReservoirPressure.Name = "txtReservoirPressure";
            txtReservoirPressure.Size = new Size(166, 25);
            txtReservoirPressure.TabIndex = 9;
            // 
            // cbOilFormationVolumeFactorUnit
            // 
            cbOilFormationVolumeFactorUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbOilFormationVolumeFactorUnit.FormattingEnabled = true;
            cbOilFormationVolumeFactorUnit.Location = new Point(425, 172);
            cbOilFormationVolumeFactorUnit.Name = "cbOilFormationVolumeFactorUnit";
            cbOilFormationVolumeFactorUnit.Size = new Size(166, 25);
            cbOilFormationVolumeFactorUnit.TabIndex = 15;
            // 
            // cbOilViscosityUnit
            // 
            cbOilViscosityUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbOilViscosityUnit.FormattingEnabled = true;
            cbOilViscosityUnit.Location = new Point(425, 135);
            cbOilViscosityUnit.Name = "cbOilViscosityUnit";
            cbOilViscosityUnit.Size = new Size(166, 25);
            cbOilViscosityUnit.TabIndex = 14;
            // 
            // cbOilRelativePermeabilityUnit
            // 
            cbOilRelativePermeabilityUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbOilRelativePermeabilityUnit.FormattingEnabled = true;
            cbOilRelativePermeabilityUnit.Location = new Point(425, 98);
            cbOilRelativePermeabilityUnit.Name = "cbOilRelativePermeabilityUnit";
            cbOilRelativePermeabilityUnit.Size = new Size(166, 25);
            cbOilRelativePermeabilityUnit.TabIndex = 13;
            // 
            // cbBubblePointPressureUnit
            // 
            cbBubblePointPressureUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbBubblePointPressureUnit.FormattingEnabled = true;
            cbBubblePointPressureUnit.Location = new Point(425, 61);
            cbBubblePointPressureUnit.Name = "cbBubblePointPressureUnit";
            cbBubblePointPressureUnit.Size = new Size(166, 25);
            cbBubblePointPressureUnit.TabIndex = 12;
            // 
            // cbReservoirPressureUnit
            // 
            cbReservoirPressureUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbReservoirPressureUnit.FormattingEnabled = true;
            cbReservoirPressureUnit.Location = new Point(425, 24);
            cbReservoirPressureUnit.Name = "cbReservoirPressureUnit";
            cbReservoirPressureUnit.Size = new Size(166, 25);
            cbReservoirPressureUnit.TabIndex = 11;
            // 
            // lblOilFormationVolumeFactor
            // 
            lblOilFormationVolumeFactor.AutoSize = true;
            lblOilFormationVolumeFactor.Location = new Point(23, 174);
            lblOilFormationVolumeFactor.Name = "lblOilFormationVolumeFactor";
            lblOilFormationVolumeFactor.Size = new Size(212, 21);
            lblOilFormationVolumeFactor.TabIndex = 10;
            lblOilFormationVolumeFactor.Text = "Oil Formation Volume Factor:";
            // 
            // txtOilRelativePermeability
            // 
            txtOilRelativePermeability.Font = new Font("Segoe UI", 9.75F);
            txtOilRelativePermeability.Location = new Point(253, 98);
            txtOilRelativePermeability.Name = "txtOilRelativePermeability";
            txtOilRelativePermeability.Size = new Size(166, 25);
            txtOilRelativePermeability.TabIndex = 8;
            txtOilRelativePermeability.TextChanged += txtOilRelativePermeability_TextChanged;
            // 
            // txtOilFormationVolumeFactor
            // 
            txtOilFormationVolumeFactor.Font = new Font("Segoe UI", 9.75F);
            txtOilFormationVolumeFactor.Location = new Point(253, 172);
            txtOilFormationVolumeFactor.Name = "txtOilFormationVolumeFactor";
            txtOilFormationVolumeFactor.Size = new Size(166, 25);
            txtOilFormationVolumeFactor.TabIndex = 7;
            // 
            // txtOilViscosity
            // 
            txtOilViscosity.Font = new Font("Segoe UI", 9.75F);
            txtOilViscosity.Location = new Point(253, 135);
            txtOilViscosity.Name = "txtOilViscosity";
            txtOilViscosity.Size = new Size(166, 25);
            txtOilViscosity.TabIndex = 6;
            // 
            // txtBubblePointPressure
            // 
            txtBubblePointPressure.BackColor = Color.White;
            txtBubblePointPressure.Font = new Font("Segoe UI", 9.75F);
            txtBubblePointPressure.Location = new Point(253, 61);
            txtBubblePointPressure.Name = "txtBubblePointPressure";
            txtBubblePointPressure.Size = new Size(166, 25);
            txtBubblePointPressure.TabIndex = 5;
            // 
            // lblOilViscosity
            // 
            lblOilViscosity.AutoSize = true;
            lblOilViscosity.Location = new Point(23, 137);
            lblOilViscosity.Name = "lblOilViscosity";
            lblOilViscosity.Size = new Size(98, 21);
            lblOilViscosity.TabIndex = 3;
            lblOilViscosity.Text = "Oil Viscosity:";
            // 
            // lblOilRelativePermeability
            // 
            lblOilRelativePermeability.AutoSize = true;
            lblOilRelativePermeability.Location = new Point(23, 100);
            lblOilRelativePermeability.Name = "lblOilRelativePermeability";
            lblOilRelativePermeability.Size = new Size(182, 21);
            lblOilRelativePermeability.TabIndex = 2;
            lblOilRelativePermeability.Text = "Oil Relative Permeability:";
            // 
            // lblBubblePointPressure
            // 
            lblBubblePointPressure.AutoSize = true;
            lblBubblePointPressure.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBubblePointPressure.Location = new Point(23, 63);
            lblBubblePointPressure.Name = "lblBubblePointPressure";
            lblBubblePointPressure.Size = new Size(164, 21);
            lblBubblePointPressure.TabIndex = 1;
            lblBubblePointPressure.Text = "Bubble Point Pressure:";
            // 
            // lblReservoirPressure
            // 
            lblReservoirPressure.AutoSize = true;
            lblReservoirPressure.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReservoirPressure.ForeColor = Color.Black;
            lblReservoirPressure.Location = new Point(23, 26);
            lblReservoirPressure.Name = "lblReservoirPressure";
            lblReservoirPressure.Size = new Size(143, 21);
            lblReservoirPressure.TabIndex = 0;
            lblReservoirPressure.Text = "Reservoir Pressure:";
            // 
            // tpFuture
            // 
            tpFuture.BackColor = Color.White;
            tpFuture.Controls.Add(cbFutureOilFormationVolumeFactorUnit);
            tpFuture.Controls.Add(cbFutureOilViscosityUnit);
            tpFuture.Controls.Add(cbFutureOilRelativePermeabilityUnit);
            tpFuture.Controls.Add(cbFutureReservoiPressureUnit);
            tpFuture.Controls.Add(txtFutureOilViscosity);
            tpFuture.Controls.Add(txtFutureOilFormationVolumeFactor);
            tpFuture.Controls.Add(txtFutureOilRelativePermeability);
            tpFuture.Controls.Add(txtFutureReservoirPressure);
            tpFuture.Controls.Add(lblFutureOilFormationVolumeFactor);
            tpFuture.Controls.Add(lblFutureOilRelativePermeability);
            tpFuture.Controls.Add(lblFutureOilViscosity);
            tpFuture.Controls.Add(lblFutureReservoirPressure);
            tpFuture.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tpFuture.Location = new Point(4, 26);
            tpFuture.Name = "tpFuture";
            tpFuture.Padding = new Padding(3);
            tpFuture.Size = new Size(640, 238);
            tpFuture.TabIndex = 1;
            tpFuture.Text = "Future";
            // 
            // cbFutureOilFormationVolumeFactorUnit
            // 
            cbFutureOilFormationVolumeFactorUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbFutureOilFormationVolumeFactorUnit.FormattingEnabled = true;
            cbFutureOilFormationVolumeFactorUnit.Location = new Point(331, 132);
            cbFutureOilFormationVolumeFactorUnit.Name = "cbFutureOilFormationVolumeFactorUnit";
            cbFutureOilFormationVolumeFactorUnit.Size = new Size(51, 25);
            cbFutureOilFormationVolumeFactorUnit.TabIndex = 18;
            // 
            // cbFutureOilViscosityUnit
            // 
            cbFutureOilViscosityUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbFutureOilViscosityUnit.FormattingEnabled = true;
            cbFutureOilViscosityUnit.Location = new Point(331, 94);
            cbFutureOilViscosityUnit.Name = "cbFutureOilViscosityUnit";
            cbFutureOilViscosityUnit.Size = new Size(51, 25);
            cbFutureOilViscosityUnit.TabIndex = 17;
            // 
            // cbFutureOilRelativePermeabilityUnit
            // 
            cbFutureOilRelativePermeabilityUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbFutureOilRelativePermeabilityUnit.FormattingEnabled = true;
            cbFutureOilRelativePermeabilityUnit.Location = new Point(331, 56);
            cbFutureOilRelativePermeabilityUnit.Name = "cbFutureOilRelativePermeabilityUnit";
            cbFutureOilRelativePermeabilityUnit.Size = new Size(51, 25);
            cbFutureOilRelativePermeabilityUnit.TabIndex = 16;
            // 
            // cbFutureReservoiPressureUnit
            // 
            cbFutureReservoiPressureUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbFutureReservoiPressureUnit.FormattingEnabled = true;
            cbFutureReservoiPressureUnit.Location = new Point(331, 18);
            cbFutureReservoiPressureUnit.Name = "cbFutureReservoiPressureUnit";
            cbFutureReservoiPressureUnit.Size = new Size(51, 25);
            cbFutureReservoiPressureUnit.TabIndex = 15;
            // 
            // txtFutureOilViscosity
            // 
            txtFutureOilViscosity.Font = new Font("Segoe UI", 9.75F);
            txtFutureOilViscosity.Location = new Point(235, 94);
            txtFutureOilViscosity.Name = "txtFutureOilViscosity";
            txtFutureOilViscosity.Size = new Size(90, 25);
            txtFutureOilViscosity.TabIndex = 7;
            // 
            // txtFutureOilFormationVolumeFactor
            // 
            txtFutureOilFormationVolumeFactor.Font = new Font("Segoe UI", 9.75F);
            txtFutureOilFormationVolumeFactor.Location = new Point(235, 132);
            txtFutureOilFormationVolumeFactor.Name = "txtFutureOilFormationVolumeFactor";
            txtFutureOilFormationVolumeFactor.Size = new Size(90, 25);
            txtFutureOilFormationVolumeFactor.TabIndex = 6;
            // 
            // txtFutureOilRelativePermeability
            // 
            txtFutureOilRelativePermeability.Font = new Font("Segoe UI", 9.75F);
            txtFutureOilRelativePermeability.Location = new Point(235, 56);
            txtFutureOilRelativePermeability.Name = "txtFutureOilRelativePermeability";
            txtFutureOilRelativePermeability.Size = new Size(90, 25);
            txtFutureOilRelativePermeability.TabIndex = 5;
            // 
            // txtFutureReservoirPressure
            // 
            txtFutureReservoirPressure.Font = new Font("Segoe UI", 9.75F);
            txtFutureReservoirPressure.Location = new Point(235, 18);
            txtFutureReservoirPressure.Name = "txtFutureReservoirPressure";
            txtFutureReservoirPressure.Size = new Size(90, 25);
            txtFutureReservoirPressure.TabIndex = 4;
            // 
            // lblFutureOilFormationVolumeFactor
            // 
            lblFutureOilFormationVolumeFactor.AutoSize = true;
            lblFutureOilFormationVolumeFactor.Location = new Point(23, 140);
            lblFutureOilFormationVolumeFactor.Name = "lblFutureOilFormationVolumeFactor";
            lblFutureOilFormationVolumeFactor.Size = new Size(212, 21);
            lblFutureOilFormationVolumeFactor.TabIndex = 3;
            lblFutureOilFormationVolumeFactor.Text = "Oil Formation Volume Factor:";
            // 
            // lblFutureOilRelativePermeability
            // 
            lblFutureOilRelativePermeability.AutoSize = true;
            lblFutureOilRelativePermeability.Location = new Point(23, 64);
            lblFutureOilRelativePermeability.Name = "lblFutureOilRelativePermeability";
            lblFutureOilRelativePermeability.Size = new Size(182, 21);
            lblFutureOilRelativePermeability.TabIndex = 2;
            lblFutureOilRelativePermeability.Text = "Oil Relative Permeability:";
            // 
            // lblFutureOilViscosity
            // 
            lblFutureOilViscosity.AutoSize = true;
            lblFutureOilViscosity.Location = new Point(23, 102);
            lblFutureOilViscosity.Name = "lblFutureOilViscosity";
            lblFutureOilViscosity.Size = new Size(98, 21);
            lblFutureOilViscosity.TabIndex = 1;
            lblFutureOilViscosity.Text = "Oil Viscosity:";
            // 
            // lblFutureReservoirPressure
            // 
            lblFutureReservoirPressure.AutoSize = true;
            lblFutureReservoirPressure.Location = new Point(23, 26);
            lblFutureReservoirPressure.Name = "lblFutureReservoirPressure";
            lblFutureReservoirPressure.Size = new Size(143, 21);
            lblFutureReservoirPressure.TabIndex = 0;
            lblFutureReservoirPressure.Text = "Reservoir Pressure:";
            // 
            // tpVLP
            // 
            tpVLP.BackColor = Color.FromArgb(245, 247, 251);
            tpVLP.Location = new Point(4, 29);
            tpVLP.Name = "tpVLP";
            tpVLP.Padding = new Padding(3);
            tpVLP.Size = new Size(1537, 721);
            tpVLP.TabIndex = 1;
            tpVLP.Text = "VLP";
            // 
            // tabResult
            // 
            tabResult.BackColor = Color.FromArgb(245, 247, 251);
            tabResult.Controls.Add(pnlPlot);
            tabResult.Location = new Point(4, 29);
            tabResult.Name = "tabResult";
            tabResult.Padding = new Padding(3);
            tabResult.Size = new Size(1537, 721);
            tabResult.TabIndex = 2;
            tabResult.Text = "Results";
            // 
            // pnlPlot
            // 
            pnlPlot.BackColor = Color.FromArgb(245, 247, 251);
            pnlPlot.Controls.Add(pnlFormatPlot);
            pnlPlot.Controls.Add(pltNodalAnalysis);
            pnlPlot.Controls.Add(btnFomatPlot);
            pnlPlot.Dock = DockStyle.Right;
            pnlPlot.Location = new Point(537, 3);
            pnlPlot.Name = "pnlPlot";
            pnlPlot.Size = new Size(997, 715);
            pnlPlot.TabIndex = 23;
            // 
            // pnlFormatPlot
            // 
            pnlFormatPlot.AutoScroll = true;
            pnlFormatPlot.BackColor = Color.FromArgb(245, 247, 251);
            pnlFormatPlot.BorderStyle = BorderStyle.FixedSingle;
            pnlFormatPlot.Controls.Add(gbCurveGenerationSettings);
            pnlFormatPlot.Controls.Add(gbCurveSetting);
            pnlFormatPlot.Controls.Add(gbPlotSetting);
            pnlFormatPlot.Controls.Add(pnlFormatPlotHeader);
            pnlFormatPlot.Dock = DockStyle.Right;
            pnlFormatPlot.ForeColor = SystemColors.ActiveCaptionText;
            pnlFormatPlot.Location = new Point(666, 0);
            pnlFormatPlot.Name = "pnlFormatPlot";
            pnlFormatPlot.Size = new Size(325, 715);
            pnlFormatPlot.TabIndex = 0;
            pnlFormatPlot.Visible = false;
            // 
            // gbCurveGenerationSettings
            // 
            gbCurveGenerationSettings.Controls.Add(nudMinimumPressure);
            gbCurveGenerationSettings.Controls.Add(nudPressureStepSize);
            gbCurveGenerationSettings.Controls.Add(label1);
            gbCurveGenerationSettings.Controls.Add(lblPressureStepSize);
            gbCurveGenerationSettings.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbCurveGenerationSettings.Location = new Point(6, 600);
            gbCurveGenerationSettings.Name = "gbCurveGenerationSettings";
            gbCurveGenerationSettings.Size = new Size(311, 119);
            gbCurveGenerationSettings.TabIndex = 25;
            gbCurveGenerationSettings.TabStop = false;
            gbCurveGenerationSettings.Text = "Curve Generation Settings";
            // 
            // nudMinimumPressure
            // 
            nudMinimumPressure.Font = new Font("Segoe UI", 9.75F);
            nudMinimumPressure.Location = new Point(141, 74);
            nudMinimumPressure.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudMinimumPressure.Name = "nudMinimumPressure";
            nudMinimumPressure.Size = new Size(165, 25);
            nudMinimumPressure.TabIndex = 27;
            // 
            // nudPressureStepSize
            // 
            nudPressureStepSize.Font = new Font("Segoe UI", 9.75F);
            nudPressureStepSize.Location = new Point(141, 40);
            nudPressureStepSize.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPressureStepSize.Name = "nudPressureStepSize";
            nudPressureStepSize.Size = new Size(165, 25);
            nudPressureStepSize.TabIndex = 26;
            nudPressureStepSize.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F);
            label1.Location = new Point(17, 78);
            label1.Name = "label1";
            label1.Size = new Size(119, 17);
            label1.TabIndex = 25;
            label1.Text = "Minimum Pressure:";
            // 
            // lblPressureStepSize
            // 
            lblPressureStepSize.AutoSize = true;
            lblPressureStepSize.Font = new Font("Segoe UI", 9.75F);
            lblPressureStepSize.Location = new Point(17, 44);
            lblPressureStepSize.Name = "lblPressureStepSize";
            lblPressureStepSize.Size = new Size(118, 17);
            lblPressureStepSize.TabIndex = 23;
            lblPressureStepSize.Text = "Pressure Setp Size:";
            // 
            // gbCurveSetting
            // 
            gbCurveSetting.Controls.Add(lblMarkerShape);
            gbCurveSetting.Controls.Add(cbMarkerShape);
            gbCurveSetting.Controls.Add(lblLinePattern);
            gbCurveSetting.Controls.Add(lblPlotLineWidth);
            gbCurveSetting.Controls.Add(nudLineWidth);
            gbCurveSetting.Controls.Add(lblPlotMarkerSize);
            gbCurveSetting.Controls.Add(cbLinePattern);
            gbCurveSetting.Controls.Add(txtLegendText);
            gbCurveSetting.Controls.Add(btnChooseColor);
            gbCurveSetting.Controls.Add(nudMarkerSize);
            gbCurveSetting.Controls.Add(lblPlotLineColor);
            gbCurveSetting.Controls.Add(lblPlotLegendText);
            gbCurveSetting.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbCurveSetting.Location = new Point(6, 262);
            gbCurveSetting.Name = "gbCurveSetting";
            gbCurveSetting.Size = new Size(311, 332);
            gbCurveSetting.TabIndex = 24;
            gbCurveSetting.TabStop = false;
            gbCurveSetting.Text = "Curve Setting";
            // 
            // lblMarkerShape
            // 
            lblMarkerShape.AutoSize = true;
            lblMarkerShape.Font = new Font("Segoe UI", 9.75F);
            lblMarkerShape.Location = new Point(17, 262);
            lblMarkerShape.Name = "lblMarkerShape";
            lblMarkerShape.Size = new Size(90, 17);
            lblMarkerShape.TabIndex = 24;
            lblMarkerShape.Text = "Marker Shape";
            // 
            // cbMarkerShape
            // 
            cbMarkerShape.Font = new Font("Segoe UI", 9.75F);
            cbMarkerShape.FormattingEnabled = true;
            cbMarkerShape.Location = new Point(127, 258);
            cbMarkerShape.Name = "cbMarkerShape";
            cbMarkerShape.Size = new Size(171, 25);
            cbMarkerShape.TabIndex = 23;
            // 
            // lblLinePattern
            // 
            lblLinePattern.AutoSize = true;
            lblLinePattern.Font = new Font("Segoe UI", 9.75F);
            lblLinePattern.Location = new Point(17, 220);
            lblLinePattern.Name = "lblLinePattern";
            lblLinePattern.Size = new Size(79, 17);
            lblLinePattern.TabIndex = 22;
            lblLinePattern.Text = "Line Pattern:";
            // 
            // lblPlotLineWidth
            // 
            lblPlotLineWidth.AutoSize = true;
            lblPlotLineWidth.Font = new Font("Segoe UI", 9.75F);
            lblPlotLineWidth.Location = new Point(17, 52);
            lblPlotLineWidth.Name = "lblPlotLineWidth";
            lblPlotLineWidth.Size = new Size(69, 17);
            lblPlotLineWidth.TabIndex = 14;
            lblPlotLineWidth.Text = "lIne Width:";
            // 
            // nudLineWidth
            // 
            nudLineWidth.Font = new Font("Segoe UI", 9.75F);
            nudLineWidth.Location = new Point(111, 42);
            nudLineWidth.Name = "nudLineWidth";
            nudLineWidth.Size = new Size(150, 25);
            nudLineWidth.TabIndex = 13;
            nudLineWidth.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblPlotMarkerSize
            // 
            lblPlotMarkerSize.AutoSize = true;
            lblPlotMarkerSize.Font = new Font("Segoe UI", 9.75F);
            lblPlotMarkerSize.Location = new Point(17, 94);
            lblPlotMarkerSize.Name = "lblPlotMarkerSize";
            lblPlotMarkerSize.Size = new Size(80, 17);
            lblPlotMarkerSize.TabIndex = 20;
            lblPlotMarkerSize.Text = "Marker Size:";
            // 
            // cbLinePattern
            // 
            cbLinePattern.Font = new Font("Segoe UI", 9.75F);
            cbLinePattern.FormattingEnabled = true;
            cbLinePattern.Location = new Point(127, 215);
            cbLinePattern.Name = "cbLinePattern";
            cbLinePattern.Size = new Size(171, 25);
            cbLinePattern.TabIndex = 17;
            // 
            // txtLegendText
            // 
            txtLegendText.Font = new Font("Segoe UI", 9.75F);
            txtLegendText.Location = new Point(127, 131);
            txtLegendText.Name = "txtLegendText";
            txtLegendText.Size = new Size(171, 25);
            txtLegendText.TabIndex = 19;
            txtLegendText.Text = "IPR";
            // 
            // btnChooseColor
            // 
            btnChooseColor.Font = new Font("Segoe UI", 9.75F);
            btnChooseColor.Location = new Point(127, 173);
            btnChooseColor.Name = "btnChooseColor";
            btnChooseColor.Size = new Size(171, 28);
            btnChooseColor.TabIndex = 16;
            btnChooseColor.Text = "Choose Color";
            btnChooseColor.UseVisualStyleBackColor = true;
            // 
            // nudMarkerSize
            // 
            nudMarkerSize.Font = new Font("Segoe UI", 9.75F);
            nudMarkerSize.Location = new Point(111, 79);
            nudMarkerSize.Name = "nudMarkerSize";
            nudMarkerSize.Size = new Size(150, 25);
            nudMarkerSize.TabIndex = 21;
            // 
            // lblPlotLineColor
            // 
            lblPlotLineColor.AutoSize = true;
            lblPlotLineColor.Font = new Font("Segoe UI", 9.75F);
            lblPlotLineColor.Location = new Point(17, 178);
            lblPlotLineColor.Name = "lblPlotLineColor";
            lblPlotLineColor.Size = new Size(70, 17);
            lblPlotLineColor.TabIndex = 15;
            lblPlotLineColor.Text = "Line Color:";
            // 
            // lblPlotLegendText
            // 
            lblPlotLegendText.AutoSize = true;
            lblPlotLegendText.Font = new Font("Segoe UI", 9.75F);
            lblPlotLegendText.Location = new Point(17, 136);
            lblPlotLegendText.Name = "lblPlotLegendText";
            lblPlotLegendText.Size = new Size(81, 17);
            lblPlotLegendText.TabIndex = 18;
            lblPlotLegendText.Text = "Legend Text:";
            // 
            // gbPlotSetting
            // 
            gbPlotSetting.Controls.Add(lblPlotTitleText);
            gbPlotSetting.Controls.Add(txtPlotTitle);
            gbPlotSetting.Controls.Add(lblPlotXAxisLabel);
            gbPlotSetting.Controls.Add(lblPlotYAxisLabel);
            gbPlotSetting.Controls.Add(txtXlabel);
            gbPlotSetting.Controls.Add(txtylabel);
            gbPlotSetting.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbPlotSetting.Location = new Point(6, 91);
            gbPlotSetting.Name = "gbPlotSetting";
            gbPlotSetting.Size = new Size(311, 164);
            gbPlotSetting.TabIndex = 23;
            gbPlotSetting.TabStop = false;
            gbPlotSetting.Text = "Plot Setting";
            // 
            // lblPlotTitleText
            // 
            lblPlotTitleText.AutoSize = true;
            lblPlotTitleText.Font = new Font("Segoe UI", 9.75F);
            lblPlotTitleText.Location = new Point(7, 36);
            lblPlotTitleText.Name = "lblPlotTitleText";
            lblPlotTitleText.Size = new Size(62, 17);
            lblPlotTitleText.TabIndex = 7;
            lblPlotTitleText.Text = "Title Text:";
            // 
            // txtPlotTitle
            // 
            txtPlotTitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPlotTitle.Location = new Point(106, 33);
            txtPlotTitle.Name = "txtPlotTitle";
            txtPlotTitle.Size = new Size(197, 22);
            txtPlotTitle.TabIndex = 8;
            txtPlotTitle.Text = "Oil Well Preformace";
            // 
            // lblPlotXAxisLabel
            // 
            lblPlotXAxisLabel.AutoSize = true;
            lblPlotXAxisLabel.Font = new Font("Segoe UI", 9.75F);
            lblPlotXAxisLabel.Location = new Point(7, 71);
            lblPlotXAxisLabel.Name = "lblPlotXAxisLabel";
            lblPlotXAxisLabel.Size = new Size(81, 17);
            lblPlotXAxisLabel.TabIndex = 9;
            lblPlotXAxisLabel.Text = "X-axis Label:";
            // 
            // lblPlotYAxisLabel
            // 
            lblPlotYAxisLabel.AutoSize = true;
            lblPlotYAxisLabel.Font = new Font("Segoe UI", 9.75F);
            lblPlotYAxisLabel.Location = new Point(7, 107);
            lblPlotYAxisLabel.Name = "lblPlotYAxisLabel";
            lblPlotYAxisLabel.Size = new Size(80, 17);
            lblPlotYAxisLabel.TabIndex = 10;
            lblPlotYAxisLabel.Text = "Y-axis Label:";
            // 
            // txtXlabel
            // 
            txtXlabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtXlabel.Location = new Point(106, 66);
            txtXlabel.Name = "txtXlabel";
            txtXlabel.Size = new Size(197, 22);
            txtXlabel.TabIndex = 11;
            txtXlabel.Text = "Flow Rate";
            // 
            // txtylabel
            // 
            txtylabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtylabel.Location = new Point(106, 103);
            txtylabel.Name = "txtylabel";
            txtylabel.Size = new Size(197, 22);
            txtylabel.TabIndex = 12;
            txtylabel.Text = "Pressure";
            // 
            // pnlFormatPlotHeader
            // 
            pnlFormatPlotHeader.BackColor = Color.FromArgb(86, 156, 214);
            pnlFormatPlotHeader.Controls.Add(lblFormatPlotTitle);
            pnlFormatPlotHeader.Dock = DockStyle.Top;
            pnlFormatPlotHeader.Location = new Point(0, 0);
            pnlFormatPlotHeader.Name = "pnlFormatPlotHeader";
            pnlFormatPlotHeader.Size = new Size(317, 84);
            pnlFormatPlotHeader.TabIndex = 22;
            // 
            // lblFormatPlotTitle
            // 
            lblFormatPlotTitle.AutoSize = true;
            lblFormatPlotTitle.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFormatPlotTitle.Location = new Point(59, 19);
            lblFormatPlotTitle.Name = "lblFormatPlotTitle";
            lblFormatPlotTitle.Size = new Size(179, 40);
            lblFormatPlotTitle.TabIndex = 0;
            lblFormatPlotTitle.Text = "Format Plot";
            // 
            // pltNodalAnalysis
            // 
            pltNodalAnalysis.BackColor = Color.FromArgb(245, 247, 251);
            pltNodalAnalysis.Font = new Font("Akhbar MT", 12F, FontStyle.Regular, GraphicsUnit.Point, 178);
            pltNodalAnalysis.ForeColor = SystemColors.ActiveCaption;
            pltNodalAnalysis.Location = new Point(26, 20);
            pltNodalAnalysis.Name = "pltNodalAnalysis";
            pltNodalAnalysis.Size = new Size(934, 676);
            pltNodalAnalysis.TabIndex = 4;
            // 
            // btnFomatPlot
            // 
            btnFomatPlot.Dock = DockStyle.Right;
            btnFomatPlot.Location = new Point(991, 0);
            btnFomatPlot.Name = "btnFomatPlot";
            btnFomatPlot.Size = new Size(6, 715);
            btnFomatPlot.TabIndex = 1;
            btnFomatPlot.UseVisualStyleBackColor = true;
            btnFomatPlot.Click += btnFomatPlot_Click;
            btnFomatPlot.MouseEnter += btnFomatPlot_MouseEnter;
            btnFomatPlot.MouseLeave += btnFomatPlot_MouseLeave;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(86, 156, 214);
            pnlMain.Controls.Add(tabMian);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1545, 754);
            pnlMain.TabIndex = 1;
            // 
            // frmNodalAnalysis
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 251);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1545, 776);
            Controls.Add(pnlMain);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(31, 41, 55);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmNodalAnalysis";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nodal Analysis";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabMian.ResumeLayout(false);
            tpIPR.ResumeLayout(false);
            panel1.ResumeLayout(false);
            gbGeneralControl.ResumeLayout(false);
            gbGeneralControl.PerformLayout();
            pnlSelectMethod.ResumeLayout(false);
            pnlSelectMethod.PerformLayout();
            pnlIPRScenario.ResumeLayout(false);
            pnlIPRScenario.PerformLayout();
            pnlWellType.ResumeLayout(false);
            pnlWellType.PerformLayout();
            gbTestData.ResumeLayout(false);
            gbTestData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTestData).EndInit();
            gbReservoirData.ResumeLayout(false);
            tabReservoirData.ResumeLayout(false);
            tpPresentReservoirData.ResumeLayout(false);
            tpPresentReservoirData.PerformLayout();
            tpFuture.ResumeLayout(false);
            tpFuture.PerformLayout();
            tabResult.ResumeLayout(false);
            pnlPlot.ResumeLayout(false);
            pnlFormatPlot.ResumeLayout(false);
            gbCurveGenerationSettings.ResumeLayout(false);
            gbCurveGenerationSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinimumPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPressureStepSize).EndInit();
            gbCurveSetting.ResumeLayout(false);
            gbCurveSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLineWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMarkerSize).EndInit();
            gbPlotSetting.ResumeLayout(false);
            gbPlotSetting.PerformLayout();
            pnlFormatPlotHeader.ResumeLayout(false);
            pnlFormatPlotHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ColorDialog colorDialog1;
        private RadioButton Future;
        private StatusStrip statusStrip1;
        private TabControl tabMian;
        private TabPage tpIPR;
        private Panel panel1;
        private GroupBox gbReservoirData;
        private TabControl tabReservoirData;
        private TabPage tpPresentReservoirData;
        private ComboBox cbOilFormationVolumeFactorUnit;
        private ComboBox cbOilViscosityUnit;
        private ComboBox cbOilRelativePermeabilityUnit;
        private ComboBox cbBubblePointPressureUnit;
        private ComboBox cbReservoirPressureUnit;
        private Label lblOilFormationVolumeFactor;
        private TextBox txtReservoirPressure;
        private TextBox txtOilRelativePermeability;
        private TextBox txtOilFormationVolumeFactor;
        private TextBox txtOilViscosity;
        private TextBox txtBubblePointPressure;
        private Label lblOilViscosity;
        private Label lblOilRelativePermeability;
        private Label lblBubblePointPressure;
        private Label lblReservoirPressure;
        private TabPage tpFuture;
        private ComboBox cbFutureOilFormationVolumeFactorUnit;
        private ComboBox cbFutureOilViscosityUnit;
        private ComboBox cbFutureOilRelativePermeabilityUnit;
        private ComboBox cbFutureReservoiPressureUnit;
        private TextBox txtFutureOilViscosity;
        private TextBox txtFutureOilFormationVolumeFactor;
        private TextBox txtFutureOilRelativePermeability;
        private TextBox txtFutureReservoirPressure;
        private Label lblFutureOilFormationVolumeFactor;
        private Label lblFutureOilRelativePermeability;
        private Label lblFutureOilViscosity;
        private Label lblFutureReservoirPressure;
        private GroupBox gbGeneralControl;
        private ComboBox cbSelectCurve;
        private Label lblSelectCurve;
        private Panel pnlSelectMethod;
        private RadioButton rdoJones;
        private RadioButton rdoFetkovich;
        private RadioButton rdoStandingMethod;
        private RadioButton rdoVogelMethod;
        private Label lblSelectMethod;
        private Panel pnlIPRScenario;
        private Label lblIPRScenario;
        private RadioButton rdoFutureIPR;
        private RadioButton rdoCurrentIPR;
        private Panel pnlWellType;
        private RadioButton rdoGasWell;
        private RadioButton rdoOilWell;
        private Label lblWellType;
        private TabPage tpVLP;
        private Panel pnlMain;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TabPage tabResult;
        private Panel pnlPlot;
        private Button btnFomatPlot;
        private ScottPlot.WinForms.FormsPlot pltNodalAnalysis;
        private Panel pnlFormatPlot;
        private GroupBox gbCurveGenerationSettings;
        private NumericUpDown nudMinimumPressure;
        private NumericUpDown nudPressureStepSize;
        private Label label1;
        private Label lblPressureStepSize;
        private GroupBox gbCurveSetting;
        private Label lblMarkerShape;
        private ComboBox cbMarkerShape;
        private Label lblLinePattern;
        private Label lblPlotLineWidth;
        private NumericUpDown nudLineWidth;
        private Label lblPlotMarkerSize;
        private ComboBox cbLinePattern;
        private TextBox txtLegendText;
        private Button btnChooseColor;
        private NumericUpDown nudMarkerSize;
        private Label lblPlotLineColor;
        private Label lblPlotLegendText;
        private GroupBox gbPlotSetting;
        private Label lblPlotTitleText;
        private TextBox txtPlotTitle;
        private Label lblPlotXAxisLabel;
        private Label lblPlotYAxisLabel;
        private TextBox txtXlabel;
        private TextBox txtylabel;
        private Panel pnlFormatPlotHeader;
        private Label lblFormatPlotTitle;
        private GroupBox gbTestData;
        private CheckBox cbUseTestData;
        private TextBox txtFlowCoefficient;
        private Label lblFlowCoefficient;
        private TextBox txtWellExponent;
        private DataGridView dgvTestData;
        private Label lblWellExponent;
        private Button btnAddTestDataRow;
        private ComboBox cbNewFlowEfficiencyUnit;
        private ComboBox cbTestFlowEfficiencyUnit;
        private TextBox txtNewFlowEfficiency;
        private Label lblNewFlowEfficiency;
        private Label lblTestFlowEfficiency;
        private TextBox txtTestFlowEfficiency;
    }
}
