using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_PresentationLayer
{
    public class clsCurvePlotter
    {

        private readonly ScottPlot.Plot _plot;

        public clsCurvePlotter(ScottPlot.Plot plot)
        {
            _plot = plot;
        }

        public void PlotScatter(List<double> x, List<double> y, clsCurvePlotSettings settings, string name)
        {

            Scatter plot = _plot.Add.Scatter(x, y);

            plot.LegendText = settings.LegendText;
            plot.LineColor = settings.LineColor;
            plot.LinePattern = settings.LinePattern;
            plot.LineWidth = settings.LineWidth;
            plot.MarkerColor = settings.MarkerColor;
            plot.MarkerShape = settings.MarkerShape;
            plot.MarkerSize = settings.MarkerSize;
            

        }

    }
}
