using OnboardingApp.Model;
using System.Collections.Generic;

namespace OnboardingApp.Repository
{
    public class TempPesertaRepo
    {
        private List<Peserta> pesertaList;

        public List<Peserta> Get()
        {
            return pesertaList;
        }

        public Peserta GetByID(int id)
        {
            return pesertaList.Find(p => p.Id == id);
        }

        public void Add(Peserta p)
        {
            pesertaList.Add(p);
        }

        public bool Delete(int id)
        {
            int index = pesertaList.FindIndex(p => p.Id == id);
            if (index > -1) {
                pesertaList.RemoveAt(index);
            }
            return index > -1;
        }
    }
}
