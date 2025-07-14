using System.Collections.Generic;

namespace Acme.ViewModels
{
    public class TeamMembersViewModel
    {
        public TeamMembersViewModel()
        {
            TeamMembers = new List<Models.TeamMember>();
            Departments = new List<string>();
        }
        public List<Models.TeamMember> TeamMembers { get; set; }
        public List<string> Departments { get; set; }
    }
}