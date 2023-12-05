using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Contactu> Contactus { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Home> Homes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<ViloationCustomer> ViloationCustomers { get; set; }

    public virtual DbSet<Violation> Violations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##MVC2;Password=Test123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##MVC2")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.Aboutid).HasName("SYS_C008580");

            entity.ToTable("ABOUT");

            entity.Property(e => e.Aboutid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ABOUTID");
            entity.Property(e => e.Content)
                .HasMaxLength(755)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Contactu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008576");

            entity.ToTable("CONTACTUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Message)
                .HasMaxLength(955)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SUBJECT");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008541");

            entity.ToTable("CUSTOMER");

            entity.HasIndex(e => e.Licensenumber, "SYS_C008542").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Contactnumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CONTACTNUMBER");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.LicenseCategory)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("LICENSE_CATEGORY");
            entity.Property(e => e.Licensenumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LICENSENUMBER");
            entity.Property(e => e.LicensingCenter)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LICENSING_CENTER");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NATIONALITY");
            entity.Property(e => e.Ssn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SSN");
        });

        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008571");

            entity.ToTable("HOME");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(555)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008531");

            entity.ToTable("ROLES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialid).HasName("SYS_C008583");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.Testimonialid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALID");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008544");

            entity.ToTable("USER_LOGIN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.Customer).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_USER_LOGIN_CUSTOMER");

            entity.HasOne(d => d.Role).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_USER_LOGIN_ROLES");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Vehicleid).HasName("SYS_C008555");

            entity.ToTable("VEHICLES");

            entity.HasIndex(e => e.VehicleRegistrationNumber, "SYS_C008556").IsUnique();

            entity.Property(e => e.Vehicleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("VEHICLEID");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COLOR");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.RegistrationStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("REGISTRATION_STATUS");
            entity.Property(e => e.StructureNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STRUCTURE_NO");
            entity.Property(e => e.VehicleCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VEHICLE_CATEGORY");
            entity.Property(e => e.VehicleNationality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VEHICLE_NATIONALITY");
            entity.Property(e => e.VehicleRegistrationNumber)
                .HasPrecision(7)
                .HasColumnName("VEHICLE_REGISTRATION_NUMBER");
            entity.Property(e => e.VehicleType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VEHICLE_TYPE");
            entity.Property(e => e.Year)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("YEAR");

            entity.HasOne(d => d.Customer).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CUSTOMER2");
        });

        modelBuilder.Entity<ViloationCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008585");

            entity.ToTable("VILOATION_CUSTOMER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.DateFrom)
                .HasColumnType("DATE")
                .HasColumnName("DATE_FROM");
            entity.Property(e => e.DateTo)
                .HasColumnType("DATE")
                .HasColumnName("DATE_TO");
            entity.Property(e => e.Violationid)
                .HasColumnType("NUMBER")
                .HasColumnName("VIOLATIONID");

            entity.HasOne(d => d.Customer).WithMany(p => p.ViloationCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CUSTOMER_CUSTOMER");

            entity.HasOne(d => d.Violation).WithMany(p => p.ViloationCustomers)
                .HasForeignKey(d => d.Violationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VIOLATION_CUSTOMER");
        });

        modelBuilder.Entity<Violation>(entity =>
        {
            entity.HasKey(e => e.Violationid).HasName("SYS_C008566");

            entity.ToTable("VIOLATIONS");

            entity.HasIndex(e => e.Citationnumber, "SYS_C008567").IsUnique();

            entity.Property(e => e.Violationid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("VIOLATIONID");
            entity.Property(e => e.Citationnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CITATIONNUMBER");
            entity.Property(e => e.CourtName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COURT_NAME");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Fineamount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("FINEAMOUNT");
            entity.Property(e => e.PoliceDirectorate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POLICE_DIRECTORATE");
            entity.Property(e => e.Street)
                .HasMaxLength(130)
                .IsUnicode(false)
                .HasColumnName("STREET");
            entity.Property(e => e.Vehicleid)
                .HasColumnType("NUMBER")
                .HasColumnName("VEHICLEID");
            entity.Property(e => e.ViolationDate)
                .HasColumnType("DATE")
                .HasColumnName("VIOLATION_DATE");
            entity.Property(e => e.ViolationDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("VIOLATION_DESCRIPTION");
            entity.Property(e => e.ViolationLocation)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("VIOLATION_LOCATION");
            entity.Property(e => e.ViolationTime)
                .HasPrecision(6)
                .HasColumnName("VIOLATION_TIME");

            entity.HasOne(d => d.Customer).WithMany(p => p.Violations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008568");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Violations)
                .HasForeignKey(d => d.Vehicleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008569");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
