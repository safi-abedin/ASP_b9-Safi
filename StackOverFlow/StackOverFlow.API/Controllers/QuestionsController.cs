using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StackOverFlow.API.RequestHandlers;
using StackOverFlow.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StackOverFlow.API.Controllers
{
    [Route("api/v3/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowSites")]

    public class QuestionsController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(ILogger<QuestionsController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }



        [HttpPost()]
        public async Task<object> Post([FromBody] QuestionsRequestHandlers handler)
        {
            handler.ResolveDependency(_scope);

            var data = await handler.GetPagedCourses();
            return data;
        }

        [HttpGet]
        public async Task<IEnumerable<Question>> Get()
        {
            try
            {
                var model = _scope.Resolve<QuestionsRequestHandlers>();
                var data = await model?.GetQuestionsAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't get courses");
                return null;
            }
        }

            /*  [HttpPost()]
              public async Task<IActionResult> Post([FromBody] QuestionsRequestHandlers model)
              {
                  try
                  {
                      model.ResolveDependency(_scope);
                      model.CreateQuestionAsync();

                      return Ok();
                  }
                  catch (Exception ex)
                  {
                      _logger.LogError(ex, "Couldn't delete course");
                      return BadRequest();
                  }
              }*/
        }
}
