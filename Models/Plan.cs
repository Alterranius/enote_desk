using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class Plan : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Aim { get; set; }
        public PlanType PlanType { get; set; }
        public Project Project { get; set; }
        public Depart Depart { get; set; }
        public Team Team { get; set; }
        public List<PlanContent> Content { get; set; }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name) || !(Name.Length > 1 && Name.Length < 50))
                        {
                            error = "Название должно быть от 2 до 50 символов";
                        }
                        break;
                    case "Aim":
                        if (string.IsNullOrEmpty(Aim) || !(Aim.Length > 1 && Aim.Length < 200))
                        {
                            error = "Цель должна быть от 2 до 200 символов";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public Plan()
        {
        }

        public Plan(string name, string aim, PlanType planType, Project project, Depart depart, Team team)
        {
            Name = name;
            Aim = aim;
            PlanType = planType;
            Project = project;
            Depart = depart;
            Team = team;
        }
    }
}
