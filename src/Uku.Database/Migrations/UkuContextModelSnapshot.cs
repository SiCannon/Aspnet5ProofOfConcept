using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Uku.Database.Persist;

namespace Uku.Database.Migrations
{
    [DbContext(typeof(UkuContext))]
    partial class UkuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Uku.Database.Model.Album", b =>
                {
                    b.Property<int?>("AlbumId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<string>("Title");

                    b.Property<int?>("Top3kPosition");

                    b.Property<int?>("Year");

                    b.HasKey("AlbumId");
                });
        }
    }
}
