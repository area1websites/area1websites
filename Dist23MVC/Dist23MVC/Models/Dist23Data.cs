using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Dist23MVC.Models
{
    public class Dist23Data:DbContext
    {
        public Dist23Data()
            : base("Name=Dist23Data")
        {
            Database.SetInitializer<Dist23Data>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<News> News { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Meetings> Meetings { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<EventCat> EventCat { get; set; }
        public DbSet<OtherDistEvents> OtherDistEvents { get; set; }

        public DbSet<dayorder> dayorder { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<VolunteerList> VolunteerList { get; set; }
        public DbSet<NextMeeting> NextMeetings { get; set; }
        public DbSet<CommHeaders> CommHeaders { get; set; }
        public DbSet<CommLinks> CommLinks { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<ContactPosition> ContactPosition { get; set; }
        public DbSet<SiteConfig> SiteConfig { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<PaymentSetup> PaymentSetup { get; set; }
        public DbSet<PaymentSpecValues> PaymentSpecValues { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<KeyList> KeyList { get; set; }
    }
}