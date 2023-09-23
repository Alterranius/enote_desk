using eNote_desk.Models;
using eNote_desk.Utils;
using eNote_desk.ViewModels.Analytics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eNote_desk.Pages.Shared
{
    /// <summary>
    /// Логика взаимодействия для UnitStatistics.xaml
    /// </summary>
    public partial class UnitStatistics : Page
    {
        public UnitStatistics(string token, Project project)
        {
            InitializeComponent();
            AnalyticsVM vm = new AnalyticsVM(token, project);
            DataContext = vm;
            LoadGraphs(vm.UnitStatsDTO);
        }
        public UnitStatistics(string token, Depart depart)
        {
            InitializeComponent();
            AnalyticsVM vm = new AnalyticsVM(token, depart);
            DataContext = vm;
            LoadGraphs(vm.UnitStatsDTO);
        }
        public UnitStatistics(string token, Team team)
        {
            InitializeComponent();
            AnalyticsVM vm = new AnalyticsVM(token, team);
            DataContext = vm;
            LoadGraphs(vm.UnitStatsDTO);
        }
        private void LoadGraphs(UnitStatsDTO stats)
        {
            try
            {
                plotLinearCompleteTasks.Plot.AddScatter(
            GraphUtil.GetDates(stats.CompletedTasks),
            GraphUtil.GetValues(stats.CompletedTasks));
                var pie = plotRoundTasks.Plot.AddPie(
                    GraphUtil.GetPies(stats.InWork, stats.Completed, stats.Failed));
                GraphUtil.PieConfigure(pie,
                    ColorTranslator.FromHtml("#45455D"),
                    ColorTranslator.FromHtml("#6CA103"),
                    ColorTranslator.FromHtml("#903131"));
                var bar = plotLinearTaskAccounts.Plot;
                plotLinearTaskAccounts.Plot.AddBarSeries(GraphUtil.GetBarSeries(stats.TaskAccounts));
                plotRoundTasks.Refresh();
                plotLinearTaskAccounts.Refresh();
                plotLinearCompleteTasks.Refresh();
            }
            catch (Exception e)
            {
            }
        }
    }
}
