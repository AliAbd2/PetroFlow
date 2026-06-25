using PetroFlow_BusinessLayer.General_Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Main__IPR_Classes;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_PersentationLayer.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;


namespace PetroFlow_Persentation.User_Control
{
    /// <summary>
    /// Interaction logic for IPRScreen.xaml
    /// </summary>
    public partial class IPRScreen : System.Windows.Controls.UserControl
    {

        private InFlowPerformanceRelationship _iPR = new();

        private ObservableCollection<FlowDataRow> _testData = new();

        public IPRScreen()
        {
            InitializeComponent();

            ResetMenu();

        }

        public IPROutput Calculate()
        {

            IPRRequest request = ReadRequest();

            return _iPR.ProcessRequest(request);

        }

        private IPRRequest ReadRequest()
        {

            IPRMethodType methodType = (IPRMethodType)IPRMethodsComboBox.SelectedValue;
            IPRScenarioType iPRScenario = 
                IPRScenarioPresent.IsChecked.Value ? IPRScenarioType.Present : IPRScenarioType.Future;

            IPRInputData inputData = ReadIPRInputData();

            return new IPRRequest(methodType, iPRScenario, inputData);

        }
        
        private List<FlowDataRow> ReadTestData()
        {

            if (_testData.Any(x => double.IsNaN(x.FlowRate)))
                throw new InvalidParameterException(new ErrorMessage(
                    "Invalid Test Data", "One or more test data flow rate has not been provided."));

            if (_testData.Any(x => double.IsNaN(x.BottomHolePressure)))
                throw new InvalidParameterException(new ErrorMessage(
                    "Invalid Test Data", "One or more test data bottom hole pressure has not been provided."));

            return _testData.ToList();

        }

        private IPRInputData ReadIPRInputData()
        {

            IPRInputData inputData = new();

            inputData.ReservoirPressure = GeneralMethods.ReadDouble(PresentReservoirPressureInput);
            inputData.BubblePointPressure = GeneralMethods.ReadDouble(BubblePointPressureInput);

            inputData.TestsData = ReadTestData();

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

            return inputData;

        }

        private void ResetMenu()
        {

            _testData.Clear();
            TestDataDataGrid.ItemsSource = _testData;

            LoadAvailableMethods();

            PresentReservoirPressureInput.Text = "";
            GeneralMethods.InitializeUnit(
                PresentReservoirPressureUnit, "psig");

            BubblePointPressureInput.Text = "";
            GeneralMethods.InitializeUnit(
                BubblePointPressureUnit, "psig");

            PresentOilRelativePermeabilityInput.Text = "";
            GeneralMethods.InitializeUnit(
                PresentOilRelativePermeabilityUnit, "fraction");

            PresentOilViscosityInput.Text = "";
            GeneralMethods.InitializeUnit(
                PresentOilViscosityUnit, "cp");

            PresentOilFormationVolumeFactorInput.Text = "";
            GeneralMethods.InitializeUnit(
                PresentOilFormationVolumeFactorUnit, "bbl/STB");

            FutureReservoirPressureInput.Text = "";
            GeneralMethods.InitializeUnit(
                FutureReservoirPressureUnit, "psig");

            FutureOilRelativePermeabilityInput.Text = "";
            GeneralMethods.InitializeUnit(
                FutureOilRelativePermeabilityUnit, "fraction");

            FutureOilViscosityInput.Text = "";
            GeneralMethods.InitializeUnit(
                FutureOilViscosityUnit, "cp");

            FutureOilFormationVolumeFactorInput.Text = "";
            GeneralMethods.InitializeUnit(
                FutureOilFormationVolumeFactorUnit, "bbl/STB");

            TestFlowEfficiecyInput.Text = "";
            GeneralMethods.InitializeUnit(
                TestFlowEfficiencyUnit, "dimensionless");

            WellExponentInput.Text = "";
            GeneralMethods.InitializeUnit(
                WellExponentUnit, "dimensionless");

            FlowCoefficientInput.Text = "";
            GeneralMethods.InitializeUnit(
                FlowCoefficientUnit, "STB/(day·psiaⁿ)");


            IPRCurveLabelInput.Text = "IPR";

            MinimumPressureInput.Text = "0";
            PressureStepSizeInput.Text = "10";

            ReservoirTypeOil.IsChecked = true;
            IPRScenarioPresent.IsChecked = true;

            ReservoirDataTabContorl.SelectedIndex = 0;
            GeneralIPRControlTabControl.SelectedIndex = 0;

            UpdateUIBasedOnMethod();

        }

        private IPRMethodFeatures GetRequiredFeatures()
        {

            IPRMethodFeatures features = IPRMethodFeatures.None;

            if (ReservoirTypeOil.IsChecked.Value)
                features |= IPRMethodFeatures.Oil;

            if (IPRScenarioFuture.IsChecked.Value)
                features |= IPRMethodFeatures.FuturePrediction;


            return features;

        }

        private void LoadAvailableMethods()
        {

            IPRMethodFeatures features = GetRequiredFeatures();

            IPRMethodsComboBox.ItemsSource =
                _iPR.GetAvailableMethods(features);

            IPRMethodsComboBox.DisplayMemberPath = nameof(IPRMethodBase.DisplayName);
            IPRMethodsComboBox.SelectedValuePath = nameof(IPRMethodBase.MethodType);

            IPRMethodsComboBox.SelectedIndex = 0;


        }

        private void UpdateUIBasedOnMethod()
        {

            GeneralMethods.DisableControl(ReservoirTypeGas);

            if (IPRMethodsComboBox.SelectedValue == null)
                IPRMethodsComboBox.SelectedIndex = 0;

            if (IPRMethodsComboBox.SelectedValue is not IPRMethodType methodType)
            {
                throw new InvalidOperationException(
                    "No IPR method selected.");
            }

            methodType = (IPRMethodType)IPRMethodsComboBox.SelectedValue;
            IPRMethodBase iPRMethod = _iPR.GetIPRMethod(methodType);

            SetPresentMenu(iPRMethod.InputRequirements.Present);

            SetFutureMenu(iPRMethod.InputRequirements.Future);

            SetTestData(iPRMethod.InputRequirements.TestData);

        }

        private void SetPresentMenu(IPRInputFields presentRequirements)
        {


            //============================
            // --- Reservoir Pressure ---
            //============================
            bool prCondtion = presentRequirements
                .HasFlag(IPRInputFields.ReservoirPressure);

            GeneralMethods.SetControlState(prCondtion,
                PresentReservoirPressureStackPanel);

            //===============================
            // --- Bubble Point Pressure ---
            //===============================
            bool pbCondition = presentRequirements
                .HasFlag(IPRInputFields.BubblePointPressure);

            GeneralMethods.SetControlState(pbCondition,
                BubblePointPressureStackPanel);

            //===============================
            // --- Test Flow Efficiency ---
            //===============================
            bool feCondition = presentRequirements
                .HasFlag(IPRInputFields.TestFlowEfficiency)
                && !IPRScenarioFuture.IsChecked.Value;

            GeneralMethods.SetControlState(feCondition,
                TestFlowEfficiencyStackPanel);

            //=======================
            // --- Well Exponent ---
            //=======================
            bool nCondition = presentRequirements
                .HasFlag(IPRInputFields.WellExponent);

            GeneralMethods.SetControlState(nCondition,
                WellExponentStackPanel);

        }

        private void SetFutureMenu(IPRFutureInputFields futureRequirements)
        {


            bool isFuture = IPRScenarioFuture.IsChecked.Value;

            bool futureCondition = !(futureRequirements
                .HasFlag(IPRFutureInputFields.None) || !isFuture);

            GeneralMethods.SetControlState(futureCondition,
                FutureReservoirDataTab);

            //===================================
            // --- Future Reservoir Pressure ---
            //===================================
            bool prfCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.FutureReservoirPressure) && isFuture;

            GeneralMethods.SetControlState(prfCondition,
                FutureReservoirPressureStackPanel);

            //==========================
            // --- Flow Coefficient ---
            //==========================
            bool cCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.FlowCoefficient) && isFuture;

            GeneralMethods.SetControlState(cCondition,
                FlowCoefficientStackPanel);

            //===========================================
            // --- Present Oil Relative Permeability ---
            //===========================================
            bool kropCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.PresentOilRelativePermeability)
                && isFuture;

            GeneralMethods.SetControlState(kropCondition,
                PresentOilRelativePermeabilityStackPanel);

            //===============================
            // --- Present Oil Viscosity ---
            //===============================
            bool muopCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.PresentOilViscosity)
                && isFuture;

            GeneralMethods.SetControlState(muopCondition,
                PresentOilViscosityStackPanel);

            //=============================================
            // --- Present Oil Formation Volume Factor ---
            //=============================================
            bool bopCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.PresentOilFormationVolumeFactor)
                && isFuture;

            GeneralMethods.SetControlState(bopCondition,
                PresentOilFormationVolumeFactorStackPanel);

            //===========================================
            // --- Future Oil Relative Permeability ---
            //===========================================
            bool krofCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.FutureOilRelativePermeability)
                && isFuture;

            GeneralMethods.SetControlState(krofCondition,
                FutureOilRelativePermeabilityStackPanel);

            //===============================
            // --- Future Oil Viscosity ---
            //===============================
            bool muofCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.FutureOilViscosity)
                && isFuture;

            GeneralMethods.SetControlState(muofCondition,
                FutureOilViscosityStackPanel);

            //=============================================
            // --- Future Oil Formation Volume Factor ---
            //=============================================
            bool bofCondition = futureRequirements
                .HasFlag(IPRFutureInputFields.FutureOilFormationVolumeFactor)
                && isFuture;

            GeneralMethods.SetControlState(bofCondition,
                FutureOilFormationVolumeFactorStackPanel);

        }

        private void AddNewTestDataRow()
        {
            _testData.Add(new FlowDataRow(double.NaN, double.NaN));
        }

        private void SetTestGridToNumberRows(int numberOfRows)
        {

            while (_testData.Count < numberOfRows)
            {
                AddNewTestDataRow();
            }

            while (_testData.Count > numberOfRows)
            {
                _testData.RemoveAt(
                    _testData.Count - 1);
            }


        }

        private void SetTestData(IPRTestDataRequirement dataRequirement)
        {

            switch (dataRequirement)
            {
                case IPRTestDataRequirement.None:

                    GeneralMethods.DisableControl(TestDataDataGrid);
                    GeneralMethods.DisableControl(AddNewTestDataRowButton);
                    break;

                case IPRTestDataRequirement.SinglePoint:

                    GeneralMethods.EnableControl(TestDataDataGrid);
                    SetTestGridToNumberRows(1);
                    GeneralMethods.DisableControl(AddNewTestDataRowButton);
                    break;

                case IPRTestDataRequirement.TwoPoints:

                    GeneralMethods.EnableControl(TestDataDataGrid);
                    SetTestGridToNumberRows(2);
                    GeneralMethods.DisableControl(AddNewTestDataRowButton);
                    break;

                case IPRTestDataRequirement.MultiplePoints:

                    GeneralMethods.EnableControl(TestDataDataGrid);
                    SetTestGridToNumberRows(3);
                    GeneralMethods.EnableControl(AddNewTestDataRowButton);
                    break;

                default:
                    throw new InvalidParameterException(
                        new ErrorMessage(
                            "Test Data Requirement Error",
                            "Unsupported test data requirement."));
            }

        }

        private void LoadAvailableMethodsEvent(object sender, RoutedEventArgs e)
        {

            LoadAvailableMethods();

        }

        private void AdjustUIBasedOnMethod(object sender, SelectionChangedEventArgs e)
        {

            UpdateUIBasedOnMethod();

        }

        private void AddNewTestDataRow(object sender, RoutedEventArgs e)
        {

            AddNewTestDataRow();

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