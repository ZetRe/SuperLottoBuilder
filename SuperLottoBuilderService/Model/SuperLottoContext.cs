using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperLottoBuilderService.Model
{
    public class SuperLottoContext : DbContext
    {
        public SuperLottoContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected SuperLottoContext()
        {
        }

        public DbSet<GenerateNumber> GenerateNumbers { get; set; }

        public DbSet<WinningNumber> WinningNumbers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenerateNumber>()
                 .HasIndex(entity => entity.Period);
        }

    }
}
