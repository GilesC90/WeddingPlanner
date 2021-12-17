using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WeddingPlanner.Extensions;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId {get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [Display(Name = "Wedder One: ")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Name may only contain letters and spaces.")]
        public string WedderOne {get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [Display(Name = "Wedder Two: ")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Name may only contain letters and spaces.")]
        public string WedderTwo {get; set; }

        [Required(ErrorMessage = "Please provide a wedding date.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{MM-DD-YYY HH:MM}", ApplyFormatInEditMode = true)]

        [DateValidator]
        public DateTime Date {get; set; }

        [Required(ErrorMessage = "Please provide an address for this wedding.")]
        [Display(Name = "Wedding Address: ")]
        // [RegularExpression(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$", ErrorMessage = "Please provide a valid address.")]
        public string WeddingAddress {get; set; }

        // Foreign Key setup for the User 
        public int? HostId {get; set; }
        public User HostedBy {get; set; }

        public List<RSVP> PeopleAttending {get; set; }
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}