﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using S3.Services.Record.Utility;

namespace S3.Services.Record.Migrations
{
    [DbContext(typeof(RecordDbContext))]
    partial class RecordDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("S3.Services.Record.Domain.ClassSubjectScores", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClassId");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ExamType")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<Guid>("SchoolId");

                    b.Property<int>("Session");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Term");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("ClassSubjectScores");
                });

            modelBuilder.Entity("S3.Services.Record.Domain.Rule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("A_DistinctionPoint");

                    b.Property<int>("B_VeryGoodPoint");

                    b.Property<float>("CAPercentage");

                    b.Property<int>("C_CreditPoint");

                    b.Property<float>("ClassParticipationPercentage");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("F_FailPoint");

                    b.Property<float>("FirstExamPercentage");

                    b.Property<float>("HomeworkPercentage");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("P_PassPoint");

                    b.Property<Guid>("SchoolId");

                    b.Property<float>("SecondExamPercentage");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("S3.Services.Record.Domain.Score", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClassSubjectScoresId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<float>("Mark");

                    b.Property<Guid>("StudentId");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ClassSubjectScoresId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("S3.Services.Record.Domain.Score", b =>
                {
                    b.HasOne("S3.Services.Record.Domain.ClassSubjectScores", "ClassSubjectScores")
                        .WithMany("Scores")
                        .HasForeignKey("ClassSubjectScoresId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
