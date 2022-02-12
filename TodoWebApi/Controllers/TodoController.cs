using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Data;

namespace TodoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public TodoDbContext dbContext;
        public TodoController(TodoDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetTodoItems() 
        {
            var todoItems = this.dbContext.TodoItem.ToList();
            return Ok(todoItems);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTodoItem(string id)
        {
            var todoItems = this.dbContext.TodoItem.Where(x => x.Id.Equals(id)).FirstOrDefault();
            return Ok(todoItems);
        }

        [HttpPost]
        public IActionResult PostTodoItem(TodoWebApi.Models.ViewModels.CreateTodoItem todoItem)
        {
            var addTodoItem = this.dbContext.TodoItem.Add(new Models.TodoItem 
            {
                Title = todoItem.Title,
                Description = todoItem.Description,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                Status = false,
            });
            var result = this.dbContext.SaveChanges();
            return Ok(result);
        }
    }
}
