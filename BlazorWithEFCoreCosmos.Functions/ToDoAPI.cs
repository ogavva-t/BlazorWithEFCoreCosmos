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
    }
}
