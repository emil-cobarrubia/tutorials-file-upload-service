using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PatientManager_API_BackEnd_Eval.Models;

namespace PatientManager_API_BackEnd_Eval.Contexts;

public partial class PrototypesDevContext : DbContext
{
    public PrototypesDevContext()
    {
    }

    public PrototypesDevContext(DbContextOptions<PrototypesDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PROTOTYPES_DEV;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__Patients__C5775520D7019198");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Address1).HasMaxLength(255);
            entity.Property(e => e.Address2).HasMaxLength(255);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastUpdate).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.SystemStatus).HasMaxLength(10);
            entity.Property(e => e.Zip)
                .HasMaxLength(10)
                .HasColumnName("ZIP");
            //entity.Property(e => e.AgeYears).HasComputedColumnSql("DATEDIFF(year, BirthDate, {fn now()})");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
