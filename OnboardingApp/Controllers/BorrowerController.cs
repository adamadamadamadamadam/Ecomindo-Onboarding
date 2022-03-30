using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnboardingApp.DTO;
using OnboardingApp.Interface;
using OnboardingApp.Model;
using OnboardingApp.Repository;
using OnboardingApp.Service;
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

        public BorrowerController(IUnitOfWork uow)
        {
            service = new BorrowerService(uow);

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

            return new OkObjectResult(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIDAsync(int id)
        {
            var res = await service.GetByID(id);

            return new OkObjectResult(res);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] BorrowerDTO input)
        {
            await service.Update(input);

            return new OkResult();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]BorrowerDTO input)
        {
            await service.AddAsync(input);
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
