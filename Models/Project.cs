using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class Project : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Specialization { get; set; }
        public string Mission { get; set; }
        public string Product { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime HardDeadline { get; set; }
        public Methodology Methodology { get; set; }
        public List<Role> Roles { get; set; }
        public List<Plan> Plans { get; set; }
        public List<Indicator> Indicators { get; set; }
        public List<Metric> Metrics { get; set; }
        public List<Depart> Departs { get; set; }

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
                    case "Prefix":
                        if (string.IsNullOrEmpty(Prefix) || !(Prefix.Length >= 1 && Prefix.Length <= 5))
                        {
                            error = "Префикс должно быть от 1 до 5 символов";
                        }
                        break;
                    case "Specialization":
                        if (string.IsNullOrEmpty(Specialization) || !(Specialization.Length > 1 && Specialization.Length < 50))
                        {
                            error = "Назначение должно быть от 2 до 50 символов";
                        }
                        break;
                    case "Product":
                        if (string.IsNullOrEmpty(Product) || !(Product.Length > 1 && Product.Length < 100))
                        {
                            error = "Продукт должен быть от 2 до 100 символов";
                        }
                        break;
                    case "Mission":
                        if (string.IsNullOrEmpty(Mission) || !(Mission.Length > 1 && Mission.Length < 100))
                        {
                            error = "Миссия должна быть от 2 до 100 символов";
                        }
                        break;
                    case "Deadline":
                        if (Deadline == null)
                        {
                            error = "Дата не должна быть пустой";
                        }
                        break;
                    case "HardDeadline":
                        if (HardDeadline == null)
                        {
                            error = "Крайний срок не должен быть пустой";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public Project()
        {
        }

        public Project(string name, string prefix, string specialization, string mission, string product, DateTime createdAt, DateTime deadline, DateTime hardDeadline, Methodology methodology)
        {
            Name = name;
            Prefix = prefix;
            Specialization = specialization;
            Mission = mission;
            Product = product;
            CreatedAt = createdAt;
            Deadline = deadline;
            HardDeadline = hardDeadline;
            Methodology = methodology;
        }
    }
}
