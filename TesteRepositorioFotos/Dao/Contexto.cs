using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TesteRepositorioFotos.Models;
using System.Configuration;

namespace TesteRepositorioFotos.Dao
{
    public class Contexto : DbContext
    {
        public Contexto() :base("TesteFotos")
        {

        }
        public DbSet<Fotos> Fotos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fotos>().HasKey(f => f.FotosId);
            base.OnModelCreating(modelBuilder);
        }

    }

   
}