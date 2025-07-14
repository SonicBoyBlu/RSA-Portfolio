using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Resumes.Models
{
    public class Major
    {
        /*
        Major()
        {
            MajorID = 0;
            MajorName = string.Empty;
        }
        */
        public int MajorID { get; set; }    
        public string? MajorName { get; set;}
    }
    public class Majors : List<Major> { }
}
