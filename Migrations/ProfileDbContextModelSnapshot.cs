﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportLize.Profile.Api.Profile.Repository;

#nullable disable

namespace SportLize.Profile.Api.Migrations
{
    [DbContext(typeof(ProfileDbContext))]
    partial class ProfileDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PubblicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<byte[]>("Media")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("PubblicationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.TransactionalOutbox", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Table")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionalOutbox", (string)null);
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Actor")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBorn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.Comment", b =>
                {
                    b.HasOne("SportLize.Profile.Api.Profile.Repository.Model.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.Post", b =>
                {
                    b.HasOne("SportLize.Profile.Api.Profile.Repository.Model.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.User", b =>
                {
                    b.HasOne("SportLize.Profile.Api.Profile.Repository.Model.User", null)
                        .WithMany("Followers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SportLize.Profile.Api.Profile.Repository.Model.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
