using OnboardingApp.Repository;
using AutoMapper;
using OnboardingApp.Model;
using OnboardingApp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OnboardingApp.Service
{
    public class PesertaService
    {
        TempPesertaRepo repository;
        private IMapper _mapper { get; set; }

        public PesertaService(TempPesertaRepo repo)
        {
            repository = repo;

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

        public List<PesertaDTO> GetAllPeserta()
        {
            try
            {
                var result = repository.Get();
                return _mapper.Map<List<PesertaDTO>>(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PesertaDTO GetByID(int id)
        {
            try
            {
                var result = repository.GetByID(id);
                return _mapper.Map<PesertaDTO>(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(PesertaDTO input)
        {
            try
            {
                repository.Add(_mapper.Map<Peserta>(input));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return repository.Delete(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
