using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.ShearedData;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_PresentationLayer
{
    public class CurvePlotter
    {

        private readonly ScottPlot.Plot _plot;

        public CurvePlotter(ScottPlot.Plot plot)
        {
            _plot = plot;
        }

        public void PlotScatter(List<double> x, List<double> y, CurvePlotSettings settings, string name)
        {

            Scatter plot = _plot.Add.Scatter(x, y);

            plot.LegendText = settings.LegendText;
            plot.LinePattern = ScottPlot.LinePattern.Solid;
            plot.LineWidth = 2;
            plot.MarkerShape = ScottPlot.MarkerShape.FilledCircle;
            plot.MarkerSize = 1;
            

        }

    }
}
