namespace Unnamed.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Unnamed.Common;
    using Unnamed.Data.Models;
    using Unnamed.Data.Repositories;
    using Unnamed.Services.Data.Interfaces;

    public class BooksService : IBooksService
    {
        private readonly IDbRepository<Book> booksRepository;
        private readonly ILogger logger;

        public BooksService(IDbRepository<Book> booksRepository, ILogger logger)
        {
            this.booksRepository = booksRepository;
            this.logger = logger;
        }

        public Book Add(Book book)
        {
            var result = this.booksRepository.Add(book);
            this.booksRepository.SaveChanges();

            logger.LogMessage(string.Format("The book with title {0} has been stored successifly exactly at {1} UTC", book.Title, DateTime.UtcNow));

            return result;
        }

        public List<Book> GetAll()
        {
            return this.booksRepository.All()
                .ToList();
        }

        public Book GetById(int id)
        {
            return this.booksRepository.All()
                .SingleOrDefault(x => x.Id == id);
        }

        public List<Book> GetAllByAuthorName(string name)
        {
            return this.booksRepository.All()
                .Where(b => b.Author.Name == name)
                .ToList();
        }
    }
}
