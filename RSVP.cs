using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WeddingPlanner.Models
{
    public class RSVP
    {
        [Key]
        public int RsvpId {get; set; }
        public int UserId {get; set; }
        public User RSVPedBy {get; set; }
        public int WeddingId {get; set; }
        public Wedding WeddingRSVPed {get; set; }
        public bool AreYouGoing {get; set; }
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;

    }
}