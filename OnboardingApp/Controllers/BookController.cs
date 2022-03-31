using AutoMapper;
using BLL.Service;
using DAL.Interface;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using OnboardingApp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private IMapper _mapper { get; set; }

        private BookService service;

        public BookController(IUnitOfWork uow)
        {
            service = new BookService(uow);

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

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var res = await service.GetAllBook();

            return new OkObjectResult(_mapper.Map<List<BookDTO>>(res));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIDAsync(int id)
        {
            var res = await service.GetByID(id);

            return new OkObjectResult(_mapper.Map<BookDTO>(res));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] BookDTO input)
        {
            await service.Update(_mapper.Map<Book>(input));

            return new OkResult();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] BookDTO input)
        {
            await service.AddAsync(_mapper.Map<Book>(input));
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
