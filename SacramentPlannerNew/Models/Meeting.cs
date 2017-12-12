using System;
using System.Collections.Generic;

namespace SacramentPlannerNew.Models
{
    public partial class Meeting
    {
        public Meeting()
        {
            Speakers = new HashSet<Speakers>();
        }

        public int MeetingId { get; set; }
        public string Conductor { get; set; }
        public int OpeningHymn { get; set; }
        public string OpeningPrayer { get; set; }
        public int SacramentHymn { get; set; }
        public int? IntermediateHymn { get; set; }
        public int ClosingHymn { get; set; }
        public string ClosingPrayer { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Speakers> Speakers { get; set; }
    }
}
