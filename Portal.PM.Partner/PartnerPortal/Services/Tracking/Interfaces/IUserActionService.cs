using System;
using System.Collections.Generic;
using Acme.Models;

namespace Acme.Services.Tracking
{
    public interface IUserActionService
    {
        void SaveUserAction(int userId, UserActionVerb action, UserActionTargetType targetType, int targetId, string details);
        List<UserActionDetail> GetUserActions(UserActionTargetType targetType, DateTime startDate, DateTime endDate);
        List<UserActionDetail> GetUserActions(UserActionTargetType targetType, UserActionVerb action, DateTime startDate, DateTime endDate);
    }
}