using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebAPINetCore.Model;
using Newtonsoft.Json;

namespace WebAPINetCore.Service
{
    public class JsonFileService
    {
        public string UpdateTodoItemJsonFile(TodoItem todoItem, string filePath)
        {
            string todoItemAsJson = string.Empty;

            using (StreamReader read = new StreamReader(filePath))
            {
                string json = read.ReadToEnd();
                var items = ConvertTodoItemJsonToObject(json);
                var updatedItem = items.FirstOrDefault(m => m.id == todoItem.id);

                if (updatedItem != null)
                {
                    items.Remove(updatedItem);
                    items.Add(todoItem);
                    items = items.OrderBy(m => m.id).ToList();

                    todoItemAsJson = ConvertTodoItemObjectToJson(items);
                }
            }

            return todoItemAsJson;
        }

        public string CreateTodoItemJsonFile(TodoItem todoItem, string filePath)
        {
            string todoItemAsJson = string.Empty;

            using (StreamReader read = new StreamReader(filePath))
            {
                string json = read.ReadToEnd();
                var items = ConvertTodoItemJsonToObject(json);
                var existsItem = items.FirstOrDefault(m => m.id == todoItem.id);

                if(existsItem == null)
                {
                    items.Add(todoItem);
                    items = items.OrderBy(m => m.id).ToList();

                    todoItemAsJson = ConvertTodoItemObjectToJson(items);
                }
            }

            return todoItemAsJson;
        }
        private List<TodoItem> ConvertTodoItemJsonToObject(string json)
        {
            return JsonConvert.DeserializeObject<List<TodoItem>>(json);
        }

        private string ConvertTodoItemObjectToJson(List<TodoItem> todoItems)
        {
            return JsonConvert.SerializeObject(todoItems);
        }
    }
}
