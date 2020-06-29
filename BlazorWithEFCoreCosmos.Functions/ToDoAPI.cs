using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BlazorWithEFCoreCosmos.Repository;
using System.Collections;

namespace BlazorWithEFCoreCosmos.Functions.API
{
    public class ToDoAPI
    {

        private readonly IToDoRepository _toDoRepository;

        public ToDoAPI(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        [FunctionName("GetTodoList")]
        public async Task<IActionResult> GetTodoList(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo")] HttpRequest req,
            ILogger log
        )
        {
            var todos = await _toDoRepository.GetAsync();
            return new OkObjectResult(JsonConvert.SerializeObject(todos));
        }


        [FunctionName("GetTodoDetail")]
        public async Task<IActionResult> GetTodoDetail(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/{id}")] HttpRequest req,
            ILogger log,
            Guid id
        )
        {
            log.LogDebug($"{id}");
            var todo = await _toDoRepository.GetAsync(id);
            if (todo == null)
            {
                return new NotFoundObjectResult("");
            }
            return new OkObjectResult(JsonConvert.SerializeObject(todo));
        }

        [FunctionName("UpdateTodo")]
        public async Task<IActionResult> UpdateTodo(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "todo/{id}")] HttpRequest req,
            ILogger log,
            Guid id
        )
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var todo = JsonConvert.DeserializeObject<Hashtable>(requestBody);
            todo["Id"] = id;
            var updatedTodo = await _toDoRepository.UpdateAsync(todo);
            return new OkObjectResult(JsonConvert.SerializeObject(updatedTodo));
        }
    }
}
