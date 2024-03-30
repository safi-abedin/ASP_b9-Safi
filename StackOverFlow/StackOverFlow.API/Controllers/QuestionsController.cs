using Autofac;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StackOverFlow.API.RequestHandlers;

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
        }
    }
}
