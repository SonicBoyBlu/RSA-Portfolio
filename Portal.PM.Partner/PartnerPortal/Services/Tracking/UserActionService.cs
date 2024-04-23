using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.Data.Repositories;
using Acme.Models;

namespace Acme.Services.Tracking
{
    public class UserActionService : IUserActionService
    {
        IUserActionRepository _userActionRepository;

        public UserActionService(IUserActionRepository userActionRepository)
        {
            _userActionRepository = userActionRepository;
        }

        public UserActionService()
        {
            _userActionRepository = new UserActionRepository();
        }

        public void SaveUserAction(int userId, UserActionVerb action, UserActionTargetType targetType, int targetId, string details)
        {
            var userAction = new UserAction()
            {
                UserID = userId,
                Action = (int)action,
                TargetType = (int)targetType,
                TargetID = (int)targetId,
                Details = details
            };
            _userActionRepository.Save(userAction);
        }

        public List<UserActionDetail> GetUserActions(UserActionTargetType targetType, DateTime startDate, DateTime endDate)
        {
            return _userActionRepository.GetUserActions(targetType, startDate, endDate);
        }

        public List<UserActionDetail> GetUserActions(UserActionTargetType targetType, UserActionVerb action,  DateTime startDate, DateTime endDate)
        {
            return _userActionRepository.GetUserActions(targetType, action, startDate, endDate);
        }

    }
}