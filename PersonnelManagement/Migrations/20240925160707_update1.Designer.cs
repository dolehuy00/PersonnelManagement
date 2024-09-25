﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonnelManagement.Data;

#nullable disable

namespace PersonnelManagement.Migrations
{
    [DbContext(typeof(PersonnelDataContext))]
    [Migration("20240925160707_update1")]
    partial class update1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonnelManagement.Model.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PersonnelManagement.Model.AccountStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountStatuses");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Assignment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("DeptAssignmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriotityLevel")
                        .HasColumnType("int");

                    b.Property<long>("ResponsiblePesonId")
                        .HasColumnType("bigint");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeptAssignmentId");

                    b.HasIndex("ResponsiblePesonId");

                    b.HasIndex("StatusId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("PersonnelManagement.Model.AssignmentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssignmentStatuses");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LeaderId")
                        .HasColumnType("bigint");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("TaskDetail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PersonnelManagement.Model.DepartmentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DepartmentStatuses");
                });

            modelBuilder.Entity("PersonnelManagement.Model.DeptAssignment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("MainTaskDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriotityLevel")
                        .HasColumnType("int");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("DeptAssignments");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BasicSalary")
                        .HasColumnType("float");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<long?>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("TeamId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PersonnelManagement.Model.EmployeeStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeStatuses");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Duration")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PersonnelManagement.Model.ProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectStatuses");
                });

            modelBuilder.Entity("PersonnelManagement.Model.ProjectTeamDetail", b =>
                {
                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.HasKey("TeamId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTeamDetails");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PersonnelManagement.Model.SalaryHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("BasicSalary")
                        .HasColumnType("float");

                    b.Property<double>("BonusSalary")
                        .HasColumnType("float");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Penalty")
                        .HasColumnType("float");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StatusId");

                    b.ToTable("SalaryHistories");
                });

            modelBuilder.Entity("PersonnelManagement.Model.SalaryHistoryStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SalaryHistoryStatuses");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LeaderId")
                        .HasColumnType("bigint");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("PersonnelManagement.Model.TeamStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TeamStatuses");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Account", b =>
                {
                    b.HasOne("PersonnelManagement.Model.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("PersonnelManagement.Model.Account", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.AccountStatus", "Status")
                        .WithMany("Accounts")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Role");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Assignment", b =>
                {
                    b.HasOne("PersonnelManagement.Model.DeptAssignment", "DeptAssignment")
                        .WithMany("Assignments")
                        .HasForeignKey("DeptAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.Employee", "ResponsiblePeson")
                        .WithMany("Assignments")
                        .HasForeignKey("ResponsiblePesonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.AssignmentStatus", "Status")
                        .WithMany("Assignments")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DeptAssignment");

                    b.Navigation("ResponsiblePeson");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Department", b =>
                {
                    b.HasOne("PersonnelManagement.Model.Employee", "Leader")
                        .WithMany("LeaderOfDepartments")
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PersonnelManagement.Model.DepartmentStatus", "Status")
                        .WithMany("Departments")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Leader");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PersonnelManagement.Model.DeptAssignment", b =>
                {
                    b.HasOne("PersonnelManagement.Model.Department", "Department")
                        .WithMany("DeptAssignments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.Project", "Project")
                        .WithMany("DeptAssignments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Employee", b =>
                {
                    b.HasOne("PersonnelManagement.Model.EmployeeStatus", "Status")
                        .WithMany("Employees")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.Team", "Team")
                        .WithMany("Employees")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Status");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Project", b =>
                {
                    b.HasOne("PersonnelManagement.Model.ProjectStatus", "Status")
                        .WithMany("Projects")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PersonnelManagement.Model.ProjectTeamDetail", b =>
                {
                    b.HasOne("PersonnelManagement.Model.Project", "Project")
                        .WithMany("ProjectTeamDetails")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.Team", "Team")
                        .WithMany("ProjectTeamDetails")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("PersonnelManagement.Model.SalaryHistory", b =>
                {
                    b.HasOne("PersonnelManagement.Model.Employee", "Employee")
                        .WithMany("SalaryHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelManagement.Model.SalaryHistoryStatus", "Status")
                        .WithMany("SalaryHistories")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Team", b =>
                {
                    b.HasOne("PersonnelManagement.Model.Employee", "Leader")
                        .WithMany("LeaderOfTeams")
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PersonnelManagement.Model.TeamStatus", "Status")
                        .WithMany("Teams")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Leader");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PersonnelManagement.Model.AccountStatus", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("PersonnelManagement.Model.AssignmentStatus", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Department", b =>
                {
                    b.Navigation("DeptAssignments");
                });

            modelBuilder.Entity("PersonnelManagement.Model.DepartmentStatus", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("PersonnelManagement.Model.DeptAssignment", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Employee", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Assignments");

                    b.Navigation("LeaderOfDepartments");

                    b.Navigation("LeaderOfTeams");

                    b.Navigation("SalaryHistories");
                });

            modelBuilder.Entity("PersonnelManagement.Model.EmployeeStatus", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Project", b =>
                {
                    b.Navigation("DeptAssignments");

                    b.Navigation("ProjectTeamDetails");
                });

            modelBuilder.Entity("PersonnelManagement.Model.ProjectStatus", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("PersonnelManagement.Model.SalaryHistoryStatus", b =>
                {
                    b.Navigation("SalaryHistories");
                });

            modelBuilder.Entity("PersonnelManagement.Model.Team", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("ProjectTeamDetails");
                });

            modelBuilder.Entity("PersonnelManagement.Model.TeamStatus", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
