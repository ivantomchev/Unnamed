namespace Unnamed.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Unnamed.Common;
    using Unnamed.Data;
    using Unnamed.Data.Models;
    using Unnamed.Data.Repositories;
    using Unnamed.Services.Data;

    [TestClass]
    public class BooksServiceTests
    {
        private List<Book> inMemoryBooks;
        private IDbRepository<Book> mockedBooksRepository;
        private ILogger mockedLogger;

        public BooksServiceTests()
        {
            inMemoryBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Greate Expectations", Author = new Author { Id = 2, Name = "Charles Dickens" } },
                new Book { Id = 2, Title = "The Great Gatsby", Author = new Author { Id = 1, Name = "F. Scott Fitzgerald" } },
                new Book { Id = 3, Title = "This Side of Paradise", Author = new Author { Id = 1, Name = "F. Scott Fitzgerald" } },
            };
            var mockContext = new Mock<IUnnamedDbContext>();
            mockContext.Setup(c => c.Set<Book>()).Returns(TestHelpers.MockDbSet(inMemoryBooks).Object);
            mockContext.Setup(c => c.Books).Returns(TestHelpers.MockDbSet(inMemoryBooks).Object);
            this.mockedBooksRepository = new DbRepository<Book>(mockContext.Object);

            var mockedLogger = new Mock<ILogger>();
            mockedLogger.Setup(x => x.LogMessage(It.IsAny<string>())).Callback<string>((s) => Debug.WriteLine(s));
            this.mockedLogger = mockedLogger.Object;
        }

        [TestMethod, TestCategory("BooksService")]
        public void CanAddBook()
        {
            var booksService = new BooksService(mockedBooksRepository, mockedLogger);

            var excpectedCount = 4;

            booksService.Add(new Book { Id = 4, Title = "NewBook" });

            Assert.AreEqual(excpectedCount, inMemoryBooks.Count());
        }

        [TestMethod, TestCategory("BooksService")]
        public void CanGetBookById()
        {
            var booksService = new BooksService(mockedBooksRepository, mockedLogger);

            var excpectedBookId = 1;

            var result = booksService.GetById(excpectedBookId);

            Assert.IsNotNull(result);
            Assert.AreEqual(excpectedBookId, result.Id);
        }

        [TestMethod, TestCategory("BooksService")]
        public void CanGetBooksByAuthorName()
        {
            var booksService = new BooksService(mockedBooksRepository, mockedLogger);

            var excpectedAuthorName = "F. Scott Fitzgerald";
            var excpectedCount = 2;

            var result = booksService.GetAllByAuthorName(excpectedAuthorName);

            Assert.IsNotNull(result);
            Assert.AreEqual(excpectedCount, result.Count());
            Assert.IsTrue(result.All(x => x.Author.Name == excpectedAuthorName));
        }

        [TestMethod, TestCategory("BooksService")]
        public void CanGetAllBooks()
        {
            var booksService = new BooksService(mockedBooksRepository, mockedLogger);

            var expected = inMemoryBooks.Count();

            var result = booksService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), expected);
        }
    }
}
