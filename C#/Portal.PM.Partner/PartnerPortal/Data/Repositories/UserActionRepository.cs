using System;
using System.Collections.Generic;
using System.Linq;
using Acme.Models;
using Dapper.FastCrud;
using Acme.Extensions;
using Dapper;

namespace Acme.Data.Repositories
{
    public class UserActionRepository : BaseRepository, IUserActionRepository
    {
        override protected string ConnectionString
        {
            get
            {
                return GlobalSettings.DateStores.AcmePortal;
            }
        }

        public UserAction Save(UserAction action)
        {
            return WithConnection(connection =>
            {
                action.ActionDateTime = DateTime.Now;
                connection.Insert(action);
                return action;
            });
        }

        public List<UserActionDetail> GetUserActionsPage(UserActionTargetType targetType, PageSortFilter filter)
        {
            return WithConnection(connection =>
            {
                string query = SelectFromClause + " WHERE TargetType=@targetType";
                AddPageSortSQL(query, filter);
                return (connection.Query<UserActionDetail>(query, new { targetType = targetType })).ToList();
            });
        }

        public List<UserActionDetail> GetUserActions(UserActionTargetType targetType, DateTime startDate, DateTime endDate)
        {
            return WithConnection(connection =>
            {
                string query = SelectFromClause + " WHERE ActionDateTime BETWEEN @startDate AND @endDate AND TargetType=@targetType";
                return (connection.Query<UserActionDetail>(query, new { targetType = targetType, startDate = startDate, endDate = endDate })).ToList();
            });
        }

        public List<UserActionDetail> GetUserActions(UserActionTargetType targetType, UserActionVerb action, DateTime startDate, DateTime endDate)
        {
            return WithConnection(connection =>
            {
                string query = SelectFromClause + " WHERE Action = @userAction AND ActionDateTime BETWEEN @startDate AND @endDate AND TargetType=@targetType";
                return (connection.Query<UserActionDetail>(query, new { userAction = action, targetType = targetType, startDate = startDate, endDate = endDate })).ToList();
            });
        }

        public List<KeyValuePair<string, int>> GetActionVerbs()
        {
            return Enums.ToList<UserActionVerb>();
        }

        public List<KeyValuePair<string, int>> GetTargetTypes()
        {
            return Enums.ToList<UserActionTargetType>();
        }

        protected string SelectFromClause => "SELECT ua.ID, ua.UserID, ua.Action, ua.ActionDateTime, ua.TargetID, ua.TargetType, ua.Details, u.LastName, u.FirstName, u.Email FROM UserAction ua INNER JOIN [User] u ON u.ID = ua.UserID";

    }
}