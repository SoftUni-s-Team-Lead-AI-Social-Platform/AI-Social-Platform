﻿// <auto-generated />
using System;
using AI_Social_Platform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    [DbContext(typeof(ASPDbContext))]
    [Migration("20231204225008_publicationDatesExtend")]
    partial class publicationDatesExtend
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AI_Social_Platform.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasDefaultValue("Test");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasDefaultValue("Test");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "72e4aca8-e4cb-4f92-b36a-b2db3f9b74ac",
                            Email = "user@user.com",
                            EmailConfirmed = false,
                            FirstName = "Georgi",
                            LastName = "Georgiev",
                            LockoutEnabled = false,
                            NormalizedEmail = "user@user.com",
                            NormalizedUserName = "USER@USER.com",
                            PasswordHash = "AQAAAAEAACcQAAAAEIAkDSw4gA1pzVXqL3tfd0HaOEcahC9oypuAD58W4f89Hpogcj7x5IQz2Qz+JElFsA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a047c1a6-fda1-4f4f-90ca-fb9e81844ff0",
                            TwoFactorEnabled = false,
                            UserName = "user@user.com"
                        },
                        new
                        {
                            Id = new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4a4f573f-087a-4654-9dba-ed14bfb4301d",
                            Email = "admin@admin.com",
                            EmailConfirmed = false,
                            FirstName = "Ivan",
                            LastName = "Ivanov",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.com",
                            NormalizedUserName = "ADMIN@ADMIN.com",
                            PasswordHash = "AQAAAAEAACcQAAAAEABS1bRfIhl+tSKyLWrodL1RasMutv2kE22N3TUHpZ4sZKuGU8oe0Mva9tPPF9Lrbw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cd044a54-1374-4d18-9c29-e2857ef77b95",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com"
                        });
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("DataFile")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("UserId");

                    b.ToTable("MediaFiles");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Publication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastCommented")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Publications");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e0b2de99-fe73-4f36-ae7f-d70b496b27d9"),
                            AuthorId = new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                            Content = "This is the first seeded publication Content from Ivan",
                            DateCreated = new DateTime(2023, 12, 4, 22, 50, 7, 889, DateTimeKind.Utc).AddTicks(7578)
                        },
                        new
                        {
                            Id = new Guid("8f82ff0f-1d2b-4362-80dd-e79daddfadd4"),
                            AuthorId = new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                            Content = "This is the second seeded publication Content from Georgi",
                            DateCreated = new DateTime(2023, 12, 4, 22, 50, 7, 889, DateTimeKind.Utc).AddTicks(7592)
                        });
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Share", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("UserId");

                    b.ToTable("Shares");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.ApplicationUser", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", null)
                        .WithMany("Friends")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Media", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.Publication.Publication", "Publication")
                        .WithMany("MediaFiles")
                        .HasForeignKey("PublicationId");

                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Comment", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.Publication.Publication", "Publication")
                        .WithMany("Comments")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Like", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.Publication.Publication", "Publication")
                        .WithMany("Likes")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Publication", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", "Author")
                        .WithMany("Publications")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Share", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.Publication.Publication", "Publication")
                        .WithMany("Shares")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", "User")
                        .WithMany("Shares")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("AI_Social_Platform.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Friends");

                    b.Navigation("Likes");

                    b.Navigation("Publications");

                    b.Navigation("Shares");
                });

            modelBuilder.Entity("AI_Social_Platform.Data.Models.Publication.Publication", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("MediaFiles");

                    b.Navigation("Shares");
                });
#pragma warning restore 612, 618
        }
    }
}
