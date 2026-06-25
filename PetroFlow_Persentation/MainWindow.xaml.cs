using PetroFlow_BusinessLayer.General_Utility.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Main__IPR_Classes;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_PersentationLayer.Utility;
using PetroFlow_PresentationLayer;
using System.Windows;

namespace PetroFlow_Persentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IPROutput _iproutput;

        public MainWindow()
        {

            InitializeComponent();


        }

        private void GeneratIPR(object sender, EventArgs e)
        {

            try
            {

                _iproutput = IPRScreen.Calculate();

            }
            catch (ExceptionBase ex)
            {

                MessageBox.Show(ex.Message, ex.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            List<FlowDataRow> rows = _iproutput.Output.ToList();

            ResultScreen.Plot(rows, IPRScreen.IPRCurveLabelInput.Text);

        }

        private void GeneratVLP(object sender, EventArgs e)
        {


        }

        private void GetOperatingPoint(object sender, EventArgs e)
        {


        }

        private void ResetMenu(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to reset the menu?", 
                "Reset The Menu", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {

                VLPScreen.ResetMenu();
                ResultScreen.ResetMenu();

            }

        }

    }
}