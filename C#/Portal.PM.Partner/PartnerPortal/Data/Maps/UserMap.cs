using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.Models;
using Acme.Models.ExternalApplication;
using DataTypes;

namespace Acme.Data.Maps {
    public class UserMap {
        public UserMap () { }

        public Models.UserDB FromForm (Acme.Models.UserUpdate data) {
            
            Models.UserDB model = ToUserDB(data.lastname, data.firstname, data.email, data.password,true);
            return model;
        }

        public Models.UserDB FromEscapia(Escapia.Owner escapiaUser)
        {
            string emailAddress = string.Empty;

            if (escapiaUser.emails.Any())
            {
                Escapia.Email email = escapiaUser.emails.FirstOrDefault();
                emailAddress = email.address.ToStringOrDefault().Trim().ToLower();
            }

            if (emailAddress.IsNullOrEmpty())
                return null;
            
            Models.UserDB model = ToUserDB(escapiaUser.lastName, escapiaUser.firstName, emailAddress,null);
            return model;
        }

        public Models.UserDB ToUserDB(string lastname, string firstname,string email, string password, bool active = false)
        {
            Models.UserDB model = new UserDB();

            model.LastName = lastname.ToStringOrDefault().Trim();
            model.FirstName = firstname.ToStringOrDefault().Trim();
            model.Email = email.ToStringOrDefault().Trim().ToLower();
            model.IsActive = active;


            string pw = password.ToStringOrDefault();

            if (pw.IsNullOrEmpty())
                pw = model.Email;

            var securePassword = Security.Legacy.MD5Hash(pw);
            model.Password = securePassword;
            model.PasswordSalt = securePassword;

            model.UserTypeID = UserType.Owner.ToInt32OrDefault();
            model.Guid = Guid.NewGuid();
            model.Created = DateTime.Now;
            model.Modified = DateTime.Now;

            model.IPAddress = null;
            model.LastLogin = null;

            return model;
        }

        internal User FillExternalApplications(User user, ExternalApplications externalApplications)
        {
            if (user.IsNull() || externalApplications.IsNull())
                return user;

            user.BambooHrUserID = 0; //externalApplications.BambooHR;
            user.EscapiaUserID = (externalApplications.Escapia.IsNull()) ? 0 : externalApplications.Escapia.id;
            user.JitBitUserID = 0; //externalApplications.JitBit.BambooHR;

            return user;
        }
    }
}
