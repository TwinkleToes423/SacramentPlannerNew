using System;
using System.Collections.Generic;

namespace SacramentPlannerNew.Models
{
    public partial class Speakers
    {
        public int SpeakerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public int MeetingId { get; set; }

        public Meeting Meeting { get; set; }
    }
}
