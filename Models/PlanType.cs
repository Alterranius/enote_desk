using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Models
{
    public class PlanType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Plan> Plans { get; set; }

        public PlanType()
        {
        }

        public PlanType(int id, string name, string description, List<Plan> plans)
        {
            Id = id;
            Name = name;
            Description = description;
            Plans = plans;
        }
    }
}
