using PetroFlow_BusinessLayer.Production.NodalAnalysis;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Main_Classes;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PetroFlow_Persentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<InFlowDataRow> IPR;
        private List<InFlowDataRow> VLP;

        public MainWindow()
        {

            InitializeComponent();

        }

        private void GeneratIPR(object sender, EventArgs e)
        {

            IPR = new();

            IPRMethodBase iPR = IPRScreen.GetIPRMethod();
            IPRInputData inputData;
            bool IsFuture;

            (inputData, IsFuture) = IPRScreen.ReadIPRData();

            NodalAnalysisValidationResult validationResult = new();

            NodalAnalysis nodalAnalysis = new(iPR, null);

            try
            {

                if (!IsFuture)
                {

                    IPR = nodalAnalysis.GenerateIPR(inputData, ref validationResult);

                }
                else
                {

                    IPR = nodalAnalysis.GenerateFututreIPR(inputData, ref validationResult);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;

            }

            ResultScreen.LoadNodalAnalysisResult(IPR, VLP);

            ResultScreen.Plot(IPR, IPRScreen.IPRCurveLabelInput.Text, ScottPlot.Color.FromColor(System.Drawing.Color.Blue));

        }

        private void GeneratVLP(object sender, EventArgs e)
        {

            VLP = new();

            IVLPModel vLP = VLPScreen.GetVLPMethod();
            VLPInputData input = VLPScreen.ReadVLPInput();

            NodalAnalysisValidationResult validationResult = new();

            NodalAnalysis nodalAnalysis = new(null, vLP);

            try
            {

                if (IPR == null)
                {

                    VLP = nodalAnalysis.GenerateVLP(input, ref validationResult);

                }
                else
                {

                    VLP = nodalAnalysis.GeneraterVLPFromIPR(input, IPR, ref validationResult);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;

            }

            ResultScreen.LoadNodalAnalysisResult(IPR, VLP);

            ResultScreen.Plot(VLP, VLPScreen.VLPCurveLabelInput.Text,
                ScottPlot.Color.FromColor(System.Drawing.Color.Red));

        }

        private void GetOperatingPoint(object sender, EventArgs e)
        {
            if (VLP == null || IPR == null)
            {
                MessageBox.Show(
                    "Unable to determine the operating point because either the IPR curve or the VLP curve is missing." +
                    " Please ensure that both analyses have been performed before calculating the operating point.",
                    "Operating Point Calculation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            InFlowDataRow OperatingPoint = new(0, 0);

            try
            {

                OperatingPoint = NodalAnalysis.GetOperatingPoint(IPR, VLP);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;

            }


            if (OperatingPoint.FlowRate == 0 && OperatingPoint.BottomHolePressure ==0)
            {

                ResultScreen.OperatingFlowRateValue.Text = "Not Found";
                ResultScreen.OperatingPressureValue.Text = "Not Found";

            }
            else
            {


                ResultScreen.OperatingFlowRateValue.Text = Math.Round(OperatingPoint.FlowRate).ToString();
                ResultScreen.OperatingPressureValue.Text = Math.Round(OperatingPoint.BottomHolePressure).ToString();

            }

        }

        private void ResetMenu(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to reset the menu?", 
                "Reset The Menu", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {

                IPRScreen.ResetMenu();
                VLPScreen.ResetMenu();
                ResultScreen.ResetMenu();

            }

        }



    }
}