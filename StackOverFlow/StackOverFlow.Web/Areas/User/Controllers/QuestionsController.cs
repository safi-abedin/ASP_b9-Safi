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
            ILogger<QuestionsController> logger, UserManager<ApplicationUser> userManager)
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


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = _scope.Resolve<QuestionDetailsModel>();
            model.Resolve(_scope);

            if (!id.Equals(null) || id != Guid.Empty)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    user.Reputation = user.Reputation + 2;
                    await _userManager.UpdateAsync(user);
                    await model.LoadAsync(id);


                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Something Went Wrong");
                    return RedirectToAction("Index");

                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = _scope.Resolve<QuestionCreateModel>();

            model.ResolveAsync(_scope);
            var tagsList = await model.GetAvailableTags();

            model.MultiTags = new List<SelectListItem>();
            foreach (var tag in tagsList)
            {
                model.MultiTags.Add(new SelectListItem { Text = tag.Name, Value = tag.Name });
            }

            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {

            model.ResolveAsync(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    model.UserId = user.Id;
                    model.UserEmail = user.Email;
                    await model.CreateAsync();
                    user.Reputation = user.Reputation + 5;
                    await _userManager.UpdateAsync(user);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Your Question Added successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating Question",
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

            model.Details = model.Details;
            model.TriedApproach = model.TriedApproach;


            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(QuestionDetailsModel model)
        {
                model.Resolve(_scope);
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
                var userEmail = user.Email;
                await model.CreateAnswerAsync(userId,userEmail);

                return Redirect($"/User/Questions/Details/{model.Id}");
        }




        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = _scope.Resolve<QuestionEditModel>();

            await model.LoadAsync(id);

            var tagsList = await model.GetAvailableTags();

            model.MultiTags = new List<SelectListItem>();
            foreach (var tag in tagsList)
            {
                model.MultiTags.Add(new SelectListItem { Text = tag.Name, Value = tag.Name });
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(QuestionEditModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveAsync(_scope);
                    await model.EditAsync();
                    var user = await _userManager.GetUserAsync(User);
                    user.Reputation = user.Reputation + 10;
                    await _userManager.UpdateAsync(user);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Your Question Edited successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in Editing Question",
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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<QuestionEditModel>();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    user.Reputation = user.Reputation + 5;
                    await _userManager.UpdateAsync(user);

                    await model.DeleteQuestionAsync(id);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = $"Question Successfully Deleted .",
                        Type = ResponseTypes.Success
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "failed to Delete Question");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = $"There is a Problem Deleting Question .",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> QuestionUpVote(Guid id)
        {

            var model = _scope.Resolve<QuestionVoteModel>();

            model.Resolve(_scope);

            var user = await _userManager.GetUserAsync(User);

            await model.CheckVoteAsync(id, user.Id);


            return Redirect($"/User/Questions/Details/{id}");
        }

        public async Task<IActionResult> QuestionDownVote(Guid id)
        {


            var model = _scope.Resolve<QuestionVoteModel>();

            var user = await _userManager.GetUserAsync(User);

            await model.GiveDownVoteAsync(id, user.Id);

            return Redirect($"/User/Questions/Details/{id}");
        }


        public async Task<IActionResult> AnswerUpVote(Guid id)
        {

            var model = _scope.Resolve<QuestionVoteModel>();

            model.Resolve(_scope);

            var user = await _userManager.GetUserAsync(User);

            await model.CheckVoteAsync(id, user.Id);


            return Redirect($"/User/Questions/Details/{id}");
        }

        public async Task<IActionResult> AnswerDownVote(Guid id)
        {


            var model = _scope.Resolve<QuestionVoteModel>();

            var user = await _userManager.GetUserAsync(User);

            await model.GiveDownVoteAsync(id, user.Id);

            return Redirect($"/User/Questions/Details/{id}");
        }

    }
}
