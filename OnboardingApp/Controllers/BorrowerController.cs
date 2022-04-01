using AutoMapper;
using BLL.Service;
using DAL.Interface;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnboardingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowerController : ControllerBase
    {
        private IMapper _mapper { get; set; }

        private BorrowerService service;

        public BorrowerController(IUnitOfWork uow, IRedisService redis)
        {
            service = new BorrowerService(uow, redis);

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

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var res = await service.GetAllBorrower();

            return new OkObjectResult(_mapper.Map<List<BorrowerDTO>>(res));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIDAsync(int id)
        {
            var res = await service.GetByID(id);

            return new OkObjectResult(_mapper.Map<BorrowerDTO>(res));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] BorrowerDTO input)
        {
            await service.Update(_mapper.Map<Borrower>(input));

            return new OkResult();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]BorrowerDTO input)
        {
            await service.AddAsync(_mapper.Map<Borrower>(input));
            return new OkResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await service.Delete(id);

            return new OkResult();
        }
    }
}
