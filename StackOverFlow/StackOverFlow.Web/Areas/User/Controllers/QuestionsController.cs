using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure;
using StackOverFlow.Infrastructure.Membership;
using StackOverFlow.Web.Areas.Admin.Models;
using StackOverFlow.Web.Areas.User.Models;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace StackOverFlow.Web.Areas.User.Controllers
{
    [Area("user")]
    public class QuestionsController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<QuestionsController> _logger;
        private UserManager<ApplicationUser> _userManager;


        public QuestionsController(ILifetimeScope scope,
            ILogger<QuestionsController> logger,UserManager<ApplicationUser> userManager)
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
        public async Task<JsonResult> GetQuestions(QuestionListModel model)
        {
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);

            model.Resolve(_scope);

            var data = await model.GetPagedQuestionsAsync(dataTableModel);

            return Json(data);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var model = _scope.Resolve<QuestionDetailsModel>();
            model.Resolve(_scope);
            await model.LoadAsync(id);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = _scope.Resolve<QuestionCreateModel>();


            var tagsList = await model.GetAvailableTags();
           
            model.MultiTags = new List<SelectListItem>();
            foreach (var tag in tagsList)
            {
                model.MultiTags.Add(new SelectListItem { Text = tag.Name ,Value=tag.Name });
            }

            return View(model);
        }



        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {
      
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    model.UserId = new Guid();
                    model.ResolveAsync(_scope);
                    await model.CreateAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Your Question Added successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating course",
                        Type = ResponseTypes.Danger
                    });
                }
                
            }

            var tagsList = await model.GetAvailableTags();
            model.MultiTags = new List<SelectListItem>();
            foreach (var tag in tagsList)
            {
                model.MultiTags.Add(new SelectListItem { Text = tag.Name, Value = tag.Name });
            }

            model.Details = HttpUtility.HtmlDecode(model.TriedApproach); 
            model.TriedApproach = HttpUtility.HtmlDecode(model.TriedApproach);


            return View(model);
        }


    }
}
