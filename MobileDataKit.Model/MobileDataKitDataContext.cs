using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model
{
 public   class MobileDataKitDataContext : DbContext
    {


       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Core.Model.Project>().ToTable("Project");
            modelBuilder.Entity<FormServer>().ToTable("Form");
            modelBuilder.Entity<FormSchema>().ToTable("FormSchema");
            modelBuilder.Entity<Company>().ToTable("Company");
           modelBuilder.Entity<CompanyUser>().ToTable("CompanyUser")
             .HasOne(p=>p.Company)
               .WithMany(p=>p.CompanyUsers).HasForeignKey(p=>p.CompanyEmail );

            modelBuilder.Entity<ProjectUser>().ToTable("ProjectUser")
            .HasOne(p => p.CompanyDevice)
            .WithMany(p => p.ProjectUsers).HasForeignKey(p => p.CompanyDeviceID).HasConstraintName("ForeignKey_ProjectUser_CompanyDevice");
            // .WithMany(p =>p.);


            modelBuilder.Entity<Device>().ToTable("Device");
            modelBuilder.Entity<CompanyDevice>().ToTable("CompanyDevice")
                .HasOne(p => p.Device)
                .WithMany(p => p.CompanyDevices)
                .HasForeignKey(p => p.IMEI)
                .HasConstraintName("ForeignKey_CompanyDevice_Device")

                ;

            modelBuilder.Entity<MobileDataKit.Core.Model.EntryVariable>().ToTable("EntryVariable");

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(Settings.Configuration.Connection["DefaultConnection"]);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<MobileDataKit.Core.Model.EntryVariable> EntryVariables { get; set; }
        public DbSet<Core.Model.Project> Projects { get; set; }
        public DbSet<FormServer> Forms { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<CompanyDevice> CompanyDevices { get; set; }
        public DbSet<FormUser> FormUsers { get; set; }
        
        public DbSet<FormSchema> FormSchemas { get; set; }
    }
}
