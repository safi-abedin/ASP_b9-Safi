using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using StackOverFlow.Application.Utilities;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure;
using StackOverFlow.Infrastructure.Membership;
using StackOverFlow.Web.Areas.Admin.Models;
using StackOverFlow.Web.Areas.User.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace StackOverFlow.Web.Areas.User.Controllers
{
    [Area("user")]
    public class QuestionsController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<QuestionsController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;



        public QuestionsController(ILifetimeScope scope,
            ILogger<QuestionsController> logger, UserManager<ApplicationUser> userManager,IEmailService emailService)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
        }


        [AllowAnonymous]
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

        [AllowAnonymous]
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


                    if (user != null)
                    {
                        var claims = await _userManager.GetClaimsAsync(user);
                        if (user.Reputation == null)
                        {
                            user.Reputation = 0;
                        }
                        user.Reputation += 2;
                        if (claims.Count==0 && user.Reputation >= 10)
                        {
                            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("CreateAnswer", "true"));
                        }
                        if (claims.Count <= 1 && user.Reputation >= 20)
                        {
                            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("CreateQuestion", "true"));
                        }
                        if (claims.Count <= 2 && user.Reputation >= 30)
                        {
                            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("EditQuestion", "true"));
                        }
                        if (claims.Count <= 3 &&  user.Reputation >= 40)
                        {
                            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("DeleteQuestion", "true"));
                        }
                        await _userManager.UpdateAsync(user);
                    }


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


        [HttpGet,Authorize(Policy = "CreateQuestion")]
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



        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreateQuestion")]
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



        [HttpPost, ValidateAntiForgeryToken,Authorize(Policy = "CreateAnswer")]
        public async Task<IActionResult> Details(QuestionDetailsModel model)
        {
                model.Resolve(_scope);
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
                var userEmail = user.Email;
                await model.CreateAnswerAsync(userId,userEmail);
                await model.LoadAsync(model.Id);
                var callbackUrl = Url.Action("Details", "Questions", new { area = "user", id = model.Id }, protocol: HttpContext.Request.Scheme);
               _emailService.SendSingleEmail($"hello {model.DisplayName}", model.CreatorEmail, $"New Answer In your Post",
                $"{model.AnswredByDisplayName} answered in your question click here to see the Answer  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return Redirect($"/User/Questions/Details/{model.Id}");
        }




        [HttpGet,Authorize(Policy = "EditQuestion")]
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


        [HttpPost,ValidateAntiForgeryToken, Authorize(Policy = "EditQuestion")]
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

        [HttpGet,Authorize(Policy = "DeleteQuestion")]
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

        [Authorize]
        public async Task<IActionResult> QuestionUpVote(Guid id)
        {

            var model = _scope.Resolve<QuestionVoteModel>();

            model.Resolve(_scope);

            var user = await _userManager.GetUserAsync(User);


            await model.CheckVoteAsync(id, user.Id);


            user.Reputation += 3;
            await _userManager.UpdateAsync(user);


            return Redirect($"/User/Questions/Details/{id}");
        }

        [Authorize]
        public async Task<IActionResult> QuestionDownVote(Guid id)
        {


            var model = _scope.Resolve<QuestionVoteModel>();

            var user = await _userManager.GetUserAsync(User);

            await model.GiveDownVoteAsync(id, user.Id);

            user.Reputation += 3;
            await _userManager.UpdateAsync(user);

            return Redirect($"/User/Questions/Details/{id}");
        }


        [Authorize]
        public async Task<IActionResult> AnswerUpVote(Guid questionId, Guid answerId)
        {

            var model = _scope.Resolve<QuestionVoteModel>();

            model.Resolve(_scope);

            var user = await _userManager.GetUserAsync(User);

            await model.GiveAnswerUpVoteAsync(questionId, user.Id, answerId);

            user.Reputation += 3;
            await _userManager.UpdateAsync(user);


            return Redirect($"/User/Questions/Details/{questionId}");
        }

        [Authorize]
        public async Task<IActionResult> AnswerDownVote(Guid questionId, Guid answerId)
        {


            var model = _scope.Resolve<QuestionVoteModel>();

            var user = await _userManager.GetUserAsync(User);


            await model.GiveAnswerDownVoteAsync(questionId, user.Id, answerId);

            user.Reputation += 3;
            await _userManager.UpdateAsync(user);

            return Redirect($"/User/Questions/Details/{questionId}");
        }

    }
}
