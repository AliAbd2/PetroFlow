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
            pnlPlot = new Panel();
            pltNodalAnalysis = new ScottPlot.WinForms.FormsPlot();
            btnFomatPlot = new Button();
            pnlFormatPlot = new Panel();
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
            colorDialog1 = new ColorDialog();
            vsbNodalAnalysisData = new VScrollBar();
            gbIPR = new GroupBox();
            gbTestData = new GroupBox();
            dgvTestData = new DataGridView();
            dgvcTestFlowRate = new DataGridViewTextBoxColumn();
            dgvcTestBottomHolePressure = new DataGridViewTextBoxColumn();
            lblTestFlowEfficiency = new Label();
            txtTestFlowEfficiency = new TextBox();
            gbReservoirData = new GroupBox();
            tabReservoirData = new TabControl();
            tpPresentReservoirData = new TabPage();
            lblOilFormationVolumeFactor = new Label();
            txtReservoirPressure = new TextBox();
            txtOilRelativePermeability = new TextBox();
            txtOilFormationVolumeFactor = new TextBox();
            txtOilViscosity = new TextBox();
            txtBubblePointPressure = new TextBox();
            lblOilViscosity = new Label();
            lblOilRelativePermeability = new Label();
            lblBubblePointPressure = new Label();
            lblReservoirPressure = new Label();
            tpFuture = new TabPage();
            txtFutureOilViscosity = new TextBox();
            txtFutureOilFormationVolumeFactor = new TextBox();
            txtFutureOilRelativePermeability = new TextBox();
            txtFutureReservoirPressure = new TextBox();
            lblFutureOilFormationVolumeFactor = new Label();
            lblFutureOilRelativePermeability = new Label();
            lblFutureOilViscosity = new Label();
            lblFutureReservoirPressure = new Label();
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
            pnlFormControl = new Panel();
            btnReset = new Button();
            btnPlot = new Button();
            pnlMain = new Panel();
            pnlPlot.SuspendLayout();
            pnlFormatPlot.SuspendLayout();
            gbCurveSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLineWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMarkerSize).BeginInit();
            gbPlotSetting.SuspendLayout();
            pnlFormatPlotHeader.SuspendLayout();
            gbIPR.SuspendLayout();
            gbTestData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTestData).BeginInit();
            gbReservoirData.SuspendLayout();
            tabReservoirData.SuspendLayout();
            tpPresentReservoirData.SuspendLayout();
            tpFuture.SuspendLayout();
            gbGeneralControl.SuspendLayout();
            pnlSelectMethod.SuspendLayout();
            pnlIPRScenario.SuspendLayout();
            pnlWellType.SuspendLayout();
            pnlFormControl.SuspendLayout();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPlot
            // 
            pnlPlot.Controls.Add(pltNodalAnalysis);
            pnlPlot.Controls.Add(btnFomatPlot);
            pnlPlot.Controls.Add(pnlFormatPlot);
            pnlPlot.Dock = DockStyle.Right;
            pnlPlot.Location = new Point(758, 0);
            pnlPlot.Name = "pnlPlot";
            pnlPlot.Size = new Size(700, 661);
            pnlPlot.TabIndex = 0;
            // 
            // pltNodalAnalysis
            // 
            pltNodalAnalysis.BackColor = Color.Transparent;
            pltNodalAnalysis.BorderStyle = BorderStyle.FixedSingle;
            pltNodalAnalysis.DisplayScale = 1F;
            pltNodalAnalysis.Dock = DockStyle.Fill;
            pltNodalAnalysis.Location = new Point(0, 0);
            pltNodalAnalysis.Name = "pltNodalAnalysis";
            pltNodalAnalysis.Size = new Size(410, 661);
            pltNodalAnalysis.TabIndex = 2;
            // 
            // btnFomatPlot
            // 
            btnFomatPlot.Dock = DockStyle.Right;
            btnFomatPlot.Location = new Point(410, 0);
            btnFomatPlot.Name = "btnFomatPlot";
            btnFomatPlot.Size = new Size(5, 661);
            btnFomatPlot.TabIndex = 1;
            btnFomatPlot.UseVisualStyleBackColor = true;
            btnFomatPlot.Click += btnFomatPlot_Click;
            btnFomatPlot.MouseEnter += btnFomatPlot_MouseEnter;
            btnFomatPlot.MouseLeave += btnFomatPlot_MouseLeave;
            // 
            // pnlFormatPlot
            // 
            pnlFormatPlot.BackColor = Color.Transparent;
            pnlFormatPlot.BorderStyle = BorderStyle.FixedSingle;
            pnlFormatPlot.Controls.Add(gbCurveSetting);
            pnlFormatPlot.Controls.Add(gbPlotSetting);
            pnlFormatPlot.Controls.Add(pnlFormatPlotHeader);
            pnlFormatPlot.Dock = DockStyle.Right;
            pnlFormatPlot.Location = new Point(415, 0);
            pnlFormatPlot.Name = "pnlFormatPlot";
            pnlFormatPlot.Size = new Size(285, 661);
            pnlFormatPlot.TabIndex = 0;
            pnlFormatPlot.Visible = false;
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
            gbCurveSetting.Location = new Point(5, 231);
            gbCurveSetting.Name = "gbCurveSetting";
            gbCurveSetting.Size = new Size(272, 293);
            gbCurveSetting.TabIndex = 24;
            gbCurveSetting.TabStop = false;
            gbCurveSetting.Text = "Curve Setting";
            // 
            // lblMarkerShape
            // 
            lblMarkerShape.AutoSize = true;
            lblMarkerShape.Font = new Font("Segoe UI", 9.75F);
            lblMarkerShape.Location = new Point(15, 231);
            lblMarkerShape.Name = "lblMarkerShape";
            lblMarkerShape.Size = new Size(90, 17);
            lblMarkerShape.TabIndex = 24;
            lblMarkerShape.Text = "Marker Shape";
            // 
            // cbMarkerShape
            // 
            cbMarkerShape.Font = new Font("Segoe UI", 9.75F);
            cbMarkerShape.FormattingEnabled = true;
            cbMarkerShape.Location = new Point(111, 228);
            cbMarkerShape.Name = "cbMarkerShape";
            cbMarkerShape.Size = new Size(150, 25);
            cbMarkerShape.TabIndex = 23;
            cbMarkerShape.SelectedIndexChanged += UpdatePlot;
            // 
            // lblLinePattern
            // 
            lblLinePattern.AutoSize = true;
            lblLinePattern.Font = new Font("Segoe UI", 9.75F);
            lblLinePattern.Location = new Point(15, 194);
            lblLinePattern.Name = "lblLinePattern";
            lblLinePattern.Size = new Size(79, 17);
            lblLinePattern.TabIndex = 22;
            lblLinePattern.Text = "Line Pattern:";
            // 
            // lblPlotLineWidth
            // 
            lblPlotLineWidth.AutoSize = true;
            lblPlotLineWidth.Font = new Font("Segoe UI", 9.75F);
            lblPlotLineWidth.Location = new Point(15, 46);
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
            nudLineWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudLineWidth.ValueChanged += UpdatePlot;
            // 
            // lblPlotMarkerSize
            // 
            lblPlotMarkerSize.AutoSize = true;
            lblPlotMarkerSize.Font = new Font("Segoe UI", 9.75F);
            lblPlotMarkerSize.Location = new Point(15, 83);
            lblPlotMarkerSize.Name = "lblPlotMarkerSize";
            lblPlotMarkerSize.Size = new Size(80, 17);
            lblPlotMarkerSize.TabIndex = 20;
            lblPlotMarkerSize.Text = "Marker Size:";
            // 
            // cbLinePattern
            // 
            cbLinePattern.Font = new Font("Segoe UI", 9.75F);
            cbLinePattern.FormattingEnabled = true;
            cbLinePattern.Location = new Point(111, 190);
            cbLinePattern.Name = "cbLinePattern";
            cbLinePattern.Size = new Size(150, 25);
            cbLinePattern.TabIndex = 17;
            cbLinePattern.SelectedIndexChanged += UpdatePlot;
            // 
            // txtLegendText
            // 
            txtLegendText.Font = new Font("Segoe UI", 9.75F);
            txtLegendText.Location = new Point(111, 116);
            txtLegendText.Name = "txtLegendText";
            txtLegendText.Size = new Size(150, 25);
            txtLegendText.TabIndex = 19;
            txtLegendText.Text = "IPR";
            txtLegendText.TextChanged += UpdatePlot;
            // 
            // btnChooseColor
            // 
            btnChooseColor.Font = new Font("Segoe UI", 9.75F);
            btnChooseColor.Location = new Point(111, 153);
            btnChooseColor.Name = "btnChooseColor";
            btnChooseColor.Size = new Size(150, 25);
            btnChooseColor.TabIndex = 16;
            btnChooseColor.Text = "Choose Color";
            btnChooseColor.UseVisualStyleBackColor = true;
            btnChooseColor.Click += btnChooseColor_Click;
            // 
            // nudMarkerSize
            // 
            nudMarkerSize.Font = new Font("Segoe UI", 9.75F);
            nudMarkerSize.Location = new Point(111, 79);
            nudMarkerSize.Name = "nudMarkerSize";
            nudMarkerSize.Size = new Size(150, 25);
            nudMarkerSize.TabIndex = 21;
            nudMarkerSize.ValueChanged += UpdatePlot;
            // 
            // lblPlotLineColor
            // 
            lblPlotLineColor.AutoSize = true;
            lblPlotLineColor.Font = new Font("Segoe UI", 9.75F);
            lblPlotLineColor.Location = new Point(15, 157);
            lblPlotLineColor.Name = "lblPlotLineColor";
            lblPlotLineColor.Size = new Size(70, 17);
            lblPlotLineColor.TabIndex = 15;
            lblPlotLineColor.Text = "Line Color:";
            // 
            // lblPlotLegendText
            // 
            lblPlotLegendText.AutoSize = true;
            lblPlotLegendText.Font = new Font("Segoe UI", 9.75F);
            lblPlotLegendText.Location = new Point(15, 120);
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
            gbPlotSetting.Location = new Point(5, 80);
            gbPlotSetting.Name = "gbPlotSetting";
            gbPlotSetting.Size = new Size(272, 145);
            gbPlotSetting.TabIndex = 23;
            gbPlotSetting.TabStop = false;
            gbPlotSetting.Text = "Plot Setting";
            // 
            // lblPlotTitleText
            // 
            lblPlotTitleText.AutoSize = true;
            lblPlotTitleText.Font = new Font("Segoe UI", 9.75F);
            lblPlotTitleText.Location = new Point(6, 32);
            lblPlotTitleText.Name = "lblPlotTitleText";
            lblPlotTitleText.Size = new Size(62, 17);
            lblPlotTitleText.TabIndex = 7;
            lblPlotTitleText.Text = "Title Text:";
            // 
            // txtPlotTitle
            // 
            txtPlotTitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPlotTitle.Location = new Point(93, 29);
            txtPlotTitle.Name = "txtPlotTitle";
            txtPlotTitle.Size = new Size(173, 22);
            txtPlotTitle.TabIndex = 8;
            txtPlotTitle.Text = "Oil Well Preformace";
            txtPlotTitle.TextChanged += UpdatePlotTitle;
            // 
            // lblPlotXAxisLabel
            // 
            lblPlotXAxisLabel.AutoSize = true;
            lblPlotXAxisLabel.Font = new Font("Segoe UI", 9.75F);
            lblPlotXAxisLabel.Location = new Point(6, 63);
            lblPlotXAxisLabel.Name = "lblPlotXAxisLabel";
            lblPlotXAxisLabel.Size = new Size(81, 17);
            lblPlotXAxisLabel.TabIndex = 9;
            lblPlotXAxisLabel.Text = "X-axis Label:";
            // 
            // lblPlotYAxisLabel
            // 
            lblPlotYAxisLabel.AutoSize = true;
            lblPlotYAxisLabel.Font = new Font("Segoe UI", 9.75F);
            lblPlotYAxisLabel.Location = new Point(6, 94);
            lblPlotYAxisLabel.Name = "lblPlotYAxisLabel";
            lblPlotYAxisLabel.Size = new Size(80, 17);
            lblPlotYAxisLabel.TabIndex = 10;
            lblPlotYAxisLabel.Text = "Y-axis Label:";
            // 
            // txtXlabel
            // 
            txtXlabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtXlabel.Location = new Point(93, 58);
            txtXlabel.Name = "txtXlabel";
            txtXlabel.Size = new Size(173, 22);
            txtXlabel.TabIndex = 11;
            txtXlabel.Text = "Flow Rate";
            txtXlabel.TextChanged += UpdatePlotTitle;
            // 
            // txtylabel
            // 
            txtylabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtylabel.Location = new Point(93, 91);
            txtylabel.Name = "txtylabel";
            txtylabel.Size = new Size(173, 22);
            txtylabel.TabIndex = 12;
            txtylabel.Text = "Pressure";
            txtylabel.TextChanged += UpdatePlotTitle;
            // 
            // pnlFormatPlotHeader
            // 
            pnlFormatPlotHeader.BackColor = Color.Silver;
            pnlFormatPlotHeader.Controls.Add(lblFormatPlotTitle);
            pnlFormatPlotHeader.Dock = DockStyle.Top;
            pnlFormatPlotHeader.Location = new Point(0, 0);
            pnlFormatPlotHeader.Name = "pnlFormatPlotHeader";
            pnlFormatPlotHeader.Size = new Size(283, 74);
            pnlFormatPlotHeader.TabIndex = 22;
            // 
            // lblFormatPlotTitle
            // 
            lblFormatPlotTitle.AutoSize = true;
            lblFormatPlotTitle.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFormatPlotTitle.Location = new Point(52, 17);
            lblFormatPlotTitle.Name = "lblFormatPlotTitle";
            lblFormatPlotTitle.Size = new Size(179, 40);
            lblFormatPlotTitle.TabIndex = 0;
            lblFormatPlotTitle.Text = "Format Plot";
            // 
            // vsbNodalAnalysisData
            // 
            vsbNodalAnalysisData.Dock = DockStyle.Right;
            vsbNodalAnalysisData.Location = new Point(739, 0);
            vsbNodalAnalysisData.Name = "vsbNodalAnalysisData";
            vsbNodalAnalysisData.Size = new Size(19, 661);
            vsbNodalAnalysisData.TabIndex = 13;
            // 
            // gbIPR
            // 
            gbIPR.Controls.Add(gbTestData);
            gbIPR.Controls.Add(gbReservoirData);
            gbIPR.Controls.Add(gbGeneralControl);
            gbIPR.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbIPR.Location = new Point(11, 11);
            gbIPR.Name = "gbIPR";
            gbIPR.Size = new Size(711, 525);
            gbIPR.TabIndex = 14;
            gbIPR.TabStop = false;
            gbIPR.Text = "IPR";
            // 
            // gbTestData
            // 
            gbTestData.Controls.Add(dgvTestData);
            gbTestData.Controls.Add(lblTestFlowEfficiency);
            gbTestData.Controls.Add(txtTestFlowEfficiency);
            gbTestData.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbTestData.Location = new Point(6, 319);
            gbTestData.Name = "gbTestData";
            gbTestData.Size = new Size(696, 195);
            gbTestData.TabIndex = 4;
            gbTestData.TabStop = false;
            gbTestData.Text = "Test Data";
            // 
            // dgvTestData
            // 
            dgvTestData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTestData.Columns.AddRange(new DataGridViewColumn[] { dgvcTestFlowRate, dgvcTestBottomHolePressure });
            dgvTestData.Dock = DockStyle.Right;
            dgvTestData.Location = new Point(397, 25);
            dgvTestData.Name = "dgvTestData";
            dgvTestData.Size = new Size(296, 167);
            dgvTestData.TabIndex = 19;
            // 
            // dgvcTestFlowRate
            // 
            dgvcTestFlowRate.HeaderText = "Test Flow Rate";
            dgvcTestFlowRate.Name = "dgvcTestFlowRate";
            dgvcTestFlowRate.Width = 150;
            // 
            // dgvcTestBottomHolePressure
            // 
            dgvcTestBottomHolePressure.HeaderText = "Pwf";
            dgvcTestBottomHolePressure.Name = "dgvcTestBottomHolePressure";
            // 
            // lblTestFlowEfficiency
            // 
            lblTestFlowEfficiency.AutoSize = true;
            lblTestFlowEfficiency.Location = new Point(15, 40);
            lblTestFlowEfficiency.Name = "lblTestFlowEfficiency";
            lblTestFlowEfficiency.Size = new Size(155, 21);
            lblTestFlowEfficiency.TabIndex = 18;
            lblTestFlowEfficiency.Text = "Test Flow Efficiency:";
            // 
            // txtTestFlowEfficiency
            // 
            txtTestFlowEfficiency.Font = new Font("Segoe UI", 9.75F);
            txtTestFlowEfficiency.Location = new Point(175, 38);
            txtTestFlowEfficiency.Name = "txtTestFlowEfficiency";
            txtTestFlowEfficiency.Size = new Size(79, 25);
            txtTestFlowEfficiency.TabIndex = 17;
            txtTestFlowEfficiency.KeyPress += EnforceNumericInput;
            // 
            // gbReservoirData
            // 
            gbReservoirData.Controls.Add(tabReservoirData);
            gbReservoirData.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbReservoirData.Location = new Point(6, 45);
            gbReservoirData.Name = "gbReservoirData";
            gbReservoirData.Size = new Size(388, 264);
            gbReservoirData.TabIndex = 8;
            gbReservoirData.TabStop = false;
            gbReservoirData.Text = "Reservoir Data";
            // 
            // tabReservoirData
            // 
            tabReservoirData.Controls.Add(tpPresentReservoirData);
            tabReservoirData.Controls.Add(tpFuture);
            tabReservoirData.Dock = DockStyle.Fill;
            tabReservoirData.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabReservoirData.Location = new Point(3, 25);
            tabReservoirData.Name = "tabReservoirData";
            tabReservoirData.SelectedIndex = 0;
            tabReservoirData.Size = new Size(382, 236);
            tabReservoirData.TabIndex = 0;
            // 
            // tpPresentReservoirData
            // 
            tpPresentReservoirData.BackColor = Color.Gainsboro;
            tpPresentReservoirData.Controls.Add(lblOilFormationVolumeFactor);
            tpPresentReservoirData.Controls.Add(txtReservoirPressure);
            tpPresentReservoirData.Controls.Add(txtOilRelativePermeability);
            tpPresentReservoirData.Controls.Add(txtOilFormationVolumeFactor);
            tpPresentReservoirData.Controls.Add(txtOilViscosity);
            tpPresentReservoirData.Controls.Add(txtBubblePointPressure);
            tpPresentReservoirData.Controls.Add(lblOilViscosity);
            tpPresentReservoirData.Controls.Add(lblOilRelativePermeability);
            tpPresentReservoirData.Controls.Add(lblBubblePointPressure);
            tpPresentReservoirData.Controls.Add(lblReservoirPressure);
            tpPresentReservoirData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tpPresentReservoirData.Location = new Point(4, 26);
            tpPresentReservoirData.Name = "tpPresentReservoirData";
            tpPresentReservoirData.Padding = new Padding(3);
            tpPresentReservoirData.Size = new Size(374, 206);
            tpPresentReservoirData.TabIndex = 0;
            tpPresentReservoirData.Text = "Present";
            // 
            // lblOilFormationVolumeFactor
            // 
            lblOilFormationVolumeFactor.AutoSize = true;
            lblOilFormationVolumeFactor.Location = new Point(6, 141);
            lblOilFormationVolumeFactor.Name = "lblOilFormationVolumeFactor";
            lblOilFormationVolumeFactor.Size = new Size(212, 21);
            lblOilFormationVolumeFactor.TabIndex = 10;
            lblOilFormationVolumeFactor.Text = "Oil Formation Volume Factor:";
            // 
            // txtReservoirPressure
            // 
            txtReservoirPressure.Font = new Font("Segoe UI", 9.75F);
            txtReservoirPressure.Location = new Point(224, 7);
            txtReservoirPressure.Name = "txtReservoirPressure";
            txtReservoirPressure.Size = new Size(79, 25);
            txtReservoirPressure.TabIndex = 9;
            txtReservoirPressure.KeyPress += EnforceNumericInput;
            // 
            // txtOilRelativePermeability
            // 
            txtOilRelativePermeability.Font = new Font("Segoe UI", 9.75F);
            txtOilRelativePermeability.Location = new Point(224, 73);
            txtOilRelativePermeability.Name = "txtOilRelativePermeability";
            txtOilRelativePermeability.Size = new Size(79, 25);
            txtOilRelativePermeability.TabIndex = 8;
            txtOilRelativePermeability.KeyPress += EnforceNumericInput;
            // 
            // txtOilFormationVolumeFactor
            // 
            txtOilFormationVolumeFactor.Font = new Font("Segoe UI", 9.75F);
            txtOilFormationVolumeFactor.Location = new Point(224, 139);
            txtOilFormationVolumeFactor.Name = "txtOilFormationVolumeFactor";
            txtOilFormationVolumeFactor.Size = new Size(79, 25);
            txtOilFormationVolumeFactor.TabIndex = 7;
            txtOilFormationVolumeFactor.KeyPress += EnforceNumericInput;
            // 
            // txtOilViscosity
            // 
            txtOilViscosity.Font = new Font("Segoe UI", 9.75F);
            txtOilViscosity.Location = new Point(224, 106);
            txtOilViscosity.Name = "txtOilViscosity";
            txtOilViscosity.Size = new Size(79, 25);
            txtOilViscosity.TabIndex = 6;
            txtOilViscosity.KeyPress += EnforceNumericInput;
            // 
            // txtBubblePointPressure
            // 
            txtBubblePointPressure.Font = new Font("Segoe UI", 9.75F);
            txtBubblePointPressure.Location = new Point(224, 40);
            txtBubblePointPressure.Name = "txtBubblePointPressure";
            txtBubblePointPressure.Size = new Size(79, 25);
            txtBubblePointPressure.TabIndex = 5;
            txtBubblePointPressure.KeyPress += EnforceNumericInput;
            // 
            // lblOilViscosity
            // 
            lblOilViscosity.AutoSize = true;
            lblOilViscosity.Location = new Point(6, 108);
            lblOilViscosity.Name = "lblOilViscosity";
            lblOilViscosity.Size = new Size(98, 21);
            lblOilViscosity.TabIndex = 3;
            lblOilViscosity.Text = "Oil Viscosity:";
            // 
            // lblOilRelativePermeability
            // 
            lblOilRelativePermeability.AutoSize = true;
            lblOilRelativePermeability.Location = new Point(6, 75);
            lblOilRelativePermeability.Name = "lblOilRelativePermeability";
            lblOilRelativePermeability.Size = new Size(182, 21);
            lblOilRelativePermeability.TabIndex = 2;
            lblOilRelativePermeability.Text = "Oil Relative Permeability:";
            // 
            // lblBubblePointPressure
            // 
            lblBubblePointPressure.AutoSize = true;
            lblBubblePointPressure.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBubblePointPressure.Location = new Point(6, 42);
            lblBubblePointPressure.Name = "lblBubblePointPressure";
            lblBubblePointPressure.Size = new Size(164, 21);
            lblBubblePointPressure.TabIndex = 1;
            lblBubblePointPressure.Text = "Bubble Point Pressure:";
            // 
            // lblReservoirPressure
            // 
            lblReservoirPressure.AutoSize = true;
            lblReservoirPressure.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReservoirPressure.Location = new Point(6, 9);
            lblReservoirPressure.Name = "lblReservoirPressure";
            lblReservoirPressure.Size = new Size(143, 21);
            lblReservoirPressure.TabIndex = 0;
            lblReservoirPressure.Text = "Reservoir Pressure:";
            // 
            // tpFuture
            // 
            tpFuture.BackColor = Color.Gainsboro;
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
            tpFuture.Size = new Size(374, 206);
            tpFuture.TabIndex = 1;
            tpFuture.Text = "Future";
            // 
            // txtFutureOilViscosity
            // 
            txtFutureOilViscosity.Font = new Font("Segoe UI", 9.75F);
            txtFutureOilViscosity.Location = new Point(224, 75);
            txtFutureOilViscosity.Name = "txtFutureOilViscosity";
            txtFutureOilViscosity.Size = new Size(79, 25);
            txtFutureOilViscosity.TabIndex = 7;
            txtFutureOilViscosity.KeyPress += EnforceNumericInput;
            // 
            // txtFutureOilFormationVolumeFactor
            // 
            txtFutureOilFormationVolumeFactor.Font = new Font("Segoe UI", 9.75F);
            txtFutureOilFormationVolumeFactor.Location = new Point(224, 109);
            txtFutureOilFormationVolumeFactor.Name = "txtFutureOilFormationVolumeFactor";
            txtFutureOilFormationVolumeFactor.Size = new Size(79, 25);
            txtFutureOilFormationVolumeFactor.TabIndex = 6;
            txtFutureOilFormationVolumeFactor.KeyPress += EnforceNumericInput;
            // 
            // txtFutureOilRelativePermeability
            // 
            txtFutureOilRelativePermeability.Font = new Font("Segoe UI", 9.75F);
            txtFutureOilRelativePermeability.Location = new Point(224, 41);
            txtFutureOilRelativePermeability.Name = "txtFutureOilRelativePermeability";
            txtFutureOilRelativePermeability.Size = new Size(79, 25);
            txtFutureOilRelativePermeability.TabIndex = 5;
            txtFutureOilRelativePermeability.KeyPress += EnforceNumericInput;
            // 
            // txtFutureReservoirPressure
            // 
            txtFutureReservoirPressure.Font = new Font("Segoe UI", 9.75F);
            txtFutureReservoirPressure.Location = new Point(224, 7);
            txtFutureReservoirPressure.Name = "txtFutureReservoirPressure";
            txtFutureReservoirPressure.Size = new Size(79, 25);
            txtFutureReservoirPressure.TabIndex = 4;
            txtFutureReservoirPressure.KeyPress += EnforceNumericInput;
            // 
            // lblFutureOilFormationVolumeFactor
            // 
            lblFutureOilFormationVolumeFactor.AutoSize = true;
            lblFutureOilFormationVolumeFactor.Location = new Point(6, 111);
            lblFutureOilFormationVolumeFactor.Name = "lblFutureOilFormationVolumeFactor";
            lblFutureOilFormationVolumeFactor.Size = new Size(212, 21);
            lblFutureOilFormationVolumeFactor.TabIndex = 3;
            lblFutureOilFormationVolumeFactor.Text = "Oil Formation Volume Factor:";
            // 
            // lblFutureOilRelativePermeability
            // 
            lblFutureOilRelativePermeability.AutoSize = true;
            lblFutureOilRelativePermeability.Location = new Point(6, 43);
            lblFutureOilRelativePermeability.Name = "lblFutureOilRelativePermeability";
            lblFutureOilRelativePermeability.Size = new Size(182, 21);
            lblFutureOilRelativePermeability.TabIndex = 2;
            lblFutureOilRelativePermeability.Text = "Oil Relative Permeability:";
            // 
            // lblFutureOilViscosity
            // 
            lblFutureOilViscosity.AutoSize = true;
            lblFutureOilViscosity.Location = new Point(6, 77);
            lblFutureOilViscosity.Name = "lblFutureOilViscosity";
            lblFutureOilViscosity.Size = new Size(98, 21);
            lblFutureOilViscosity.TabIndex = 1;
            lblFutureOilViscosity.Text = "Oil Viscosity:";
            // 
            // lblFutureReservoirPressure
            // 
            lblFutureReservoirPressure.AutoSize = true;
            lblFutureReservoirPressure.Location = new Point(6, 9);
            lblFutureReservoirPressure.Name = "lblFutureReservoirPressure";
            lblFutureReservoirPressure.Size = new Size(143, 21);
            lblFutureReservoirPressure.TabIndex = 0;
            lblFutureReservoirPressure.Text = "Reservoir Pressure:";
            // 
            // gbGeneralControl
            // 
            gbGeneralControl.BackColor = Color.Transparent;
            gbGeneralControl.Controls.Add(cbSelectCurve);
            gbGeneralControl.Controls.Add(lblSelectCurve);
            gbGeneralControl.Controls.Add(pnlSelectMethod);
            gbGeneralControl.Controls.Add(pnlIPRScenario);
            gbGeneralControl.Controls.Add(pnlWellType);
            gbGeneralControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbGeneralControl.Location = new Point(400, 45);
            gbGeneralControl.Name = "gbGeneralControl";
            gbGeneralControl.Size = new Size(302, 264);
            gbGeneralControl.TabIndex = 11;
            gbGeneralControl.TabStop = false;
            gbGeneralControl.Text = "General Control";
            // 
            // cbSelectCurve
            // 
            cbSelectCurve.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectCurve.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbSelectCurve.FormattingEnabled = true;
            cbSelectCurve.Location = new Point(117, 214);
            cbSelectCurve.Name = "cbSelectCurve";
            cbSelectCurve.Size = new Size(174, 25);
            cbSelectCurve.TabIndex = 17;
            cbSelectCurve.DropDown += cbSelectCurve_DropDown;
            cbSelectCurve.DropDownClosed += cbSelectCurve_DropDownClosed;
            // 
            // lblSelectCurve
            // 
            lblSelectCurve.AutoSize = true;
            lblSelectCurve.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectCurve.Location = new Point(5, 216);
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
            pnlSelectMethod.Location = new Point(3, 97);
            pnlSelectMethod.Name = "pnlSelectMethod";
            pnlSelectMethod.Size = new Size(296, 92);
            pnlSelectMethod.TabIndex = 15;
            // 
            // rdoJones
            // 
            rdoJones.AutoSize = true;
            rdoJones.Location = new Point(14, 58);
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
            rdoFetkovich.Location = new Point(182, 27);
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
            rdoStandingMethod.Location = new Point(87, 27);
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
            rdoVogelMethod.Location = new Point(14, 27);
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
            lblSelectMethod.Location = new Point(5, 3);
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
            pnlIPRScenario.Location = new Point(3, 64);
            pnlIPRScenario.Name = "pnlIPRScenario";
            pnlIPRScenario.Size = new Size(296, 33);
            pnlIPRScenario.TabIndex = 14;
            // 
            // lblIPRScenario
            // 
            lblIPRScenario.AutoSize = true;
            lblIPRScenario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIPRScenario.Location = new Point(5, 3);
            lblIPRScenario.Name = "lblIPRScenario";
            lblIPRScenario.Size = new Size(105, 21);
            lblIPRScenario.TabIndex = 11;
            lblIPRScenario.Text = "IPR Scenario:";
            // 
            // rdoFutureIPR
            // 
            rdoFutureIPR.AutoSize = true;
            rdoFutureIPR.Location = new Point(200, 1);
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
            rdoCurrentIPR.Location = new Point(111, 1);
            rdoCurrentIPR.Name = "rdoCurrentIPR";
            rdoCurrentIPR.Size = new Size(81, 25);
            rdoCurrentIPR.TabIndex = 12;
            rdoCurrentIPR.TabStop = true;
            rdoCurrentIPR.Text = "Current";
            rdoCurrentIPR.UseVisualStyleBackColor = false;
            rdoCurrentIPR.CheckedChanged += ShowAvailbleMthods;
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
            pnlWellType.Size = new Size(296, 39);
            pnlWellType.TabIndex = 0;
            // 
            // rdoGasWell
            // 
            rdoGasWell.AutoSize = true;
            rdoGasWell.Location = new Point(200, 8);
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
            rdoOilWell.Location = new Point(111, 8);
            rdoOilWell.Name = "rdoOilWell";
            rdoOilWell.Size = new Size(82, 25);
            rdoOilWell.TabIndex = 6;
            rdoOilWell.TabStop = true;
            rdoOilWell.Text = "Oil Well";
            rdoOilWell.UseVisualStyleBackColor = true;
            rdoOilWell.CheckedChanged += ShowAvailbleMthods;
            // 
            // lblWellType
            // 
            lblWellType.AutoSize = true;
            lblWellType.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWellType.Location = new Point(5, 10);
            lblWellType.Name = "lblWellType";
            lblWellType.Size = new Size(85, 21);
            lblWellType.TabIndex = 5;
            lblWellType.Text = "Well Type:";
            // 
            // pnlFormControl
            // 
            pnlFormControl.BackColor = Color.Transparent;
            pnlFormControl.BorderStyle = BorderStyle.FixedSingle;
            pnlFormControl.Controls.Add(btnReset);
            pnlFormControl.Controls.Add(btnPlot);
            pnlFormControl.Dock = DockStyle.Bottom;
            pnlFormControl.Location = new Point(0, 599);
            pnlFormControl.Name = "pnlFormControl";
            pnlFormControl.Size = new Size(739, 62);
            pnlFormControl.TabIndex = 16;
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.Location = new Point(421, 8);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(147, 41);
            btnReset.TabIndex = 1;
            btnReset.Text = "Reset ";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnPlot
            // 
            btnPlot.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlot.Location = new Point(574, 8);
            btnPlot.Name = "btnPlot";
            btnPlot.Size = new Size(147, 41);
            btnPlot.TabIndex = 0;
            btnPlot.Text = "Plot";
            btnPlot.UseVisualStyleBackColor = true;
            btnPlot.Click += Main;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.Transparent;
            pnlMain.Controls.Add(pnlFormControl);
            pnlMain.Controls.Add(gbIPR);
            pnlMain.Controls.Add(vsbNodalAnalysisData);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(758, 661);
            pnlMain.TabIndex = 1;
            // 
            // frmNodalAnalysis
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1458, 661);
            Controls.Add(pnlMain);
            Controls.Add(pnlPlot);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmNodalAnalysis";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nodal Analysis";
            Load += frmNodalAnalysis_Load;
            pnlPlot.ResumeLayout(false);
            pnlFormatPlot.ResumeLayout(false);
            gbCurveSetting.ResumeLayout(false);
            gbCurveSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLineWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMarkerSize).EndInit();
            gbPlotSetting.ResumeLayout(false);
            gbPlotSetting.PerformLayout();
            pnlFormatPlotHeader.ResumeLayout(false);
            pnlFormatPlotHeader.PerformLayout();
            gbIPR.ResumeLayout(false);
            gbTestData.ResumeLayout(false);
            gbTestData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTestData).EndInit();
            gbReservoirData.ResumeLayout(false);
            tabReservoirData.ResumeLayout(false);
            tpPresentReservoirData.ResumeLayout(false);
            tpPresentReservoirData.PerformLayout();
            tpFuture.ResumeLayout(false);
            tpFuture.PerformLayout();
            gbGeneralControl.ResumeLayout(false);
            gbGeneralControl.PerformLayout();
            pnlSelectMethod.ResumeLayout(false);
            pnlSelectMethod.PerformLayout();
            pnlIPRScenario.ResumeLayout(false);
            pnlIPRScenario.PerformLayout();
            pnlWellType.ResumeLayout(false);
            pnlWellType.PerformLayout();
            pnlFormControl.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPlot;
        private Button btnFomatPlot;
        private ScottPlot.WinForms.FormsPlot pltNodalAnalysis;
        private ColorDialog colorDialog1;
        private RadioButton Future;
        private Panel pnlFormatPlot;
        private GroupBox gbCurveSetting;
        private Label lblPlotLineWidth;
        private NumericUpDown nudLineWidth;
        private Label lblPlotMarkerSize;
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
        private ComboBox cbLinePattern;
        private Label lblLinePattern;
        private VScrollBar vsbNodalAnalysisData;
        private GroupBox gbIPR;
        private GroupBox gbTestData;
        private Label label1;
        private TextBox txtTestFlowEfficiency;
        private GroupBox gbReservoirData;
        private TabControl tabReservoirData;
        private TabPage tpPresentReservoirData;
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
        private TextBox txtFutureOilViscosity;
        private TextBox txtFutureOilFormationVolumeFactor;
        private TextBox txtFutureOilRelativePermeability;
        private TextBox txtFutureReservoirPressure;
        private Label lblFutureOilFormationVolumeFactor;
        private Label lblFutureOilRelativePermeability;
        private Label lblFutureOilViscosity;
        private Label lblFutureReservoirPressure;
        private Label lblTestFlowEfficiency;
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
        private Panel pnlFormControl;
        private Button btnReset;
        private Button btnPlot;
        private Panel pnlMain;
        private Label lblMarkerShape;
        private ComboBox cbMarkerShape;
        private DataGridView dgvTestData;
        private DataGridViewTextBoxColumn dgvcTestFlowRate;
        private DataGridViewTextBoxColumn dgvcTestBottomHolePressure;
    }
}
