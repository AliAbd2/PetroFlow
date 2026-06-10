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
    /// Interaction logic for IPRScreen.xaml
    /// </summary>
    public partial class IPRScreen : UserControl
    {
        public IPRScreen()
        {
            InitializeComponent();

            _initiateTheMenu();

        }


        private void _initiateTheMenu()
        {

            UseTestDataCheckBox.IsChecked = true;

            _adjustUIBasedOnMethod();

        }

        private void _adjustUIBasedOnMethod()
        {

            bool IsVogel = IPRMethodSelectedVogel.IsChecked.Value;
            bool IsStanding = IPRMethodSelectedStanding.IsChecked.Value;
            bool IsFetkovich = IPRMethodSelectedFetkovich.IsChecked.Value;
            bool IsJones = IPRMethodSelectedJones.IsChecked.Value;
            bool IsFuture = IPRScenarioFuture.IsChecked.Value;
            bool IsUseTestData = UseTestDataCheckBox.IsChecked.Value;

            GeneralMethods.DisableControl(ReservoirTypeGas);

            if (IsFuture)
            {

                GeneralMethods.DisableControl(IPRMethodSelectedVogel);
                GeneralMethods.DisableControl(IPRMethodSelectedJones);

                if (IsVogel || IsJones)
                    IPRMethodSelectedStanding.IsChecked = true;

            }
            else
            {

                GeneralMethods.EnableControl(IPRMethodSelectedVogel);
                GeneralMethods.EnableControl(IPRMethodSelectedJones);

            }

            if (IsVogel)
            {

            }
            else
            {

            }

            if (IsStanding)
            {

                GeneralMethods.EnableControl(TestFlowEfficiencyLabel);
                GeneralMethods.EnableControl(TestFlowEfficiecyInput);
                GeneralMethods.EnableControl(TestFlowEfficiencyUnit);

                GeneralMethods.EnableControl(NewFlowEfficiencyLabel);
                GeneralMethods.EnableControl(NewFlowEfficiencyInput);
                GeneralMethods.EnableControl(NewFlowEfficincyUnit);

            }
            else
            {

                GeneralMethods.DisableControl(TestFlowEfficiencyLabel);
                GeneralMethods.DisableControl(TestFlowEfficiecyInput);
                GeneralMethods.DisableControl(TestFlowEfficiencyUnit);

                GeneralMethods.DisableControl(NewFlowEfficiencyLabel);
                GeneralMethods.DisableControl(NewFlowEfficiencyInput);
                GeneralMethods.DisableControl(NewFlowEfficincyUnit);

            }

            if (IsStanding && IsFuture)
            {

                GeneralMethods.EnableControl(FutureReservoirDataTab);

                GeneralMethods.EnableControl(PresentOilRelativePremeabilityLabel);
                GeneralMethods.EnableControl(PresentOilRelativePermeabilityInput);
                GeneralMethods.EnableControl(PresentOilRelativePermeabilityUnit);

                GeneralMethods.EnableControl(PresentOilViscosityLabel);
                GeneralMethods.EnableControl(PresentOilViscosityInput);
                GeneralMethods.EnableControl(PresentOilViscosityUnit);

                GeneralMethods.EnableControl(PresentOilFormationVolumeFactorLabel);
                GeneralMethods.EnableControl(PresentOilFormationVolumeFactorInput);
                GeneralMethods.EnableControl(PresentOilFormationVolumeFactorUnit);

            }
            else
            {

                GeneralMethods.DisableControl(FutureReservoirDataTab);

                GeneralMethods.DisableControl(PresentOilRelativePremeabilityLabel);
                GeneralMethods.DisableControl(PresentOilRelativePermeabilityInput);
                GeneralMethods.DisableControl(PresentOilRelativePermeabilityUnit);

                GeneralMethods.DisableControl(PresentOilViscosityLabel);
                GeneralMethods.DisableControl(PresentOilViscosityInput);
                GeneralMethods.DisableControl(PresentOilViscosityUnit);

                GeneralMethods.DisableControl(PresentOilFormationVolumeFactorLabel);
                GeneralMethods.DisableControl(PresentOilFormationVolumeFactorInput);
                GeneralMethods.DisableControl(PresentOilFormationVolumeFactorUnit);

            }


            if (IsFetkovich)
            {

                GeneralMethods.EnableControl(AddNewTestDataRowButton);

                GeneralMethods.EnableControl(UseTestDataCheckBox);

            }
            else
            {

                GeneralMethods.DisableControl(AddNewTestDataRowButton);

                GeneralMethods.DisableControl(UseTestDataCheckBox);

            }


        }

        private void AdjustUIBasedOnMethod(object sender, RoutedEventArgs e)
        {

            _adjustUIBasedOnMethod();

        }

    }

}