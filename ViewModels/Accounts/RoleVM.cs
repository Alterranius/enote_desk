using eNote_desk.APIService;
using eNote_desk.Models;
using eNote_desk.ViewModels.Consts;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace eNote_desk.ViewModels.Accounts
{
    public class RoleVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private Account _account;
        public Account Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }

        private Role _role;
        public Role Role
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }

        private List<Role> _teamRoles;
        public List<Role> TeamRoles
        {
            get { return _teamRoles; }
            set { SetProperty(ref _teamRoles, value); }
        }

        private List<Role> _departRoles;
        public List<Role> DepartRoles
        {
            get { return _departRoles; }
            set { SetProperty(ref _departRoles, value); }
        }

        private List<Role> _projectRoles;
        public List<Role> ProjectRoles
        {
            get { return _projectRoles; }
            set { SetProperty(ref _projectRoles, value); }
        }

        private List<Role> _rolesCmb;
        public List<Role> RolesCmb
        {
            get { return _rolesCmb; }
            set { SetProperty(ref _rolesCmb, value); }
        }

        private List<AccountRoleDTO> _unitAccounts;
        public List<AccountRoleDTO> UnitAccounts
        {
            get { return _unitAccounts; }
            set { SetProperty(ref _unitAccounts, value); }
        }

        private AccountRoleDTO _selectedUnitAccounts;
        public AccountRoleDTO SelectedUnitAccounts
        {
            get { return _selectedUnitAccounts; }
            set { SetProperty(ref _selectedUnitAccounts, value); }
        }

        private List<Account> _accountsCmb;
        public List<Account> AccountsCmb
        {
            get { return _accountsCmb; }
            set { SetProperty(ref _accountsCmb, value); }
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

        private Team _team;
        public Team Team
        {
            get { return _team; }
            set { SetProperty(ref _team, value); }
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
        #endregion
        #region ICommands
        public DelegateCommand PostRoleAccountClick { get; set; }
        public DelegateCommand DeleteRoleAccountClick { get; set; }
        #endregion
        #region Constructor
        public RoleVM(string token)
        {
            PostRoleAccountClick = new DelegateCommand(Set);
            DeleteRoleAccountClick = new DelegateCommand(Reset);
            Token = token;
        }
        public RoleVM(string token, Project project)
        {
            PostRoleAccountClick = new DelegateCommand(Set);
            DeleteRoleAccountClick = new DelegateCommand(Reset);
            Token = token;
            Project = project;
            GetProjectAccounts();
            GetAccountsCmb();
            GetProjectRolesCmb();
        }
        public RoleVM(string token, Depart depart)
        {
            PostRoleAccountClick = new DelegateCommand(Set);
            DeleteRoleAccountClick = new DelegateCommand(Reset);
            Token = token;
            Depart = depart;
            Project = depart.Project;
            GetDepartAccounts();
            GetAccountsCmb();
            GetDepartRolesCmb();
        }
        public RoleVM(string token, Team team)
        {
            PostRoleAccountClick = new DelegateCommand(Set);
            DeleteRoleAccountClick = new DelegateCommand(Reset);
            Token = token;
            Team = team;
            Project = team.Depart.Project;
            GetTeamAccounts();
            GetAccountsCmb();
            GetTeamRolesCmb();
        }
        #endregion
        #region Service
        private void Set()
        {
            try
            {
                var response = WebAPI.PostCall(URIs.ROLE_SET + "/" + Account.Id, Role, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно назначено";
                    eNote_desk.Wins.RoleSetter.Performed();
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
            MessageBox.Show(Message);
        }
        private void Reset()
        {
            try
            {
                var response = WebAPI.PostCall(URIs.ROLE_RESET + "/" + SelectedUnitAccounts.Account.Id, SelectedUnitAccounts.Role, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно снято";
                    UnitAccounts.Remove(UnitAccounts.FirstOrDefault(x => x.Account.Id == SelectedUnitAccounts.Account.Id && x.Role.Id == SelectedUnitAccounts.Role.Id));
                    eNote_desk.Wins.RoleSetter.Performed();
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
            MessageBox.Show(Message);
        }

        public void GetProjectAccounts()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ROLE_PROJECT + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnitAccounts = response.Result.Content.ReadAsAsync<List<AccountRoleDTO>>().Result;
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

        public void GetTeamAccounts()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ROLE_TEAM + "/" + Team.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnitAccounts = response.Result.Content.ReadAsAsync<List<AccountRoleDTO>>().Result;
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

        public void GetDepartAccounts()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ROLE_DEPART + "/" + Depart.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UnitAccounts = response.Result.Content.ReadAsAsync<List<AccountRoleDTO>>().Result;
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
        public void GetAccountsCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ACCOUNT + "/project/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AccountsCmb = response.Result.Content.ReadAsAsync<List<Account>>().Result;
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
        public void GetProjectRolesCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ROLE + "/projectRoles/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    RolesCmb = response.Result.Content.ReadAsAsync<List<Role>>().Result;
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

        public void GetDepartRolesCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ROLE + "/departRoles/" + Depart.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    RolesCmb = response.Result.Content.ReadAsAsync<List<Role>>().Result;
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

        public void GetTeamRolesCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.ROLE + "/teamRoles/" + Team.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    RolesCmb = response.Result.Content.ReadAsAsync<List<Role>>().Result;
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
