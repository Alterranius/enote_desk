using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.ViewModels.Analytics
{
    public class UnitStatsDTO
    {
        public string Completed { get; set; }
        public string Failed { get; set; }
        public string InWork { get; set; }
        public string Speed { get; set; }
        public string Week { get; set; }
        public string Effectivency { get; set; }
        public Dictionary<string, string> TaskAccounts { get; set; }
        public Dictionary<string, string> CompletedTasks { get; set; }
    }
}
