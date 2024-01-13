using Autofac;
using Library.Application.Features.Management;

namespace Library.Web.Areas.Admin.Models
{
    public class BookUpdateModel
    {
        public Guid Id { get; set; }


        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorName { get; set; }

        private IBookManagementService _bookManagementService;

        private ILifetimeScope _scope;



        public BookUpdateModel()
        {

        }

        public BookUpdateModel(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _bookManagementService = _scope.Resolve<IBookManagementService>();
        }

        internal async Task LoadAsync(Guid id)
        {
            var book = await _bookManagementService.GetBookAsync(id);

            if(book is not null)
            {
                Id = book.Id;
                Title = book.Title;
                PublishDate = book.PublishDate;
                AuthorName = book.AuthorName;
            }
        }

        internal async Task UpdateBookAsync()
        {
            await _bookManagementService.UpdateBookAsync(Id, Title, PublishDate,AuthorName);
        }
    }
}
