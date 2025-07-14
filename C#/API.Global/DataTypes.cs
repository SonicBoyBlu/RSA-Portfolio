/// <summary>
/// Common DataTypes throughout the internal API
/// </summary>

namespace Global
{
    public class DataTypes
    {
        public enum Markets
        {
            All = 0,
            SEA = 1,
            PDX = 2,
            PHX = 3,
            AUS = 4,
            SAT = 5,
            DWF = 6,
            HOU = 7,
        }
        public enum Gender { Unspecified, Male, Female }

        public enum SaveResultStatus { Success, Fail, Error }

        public enum TestimonialType { New = -1, All = 0, Candidate = 1, Employer = 2, Schools = 3, Staff = 4 }

        public enum UserType
        {
            // 11-02-2022
            //*
            Guest = 0,
            Candidate = 1,
            Employer = 2,
            School = 3,
            SchoolOld = 4,
            Admin = 5,
            Corporate = 6,
            //*/

            //legacy
            /*
            School = 6, 
            Admin = 5, 
            Corporate = 4, 
            Employer = 3, 
            Candidate = 2, 
            Guest = 1
            //*/
        }

        public enum AccountNotificaitonType
        {
            UpdatePassword, UpdateUserName, CancelAccount, Unsubscribe
        }

        public enum RecoveryMethod { Lookup, SetTemp, Verify, Reset, Complete }

        #region Data Sorting
        public enum SortDirection { asc, desc }
        public enum JobSortOrderBy
        {
            Default = -1,
            Description = 1,
            Title = 2,
            DateStart = 3,
            DateEnd = 4,
            DateCreated = 5,
            CategoryID = 6,
            CompanyName = 7,
            PayRange = 8,
            IsOpen = 9,
            IsActive = 10,
            IsPublic = 11,
            DatePublished = 12
        }

        public enum SortOrderBy
        {
            ID = 0,
            CompanyName = 1,
            Phone = 2,
            Websire = 3,
            MarketID = 4,
            MarketName = 5,
            MarketCode = 6,
            IsActive = 7,
            IsTest = 8,
            IsDelete = 9
        }
        #endregion

        public enum FavoriteType
        {
            None = -1,
            All = 0,
            Job = 1,
            Employer = 2
        }

        public enum MessageType
        {
            Email = 1,
            SMS = 2
        }

        public enum BulletinType
        {
            Daily = 2,
            Weekly = 1,
            Monthly = 3
        }

        public enum BulletinFeedback
        {
            Like = 1,
            Meh = 2,
            Dislike = 3
        }


        public enum JsonStatusType
        {
            error = 0,
            success = 1,
            warning = 2,
            info = 3
        }

        public enum AddressBookType
        {
            Candidates = 1,
            Employers = 2
        }

        public enum AppStatusType
        {
            Denied = 0,
            Submitted = 1,
            UnderReview = 2,
            InterviewScheduled = 3,
            NotApplied = 4,
            Incomplete = 5,
            ResumeSent = 6,
            Hidden = 7
        }

        public enum CandidateStatus
        {
            Black = 5,
            Blue = 4,
            Green = 3,
            Yellow = 2,
            Red = 1,
            Unknown = 0
        }

        public enum ConversionTypes
        {
            Unknown = 0,
            Direct = 8,
            Convert = 4
        }

        public enum LocationTypes
        {
            OnPrem = 1,
            Hybrid = 2,
            Remote = 3
        }

        public enum ResumeScore
        {
            Contact = 35,
            Education = 30,
            Employment = 12,
            Skill = 5,
            Language = 2,
            Certificate = 3,
            Reference = 10,
            CourseWork = 3,
            Activity = 2,
            Award = 2
        }

        public enum PortalTypes
        {
            CandidatesV2_WS = 1,
            CandidatesV3_Web = 2,
            CandidatesV3_Mobile = 3
        }

        public enum TicketStatus
        {
            Any = 0,
            Open = 1,
            Pending_Review = 2,
            Assigned = 3,
            Pending_Response = 4,
            Shelved = 5,
            Closed = 6
        }

        public enum AppTrackerViews
        {
            All = 1,
            Dashboard = 2,
            Incomplete = 3,
            AdminByJob = 4
        }

        public class Consoles
        {
            public enum User
            {
                CON,
                SYS,
                QRY,
                BUL,
                FSI,
                TMC,
                JOB

            }
            public enum ConsoleTypes
            {
                All = 1,
                Console = 2,
                Bulletins = 3,
                FSI = 4
            }
            public enum MessageType
            {
                Idle = 1,
                Error = 2,
                Warn = 3,
                Info = 4,
                Query = 5,
                Default = 6
            }
        }

        public enum EnvironmentMode
        {
            Unknown,
            Dev,
            Beta,
            Production
        }
    }
}