﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class PhonebookContext : DbContext
{
    public PhonebookContext(DbContextOptions<PhonebookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactType> ContactTypes { get; set; }

    public virtual DbSet<PersonContact> PersonContacts { get; set; }

    public virtual DbSet<PrivateOrganizationContact> PrivateOrganizationContacts { get; set; }

    public virtual DbSet<PublicOrganizationContact> PublicOrganizationContacts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contact_pkey");

            entity.ToTable("contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactTypeId).HasColumnName("contact_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.TextComments).HasColumnName("text_comments");

            entity.HasOne(d => d.ContactType).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_contact_type_id_fkey");
        });

        modelBuilder.Entity<ContactType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contact_type_pkey");

            entity.ToTable("contact_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PersonContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_contact_pkey");

            entity.ToTable("person_contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Relationship)
                .HasMaxLength(100)
                .HasColumnName("relationship");

            entity.HasOne(d => d.Contact).WithMany(p => p.PersonContacts)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_contact_contact_id_fkey");
        });

        modelBuilder.Entity<PrivateOrganizationContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("private_organization_contact_pkey");

            entity.ToTable("private_organization_contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasColumnName("fax");
            entity.Property(e => e.OfficeAddress)
                .HasMaxLength(255)
                .HasColumnName("office_address");

            entity.HasOne(d => d.Contact).WithMany(p => p.PrivateOrganizationContacts)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("private_organization_contact_contact_id_fkey");
        });

        modelBuilder.Entity<PublicOrganizationContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("public_organization_contact_pkey");

            entity.ToTable("public_organization_contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.IndustrialSector)
                .HasMaxLength(100)
                .HasColumnName("industrial_sector");
            entity.Property(e => e.WebpageUrl)
                .HasMaxLength(255)
                .HasColumnName("webpage_url");

            entity.HasOne(d => d.Contact).WithMany(p => p.PublicOrganizationContacts)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("public_organization_contact_contact_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}