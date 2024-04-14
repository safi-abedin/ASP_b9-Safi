using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverFlow.Infrastructure;
using StackOverFlow.Infrastructure.Membership;
using StackOverFlow.Web.Areas.User.Models;
using static System.Formats.Asn1.AsnWriter;

namespace StackOverFlow.Web.Areas.User.Controllers
{
    [Area("user")]
    public class ProfileController : Controller
    {


        private readonly ILifetimeScope _scope;
        private readonly ILogger<ProfileController> _logger;
        private UserManager<ApplicationUser> _userManager;


        public ProfileController(ILifetimeScope scope,
            ILogger<ProfileController> logger, UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetQuestions(QuestionAskedListModel model)
        {
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);

            var user = await _userManager.GetUserAsync(User);

            if (user is not  null) {
                model.UserId = user.Id;
            }

            model.Resolve(_scope);

            var data = await model.GetPagedQuestionsAsync(dataTableModel);

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var model = _scope.Resolve<ProfileEditModel>();

            var user = await _userManager.GetUserAsync(User);

            if (user is not null)
            {
                model.UserId = user.Id;
                model.AboutMe = user.AboutMe;
                model.DisplayName = user.DisplayName;
                model.Location = user.Location;
            }

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileEditModel model)
        {
            var user = await _userManager.GetUserAsync(User);


            if (user is not null)
            {
                user.DisplayName = model.DisplayName;
                user.Location = model.Location;
                user.AboutMe = model.AboutMe;


                var result = await _userManager.UpdateAsync(user);


                if (result.Succeeded) return RedirectToAction("Index");
            }


            return View(model);
        }
    }
}
