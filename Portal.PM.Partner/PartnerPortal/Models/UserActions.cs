using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models
{
 
    public enum UserActionVerb 
    {
        [Description("Create")]
        Create =1,
        [Description("Update")]
        Update = 2,
        [Description("Delete")]
        Delete = 3,
        [Description("Upload")]
        Upload = 4
    }
    public enum UserActionTargetType 
    {
        [Description("User")]
        User = 1,
        [Description("Invoice")]
        Invoice = 2
    }

    [Table("UserAction")]
    public class UserAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long ID { get; set; }
        
        public int UserID { get; set; }

        public int Action { get; set; }

        public DateTime ActionDateTime { get; set; }

        public int TargetID { get; set; }
        
        public int TargetType { get; set; }

        public string Details { get; set; }

    }

    public class UserActionDetail : UserAction
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public string Email { get; set; }

    }
}