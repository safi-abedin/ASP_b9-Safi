using Autofac;
using Library.Application.Features.Management;
using Library.Infrastructure;
using System.Web;

namespace Library.Web.Areas.Admin.Models
{
    public class BookListModel
    {

        private IBookManagementService _bookManagementService;

        private ILifetimeScope _scope;


        public BookSearch SearchItem { get; set; }



        public BookListModel()
        {

        }

        public BookListModel(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _bookManagementService = _scope.Resolve<IBookManagementService>();
        }

        internal async Task<object> GetTableDataAsync(DataTablesAjaxRequestUtility dataTableModel)
        {
            var data = await _bookManagementService.GetTableDataAsync(
                dataTableModel.PageIndex,
                dataTableModel.PageSize,
                SearchItem.Title,
                SearchItem.PublishDateFrom,
                SearchItem.PublishDateTo,
                dataTableModel.GetSortText(new string[] {"Title","PublishDate","AuthorName"}));


            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.Title),
                            HttpUtility.HtmlEncode(record.PublishDate),
                            HttpUtility.HtmlEncode(record.AuthorName),
                            record.Id.ToString()
                        }).ToArray()
            };
        }

        internal async Task DeleteBookAsync(Guid id)
        {
            await _bookManagementService.DeleteBookAsync(id);
        }
    }
}
