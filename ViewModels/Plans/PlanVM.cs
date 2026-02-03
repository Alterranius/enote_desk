using eNote_desk.APIService;
using eNote_desk.Models;
using eNote_desk.ViewModels.Consts;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace eNote_desk.ViewModels.Plans
{
    public class PlanVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _aim;
        public string Aim
        {
            get { return _aim; }
            set { SetProperty(ref _aim, value); }
        }
        private PlanType _type;
        public PlanType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        private List<PlanType> _typesCmb;
        public List<PlanType> TypesCmb
        {
            get { return _typesCmb; }
            set { SetProperty(ref _typesCmb, value); }
        }
        private List<Depart> _departsCmb;
        public List<Depart> DepartsCmb
        {
            get { return _departsCmb; }
            set { SetProperty(ref _departsCmb, value); }
        }
        private List<Plan> _plans;
        public List<Plan> Plans
        {
            get { return _plans; }
            set { SetProperty(ref _plans, value); }
        }
        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }
        private Depart _depart;
        public Depart Depart
        {
            get { return _depart; }
            set { SetProperty(ref _depart, value); }
        }
        private Plan _selectedPlan;
        public Plan SelectedPlan
        {
            get { return _selectedPlan; }
            set { SetProperty(ref _selectedPlan, value); }
        }
        private PlanContent _selectedPlanContent;
        public PlanContent SelectedPlanContent
        {
            get { return _selectedPlanContent; }
            set { SetProperty(ref _selectedPlanContent, value); }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        private string _token;
        public string Token
        {
            get { return _token; }
            set { SetProperty(ref _token, value); }
        }
        private bool isProject = false;
        #endregion
        #region ICommands
        public DelegateCommand PostPlanAddClick { get; set; }
        public DelegateCommand PostPlanContentClick { get; set; }
        public DelegateCommand<Plan> DeletePlanRemoveClick { get; set; }
        public DelegateCommand<Plan> PutPlanUpdateClick { get; set; }
        public DelegateCommand<PlanContent> PutPlanContentUpdateClick { get; set; }
        public DelegateCommand<PlanContent> DeletePlanContentRemoveClick { get; set; }
        public DelegateCommand GetPlansLoad { get; set; }
        #endregion
        #region Constructor
        public PlanVM(string token, Plan plan)
        {
            DeletePlanRemoveClick = new DelegateCommand<Plan>(RemovePlan);
            PutPlanUpdateClick = new DelegateCommand<Plan>(UpdatePlan);
            Token = token;
            SelectedPlan = plan;
            if (SelectedPlan.Project != null)
            {
                Project = SelectedPlan.Project;
            }
            if (SelectedPlan.Depart != null)
            {
                Project = SelectedPlan.Depart.Project;
            }
            GetDepartsCmb();
            GetTypesCmb();
        }
        public PlanVM(string token, Depart depart)
        {
            isProject = false;
            SelectedPlan = new Plan() { Depart = depart };
            SelectedPlanContent = new PlanContent();
            PostPlanAddClick = new DelegateCommand(AddPlan);
            GetPlansLoad = new DelegateCommand(GetPlans);
            PostPlanContentClick = new DelegateCommand(AddPlanContent);
            PutPlanContentUpdateClick = new DelegateCommand<PlanContent>(UpdatePlanContent);
            DeletePlanContentRemoveClick = new DelegateCommand<PlanContent>(DeletePlanContent);
            Token = token;
            Depart = depart;
            Project = Depart.Project;
            GetPlans();
            GetDepartsCmb();
            GetTypesCmb();
            SelectedPlanContent = new PlanContent();
        }
        public PlanVM(string token, Project project)
        {
            isProject = true;
            SelectedPlan = new Plan() { Project = project };
            SelectedPlanContent = new PlanContent();
            PostPlanAddClick = new DelegateCommand(AddPlan);
            GetPlansLoad = new DelegateCommand(GetPlans);
            PostPlanContentClick = new DelegateCommand(AddPlanContent);
            PutPlanContentUpdateClick = new DelegateCommand<PlanContent>(UpdatePlanContent);
            DeletePlanContentRemoveClick = new DelegateCommand<PlanContent>(DeletePlanContent);
            Token = token;
            Project = project;
            GetPlans();
            GetDepartsCmb();
            GetTypesCmb();
            SelectedPlanContent = new PlanContent();
        }
        #endregion
        #region Service
        private void AddPlan()
        {
            if (!string.IsNullOrEmpty(SelectedPlan.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PostCall(URIs.PLAN, SelectedPlan, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    GetPlans();
                    eNote_desk.Wins.PlanDetails.Performed();
                }
                else
                {
                    Message = "Ошибка добавления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        public void RemovePlan(Plan plan)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.PLAN + "/" + plan.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                    GetPlans();
                    eNote_desk.Wins.PlanDetails.Performed();
                }
                else
                {
                    Message = "Ошибка удаления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        private void UpdatePlan(Plan plan)
        {
            if (!string.IsNullOrEmpty(plan.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.PLAN + "/" + plan.Id, plan, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    GetPlans();
                    eNote_desk.Wins.PlanDetails.Performed();
                }
                else
                {
                    Message = "Ошибка обновления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        private void AddPlanContent()
        {
            if (SelectedPlan == null)
            {
                return;
            }
            
            PlanContent planContent = new PlanContent(
                SelectedPlanContent.Name,
                SelectedPlanContent.Description,
                SelectedPlanContent.Aim,
                SelectedPlanContent.Position,
                SelectedPlan);
            if (!string.IsNullOrEmpty(planContent.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PostCall(URIs.PLAN_CONTENT + "/" + SelectedPlan.Id, planContent, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    GetPlans();
                    eNote_desk.Wins.PlanDetails.Performed();
                }
                else
                {
                    Message = "Ошибка добавления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        public void UpdatePlanContent(PlanContent planContent)
        {
            if (SelectedPlan == null)
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.PLAN_CONTENT + "/" + SelectedPlan.Id, planContent, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    GetPlans();
                    eNote_desk.Wins.PlanDetails.Performed();
                }
                else
                {
                    Message = "Ошибка обновления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        public void DeletePlanContent(PlanContent planContent)
        {
            MessageBox.Show(planContent.Id.ToString());
            try
            {
                var response = WebAPI.DeleteCall(URIs.PLAN_CONTENT + "/" + planContent.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    GetPlans();
                    eNote_desk.Wins.PlanDetails.Performed();
                }
                else
                {
                    Message = "Ошибка обновления";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        private void GetPlans()
        {
            string path;
            int id;
            if (isProject)
            {
                path = "/project";
                id = Project.Id;
            }
            else
            {
                path = "/depart";
                id = Depart.Id;
            }
            try
            {
                var response = WebAPI.GetCall(URIs.PLAN + path + "/" + id.ToString(), Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Plans = response.Result.Content.ReadAsAsync<List<Plan>>().Result;
                    Message = "Успешно загружено";
                }
                else
                {
                    Message = "Ошибка загрузки";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        public void GetTypesCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.PLAN + "/list", Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TypesCmb = response.Result.Content.ReadAsAsync<List<PlanType>>().Result;
                    Message = "Успешно загружено";
                }
                else
                {
                    Message = "Ошибка загрузки";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        public void GetDepartsCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.DEPART + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DepartsCmb = response.Result.Content.ReadAsAsync<List<Depart>>().Result;
                    Message = "Успешно загружено";
                }
                else
                {
                    Message = "Ошибка загрузки";
                }
            }
            catch (Exception e)
            {
                Message = "Нет подключения";
            }
        }
        #endregion
    }
}
