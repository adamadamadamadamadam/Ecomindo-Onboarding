using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using DAL.Interface;
using DAL.Model;

namespace BLL.Service
{
    public class BorrowerService
    {
        private IUnitOfWork unitOfWork;
        private IRedisService redis;

        public BorrowerService(IUnitOfWork uow, IRedisService red)
        {
            unitOfWork = uow;
            redis = red;
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
                Borrower result;

                result = await redis.GetAsync<Borrower>($"book#{id}");

                if (result == null) { 
                    result = await unitOfWork.BorrowerRepository.GetAll().Include(b => b.Book).FirstOrDefaultAsync(b => b.Id == id);
                    await redis.SetAsync($"book#{id}", result);
                }
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

                await redis.SetAsync($"book#{input.Id}", input);
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

            await redis.SetAsync($"book#{input.Id}", input);
        }

        public async Task Delete(int id)
        {
            try
            {
                unitOfWork.BorrowerRepository.Delete(b => b.Id == id);

                await unitOfWork.SaveAsync();

                await redis.DeleteAsync($"book#{id}");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
