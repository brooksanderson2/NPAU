using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class DonationDetails
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int DonationCauseId { get; set; }

        public string DonorName { get; set; }
        [Required]
        public double DonationTotal { get; set; }
        public string PaymentStatus { get; set; }
        public string Email { get; set; }

        public string TransactionId { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
