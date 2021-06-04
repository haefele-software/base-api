﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectName.Application.Persistence;

namespace ProjectName.Application.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectName.Application.Domain.Entities.AuditEntry", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("AuditEntryId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("AuditEvent")
                        .HasColumnType("TINYINT")
                        .HasColumnName("AuditEvent");

                    b.Property<byte>("State")
                        .HasColumnType("TINYINT")
                        .HasColumnName("DataState");

                    b.HasKey("Id");

                    b.ToTable("AuditEntry");
                });

            modelBuilder.Entity("ProjectName.Application.Domain.Entities.TodoItem", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TodoItemId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Priority")
                        .HasColumnType("tinyint");

                    b.Property<byte>("State")
                        .HasColumnType("TINYINT");

                    b.Property<long>("TodoListId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TodoListId");

                    b.ToTable("TodoItem");
                });

            modelBuilder.Entity("ProjectName.Application.Domain.Entities.TodoList", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TodoListId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("State")
                        .HasColumnType("TINYINT");

                    b.HasKey("Id");

                    b.ToTable("TodoList");
                });

            modelBuilder.Entity("ProjectName.Application.Domain.Entities.AuditEntry", b =>
                {
                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.CreatedDate", "AuditDate", b1 =>
                        {
                            b1.Property<long>("AuditEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("Value")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("AuditDate");

                            b1.HasKey("AuditEntryId");

                            b1.ToTable("AuditEntry");

                            b1.WithOwner()
                                .HasForeignKey("AuditEntryId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.EntityId", "AuditedEntityId", b1 =>
                        {
                            b1.Property<long>("AuditEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<long?>("Value")
                                .HasColumnType("BIGINT")
                                .HasColumnName("AuditedEntityId");

                            b1.HasKey("AuditEntryId");

                            b1.ToTable("AuditEntry");

                            b1.WithOwner()
                                .HasForeignKey("AuditEntryId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.EntityType", "AuditedEntityType", b1 =>
                        {
                            b1.Property<long>("AuditEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .HasColumnType("varchar(512)")
                                .HasColumnName("AuditedEntityType");

                            b1.HasKey("AuditEntryId");

                            b1.ToTable("AuditEntry");

                            b1.WithOwner()
                                .HasForeignKey("AuditEntryId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.Name", "InitiatedBy", b1 =>
                        {
                            b1.Property<long>("AuditEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .HasColumnType("varchar(256)")
                                .HasColumnName("InitiatedBy");

                            b1.HasKey("AuditEntryId");

                            b1.ToTable("AuditEntry");

                            b1.WithOwner()
                                .HasForeignKey("AuditEntryId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.ReferenceId", "ReferenceId", b1 =>
                        {
                            b1.Property<long>("AuditEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("Value")
                                .HasColumnType("UNIQUEIDENTIFIER")
                                .HasColumnName("ReferenceId");

                            b1.HasKey("AuditEntryId");

                            b1.ToTable("AuditEntry");

                            b1.WithOwner()
                                .HasForeignKey("AuditEntryId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.SerializedEntityState", "ObjectNewState", b1 =>
                        {
                            b1.Property<long>("AuditEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .HasColumnType("NVARCHAR(MAX)")
                                .HasColumnName("NewState");

                            b1.HasKey("AuditEntryId");

                            b1.ToTable("AuditEntry");

                            b1.WithOwner()
                                .HasForeignKey("AuditEntryId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.SerializedEntityState", "ObjectPreviousState", b1 =>
                        {
                            b1.Property<long>("AuditEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .HasColumnType("NVARCHAR(MAX)")
                                .HasColumnName("PreviousState");

                            b1.HasKey("AuditEntryId");

                            b1.ToTable("AuditEntry");

                            b1.WithOwner()
                                .HasForeignKey("AuditEntryId");
                        });

                    b.Navigation("AuditDate");

                    b.Navigation("AuditedEntityId");

                    b.Navigation("AuditedEntityType");

                    b.Navigation("InitiatedBy");

                    b.Navigation("ObjectNewState");

                    b.Navigation("ObjectPreviousState");

                    b.Navigation("ReferenceId");
                });

            modelBuilder.Entity("ProjectName.Application.Domain.Entities.TodoItem", b =>
                {
                    b.HasOne("ProjectName.Application.Domain.Entities.TodoList", "TodoList")
                        .WithMany("Items")
                        .HasForeignKey("TodoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.CreatedDate", "CreatedDate", b1 =>
                        {
                            b1.Property<long>("TodoItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("Value")
                                .HasColumnType("DATETIMEOFFSET")
                                .HasColumnName("CreateddDate");

                            b1.HasKey("TodoItemId");

                            b1.ToTable("TodoItem");

                            b1.WithOwner()
                                .HasForeignKey("TodoItemId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.Note", "Note", b1 =>
                        {
                            b1.Property<long>("TodoItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .HasColumnType("NVARCHAR(MAX)")
                                .HasColumnName("Note");

                            b1.HasKey("TodoItemId");

                            b1.ToTable("TodoItem");

                            b1.WithOwner()
                                .HasForeignKey("TodoItemId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.ReferenceId", "ReferenceId", b1 =>
                        {
                            b1.Property<long>("TodoItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("Value")
                                .HasColumnType("UNIQUEIDENTIFIER")
                                .HasColumnName("ReferenceId");

                            b1.HasKey("TodoItemId");

                            b1.ToTable("TodoItem");

                            b1.WithOwner()
                                .HasForeignKey("TodoItemId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.ReminderDate", "ReminderDate", b1 =>
                        {
                            b1.Property<long>("TodoItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset?>("Value")
                                .HasColumnType("DATETIMEOFFSET")
                                .HasColumnName("ReminderDate");

                            b1.HasKey("TodoItemId");

                            b1.ToTable("TodoItem");

                            b1.WithOwner()
                                .HasForeignKey("TodoItemId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.Title", "Title", b1 =>
                        {
                            b1.Property<long>("TodoItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("NVARCHAR(MAX)")
                                .HasColumnName("Title");

                            b1.HasKey("TodoItemId");

                            b1.ToTable("TodoItem");

                            b1.WithOwner()
                                .HasForeignKey("TodoItemId");
                        });

                    b.Navigation("CreatedDate");

                    b.Navigation("Note");

                    b.Navigation("ReferenceId");

                    b.Navigation("ReminderDate");

                    b.Navigation("Title");

                    b.Navigation("TodoList");
                });

            modelBuilder.Entity("ProjectName.Application.Domain.Entities.TodoList", b =>
                {
                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.ReferenceId", "ReferenceId", b1 =>
                        {
                            b1.Property<long>("TodoListId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("Value")
                                .HasColumnType("UNIQUEIDENTIFIER")
                                .HasColumnName("ReferenceId");

                            b1.HasKey("TodoListId");

                            b1.ToTable("TodoList");

                            b1.WithOwner()
                                .HasForeignKey("TodoListId");
                        });

                    b.OwnsOne("ProjectName.Application.Domain.ValueObjects.Title", "Title", b1 =>
                        {
                            b1.Property<long>("TodoListId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("NVARCHAR(MAX)")
                                .HasColumnName("Title");

                            b1.HasKey("TodoListId");

                            b1.ToTable("TodoList");

                            b1.WithOwner()
                                .HasForeignKey("TodoListId");
                        });

                    b.Navigation("ReferenceId");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("ProjectName.Application.Domain.Entities.TodoList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
