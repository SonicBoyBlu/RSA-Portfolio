namespace Global.Admin.Users.Models
{
    public class User
    {
        public User() {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int DepartmentID { get; set; }
    }
}
