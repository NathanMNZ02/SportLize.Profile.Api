using System;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SportLize.Profile.Api.Profile.Repository.Model;

namespace SportLize.Profile.Api.Profile.Repository
{
	public class ProfileDbContext : DbContext
	{
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<TransactionalOutbox>().ToTable("TransactionalOutbox");         

            modelBuilder.Entity<User>().HasKey(s => s.Id);
            modelBuilder.Entity<Post>().HasKey(s => s.Id );
            modelBuilder.Entity<Comment>().HasKey(s => s.Id);
            modelBuilder.Entity<TransactionalOutbox>().HasKey(s=> s.Id);

            modelBuilder.Entity<User>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Comment>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TransactionalOutbox>().Property(s => s.Id).ValueGeneratedOnAdd();

            //Relation one to many
            modelBuilder.Entity<User>().HasMany(s => s.Posts).WithOne(s => s.User).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>().HasMany(S => S.Comments).WithOne(s => s.Post).HasForeignKey(s => s.PostId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }
		public DbSet<Post> Post { get; set; }
		public DbSet<Comment> Comment { get; set; }

        /*TRANSACTIONAL*/
        public DbSet<TransactionalOutbox> TransactionalOutboxes { get; set; }
	}
}

