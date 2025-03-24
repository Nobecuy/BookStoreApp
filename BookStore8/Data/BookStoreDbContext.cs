using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookStore8.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder
    //        .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07FA0D9AAF");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC072B73B4D1");

            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        //  Guid statis untuk role

        //  Guid statis untuk pengguna

        //modelBuilder.Entity<IdentityRole>().HasData(
        //    new IdentityRole
        //        {
        //            Name = "User",
        //            NormalizedName = "USER",
        //            Id = "2e891c19-3594-4362-82b1-e8b7c750173f"
        //    },
        //     new IdentityRole
        //        {
        //            Name = "Administrator",
        //            NormalizedName = "ADMINISTRATOR",
        //            Id = "560dbc73-4df3-4494-9d1f-4d2e44cb31b9" 
        //     }
        //    );

        //var hasher = new PasswordHasher<ApiUser>();

        //modelBuilder.Entity<ApiUser>().HasData(
        //    new ApiUser
        //    {
        //        Id = "0a56a833-809f-441d-bbfd-7efb819d0671",
        //        Email = "admin@booksore.com",
        //        NormalizedEmail = "ADMIN@BOOKSTORE.COM",
        //        UserName = "admin@booksore.com",
        //        NormalizedUserName = "ADMIN@BOOKSTORE.COM",
        //        FirstName = "System",
        //        LastName = "Admin",
        //        PasswordHash = hasher.HashPassword(null,"Admin123#"),
        //    },
        //    new ApiUser
        //    {
        //        Id = "0f51be92-235f-4c90-857b-d90e07eaae9b",
        //        Email = "user@booksore.com",
        //        NormalizedEmail = "USER@BOOKSTORE.COM",
        //        UserName = "user@booksore.com",
        //        NormalizedUserName = "USER@BOOKSTORE.COM",
        //        FirstName = "System",
        //        LastName = "User",
        //        PasswordHash = hasher.HashPassword(null, "User123#"),
        //    }
        //);

        //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        //    new IdentityUserRole<string> //user
        //    {
        //        RoleId = "2e891c19-3594-4362-82b1-e8b7c750173f",                
        //        UserId = "0f51be92-235f-4c90-857b-d90e07eaae9b"
        //    },
        //    new IdentityUserRole<string> //admin

        //    {
        //        RoleId = "560dbc73-4df3-4494-9d1f-4d2e44cb31b9",
        //        UserId = "0a56a833-809f-441d-bbfd-7efb819d0671"
        //    }
            
        //);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
