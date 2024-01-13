using Autofac;
using Library.Web.Areas.Admin.Models;

namespace Library.Web
{
    public class WebModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookCreateModel>().AsSelf();
            builder.RegisterType<BookUpdateModel>().AsSelf();
            builder.RegisterType<BookListModel>().AsSelf();
        }
    }
}
