using DAL.Interface;
using DAL.Model;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using MockQueryable.Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.Service;

namespace BLL.Test
{
    public class BookTest
    {
        private IEnumerable<Book> books;
        private Mock<IUnitOfWork> uow;

        public BookTest()
        {
            var bookData = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Name = "book1",
                    LamaPinjam = 10,
                },
                new Book()
                {
                    Id= 2,
                    Name = "book2",
                    LamaPinjam = 20,
                }
            };

            books = bookData;

            uow = MockUnitOfWork();
        }

        public BookService CreateBookService()
        {
            return new BookService(uow.Object);
        }

        public Mock<IUnitOfWork> MockUnitOfWork()
        {
            var mockData = books.AsQueryable().BuildMock();

            var mockUow = new Mock<IUnitOfWork>();

            mockUow.Setup(x => x.BookRepository.GetAll()).Returns(mockData);

            mockUow.Setup(x => x.BookRepository.Get(It.IsAny<int>())).Returns<int>(x => mockData.Where(b => b.Id == x).FirstOrDefault());

            mockUow.Setup(x => x.BookRepository.GetAsync(It.IsAny<int>())).ReturnsAsync((int i) => mockData.Where(b => b.Id == i).FirstOrDefault());

            mockUow.Setup(x => x.BookRepository.Add(It.IsAny<Book>())).Returns((Book book) => { book.Id = 0; return book; });

            mockUow.Setup(x => x.BookRepository.AddAsync(It.IsAny<Book>())).ReturnsAsync((Book book) => { book.Id = 0; return book; });

            mockUow.Setup(x => x.BookRepository.Update(It.IsAny<Book>())).Verifiable();

            mockUow.Setup(x => x.BookRepository.Delete(It.IsAny<Expression<Func<Book, bool>>>())).Verifiable();

            mockUow.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            return mockUow;
        }

        [Fact]
        public async Task GetAll_Success()
        {
            var service = CreateBookService();

            var expected = books;

            var result = await service.GetAllBook();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Get_Success(int id)
        {
            var service = CreateBookService();

            var expected = books.FirstOrDefault(x => x.Id == id);

            var result = await service.GetByID(id);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Add_Success()
        {
            var service = CreateBookService();

            var input = new Book() { Id = 3, Name= "book3"};

            await service.AddAsync(input);
        }

        [Fact]
        public async Task Update_Success()
        {
            var service = CreateBookService();

            var input = new Book() { Id = 3, Name = "book3" };

            await service.Update(input);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var service = CreateBookService();

            await service.Delete(1);
        }
    }
}
