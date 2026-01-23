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


        private void frmNodalAnalysis_Load(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlFormatPlot.Visible = !pnlFormatPlot.Visible;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (pnlFormatPlot.Visible)
                btnFomatPlot.Image = Resources.right_arrow1;
            else
                btnFomatPlot.Image = Resources.left_arrow1;

                btnFomatPlot.Width = 15;

        }

        private void btnFomatPlot_MouseLeave(object sender, EventArgs e)
        {



            if (!pnlFormatPlot.Visible)
            {

                btnFomatPlot.Image = null;

                btnFomatPlot.Width = 5;

            }


        }
    }
}
