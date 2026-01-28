using Microsoft.VisualBasic.ApplicationServices;
using OpenTK.Graphics.OpenGL;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions_and_Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_PresentationLayer;
using PetroFlow_PresentationLayer.Properties;
using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.Hatches;
using ScottPlot.Plottables;
using ScottPlot.TickGenerators.Financial;
using ScottPlot.Triangulation;
using System.Security.Policy;
using System.Windows.Media;
using static System.Net.WebRequestMethods;


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

            ResetMenu();


        }

        private void Main(object sender, EventArgs e)
        {

            string CurveName = cbSelectCurve.Text == "Add Curve" ? txtLegendText.Text : cbSelectCurve.Text;

            if (!AddCurve(ref CurveName))
                return;

            GenerateIPR(CurveName);

            PlotCurves();

            pltNodalAnalysis.PerformAutoScale();

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

        private bool ReadDuoble(TextBox textBox, string dataType, out double result)
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

            if (dgvTestData.Rows.Count <= 0)
            {

                MessageBox.Show("No test data has been provided: Test data is rquaired to generate IPR.", "Data Reading Erorr",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;

            }

            foreach (DataGridViewRow row in dgvTestData.Rows)
            {

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


                if (!double.TryParse(row.Cells[0].Value?.ToString(), out double bottomHolePressure))
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

        private Dictionary<enIPRData, object> ReadData()
        {

            Dictionary<enIPRData, object> data = new();

            if (ReadDuoble(txtReservoirPressure, "Reservoir Pressure", out double result))
                data[enIPRData.ReservoirPressure] = result;

            if (ReadDuoble(txtBubblePointPressure, "Bubble Point Pressure", out result))
                data[enIPRData.BubblePointPressure] = result;

            List<clsInFlowDataRow> testData = ReadTestData();

            if (!(testData == null))
                data[enIPRData.TestData] = testData;


            return data;

        }

        private void SetWarnings(clsValidationResult result)
        {

            foreach (string warning in result.Warnings)
                MessageBox.Show(warning, "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

        }

        private bool AddCurve(ref string curveName)
        {
            IIPRMethod method;
            Dictionary<enIPRData, object> data = ReadData();
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

                validationResult = method.SetInputData(data);
                method.CurvePlotSetting = GetPlotsetting(curveName);
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

        private void GenerateIPR(string curveName)
        {



            var curve = _iPRRepository.Get(curveName);


            if (rdoCurrentIPR.Checked)
                curve.GenerateIPR();

            if (rdoFutureIPR.Checked && curve is IFuturePredictable future) ;


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

            pltNodalAnalysis.Refresh();

            pltNodalAnalysis.PerformAutoScale();

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

            rdoVogelMethod.Enabled = rdoVogelMethod.Checked = !rdoFutureIPR.Checked && !rdoGasWell.Checked;
            rdoStandingMethod.Enabled = rdoStandingMethod.Checked = !rdoGasWell.Checked;
            rdoFetkovich.Enabled = rdoFetkovich.Checked = !rdoGasWell.Checked;

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

            TextBox textBox = sender as TextBox;

            bool isDigit = char.IsDigit(e.KeyChar);
            bool isControl = char.IsControl(e.KeyChar);
            bool isDecimalPoint = e.KeyChar == '.' && !textBox.Text.Contains(".");

            e.Handled = !(isDigit || isControl || isDecimalPoint);
        }

        private void frmNodalAnalysis_Load(object sender, EventArgs e)
        {

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

            if (curve.MethodType == enIPRMethodType.Standing)
            txtOilRelativePermeability.Text = 
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
        }

    }
}
