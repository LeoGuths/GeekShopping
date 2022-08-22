﻿// <auto-generated />
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.ProductAPI.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20220822163546_SeedProductDataTable")]
    partial class SeedProductDataTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.ProductAPI.Model.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("category_name");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.ToTable("product");

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            CategoryName = "T-shirt",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ut egestas ipsum, non venenatis nulla. Morbi iaculis aliquam urna. Curabitur at volutpat arcu. Morbi at dictum massa, in sodales velit. Nulla facilisi. Curabitur quam turpis. ",
                            ImageUrl = "https://gitlab.com/uploads/-/system/user/avatar/12037378/avatar.png?width=400",
                            Name = "T-shirt",
                            Price = 69.9m
                        },
                        new
                        {
                            Id = 3L,
                            CategoryName = "Action Figure",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ut egestas ipsum, non venenatis nulla. Morbi iaculis aliquam urna. Curabitur at volutpat arcu. Morbi at dictum massa, in sodales velit. Nulla facilisi. Curabitur quam turpis. ",
                            ImageUrl = "https://gitlab.com/uploads/-/system/user/avatar/12037378/avatar.png?width=400",
                            Name = "Bleach Aizen Action Figure PVC",
                            Price = 109.9m
                        },
                        new
                        {
                            Id = 4L,
                            CategoryName = "Sweatshirt",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ut egestas ipsum, non venenatis nulla. Morbi iaculis aliquam urna. Curabitur at volutpat arcu. Morbi at dictum massa, in sodales velit. Nulla facilisi. Curabitur quam turpis. ",
                            ImageUrl = "https://gitlab.com/uploads/-/system/user/avatar/12037378/avatar.png?width=400",
                            Name = "Sweatshirt",
                            Price = 329.9m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
