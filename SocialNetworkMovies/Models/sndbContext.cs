using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SocialNetworkMovies.Models
{
    public partial class sndbContext : DbContext
    {
        public sndbContext()
        {
        }

        public sndbContext(DbContextOptions<sndbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<MovieList> MovieLists { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Professor> Professors { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserList> UserLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SNM_Database;Database=sndb;User Id=SA;Password=A&VeryComplex123Password;TrustServerCertificate=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastChanged).HasColumnType("datetime");

                entity.Property(e => e.HashUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HashURL");

                entity.Property(e => e.StrController)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.StrName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.StrState)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.ActionFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Actions__FkIdUse__403A8C7D");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.ActionFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Actions__FkIdUse__412EB0B6");
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
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

                entity.HasOne(d => d.FkIdUserNavigation)
                    .WithMany(p => p.AdministratorFkIdUserNavigations)
                    .HasForeignKey(d => d.FkIdUser)
                    .HasConstraintName("FK__Administr__FkIdU__4E88ABD4");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.AdministratorFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Administr__FkIdU__4F7CD00D");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.AdministratorFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Administr__FkIdU__5070F446");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
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

                entity.HasOne(d => d.FkIdCommentNavigation)
                    .WithMany(p => p.InverseFkIdCommentNavigation)
                    .HasForeignKey(d => d.FkIdComment)
                    .HasConstraintName("FK__Comment__FkIdCom__5DCAEF64");

                entity.HasOne(d => d.FkIdUserNavigation)
                    .WithMany(p => p.CommentFkIdUserNavigations)
                    .HasForeignKey(d => d.FkIdUser)
                    .HasConstraintName("FK__Comment__FkIdUse__5CD6CB2B");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.CommentFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Comment__FkIdUse__5EBF139D");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.CommentFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Comment__FkIdUse__5FB337D6");
            });

            modelBuilder.Entity<MovieList>(entity =>
            {
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

                entity.HasOne(d => d.FkIdListNavigation)
                    .WithMany(p => p.MovieLists)
                    .HasForeignKey(d => d.FkIdList)
                    .HasConstraintName("FK__MovieList__FkIdL__68487DD7");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.MovieListFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__MovieList__FkIdU__693CA210");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.MovieListFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__MovieList__FkIdU__6A30C649");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastChanged).HasColumnType("datetime");

                entity.Property(e => e.HashUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HashURL");

                entity.Property(e => e.StrState)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdActionNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.FkIdAction)
                    .HasConstraintName("FK__Permissio__FkIdA__44FF419A");

                entity.HasOne(d => d.FkIdRoleAllowedNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.FkIdRoleAllowed)
                    .HasConstraintName("FK__Permissio__FkIdR__440B1D61");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.PermissionFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Permissio__FkIdU__45F365D3");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.PermissionFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Permissio__FkIdU__46E78A0C");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
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

                entity.HasOne(d => d.FkIdUserNavigation)
                    .WithMany(p => p.ProfessorFkIdUserNavigations)
                    .HasForeignKey(d => d.FkIdUser)
                    .HasConstraintName("FK__Professor__FkIdU__534D60F1");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.ProfessorFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Professor__FkIdU__5441852A");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.ProfessorFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Professor__FkIdU__5535A963");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastChanged).HasColumnType("datetime");

                entity.Property(e => e.StrState)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdUserNavigation)
                    .WithMany(p => p.ReviewFkIdUserNavigations)
                    .HasForeignKey(d => d.FkIdUser)
                    .HasConstraintName("FK__Review__FkIdUser__49C3F6B7");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.ReviewFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Review__FkIdUser__4AB81AF0");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.ReviewFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Review__FkIdUser__4BAC3F29");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastChanged).HasColumnType("datetime");

                entity.Property(e => e.HashUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HashURL");

                entity.Property(e => e.StrName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StrState)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.RoleFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Roles__FkIdUserC__3B75D760");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.RoleFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Roles__FkIdUserL__3C69FB99");
            });

            modelBuilder.Entity<Student>(entity =>
            {
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

                entity.HasOne(d => d.FkIdUserNavigation)
                    .WithMany(p => p.StudentFkIdUserNavigations)
                    .HasForeignKey(d => d.FkIdUser)
                    .HasConstraintName("FK__Student__FkIdUse__5812160E");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.StudentFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Student__FkIdUse__59063A47");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.StudentFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Student__FkIdUse__59FA5E80");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastChanged).HasColumnType("datetime");

                entity.Property(e => e.HashUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HashURL");

                entity.Property(e => e.HsPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StrEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StrName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StrPhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StrRecoverMail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StrState)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FkIdRole)
                    .HasConstraintName("FK__Users__FkIdRole__3D5E1FD2");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.InverseFkIdUserCreatedNavigation)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__Users__FkIdUserC__37A5467C");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.InverseFkIdUserLastChangedNavigation)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__Users__FkIdUserL__38996AB5");
            });

            modelBuilder.Entity<UserList>(entity =>
            {
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

                entity.HasOne(d => d.FkIdMovieListNavigation)
                    .WithMany(p => p.UserLists)
                    .HasForeignKey(d => d.FkIdMovieList)
                    .HasConstraintName("FK__UserList__FkIdMo__6383C8BA");

                entity.HasOne(d => d.FkIdUserNavigation)
                    .WithMany(p => p.UserListFkIdUserNavigations)
                    .HasForeignKey(d => d.FkIdUser)
                    .HasConstraintName("FK__UserList__FkIdUs__628FA481");

                entity.HasOne(d => d.FkIdUserCreatedNavigation)
                    .WithMany(p => p.UserListFkIdUserCreatedNavigations)
                    .HasForeignKey(d => d.FkIdUserCreated)
                    .HasConstraintName("FK__UserList__FkIdUs__6477ECF3");

                entity.HasOne(d => d.FkIdUserLastChangedNavigation)
                    .WithMany(p => p.UserListFkIdUserLastChangedNavigations)
                    .HasForeignKey(d => d.FkIdUserLastChanged)
                    .HasConstraintName("FK__UserList__FkIdUs__656C112C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
