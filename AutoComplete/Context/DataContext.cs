using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AutoComplete.Models;

namespace AutoComplete
{
    public class DataContext : DbContext
    {
         public DataContext(string connString)
            : base(connString)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Practices> Practices { get; set; }
        public DbSet<AdditionalSurgeries> AdditionalSurgeries { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<AboutPerson> AboutPerson { get; set; }
        public DbSet<Expences> Expences { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<Categrory> Categrory { get; set; }
        public DbSet<bookmarks> bookmarks { get; set; }
        public DbSet<webcontent> webcontent{ get; set; }
        public DbSet<InvoiceFileGenerator> Invoices { get; set; }
         
        public DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //A doctor/person can have more then 1 practices
            modelBuilder.Entity<Person>()
                .HasMany(p => p.practices)
                .WithRequired(Practices => Practices.person);
            //A practice can have more then 1 additional surgery and sessions
            //     modelBuilder.Entity<Practices>()
            //.HasRequired(diner => diner.sessions)
            //.WithMany()
            //.Map(s => { s.MapKey(Sessions => Sessions.Id, "Name_SessionsID"); });
            modelBuilder.Entity<Practices>()
                .HasMany(p => p.sessions)
                .WithRequired(Sessions => Sessions.Practices).WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<SessionColor> SessionColors { get; set; }
    }
}
