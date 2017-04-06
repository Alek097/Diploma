using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Diploma.Data;

namespace Diploma.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Diploma.Data.Models.Ban", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cause");

                    b.Property<Guid>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastModifyDate");

                    b.Property<Guid>("ModifyBy");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bans");
                });

            modelBuilder.Entity("Diploma.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModifyDate");

                    b.Property<Guid>("ModifyBy");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Diploma.Data.Models.Characteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CharacteristicsGroupId");

                    b.Property<Guid>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModifyDate");

                    b.Property<Guid>("ModifyBy");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ProductId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicsGroupId");

                    b.HasIndex("ProductId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("Diploma.Data.Models.CharacteristicsGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModifyDate");

                    b.Property<Guid>("ModifyBy");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("CharacteristicsGroups");
                });

            modelBuilder.Entity("Diploma.Data.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModifyDate");

                    b.Property<Guid>("ModifyBy");

                    b.Property<decimal>("TotalPrice");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Diploma.Data.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId");

                    b.Property<Guid>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastModifyDate");

                    b.Property<Guid>("ModifyBy");

                    b.Property<string>("Name");

                    b.Property<Guid?>("OrderId");

                    b.Property<string>("PhotoPath");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrderId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Diploma.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsBanned");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostCode");

                    b.Property<string>("Region");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Diploma.Data.Models.Ban", b =>
                {
                    b.HasOne("Diploma.Data.Models.User")
                        .WithMany("Bans")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Diploma.Data.Models.Characteristic", b =>
                {
                    b.HasOne("Diploma.Data.Models.CharacteristicsGroup")
                        .WithMany("Characteristics")
                        .HasForeignKey("CharacteristicsGroupId");

                    b.HasOne("Diploma.Data.Models.Product")
                        .WithMany("Characteristics")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Diploma.Data.Models.CharacteristicsGroup", b =>
                {
                    b.HasOne("Diploma.Data.Models.Product")
                        .WithMany("CharacteristicsGroups")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Diploma.Data.Models.Order", b =>
                {
                    b.HasOne("Diploma.Data.Models.User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Diploma.Data.Models.Product", b =>
                {
                    b.HasOne("Diploma.Data.Models.Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Diploma.Data.Models.Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Diploma.Data.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Diploma.Data.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Diploma.Data.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
