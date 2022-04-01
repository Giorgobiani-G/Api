using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnApi.Models;

namespace LearnApi.Models
{
    public class CitizenDbContext : DbContext
    {
        public CitizenDbContext(DbContextOptions<CitizenDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FizikPiri>().Property("SaxeliLatinuri").HasColumnType("varchar(50)");
            modelBuilder.Entity<FizikPiri>().Property("GvariLatinuri").HasColumnType("varchar(50)");
            
        }


        public  DbSet<Image> Images { get; set; }

        public DbSet<FizikPiri> FizikPiris { get; set; }

        public DbSet<LearnApi.Models.ContactInfo> ContactInfo { get; set; }

        public DbSet<ConnectedPerson> ConnectedPersons { get; set; }
    }
}
