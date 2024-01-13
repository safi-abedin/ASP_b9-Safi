using Autofac;
using Library.Application.Features.Management;

namespace Library.Web.Areas.Admin.Models
{
    public class BookCreateModel
    {

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorName { get; set; }

        private IBookManagementService _bookManagementService;

        private ILifetimeScope _scope;



        public BookCreateModel()
        {
            
        }

        public BookCreateModel(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _bookManagementService = _scope.Resolve<IBookManagementService>();
        }

        internal async Task CreateBookAsync()
        {
            await _bookManagementService.CreateBookAsync(Title, PublishDate,AuthorName);
        }
    }
}
