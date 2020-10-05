using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SwiftTransferProcessor.Data.Models
{
    public class Transfer
    {
        public int Id { get; set; }

        [Required]
        public string SenderReference { get; set; }

        [Required]
        public string BankOperationCode { get; set; }

        [Required]
        public DateTime ValueDate { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal InterbankSettledAmount { get; set; }

        [Required]
        public string SenderAccount { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public string SenderAddress { get; set; }

        [Required]
        public string SenderBIC { get; set; }

        [Required]
        public string BeneficiaryAccount { get; set; }

        [Required]
        public string BeneficiaryName { get; set; }

        [Required]
        public string BeneficiaryAddress { get; set; }

        [Required]
        public string BeneficiaryBIC { get; set; }

        [Required]
        public string DetailsOfCharges { get; set; }

        public string Reason { get; set; }

        [ForeignKey("FileRecord")]
        public int FileRecordId { get; set; }

        public virtual FileRecord File { get; set; }
    }
}
