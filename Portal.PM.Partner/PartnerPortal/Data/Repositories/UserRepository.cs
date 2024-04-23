using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.Models;
using Dapper;
using Dapper.FastCrud;

namespace Acme.Data.Repositories
{
    public class UserRepository : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.AcmePortal; }
        }

        public Models.UserDB GetUserByEmail(string email)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("email", email);

            return WithConnection<Models.UserDB>(connection =>
            {
                string query = "SELECT TOP 1 * FROM [User] WHERE Email = @email";
                return (connection.Query<Models.UserDB>(query, dynamicParameters)).FirstOrDefault();
            });
        }

        public Models.UserDB Add(Models.UserDB data)
        {
            return WithConnection<UserDB>(connection =>
            {
                connection.Insert(data);
                return data;
            });

        }

        public Models.UserDB Update(Models.UserDB data)
        {
            return WithConnection<UserDB>(connection =>
            {
                connection.Update(data);
                return data;
            });
        }

        public List<Models.UserDB> GetUsersFromEmails(List<string> emails)
        {
            return WithConnection<List<UserDB>>(connection =>
            {
                emails = emails.Select(e => e = "'" + e + "'").ToList();
                string inStatement = string.Join(",", emails);
                string query = $"SELECT  * FROM [User] WHERE Email IN ({inStatement})";
                return connection.Query<Models.UserDB>(query).ToList();
            });
        }

    }
}