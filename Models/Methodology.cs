using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Models
{
    public class Methodology
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thesis { get; set; }
        public string Description { get; set; }
        public List<Project> Projects { get; set; }
    }
}
