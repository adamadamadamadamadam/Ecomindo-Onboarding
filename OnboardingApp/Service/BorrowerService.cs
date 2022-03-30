using OnboardingApp.Repository;
using AutoMapper;
using OnboardingApp.Model;
using OnboardingApp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using OnboardingApp.Interface;

namespace OnboardingApp.Service
{
    public class BorrowerService
    {
        private IUnitOfWork unitOfWork;
        private IMapper _mapper { get; set; }

        public BorrowerService(IUnitOfWork uow)
        {
            unitOfWork = uow;

            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Borrower, BorrowerDTO>();
                    cfg.CreateMap<BorrowerDTO, Borrower>();

                    cfg.CreateMap<Book, BookDTO>();
                    cfg.CreateMap<BookDTO, Book>();
                });

                _mapper = config.CreateMapper();
            }
        }

        public async Task<List<BorrowerDTO>> GetAllBorrower()
        {
            try
            {
                var result = await unitOfWork.BorrowerRepository.GetAll().Include(b => b.Book).ToListAsync();
                return _mapper.Map<List<BorrowerDTO>>(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BorrowerDTO> GetByID(int id)
        {
            try
            {
                var result = await unitOfWork.BorrowerRepository.GetAll().Include(b => b.Book).FirstOrDefaultAsync(b => b.Id == id);
                return _mapper.Map<BorrowerDTO>(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task AddAsync(BorrowerDTO input)
        {
            try
            {
                await unitOfWork.BorrowerRepository.AddAsync(_mapper.Map<Borrower>(input));

                await unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task Update (BorrowerDTO input)
        {
            unitOfWork.BorrowerRepository.Update(_mapper.Map<Borrower>(input));

            await unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                unitOfWork.BorrowerRepository.Delete(b => b.Id == id);

                await unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
