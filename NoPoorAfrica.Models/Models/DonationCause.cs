using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class DonationCause
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double FundingGoal { get; set; }
        public double GoalProgress { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        [Display(Name = "Donation Cause Category Type")]
        public int DonationCauseCategoryId { get; set; }
        [ForeignKey("DonationCauseCategoryId")]
        public virtual DonationCauseCategory DonationCauseCategory { get; set; }

        [Display(Name = "Active Cause")]
        public Boolean IsActive { get; set; }   
        [Display(Name = "Featured Cause")]
        public Boolean IsFeatured { get; set; }
    }
}
