using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class DonationDetails
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        public int DonationCauseId { get; set; }
        [Required]
        public string DonorName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(1.00, double.MaxValue, ErrorMessage = "Donations require a minimum of $1")]
        public double DonationTotal { get; set; }
        public string PaymentStatus { get; set; }
        [Required]
        public string Email { get; set; }
        public string Comments { get; set; }
        public bool FollowUp { get; set; }

        public string TransactionId { get; set; }
        public DateTime DonationDate { get; set; }
        [ForeignKey("DonationCauseId")]
        public virtual DonationCause DonationCause { get; set; }
    }
}
