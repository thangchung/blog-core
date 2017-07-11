using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BlogCore.Blog.Infrastructure;
using BlogCore.Blog.Domain;

namespace BlogCore.Blog.Migrator.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20170711050914_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogCore.Blog.Domain.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BlogSettingId");

                    b.Property<string>("Description");

                    b.Property<string>("ImageFilePath")
                        .IsRequired();

                    b.Property<string>("OwnerEmail")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<int>("Theme");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BlogSettingId");

                    b.ToTable("Blogs","blog");
                });

            modelBuilder.Entity("BlogCore.Blog.Domain.BlogSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DaysToComment");

                    b.Property<bool>("ModerateComments");

                    b.Property<int>("PostsPerPage");

                    b.HasKey("Id");

                    b.ToTable("BlogSettings","blog");
                });

            modelBuilder.Entity("BlogCore.Blog.Domain.Blog", b =>
                {
                    b.HasOne("BlogCore.Blog.Domain.BlogSetting", "BlogSetting")
                        .WithMany()
                        .HasForeignKey("BlogSettingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
