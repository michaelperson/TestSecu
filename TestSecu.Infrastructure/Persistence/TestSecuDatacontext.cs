using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestSecu.Domain.Entities;

namespace TestSecu.Infrastructure.Persistence
{
    public class TestSecuDatacontext : DbContext
    {
        private readonly string _cnstr;
        public TestSecuDatacontext(string cnstr)
        {
            _cnstr = cnstr;
        }

        //Dbset
        public DbSet<UserEntity> Users { get; set; }

        //configurer le "moteur sql et ses options"
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Définir le moteur que je veux utiliser
            optionsBuilder.UseSqlServer(_cnstr);
        }

        //Configurer la façon dont mes classes(Models) sont traduites en table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("UserAccount");
        }
    }
}
