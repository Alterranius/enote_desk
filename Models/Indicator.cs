using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Models
{
    public class Indicator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public string Description { get; set; }
        public string Formulae { get; set; }
        public List<Metric> Metrics { get; set; }
        public List<Indicator> Indicators { get; set; }
        public List<Indicator> IndicatorsJoin { get; set; }
        public List<Project> Projects { get; set; }
        public List<Depart> Departs { get; set; }
        public List<Team> Teams { get; set; }
    }
}
