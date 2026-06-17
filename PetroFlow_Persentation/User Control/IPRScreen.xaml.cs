using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_PersentationLayer.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PetroFlow_Persentation.User_Control
{
    /// <summary>
    /// Interaction logic for IPRScreen.xaml
    /// </summary>
    public partial class IPRScreen : System.Windows.Controls.UserControl
    {

        private ObservableCollection<FlowDataRow> _testData;

        public IPRScreen()
        {
            InitializeComponent();

            _testData = new();

            ResetMenu();

        }

        public IPRMethodBase GetIPRMethod()
        {



        }

        public (IPRInputData, bool) ReadIPRData()
        {

            IPRInputData inputData = new();

            inputData.ReservoirPressure = GeneralMethods.ReadDouble(PresentReservoirPressureInput);
            inputData.BubblePointPressure = GeneralMethods.ReadDouble(BubblePoitPressureInput);

            inputData.TestsData = _testData.ToList();

            inputData.PresentOilRelativePermeability = GeneralMethods.ReadDouble(PresentOilRelativePermeabilityInput);
            inputData.PresentOilViscosity = GeneralMethods.ReadDouble(PresentOilViscosityInput);
            inputData.PresentOilFormationVolumeFactor = GeneralMethods.ReadDouble(PresentOilFormationVolumeFactorInput);

            inputData.FutureReservoirPressure = GeneralMethods.ReadDouble(FutureReservoirPressureInput);
            inputData.FutureOilRelativePermeability = GeneralMethods.ReadDouble(FutureOilRelativePermeabilityInput);
            inputData.FutureOilViscosity = GeneralMethods.ReadDouble(FutureOilViscosityInput);
            inputData.FutureOilFormationVolumeFactor = GeneralMethods.ReadDouble(FutureOilFormationVolumeFactorInput);

            inputData.TestFlowEfficiency = GeneralMethods.ReadDouble(TestFlowEfficiecyInput);

            inputData.WellExponent = GeneralMethods.ReadDouble(WellExponentInput);
            inputData.FlowCoefficient = GeneralMethods.ReadDouble(FlowCoefficientInput);

            inputData.GenerationSettings = new(GeneralMethods.ReadDouble(PressureStepSizeInput).Value,
            GeneralMethods.ReadDouble(MinimumPressureInput).Value);

            return (inputData, IPRScenarioFuture.IsChecked.Value);

        }

        public void ResetMenu()
        {

            UseTestDataCheckBox.IsChecked = true;

            _testData.Clear();
            TestDataDataGrid.ItemsSource = _testData;

            PresentReservoirPressureInput.Text = "";
            if (PresentReservoirPressureUnit.Items.Count == 0)
            {

                PresentReservoirPressureUnit.Items.Add("psig");
                PresentReservoirPressureUnit.SelectedIndex = 0;

            }

            BubblePoitPressureInput.Text = "";
            if (BubblePointPressureUnit.Items.Count == 0)
            {

                BubblePointPressureUnit.Items.Add("psig");
                BubblePointPressureUnit.SelectedIndex = 0;

            }

            PresentOilRelativePermeabilityInput.Text = "";
            if (PresentOilRelativePermeabilityUnit.Items.Count == 0)
            {

                PresentOilRelativePermeabilityUnit.Items.Add("fraction");
                PresentOilRelativePermeabilityUnit.SelectedIndex = 0;

            }

            PresentOilViscosityInput.Text = "";
            if (PresentOilViscosityUnit.Items.Count == 0)
            {

                PresentOilViscosityUnit.Items.Add("cp");
                PresentOilViscosityUnit.SelectedIndex = 0;

            }

            PresentOilFormationVolumeFactorInput.Text = "";
            if (PresentOilFormationVolumeFactorUnit.Items.Count == 0)
            {

                PresentOilFormationVolumeFactorUnit.Items.Add("bbl/STB");
                PresentOilFormationVolumeFactorUnit.SelectedIndex = 0;

            }

            FutureReservoirPressureInput.Text = "";
            if (FutureReservoirPressureUnit.Items.Count == 0)
            {

                FutureReservoirPressureUnit.Items.Add("psig");
                FutureReservoirPressureUnit.SelectedIndex = 0;

            }

            FutureOilRelativePermeabilityInput.Text = "";
            if (FutureOilRelativePermeabilityUnit.Items.Count == 0)
            {

                FutureOilRelativePermeabilityUnit.Items.Add("fraction");
                FutureOilRelativePermeabilityUnit.SelectedIndex = 0;

            }

            FutureOilViscosityInput.Text = "";
            if (FutureOilViscosityUnit.Items.Count == 0)
            {

                FutureOilViscosityUnit.Items.Add("cp");
                FutureOilViscosityUnit.SelectedIndex = 0;

            }

            FutureOilFormationVolumeFactorInput.Text = "";
            if (FutureOilFormationVolumeFactorUnit.Items.Count == 0)
            {

                FutureOilFormationVolumeFactorUnit.Items.Add("bbl/STB");
                FutureOilFormationVolumeFactorUnit.SelectedIndex = 0;

            }

            TestFlowEfficiecyInput.Text = "";
            if (TestFlowEfficiencyUnit.Items.Count == 0)
            {

                TestFlowEfficiencyUnit.Items.Add("dimensionless");
                TestFlowEfficiencyUnit.SelectedIndex = 0;

            }

            NewFlowEfficiencyInput.Text = "";
            if (NewFlowEfficincyUnit.Items.Count == 0)
            {

                NewFlowEfficincyUnit.Items.Add("dimensionless");
                NewFlowEfficincyUnit.SelectedIndex = 0;

            }

            WellExponentInput.Text = "";
            if (WellExponentUnit.Items.Count == 0)
            {

                WellExponentUnit.Items.Add("dimensionless");
                WellExponentUnit.SelectedIndex = 0;

            }


            FlowCoefficientInput.Text = "";
            if (FlowCoefficientUnit.Items.Count == 0)
            {

                FlowCoefficientUnit.Items.Add("STB/(day·psiaⁿ)");
                FlowCoefficientUnit.SelectedIndex = 0;

            }



            IPRCurveLabelInput.Text = "IPR";

            MinimumPressureInput.Text = "0";
            PressureStepSizeInput.Text = "10";

            ReservoirTypeOil.IsChecked = true;
            IPRScenarioPresent.IsChecked = true;
            IPRMethodSelectedVogel.IsChecked = true;

            ReservoirDataTabContorl.SelectedIndex = 0;
            GeneralIPRControlTabControl.SelectedIndex = 0;

            UpdateUIBasedOnMethod();

        }

        private void SetVogelScreen()
        {

            GeneralMethods.EnableControl(PresentReservoirPressureStackPanel);
            GeneralMethods.EnableControl(BubblePointPressureStackPanel);
            GeneralMethods.EnableControl(TestDataDataGrid);

            GeneralMethods.DisableControl(PresentOilRelativePermeabilityStackPanel);
            GeneralMethods.DisableControl(PresentOilViscosityStackPanel);
            GeneralMethods.DisableControl(PresentOilFormationVolumeFactorStackPanel);

            GeneralMethods.DisableControl(FutureReservoirDataTab);

            GeneralMethods.DisableControl(AddNewTestDataRowButton);
            GeneralMethods.DisableControl(UseTestDataCheckBox);

            GeneralMethods.DisableControl(TestFlowEfficiencyStackPanel);
            GeneralMethods.DisableControl(NewFlowEfficiencyStackPanel);
            GeneralMethods.DisableControl(WellExponentStackPanel);
            GeneralMethods.DisableControl(FlowCoefficientStackPanel);

            _testData.Clear();

            _testData.Add(new(0, 0));

        }

        private void SetStandingScreent(bool IsFuture)
        {

            GeneralMethods.EnableControl(PresentReservoirPressureStackPanel);
            GeneralMethods.EnableControl(BubblePointPressureStackPanel);
            GeneralMethods.EnableControl(TestDataDataGrid);

            if (IsFuture)
            {

                GeneralMethods.EnableControl(PresentOilRelativePermeabilityStackPanel);
                GeneralMethods.EnableControl(PresentOilViscosityStackPanel);
                GeneralMethods.EnableControl(PresentOilFormationVolumeFactorStackPanel);

                GeneralMethods.EnableControl(FutureReservoirDataTab);

                GeneralMethods.EnableControl(FutureOilRelativePermeabilityStackPanel);
                GeneralMethods.EnableControl(FutureOilViscosityStackPanel);
                GeneralMethods.EnableControl(FutureOilFormationVolumeFactorStackPanel);

                GeneralMethods.EnableControl(TestFlowEfficiencyStackPanel);
                TestFlowEfficiecyInput.Text = "1";
                GeneralMethods.DisableControl(NewFlowEfficiencyStackPanel);

            }
            else
            {

                GeneralMethods.DisableControl(PresentOilRelativePermeabilityStackPanel);
                GeneralMethods.DisableControl(PresentOilViscosityStackPanel);
                GeneralMethods.DisableControl(PresentOilFormationVolumeFactorStackPanel);

                GeneralMethods.DisableControl(FutureReservoirDataTab);

                GeneralMethods.DisableControl(FutureOilRelativePermeabilityStackPanel);
                GeneralMethods.DisableControl(FutureOilViscosityStackPanel);
                GeneralMethods.DisableControl(FutureOilFormationVolumeFactorStackPanel);

                GeneralMethods.EnableControl(TestFlowEfficiencyStackPanel);
                GeneralMethods.EnableControl(NewFlowEfficiencyStackPanel);

            }


            GeneralMethods.DisableControl(AddNewTestDataRowButton);
            GeneralMethods.DisableControl(UseTestDataCheckBox);


            GeneralMethods.DisableControl(WellExponentStackPanel);
            GeneralMethods.DisableControl(FlowCoefficientStackPanel);

            _testData.Clear();

            _testData.Add(new(0, 0));

        }

        private void SetFetkovichScreen(bool IsFuture)
        {

            GeneralMethods.EnableControl(PresentReservoirPressureStackPanel);
            GeneralMethods.EnableControl(BubblePointPressureStackPanel);
            GeneralMethods.EnableControl(TestDataDataGrid);

            if (IsFuture)
            {

                GeneralMethods.EnableControl(FutureReservoirDataTab);
                GeneralMethods.EnableControl(FutureReservoirPressureStackPanel);

            }
            else
            {

                GeneralMethods.DisableControl(FutureReservoirDataTab);
                GeneralMethods.EnableControl(FutureReservoirPressureStackPanel);

            }


            GeneralMethods.EnableControl(AddNewTestDataRowButton);
            GeneralMethods.EnableControl(UseTestDataCheckBox);

            GeneralMethods.DisableControl(TestFlowEfficiencyStackPanel);
            GeneralMethods.DisableControl(NewFlowEfficiencyStackPanel);

            GeneralMethods.DisableControl(PresentOilRelativePermeabilityStackPanel);
            GeneralMethods.DisableControl(PresentOilViscosityStackPanel);
            GeneralMethods.DisableControl(PresentOilFormationVolumeFactorStackPanel);

            GeneralMethods.DisableControl(FutureOilRelativePermeabilityStackPanel);
            GeneralMethods.DisableControl(FutureOilViscosityStackPanel);
            GeneralMethods.DisableControl(FutureOilFormationVolumeFactorStackPanel);

            GeneralMethods.DisableControl(WellExponentStackPanel);
            GeneralMethods.DisableControl(FlowCoefficientStackPanel);

            UseTestDataCheckBox.IsChecked = true;

            _testData.Clear();

            _testData.Add(new(0, 0));
            _testData.Add(new(0, 0));
            _testData.Add(new(0, 0));


        }

        private void SetJonesScreen()
        {

            GeneralMethods.EnableControl(PresentReservoirPressureStackPanel);
            GeneralMethods.EnableControl(BubblePointPressureStackPanel);
            GeneralMethods.EnableControl(TestDataDataGrid);

            GeneralMethods.DisableControl(PresentOilRelativePermeabilityStackPanel);
            GeneralMethods.DisableControl(PresentOilViscosityStackPanel);
            GeneralMethods.DisableControl(PresentOilFormationVolumeFactorStackPanel);

            GeneralMethods.DisableControl(FutureReservoirDataTab);

            GeneralMethods.EnableControl(AddNewTestDataRowButton);
            GeneralMethods.DisableControl(UseTestDataCheckBox);

            GeneralMethods.DisableControl(TestFlowEfficiencyStackPanel);
            GeneralMethods.DisableControl(NewFlowEfficiencyStackPanel);
            GeneralMethods.DisableControl(WellExponentStackPanel);
            GeneralMethods.DisableControl(FlowCoefficientStackPanel);

            _testData.Clear();

            _testData.Add(new(0, 0));
            _testData.Add(new(0, 0));
            _testData.Add(new(0, 0));

        }

        private void UpdateUIBasedOnMethod()
        {

            bool IsVogel = IPRMethodSelectedVogel.IsChecked.Value;
            bool IsStanding = IPRMethodSelectedStanding.IsChecked.Value;
            bool IsFetkovich = IPRMethodSelectedFetkovich.IsChecked.Value;
            bool IsJones = IPRMethodSelectedJones.IsChecked.Value;
            bool IsFuture = IPRScenarioFuture.IsChecked.Value;
            bool IsUseTestData = UseTestDataCheckBox.IsChecked.Value;

            GeneralMethods.DisableControl(ReservoirTypeGas); // since gas is not supported yet.

            if (IsFuture)
            {

                if (IsVogel || IsJones)
                {

                    IPRMethodSelectedStanding.IsChecked = true;
                    SetStandingScreent(IsFuture);

                }


                GeneralMethods.DisableControl(IPRMethodSelectedVogel);
                GeneralMethods.DisableControl(IPRMethodSelectedJones);

            }
            else
            {

                GeneralMethods.EnableControl(IPRMethodSelectedVogel);
                GeneralMethods.EnableControl(IPRMethodSelectedJones);

                ReservoirDataTabContorl.SelectedIndex = 0;

            }

            if (IsVogel && !IsFuture)
                SetVogelScreen();

            if (IsStanding)
                SetStandingScreent(IsFuture);

            if (IsFetkovich)
                SetFetkovichScreen(IsFuture);

            if (IsJones && !IsFuture)
                SetJonesScreen();

        }

        private void AdjustUIBasedOnMethod(object sender, RoutedEventArgs e)
        {

            UpdateUIBasedOnMethod();

        }

        private void UseTestData(object sender, RoutedEventArgs e)
        {

            if (UseTestDataCheckBox.IsChecked.Value)
            {

                GeneralMethods.EnableControl(TestDataDataGrid);
                GeneralMethods.EnableControl(AddNewTestDataRowButton);

                GeneralMethods.DisableControl(WellExponentStackPanel);
                GeneralMethods.DisableControl(FlowCoefficientStackPanel);

                _testData.Clear();
                _testData.Add(new(0, 0));
                _testData.Add(new(0, 0));
                _testData.Add(new(0, 0));

            }
            else
            {

                GeneralMethods.DisableControl(AddNewTestDataRowButton);

                GeneralMethods.EnableControl(WellExponentStackPanel);
                GeneralMethods.EnableControl(FlowCoefficientStackPanel);

                _testData.Clear();
                _testData.Add(new(0, 0));


            }

        }

        private void AddNewTestDataRow(object sender, RoutedEventArgs e)
        {

            _testData.Add(new FlowDataRow(0,0));

        }

        private System.Drawing.Color? SelectColor()
        {
            ColorDialog dialog = new();

            if (dialog.ShowDialog() == DialogResult.OK)
                return dialog.Color;

            return null;
        }

        private void OpenColorDialog(object sender, RoutedEventArgs e)
        {

            IPRCurveColorInput.Text = SelectColor().ToString();

        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;

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

        private void DecimalOnly(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;

            if (!char.IsDigit(e.Text, 0))
            {
                if (e.Text != "." || textBox.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void DataGrid_PreparingCellForEdit(object sender,
        DataGridPreparingCellForEditEventArgs e)
        {
            if (e.EditingElement is System.Windows.Controls.TextBox textBox)
            {
                textBox.PreviewTextInput -= DecimalOnly;
                textBox.PreviewTextInput += DecimalOnly;
            }
        }

    }

}