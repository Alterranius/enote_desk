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

namespace eNote_desk.Pages.Accounts
{
    /// <summary>
    /// Логика взаимодействия для AccountStatistics.xaml
    /// </summary>
    public partial class AccountStatistics : Page
    {
        public AccountStatistics(string token)
        {
            InitializeComponent();
            AnalyticsVM vm = new AnalyticsVM(token);
            DataContext = vm;
            LoadGraphs(vm.AccountStatsDTO);
        }
        private void LoadGraphs(AccountStatsDTO stats)
        {
            var pie = plotRoundTasks.Plot.AddPie(
                GraphUtil.GetPies(stats.InWork, stats.Completed, stats.Failed));
            GraphUtil.PieConfigure(pie,
                ColorTranslator.FromHtml("#45455D"),
                ColorTranslator.FromHtml("#6CA103"),
                ColorTranslator.FromHtml("#903131"));
            var bar = plotLinearTasks.Plot;
            plotLinearTasks.Plot.AddBarSeries(GraphUtil.GetBarSeries(stats.TaskAccounts));
            plotRoundTasks.Refresh();
            plotLinearTasks.Refresh();

        }
    }
}
