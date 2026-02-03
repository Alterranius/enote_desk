using eNote_desk.Models;
using eNote_desk.ViewModels.Plans;
using eNote_desk.Wins;
using System;
using System.Collections.Generic;
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

namespace eNote_desk.Pages.Plans
{
    /// <summary>
    /// Логика взаимодействия для Plans.xaml
    /// </summary>
    public partial class Plans : Page
    {
        private readonly string token;
        private readonly Project project;
        private readonly Depart depart;
        private readonly PlanVM vm;
        public Plans(string token, Project project)
        {
            InitializeComponent();
            PlanVM vm = new PlanVM(token, project);
            DataContext = vm;
            btnDeleteNode.Command = vm.DeletePlanRemoveClick;
            btnDeleteNode.CommandParameter = vm.SelectedPlan;
            btnAdd.Command = vm.PostPlanContentClick;
            this.token = token;
            this.project = project;
            this.vm = vm;
        }
        public Plans(string token, Depart depart)
        {
            InitializeComponent();
            PlanVM vm = new PlanVM(token, depart);
            DataContext = vm;
            btnDeleteNode.Command = vm.DeletePlanRemoveClick;
            btnDeleteNode.CommandParameter = vm.SelectedPlan;
            btnAdd.Command = vm.PostPlanContentClick;
            this.token = token;
            this.depart = depart;
            this.vm = vm;
        }

        private void btnAddNode_Click(object sender, RoutedEventArgs e)
        {
            if (project != null)
            {
                new PlanDetails(token, project).ShowDialog();
                return;
            }
            if (depart != null)
            {
                new PlanDetails(token, depart).ShowDialog();
                return;
            }
        }

        private void treePlan_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treePlan.SelectedItem != null && treePlan.SelectedItem.GetType() == typeof(PlanContent))
            {
                vm.SelectedPlanContent = treePlan.SelectedItem as PlanContent;
            }
            if (treePlan != null && treePlan.SelectedItem.GetType() == typeof(Plan))
            {
                vm.SelectedPlan = treePlan.SelectedItem as Plan;
            }
        }

        private void treePlan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (treePlan.SelectedItem != null && treePlan.SelectedItem.GetType() == typeof(Plan))
            {
                new PlanDetails(token, treePlan.SelectedItem as Plan).ShowDialog();
            }
        }

        private void btnSaveNode_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedPlan != null && vm.SelectedPlanContent != null)
            {
                vm.UpdatePlanContent(vm.SelectedPlanContent);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedPlanContent != null)
            {
                vm.DeletePlanContent(vm.SelectedPlanContent);
            }
        }

        private void btnDeleteNode_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedPlan != null)
            {
                vm.RemovePlan(vm.SelectedPlan);
            }
        }
    }
}
