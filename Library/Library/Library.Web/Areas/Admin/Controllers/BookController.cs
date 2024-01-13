using Autofac;
using Library.Infrastructure;
using Library.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class BookController : Controller
    {
        private  ILifetimeScope _scope;
        private  ILogger<BookController> _logger;

        public BookController(ILifetimeScope scope,ILogger<BookController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetBooks(BookListModel model)
        {
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);

            model.Resolve(_scope);

            var data = await model.GetTableDataAsync(dataTableModel);

            return Json(data);
        }


        public IActionResult Create()
        {
            var model = _scope.Resolve<BookCreateModel>();
            return View(model);
        }


        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);

                    await model.CreateBookAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = " Book Created",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Something Went wrong");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = " Book  can not Create ",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Update(Guid id)
        {
            var model = _scope.Resolve<BookUpdateModel>();

            await model.LoadAsync(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);

                    await model.UpdateBookAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = " Book Updated ",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Something Went wrong",ex);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = " Book  can not Update ",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<BookListModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    await model.DeleteBookAsync(id);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = " Book Deleted",
                        Type = ResponseTypes.Success
                    });

                }
                catch (Exception ex)
                {
                    _logger.LogError("Something Went wrong");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = " Book  can not Delete",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return RedirectToAction("Index");
        }

    }
}
