namespace Unnamed.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using Unnamed.Data.Models;

    public interface IBooksService
    {
        Book GetById(int id);

        Book Add(Book book);

        List<Book> GetAll();
    }
}
