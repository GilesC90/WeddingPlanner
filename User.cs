using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int? UserId {get; set; }

        [Required(ErrorMessage = "Please provide a first name")]
        [Display(Name = "First Name: ")]
        [MinLength(3, ErrorMessage = "Please provide a first name with at least 3 characters")]
        public string FirstName {get; set; }

        [Required(ErrorMessage = "Please provide a last name")]
        [Display(Name = "Last Name: ")]
        [MinLength(3, ErrorMessage = "Please provide a last name with at least 3 characters")]
        public string LastName {get; set; }

        [Required(ErrorMessage = "Please provide an email")]
        [MinLength(8, ErrorMessage = "Please provide an email with at least 8 characters")]
        public string Email {get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide a password")]
        [MinLength(8, ErrorMessage = "Please provide a password with at least 8 characters")]
        public string Password { get; set; }

        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;

        public List<Wedding> WeddingsCreated {get; set; }
        public List<RSVP> WeddingAttending {get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Your passwords must match.")]
        [DataType(DataType.Password)]
        public string Confirm {get; set; }

    }

    public class LoginUser
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [Display(Name = "Email: ")]
        public string LogEmail {get; set;}

        [Required(ErrorMessage = "You must enter a password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string LogPassword { get; set; }
    }
}