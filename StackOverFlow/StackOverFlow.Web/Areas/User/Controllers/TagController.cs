using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverFlow.Infrastructure;
using StackOverFlow.Infrastructure.Membership;
using StackOverFlow.Web.Areas.User.Models;

namespace StackOverFlow.Web.Areas.User.Controllers
{
    [Area("user")]
    public class TagController : Controller
    {

        private readonly ILifetimeScope _scope;
        private readonly ILogger<TagController> _logger;
        private UserManager<ApplicationUser> _userManager;



        public TagController(ILifetimeScope scope,
            ILogger<TagController> logger, UserManager<ApplicationUser> userManager)
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
        public async Task<JsonResult> GetTags(TagListModel model)
        {
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);

            model.Resolve(_scope);

            var data = await model.GetPagedTagsAsync(dataTableModel);

            return Json(data);
        }


        [HttpGet]
        public async Task<IActionResult> TaggedQuestions(Guid id)
        {
            var model = _scope.Resolve<TagQuestionModel>();

            var user = await _userManager.GetUserAsync(User);


            if(user is not null)
            {
                model.DisplayName = user.DisplayName;
                var response = await model.GetPhotoAsync(user.ProfilePictureUrl);
                model.ImageURL = response;
            }

            if(ModelState.IsValid)
            {
                 await model.GetTagedQuestonsAsync(id);
            }

            return View(model);
        }
    }
}
