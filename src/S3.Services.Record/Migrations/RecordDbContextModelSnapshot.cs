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
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("S3.Services.Record.Domain.Rule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("CAPercentage");

                    b.Property<float>("ClassParticipationPercentage");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<float>("FirstExamPercentage");

                    b.Property<float>("HomeworkPercentage");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("SchoolId");

                    b.Property<float>("SecondExamPercentage");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("S3.Services.Record.Domain.StudentScore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClassId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<float>("Score");

                    b.Property<int>("Session");

                    b.Property<Guid>("StudentId");

                    b.Property<Guid>("SubjectId");

                    b.Property<int>("Term");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("StudentScores");
                });
#pragma warning restore 612, 618
        }
    }
}
