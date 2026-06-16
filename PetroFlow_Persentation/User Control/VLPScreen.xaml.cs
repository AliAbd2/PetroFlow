using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Models;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPFrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPHoldup;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethod.VLPFrictionFactorMethods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using PetroFlow_PersentationLayer.Utility;
using System;
using System.Collections.Generic;
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

namespace PetroFlow_Persentation.User_Control
{
    /// <summary>
    /// Interaction logic for VLPScreen.xaml
    /// </summary>
    public partial class VLPScreen : UserControl
    {
        public VLPScreen()
        {
            InitializeComponent();

            ResetMenu();

        }

        public void ResetMenu()
        {

            VLPModelSelectedNoSlipNoFlowRegime.IsChecked = true;

            WellHeadPressureInput.Text = "";
            if (WellHeadPressureUnit.Items.Count == 0)
            {

                WellHeadPressureUnit.Items.Add("psia");
                WellHeadPressureUnit.SelectedIndex = 0;

            }

            TotalDepthInput.Text = "";
            if (TotalDepthUnit.Items.Count == 0)
            {

                TotalDepthUnit.Items.Add("ft");
                TotalDepthUnit.SelectedIndex = 0;

            }

            TubingSizeInput.Text = "";
            if (TubingSizeUnit.Items.Count == 0)
            {

                TubingSizeUnit.Items.Add("ft");
                TubingSizeUnit.SelectedIndex = 0;

            }

            PipeRelativeRoughnessInput.Text = "";
            if (PipeRelativeRoughnessUnit.Items.Count == 0)
            {

                PipeRelativeRoughnessUnit.Items.Add("dimensionless");
                PipeRelativeRoughnessUnit.SelectedIndex = 0;

            }

            GravityAccelerationInput.Text = "32.2";
            if (GravityAccelerationUnit.Items.Count == 0)
            {

                GravityAccelerationUnit.Items.Add("ft/sec²");
                GravityAccelerationUnit.SelectedIndex = 0;

            }


            ReservoirTemperatureInput.Text = "";
            if (ReserovirTemperatureUnit.Items.Count == 0)
            {

                ReserovirTemperatureUnit.Items.Add("°F");
                ReserovirTemperatureUnit.SelectedIndex = 0;

            }

            BubblePointPressureInput.Text = "";
            if (BubblePointPressureUnit.Items.Count == 0)
            {

                BubblePointPressureUnit.Items.Add("psig");
                BubblePointPressureUnit.SelectedIndex = 0;

            }

            OilAPISInput.Text = "";
            BubblePointPressureInput.Text = "";
            if (OilAPISUnit.Items.Count == 0)
            {

                OilAPISUnit.Items.Add("API");
                OilAPISUnit.SelectedIndex = 0;

            }

            LiquidViscosityInput.Text = "";
            if (LiquidViscosityUnit.Items.Count == 0)
            {

                LiquidViscosityUnit.Items.Add("cp");
                LiquidViscosityUnit.SelectedIndex = 0;

            }

            SurfaceTensionInput.Text = "";
            if (SurfaceTensionUnit.Items.Count == 0)
            {

                SurfaceTensionUnit.Items.Add("dynes/cm");
                SurfaceTensionUnit.SelectedIndex = 0;

            }

            GasSpecificGravityInput.Text = "";
            if (GasSpecificGravityUnit.Items.Count == 0)
            {

                GasSpecificGravityUnit.Items.Add("dimensionless");
                GasSpecificGravityUnit.SelectedIndex = 0;

            }

            GasViscosityInput.Text = "";
            if (GasViscosityUnit.Items.Count == 0)
            {

                GasViscosityUnit.Items.Add("cp");
                GasViscosityUnit.SelectedIndex = 0;

            }

            GasOilRatioInput.Text = "";
            if (GasOilRatioUnit.Items.Count == 0)
            {

                GasOilRatioUnit.Items.Add("SCF/STB");
                GasOilRatioUnit.SelectedIndex = 0;

            }

            VLPCurveLabelInput.Text = "VLP";
            DepthStepSizeInput.Text = "10";
            FlowRateStepSizeInput.Text = "10";
            MinimumFlowRateInput.Text = "10";
            MaximumFlowRateInput.Text = "1000";

            VLPGeneratControlTabControl.SelectedIndex = 0;

            _adjustUIBasedOnMethod();


        }

        public IVLPModel GetVLPMethod()
        {

            if (VLPModelSelectedNoSlipNoFlowRegime.IsChecked.Value)
            {

                if (PoettamnnCarpenterVLPSelectedMethod.IsChecked.Value)
                    return new NoSlipNoFlowRegime(new PoettmannCarpenterFrictionFactor());

                if (BaxendellThomasVLPSelectedMethod.IsChecked.Value)
                    return new NoSlipNoFlowRegime(new BaxendellThomasFrictionFactor());

                if (FancherBrownVLPSelectedMethod.IsChecked.Value)
                    return new NoSlipNoFlowRegime(new FancherBrownFrictionFactor());


            }

            if (VLPModelSelectedSlipNoFlowRegime.IsChecked.Value)
            {

                if (HagedornBrownVLPSelectedMethod.IsChecked.Value)
                    return new SlipNoFlowRegime(new HagedornBrownFrictionFactor(),
                        new HagedornBrownHoldupCalculator());

            }

            if (VLPModelSelectedSlipFlowRegime.IsChecked.Value)
            {

                if (DunsRosVLPSelectedMethod.IsChecked.Value)
                    return new DunsRos();

            }

            throw new Exception("No VLP Method has been selected.");

        }

        public VLPInputData ReadVLPInput()
        {

            VLPInputData dataInput = new();

            dataInput.WellHeadPressure = GeneralMethods.ReadDouble(WellHeadPressureInput);
            dataInput.TotalDepth = GeneralMethods.ReadDouble(TotalDepthInput);
            dataInput.PipeInsideDiameter = GeneralMethods.ReadDouble(TubingSizeInput);
            dataInput.PipeRelativeRoughness = GeneralMethods.ReadDouble(PipeRelativeRoughnessInput);
            dataInput.GravityAcceleration = GeneralMethods.ReadDouble(GravityAccelerationInput);

            dataInput.PVT.FahrenheitTemperature = GeneralMethods.ReadDouble(ReservoirTemperatureInput);
            dataInput.PVT.PSIBubblePointPressure = GeneralMethods.ReadDouble(BubblePointPressureInput);
            dataInput.PVT.API = GeneralMethods.ReadDouble(OilAPISInput);
            dataInput.LiquidViscosity = GeneralMethods.ReadDouble(LiquidViscosityInput);
            dataInput.SurfaceTension = GeneralMethods.ReadDouble(SurfaceTensionInput);
            dataInput.PVT.GasSpecificGravity = GeneralMethods.ReadDouble(GasSpecificGravityInput);
            dataInput.GasViscosity = GeneralMethods.ReadDouble(GasViscosityInput);
            dataInput.PVT.GasOilRatio = GeneralMethods.ReadDouble(GasOilRatioInput);

            dataInput.MinimumPressure = GeneralMethods.ReadDouble(MinimumPressureInput);
            dataInput.MinimumFlowRate = GeneralMethods.ReadDouble(MinimumFlowRateInput);
            dataInput.DepthStepSize = GeneralMethods.ReadDouble(DepthStepSizeInput);
            dataInput.FlowRateStepSize = GeneralMethods.ReadDouble(FlowRateStepSizeInput);
            dataInput.MaxFlowRate = GeneralMethods.ReadDouble(MaximumFlowRateInput);


            return dataInput;

        }

        private void _adjustUIBasedOnMethod()
        {

            if (VLPModelSelectedNoSlipNoFlowRegime.IsChecked.Value)
            {

                NoSlipNoFlowRegimeVLPMethodSelectionBorder.Visibility = Visibility.Visible;

                PoettamnnCarpenterVLPSelectedMethod.IsChecked = true;

            }
            else
            { 

                NoSlipNoFlowRegimeVLPMethodSelectionBorder.Visibility = Visibility.Hidden;

                PoettamnnCarpenterVLPSelectedMethod.IsChecked = false;
                BaxendellThomasVLPSelectedMethod.IsChecked = false;
                FancherBrownVLPSelectedMethod.IsChecked = false;

            }

            if (VLPModelSelectedSlipNoFlowRegime.IsChecked.Value)
            { 

                SlipNoFlowRegimeVLPMethodSelectionBorder.Visibility = Visibility.Visible;

                HagedornBrownVLPSelectedMethod.IsChecked = true;

            }
            else
            { 

                SlipNoFlowRegimeVLPMethodSelectionBorder.Visibility = Visibility.Hidden;

                HagedornBrownVLPSelectedMethod.IsChecked = false;

            }

            if (VLPModelSelectedSlipFlowRegime.IsChecked.Value)
            { 

                SlipFlowRegimeVLPMethodSelectionBorder.Visibility = Visibility.Visible;

                DunsRosVLPSelectedMethod.IsChecked = true;

            }
            else
            { 
                
                SlipFlowRegimeVLPMethodSelectionBorder.Visibility = Visibility.Hidden;

                DunsRosVLPSelectedMethod.IsChecked = false;

            }

        }

        private void AdjustUIBasedOnMethod(object sender, RoutedEventArgs e)
        {

            _adjustUIBasedOnMethod();

        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            char c = e.Text[0];

            if (char.IsDigit(c))
                return;

            if (c == '.')
            {
                e.Handled = textBox.Text.Contains('.');
                return;
            }

            if (c == '-')
            {
                e.Handled = textBox.CaretIndex != 0 ||
                            textBox.Text.Contains('-');
                return;
            }

            e.Handled = true;
        }

        private void Numeric_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));

                if (!double.TryParse(text, out _))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

    }
}
