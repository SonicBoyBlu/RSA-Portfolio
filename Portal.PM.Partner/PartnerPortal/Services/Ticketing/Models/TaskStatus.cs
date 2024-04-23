using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Ticketing
{
    public static class TaskStatus
    {
        public static int UnderConsideration = 3;
        public static int InProgress = 4;
        public static int OnHold = 5;
        public static int InReview = 6;
        public static int Completed = 7;
        public static int Rejected = 8;
        public static int Approved = 9;
    }
}