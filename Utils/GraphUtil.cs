using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Utils
{
    public static class GraphUtil
    {
        public static double[] GetDates(Dictionary<string, string> data)
        {
            double[] result = new double[data.Keys.Count];
            int i = 0;
            foreach (KeyValuePair<string, string> pair in data)
            {
                result[i] = Convert.ToDateTime(pair.Key).ToOADate();
                i++;
            }
            return result;
        }

        public static List<ScottPlot.Plottable.Bar> GetBarSeries(Dictionary<string, string> data)
        {
            List<ScottPlot.Plottable.Bar> result = new List<Bar>();
            int i = 0;
            foreach (KeyValuePair<string, string> pair in data)
            {
                result.Add(new Bar()
                {
                    Value = Convert.ToDouble(pair.Value),
                    Position = i,
                    Label = pair.Key,
                    IsVertical = false
                });
                i++;
            }
            return result;
        }

        public static double[] GetValues(Dictionary<string, string> data)
        {
            double[] result = new double[data.Values.Count];
            int i = 0;
            foreach (KeyValuePair<string, string> pair in data)
            {
                result[i] = Convert.ToDouble(pair.Value);
                i++;
            }
            return result;
        }

        public static double[] GetPies(params string[] values)
        {
            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = Convert.ToDouble(values[i]);
            }
            return result;
        }

        public static void PieConfigure(PiePlot pie, params System.Drawing.Color[] colors)
        {
            pie.Explode = true;
            pie.DonutSize = .6;
            pie.SliceLabelPosition = 0.6;
            pie.Size = .7;
            pie.ShowLabels = true;
            pie.SliceLabelColors = colors;
            pie.SliceFillColors = colors;
        }
    }
}
