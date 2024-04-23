using Acme.Services;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Acme;

public class Identity
{
    public static UserIdentity Current
    {
        get
        {
            try
            {
                var identity = HttpContext.Current.User.Identity as UserIdentity;
                //if (identity == null) HttpContext.Current.Response.Redirect("~/logout");
                return identity;
            }
            catch
            {
                HttpContext.Current.Response.Redirect("~/logout");
                return null;
            }
        }
    }
}

public class UserIdentity : IIdentity
{
    public UserIdentity(FormsAuthenticationTicket ticket)
    {
        Name = ticket.Name;
        Expires = ticket.Expiration;

        // Populate this object with the properties
        DeserializeProperties(ticket.UserData);
    }
    public UserIdentity()
    {

    }

    /// <summary>
    /// These are the properties that will be transferred from the FormsAuthenticationTicket to the Identity. 
    /// If the property is not listed here, it will not be accounted for or auto-populated from the ticket when DeserializeProperties() is invoked.
    /// </summary>
    private List<string> SerializableFields = new List<string>()
    {
        "UserID",
        "UserTypeID",
        "JitBitUserID",
        "SmarterTrackUserID",
        "EscapiaUserID",
        "BambooHrUserID",
        "QuickBooksUserID",

        //"ZohoUserID",
        //"CreatorUserID",
        //"AuthToken",

        "FirstName",
        "LastName",
        "Username",
        "AvatarUrl",

        "Phone",
        "Email",
        "JobTitle",
        "Department",
        "DateLastLogin",

        "IsActive",
        "IsAdmin",
        "IsDepartmentAdmin"
};

    #region Properties
    public int UserID { get; set; }
    public int UserTypeID { get; set; }
    public DataTypes.UserType UserType { get { return (DataTypes.UserType)UserTypeID; } }

    public int EscapiaUserID { get; set; }
    public int BambooHrUserID { get; set; }
    public int SmarterTrackUserID { get; set; }
    public int JitBitUserID { get; set; }
    public int QuickBooksUserID { get; set; }

    //public string ZohoUserID { get; set; }
    //public string CreatorUserID { get; set; }
    //public string AuthToken { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; }

    public string Phone { get; set; }
    public string Email { get; set; }
    public string JobTitle { get; set; }
    public string Department { get; set; }

    public DateTime DateLastLogin { get; set; }


    public bool IsActive { get; set; }
    public bool IsOnboarded { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsDepartmentAdmin { get; set; }

    // Easy-access Properties
    public string FullName
    {
        get { return this.FirstName + " " + this.LastName; }
    }
    #endregion

    #region Serialization
    public string SerializeProperties()
    {
        // Get the string format
        var formatter = string.Empty;
        for (var i = 0; i < SerializableFields.Count; i++)
        {
            if (!string.IsNullOrEmpty(formatter)) formatter += "|";
            formatter += "{" + i + "}";
        }

        // Get the field data using reflection
        var fieldData = new List<object>();
        var type = typeof(UserIdentity);

        foreach (var field in SerializableFields)
        {
            foreach (var property in type.GetProperties())
            {
                if (property.Name.Equals(field, StringComparison.InvariantCultureIgnoreCase))
                {
                    fieldData.Add(property.GetValue(this));
                    break;
                }
            }
        }

        // Return the formatted data
        return string.Format(formatter, fieldData.ToArray());
    }
    public void DeserializeProperties(string data)
    {
        var counter = 0;
        var dataArray = data.Split('|');


        // Re-populate this object using reflection
        var type = typeof(UserIdentity);
        foreach (var field in SerializableFields)
        {
            foreach (var property in type.GetProperties())
            {
                if (property.Name.Equals(field, StringComparison.InvariantCultureIgnoreCase))
                {
                    property.SetValue(this, Convert.ChangeType(dataArray[counter], property.PropertyType));
                    counter++;
                    break;
                }
            }
        }
    }

    public static UserIdentity Deserialize(string data)
    {
        try
        {
            var ticket = FormsAuthentication.Decrypt(data);
            return new UserIdentity(ticket);
        }
        catch
        {
            var service = new IdentityService();
            service.SignOut();
            return null;
        }
    }
    #endregion

    #region IIdentity Settings
    string IIdentity.AuthenticationType
    {
        get { return "Custom"; }
    }
    bool IIdentity.IsAuthenticated
    {
        get { return true; }
    }
    public string Name { get; set; }
    public DateTime Expires { get; set; }
    #endregion
}