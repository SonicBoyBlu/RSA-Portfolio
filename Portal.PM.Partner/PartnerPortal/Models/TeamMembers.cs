namespace Acme.Models
{
    public class TeamMember
    {
        public TeamMember()
        {
            ProfilePic = "http://www.sbcs.edu.tt/wp-content/uploads/2016/04/profile-default.png";
        }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string ProfilePic { get; set; }
    }
}