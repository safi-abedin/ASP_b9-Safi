using Microsoft.AspNetCore.Mvc;

namespace StackOverFlow.Web.Areas.User.Controllers
{
    [Area("user")]
    public class QuestionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
