using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;

namespace PetroFlow
{
    public partial class frmNodalAnalysis : Form
    {
        public frmNodalAnalysis()
        {
            InitializeComponent();
        }

        void PlotIPRData()
        {

            double.TryParse(txtReservoirPressure.Text, out double Pr);
            double.TryParse(txtBubblePointPressure.Text, out double pb);
            double.TryParse(txtTestFlowingPressure.Text, out double pwf);
            double.TryParse(txtTestFlowrate.Text, out double qo);


            List<clsInFlowDataRow> data = clsVogel.GeneratIPR(Pr, qo, pwf, pb);

            List<double> x = new List<double>();
            List<double> y = new List<double>();

            foreach (clsInFlowDataRow row in data)
            {

                x.Add(row.FlowRate);
                y.Add(row.FlowingPressure);

            }

            pltNodalAnalysis.Plot.Clear();

            var scatter = pltNodalAnalysis.Plot.Add.Scatter(x, y);
            scatter.LineWidth = 1;
            scatter.MarkerSize = 0;

            scatter.LegendText = $"IPR";

            pltNodalAnalysis.Plot.Title("Oil Well Preformance");
            pltNodalAnalysis.Plot.XLabel("Flowrate");
            pltNodalAnalysis.Plot.YLabel("pressure");

            pltNodalAnalysis.Plot.Axes.AutoScale();

            pltNodalAnalysis.Refresh();

        }

        private void frmNodalAnalysis_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlotIPRData();
        }
    }
}
