using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public double DonationAmount { get; set; }
        public string Country { get; set; }
        public string HowDidYouHear { get; set; }
        public bool FollowUp { get; set; }
        public int CauseId { get; set; }
        [ForeignKey("DonationCauseId")]
        public virtual DonationCause DonationCause {get; set;}
    }
}
