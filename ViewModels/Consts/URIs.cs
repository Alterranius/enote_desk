using System;
using System.Collections.Generic;
using System.Text;

namespace eNote_desk.ViewModels.Consts
{
    public static class URIs
    {
        public const string AUTH = "/auth/login";
        public const string SIGN = "/auth/registration";
        public const string APPEAL = "/appeal";
        public const string APPEALS = "/appeal/getAll";
        public const string DELEGATION = "/delegation";
        public const string DELEGATIONS = "/delegation/getAll";
        public const string DELEGATION_COMPLETE = "/delegation/complete";
        public const string PROJECT = "/project";
        public const string TEAMS_PROJECT = "/team/project";
        public const string PROJECT_LEAVE = "/project/leave";
        public const string DEPART = "/depart";
        public const string DEPART_STATS = "/depart/stats";
        public const string TEAM = "/team"; 
        public const string TEAM_STATS = "/team/stats";
        public const string TASK = "/task";
        public const string TASK_SET = "/task/set";
        public const string TASK_RESET = "/task/reset";
        public const string TASK_INWORK = "/task/inwork";
        public const string TASK_UNSIGNED = "/task/unsigned";
        public const string TASK_HISTORY = "/task/history";
        public const string TASK_COMPLETE = "/task/complete";
        public const string TASK_FAIL = "/task/fail";
        public const string TASK_MY = "/task/my";
        public const string ROLE_SET = "/role/set";
        public const string ROLE = "/role";
        public const string ROLE_RESET = "/role/reset";
        public const string ROLE_PROJECT = "/role/project";
        public const string ROLE_TEAM = "/role/team";
        public const string ROLE_DEPART = "/role/depart";
        public const string COMMENT = "/comment";
        public const string PLAN = "/plan";
        public const string PLAN_CONTENT = "/plan/content";
        public const string ACCOUNT = "/account";
        public const string ACCOUNT_SECURITY = "/account/security";
        public const string INVITE = "/invite";
        public const string INVITE_ACCEPT = "/invite/accept";
        public const string INVITE_DECLINE = "/invite/decline";
        public const string STATISTICS = "/statistics";
        public const string STATISTICS_TEAM = "/statistics/team";
        public const string STATISTICS_PROJECT = "/statistics/project";
        public const string STATISTICS_DEPART = "/statistics/depart";
    }
}
