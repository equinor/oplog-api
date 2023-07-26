﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oplog.Persistence;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    [DbContext(typeof(OplogDbContext))]
    partial class OplogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Oplog.Persistence.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastChangeTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.ConfiguredType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("DefaultUomTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndLife")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDuplicate")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastChangeTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastChangeUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartLife")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UomTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ConfiguredTypes");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.CustomFilter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsGlobalFilter")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomFilters");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.CustomFilterItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CustomFilterId")
                        .HasColumnType("int");

                    b.Property<int>("FilterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomFilterId");

                    b.ToTable("CustomFilterItems");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EffectiveTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsCritical")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastChangeDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastChangeUserId")
                        .HasColumnType("int");

                    b.Property<int?>("LogTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("OperationAreaId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduleItemState")
                        .HasColumnType("int");

                    b.Property<int?>("Subtype")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Unit")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedDate");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.LogTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsCritical")
                        .HasColumnType("bit");

                    b.Property<int?>("LogTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OperationAreaId")
                        .HasColumnType("int");

                    b.Property<int?>("Subtype")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LogTemplates");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.LogsView", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("AreaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EffectiveTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsCritical")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastChangeDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastChangeUserId")
                        .HasColumnType("int");

                    b.Property<int?>("LogTypeId")
                        .HasColumnType("int");

                    b.Property<string>("LogTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OperationAreaId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduleItemState")
                        .HasColumnType("int");

                    b.Property<string>("SubTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Subtype")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Unit")
                        .HasColumnType("int");

                    b.Property<string>("UnitName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("LogsView", (string)null);
                });

            modelBuilder.Entity("Oplog.Persistence.Models.OperationArea", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperationAreas");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.CustomFilterItem", b =>
                {
                    b.HasOne("Oplog.Persistence.Models.CustomFilter", "CustomFilter")
                        .WithMany("CustomFilterItems")
                        .HasForeignKey("CustomFilterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomFilter");
                });

            modelBuilder.Entity("Oplog.Persistence.Models.CustomFilter", b =>
                {
                    b.Navigation("CustomFilterItems");
                });
#pragma warning restore 612, 618
        }
    }
}
