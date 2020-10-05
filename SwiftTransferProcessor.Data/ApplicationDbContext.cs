using Microsoft.EntityFrameworkCore;
using SwiftTransferProcessor.Data;
using SwiftTransferProcessor.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.SwiftTransferProcessor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Transfer> Transactions { get; set; }

        public DbSet<FileRecord> FileRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

    }
}
