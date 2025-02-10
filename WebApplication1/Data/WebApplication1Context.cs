using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>().ToTable("AspNetUsers");
        }

        public DbSet<WebApplication1.Models.BookModel> BookModel { get; set; } = default!;
        public DbSet<WebApplication1.Models.AuthorModel> AuthorModel { get; set; }
        public DbSet<WebApplication1.Models.UserModel> UserModel { get; set; } = default!;
    }
}
