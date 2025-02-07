using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<DemoBookStore.Models.BookModel> BookModel { get; set; } = default!;
        public DbSet<DemoBookStore.Models.AuthorModel> AuthorModel { get; set; }
        public DbSet<DemoBookStore.Models.UserModel> UserModel { get; set; } = default!;
    }
}
