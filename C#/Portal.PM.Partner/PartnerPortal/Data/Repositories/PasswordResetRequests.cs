using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.Models;
using Dapper;
using Dapper.FastCrud;
using Intuit.Ipp.Data;
using User = Acme.Models.User;

namespace Acme.Data.Repositories
{
    public class PasswordResetRequests : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.AcmePortal; }
        }

        public User_PasswordToken RecentRequestGuid(int userId)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("userId", userId);
            string query = "SELECT TOP 1 * FROM [User_PasswordToken] WHERE userId = @userId";

            var model = WithConnection<User_PasswordToken>(connection =>
            {
                return (connection.Query<User_PasswordToken>(query, dynamicParameters)).FirstOrDefault();
            });

            if (model.IsNull()) 
                return null;

            return model;

        }
        public void DeleteOldPasswordResetRequests()
        {
            WithConnection(connection =>
            {
                var isSuccess = connection.BulkDelete<User_PasswordToken>(statement => statement
                    .Where($"{nameof(User_PasswordToken.Created):C} <@Today")
                    .WithParameters(new { Today = DateTime.Now.AddDays(-1) })
                );

                return true;
            });
        }

        public void DeleteOldPasswordResetRequests(int userId)
        {
            var model = RecentRequestGuid(userId);

            if(model.IsNull())
                return;
            
            WithConnection(connection =>
            {
                var isSuccess = connection.Delete(new User_PasswordToken { ID = model.ID});
                return true;
            });

        }

        public User_PasswordToken CreateUser_PasswordToken(User_PasswordToken model)
        {
            return WithConnection(connection =>
            {
                connection.Insert(model);
                return model;
            });

        }

        public User_PasswordToken GetResetPasswordByToken(Guid token)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("token", token);
            string query = "SELECT TOP 1 * FROM [User_PasswordToken] WHERE Token = @token";

            var model = WithConnection<User_PasswordToken>(connection =>
                (connection.Query<User_PasswordToken>(query, dynamicParameters)).FirstOrDefault()
            );

            if (model.IsNull())
                return null;
            
            return model;
        }
    }
}