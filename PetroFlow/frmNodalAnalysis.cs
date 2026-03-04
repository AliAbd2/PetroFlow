 using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions_and_Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_PresentationLayer;
using PetroFlow_PresentationLayer.Properties;
using ScottPlot;


namespace PetroFlow
{
    public partial class frmNodalAnalysis : Form
    {

        private clsIPRRepository _iPRRepository;

        private clsCurvePlotter _plotter;

        private List<string> _linePatternStrings = new List<string>
        {
            "Solid Line",
            "Dash Line",
            "Densely Dashed Line",
            "Dotted Line"
        };

        private List<string> _markerShapStrings = new List<string>
        {
            "None",
            "Asterisk",
            "Cross",
            "Eks",
            "Filled Circle",
            "Open Circle",
            "Open Circle With Cross",
            "Open Circle With Dot",
            "Open Circle With Eks",
            "Filled Diamond",
            "Open Diamond",
            "Filled Square",
            "Open Square",
            "Filled Triangle Down",
            "Open Triangle Down",
            "Filled Triangle Up",
            "Open Triangle Up",
            "Hash Tag",
            "Horizontal Bar",
            "Vertical Bar",
            "Tri Down",
            "Tri Up"
        };

        public frmNodalAnalysis()
        {
            InitializeComponent();

            _plotter = new clsCurvePlotter(pltNodalAnalysis.Plot);

            cbLinePattern.DataSource = _linePatternStrings;
            cbMarkerShape.DataSource = _markerShapStrings;

            SetTestDataGridViewProperties();

            ResetMenu();


        }

        private void Main(object sender, EventArgs e)
        {

            string CurveName = cbSelectCurve.Text == "Add Curve" ? txtLegendText.Text : cbSelectCurve.Text;

            if (!AddCurve(ref CurveName))
                return;

            if (!GenerateIPR(CurveName))
                return;

            PlotCurves();

        }

        private ScottPlot.LinePattern GetLinePattern()
        {

            switch (cbLinePattern.SelectedIndex)
            {

                case 1:
                    return ScottPlot.LinePattern.Dashed;
                case 2:
                    return ScottPlot.LinePattern.DenselyDashed;
                case 3:
                    return ScottPlot.LinePattern.Dotted;
                default:
                    return ScottPlot.LinePattern.Solid;
            }

        }

        private MarkerShape GetMarkerShape()
        {

            switch (cbMarkerShape.SelectedIndex)
            {

                case 1:
                    return MarkerShape.Asterisk;
                case 2:
                    return MarkerShape.Cross;
                case 3:
                    return MarkerShape.Eks;
                case 4:
                    return MarkerShape.FilledCircle;
                case 5:
                    return MarkerShape.OpenCircle;
                case 6:
                    return MarkerShape.OpenCircleWithCross;
                case 7:
                    return MarkerShape.OpenCircleWithDot;
                case 8:
                    return MarkerShape.OpenCircleWithEks;
                case 9:
                    return MarkerShape.FilledDiamond;
                case 10:
                    return MarkerShape.OpenDiamond;
                case 11:
                    return MarkerShape.FilledSquare;
                case 12:
                    return MarkerShape.OpenSquare;
                case 13:
                    return MarkerShape.FilledTriangleDown;
                case 14:
                    return MarkerShape.OpenTriangleDown;
                case 15:
                    return MarkerShape.FilledTriangleUp;
                case 16:
                    return MarkerShape.OpenTriangleUp;
                case 17:
                    return MarkerShape.HashTag;
                case 18:
                    return MarkerShape.HorizontalBar;
                case 19:
                    return MarkerShape.VerticalBar;
                case 20:
                    return MarkerShape.TriDown;
                case 21:
                    return MarkerShape.TriUp;
                default:
                    return MarkerShape.None;

            }

        }

        private clsCurvePlotSettings GetPlotsetting(string CurveName)
        {

            clsCurvePlotSettings plotSettings = new clsCurvePlotSettings();


            plotSettings.LegendText = CurveName;
            plotSettings.LineColor = ScottPlot.Color.FromColor(colorDialog1.Color);
            plotSettings.LinePattern = GetLinePattern();
            plotSettings.LineWidth = (int)nudLineWidth.Value;
            plotSettings.MarkerShape = GetMarkerShape();
            plotSettings.MarkerColor = ScottPlot.Color.FromColor(colorDialog1.Color);
            plotSettings.MarkerSize = (int)nudMarkerSize.Value;


            return plotSettings;

        }

        private bool ReadDouble(TextBox textBox, string dataType, out double result)
        {

            result = 0;

            if (string.IsNullOrWhiteSpace(textBox.Text))
                return false;



            if (double.TryParse(textBox.Text, out result))
                return true;

            MessageBox.Show($"{dataType} must be bumeric.", "Data Reading Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;

        }

        private List<clsInFlowDataRow> ReadTestData()
        {

            List<clsInFlowDataRow> data = new();

            foreach (DataGridViewRow row in dgvTestData.Rows)
            {

                if (row.Cells[0] is DataGridViewComboBoxCell)
                    continue;

                if (string.IsNullOrWhiteSpace(row.Cells[0].Value?.ToString()))
                    continue;

                if (!double.TryParse(row.Cells[0].Value?.ToString(), out double flowRate))
                {

                    MessageBox.Show("One or more test flow rate is not numberic: test flow rates musd be numric.",
                        "Data Reading Erorr",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return null;

                }


                if (!double.TryParse(row.Cells[1].Value?.ToString(), out double bottomHolePressure))
                {

                    MessageBox.Show("One or more test bottom hole pressure is not numberic: test bottom hole pressure musd be numric.",
                        "Data Reading Erorr",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return null;

                }


                clsInFlowDataRow newRow = new clsInFlowDataRow(bottomHolePressure, flowRate);

                data.Add(newRow);

            }

            return data;


        }

        private clsPresentIPRDataInput ReadPresentIPRDataInput()
        {

            double? reservoirPressure = null;
            double? bubblePointPressure = null;
            List<clsInFlowDataRow>? testData = null;
            double? testFlowEfficiency = null;
            double? wellExponent = null;

            if (ReadDouble(txtReservoirPressure, "Reservoir Pressure", out double ReservoirPressure))
                reservoirPressure = ReservoirPressure;

            if (ReadDouble(txtBubblePointPressure, "Bubble Point Pressure", out double BubblePointPressure))
                bubblePointPressure = BubblePointPressure;

            testData = ReadTestData();

            if (ReadDouble(txtTestFlowEfficiency, "Test Flow Efficiency", out double TestFlowEfficiency))
                testFlowEfficiency = TestFlowEfficiency;

            if (ReadDouble(txtWellExponent, "Well Exponent", out double WellExponent))
                wellExponent = WellExponent;


            clsPresentIPRDataInput inputData = new clsPresentIPRDataInput(reservoirPressure,
                bubblePointPressure, testData, testFlowEfficiency, wellExponent);

            return inputData;


        }

        private clsFutureIPRDataInput ReadFuturteIPRDataInput()
        {

            double? PresentOilRelativePermeability = null;
            double? PresentOilViscosity = null;
            double? PresentOilFormationVolumeFactor = null;
            double? FutureReservoirPressure = null;
            double? FutureOilRelativePermeability = null;
            double? FutureOilViscosity = null;
            double? FutureOilFormationVolumeFactor = null;

            if (ReadDouble(txtOilRelativePermeability, "Present Oil Relative Permeability", out double presentOilRelativePermeability))
                PresentOilRelativePermeability = presentOilRelativePermeability;

            if (ReadDouble(txtOilViscosity, "Present Oil Viscosity", out double presentOilViscosity))
                PresentOilViscosity = presentOilViscosity;

            if (ReadDouble(txtOilFormationVolumeFactor, "Present Oil Formation Volume Factor", out double presentOilFormationVolumeFactor))
                PresentOilFormationVolumeFactor = presentOilFormationVolumeFactor;

            if (ReadDouble(txtFutureReservoirPressure, "Future Reservoir Pressure", out double futureReservoirPressure))
                FutureReservoirPressure = futureReservoirPressure;

            if (ReadDouble(txtFutureOilRelativePermeability, "Future Oil Relative Permeability", out double futureOilRelativePermeability))
                FutureOilRelativePermeability = futureOilRelativePermeability;

            if (ReadDouble(txtFutureOilViscosity, "Future Oil Viscosity", out double futureOilViscosity))
                FutureOilViscosity = futureOilViscosity;

            if (ReadDouble(txtFutureOilFormationVolumeFactor, "Future Oil Formation Volume Factor", out double futureOilFormationVolumeFactor))
                FutureOilFormationVolumeFactor = futureOilFormationVolumeFactor;

            clsFutureIPRDataInput futureIPRDataInput = new(FutureReservoirPressure,
                PresentOilRelativePermeability, PresentOilViscosity, PresentOilFormationVolumeFactor,
                FutureOilRelativePermeability, FutureOilViscosity, FutureOilFormationVolumeFactor);

            return futureIPRDataInput;

        }
         
        private void SetWarnings(clsValidationResult result)
        {

            foreach (string warning in result.Warnings)
                MessageBox.Show(warning, "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

        }

        private clsIPRGenerationSettings SetGenerationSettings()
        {

            double MinimumPressure = (double)nudMinimumPressure.Value;
            double PressueStepSize = (double)nudPressureStepSize.Value;


            clsIPRGenerationSettings generationSettings = new(PressueStepSize, MinimumPressure);

            return generationSettings;

        }

        private bool AddCurve(ref string curveName)
        {
            IIPRMethod method;
            clsPresentIPRDataInput inputData = ReadPresentIPRDataInput();
            clsIPRGenerationSettings generationSettings = SetGenerationSettings();

            if (rdoFutureIPR.Checked)
            {
                clsFutureIPRDataInput futureInputData = ReadFuturteIPRDataInput();
            }


            if (inputData == null)
                return false;

            clsValidationResult validationResult = new();

            if (rdoStandingMethod.Checked)
                method = new clsStanding();
            else if (rdoFetkovich.Checked)
                method = new clsFetkovich();
            else if (rdoJones.Checked)
                method = new clsJonesBlountGlaze();
            else
                method = new clsVogel();

            try
            {

                validationResult = method.SetInputData(inputData);
                method.CurvePlotSetting = GetPlotsetting(curveName);
                method.GenerationSettings = generationSettings;
                _iPRRepository.Add(method, ref curveName);

                SetWarnings(validationResult);

                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Data Reading Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;

            }

        }

        private bool GenerateIPR(string curveName)
        {

            var curve = _iPRRepository.Get(curveName);


            if (rdoCurrentIPR.Checked)
            {

                curve.GenerateIPR();
                return true;
            }
                

            if (rdoFutureIPR.Checked && curve is IFuturePredictable future)
            {

                try
                {
                    future.GenerateFutureIPR(ReadFuturteIPRDataInput());
                    return true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Data Reading Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;


                }

            }
            else
            {

                MessageBox.Show(
                    "The selected method is not compatible with the selected scenario.",
                    "Method Selection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }


            return false;
        }

        private void PlotCurves()
        {

            List<IIPRMethod> curves = _iPRRepository.GetAll();

            pltNodalAnalysis.Plot.Clear();

            foreach (IIPRMethod curve in curves)
            {

                List<double> flowrates = curve.GeneratedData.Select(x => x.FlowRate).ToList();
                List<double> bottomHolePressures = curve.GeneratedData.Select(y => y.BottomHolePressure).ToList();

                _plotter.PlotScatter(flowrates, bottomHolePressures, curve.CurvePlotSetting, curve.Name);

            }

            pltNodalAnalysis.Plot.Title(txtPlotTitle.Text);
            pltNodalAnalysis.Plot.XLabel(txtXlabel.Text);
            pltNodalAnalysis.Plot.YLabel(txtylabel.Text);

            pltNodalAnalysis.Plot.Axes.AutoScale();

            pltNodalAnalysis.Refresh();

        }

        private void ResetMenu()
        {

            // A method to Restores the menu to its initial state

            rdoOilWell.Select();
            rdoCurrentIPR.Select();
            rdoVogelMethod.Select();

            txtReservoirPressure.Text = "";
            txtBubblePointPressure.Text = "";
            txtOilRelativePermeability.Text = "";
            txtOilViscosity.Text = "";
            txtOilFormationVolumeFactor.Text = "";
            txtFutureReservoirPressure.Text = "";
            txtFutureOilRelativePermeability.Text = "";
            txtFutureOilFormationVolumeFactor.Text = "";
            txtFutureOilViscosity.Text = "";

            cbSelectCurve.Items.Add("Add Curve");
            cbSelectCurve.SelectedIndex = 0;

            txtPlotTitle.Text = "Nodal Analysis";
            txtylabel.Text = "Pressure";
            txtXlabel.Text = "Flowrate";

            pltNodalAnalysis.Plot.Title(txtPlotTitle.Text);
            pltNodalAnalysis.Plot.XLabel(txtXlabel.Text);
            pltNodalAnalysis.Plot.YLabel(txtylabel.Text);

            nudLineWidth.Value = 1;
            nudMarkerSize.Value = 0;
            cbLinePattern.SelectedIndex = 0;
            cbMarkerShape.SelectedIndex = 0;

            _iPRRepository = new();

            SetDataGridViewRows();

        }

        private void UpdatePlot(object sender, EventArgs e)
        {

            if (cbSelectCurve.SelectedItem == "Add Curve" || cbSelectCurve.SelectedItem == null)
                return;

            IIPRMethod curve = _iPRRepository.Get(cbSelectCurve.SelectedItem.ToString());

            clsCurvePlotSettings plotSettings = curve.CurvePlotSetting;


            plotSettings.LegendText = txtLegendText.Text;
            plotSettings.LineColor = ScottPlot.Color.FromColor(colorDialog1.Color);
            plotSettings.LinePattern = GetLinePattern();
            plotSettings.LineWidth = (int)nudLineWidth.Value;
            plotSettings.MarkerShape = GetMarkerShape();
            plotSettings.MarkerColor = ScottPlot.Color.FromColor(colorDialog1.Color);
            plotSettings.MarkerSize = (int)nudMarkerSize.Value;

            curve.CurvePlotSetting = plotSettings;

            PlotCurves();

        }

        private void ShowAvailbleMthods(object sender, EventArgs e)
        {

            bool IsGasWell = rdoGasWell.Checked;
            bool IsFuture = rdoFutureIPR.Checked;

            rdoVogelMethod.Enabled = !IsGasWell && !IsFuture;
            rdoStandingMethod.Enabled = !IsGasWell;
            rdoFetkovich.Enabled = !IsGasWell;
            rdoJones.Enabled = !IsGasWell && !IsFuture;

        }

        private void btnFomatPlot_Click(object sender, EventArgs e)
        {
            pnlFormatPlot.Visible = !pnlFormatPlot.Visible;
        }

        // Note: The buttom functionility need better impelementation.
        private void btnFomatPlot_MouseEnter(object sender, EventArgs e)
        {

            if (!pnlFormatPlot.Visible)
                btnFomatPlot.Image = Resources.left_arrow1;
            else
                btnFomatPlot.Image = Resources.right_arrow1;

            btnFomatPlot.Width = 20;

        }

        private void btnFomatPlot_MouseLeave(object sender, EventArgs e)
        {

            if (!pnlFormatPlot.Visible)
                btnFomatPlot.Image = Resources.left_arrow1;
            else
                btnFomatPlot.Image = Resources.right_arrow1;


            if (!pnlFormatPlot.Visible)
            {

                btnFomatPlot.Image = null;

                btnFomatPlot.Width = 5;

            }


        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {

            colorDialog1.ShowDialog();

            UpdatePlot(sender, e);

        }

        private void cbSelectCurve_DropDown(object sender, EventArgs e)
        {

            List<string> Curves = _iPRRepository.GetAllCurvesNames();

            cbSelectCurve.Items.Clear();

            for (int i = 0; i < Curves.Count; i++)
            {

                cbSelectCurve.Items.Add(Curves[i]);

            }

            cbSelectCurve.Items.Add("Add Curve");



        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetMenu();
        }

        private void EnforceNumericInput(object sender, KeyPressEventArgs e)
        {
            // Restricts the TextBox input to numeric characters and a single decimal point,
            // while allowing control keys such as Backspace.

            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;

            bool isDigit = char.IsDigit(e.KeyChar);
            bool isControl = char.IsControl(e.KeyChar);
            bool isDecimalPoint = e.KeyChar == '.' && !textBox.Text.Contains(".");

            e.Handled = !(isDigit || isControl || isDecimalPoint);
        }

        private void cbSelectCurve_DropDownClosed(object sender, EventArgs e)
        {
            if (cbSelectCurve.SelectedItem == null)
                cbSelectCurve.SelectedItem = "Add Curve";
        }

        private void UpdatePlotTitle(object sender, EventArgs e)
        {

            pltNodalAnalysis.Plot.Title(txtPlotTitle.Text);
            pltNodalAnalysis.Plot.XLabel(txtXlabel.Text);
            pltNodalAnalysis.Plot.YLabel(txtylabel.Text);

            pltNodalAnalysis.Refresh();

        }

        private void LoadCurveDataToForm()
        {


            if (cbSelectCurve.SelectedItem == "Add Curve" || cbSelectCurve.SelectedItem == null)
                return;

            IIPRMethod curve = _iPRRepository.Get(cbSelectCurve.SelectedItem.ToString());

            txtReservoirPressure.Text = curve.ReservoirPressure.ToString();
            txtBubblePointPressure.Text = curve.BubblePointPressure.ToString();




        }

        private void SetTestDataGridViewProperties()
        {

            DataGridViewColumn flowRate = new DataGridViewColumn();
            flowRate.HeaderText = "Flow Rate";
            flowRate.Width = 100;
            flowRate.CellTemplate = new DataGridViewTextBoxCell();

            DataGridViewColumn bottomHolePressure = new DataGridViewColumn();
            bottomHolePressure.HeaderText = "Pwf";
            bottomHolePressure.CellTemplate = new DataGridViewTextBoxCell();

            dgvTestData.Columns.Add(flowRate);
            dgvTestData.Columns.Add(bottomHolePressure);


        }

        private void SetDataGridViewRows()
        {

            dgvTestData.Rows.Clear();

            DataGridViewCell cell = new DataGridViewComboBoxCell();
            DataGridViewCell cell1 = new DataGridViewComboBoxCell();

            dgvTestData.Rows.Add(new DataGridViewRow());

            dgvTestData.Rows[0].Cells[0] = cell;
            dgvTestData.Rows[0].Cells[1] = cell1;

            if (rdoVogelMethod.Checked || rdoStandingMethod.Checked)
                dgvTestData.Rows.Add(new DataGridViewRow());

            if (rdoFetkovich.Checked)
                for (int i = 0; i < 3; i++)
                    dgvTestData.Rows.Add(new DataGridViewRow());

            if (rdoJones.Checked)
                for (int i = 0; i < 2; i++)
                    dgvTestData.Rows.Add(new DataGridViewRow());


        }

        private void AddTestDataRow(object sender, EventArgs e)
        {

            dgvTestData.Rows.Add(new DataGridViewRow());

        }

        private void btnDeleteCurve_Click(object sender, EventArgs e)
        {

            if (cbSelectCurve.SelectedItem == "Add Curve")
                return;

            _iPRRepository.Remove(cbSelectCurve.SelectedItem.ToString());

            if (_iPRRepository.GetAll().Count > 0)
                PlotCurves();
            else
                pltNodalAnalysis.Plot.Clear();

            pltNodalAnalysis.Refresh();

        }

        private void SetMenu(object sender, EventArgs e)
        {

            bool IsFuture = rdoFutureIPR.Checked;
            bool IsVogel = rdoVogelMethod.Checked;
            bool IsStanding = rdoStandingMethod.Checked;
            bool IsFetkovich = rdoFetkovich.Checked;
            bool IsJounes = rdoJones.Checked;

            txtBubblePointPressure.Visible = lblBubblePointPressure.Visible =
                cbBubblePointPressureUnit.Visible = !IsJounes;

            txtOilRelativePermeability.Visible =
                lblOilRelativePermeability.Visible = cbOilRelativePermeabilityUnit.Visible = IsStanding;

            txtOilViscosity.Visible =
                lblOilViscosity.Visible = cbOilViscosityUnit.Visible = IsStanding;

            txtOilFormationVolumeFactor.Visible =
                lblOilFormationVolumeFactor.Visible = cbOilFormationVolumeFactorUnit.Visible = IsStanding;


            txtTestFlowEfficiency.Visible = lblTestFlowEfficiency.Visible
                = cbTestFlowEfficiencyUnit.Visible = IsStanding;

            txtNewFlowEfficiency.Visible = lblNewFlowEfficiency.Visible =
                cbNewFlowEfficiencyUnit.Visible = IsStanding;

            txtWellExponent.Visible =
                lblWellExponent.Visible = IsFetkovich;

            txtFutureReservoirPressure.Visible =
                lblFutureReservoirPressure.Visible = cbFutureReservoiPressureUnit.Visible = IsStanding || IsFetkovich;

            txtFutureOilRelativePermeability.Visible =
                lblFutureOilRelativePermeability.Visible = cbFutureOilRelativePermeabilityUnit.Visible = IsStanding;

            txtFutureOilViscosity.Visible =
                lblFutureOilViscosity.Visible = cbFutureOilViscosityUnit.Visible = IsStanding;

            txtFutureOilFormationVolumeFactor.Visible =
                lblFutureOilFormationVolumeFactor.Visible = cbFutureOilFormationVolumeFactorUnit.Visible = IsStanding;

            btnAddTestDataRow.Visible = IsFetkovich || IsJounes;

            SetDataGridViewRows();

        }


    }
}
