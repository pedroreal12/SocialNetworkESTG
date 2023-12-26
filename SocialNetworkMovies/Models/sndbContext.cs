using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SocialNetworkMovies.Models;

namespace SocialNetworkMovies.Models;

public partial class SndbContext : DbContext
{
    public SndbContext()
    {
    }

    public SndbContext(DbContextOptions<SndbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Discussion> Discussions { get; set; }

    public virtual DbSet<MovieList> MovieLists { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=sndb;User Id=SA;Password=A&VeryComplex123Password;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Administ__3214EC07777210A3");

            entity.ToTable("Administrator");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.HashUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HashURL");
            entity.Property(e => e.StrMail)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC07263B9E24");

            entity.ToTable("Comment");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.HashUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HashURL");
            entity.Property(e => e.StrName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TextComment)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.FkIdCommentNavigation).WithMany(p => p.InverseFkIdCommentNavigation)
                .HasForeignKey(d => d.FkIdComment)
                .HasConstraintName("FK__Comment__FkIdCom__412EB0B6");

            entity.HasOne(d => d.FkIdDiscussionNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FkIdDiscussion)
                .HasConstraintName("FK__Comment__FkIdDis__4222D4EF");
        });

        modelBuilder.Entity<Discussion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discussi__3214EC079A4FC8E9");

            entity.ToTable("Discussion");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.HashUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HashURL");
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StrText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("strText");
        });

        modelBuilder.Entity<MovieList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieLis__3214EC07F35D171A");

            entity.ToTable("MovieList");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.HashUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HashURL");
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.FkIdListNavigation).WithMany(p => p.MovieLists)
                .HasForeignKey(d => d.FkIdList)
                .HasConstraintName("FK__MovieList__FkIdL__47DBAE45");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Professo__3214EC07154E7EC5");

            entity.ToTable("Professor");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.HashUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HashURL");
            entity.Property(e => e.StrMail)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3214EC077ECFEBC5");

            entity.ToTable("Review");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC07ED3C5FA4");

            entity.ToTable("Student");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.HashUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HashURL");
            entity.Property(e => e.StrMail)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserList__3214EC077CE86052");

            entity.ToTable("UserList");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateLastChanged).HasColumnType("datetime");
            entity.Property(e => e.HashUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HashURL");
            entity.Property(e => e.StrName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.StrState)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.FkIdMovieListNavigation).WithMany(p => p.UserLists)
                .HasForeignKey(d => d.FkIdMovieList)
                .HasConstraintName("FK__UserList__FkIdMo__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
