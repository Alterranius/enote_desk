using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; }
        public Team Team { get; set; }
        public Depart Depart { get; set; }
    }
}
