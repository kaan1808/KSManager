﻿// <auto-generated />
using System;
using KSManager_API.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KSManager_API.Migrations
{
    [DbContext(typeof(KsDatabase))]
    partial class KsDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KSManager_API.DB.PasswordStorageData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<byte[]>("Icon")
                        .HasMaxLength(524288);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastChanges")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Note");

                    b.Property<string>("Password");

                    b.Property<string>("SecurityAnswer");

                    b.Property<string>("SecurityQuestion");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url");

                    b.Property<Guid>("UserId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordStorageDatas");
                });

            modelBuilder.Entity("KSManager_API.DB.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64);

                    b.Property<int>("Iterations");

                    b.Property<string>("LastName")
                        .HasMaxLength(64);

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("KSManager_API.DB.PasswordStorageData", b =>
                {
                    b.HasOne("KSManager_API.DB.User", "User")
                        .WithMany("PasswordStorageDatas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
