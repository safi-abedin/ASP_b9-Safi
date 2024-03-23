using Autofac;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using StackOverFlow.Web.Areas.User.Models;

namespace StackOverFlow.Web.Areas.User.Controllers
{
    [Area("user")]
    public class QuestionsController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(ILifetimeScope scope,
            ILogger<QuestionsController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<QuestionCreateModel>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {

            if (ModelState.IsValid)
            {
                
                    model.Resolve(_scope);
                    //await model.CreateCourseAsync();

                    /*TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Course created successfuly",
                        Type = ResponseTypes.Success
                    });*/

                    return RedirectToAction("Index");
                
               /* catch (DuplicateTitleException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating course",
                        Type = ResponseTypes.Danger
                    });
                }*/
            }
            return View(model);
        }
    }
}
