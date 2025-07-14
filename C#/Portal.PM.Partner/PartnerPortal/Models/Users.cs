using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Acme.Models.ExternalApplication;

namespace Acme.Models
{
    public class User
    {
        public int UserID { get; set; }
        public DataTypes.UserType UserType { get; set; }
        public Guid MembershipID { get; set; }
        // public EscapiaDetails EscapiaDetails { get; set; }
        public int EscapiaUserID { get; set; }
        public int BambooHrUserID { get; set; }
        public int SmarterTrackUserID { get; set; }
        public int JitBitUserID { get; set; }
        public int QuickBooksCustomerID { get; set; }

        public string ZohoCrmUserID { get; set; }
        public string ZohoCreatorUserID { get; set; }
        public string ZohoDeskUserID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        // [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }

        public string JobTitle { get; set; }
        public string AssignedTo { get; set; }
        public string Company { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public string SecurityLevel { get; set; }
        public DateTime HireDate { get; set; }

        public int VendorID { get; set; }

        public bool IsActive { get; set; }
        public bool IsOnboarded { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDepartmentAdmin { get; set; }
        public bool IsDisabled { get; set; }

        public DateTime DateLastLogin { get; set; }

        public string Notes { get; set; }

        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        public Guid Guid { get; internal set; }
        public int UserTypeID { get; internal set; }
        public string UserTypeName { get; internal set; }
        public Models.ExternalApplication.ExternalApplications ExternalApplications { get; internal set; }
    }
    public class UserUpdate
    {
        public int userid { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public int usertypeid { get; set; }
        public string password { get; set; }
        public string ipaddress { get; set; }
        public int bambooid { get; set; }
    }

    [Table("User_PasswordToken")]
    public class User_PasswordToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public bool HasClicked { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
    }

    [Table("User")]
    public class UserDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int UserTypeID { get; set; }
        public string IPAddress { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsActive { get; set; }

    }
}
