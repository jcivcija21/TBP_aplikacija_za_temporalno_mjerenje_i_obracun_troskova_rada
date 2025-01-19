using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Services
{
    public class PozicijaService
    {
        private readonly PozicijaRepository _repository;

        public PozicijaService(PozicijaRepository repository)
        {
            _repository = repository;
        }

        public List<PozicijaDTO> GetAllPozicije()
        {
            return _repository.GetAllPozicije();
        }

        public void AddPozicije(PozicijaDTO zaposlenik)
        {
            _repository.AddPozicija(zaposlenik);
        }

        public void UpdatePozicije(PozicijaDTO zaposlenik)
        {
            _repository.UpdatePozicija(zaposlenik);
        }

        public void DeletePozicija(int pozicijaId)
        {
            _repository.DeletePozicija(pozicijaId);
        }
    }
}
