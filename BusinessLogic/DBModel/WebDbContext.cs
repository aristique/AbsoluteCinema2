using System;
using System.Data.Entity;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA
{
    public class WebDbContext : DbContext
    {
        // Конструктор: будет искать в Web.config строку с name="WebDbContext"
        public WebDbContext() : base("name=WebDbContext")
        {
            // Включаем логирование SQL-запросов в Output → Debug (Visual Studio)
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 1) Указываем, что все таблицы по умолчанию лежат в схеме "dbo"
            modelBuilder.HasDefaultSchema("dbo");

            // 2) Настраиваем составные ключи
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieDirector>()
                .HasKey(md => new { md.MovieId, md.DirectorId });

            // 3) Привязываем связи для комментариев
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.MovieId).HasName("IX_Comments_MovieId");
            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.UserId).HasName("IX_Comments_UserId");

            // 4) Привязываем подписки к пользователям
            modelBuilder.Entity<Subscription>()
                .HasRequired(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
