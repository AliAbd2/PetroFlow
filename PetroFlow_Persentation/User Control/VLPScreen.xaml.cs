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

            _adjustUIBasedOnMethod();

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

    }
}
