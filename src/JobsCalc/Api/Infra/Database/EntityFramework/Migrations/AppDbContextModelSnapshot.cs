﻿// <auto-generated />
using System;
using JobsCalc.Api.Infra.Database.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobsCalc.Api.Infra.Database.EntityFramework.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JobsCalc.Api.Domain.Entities.Job", b =>
                {
                    b.Property<Guid>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("job_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("DailyHours")
                        .HasColumnType("integer")
                        .HasColumnName("daily_hours");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("TotalHours")
                        .HasColumnType("integer")
                        .HasColumnName("total_hours");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("jobs", (string)null);
                });

            modelBuilder.Entity("JobsCalc.Api.Domain.Entities.Planning", b =>
                {
                    b.Property<Guid>("PlanningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("planning_id");

                    b.Property<int>("DaysPerWeek")
                        .HasColumnType("integer")
                        .HasColumnName("days_per_week");

                    b.Property<int>("HoursPerDay")
                        .HasColumnType("integer")
                        .HasColumnName("hours_per_day");

                    b.Property<decimal>("MonthlyBudget")
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("monthly_budget");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int?>("VacationPerYear")
                        .HasColumnType("integer")
                        .HasColumnName("vacation_per_year");

                    b.Property<decimal>("ValueHour")
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("value_hour");

                    b.HasKey("PlanningId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("plannings", (string)null);
                });

            modelBuilder.Entity("JobsCalc.Api.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text")
                        .HasColumnName("avatar_url");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.HasKey("UserId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("JobsCalc.Api.Domain.Entities.Job", b =>
                {
                    b.HasOne("JobsCalc.Api.Domain.Entities.User", "User")
                        .WithMany("Jobs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobsCalc.Api.Domain.Entities.Planning", b =>
                {
                    b.HasOne("JobsCalc.Api.Domain.Entities.User", "User")
                        .WithOne("Planning")
                        .HasForeignKey("JobsCalc.Api.Domain.Entities.Planning", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobsCalc.Api.Domain.Entities.User", b =>
                {
                    b.Navigation("Jobs");

                    b.Navigation("Planning");
                });
#pragma warning restore 612, 618
        }
    }
}
