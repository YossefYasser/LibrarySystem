﻿// <auto-generated />
using System;
using EnozomTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnozomTask.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241001094554_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EnozomTask.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Clean Code"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Algorithms"
                        });
                });

            modelBuilder.Entity("EnozomTask.Models.Copy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("CopyStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CopyStatusId");

                    b.ToTable("Copies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            CopyStatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookId = 1,
                            CopyStatusId = 1
                        },
                        new
                        {
                            Id = 3,
                            BookId = 2,
                            CopyStatusId = 1
                        });
                });

            modelBuilder.Entity("EnozomTask.Models.CopyStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Status = "Good"
                        },
                        new
                        {
                            Id = 2,
                            Status = "Damaged"
                        },
                        new
                        {
                            Id = 3,
                            Status = "Lost"
                        },
                        new
                        {
                            Id = 4,
                            Status = "Borrowed"
                        });
                });

            modelBuilder.Entity("EnozomTask.Models.EnozomTask.Models.Borrowing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CopyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CopyId");

                    b.HasIndex("StatusId");

                    b.HasIndex("StudentId");

                    b.ToTable("Borrowings");
                });

            modelBuilder.Entity("EnozomTask.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("StudentId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            Email = "Ali@enozom.com",
                            Name = "Ali",
                            PhoneNumber = "0122224400"
                        },
                        new
                        {
                            StudentId = 2,
                            Email = "mohamed@enozom.com",
                            Name = "Mohamed",
                            PhoneNumber = "0111155000"
                        },
                        new
                        {
                            StudentId = 3,
                            Email = "Ahmed@enozom.com",
                            Name = "Ahmed",
                            PhoneNumber = "0155553311"
                        });
                });

            modelBuilder.Entity("EnozomTask.Models.Copy", b =>
                {
                    b.HasOne("EnozomTask.Models.Book", "Book")
                        .WithMany("Copies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnozomTask.Models.CopyStatus", "CopyStatus")
                        .WithMany()
                        .HasForeignKey("CopyStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("CopyStatus");
                });

            modelBuilder.Entity("EnozomTask.Models.EnozomTask.Models.Borrowing", b =>
                {
                    b.HasOne("EnozomTask.Models.Copy", "Copy")
                        .WithMany()
                        .HasForeignKey("CopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnozomTask.Models.CopyStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnozomTask.Models.Student", "Student")
                        .WithMany("Borrowings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Copy");

                    b.Navigation("Status");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("EnozomTask.Models.Book", b =>
                {
                    b.Navigation("Copies");
                });

            modelBuilder.Entity("EnozomTask.Models.Student", b =>
                {
                    b.Navigation("Borrowings");
                });
#pragma warning restore 612, 618
        }
    }
}
