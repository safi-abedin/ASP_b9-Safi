using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure.Membership;
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
        Uri baseAdress = new Uri("https://localhost:7242/api/v3");
        private readonly HttpClient _httpClient;
        private UserManager<ApplicationUser> _userManager;


        public QuestionsController(ILifetimeScope scope,
            ILogger<QuestionsController> logger,UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Create()
        {
            var model = _scope.Resolve<QuestionCreateModel>();


            var tagsList = GetAvailableTags();

           
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
            var selectedTags = model.Tags;

            //var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                    model.UserId = new Guid();
                    var data = JsonConvert.SerializeObject(model);
                    var content =  new StringContent(data,Encoding.UTF8,"application/json");
                    //call api
                    var response =  _httpClient.PostAsync(_httpClient.BaseAddress + "/Questions/Post", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                       return  RedirectToAction("Index");
                    }
            }

            var tagsList = GetAvailableTags();

            // Pass the list of available tags to the view
            model.MultiTags = new List<SelectListItem>();
            foreach (var tag in tagsList)
            {
                model.MultiTags.Add(new SelectListItem { Text = tag.Name, Value = tag.Name });
            }

            model.Details = HttpUtility.HtmlDecode(model.TriedApproach); 
            model.TriedApproach = HttpUtility.HtmlDecode(model.TriedApproach);
            return View(model);
        }


        private List<Tag> GetAvailableTags()
        {
            var tagsList = new List<Tag>
        {
            new Tag { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "CSS" },
            new Tag { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "HTML" },
            new Tag { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "JavaScript" },
            new Tag { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "C#" },
            new Tag { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Python" },
            new Tag { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Java" },
            new Tag { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "React" },
            new Tag { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "SQL" },
            new Tag { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Angular" },
            new Tag { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Node.js" },
            new Tag { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "PHP" },
            new Tag { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Ruby" },
            new Tag { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Bootstrap" },
            new Tag { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "jQuery" },
            new Tag { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Swift" },
            new Tag { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Android" },
            new Tag { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "iOS" },
            new Tag { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Git" },
            new Tag { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "TypeScript" },
            new Tag { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Docker" }
            // Add more tags as needed
        };

            return tagsList;
        }




    }
}
