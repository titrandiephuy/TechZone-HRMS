using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TechZone_HRMS.Domain.Models;

#nullable disable

namespace TechZone_HRMS.Domain
{
    public partial class EmployeesManagementContext : IdentityDbContext<AppIdentityUser, IdentityRole, string>
    {
        public EmployeesManagementContext()
        {
        }

        public EmployeesManagementContext(DbContextOptions<EmployeesManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<InCharge> InCharges { get; set; }
        public virtual DbSet<Leaves> Leaves { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("workstation id=EmployeesManagement.mssql.somee.com;packet size=4096;user id=Marphantom_SQLLogin_1;pwd=srmz295xet;data source=EmployeesManagement.mssql.somee.com;persist security info=False;initial catalog=EmployeesManagement");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<EducationLevel>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Employee>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<InCharge>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Leaves>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Position>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Salary>().Metadata.SetIsTableExcludedFromMigrations(true);

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentLocation)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DepartmentPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EducationLevel>(entity =>
            {
                entity.ToTable("EducationLevel");

                entity.Property(e => e.Degree)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Major)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmployeeAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmployeeAvatar).IsRequired();

                entity.Property(e => e.EmployeePhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ethnicity)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PlaceOfOrigin)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Departments");

                entity.HasOne(d => d.EducationLevel)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EducationLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_EducationLevel");
            });

            modelBuilder.Entity<InCharge>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("InCharge");

                entity.Property(e => e.EmployeedDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_InCharge_Employees");

                entity.HasOne(d => d.Position)
                    .WithMany()
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_InCharge_Positions");
            });

            modelBuilder.Entity<Leaves>(entity =>
            {
                entity.HasKey(e => e.LeaveId);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.LeaveName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Leaves_Employees");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salary_Employees");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
