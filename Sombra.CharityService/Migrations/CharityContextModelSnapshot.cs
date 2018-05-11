﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Sombra.CharityService.DAL;

namespace Sombra.CharityService.Migrations
{
    [DbContext(typeof(CharityContext))]
    partial class CharityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sombra.CharityService.DAL.CharityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<string>("CharityId");

                    b.Property<string>("EmailCharity");

                    b.Property<string>("IBAN");

                    b.Property<string>("KVKNumber");

                    b.Property<string>("NameCharity");

                    b.Property<string>("NameOwner");

                    b.HasKey("Id");

                    b.ToTable("Charities");
                });
#pragma warning restore 612, 618
        }
    }
}
