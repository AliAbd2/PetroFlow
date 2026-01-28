using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{
    public class clsIPRRepository
    {

        private readonly List<IIPRMethod> _curves = new();

        private readonly List<ScottPlot.Color> CurvePalette = new()
        {
            ScottPlot.Color.FromHex("#1f77b4"), // Blue
            ScottPlot.Color.FromHex("#ff7f0e"), // Orange
            ScottPlot.Color.FromHex("#2ca02c"), // Green
            ScottPlot.Color.FromHex("#d62728"), // Red
            ScottPlot.Color.FromHex("#9467bd"), // Purple
            ScottPlot.Color.FromHex("#8c564b"), // Brown
            ScottPlot.Color.FromHex("#e377c2"), // Pink
            ScottPlot.Color.FromHex("#7f7f7f"), // Gray
            ScottPlot.Color.FromHex("#bcbd22"), // Olive
            ScottPlot.Color.FromHex("#17becf"), // Cyan
            ScottPlot.Color.FromHex("#393b79"), // Dark Indigo
            ScottPlot.Color.FromHex("#637939"), // Dark Olive Green
            ScottPlot.Color.FromHex("#8c6d31"), // Dark Golden Brown
            ScottPlot.Color.FromHex("#843c39"), // Deep Brick
            ScottPlot.Color.FromHex("#7b4173"), // Dark Plum
            ScottPlot.Color.FromHex("#3182bd"), // Strong Blue
            ScottPlot.Color.FromHex("#31a354"), // Strong Green
            ScottPlot.Color.FromHex("#756bb1"), // Soft Purple
            ScottPlot.Color.FromHex("#636363"), // Charcoal
            ScottPlot.Color.FromHex("#e6550d"), // Deep Orange
        };

        public void Add(IIPRMethod method, ref string Name)
        {

            method.Name = Name = GenerateUniqueCurveName(Name);
            method.CurvePlotSetting.LineColor = 
                method.CurvePlotSetting.MarkerColor = GetUniqueColor();

            _curves.Add(method);
        }

        public void Remove(string name)
        {

            var Curve = _curves.FirstOrDefault(c => c.Name == name);
            if (Curve != null)
                _curves.Remove(Curve);

        }

        public IIPRMethod Get(string name)
        {

            return _curves.FirstOrDefault(c => c.Name == name);

        }

        public List<IIPRMethod> GetAll()
        {
            return _curves.ToList();
        }

        public List<string> GetAllCurvesNames()
        {
            return _curves.Select(c => c.Name).ToList();
        }

        private string GenerateUniqueCurveName(string baseName)
        {

            // Generates a unique curve name when no explicit name is provided
            // by appending an incremental numeric suffix if necessary.

            int curveNumber = 1;

            List<string> existingCurveNames = GetAll().Select(x => x.Name).ToList();

            string curveName = baseName;

            while (existingCurveNames.Contains(curveName))
            {
                curveName = $"IPR {curveNumber}";
                curveNumber++;
            }

            return curveName;

        }

        private ScottPlot.Color GetUniqueColor()
        {
            var usedColors = GetAll()
                .Select(x => x.CurvePlotSetting.LineColor)
                .ToHashSet();

            foreach (var paletteColor in CurvePalette)
            {
                if (!usedColors.Contains(paletteColor))
                    return paletteColor;
            }

            // If all colors are used, cycle deterministically
            return CurvePalette[usedColors.Count % CurvePalette.Count];
        }

    }
}
