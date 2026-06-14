using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PetroFlow_PersentationLayer.Utility
{
    public static class GeneralMethods
    {

        public static void DisableControl(FrameworkElement element)
        {

            element.Opacity = 0.4;
            element.IsEnabled = false;

        }

        public static void EnableControl(FrameworkElement element)
        {

            element.Opacity = 1;
            element.IsEnabled = true;

        }

        public static ScottPlot.Color GetThemeColor(string resourceKey)
        {
            var brush =
                (SolidColorBrush)Application.Current.Resources[resourceKey];

            return new ScottPlot.Color(
                brush.Color.R,
                brush.Color.G,
                brush.Color.B,
                brush.Color.A);
        }

        public static double? ReadDouble(TextBox textBox)
        {

            string text = textBox.Text;

            if (double.TryParse(text, out double value))
                return value;
            else
                return null;

        }


    }
}
