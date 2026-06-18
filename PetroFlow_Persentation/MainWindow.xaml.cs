using System.Windows;

namespace PetroFlow_Persentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();

        }

        private void GeneratIPR(object sender, EventArgs e)
        {

            

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

                IPRScreen.ResetMenu();
                VLPScreen.ResetMenu();
                ResultScreen.ResetMenu();

            }

        }

    }
}