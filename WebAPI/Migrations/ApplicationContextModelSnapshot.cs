﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPI.Services.DataBase;

namespace WebAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    internal class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.User", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Login")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("WebAPI.Models.UserBankingAccount", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<decimal>("Money")
                    .HasColumnType("decimal(18,2)");

                b.Property<bool>("State")
                    .HasColumnType("bit");

                b.Property<Guid>("UserId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("UserId")
                    .IsUnique();

                b.ToTable("BankingAccount");
            });

            modelBuilder.Entity("WebAPI.Models.UserProfile", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("Age")
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid>("UserId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("UserId")
                    .IsUnique();

                b.ToTable("Profile");
            });

            modelBuilder.Entity("WebAPI.Models.UserBankingAccount", b =>
            {
                b.HasOne("WebAPI.Models.User", "User")
                    .WithOne("BankingAccount")
                    .HasForeignKey("WebAPI.Models.UserBankingAccount", "UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });

            modelBuilder.Entity("WebAPI.Models.UserProfile", b =>
            {
                b.HasOne("WebAPI.Models.User", "User")
                    .WithOne("Profile")
                    .HasForeignKey("WebAPI.Models.UserProfile", "UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });

            modelBuilder.Entity("WebAPI.Models.User", b =>
            {
                b.Navigation("BankingAccount");

                b.Navigation("Profile");
            });
#pragma warning restore 612, 618
        }
    }
}