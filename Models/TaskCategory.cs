using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.Models
{
    public class TaskCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
