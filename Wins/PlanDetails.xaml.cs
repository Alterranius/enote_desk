using eNote_desk.Models;
using eNote_desk.ViewModels.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace eNote_desk.Wins
{
    /// <summary>
    /// Логика взаимодействия для PlanDetails.xaml
    /// </summary>
    public partial class PlanDetails : Window
    {
        private static PlanDetails win;
        public PlanDetails(string token, Project project)
        {
            InitializeComponent();
            PlanVM vm = new PlanVM(token, project);
            DataContext = vm;
            btnDelete.Visibility = Visibility.Hidden;
            btnSave.Command = vm.PostPlanAddClick;
            win = this;
            stkDepart.Visibility = Visibility.Hidden;
        }
        public PlanDetails(string token, Depart depart)
        {
            InitializeComponent();
            PlanVM vm = new PlanVM(token, depart);
            DataContext = vm;
            btnDelete.Visibility = Visibility.Hidden;
            btnSave.Command = vm.PostPlanAddClick;
            win = this;
        }
        public PlanDetails(string token, Plan plan)
        {
            InitializeComponent();
            PlanVM vm = new PlanVM(token, plan);
            DataContext = vm;
            btnDelete.Command = vm.DeletePlanRemoveClick;
            btnDelete.CommandParameter = vm.SelectedPlan;
            btnSave.Command = vm.PutPlanUpdateClick;
            btnSave.CommandParameter = vm.SelectedPlan;
            win = this;
            if (plan.PlanType != null)
            {
                cbType.SelectedItem = vm.TypesCmb.FirstOrDefault(x => x.Id == plan.PlanType.Id);
            }
            if (plan.Depart != null)
            {
                cbDepart.SelectedItem = vm.DepartsCmb.FirstOrDefault(x => x.Id == plan.Depart.Id);
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public static void Performed()
        {
            if (win != null)
            {
                win.Close();
            }
        }
    }
}
