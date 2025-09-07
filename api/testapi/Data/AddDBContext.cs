using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using testapi.Models;

namespace testapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Data { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskTags>()
                .HasKey(tt => new { tt.TaskID, tt.TagsID });  // Composite PK

            modelBuilder.Entity<TaskTags>()
                .HasOne(tt => tt.TaskItem)
                .WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TaskID);

            modelBuilder.Entity<TaskTags>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TagsID);


            modelBuilder.Entity<TaskItem>()
            .HasKey(t => t.ProdataID);

            modelBuilder.Entity<Tag>()
               .HasKey(t => t.ProdataTagsID);

           
        }
    }
}