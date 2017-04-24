using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BlogCore.Infrastructure.Data;

namespace BlogCore.Infrastructure.MigrationConsole.Migrations
{
    [DbContext(typeof(BlogCoreDbContext))]
    partial class BlogCoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogCore.Core.BlogFeature.Blog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DaysToComment");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<bool>("ModerateComments");

                    b.Property<int>("PostsPerPage");

                    b.Property<string>("Theme");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("BlogCore.Core.PostFeature.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BlogCore.Core.PostFeature.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Post");
                });
        }
    }
}
