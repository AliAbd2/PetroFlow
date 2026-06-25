using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.ShearedData;
using PetroFlow_PersentationLayer.Utility;
using PetroFlow_PresentationLayer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PetroFlow_Persentation.User_Control
{
    /// <summary>
    /// Interaction logic for ResultScreen.xaml
    /// </summary>
    public partial class ResultScreen : UserControl
    {

        private CurvePlotter curvePlotter;

        public event EventHandler? OnGeneratIPRButtonClick;

        public event EventHandler? OnGeneratVLPButtonClick;

        public event EventHandler? OnGetOperatingButtonClick;

        public event EventHandler? OnResetMenuButtonClick;

        public ResultScreen()
        {
            InitializeComponent();

            SetPlotTheme();

            curvePlotter = new(ResultPlot.Plot);


        }

        public void ResetMenu()
        {
            SetPlotTheme();

            ResultPlot.Plot.Clear();
            ResultPlot.Refresh();

            OperatingPressureValue.Text = "Unknown";
            OperatingFlowRateValue.Text = "Unknown";

        }

        public void Plot(List<FlowDataRow> data, string curveName)
        {

            List<double> x = data.Select(x => x.FlowRate).ToList();
            List<double> y = data.Select(x => x.BottomHolePressure).ToList();

            CurvePlotSettings curvePlotSettings = new();


            curvePlotter.PlotScatter(x,y, curvePlotSettings, curveName);

            ResultPlot.Plot.ShowLegend();
            ResultPlot.Plot.Axes.AutoScale();
            ResultPlot.Refresh();

        }

        public void LoadNodalAnalysisResult(List<FlowDataRow>? iPR, List<FlowDataRow>? vLP)
        {



        }

        private void SetPlotTheme()
        {

            ScottPlot.Color BackgroundColor = GeneralMethods.GetThemeColor("Brush.Surface");
            ScottPlot.Color AxesColor = GeneralMethods.GetThemeColor("Brush.TextPrimary");
            ScottPlot.Color LinesColor = GeneralMethods.GetThemeColor("Brush.Selected");

            ResultPlot.Plot.FigureBackground.Color = ScottPlot.Color.FromColor(System.Drawing.Color.Transparent);

            ResultPlot.Plot.DataBackground.Color = BackgroundColor;

            ResultPlot.Plot.Axes.Color(AxesColor);

            ResultPlot.Plot.Grid.MajorLineColor = LinesColor;

            ResultPlot.Plot.Title("Nodal Analysis");
            ResultPlot.Plot.XLabel("Flow Rate (STB/day)");
            ResultPlot.Plot.YLabel("Pressure (psi)");

        }

        private void GenerateIPR(object sender, RoutedEventArgs e)
        {
            OnGeneratIPRButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void GenerateVLP(object sender, RoutedEventArgs e)
        {
            OnGeneratVLPButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void GetOperatingPoint(object sender, RoutedEventArgs e)
        {
            OnGetOperatingButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void ResetMenu(object sender, RoutedEventArgs e)
        {

            OnResetMenuButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void ClearPlot(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to clear the plot?",
                "Clear The Plot",
                MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {

                ResultPlot.Plot.Clear();
                ResultPlot.Refresh();

            }

        }

    }
}
