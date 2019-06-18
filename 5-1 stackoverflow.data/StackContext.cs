using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_1_stackoverflow.data
{
    public class StackContext : DbContext
    {
        private readonly string _connString;

        public StackContext(string connString)
        {
            _connString = connString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTags> QuestionTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<QuestionTags>().HasKey(qt => new { qt.QuestionId, qt.TagId });


            modelBuilder.Entity<QuestionTags>()
                     .HasOne(qt => qt.Question)
                     .WithMany(q => q.QuestionTags)
                     .HasForeignKey(qt => qt.QuestionId);

            modelBuilder.Entity<QuestionTags>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionTags)
                .HasForeignKey(qt => qt.TagId);

            modelBuilder.Entity<Likes>().HasKey(l => new { l.QuestionId, l.UserId });

            modelBuilder.Entity<Likes>()
                            .HasOne(l => l.Question)
                            .WithMany(q => q.Likes)
                            .HasForeignKey(l => l.QuestionId);

            modelBuilder.Entity<Likes>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId);
        }
    }
}
