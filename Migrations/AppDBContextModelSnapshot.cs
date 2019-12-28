using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Teretana;

namespace Teretana.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3");

            modelBuilder.Entity("Teretana.Data.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("JMB");

                    b.Property<bool>("active");

                    b.Property<DateTime>("birthDate");

                    b.Property<bool>("gender");

                    b.Property<byte[]>("imgBytes");

                    b.Property<DateTime>("joinDate");

                    b.Property<string>("lastName");

                    b.Property<int?>("membershipTypeid");

                    b.Property<string>("name");

                    b.Property<DateTime>("untilDate");

                    b.HasKey("Id");

                    b.HasIndex("membershipTypeid");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Teretana.Data.MembershipFee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.Property<double>("discount");

                    b.Property<int?>("memberId");

                    b.Property<int?>("membershipTypeid");

                    b.Property<int>("numberOfMonths");

                    b.Property<double>("price");

                    b.Property<double>("realPrice");

                    b.HasKey("Id");

                    b.HasIndex("memberId");

                    b.HasIndex("membershipTypeid");

                    b.ToTable("MembershipFees");
                });

            modelBuilder.Entity("Teretana.Data.MembershipType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("active");

                    b.Property<string>("name");

                    b.Property<double>("price");

                    b.HasKey("id");

                    b.ToTable("MembershipTypes");
                });

            modelBuilder.Entity("Teretana.Data.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<double>("price");

                    b.Property<byte[]>("productImage");

                    b.Property<int>("state");

                    b.Property<bool>("type");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Teretana.Data.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.Property<double>("total");

                    b.Property<bool>("type");

                    b.HasKey("Id");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Teretana.Data.PurchaseItem", b =>
                {
                    b.Property<int>("productId");

                    b.Property<int>("purchaseId");

                    b.Property<int>("count");

                    b.Property<int?>("productId1");

                    b.Property<int?>("purchaseId1");

                    b.HasKey("productId", "purchaseId");

                    b.HasIndex("productId1");

                    b.HasIndex("purchaseId1");

                    b.ToTable("PurchaseItems");
                });

            modelBuilder.Entity("Teretana.Data.Training", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("TrainingDate");

                    b.Property<DateTime>("EndingDate");

                    b.Property<int?>("MemberId");

                    b.HasKey("Id", "TrainingDate");

                    b.HasIndex("MemberId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("Teretana.Data.TrainingFee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("TrainingDate");

                    b.Property<double>("price");

                    b.Property<int?>("trainingId");

                    b.HasKey("Id");

                    b.HasIndex("trainingId", "TrainingDate");

                    b.ToTable("TrainingFees");
                });

            modelBuilder.Entity("Teretana.Data.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("active");

                    b.Property<bool>("logged");

                    b.Property<bool>("master");

                    b.Property<string>("password");

                    b.Property<bool>("type");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Teretana.Data.Member", b =>
                {
                    b.HasOne("Teretana.Data.MembershipType", "membershipType")
                        .WithMany()
                        .HasForeignKey("membershipTypeid");
                });

            modelBuilder.Entity("Teretana.Data.MembershipFee", b =>
                {
                    b.HasOne("Teretana.Data.Member", "member")
                        .WithMany()
                        .HasForeignKey("memberId");

                    b.HasOne("Teretana.Data.MembershipType", "membershipType")
                        .WithMany()
                        .HasForeignKey("membershipTypeid");
                });

            modelBuilder.Entity("Teretana.Data.PurchaseItem", b =>
                {
                    b.HasOne("Teretana.Data.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId1");

                    b.HasOne("Teretana.Data.Purchase", "purchase")
                        .WithMany()
                        .HasForeignKey("purchaseId1");
                });

            modelBuilder.Entity("Teretana.Data.Training", b =>
                {
                    b.HasOne("Teretana.Data.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("Teretana.Data.TrainingFee", b =>
                {
                    b.HasOne("Teretana.Data.Training", "training")
                        .WithMany()
                        .HasForeignKey("trainingId", "TrainingDate");
                });
        }
    }
}
