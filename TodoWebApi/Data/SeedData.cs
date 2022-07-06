using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models;

namespace TodoWebApi.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TodoDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TodoDbContext>>())) 
            {
                if (context.TodoItem.Any()) 
                {
                    return;
                }
                
                var todoItems = new List<TodoItem> 
                {
                    new TodoItem
                    {
                        Id = Guid.NewGuid(),
                        UserId = "NotAUserId",
                        Title = "Task 1",
                        Description = "Description for task 1",
                        CreatedDate = DateTime.Now,
                        Status = false,

                    }, new TodoItem
                    {
                        Id = Guid.NewGuid(),
                        UserId = "NotAUserId",
                        Title = "Task 2",
                        Description = "Description for task 2",
                        CreatedDate = DateTime.Now,
                        Status = false,

                    }, new TodoItem
                    {
                        Id = Guid.NewGuid(),
                        UserId = "NotAUserId",
                        Title = "Task 3",
                        Description = "Description for task 3",
                        CreatedDate = DateTime.Now,
                        Status = false,

                    }
                };

                context.TodoItem.AddRange(todoItems) ;

                context.SaveChanges();
                Console.WriteLine("TodoItem data added!");
            }
        }
    }
}
