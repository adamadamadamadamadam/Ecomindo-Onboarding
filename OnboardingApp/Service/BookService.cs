using AutoMapper;
using OnboardingApp.DTO;
using OnboardingApp.Interface;
using OnboardingApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardingApp.Service
{
    public class BookService
    {
        private IUnitOfWork unitOfWork;
        private IMapper _mapper { get; set; }

        public BookService(IUnitOfWork uow)
        {
            unitOfWork = uow;

            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Book, BookDTO>();

                    cfg.CreateMap<BookDTO, Book>();
                });

                _mapper = config.CreateMapper();
            }
        }

        public async Task<List<BookDTO>> GetAllBook()
        {
            try
            {
                var result = await unitOfWork.BookRepository.GetAll().ToListAsync();
                return _mapper.Map<List<BookDTO>>(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BookDTO> GetByID(int id)
        {
            try
            {
                var result = await unitOfWork.BookRepository.GetAsync(id);
                return _mapper.Map<BookDTO>(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task AddAsync(BookDTO input)
        {
            try
            {
                await unitOfWork.BookRepository.AddAsync(_mapper.Map<Book>(input));

                await unitOfWork.SaveAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task Update(BookDTO input)
        {
            unitOfWork.BookRepository.Update(_mapper.Map<Book>(input));

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
