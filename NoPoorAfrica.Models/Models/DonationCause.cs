using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class DonationCause
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double FundingGoal { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
    }
}
