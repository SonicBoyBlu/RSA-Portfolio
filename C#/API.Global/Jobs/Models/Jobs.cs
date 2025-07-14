using Global.Location;

namespace Global.Jobs.Models
{
    public class Job
    {
        public Job()
        {
            Title = string.Empty;
            Description = string.Empty;
            CompanyName = string.Empty;
            CompanyLogo = string.Empty;
            Location = string.Empty;
            PayRange = string.Empty;
            MarketCode = string.Empty;
            MarketName = string.Empty;
            CategoryName = string.Empty;
            //Categories = new Categories.CategoryList();
            Address = new Address();
            JobType = "?";
            JobTypeDescription = string.Empty;
            JobAvailability = string.Empty;
            ExperienceMin = string.Empty;
        }
        public int JobID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public Address Address { get; set; }
        public string Location { get; set; }
        public int LocationTypeID { get; set; }
        //*
        public string LocationTypeHTML
        {
            get
            {
                string html = "Unknown";
                try
                {
                    switch (LocationTypeID)
                    {
                        case 2:
                            html = string.Format("<i class='text-info fas fa-laptop-house'></i> Hybrid / <br />{0}, {1}", Address.City, Address.State);
                            break;
                        case 3:
                            html = "<i class='text-info fa fa-laptop'></i> Remote Position";
                            break;
                        default:
                            html = string.Format("<i class='text-danger fa fa-map-marker'></i> {0}, {1}", Address.City, Address.State);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    html = "Error: " + ex.Message;
                }
                return html;
            }
        }
        public string LocationTypeIcon
        {
            get
            {
                string html;
                switch (LocationTypeID)
                {
                    case 2:
                        html = string.Format("<i class='text-success fas fa-laptop-house' title='Hybrid / {0}, {1}'></i>", Address.City, Address.State);
                        break;
                    case 3:
                        html = "<i class='text-info fa fa-laptop' title='Remote Position'></i>";
                        break;
                    default:
                        html = string.Format("<i class='text-danger fa fa-map-marker' title='{0}, {1}'></i>", Address.City, Address.State);
                        break;
                }
                return html;
            }
        }
        //*/
        public string GeneralLocation
        {
            get
            {
                return IsRemote ? "Remote" : Location;
            }
        }
        public string JobType { get; set; }
        public string JobTypeDescription { get; set; }
        public string JobAvailability { get; set; }
        public string ExperienceMin { get; set; }
        public int NumOpenings { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DatePublished { get; set; }
        public string PayRange { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public int MarketID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        //public Categories.CategoryList Categories { get; set; }
        //public Dictionary<int, string> CategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsOpen { get; set; }
        public bool IsPublic { get; set; }
        public bool IsConfidential { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsTest { get; set; }
        public bool IsNotification { get; set; }

        public bool IsRemote { get; set; }

        public bool IsApplied { get; set; }
        public int NumAppQuestions { get; set; }
        public int NumJobApps { get; set; }
        public int AppStatusTypeID { get; set; }
        //public string AppStatus { get; set; }
        public DateTime DateApplied { get; set; }
        public DateTime DateAppStatus { get; set; }

        public bool IsHidden { get; set; }
        public DateTime DateHidden { get; set; }
        /*
        public DataTypes.AppStatusType AppStatusType
        {
            get
            {
                return (DataTypes.AppStatusType)AppStatusTypeID;
            }
        }
        */
        public int SortOrder { get; set; }
    }
}