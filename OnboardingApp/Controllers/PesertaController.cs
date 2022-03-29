using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnboardingApp.DTO;
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
    public class PesertaController : ControllerBase
    {
        private IMapper _mapper { get; set; }

        private PesertaService service;

        public PesertaController(TempPesertaRepo repository)
        {
            service = new PesertaService(repository);

            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Peserta, PesertaDTO>();

                    cfg.CreateMap<PesertaDTO, Peserta>();
                });

                _mapper = config.CreateMapper();
            }
        }

        [HttpGet]
        public List<PesertaDTO> Get()
        {
            var res = service.GetAllPeserta();

            return res;
        }
    }
}
