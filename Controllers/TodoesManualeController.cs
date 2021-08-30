using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestStorage.Models;

namespace TestStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesManualeController : ControllerBase
    {
        private readonly AppDataContext _context;

        public TodoesManualeController(AppDataContext appDataContext)
        {
            _context = appDataContext;
        }

        [HttpGet]
        public List<Todo> GetTodos()
        {
            return _context.Todos.ToList();
        }

        [HttpGet("{id}")]
        public Todo GetTodo(int id)
        {
            var todo = _context.Todos.Find(id);
            
            return todo;
        }

        [HttpPost]
        public Todo PostTodo(Todo todo)
        {             
            while (_context.Todos.Find(todo.Id) != null || todo.Id==0)
            {
                todo.Id++;
            }
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return todo;
        }

        [HttpPut("{id}")]
        public Todo UpdateTodo(int id, Todo todoUpd)
        {
            
            var todo = _context.Todos.Find(id);
            todo.Name = todoUpd.Name;
            todo.IsComplete = todoUpd.IsComplete;
            todo.Notes = todoUpd.Notes;
            _context.SaveChanges();
            return todo;
        }

        [HttpDelete("{id}")]
        public Todo DeleteTodo(int id)
        {
            var todo = _context.Todos.Find(id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return todo;
        }
    }
}
