using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    class DonationProgress
    {
        [Key]
        public int Id { get; set; }

        public string DonationId { get; set; }

        [ForeignKey("DonationId")]
        public virtual Donation Donation { get; set; }

        public string DonationCauseId { get; set; }
        [ForeignKey("DonationCauseId")]
        public virtual DonationCause DonationCause { get; set; }

        [Required]
        [Display(Name = "Donation Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double DonationTotal { get; set; }
    }
}
