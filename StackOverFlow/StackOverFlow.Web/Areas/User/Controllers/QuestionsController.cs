using Autofac;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using StackOverFlow.Web.Areas.User.Models;
using System.Web;

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
                //more logic
            }
            model.TriedApproach = HttpUtility.HtmlDecode(model.TriedApproach);
            return View(model);
        }
    }
}
