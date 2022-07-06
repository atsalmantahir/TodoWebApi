using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models;

namespace TodoWebApi.Data
{
    public class TodoDbContext : IdentityDbContext<ApplicationUser>
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoWebApi.Models.TodoItem> TodoItem { get; set; }

    }
}
