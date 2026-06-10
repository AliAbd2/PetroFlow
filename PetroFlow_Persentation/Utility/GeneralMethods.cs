using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

    }
}
