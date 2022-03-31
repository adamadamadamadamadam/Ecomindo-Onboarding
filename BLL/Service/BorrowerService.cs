using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using DAL.Interface;
using DAL.Model;

namespace DLL.Service
{
    public class BorrowerService
    {
        private IUnitOfWork unitOfWork;

        public BorrowerService(IUnitOfWork uow)
        {
            unitOfWork = uow;

        }

        public async Task<List<Borrower>> GetAllBorrower()
        {
            try
            {
                var result = await unitOfWork.BorrowerRepository.GetAll().Include(b => b.Book).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Borrower> GetByID(int id)
        {
            try
            {
                var result = await unitOfWork.BorrowerRepository.GetAll().Include(b => b.Book).FirstOrDefaultAsync(b => b.Id == id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task AddAsync(Borrower input)
        {
            try
            {
                await unitOfWork.BorrowerRepository.AddAsync(input);

                await unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task Update (Borrower input)
        {
            unitOfWork.BorrowerRepository.Update(input);

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
