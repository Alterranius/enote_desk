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

namespace eNote_desk.ViewModels.Tasks
{
    public class TaskVM : BindableBase, INotifyPropertyChanged
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _code;
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private int _priority;
        public int Priority
        {
            get { return _priority; }
            set { SetProperty(ref _priority, value); }
        }
        private List<Task> _tasks;
        public List<Task> Tasks
        {
            get { return _tasks; }
            set { SetProperty(ref _tasks, value); }
        }
        private List<Comment> _comments;
        public List<Comment> Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }
        private List<Team> _teamsCmb;
        public List<Team> TeamsCmb
        {
            get { return _teamsCmb; }
            set { SetProperty(ref _teamsCmb, value); }
        }
        private List<TaskType> _typesCmb;
        public List<TaskType> TypesCmb
        {
            get { return _typesCmb; }
            set { SetProperty(ref _typesCmb, value); }
        }
        private List<TaskStatus> _statusCmb;
        public List<TaskStatus> StatusCmb
        {
            get { return _statusCmb; }
            set { SetProperty(ref _statusCmb, value); }
        }
        private List<Account> _accountsCmb;
        public List<Account> AccountsCmb
        {
            get { return _accountsCmb; }
            set { SetProperty(ref _accountsCmb, value); }
        }
        private List<TaskCategory> _categoriesCmb;
        public List<TaskCategory> CategoriesCmb
        {
            get { return _categoriesCmb; }
            set { SetProperty(ref _categoriesCmb, value); }
        }
        private DateTime? _delegatedAt;
        public DateTime? DelegatedAt
        {
            get { return _delegatedAt; }
            set { SetProperty(ref _delegatedAt, value); }
        }
        private DateTime? _completedAt;
        public DateTime? CompletedAt
        {
            get { return _completedAt; }
            set { SetProperty(ref _completedAt, value); }
        }
        private TaskCategory _category;
        public TaskCategory Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }
        private TaskStatus _status;
        public TaskStatus Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
        private TaskType _type;
        public TaskType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        private Account _account;
        public Account Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }
        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }
        private Team _team;
        public Team Team
        {
            get { return _team; }
            set { SetProperty(ref _team, value); }
        }

        private Task _task;
        public Task Task
        {
            get { return _task; }
            set { SetProperty(ref _task, value); }
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set { SetProperty(ref _selectedTask, value); }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
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
        public DelegateCommand PostTaskAddClick { get; set; }
        public DelegateCommand<Task> DeleteTaskRemoveClick { get; set; }
        public DelegateCommand<Task> PutTaskUpdateClick { get; set; }
        public DelegateCommand GetTasksLoad { get; set; }
        public DelegateCommand PostCommentAddClick { get; set; }
        public DelegateCommand GetMyTasksClick { get; set; }
        public DelegateCommand GetTaskHistoryClick { get; set; }
        public DelegateCommand GetTasksInWorkLoad { get; set; }
        public DelegateCommand GetTasksUnsignedLoad { get; set; }
        public DelegateCommand<Task> PutTaskSetClick { get; set; }
        public DelegateCommand<Task> PutTaskResetClick { get; set; }
        public DelegateCommand<Task> PutTaskCompleteClick { get; set; }
        public DelegateCommand<Task> PutTaskFailClick { get; set; }
        #endregion
        #region Constructor
        public TaskVM(string token, Task task)
        {
            DeleteTaskRemoveClick = new DelegateCommand<Task>(RemoveTask);
            PutTaskUpdateClick = new DelegateCommand<Task>(UpdateTask);
            PostCommentAddClick = new DelegateCommand(AddComment);
            Token = token;
            SelectedTask = task;
            Project = task.Team.Depart.Project;
            GetTeamsCmb();
            GetTypesCmb();
            GetStatusCmb();
            GetCategoriesCmb();
            GetAccountsCmb();
            GetComments();
        }
        public TaskVM(string token, Project project)
        {
            PutTaskCompleteClick = new DelegateCommand<Task>(CompleteTask);
            PutTaskFailClick = new DelegateCommand<Task>(FailTask);
            GetMyTasksClick = new DelegateCommand(GetMyTasks);
            Token = token;
            Project = project;
            GetMyTasks();
            GetTeamsCmb();
            GetTypesCmb();
            GetStatusCmb();
            GetCategoriesCmb();
            GetAccountsCmb();
        }
        public TaskVM(string token, Project project, DateTime date)
        {
            GetTaskHistoryClick = new DelegateCommand(GetHistoryTask);
            Token = token;
            Project = project;
            GetHistoryTask();
            GetTeamsCmb();
            GetTypesCmb();
            GetStatusCmb();
            GetCategoriesCmb();
            GetAccountsCmb();
        }
        public TaskVM(string token, Project project, int count)
        {
            GetTasksInWorkLoad = new DelegateCommand(GetTasksInWork);
            GetTasksUnsignedLoad = new DelegateCommand(GetTasksUnsigned);
            PutTaskSetClick = new DelegateCommand<Task>(SetTask);
            PutTaskResetClick = new DelegateCommand<Task>(ResetTask);
            Token = token;
            Project = project;
            GetTasksInWork();
            GetTeamsCmb();
            GetTypesCmb();
            GetStatusCmb();
            GetCategoriesCmb();
            GetAccountsCmb();
        }
        public TaskVM(string token, Project project, bool all)
        {
            SelectedTask = new Task();
            PostTaskAddClick = new DelegateCommand(AddTask);
            DeleteTaskRemoveClick = new DelegateCommand<Task>(RemoveTask);
            GetTasksLoad = new DelegateCommand(GetTasks);
            PostCommentAddClick = new DelegateCommand(AddComment);
            Token = token;
            Project = project;
            GetTasks();
            GetTeamsCmb();
            GetTypesCmb();
            GetStatusCmb();
            GetCategoriesCmb();
            GetAccountsCmb();
        }
        #endregion
        #region Service
        public TaskVM InWork()
        {
            GetTasksInWork();
            return this;
        }
        public TaskVM Unsigned()
        {
            GetTasksUnsigned();
            return this;
        }
        private void AddTask()
        {
            if (!string.IsNullOrEmpty(SelectedTask.Error))
            {
                return;
            }
            SelectedTask.CreatedAt = DateTime.Now;
            try
            {
                var response = WebAPI.PostCall(URIs.TASK, SelectedTask, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно добавлено";
                    eNote_desk.Wins.TaskDetails.Performed();
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
            MessageBox.Show(Message);
        }
        private void RemoveTask(Task task)
        {
            try
            {
                var response = WebAPI.DeleteCall(URIs.TASK + "/" + task.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно удалено";
                    eNote_desk.Wins.TaskDetails.Performed();
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
            MessageBox.Show(Message);
        }
        private void UpdateTask(Task task)
        {
            if (!string.IsNullOrEmpty(task.Error))
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.TASK + "/" + task.Id, task, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно обновлено";
                    eNote_desk.Wins.TaskDetails.Performed();
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
        private void AddComment()
        {
            Comment comment = new Comment(
                "Head",
                Content,
                DateTime.Now,
                null,
                SelectedTask);
            try
            {
                var response = WebAPI.PostCall(URIs.COMMENT, comment, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Message = "Успешно отправлено";
                    GetComments();
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
            MessageBox.Show(Message);
        }
        private void CompleteTask(Task task)
        {
            try
            {
                var response = WebAPI.PutCall(URIs.TASK_COMPLETE + "/" + task.Id, task, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно выполнено";
                    GetTasks();
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
        private void FailTask(Task task)
        {
            try
            {
                var response = WebAPI.PutCall(URIs.TASK_FAIL + "/" + task.Id, task, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Задача провалена";
                    GetTasks();
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
        private void SetTask(Task task)
        {
            if (SelectedTask == null || SelectedTask.Account == null)
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.TASK_SET + "/" + task.Id, SelectedTask.Account, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно назначено";
                    GetTasks();
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
        private void ResetTask(Task task)
        {
            if (SelectedTask == null || SelectedTask.Account == null)
            {
                return;
            }
            try
            {
                var response = WebAPI.PutCall(URIs.TASK_RESET + "/" + task.Id, SelectedTask.Account, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Успешно снято";
                    GetTasks();
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
        public void GetTasksUnsigned()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK_UNSIGNED + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Tasks = response.Result.Content.ReadAsAsync<List<Task>>().Result;
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
        public void GetTasks()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK + "/project/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Tasks = response.Result.Content.ReadAsAsync<List<Task>>().Result;
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
        public void GetTasksInWork()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK_INWORK + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Tasks = response.Result.Content.ReadAsAsync<List<Task>>().Result;
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
        private void GetHistoryTask()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK_HISTORY + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Tasks = response.Result.Content.ReadAsAsync<List<Task>>().Result;
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
        public void GetMyTasks()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK_MY + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Tasks = response.Result.Content.ReadAsAsync<List<Task>>().Result;
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
        private void GetComments()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.COMMENT + "/" + SelectedTask.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Comments = response.Result.Content.ReadAsAsync<List<Comment>>().Result;
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
        public void GetTeamsCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TEAMS_PROJECT + "/" + Project.Id, Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TeamsCmb = response.Result.Content.ReadAsAsync<List<Team>>().Result;
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
        public void GetCategoriesCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK + "/category", Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    CategoriesCmb = response.Result.Content.ReadAsAsync<List<TaskCategory>>().Result;
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
        public void GetStatusCmb()
        {
            try
            {
                var response = WebAPI.GetCall(URIs.TASK + "/status", Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    StatusCmb = response.Result.Content.ReadAsAsync<List<TaskStatus>>().Result;
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
                var response = WebAPI.GetCall(URIs.TASK + "/type", Token);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Неустойчивое соединение";
                    return;
                }
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TypesCmb = response.Result.Content.ReadAsAsync<List<TaskType>>().Result;
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