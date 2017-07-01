using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BlogCore.Blog.Infrastructure;

namespace BlogCore.Blog.Migrator.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20170701102054_InitDatabase")]
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

                    b.Property<int>("DaysToComment");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<bool>("ModerateComments");

                    b.Property<string>("OwnerEmail");

                    b.Property<int>("PostsPerPage");

                    b.Property<string>("Theme");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Blogs","blog");
                });

            modelBuilder.Entity("BlogCore.Blog.Domain.PostId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BlogId");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("PostIds","blog");
                });

            modelBuilder.Entity("BlogCore.Blog.Domain.PostId", b =>
                {
                    b.HasOne("BlogCore.Blog.Domain.Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId");
                });
        }
    }
}
