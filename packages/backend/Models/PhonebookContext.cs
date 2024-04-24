using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class PhonebookContext : DbContext
{
    public PhonebookContext()
    {
    }

    public PhonebookContext(DbContextOptions<PhonebookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactType> ContactTypes { get; set; }

    public virtual DbSet<PersonContact> PersonContacts { get; set; }

    public virtual DbSet<PrivateOrganizationContact> PrivateOrganizationContacts { get; set; }

    public virtual DbSet<PublicOrganizationContact> PublicOrganizationContacts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contact_pkey");

            entity.ToTable("contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactTypeId).HasColumnName("contact_type_id");
            entity.Property(e => e.Deleted)
                .HasDefaultValue(false)
                .HasColumnName("deleted");
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

            entity.HasIndex(e => e.ContactId, "person_contact_contact_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Relationship)
                .HasMaxLength(100)
                .HasColumnName("relationship");

            entity.HasOne(d => d.Contact).WithOne(p => p.PersonContact)
                .HasForeignKey<PersonContact>(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_contact_contact_id_fkey");
        });

        modelBuilder.Entity<PrivateOrganizationContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("private_organization_contact_pkey");

            entity.ToTable("private_organization_contact");

            entity.HasIndex(e => e.ContactId, "private_organization_contact_contact_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasColumnName("fax");
            entity.Property(e => e.OfficeAddress)
                .HasMaxLength(255)
                .HasColumnName("office_address");

            entity.HasOne(d => d.Contact).WithOne(p => p.PrivateOrganizationContact)
                .HasForeignKey<PrivateOrganizationContact>(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("private_organization_contact_contact_id_fkey");
        });

        modelBuilder.Entity<PublicOrganizationContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("public_organization_contact_pkey");

            entity.ToTable("public_organization_contact");

            entity.HasIndex(e => e.ContactId, "public_organization_contact_contact_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.IndustrialSector)
                .HasMaxLength(100)
                .HasColumnName("industrial_sector");
            entity.Property(e => e.WebpageUrl)
                .HasMaxLength(255)
                .HasColumnName("webpage_url");

            entity.HasOne(d => d.Contact).WithOne(p => p.PublicOrganizationContact)
                .HasForeignKey<PublicOrganizationContact>(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("public_organization_contact_contact_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
