using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_PresentationLayer.Properties;

namespace PetroFlow
{
    public partial class frmNodalAnalysis : Form
    {
        public frmNodalAnalysis()
        {
            InitializeComponent();


        }

        private void UpdatePlotText()
        {

            pltNodalAnalysis.Plot.Title(txtPlotTitle.Text);
            pltNodalAnalysis.Plot.XLabel(txtXlabel.Text);
            pltNodalAnalysis.Plot.YLabel(txtylabel.Text);

        }


        private void frmNodalAnalysis_Load(object sender, EventArgs e)
        {

            UpdatePlotText();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlFormatPlot.Visible = !pnlFormatPlot.Visible;
        }

        // Note: The buttom functionility need beffer impelementation.
        private void button1_MouseEnter(object sender, EventArgs e)
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

    }
}
