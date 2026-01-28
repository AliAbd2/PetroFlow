using ScottPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData
{
    public class clsCurvePlotSettings
    {

        public string LegendText { get; set; }

        public ScottPlot.LinePattern LinePattern { get; set; }
        public int LineWidth { get; set; }
        public ScottPlot.Color LineColor { get; set; }

        public MarkerShape MarkerShape { get; set; }
        public int MarkerSize { get; set; }
        public ScottPlot.Color MarkerColor { get; set; }

        public clsCurvePlotSettings()
        {
            LinePattern = ScottPlot.LinePattern.Solid;
            MarkerShape = MarkerShape.None;
            LineWidth = 1;
            MarkerSize = 0;
        }

        public clsCurvePlotSettings(string legendText, ScottPlot.LinePattern linePattren, int lineWidth,
            Color lineColor, ScottPlot.MarkerShape markerShape, int markeSize, Color markerColor)
        {

            LegendText = legendText;
            LinePattern = linePattren;
            LineWidth = lineWidth;
            LineColor = lineColor;
            MarkerShape = markerShape;
            MarkerSize = markeSize;
            MarkerColor = markerColor;

        }


    }
}
