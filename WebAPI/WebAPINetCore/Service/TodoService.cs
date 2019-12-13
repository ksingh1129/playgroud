using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPINetCore.Model;

namespace WebAPINetCore.Service
{
    public class TodoService
    {
        public List<TodoItem> GetAllTodoItem()
        {
            using(StreamReader read = new StreamReader("Source/resource.json"))
            {
                string json = read.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TodoItem>>(json);
            }            
        }

        public TodoItem GetTodoItem(int id)
        {
            using (StreamReader read = new StreamReader("Source/resource.json"))
            {
                string json = read.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                return items.Where(m => m.id == id).FirstOrDefault();
            }
        }

        public IActionResult CreateTodoItem(TodoItem todoItem)
        {
            var jsonFileService = new JsonFileService();
            string filePath = "Source/resource.json";
            var todoItemJsonString = jsonFileService.CreateTodoItemJsonFile(todoItem, filePath);

            if (!string.IsNullOrEmpty(todoItemJsonString))
            {
                File.WriteAllText(filePath, todoItemJsonString);
                return new OkObjectResult("Create Success");
            }

            return new BadRequestObjectResult("Can't create new TodoItem");
        }

        public IActionResult UpdateTodoItem(TodoItem todoItem)
        {
            var jsonFileService = new JsonFileService();
            string filePath = "Source/resource.json";
            var todoItemJsonString = jsonFileService.UpdateTodoItemJsonFile(todoItem, filePath);

            if(!string.IsNullOrEmpty(todoItemJsonString))
            {
                File.WriteAllText(filePath, todoItemJsonString);
                return new OkObjectResult("Update Success");
            }

            return new NotFoundObjectResult("TodoItem is not found.");

        }

    }
}
