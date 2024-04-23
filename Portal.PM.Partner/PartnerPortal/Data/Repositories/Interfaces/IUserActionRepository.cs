using Acme.Models;
using System;
using System.Collections.Generic;

namespace Acme.Data.Repositories
{
    public interface IUserActionRepository
    {
        UserAction Save(UserAction action);
        List<UserActionDetail> GetUserActionsPage(UserActionTargetType targetType, PageSortFilter filter);
        List<UserActionDetail> GetUserActions(UserActionTargetType targetType, DateTime startDate, DateTime endDate);
        List<UserActionDetail> GetUserActions(UserActionTargetType targetType, UserActionVerb action, DateTime startDate, DateTime endDate);
        List<KeyValuePair<string, int>> GetActionVerbs();
        List<KeyValuePair<string, int>> GetTargetTypes();

    }
}