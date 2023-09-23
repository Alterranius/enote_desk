﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eNote_desk.Models
{
    public class Depart : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Product { get; set; }
        public Project Project { get; set; }

        public List<Role> Roles { get; set; }
        public List<Plan> Plans { get; set; }
        public List<Indicator> Indicators { get; set; }
        public List<Metric> Metrics { get; set; }
        public List<Team> Teams { get; set; }

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
                    case "Product":
                        if (string.IsNullOrEmpty(Product) || !(Product.Length > 1 && Product.Length < 100))
                        {
                            error = "Продукт должен быть от 2 до 100 символов";
                        }
                        break;
                    case "Project":
                        if (Project == null)
                        {
                            error = "Проект не должен быть пустым";
                        }
                        break;
                }
                Error = error;
                return error;
            }
        }

        public Depart()
        {
        }

        public Depart(string name, string description, string product, Project project)
        {
            Name = name;
            Description = description;
            Product = product;
            Project = project;
        }
    }
}
