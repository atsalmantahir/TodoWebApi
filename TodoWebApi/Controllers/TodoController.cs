using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Fetch the list of todoitem.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetTodoItems() 
        {
            var todoItems = this.dbContext.TodoItem.ToList().OrderByDescending(x => x.CreatedDate);
            return Ok(todoItems);
        }

        /// <summary>
        /// Fetch a specific todoitem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTodoItem(Guid id)
        {
            var todoItems = this.dbContext.TodoItem.Where(x => x.Id.Equals(id)).FirstOrDefault();
            return Ok(todoItems);
        }

        /// <summary>
        /// Create a todoitem.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Update a specific todoitem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTodoItem(Guid id,TodoWebApi.Models.ViewModels.CreateTodoItem todoItem)
        {
            var todoItemToUpdate = this.dbContext.TodoItem.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (todoItemToUpdate == null) 
            {
                return NoContent();
            }
            todoItemToUpdate.LastUpdatedDate = DateTime.UtcNow;
            todoItemToUpdate.Title = todoItem.Title;
            todoItemToUpdate.Description = todoItem.Description;

            var updateTodoItem = this.dbContext.TodoItem.Update(todoItemToUpdate);
            var result = this.dbContext.SaveChanges();

            return Ok(result);
        }

        /// <summary>
        /// Deletes a specific todoitem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTodoItem(Guid id)
        {
            var todoItemToDelete = this.dbContext.TodoItem.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (todoItemToDelete == null)
            {
                return NoContent();
            }

            var updateTodoItem = this.dbContext.TodoItem.Remove(todoItemToDelete);
            var result = this.dbContext.SaveChanges();

            return Ok(result);
        }
    }
}
