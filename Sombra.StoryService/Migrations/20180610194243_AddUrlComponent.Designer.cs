﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService.Migrations
{
    [DbContext(typeof(StoryContext))]
    [Migration("20180610194243_AddUrlComponent")]
    partial class AddUrlComponent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sombra.StoryService.DAL.Charity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CharityKey");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CharityKey");

                    b.HasIndex("Url");

                    b.ToTable("Charities");
                });

            modelBuilder.Entity("Sombra.StoryService.DAL.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Base64");

                    b.Property<Guid?>("StoryId");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Sombra.StoryService.DAL.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AuthorId");

                    b.Property<Guid>("CharityId");

                    b.Property<string>("ConclusionText");

                    b.Property<string>("CoreText");

                    b.Property<string>("CoverImage");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("OpeningText");

                    b.Property<string>("QuoteText");

                    b.Property<string>("StoryImage");

                    b.Property<Guid>("StoryKey");

                    b.Property<string>("Title");

                    b.Property<string>("UrlComponent");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CharityId");

                    b.HasIndex("StoryKey");

                    b.HasIndex("UrlComponent");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("Sombra.StoryService.DAL.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ProfileImage");

                    b.Property<Guid>("UserKey");

                    b.HasKey("Id");

                    b.HasIndex("UserKey");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sombra.StoryService.DAL.Image", b =>
                {
                    b.HasOne("Sombra.StoryService.DAL.Story")
                        .WithMany("Images")
                        .HasForeignKey("StoryId");
                });

            modelBuilder.Entity("Sombra.StoryService.DAL.Story", b =>
                {
                    b.HasOne("Sombra.StoryService.DAL.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Sombra.StoryService.DAL.Charity", "Charity")
                        .WithMany()
                        .HasForeignKey("CharityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
