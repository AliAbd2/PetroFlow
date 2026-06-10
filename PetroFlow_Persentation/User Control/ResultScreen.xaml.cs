using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for ResultScreen.xaml
    /// </summary>
    public partial class ResultScreen : UserControl
    {
        public ResultScreen()
        {
            InitializeComponent();

            ResultPlot.Plot.FigureBackground.Color = ScottPlot.Color.FromColor(System.Drawing.Color.Transparent);
            ResultPlot.Plot.DataBackground.Color = ScottPlot.Color.FromHex("#111827");

            ResultPlot.Plot.Axes.Color(ScottPlot.Color.FromHex("#C9D1D9"));

            ResultPlot.Plot.Grid.MajorLineColor =
                ScottPlot.Color.FromHex("#30363D");

        }



    }
}
