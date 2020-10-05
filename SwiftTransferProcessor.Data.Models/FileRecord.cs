using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwiftTransferProcessor.Data.Models
{
    public class FileRecord
    {
        public FileRecord()
        {
            this.Transactions = new HashSet<Transfer>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime TimeOfArrival { get; set; }

        public virtual ICollection<Transfer> Transactions { get; set; }
    }
}
