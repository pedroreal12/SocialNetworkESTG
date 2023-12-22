using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class User
    {
        public User()
        {
            ActionFkIdUserCreatedNavigations = new HashSet<Action>();
            ActionFkIdUserLastChangedNavigations = new HashSet<Action>();
            AdministratorFkIdUserCreatedNavigations = new HashSet<Administrator>();
            AdministratorFkIdUserLastChangedNavigations = new HashSet<Administrator>();
            AdministratorFkIdUserNavigations = new HashSet<Administrator>();
            CommentFkIdUserCreatedNavigations = new HashSet<Comment>();
            CommentFkIdUserLastChangedNavigations = new HashSet<Comment>();
            CommentFkIdUserNavigations = new HashSet<Comment>();
            InverseFkIdUserCreatedNavigation = new HashSet<User>();
            InverseFkIdUserLastChangedNavigation = new HashSet<User>();
            MovieListFkIdUserCreatedNavigations = new HashSet<MovieList>();
            MovieListFkIdUserLastChangedNavigations = new HashSet<MovieList>();
            PermissionFkIdUserCreatedNavigations = new HashSet<Permission>();
            PermissionFkIdUserLastChangedNavigations = new HashSet<Permission>();
            ProfessorFkIdUserCreatedNavigations = new HashSet<Professor>();
            ProfessorFkIdUserLastChangedNavigations = new HashSet<Professor>();
            ProfessorFkIdUserNavigations = new HashSet<Professor>();
            ReviewFkIdUserCreatedNavigations = new HashSet<Review>();
            ReviewFkIdUserLastChangedNavigations = new HashSet<Review>();
            ReviewFkIdUserNavigations = new HashSet<Review>();
            RoleFkIdUserCreatedNavigations = new HashSet<Role>();
            RoleFkIdUserLastChangedNavigations = new HashSet<Role>();
            StudentFkIdUserCreatedNavigations = new HashSet<Student>();
            StudentFkIdUserLastChangedNavigations = new HashSet<Student>();
            StudentFkIdUserNavigations = new HashSet<Student>();
            UserListFkIdUserCreatedNavigations = new HashSet<UserList>();
            UserListFkIdUserLastChangedNavigations = new HashSet<UserList>();
            UserListFkIdUserNavigations = new HashSet<UserList>();
        }

        public int Id { get; set; }
        public string StrName { get; set; } = null!;
        public string StrEmail { get; set; } = null!;
        public string HsPassword { get; set; } = null!;
        public string StrRecoverMail { get; set; } = null!;
        public string StrPhoneNumber { get; set; } = null!;
        public string StrState { get; set; } = null!;
        public int? FkIdUserCreated { get; set; }
        public int? FkIdUserLastChanged { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastChanged { get; set; }
        public string? HashUrl { get; set; }
        public int? FkIdRole { get; set; }

        public virtual Role? FkIdRoleNavigation { get; set; }
        public virtual User? FkIdUserCreatedNavigation { get; set; }
        public virtual User? FkIdUserLastChangedNavigation { get; set; }
        public virtual ICollection<Action> ActionFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Action> ActionFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Administrator> AdministratorFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Administrator> AdministratorFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Administrator> AdministratorFkIdUserNavigations { get; set; }
        public virtual ICollection<Comment> CommentFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Comment> CommentFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Comment> CommentFkIdUserNavigations { get; set; }
        public virtual ICollection<User> InverseFkIdUserCreatedNavigation { get; set; }
        public virtual ICollection<User> InverseFkIdUserLastChangedNavigation { get; set; }
        public virtual ICollection<MovieList> MovieListFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<MovieList> MovieListFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Permission> PermissionFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Permission> PermissionFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Professor> ProfessorFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Professor> ProfessorFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Professor> ProfessorFkIdUserNavigations { get; set; }
        public virtual ICollection<Review> ReviewFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Review> ReviewFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Review> ReviewFkIdUserNavigations { get; set; }
        public virtual ICollection<Role> RoleFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Role> RoleFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Student> StudentFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Student> StudentFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<Student> StudentFkIdUserNavigations { get; set; }
        public virtual ICollection<UserList> UserListFkIdUserCreatedNavigations { get; set; }
        public virtual ICollection<UserList> UserListFkIdUserLastChangedNavigations { get; set; }
        public virtual ICollection<UserList> UserListFkIdUserNavigations { get; set; }
    }
}
