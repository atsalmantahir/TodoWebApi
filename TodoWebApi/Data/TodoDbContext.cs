using Microsoft.EntityFrameworkCore;

namespace TodoWebApi.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoWebApi.Models.TodoItem> TodoItem { get; set; }

    }
}
