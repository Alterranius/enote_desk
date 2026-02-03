using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class PlanContent : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Aim { get; set; }
        public int Position { get; set; }
        public Plan Plan { get; set; }

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
                    case "Description":
                        if (string.IsNullOrEmpty(Description) || !(Description.Length > 1 && Description.Length < 200))
                        {
                            error = "Описание должно быть от 2 до 200 символов";
                        }
                        break;
                    case "Aim":
                        if (string.IsNullOrEmpty(Aim) || !(Aim.Length > 1 && Aim.Length < 200))
                        {
                            error = "Цель должна быть от 2 до 200 символов";
                        }
                        break;
                    case "Position":
                        if (Position < 0)
                        {
                            error = "Позиция должна быть положительна";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public PlanContent()
        {
        }

        public PlanContent(string name, string description, string aim, int position, Plan plan)
        {
            Name = name;
            Description = description;
            Aim = aim;
            Position = position;
            Plan = plan;
        }
    }
}
