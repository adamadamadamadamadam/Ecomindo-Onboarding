using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Model;

namespace BLL.Service
{
    public class BookService
    {
        private IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public async Task<List<Book>> GetAllBook()
        {
            try
            {
                var result = await unitOfWork.BookRepository.GetAll().ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Book> GetByID(int id)
        {
            try
            {
                var result = await unitOfWork.BookRepository.GetAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task AddAsync(Book input)
        {
            try
            {
                await unitOfWork.BookRepository.AddAsync(input);

                await unitOfWork.SaveAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task Update(Book input)
        {
            unitOfWork.BookRepository.Update(input);

            await unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                unitOfWork.BookRepository.Delete(b => b.Id == id);

                await unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
