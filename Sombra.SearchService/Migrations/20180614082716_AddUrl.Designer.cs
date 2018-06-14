﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService.Migrations
{
    [DbContext(typeof(SearchContext))]
    [Migration("20180614082716_AddUrl")]
    partial class AddUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sombra.SearchService.DAL.Content", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<Guid>("CharityActionKey");

                    b.Property<string>("CharityActionName");

                    b.Property<Guid>("CharityKey");

                    b.Property<string>("CharityName");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Content");
                });
#pragma warning restore 612, 618
        }
    }
}
